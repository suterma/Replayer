using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Windows.Forms;

namespace Replayer.WinForms.Ui.Gui {
    /// <summary>
    ///     A path editor for a WinForms UI.
    /// </summary>
    /// <devdoc>
    ///     Code taken from:
    ///     http://www.devfuel.com/2007/03/uitypeeditor-reusable-file-path-editor.html
    /// </devdoc>
    public class UiPathEditor : UITypeEditor {
 
        //The default settings for the file dialog
        private OfdParamsAttribute m_Settings = new OfdParamsAttribute("All Files (*.*)|*.*", "Open",
                                                                       Environment.SpecialFolder.Desktop);

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        public OfdParamsAttribute Settings {
            get { return m_Settings; }
            set { m_Settings = value; }
        }

        
        /// <summary>
        /// Gets the editor style used by the <see cref="M:System.Drawing.Design.UITypeEditor.EditValue(System.IServiceProvider,System.Object)" /> method.
        /// </summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information.</param>
        /// <returns>
        /// A <see cref="T:System.Drawing.Design.UITypeEditorEditStyle" /> value that indicates the style of editor used by the <see cref="M:System.Drawing.Design.UITypeEditor.EditValue(System.IServiceProvider,System.Object)" /> method. If the <see cref="T:System.Drawing.Design.UITypeEditor" /> does not support this method, then <see cref="M:System.Drawing.Design.UITypeEditor.GetEditStyle" /> will return <see cref="F:System.Drawing.Design.UITypeEditorEditStyle.None" />.
        /// </returns>
        public override UITypeEditorEditStyle GetEditStyle(
            //Define a modal editor style and capture the settings from the property
            ITypeDescriptorContext context) {
            if (context == null || context.Instance == null) {
                return base.GetEditStyle(context);
            }

            //Retrieve our settings attribute (if one is specified)
            var sa = (OfdParamsAttribute)context.PropertyDescriptor.Attributes[typeof(OfdParamsAttribute)];
            if (sa != null) {
                m_Settings = sa; //Store it in the editor
            }
            return UITypeEditorEditStyle.Modal;
        }

        /// <summary>
        /// Edits the specified object's value using the editor style indicated by the <see cref="M:System.Drawing.Design.UITypeEditor.GetEditStyle" /> method.
        /// </summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information.</param>
        /// <param name="provider">An <see cref="T:System.IServiceProvider" /> that this editor can use to obtain services.</param>
        /// <param name="value">The object to edit.</param>
        /// <returns>
        /// The new value of the object. If the value of the object has not changed, this should return the same object it was passed.
        /// </returns>
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value) {
            if (context == null || context.Instance == null || provider == null) {
                return value;
            }

            //Initialize the file dialog with our settings
            var dlg = new OpenFileDialog {
                Filter = m_Settings.Filter,
                CheckFileExists = true,
                Title = m_Settings.Title,
                InitialDirectory = Environment.GetFolderPath(m_Settings.DefaultDirectory)
            };

            //Find if the current value is legitimate
            var filename = (string)value;
            if (!File.Exists(filename)) {
                filename = null;
            }

            //Preselect the existing file (if it exists)
            dlg.FileName = filename;
            //Display the dialog and change the value if confirmed
            if (dlg.ShowDialog() == DialogResult.OK) {
                filename = dlg.FileName;
            }
            return filename;
        }

        /// <summary>
        /// Open File Dialog Attributes.
        /// </summary>
        /// <seealso cref="System.Attribute" />
        public class OfdParamsAttribute : Attribute {
            //The File Filter(s) of the open dialog
            private Environment.SpecialFolder m_DefaultDirectory;
            private string m_Filter;

            //The Title of the open dialog
            private string m_Title;

            /// <summary>
            /// Gets or sets the filter.
            /// </summary>
            /// <value>
            /// The filter.
            /// </value>
            public string Filter {
                get { return m_Filter; }
                set { m_Filter = value; }
            }

            /// <summary>
            /// Gets or sets the title.
            /// </summary>
            /// <value>
            /// The title.
            /// </value>
            public string Title {
                get { return m_Title; }
                set { m_Title = value; }
            }

            /// <summary>
            /// Gets or sets the default directory to look into.
            /// </summary>
            /// <value>
            /// The default directory.
            /// </value>
            public Environment.SpecialFolder DefaultDirectory {
                get { return m_DefaultDirectory; }
                set { m_DefaultDirectory = value; }
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="OfdParamsAttribute"/> class.
            /// </summary>
            /// <param name="sFileFilter">The s file filter.</param>
            /// <param name="sDialogTitle">The s dialog title.</param>
            /// <param name="defaultDirectory">The default directory.</param>
            public OfdParamsAttribute(string sFileFilter, string sDialogTitle,
                                      Environment.SpecialFolder defaultDirectory) {
                m_Filter = sFileFilter;
                m_Title = sDialogTitle;
                m_DefaultDirectory = defaultDirectory;
            }
        }
    }
}