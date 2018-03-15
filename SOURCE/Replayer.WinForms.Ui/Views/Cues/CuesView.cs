using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.ViewInfo;
using Replayer.Core;
using Replayer.WinForms.Ui.Properties;

namespace Replayer.WinForms.Ui.Views.Cues {
    /// <summary>
    ///     A view for cues in a compilation.
    /// </summary>
    public partial class CuesView : XtraUserControl, IView<CuesPresenter>, ICuesView {
        /// <summary>
        ///     Gets the cues visual.
        /// </summary>
        /// <value>The cues.</value>
        public ListBoxControl Cues {
            get { return _lbcCues; }
        }

        /// <summary>
        ///     Gets or sets the presenter.
        /// </summary>
        /// <value>The presenter.</value>
        public CuesPresenter Presenter {
            get
; set
;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CuesView" /> class.
        /// </summary>
        public CuesView() {
            InitializeComponent();
            Presenter = new CuesPresenter(this);
            _lbcCues.ContextMenuStrip = _cmsCues;

            //apply appearance
            _cmsCues.Font = Settings.Default.UiFont;

            Application.Idle += Application_Idle;
        }

        private void Application_Idle(object sender, EventArgs e) {
            editToolStripMenuItem.Enabled = (Core.Model.Instance.SelectedCue != null);
            deleteToolStripMenuItem.Enabled = (Core.Model.Instance.SelectedCue != null);
            moveDownToolStripMenuItem.Enabled = (Core.Model.Instance.SelectedCue != null);
            moveUpToolStripMenuItem.Enabled = (Core.Model.Instance.SelectedCue != null);
        }

        /// <summary>
        ///     Handles the clicked event of the Edit menu item in the cms context menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditToolStripMenuItemClick(object sender, EventArgs e) {
            EventBroker.Instance.IssueEvent("Menu:EditSelectedCue");
        }

        /// <summary>
        ///     Handles the clicked event of the delete menu entry in the cms context menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteToolStripMenuItemClick(object sender, EventArgs e) {
            EventBroker.Instance.IssueEvent("Menu:DeleteSelectedCue");
        }


        /// <summary>
        ///     Handles the MouseDown event of the cues ListBoxControl.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _lbcCuesMouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                //select the item under the mouse
                var vi = _lbcCues.GetViewInfo() as BaseListBoxViewInfo;
                BaseListBoxViewInfo.ItemInfo ii = vi.GetItemInfoByPoint(e.Location);
                if (ii != null) {
                    _lbcCues.SelectedIndex = ii.Index;
                    Text = ii.Index.ToString();
                }
            }
        }

        private void moveUpToolStripMenuItem_Click(object sender, EventArgs e) {
            EventBroker.Instance.IssueEvent("Menu:MoveUpSelectedCue");
        }

        private void moveDownToolStripMenuItem_Click(object sender, EventArgs e) {
            EventBroker.Instance.IssueEvent("Menu:MoveDownSelectedCue");
        }
    }
}