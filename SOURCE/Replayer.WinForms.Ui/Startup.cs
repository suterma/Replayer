using Microsoft.VisualBasic.ApplicationServices;

namespace Replayer.WinForms.Ui {
    /// <summary>
    /// A startup splash form
    /// </summary>
    /// <seealso cref="Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase" />
    public class Startup : WindowsFormsApplicationBase {
        /// <summary>
        /// When overridden in a derived class, allows a designer to emit code that initializes the splash screen.
        /// </summary>
        protected override void OnCreateSplashScreen() {
            SplashScreen = new SplashForm();
        }

        /// <summary>
        /// When overridden in a derived class, allows a designer to emit code that configures the splash screen and main form.
        /// </summary>
        protected override void OnCreateMainForm() {
            MainForm = new ReplayerApplicationForm();
        }
    }
}