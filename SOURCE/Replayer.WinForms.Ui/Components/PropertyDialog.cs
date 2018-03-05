using DevExpress.XtraEditors;

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
    }
}