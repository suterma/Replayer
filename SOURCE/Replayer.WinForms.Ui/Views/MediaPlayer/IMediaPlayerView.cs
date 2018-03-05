using Replayer.Core.Player;

namespace Replayer.WinForms.Ui.Views.MediaPlayer {
    /// <summary>
    ///     Defines the functionality of a Media Player view
    /// </summary>
    public interface IMediaPlayerView {
        /// <summary>
        ///     Gets the media player.
        /// </summary>
        /// <value>The player.</value>
        IMediaPlayer MediaPlayer { get; }
    }
}