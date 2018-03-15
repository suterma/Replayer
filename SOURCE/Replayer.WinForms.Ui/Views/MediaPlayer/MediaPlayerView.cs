using System.Windows.Forms;
using DevExpress.XtraEditors;
using Replayer.Core.Player;
using NAudioDemo.AudioPlaybackDemo;

namespace Replayer.WinForms.Ui.Views.MediaPlayer {
    /// <summary>
    ///     A Windows Media Player view.
    /// </summary>
    public partial class MediaPlayerView : XtraUserControl, IMediaPlayerView {
        /// <summary>
        ///     The media player within this instance.
        /// </summary>
        private readonly AudioPlaybackPanel _mediaPlayer;

        /// <summary>
        ///     Gets or sets the presenter.
        /// </summary>
        /// <value>The presenter.</value>
        public MediaPlayerPresenter Presenter { get; set; }

        /// <summary>
        ///     Gets the media player.
        /// </summary>
        /// <value>The player.</value>
        public IMediaPlayer MediaPlayer {
            get { return _mediaPlayer; }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MediaPlayerView" /> class.
        /// </summary>
        public MediaPlayerView() {
            InitializeComponent();

            //add media player
            _mediaPlayer = new AudioPlaybackPanel { Dock = DockStyle.Fill };
            Controls.Add(_mediaPlayer);

            Presenter = new MediaPlayerPresenter(this);
        }
    }
}