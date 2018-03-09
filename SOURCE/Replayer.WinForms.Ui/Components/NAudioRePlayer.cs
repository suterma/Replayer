using System;
using System.ComponentModel;
using System.Windows.Forms;
using Replayer.Core.Player;

namespace Replayer.WinForms.Common {
    /// <summary>
    ///     A Media Player using anNAudio instance which is
    ///     usable as media player within the
    ///     RePlayer application
    /// </summary>
    public partial class NAudioRePlayer : UserControl, IMediaPlayer {
        /// <summary>
        ///     A timer for updating the Position while playing is active.
        /// </summary>
        private readonly Timer _positionUpdater = new Timer();

        /// <summary>
        ///     Backing field.
        /// </summary>
        private MediaPlayerState _state;


        /// <summary>
        ///     Gets or sets the position within the currently loaded media track.
        /// </summary>
        /// <value>The position.</value>
        public TimeSpan Position {
            get {
                return new TimeSpan((long)(axWindowsMediaPlayer.Ctlcontrols.currentPosition * 10000000));
                //convert from ticks to seconds
            }
            set {
                //skip to position
                axWindowsMediaPlayer.Ctlcontrols.pause();
                axWindowsMediaPlayer.Ctlcontrols.currentPosition = value.TotalSeconds;

                //continue at new place if required.
                if (State.Equals(MediaPlayerState.Playing)) {
                    axWindowsMediaPlayer.Ctlcontrols.play();
                }
                OnPropertyChanged(this, "Position");
            }
        }

        /// <summary>
        ///     Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        public MediaPlayerState State {
            get { return _state; }
            set {
                if (_state == value) //no change?
                {
                    return;
                }

                _state = value; //remember value

                //apply state to wrapped player
                switch (value) {
                    case MediaPlayerState.Playing:
                        axWindowsMediaPlayer.Ctlcontrols.play();
                        _positionUpdater.Start();
                        break;
                    case MediaPlayerState.Paused:
                        axWindowsMediaPlayer.Ctlcontrols.pause();
                        _positionUpdater.Stop();
                        break;
                    default:
                        break;
                }
                OnPropertyChanged(this, "State");
            }
        }

        /// <summary>
        ///     Gets or sets the URL, which represents the current media to use.
        /// </summary>
        /// <value>The URL.</value>
        public string Url {
            get { return axWindowsMediaPlayer.URL; }
            set {
                if (!axWindowsMediaPlayer.URL.Equals(value)) //value really changed?
                {
                    axWindowsMediaPlayer.URL = value; //set new value, causing the meadia to get loaded
                    OnPropertyChanged(this, "Url");
                }
            }
        }

        /// <summary>
        ///     Gets or sets the volume. The value is expected to be in the range
        ///     of 0 to 100.
        /// </summary>
        /// <value>The volume.</value>
        public double Volume {
            get { return axWindowsMediaPlayer.settings.volume; }
            set {
                double volume = value;
                volume = Math.Max(volume, 0);
                volume = Math.Min(volume, 100);
                axWindowsMediaPlayer.settings.volume = (int) value;
                OnPropertyChanged(this, "Volume");
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NAudioRePlayer" /> class.
        /// </summary>
        public NAudioRePlayer() {
            InitializeComponent();

            //do not autoplay
            axWindowsMediaPlayer.settings.autoStart = false;

            //update the position if required.
            _positionUpdater.Interval = 500;
            _positionUpdater.Tick += PositionUpdater_Tick;
        }

        /// <summary>
        ///     Occurs when a property has changed it's value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Seeks forward within the currently loaded media track.
        /// </summary>
        /// <param name="interval">The interval.</param>
        public void SeekForward(double interval) {
            Position = Position.Add(new TimeSpan((long) (interval*10000000)));
        }

        /// <summary>
        ///     Seeks backward within the currently loaded media track.
        /// </summary>
        /// <param name="interval">The interval.</param>
        public void SeekBackward(double interval) {
            Position = Position.Subtract(new TimeSpan((long) (interval*10000000)));
        }

        /// <summary>
        ///     Toggles the play/pause state.
        /// </summary>
        public void TogglePlayPause() {
            switch (State) {
                case MediaPlayerState.Playing:
                    State = MediaPlayerState.Paused;
                    break;
                case MediaPlayerState.Paused:
                    State = MediaPlayerState.Playing;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        ///     Announces a new position on the respective property.
        /// </summary>
        /// <remarks>
        ///     This should only get called while the
        ///     player is in playing state.
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PositionUpdater_Tick(object sender, EventArgs e) {
            ///just announce a change, the getter of the corresponding
            ///property is returning the exact position upon the get request.
            OnPropertyChanged(this, "Position");
        }

        /// <summary>
        ///     Called when [property changed].
        /// </summary>
        /// <remarks>
        ///     OnPropertyChanged will raise the PropertyChanged event passing the
        ///     source property that is being updated.
        /// </remarks>
        /// <param name="sender">The sender.</param>
        /// <param name="propertyName">Name of the property.</param>
        private void OnPropertyChanged(object sender, string propertyName) {
            if (PropertyChanged != null) {
                PropertyChanged(sender, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}