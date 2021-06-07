using System;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Replayer.Core.UserInteraction;

namespace Replayer.WinForms.Ui.Components {
    /// <summary>
    ///     A form that indicates business to the user.
    /// </summary>
    public partial class BusyForm : XtraForm, IBusyIndicator {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BusyForm" /> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        public BusyForm(Form owner) {
            Owner = owner;
            InitializeComponent();
        }

        /// <summary>
        ///     Determines whether this is busy, with the specified action.
        ///     The implementor should now indicate this ongoing task to the user.
        /// </summary>
        /// <param name="activityDescription"></param>
        public void IsBusyWith(string activityDescription) {
            Owner.Cursor = Cursors.WaitCursor;
            Owner.Enabled = false;
            Text = activityDescription;
            ShowDialog();
        }

        /// <summary>
        ///     Indicates that this is no more busy.
        /// </summary>
        public void IsNoMoreBusy() {
            Owner.Enabled = true;
            Owner.Cursor = Cursors.Default;
            Close();
        }

        /// <summary>
        ///     Handles the Click event of the labelControl1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///     The <see cref="System.EventArgs" /> instance containing the event data.
        /// </param>
        private void labelControl1_Click(object sender, EventArgs e) {
            ShowBrowser(@"https://replayer.ch");
        }

        /// <summary>
        ///     Shows the browser.
        /// </summary>
        /// <param name="link">The link.</param>
        public void ShowBrowser(string link) {
            Process process = new Process();
            process.StartInfo.FileName = link;
            process.StartInfo.Verb = "open";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            try {
                process.Start();
            }
            catch { }
        }
    }
}