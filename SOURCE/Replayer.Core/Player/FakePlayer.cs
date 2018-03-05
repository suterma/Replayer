using System;
using System.ComponentModel;

namespace Replayer.Core.Player {
    /// <summary>
    ///     A faked player that just does nothing. It is used to initialize
    ///     the model with an existing player, as long as no
    ///     real player is set.
    /// </summary>
    internal class FakePlayer : IMediaPlayer {
        /// <summary>
        ///     Gets or sets the position within the currently loaded media track.
        /// </summary>
        /// <value>The position.</value>
        public TimeSpan Position {
            get { return new TimeSpan(0); }
            set { }
        }

        /// <summary>
        ///     Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        public MediaPlayerState State {
            get { return MediaPlayerState.Paused; }
            set { }
        }

        /// <summary>
        ///     Gets or sets the URL, which represents the current media to use.
        /// </summary>
        /// <value>The URL.</value>
        public string Url {
            get { return string.Empty; }
            set { }
        }

        /// <summary>
        ///     Gets or sets the volume. The value is expected to be in the range
        ///     of 0 to 100.
        /// </summary>
        /// <value>The volume.</value>
        public double Volume {
            get { return 0; }
            set { }
        }

        /// <summary>
        ///     Occurs when a property has changed it's value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Seeks backward within the currently loaded media track.
        /// </summary>
        /// <param name="interval">The interval.</param>
        public void SeekBackward(double interval) {}

        /// <summary>
        ///     Seeks forward within the currently loaded media track.
        /// </summary>
        /// <param name="interval">The interval.</param>
        public void SeekForward(double interval) {}

        /// <summary>
        ///     Toggles the play/pause state.
        /// </summary>
        public void TogglePlayPause() {}
    }
}