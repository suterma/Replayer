using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace Replayer.WinForms.Ui.Gui {
    /// <summary>
    ///     A form, presenting changeable properties.
    /// </summary>
    public partial class PropertyDialog : XtraForm {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PropertyDialog" /> class.
        /// </summary>
        /// <param name="settings">The properties object.</param>
        /// <param name="title">The title.</param>
        public PropertyDialog(object settings, string title) {
            InitializeComponent();
            Text = title;
            propertyGrid1.SelectedObject = settings;
        }

        private void PropertyDialog_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Escape) {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }

            if (e.KeyCode == Keys.Enter) {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }


    }
}