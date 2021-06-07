using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.ViewInfo;
using Replayer.Core;
using Replayer.WinForms.Ui.Properties;

namespace Replayer.WinForms.Ui.Views.Tracks {
    /// <summary>
    ///     A view for showing tracks.
    /// </summary>
    public partial class TracksView : XtraUserControl, IView<TracksPresenter>, ITracksView {
        /// <summary>
        ///     Gets access to the tracks visual.
        /// </summary>
        /// <value></value>
        public ListBoxControl Tracks {
            get { return _lbcTracks; }
        }

        /// <summary>
        ///     Gets or sets the presenter.
        /// </summary>
        /// <value>The presenter.</value>
        public TracksPresenter Presenter { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TracksView" /> class.
        /// </summary>
        public TracksView() {
            InitializeComponent();
            Presenter = new TracksPresenter(this);
            _lbcTracks.ContextMenuStrip = _cmsTracks;

            //apply appearance
            _cmsTracks.Font = Settings.Default.UiFont;

            Application.Idle += Application_Idle;
        }

        /// <summary>
        ///     Handles the Idle event of the Application.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///     The <see cref="System.EventArgs" /> instance containing the event data.
        /// </param>
        private void Application_Idle(object sender, EventArgs e) {
            editToolStripMenuItem.Enabled = (Core.Model.Instance.SelectedTrack != null);
            deleteToolStripMenuItem.Enabled = (Core.Model.Instance.SelectedTrack != null);
            moveDownToolStripMenuItem.Enabled = (Core.Model.Instance.SelectedTrack != null);
            moveUpToolStripMenuItem.Enabled = (Core.Model.Instance.SelectedTrack != null);
        }

        /// <summary>
        ///     Handles the Click event of the EditToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///     The <see cref="System.EventArgs" /> instance containing the event data.
        /// </param>
        private void EditToolStripMenuItem_Click(object sender, EventArgs e) {
            EventBroker.Instance.IssueEvent("Menu:EditSelectedTrack");
        }

        /// <summary>
        ///     Handles the clicked event of the delete menu entry in the cms context menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e) {
            EventBroker.Instance.IssueEvent("Menu:DeleteSelectedTrack");
        }

        /// <summary>
        ///     Handles the MouseDown event of the _lbcTracks control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///     The <see cref="System.Windows.Forms.MouseEventArgs" /> instance containing the event data.
        /// </param>
        private void _lbcTracks_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                //select the item under the mouse
                BaseListBoxViewInfo vi = _lbcTracks.GetViewInfo() as BaseListBoxViewInfo;
                BaseListBoxViewInfo.ItemInfo ii = vi.GetItemInfoByPoint(e.Location);
                if (ii != null) {
                    _lbcTracks.SelectedIndex = ii.Index;
                    Text = ii.Index.ToString();
                }
            }
        }

        /// <summary>
        ///     Handles the Click event of the moveUpToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///     The <see cref="System.EventArgs" /> instance containing the event data.
        /// </param>
        private void moveUpToolStripMenuItem_Click(object sender, EventArgs e) {
            EventBroker.Instance.IssueEvent("Menu:MoveUpSelectedTrack");
        }

        /// <summary>
        ///     Handles the Click event of the moveDownToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///     The <see cref="System.EventArgs" /> instance containing the event data.
        /// </param>
        private void moveDownToolStripMenuItem_Click(object sender, EventArgs e) {
            EventBroker.Instance.IssueEvent("Menu:MoveDownSelectedTrack");
        }

        private void _cmsTracks_Opening(object sender, System.ComponentModel.CancelEventArgs e) {

        }

        /// <summary>
        ///     Handles the Click event of the cloneToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///     The <see cref="System.EventArgs" /> instance containing the event data.
        /// </param>
        private void cloneToolStripMenuItem_Click(object sender, EventArgs e) {
            EventBroker.Instance.IssueEvent("Menu:CloneSelectedTrack");
        }
    }
}