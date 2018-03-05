using DevExpress.XtraEditors;

namespace Replayer.WinForms.Ui.Views.Tracks {
    /// <summary>
    ///     The interface to the tracks view.
    /// </summary>
    public interface ITracksView {
        /// <summary>
        ///     Gets access to the tracks visual.
        /// </summary>
        ListBoxControl Tracks { get; }
    }
}