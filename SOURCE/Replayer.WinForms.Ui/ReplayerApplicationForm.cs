using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using log4net;
using Replayer.Core;
using Replayer.Core.Input;
using Replayer.WinForms.Ui.Components;
using Replayer.WinForms.Ui.Gui;
using Replayer.WinForms.Ui.Properties;

namespace Replayer.WinForms.Ui {
    /// <summary>
    ///     The main form for the RePlayer Application.
    /// </summary>
    public partial class ReplayerApplicationForm : XtraForm {

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        /// <summary>
        ///     Initializes a new instance of the <see cref="ReplayerApplicationForm" /> class.
        /// </summary>
        public ReplayerApplicationForm() {
            Log.Info("### Replayer startup ###");

            ApplyAppearance();
            //Before creating any visual components. Do not call before creating this form, because this will mess the spacing between the controls

            new ReplayerApplicationEventHandler(); //create event handler

            InitializeComponent();

            //Handle application title
            Core.Model.Instance.PropertyChanged += (sender, e) => {
                if (
                    (e.PropertyName.Equals("Compilation"))
                    ) //changed?
                {
                    SetTitle();
                }
            };
            SetTitle();

            //install keyboard hook
            KeyPress += Form_KeyPressed;
            KeyDown += Form_KeyDown;
            KeyUp += Form_KeyUp;
            KeyPreview = true; //handle on form level first before any control

            //handle app exit
            FormClosing += ReplayerLiveApplicationForm_FormClosing;

            //initialize the busy bar
            Core.Model.Instance.BusyIndicator = new BusyForm(this);

            Shown += ReplayerLiveApplicationForm_Shown;
            Log.Info("Replayer ready!");

        }

        private void ReplayerLiveApplicationForm_Shown(object sender, EventArgs e) {
            HandleCommandlineArguments();
        }

        /// <summary>
        ///     Handles the command line arguments.
        /// </summary>
        private void HandleCommandlineArguments() {
            //handle command line arguments
            string compilationFilenameArgument = string.Empty;
            if (
                (AppDomain.CurrentDomain != null) &&
                (AppDomain.CurrentDomain.SetupInformation != null) &&
                (AppDomain.CurrentDomain.SetupInformation.ActivationArguments != null) &&
                (AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData != null)
                //activation data available?
                ) {
                string[] activationData = AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData;
                compilationFilenameArgument = (from arg in activationData
                                               where
                                                   arg.Contains(".rez") ||
                                                   arg.Contains(".rex")
                                               select arg).FirstOrDefault();
                //get the argument with a valid compilation extension, if any
            }
            try {
                //execute the command line if necessary
                if (!String.IsNullOrEmpty(compilationFilenameArgument)) //is available?
                {

                    //if we got a command line argument then try to load that as a compilation
                    Core.Model.Instance.Retrieve(compilationFilenameArgument);

                }
                else //no command line args
                {
                    string lastLoaded = Settings.Default.LastLoadedCompilationPath;
                    if (!string.IsNullOrEmpty(lastLoaded)) {
                        //load the compilation from last time, if possible
                        if (File.Exists(lastLoaded)) //there is a file at that place?
                        {
                            Core.Model.Instance.Retrieve(Settings.Default.LastLoadedCompilationPath);
                        }
                    }
                    else {
                        //first use, try to load a local (in the same directory) compilation
                        var firstLocalCompilation = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.rez", SearchOption.TopDirectoryOnly)
                            .Concat(Directory.GetFiles(Directory.GetCurrentDirectory(), "*.rex", SearchOption.TopDirectoryOnly)).FirstOrDefault();
                        if (!string.IsNullOrEmpty(firstLocalCompilation)) {
                            Core.Model.Instance.Retrieve(firstLocalCompilation);
                        }
                    }
                }
            }
            catch (Exception) {
                //silently catch the failed retrieval, and leave the compilation empty. The user may later manually open a collection.
            }
        }

        /// <summary>
        ///     Handles the application exit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReplayerLiveApplicationForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (
                (Core.Model.Instance.Compilation != null) &&
                (Core.Model.Instance.Compilation.IsDirty)
                ) //do we have unsaved changes?
            {
                if (new Question("Save changes to compilation before exit?").Ask().Equals(DialogResult.OK)) {
                    //yes, then save
                    EventBroker.Instance.IssueEvent("Menu:SaveFile");
                }
            }

