using System;
using System.Windows.Forms;

namespace Replayer.WinForms.Ui.Gui {
    /// <summary>
    ///     A ready-made dialog to browse for media files for a track.
    /// </summary>
    public class OpenTrackFileDialog {
        /// <summary>
        ///     Gets or sets the inner dialog, which is configured and shown
        ///     according to the specific need for track media files.
        /// </summary>
        /// <value>
        ///     The inner dialog.
        /// </value>
        private OpenFileDialog InnerDialog { get; set; }

        /// <summary>
        ///     Gets the file names of all selected files in the dialog box.
        /// </summary>
        public string[] FileNames {
            get { return InnerDialog.FileNames; }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="OpenTrackFileDialog" /> class.
        /// </summary>
        public OpenTrackFileDialog() {
            InnerDialog = new OpenFileDialog();
            InnerDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            InnerDialog.Filter = "Track files (*.mp3)|*.mp3";
            InnerDialog.FilterIndex = 1;
        }

        /// <summary>
        ///     Provides a simple way for asking the user for media file paths.
        /// </summary>
        /// <param name="multiselect">If set to true, allows selecting multiple files.</param>
        /// <returns></returns>
        public DialogResult ShowDialog(bool multiselect) {
            InnerDialog.Multiselect = multiselect;
            return InnerDialog.ShowDialog();
        }
    }
}