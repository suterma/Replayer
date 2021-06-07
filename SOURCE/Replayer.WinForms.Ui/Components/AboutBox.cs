using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Replayer.WinForms.Ui.Components {
    /// <summary>
    ///     An about box for the Replayer Face
    /// </summary>
    public partial class AboutBox : Form {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AboutBox" /> class.
        /// </summary>
        public AboutBox() {
            InitializeComponent();
            Text = String.Format("About {0}", AssemblyTitle);
            labelProductName.Text = AssemblyProduct;
            labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
            labelCopyright.Text = AssemblyCopyright;
            labelCompanyName.Text = AssemblyCompany;
        }

        #region Assembly Attribute Accessors

        /// <summary>
        /// Gets the assembly title.
        /// </summary>
        /// <value>
        /// The assembly title.
        /// </value>
        public string AssemblyTitle {
            get {
                object[] attributes =
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0) {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "") {
                        return titleAttribute.Title;
                    }
                }
                return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        /// <summary>
        /// Gets the assembly version.
        /// </summary>
        /// <value>
        /// The assembly version.
        /// </value>
        public string AssemblyVersion {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }

        /// <summary>
        /// Gets the assembly description.
        /// </summary>
        /// <value>
        /// The assembly description.
        /// </value>
        public string AssemblyDescription {
            get {
                object[] attributes =
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0) {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        /// <summary>
        /// Gets the assembly product.
        /// </summary>
        /// <value>
        /// The assembly product.
        /// </value>
        public string AssemblyProduct {
            get {
                object[] attributes =
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0) {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        /// <summary>
        /// Gets the assembly copyright.
        /// </summary>
        /// <value>
        /// The assembly copyright.
        /// </value>
        public string AssemblyCopyright {
            get {
                object[] attributes =
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0) {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        /// <summary>
        /// Gets the assembly company.
        /// </summary>
        /// <value>
        /// The assembly company.
        /// </value>
        public string AssemblyCompany {
            get {
                object[] attributes =
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0) {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

        #endregion

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            // Specify that the link was visited.
            this.linkLabel1.LinkVisited = true;

            // Navigate to a URL.
            System.Diagnostics.Process.Start("https://github.com/suterma/Replayer/releases");
        }
    }
}