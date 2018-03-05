using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Replayer.Core.Player;
using Replayer.Model;

namespace Replayer.WinForms.Ui.Views.Tracks {
    /// <summary>
    ///     A presenter for a tracks view.
    /// </summary>
    public class TracksPresenter : Presenter<TracksView> {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TracksPresenter" /> class.
        /// </summary>
        /// <param name="view">The view.</param>
        public TracksPresenter(TracksView view)
            : base(view) {
            Core.Model.Instance.PropertyChanged += Model_PropertyChanged;
            View.Tracks.SelectedIndexChanged += Tracks_SelectedIndexChanged;
            View.KeyDown += View_KeyDown;
            View.Tracks.DoubleClick += Tracks_DoubleClick;

            //show initial values, if available
            ExhibitNewCompilation();
        }

        /// <summary>
        ///     Handles the PropertyChanged event of the Model.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///     The <see cref="System.ComponentModel.PropertyChangedEventArgs" /> instance containing the event data.
        /// </param>
        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName.Equals("Compilation")) {
                ExhibitNewCompilation();
            } else if (e.PropertyName.Equals("SelectedTrack")) {
                var items = View.Tracks.DataSource as List<DisplayTrack>;
                View.Tracks.SelectedItem =
                    (from item in items where item.Model.Equals(Core.Model.Instance.SelectedTrack) select item)
                        .SingleOrDefault(); //default in case no track is selected in the model
            }
        }

        /// <summary>
        ///     Handles the DoubleClick event of the Tracks control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///     The <see cref="System.EventArgs" /> instance containing the event data.
        /// </param>
        private void Tracks_DoubleClick(object sender, EventArgs e) {
            //start the player with the first cue of the track if possible
            ///it is assumed that the expected track was already selected by the changed selection index
            Cue firstCue = Core.Model.Instance.SelectedTrack.Cues.FirstOrDefault();
            if (firstCue != null) // there is one?
            {
                Core.Model.Instance.SelectedCue = firstCue;
                Core.Model.Instance.Player.State = MediaPlayerState.Playing; //start playing the selected track
            } else {
                //just start at the beginning of the track
                Core.Model.Instance.Player.Position = new TimeSpan(0);
                Core.Model.Instance.Player.State = MediaPlayerState.Playing;
            }
        }

        /// <summary>
        ///     Handles the KeyDown event of the View control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///     The <see cref="System.Windows.Forms.KeyEventArgs" /> instance containing the event data.
        /// </param>
        private void View_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Delete) //user wants to delete track?
            {
                Track trackToDelete = Core.Model.Instance.SelectedTrack;
                //the track to delete, which the user must have had selected previously
                if (trackToDelete != null) //we have a selected track 
                {
                    Core.Model.Instance.SelectedCue = null; //unselect cue, if any
                    trackToDelete.Cues.Clear(); //first remove the cues
                    Core.Model.Instance.Compilation.Tracks.Remove(trackToDelete);
                }
                e.Handled = true;
            }
        }

        /// <summary>
        ///     Handles the SelectedIndexChanged event of the Tracks control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///     The <see cref="System.EventArgs" /> instance containing the event data.
        /// </param>
        private void Tracks_SelectedIndexChanged(object sender, EventArgs e) {
            if (View.Tracks.SelectedIndex >= 0) //valid selection?
            {
                //get that track
                Track selectedTrack =
                    (View.Tracks.DataSource as List<DisplayTrack>).ElementAt(View.Tracks.SelectedIndex).Model;
                Core.Model.Instance.SelectedTrack = selectedTrack; //update the model  
            }
        }


        /// <summary>
        ///     Exhibits the tracks.
        /// </summary>
        private void ExhibitTracks() {
            if (View.InvokeRequired) {
                // Reinvoke the same method if necessary        
                View.BeginInvoke(new MethodInvoker(delegate { ExhibitTracks(); }));
            } else {
                if (
                    (Core.Model.Instance.Compilation == null) || //no compilation available?
                    (Core.Model.Instance.Compilation.Tracks == null) || //none available?
                    (Core.Model.Instance.Compilation.Tracks.Count == 0) //none available?
                    ) {
                    View.Tracks.DataSource = null;
                } else {
                    View.Tracks.DataSource = (from track in Core.Model.Instance.Compilation.Tracks
                                              select new DisplayTrack(track)).ToList();
                    View.Tracks.DisplayMember = "Name";
                }
            }
        }


        /// <summary>
        ///     Exhibits the new compilation.
        /// </summary>
        private void ExhibitNewCompilation() {
            ExhibitTracks();
            if (Core.Model.Instance.Compilation != null) //one available?
            {
                //keep track of changes of the tracks in the compilation
                Core.Model.Instance.Compilation.Tracks.CollectionChanged += (sender, e) => ExhibitTracks();
            }
        }
    }
}