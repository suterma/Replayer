namespace Replayer.WinForms.Ui.Views.MediaPlayer {
    /// <summary>
    ///     A presenter for a media player view.
    /// </summary>
    public class MediaPlayerPresenter : Presenter<IMediaPlayerView> {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Presenter&lt;T&gt;"></see> class.
        /// </summary>
        /// <param name="view">The view.</param>
        public MediaPlayerPresenter(IMediaPlayerView view)
            : base(view) {
            //register player in the model
            Core.Model.Instance.Player = View.MediaPlayer;
        }
    }
}