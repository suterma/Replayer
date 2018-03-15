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
        //A class to hold our OpenFileDialog Settings

        //The default settings for the file dialog
        private OfdParamsAttribute m_Settings = new OfdParamsAttribute("All Files (*.*)|*.*", "Open",
                                                                       Environment.SpecialFolder.Desktop);

        public OfdParamsAttribute Settings {
            get { return m_Settings; }
            set { m_Settings = value; }
        }

        //Define a modal editor style and capture the settings from the property
        public override UITypeEditorEditStyle GetEditStyle(
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

        //Do the actual editing
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value) {
            if (context == null || context.Instance == null || provider == null) {
                return value;
            }

            //Initialize the file dialog with our settings
            var dlg = new OpenFileDialog();
            dlg.Filter = m_Settings.Filter;
            dlg.CheckFileExists = true;
            dlg.Title = m_Settings.Title;
            dlg.InitialDirectory = Environment.GetFolderPath(m_Settings.DefaultDirectory);

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

        public class OfdParamsAttribute : Attribute {
            //The File Filter(s) of the open dialog
            private Environment.SpecialFolder m_DefaultDirectory;
            private string m_Filter;

            //The Title of the open dialog
            private string m_Title;

            public string Filter {
                get { return m_Filter; }
                set { m_Filter = value; }
            }

            public string Title {
                get { return m_Title; }
                set { m_Title = value; }
            }

            //The default directory to look into

            public Environment.SpecialFolder DefaultDirectory {
                get { return m_DefaultDirectory; }
                set { m_DefaultDirectory = value; }
            }

            public OfdParamsAttribute(string sFileFilter, string sDialogTitle,
                                      Environment.SpecialFolder defaultDirectory) {
                m_Filter = sFileFilter;
                m_Title = sDialogTitle;
                m_DefaultDirectory = defaultDirectory;
            }
        }
    }
}