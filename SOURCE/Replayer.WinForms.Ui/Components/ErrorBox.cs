using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Replayer.WinForms.Ui.Gui {
    /// <summary>
    ///     Displays an Error Message.
    /// </summary>
    public partial class ErrorBox : XtraForm {
        /// <summary>
        ///     Prepares the message.
        /// </summary>
        /// <param name="message"></param>
        public ErrorBox(string message, string title = "") {
            InitializeComponent();
            labelControl1.Text = message;
            this.Text = title;
        }

        /// <summary>
        ///     Shows the message and returns a DialogResult.
        /// </summary>
        /// <returns></returns>
        public DialogResult Show() {
            return ShowDialog();
        }

        public static DialogResult Show(string message) {
            return new ErrorBox(message).Show();
        }

        internal static DialogResult Show(string message, string title) {
            return new ErrorBox(message, title).Show();
        }
    }
}