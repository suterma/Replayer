using System;
using System.ComponentModel;

namespace Replayer.Core.Player {
    /// <summary>
    ///     The state of the media player
    /// </summary>
    public enum MediaPlayerState {
        /// <summary>
        ///     The player is playing.
        /// </summary>
        Playing,

        /// <summary>
        ///     The player is paused.
        /// </summary>
        Paused
    }

    /// <summary>
    ///     Defines the behaviour and properties of a media player,
    ///     for use within the Replayer application.
    /// </summary>
    /// <devdoc>
    ///     The player definition is an integral part of the model, but
    ///     defined as a separate class.
    ///     It can be viewed as a component of it's
    ///     own.
    /// </devdoc>
    public interface IMediaPlayer : INotifyPropertyChanged {
        /// <summary>
        ///     Gets or sets the position within the currently loaded media track.
        /// </summary>
        /// <remarks>
        ///     While playing, the position is expected to get updated
        ///     at about every half a second.
        /// </remarks>
        /// <value>The position.</value>
        TimeSpan Position { get; set; }

        /// <summary>
        ///     Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        MediaPlayerState State { get; set; }

        /// <summary>
        ///     Gets or sets the URL, which represents the current media to use.
        /// </summary>
        /// <value>The URL, or null if no media should get used now.</value>
        string Url { get; set; }

        /// <summary>
        ///     Gets or sets the volume. The value is expected to be in the range
        ///     of 0 to 100.
        /// </summary>
        /// <value>The volume.</value>
        double Volume { get; set; }

        /// <summary>
        ///     Occurs when a property has changed it's value.
        /// </summary>
        event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Seeks backward within the currently loaded media track.
        /// </summary>
        /// <param name="interval">The interval, in [seconds].</param>
        void SeekBackward(double interval);

        /// <summary>
        ///     Seeks forward within the currently loaded media track.
        /// </summary>
        /// <param name="interval">The interval, in [seconds].</param>
        void SeekForward(double interval);

        /// <summary>
        ///     Toggles the play/pause state.
        /// </summary>
        void TogglePlayPause();
    }
}