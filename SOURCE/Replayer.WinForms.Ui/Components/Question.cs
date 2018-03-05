using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Replayer.WinForms.Ui.Gui {
    /// <summary>
    ///     Asks the user a question.
    /// </summary>
    /// <param name="question"></param>
    public partial class Question : XtraForm {
        /// <summary>
        ///     Prepares the question.
        /// </summary>
        /// <param name="question"></param>
        public Question(string question) {
            InitializeComponent();
            labelControl1.Text = question;
        }

        /// <summary>
        ///     Asks the question and returns the answer as a DialogResult.
        /// </summary>
        /// <returns></returns>
        public DialogResult Ask() {
            return ShowDialog();
        }
    }
}