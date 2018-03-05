using DevExpress.XtraEditors;

namespace Replayer.WinForms.Ui.Views.Cues {
    /// <summary>
    ///     Defines a view for cues.
    /// </summary>
    public interface ICuesView {
        /// <summary>
        ///     Gets the cues visual.
        /// </summary>
        /// <value>The cues.</value>
        ListBoxControl Cues { get; }
    }
}