            //save the path of the loaded compilation to the settings for later retrieval
            if (Core.Model.Instance.Compilation != null) //there is any?
            {
                Settings.Default.LastLoadedCompilationPath = Core.Model.Instance.Compilation.Url;
            }
            else {
                Settings.Default.LastLoadedCompilationPath = String.Empty;
            }
            Settings.Default.Save();
            Log.Info("### Replayer shutdown ###");

        }

        /// <summary>
        ///     Handles the PropertyChanged event of the Model.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///     The <see cref="System.ComponentModel.PropertyChangedEventArgs" /> instance containing the event data.
        /// </param>
        /// <summary>
        ///     Sets the title of this form.
        /// </summary>
        private void SetTitle() {
            if (InvokeRequired) {
                // Reinvoke the same method if necessary        
                BeginInvoke(new MethodInvoker(delegate { SetTitle(); }));
            }
            else {
                if (Core.Model.Instance.Compilation != null) //thre is any?
                {
                    Text = String.Format("Replayer - {0} - {1}",
                                         Core.Model.Instance.Compilation.Title,
                                         Path.GetFileName(Core.Model.Instance.Compilation.Url)
                        );
                }
            }
        }

        /// <summary>
        ///     Applies the appearance as defined in the model.
        /// </summary>
        private static void ApplyAppearance() {
            // Access the Default LookAndFeel. 
            //UserLookAndFeel defaultLF = UserLookAndFeel.Default;
            //defaultLF.UseWindowsXPTheme = false;
            //defaultLF.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            //defaultLF.SkinName = "Black";
            //defaultLF.UseDefaultLookAndFeel = true;

            //apply appearance
            AppearanceObject.DefaultFont = Settings.Default.UiFont;

            //DevExpress.LookAndFeel.Helpers.FormUserLookAndFeel.Default.Assign(defaultLF);
            //DevExpress.LookAndFeel.UserLookAndFeel.Default.UseDefaultLookAndFeel = true;
        }


        /// <summary>
        ///     Handles the KeyPressed event of the Form control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">
        ///     The <see cref="System.Windows.Forms.KeyPressEventArgs" /> instance containing the event data.
        /// </param>
        private void Form_KeyPressed(object sender, KeyPressEventArgs e) {
            Core.Model.Instance.KeyboardInputHandler.HandleKeyPress(new SimpleKeyPressEventArgs {
                Handled = e.Handled,
                KeyChar = e.KeyChar
            }); //push the key to the handler
            e.Handled = true; //do not allow handling elsewhere
        }

        /// <summary>
        ///     Handles the KeyUp event of the Form control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///     The <see cref="System.Windows.Forms.KeyEventArgs" /> instance containing the event data.
        /// </param>
        private void Form_KeyUp(object sender, KeyEventArgs e) {
            if (e.Alt) //is modified?
            {
                return; //do not handle here, let pass further
            }
            if (e.KeyCode == Keys.Delete) //delete?
            {
                return; //do not handle here, let pass further
            }
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up) {
                return; //do not handle here, let pass further
            }
            Core.Model.Instance.KeyboardInputHandler.HandleKeyUp(new SimpleKeyEventArgs { Handled = e.Handled });
            e.Handled = true; //do not allow handling elsewhere
        }

        /// <summary>
        ///     Handles the KeyDown event of the Form control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///     The <see cref="System.Windows.Forms.KeyEventArgs" /> instance containing the event data.
        /// </param>
        private void Form_KeyDown(object sender, KeyEventArgs e) {
            if (e.Alt) //is modified?
            {
                return; //do not handle here, let pass further
            }
            if (e.KeyCode == Keys.Delete) //delete?
            {
                return; //do not handle here, let pass further
            }
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up) {
                return; //do not handle here, let pass further
            }


            //do let the input handler handle this key
            Core.Model.Instance.KeyboardInputHandler.HandleKeyDown
                (new SimpleKeyEventArgs {
                    Handled = e.Handled
                }
                );
            e.Handled = true; //do not allow handling elsewhere
        }
    }
}