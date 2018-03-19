using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Replayer.Core.Data;
using Replayer.Core.Input;
using Replayer.Core.Player;
using Replayer.Core.Properties;
using Replayer.Core.UserInteraction;
using Replayer.Model;

namespace Replayer.Core {
    /// <summary>
    ///     The Model (in MVP) of the RePlayer application.
    /// </summary>
    /// <remarks>
    ///     This is where all the relevant states are stored.
    ///     <para>
    ///         This model depends on a player, with the IMediaPlayer interface,
    ///         on which it acts upon, as necesseary by the operations provided by
    ///         this model.
    ///     </para>
    ///     <para>
    ///         Behaviour for cues:
    ///         The selected cue denotes a cue, on which future operations
    ///         take place. When a cue gets selected by the setter of the property
    ///         it also gets Loaded into the player, even when it was already selected before.
    ///         However, the selected cue event is only fired, when it actually
    ///         changes.
    ///     </para>
    /// </remarks>
    public sealed class Model : INotifyPropertyChanged {
        /// <summary>
        ///     The model
        /// </summary>
        private static readonly Model instance = new Model();

        /// <summary>
        ///     The background worker for retrieveing a compilation.
        /// </summary>
        private readonly BackgroundWorker _retriever = new BackgroundWorker();

        /// <summary>
        ///     Backing store.
        /// </summary>
        private ICompilation _Compilation;


        /// <summary>
        ///     Backing field for the BusyIndicator property.
        /// </summary>
        private IBusyIndicator _busyIndicator = new FakeBusyIndicator();


        /// <summary>
        ///     Backing Field.
        /// </summary>
        private IMediaPlayer _player = new FakePlayer();

        /// <summary>
        ///     Backing field.
        /// </summary>
        private Cue _selectedCue;

        /// <summary>
        ///     Backing field
        /// </summary>
        private Track _selectedTrack;

        /// <summary>
        ///     The translator for commands into player state changes.
        /// </summary>
        private CommandToPlayerTranslator _translator;

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit

        /// <summary>
        ///     Gets the keyboard input handler.
        /// </summary>
        /// <remarks>
        ///     There must be a hook in the process that feeds
        ///     all relevant key presses into this handler. Preferably, this
        ///     happens at the form level, but could also be provided by
        ///     a dedicated control.
        /// </remarks>
        /// <value>The keyboard input handler.</value>
        public KeyInputHandler KeyboardInputHandler { get; private set; }

        /// <summary>
        ///     Gets or sets the current Compilation.
        /// </summary>
        public ICompilation Compilation {
            get { return _Compilation; }
            set {
                if (_Compilation == value) {
                    return;
                }
                _Compilation = value;
                OnPropertyChanged("Compilation");
            }
        }

        /// <summary>
        ///     Gets or sets the busy indicator.
        /// </summary>
        public IBusyIndicator BusyIndicator {
            get { return _busyIndicator; }
            set {
                if (_busyIndicator == value) {
                    return;
                }
                _busyIndicator = value;
                OnPropertyChanged("BusyIndicator");
            }
        }

        /// <summary>
        ///     Gets or sets the player.
        /// </summary>
        public IMediaPlayer Player {
            get { return _player; }
            set {
                if (_player == value) {
                    return;
                }
                _player = value;
                OnPropertyChanged("Player");
            }
        }


        /// <summary>
        ///     Gets the instance of the model.
        /// </summary>
        /// <value>The instance.</value>
        public static Model Instance {
            get { return instance; }
        }

        /// <summary>
        ///     Gets or sets the settings.
        /// </summary>
        /// <value>
        ///     The settings.
        /// </value>
        public Settings Settings { get; set; }

        /// <summary>
        ///     Gets or sets the selected cue, on a selected track of the
        ///     loaded compilation.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         This is a convenience property for accessing the current compilation's
        ///         properties.
        ///     </para>
        ///     When get, if no cue is selected on a selected track, this returns null.
        ///     <para>
        ///         When set, the selected track is changed to the track which contains the cue
        ///         when not yet matching.
        ///     </para>
        /// </remarks>
        /// <value>The selected cue.</value>
        public Cue SelectedCue {
            get { return _selectedCue; }
            set {
                if (SelectedCue != value) //really changed? (setter guard)
                {
                    if (value != null) //there is someting to select?
                    {
                        FollowSelectedTrackFor(value); //keep track up to date
                        _selectedCue = value;
                        LoadSelectedCue();
                    }
                    else {
                        //unselection is requested
                        //dont use the setter accessor for this property, as we are already in there
                        _selectedCue = null; //simply unselect the current selected cue                         
                    }
                    OnPropertyChanged("SelectedCue"); //report the change
                }
                else //selected the same cue again
              {
                    LoadSelectedCue(); //just reload, but there is no actual change for this property
                }
            }
        }

