using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Replayer.Core;
using Replayer.WinForms.Ui.Properties;

namespace Replayer.WinForms.Ui.Views.StandardMenu {
    /// <summary>
    ///     A Menu view with Standards.
    /// </summary>
    public partial class StandardMenuView : XtraUserControl, IStandardMenuView {
        /// <summary>
        ///     Gets or sets the presenter.
        /// </summary>
        /// <value>The presenter.</value>
        public StandardMenuPresenter Presenter { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="StandardMenuView" /> class.
        /// </summary>
        public StandardMenuView() {
            InitializeComponent();

            //apply appearance
            menuStrip1.Font = Settings.Default.UiFont;

            Presenter = new StandardMenuPresenter(this);

            //wire the actions
            createNewCompilationToolStripMenuItem.Click +=
                (sender, e) => EventBroker.Instance.IssueEvent("Menu:CreateNewCompilationClicked");
            addCueToolStripMenuItem.Click += (sender, e) => EventBroker.Instance.IssueEvent("Menu:AddCueClicked");
            saveCompilationToolStripMenuItem.Click += (sender, e) => EventBroker.Instance.IssueEvent("Menu:SaveFile");
            openCompilationToolStripMenuItem.Click +=
                (sender, e) => EventBroker.Instance.IssueEvent("Menu:OpenCompilationClicked");

            //handle the enabling of menu items
            Application.Idle += Application_Idle;
        }

        private void Application_Idle(object sender, EventArgs e) {
            saveCompilationAsToolStripMenuItem.Enabled = (Core.Model.Instance.Compilation != null);
            saveCompilationToolStripMenuItem.Enabled = (Core.Model.Instance.Compilation != null);
            editCompilationToolStripMenuItem.Enabled = (Core.Model.Instance.Compilation != null);

            editSelectedTrackToolStripMenuItem.Enabled = (Core.Model.Instance.SelectedTrack != null);
            deleteSelectedTrackToolStripMenuItem.Enabled = (Core.Model.Instance.SelectedTrack != null);
            addTrackToolStripMenuItem.Enabled = (Core.Model.Instance.Compilation != null);

            editSelectedCueToolStripMenuItem.Enabled = (Core.Model.Instance.SelectedCue != null);
            deleteSelectedCueToolStripMenuItem.Enabled = (Core.Model.Instance.SelectedCue != null);
            addCueToolStripMenuItem.Enabled = (Core.Model.Instance.SelectedTrack != null);
            createCueHereToolStripMenuItem.Enabled = (Core.Model.Instance.SelectedTrack != null);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            EventBroker.Instance.IssueEvent("Menu:ExitClicked");
        }

        private void CreateCueHereToolStripMenuItemClick(object sender, EventArgs e) {
            EventBroker.Instance.IssueEvent("Menu:CreateCueHereClicked");
        }

        private void DeleteSelectedCueToolStripMenuItemClick(object sender, EventArgs e) {
            EventBroker.Instance.IssueEvent("Menu:DeleteSelectedCue");
        }

        private void EditSelectedTrackToolStripMenuItemClick(object sender, EventArgs e) {
            EventBroker.Instance.IssueEvent("Menu:EditSelectedTrack");
        }

        private void DeleteSelectedTrackToolStripMenuItemClick(object sender, EventArgs e) {
            EventBroker.Instance.IssueEvent("Menu:DeleteSelectedTrack");
        }

        private void SettingsToolStripMenuItemClick(object sender, EventArgs e) {
            EventBroker.Instance.IssueEvent("Menu:SettingsClicked");
        }

        private void uISettingsToolStripMenuItem_Click(object sender, EventArgs e) {
            EventBroker.Instance.IssueEvent("Menu:UiSettingsClicked");
        }

        private void AboutToolStripMenuItem1Click(object sender, EventArgs e) {
            EventBroker.Instance.IssueEvent("Menu:AboutClicked");
        }

        private void EditSelectedCueToolStripMenuItemClick(object sender, EventArgs e) {
            EventBroker.Instance.IssueEvent("Menu:EditSelectedCue");
        }

        private void addTrackToolStripMenuItem_Click(object sender, EventArgs e) {
            EventBroker.Instance.IssueEvent("Menu:AddTrackClicked");
        }

        private void saveCompilationAsToolStripMenuItem_Click(object sender, EventArgs e) {
            EventBroker.Instance.IssueEvent("Menu:SaveFileAs");
        }

        private void editCompilationToolStripMenuItem_Click(object sender, EventArgs e) {
            EventBroker.Instance.IssueEvent("Menu:EditCompilationClicked");
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e) {
            EventBroker.Instance.IssueEvent("Menu:ExportFile");
        }
    }
}