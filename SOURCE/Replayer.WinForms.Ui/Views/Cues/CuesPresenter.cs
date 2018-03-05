using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Replayer.Core.Player;
using Replayer.Model;

namespace Replayer.WinForms.Ui.Views.Cues {
    /// <summary>
    ///     A presenter for a cues view.
    /// </summary>
    public class CuesPresenter : Presenter<CuesView> {
        /// <summary>
        ///     Cues to monitor for changes.
        /// </summary>
        /// <remarks>
        ///     This is held for internal reference; To unregister properly from previously
        ///     monitored collections.
        /// </remarks>
        private ObservableCollection<Cue> _cuesToMonitor;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Presenter&lt;T&gt;"></see> class.
        /// </summary>
        /// <param name="view">The view.</param>
        public CuesPresenter(CuesView view)
            : base(view) {
            //handle model property changes
            Core.Model.Instance.PropertyChanged += Model_PropertyChanged;

            MonitorCuesCollection();


            View.Cues.SelectedIndexChanged += Cues_SelectedIndexChanged;

            View.Cues.DoubleClick += CuesList_DoubleClick;

            //show initial values, if available
            ExhibitCues();

            Core.Model.Instance.PropertyChanged += Instance_PropertyChanged;

            //maintain information about passed cues
            View.Cues.DrawItem += Cues_DrawItem;
        }

        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName.Equals("SelectedCue")) {
                SetVisualSelectedCue();
            } else if (e.PropertyName.Equals("SelectedTrack")) {
                ExhibitCues();
                MonitorCuesCollection();
            } else if (e.PropertyName.Equals("Compilation")) {
                ExhibitCues();
            }
        }

        /// <summary>
        ///     Handles the DoubleClick event of the CuesList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///     The <see cref="System.EventArgs" /> instance containing the event data.
        /// </param>
        private void CuesList_DoubleClick(object sender, EventArgs e) {
            //go back to the start of the cue and start playing immediately
            Core.Model.Instance.LoadSelectedCue();
            Core.Model.Instance.Player.State = MediaPlayerState.Playing;
        }

        /// <summary>
        ///     Handles the PropertyChanged event of the Model.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Instance_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName.Equals("Player")) //there is a new player?
            {
                //follow the new Player
                Core.Model.Instance.Player.PropertyChanged += Player_PropertyChanged;
            }
        }

        /// <summary>
        ///     Handles the draw event for individual cue items in the listbox control.
        /// </summary>
        /// <remarks>
        ///     This allows to change the properties of items to draw.
        ///     Currently we use this to mark already played items.
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cues_DrawItem(object sender, ListBoxDrawItemEventArgs e) {
            if ((e.Item is DisplayCue) && (Core.Model.Instance.SelectedCue != null)) //we are eligible to mark something
            {
                bool isPassed = (((e.Item as DisplayCue).Item.Time) < Core.Model.Instance.Player.Position.TotalSeconds);
                //this item is passed?
                bool isAfterSelectedCue = (((e.Item as DisplayCue).Item.Time) > Core.Model.Instance.SelectedCue.Time);
                //this item is after the current cue?
                if (
                    isPassed &&
                    isAfterSelectedCue &&
                    (e.State == DrawItemState.None)
                    ) //passed and done, but not selected
                {
                    //shade that
                    e.Appearance.BackColor = Color.FromArgb(64, Color.Gray);
                }
            }
        }

        /// <summary>
        ///     Handles the propertyChanged event of the media player.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Player_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName.Equals("Position")) //position has changed?
            {
                View.Cues.Refresh();
            }
        }

        /// <summary>
        ///     Desctructor
        /// </summary>
        ~CuesPresenter() {
            Core.Model.Instance.PropertyChanged -= Model_PropertyChanged;
            View.Cues.SelectedIndexChanged -= Cues_SelectedIndexChanged;
            View.Cues.DoubleClick -= CuesList_DoubleClick;
        }


        /// <summary>
        ///     Monitors the cues collection for changes.
        /// </summary>
        private void MonitorCuesCollection() {
            //handle changes of the current cues in use(additions/removals)
            if (Core.Model.Instance.SelectedTrack != null) //there is any track whose cues are to monitor?
            {
                if (_cuesToMonitor != null) //we had cues to monitor?
                {
                    _cuesToMonitor.CollectionChanged -= Cues_CollectionChanged; //unwire from previously monitored cues
                }
                _cuesToMonitor = Core.Model.Instance.SelectedTrack.Cues; //keep reference
                _cuesToMonitor.CollectionChanged += Cues_CollectionChanged; //monitor the new ones
            }
        }

        private void Cues_SelectedIndexChanged(object sender, EventArgs e) {
            if (View.Cues.SelectedIndices.Count > 1) //more than one selected
            {
                ///force selection of a single item
                ///forcing is necessary because we have a multiselect list. This is needed to also allow "no selection". See
                ///http://devexpress.com/Support/Center/p/Q102277.aspx
                int index = View.Cues.SelectedIndex;
                View.Cues.UnSelectAll();
                View.Cues.SetSelected(index, true);
                return;
            }

            if (View.Cues.SelectedIndex >= 0) //valid selection?
            {
                //get that cue
                Cue selectedCue = (View.Cues.Items[View.Cues.SelectedIndex] as DisplayCue).Item;
                Core.Model.Instance.SelectedCue = selectedCue; //update the model  
            }
        }


        /// <summary>
        ///     Handles the CollectionChanged event of the Cues collection.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///     The <see cref="System.Collections.Specialized.NotifyCollectionChangedEventArgs" /> instance containing the event data.
        /// </param>
        private void Cues_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            ExhibitCues();
        }

        /// <summary>
        ///     Sets the visual selected cue.
        /// </summary>
        private void SetVisualSelectedCue() {
            if (View.Cues.Items == null) //selection not possible?
            {
                return;
            }

            DisplayCue visualToSelect = (from item in View.Cues.Items.Cast<DisplayCue>()
                                         where item.Item.Equals(Core.Model.Instance.SelectedCue)
                                         select item).SingleOrDefault();

            if (visualToSelect != null) //any matching found?
            {
                if (!(visualToSelect.Equals(View.Cues.SelectedItem))) //not yet selected?
                {
                    View.Cues.SelectedItem = visualToSelect;
                }
            } else {
                //none is matching? Probably the selection points to another track, so refresh everyting
                ExhibitCues();
            }
        }

        /// <summary>
        ///     Exhibits the cues.
        /// </summary>
        private void ExhibitCues() {
            if (View.InvokeRequired) {
                // Reinvoke the same method if necessary        
                View.BeginInvoke(new MethodInvoker(delegate { ExhibitCues(); }));
            } else {
                if (
                    (Core.Model.Instance.Compilation == null) || //no compilation available?
                    (Core.Model.Instance.SelectedTrack == null) || //no track selected?
                    (Core.Model.Instance.SelectedTrack.Cues == null) //none available?
                    ) {
                    View.Cues.Items.Clear();
                } else {
                    View.Cues.SelectedIndexChanged -= Cues_SelectedIndexChanged;
                    //unwire to keep from selecting an item while the data is manipulated
                    View.Cues.Items.Clear();
                    DisplayCue[] data = (from cue in Core.Model.Instance.SelectedTrack.Cues
                                         select
                                             new DisplayCue(
                                             "(" + ToTimeString(cue.Time) + ") [" + cue.Shortcut + "] " +
                                             cue.Description, cue)).ToArray();
                    View.Cues.Items.AddRange(data);
                    View.Cues.SelectedIndexChanged += Cues_SelectedIndexChanged; //wire back to track changes again
                }
            }
        }

        /// <summary>
        ///     Formats the given amount of secons into a formatted time string.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private string ToTimeString(double secs) {
            TimeSpan t = TimeSpan.FromSeconds(secs);
            return string.Format("{0:D2}:{1:D2}.{2:D1}",
                                 t.Minutes,
                                 t.Seconds,
                                 t.Milliseconds);
        }
    }
}