        /// <summary>
        ///     Gets or sets the selected track for the current compilation.
        /// </summary>
        /// <remarks>
        ///     This is a convenience property that uses the public
        ///     properties of the collection.
        ///     <para>
        ///         When set, the selected cue of the selected track's cues
        ///         is set to null, reflecting an yet unselected state.
        ///     </para>
        /// </remarks>
        /// <value>The selected track.</value>
        public Track SelectedTrack {
            get { return _selectedTrack; }
            set {
                if (SelectedTrack != value) //really changed?
                {
                    _selectedTrack = value;
                    SelectedCue = null; //unselect any cue, as we dont know what to select for this new track first
                    OnPropertyChanged("SelectedTrack");

                    //TODO check with winforms app, this code is new
                    if (_selectedTrack != null) //any selected?
                    {
                        //provide absolute url to the player
                        Player.Url = _Compilation.Find(_selectedTrack);
                    }
                    else //no track selected
                  {
                        Player.Url = null; //do not use a media
                    }
                }
            }
        }

        static Model() { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Model" /> class.
        /// </summary>
        private Model() {
            Settings = new Settings(); //retrieve Settings

            KeyboardInputHandler = new KeyInputHandler(Settings.KeyboardShortcutCharacterTimeout_sec);
            _translator = new CommandToPlayerTranslator(KeyboardInputHandler);
        }

        /// <summary>
        ///     Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Handles the SelectedChanged event of the Cues control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///     The <see cref="System.EventArgs" /> instance containing the event data.
        /// </param>
        private void Cues_SelectedChanged(object sender, EventArgs e) {
            LoadSelectedCue();
        }

        /// <summary>
        ///     Moves the player to the start
        ///     position of the currently selected cue.
        /// </summary>
        /// <remarks>This honors the PrePlayDuration.</remarks>
        public void LoadSelectedCue() {
            if (SelectedCue == null) //no cue selected?
            {
                //stop any playing and remove any media from the player
                Player.State = MediaPlayerState.Paused;
                Player.Url = "";
                return;
            }

            //load the track
            String trackUrl = Compilation.Find(SelectedTrack);

            if (!String.Empty.Equals(trackUrl)) //found an url? (yoda condition to avoid null reference exception)
            {
                //go to cue point
                Player.State = MediaPlayerState.Paused;
                Player.Url = trackUrl;
                Player.Position = TimeSpan.FromSeconds((SelectedCue.Time - Settings.PrePlayDuration_Seconds));
            }
            else {
                throw new FileNotFoundException("Media for this cue point was not found!");
            }
        }

        /// <summary>
        ///     Releases unmanaged resources and performs other cleanup operations before the
        ///     <see cref="Model" /> is reclaimed by garbage collection.
        /// </summary>
        ~Model() {
            Settings.Save(); //at application exit, save all eventual changes
        }

        /// <summary>
        ///     Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        private void OnPropertyChanged(String propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     Creates and adds a new cue at the current player position.
        /// </summary>
        /// <remarks>
        ///     This should not change the currently selected cue and the player itself.
        ///     Usually, the player would actually play the music when this is called,
        ///     and the player should run through.
        /// </remarks>
        public void CreateCueHere() {
            if ((SelectedTrack != null)) //there is a track selected?
            {
                //add a cue to the current track
                TimeSpan position = Player.Position; //get position of the cursor 

                var cue = new Cue {
                    Description = String.Format("Cue at {0}",
                                                (DateTime.MinValue +
                                                 new TimeSpan(0, 0, position.Minutes, position.Seconds,
                                                              position.Milliseconds)).ToString("mm:ss.fff")),
                    Shortcut = GetNextCueShortcut(),
                    Time = position.TotalSeconds
                };
                InsertCueToTrackOrdered(cue);
                Compilation.IsDirty = true;
                //explicitly set to dirty TODO this new dirty state should get recognized by the model itself.

                //OnPropertyChanged("SelectedCue"); //explicitly raise now to reflect the change to the cues in general
            }
        }

        /// <summary>
        ///     Inserts the specified cue to the currently selected track, keeping the timely order.
        /// </summary>
        /// <remarks>
        ///     This method assumes, that the existing cues of the selected track
        ///     are already ordered by time, and the new one is just inserted
        ///     at the correct position.
        /// </remarks>
        /// <param name="cueToInsert"></param>
        private void InsertCueToTrackOrdered(Cue cueToInsert) {
            //get the index of the last cue whose time is smaller than the new one
            Cue previousCue = (from cue in SelectedTrack.Cues
                               orderby cue.Time descending
                               where cue.Time < cueToInsert.Time
                               select cue).FirstOrDefault();
            if (previousCue == null) //no previous?
            {
                SelectedTrack.Cues.Insert(0, cueToInsert); //put into first position
            }
            else {
                int previousIndex = SelectedTrack.Cues.IndexOf(previousCue);
                SelectedTrack.Cues.Insert(previousIndex + 1, cueToInsert); //insert after previous
            }
            return;
        }

        /// <summary>
        ///     Gets the next cue shortcut, which is a string representation
        ///     of an integer value, which is as close to zero as possible,
        ///     while being unique among the existing cue shortcuts of the selected
        ///     track, if any.
        /// </summary>
        /// <returns></returns>
        private string GetNextCueShortcut() {
            IEnumerable<string> shortcuts = from cue in SelectedTrack.Cues
                                            select cue.Shortcut;

            //find hightest numerical shortcut
            var numberShortcuts = new List<int>();
            foreach (string shortcut in shortcuts) {
                int number;
                bool parsed = int.TryParse(shortcut, out number);
                if (parsed) {
                    numberShortcuts.Add(number);
                }
            }
            int highestShortcut = numberShortcuts.OrderByDescending(number => number).FirstOrDefault();
            return (highestShortcut + 1).ToString();
        }

        /// <summary>
        ///     Moves the selected cue in the cues list, by the specified amount of steps and direction.
        /// </summary>
        /// <param name="steps"></param>
        public void MoveSelectedCue(int steps) {
            Cue selectedCue = Instance.SelectedCue; //remember
            SelectedTrack.Cues.Move(Instance.SelectedCue, steps);
            SelectedCue = selectedCue; //select again for the user convenience
        }

        /// <summary>
        ///     Moves the selected track in the track list, by the specified amount of steps and direction.
        /// </summary>
        /// <param name="steps"></param>
        public void MoveSelectedTrack(int steps) {
            Track selectedTrack = Instance.SelectedTrack; //remember
            Compilation.Tracks.Move(Instance.SelectedTrack, steps);
            SelectedTrack = selectedTrack; //select again for the user convenience
        }

        /// <summary>
        ///     Removes the selected cue from the selected track of the loaded compilation, if there is any.
        /// </summary>
        public void RemoveSelectedCue() {
            if (SelectedTrack != null) //any one selected?
            {
                SelectedTrack.Cues.Remove(SelectedCue);
                SelectedCue = null;
                OnPropertyChanged("SelectedTrack");
                //intended to force a visual update of the cues of the selected track wich are now changed
            }
        }

        /// <summary>
        ///     Removes the selected track from the set of tracks of the loaded compilation, if there is any.
        /// </summary>
        public void RemoveSelectedTrack() {
            if (
                (Compilation != null) && //there is any?
                (SelectedTrack != null) //and one selected at all?
                ) {
                Track trackToRemove = SelectedTrack; //keep reference
                SelectedTrack = null;
                int indexOfTrackToRemove = Compilation.Tracks.IndexOf(trackToRemove);
                //Compilation.Tracks.Remove(trackToRemove);   //             Why does removal give a nullreferenceexception in the Accordion?
                Compilation.Tracks.RemoveAt(indexOfTrackToRemove);
                OnPropertyChanged("SelectedTrack"); //intended to force a visual update of the tracks
            }
        }


        /// <summary>
        ///     Updates the selected track if necessary for the currently
        ///     selected cue. This simply keeps the model internal
        ///     state consistent.
        /// </summary>
        /// <param name="cue">The cue.</param>
        private void FollowSelectedTrackFor(Cue cue) {
            if (cue == null) //nothing selected?
            {
                return; //nothing to follow
            }

            if (
                (SelectedTrack != null) && //track selected at all?
                (SelectedTrack.Cues.Contains(cue)) //contained in currently selected cue?
                ) {
                return; //because everything is already correct, no update needed
            }

            #region We must try to find it in another track

            //first, find the track which contains the selected cue
            Track containingTrack = (from track in Compilation.Tracks
                                     where track.Cues.Contains(cue)
                                     select track).FirstOrDefault();

            if (containingTrack != null) //any track contains this cue?
            {
                //make the found track selected in the model
                //Dont use setter method in the model, since this would first unselect the cue!!! Directly do this here on our own to not cause a recurive loop.
                if (_selectedTrack != containingTrack) //really changed?
                {
                    _selectedTrack = containingTrack;
                    OnPropertyChanged("SelectedTrack"); //explicitly report change, as we did not invoke the setter
                }
            } //else the selected cue is a stray cue, not belonging to this compilation. Just ignore here.           

            #endregion
        }


        /// <summary>
        ///     Retrieves the compliation at the specified path
        /// </summary>
        /// <param name="compilationPath"></param>
        public void Retrieve(string compilationPath) {
            _retriever.DoWork += Retriever_DoWork;
            _retriever.RunWorkerCompleted += Retriever_RunWorkerCompleted;
            _retriever.RunWorkerAsync(compilationPath);

            BusyIndicator.IsBusyWith("Loading compilation");
        }

        /// <summary>
        ///     Handles the RunWorkerCompleted event of the Retriever worker.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///     The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs" /> instance containing the event data.
        /// </param>
        private void Retriever_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            BusyIndicator.IsNoMoreBusy();
        }

        /// <summary>
        ///     Handles the DoWork event of the Retriever worker.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///     The <see cref="System.ComponentModel.DoWorkEventArgs" /> instance containing the event data.
        /// </param>
        private void Retriever_DoWork(object sender, DoWorkEventArgs e) {
            Compilation = CompilationFactory.Retrieve(e.Argument as string);
        }
    }
}