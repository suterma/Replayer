using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Replayer.Core.v04.Player;
using Replayer.Core.v04;
using System.Windows.Threading;
using System.ComponentModel;

namespace Replayer.Wpf.Ui
{
    /// <summary>
    /// A media player for use with Replayer
    /// </summary>
    public partial class MediaReplayer : UserControl, IMediaPlayer
    {
        public MediaReplayer()
        {
            InitializeComponent();

            _sliderDispatcher = new DispatcherTimer();
            _sliderDispatcher.Interval = TimeSpan.FromSeconds(2);
            _sliderDispatcher.Tick += new EventHandler(SliderDispatcher_Tick);
        }

        public TimeSpan Position
        {
            get
            {
                return mediaElement1.Position;
            }
            set
            {
                if (mediaElement1.Position == value) return;
                mediaElement1.Position = value;
                FollowSliderAfterMedia(); //Update the slider immediately, do not wait on timed follow
                OnPropertyChanged("Position");
            }
        }

        /// <summary>
        /// Follows the slider position according to the media position
        /// </summary>
        private void FollowSliderAfterMedia()
        {
            PositionSlider.Value = mediaElement1.Position.TotalSeconds;
        }

        /// <summary>
        /// Occurrs when a property has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <remarks>
        /// OnPropertyChanged will raise the PropertyChanged event passing the
        /// source property that is being updated. 
        /// </remarks>
        /// <param name="sender">The sender.</param>
        /// <param name="propertyName">Name of the property.</param>
        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Seeks backward in time by the specified interval.
        /// </summary>
        /// <param name="interval">The interval in [seconds]</param>
        public void SeekBackward(double interval)
        {
            Position.Subtract(new TimeSpan((long)(interval * 10000000)));
        }

        /// <summary>
        /// Seeks forward in time by the specified interval.
        /// </summary>
        /// <param name="interval">The interval in [seconds]</param>
        public void SeekForward(double interval)
        {
            Position.Add(new TimeSpan((long)(interval * 10000000)));
        }

        /// <summary>
        /// Backing field. Start with paused state, as this is the natural state at startup.
        /// </summary>
        private MediaPlayerState _state = MediaPlayerState.Paused;

        public MediaPlayerState State
        {
            get
            {
                return _state;
            }
            set
            {
                if (_state == value) return;
                _state = value;
                switch (value)
                {
                    case MediaPlayerState.Playing:
                        mediaElement1.Play();
                        break;
                    case MediaPlayerState.Paused:
                        mediaElement1.Pause();
                        break;
                    default:
                        break;
                }
                OnPropertyChanged("State");
            }
        }

        public void TogglePlayPause()
        {
            if (State == MediaPlayerState.Playing)
            {
                State = MediaPlayerState.Paused;
            }
            else if (State == MediaPlayerState.Paused)
            {
                State = MediaPlayerState.Playing;

            }
        }

        public string Url
        {
            get
            {
                return mediaElement1.Source.AbsolutePath;
            }
            set
            {
                Uri newSource = null; //source to use
                try
                {
                    newSource = new Uri(value, UriKind.Absolute);
                    if (
                        (mediaElement1.Source != null) && //had a valid path?
                        (newSource.AbsolutePath.Equals(mediaElement1.Source.AbsolutePath)) //and is equal?
                        )
                    {
                        return; //because no change
                    }
                }
                catch (Exception ex)
                {
                    if (ex is UriFormatException ||
                        ex is ArgumentNullException)
                    {
                        newSource = null; //if not working, just use no file
                        if (mediaElement1.Source == null) return; //no change?
                    }
                    else
                    {
                        throw; //we can not handle this
                    }
                }
                mediaElement1.Source = newSource;
                mediaElement1.Position = new TimeSpan(0); //to have an initial value set.
                mediaElement1.Play(); mediaElement1.Pause(); //play/pause causes a load, which in turn triggers the media updated event, that will update the slider maximum (Long chain though...)
                State = MediaPlayerState.Paused;
                OnPropertyChanged("Url");
            }
        }

        public double Volume
        {
            get
            {
                return mediaElement1.Volume;
            }
            set
            {
                if (mediaElement1.Volume == value) return;
                mediaElement1.Volume = value;
                OnPropertyChanged("Url");
            }
        }

        /// <summary>
        /// A timer for regular ui updates, where necessary.
        /// </summary>
        private DispatcherTimer _sliderDispatcher;

        void SliderDispatcher_Tick(object sender, EventArgs e)
        {
            if (!isDragging)
            {
                FollowSliderAfterMedia();
            }
        }


        private void PositionSlider_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            isDragging = true;
        }

        private void PositionSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            isDragging = false;
            Position = TimeSpan.FromSeconds(PositionSlider.Value);
        }


        /// <summary>
        /// Remember whether we are currently in a dragging operation.
        /// </summary>
        /// <remarks>This helps to avoid flickering with the position caused
        /// by the slider timer update.</remarks>
        bool isDragging = false;

        /// <summary>
        /// Called when the value of the PositionSlider control changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PositionSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TimeSpan ts = TimeSpan.FromSeconds(e.NewValue);
            this.textBlock1.Text =                String.Format("{0:00}:{1:00}:{2:00}",                ts.Hours, ts.Minutes, ts.Seconds);

            ///do not propagate the slider value to the position property, because this is probably a programmatic update.
            ///The position updates triggered by the user are propagated in ui-related methods.
        }

        //private void ButtonPause_Click(object sender, RoutedEventArgs e)
        //{
        //    Model.Instance.Player.State = Core.v04.Player.MediaPlayerState.Paused;
        //}

        //private void ButtonStart_Click(object sender, RoutedEventArgs e)
        //{
        //    Model.Instance.Player.State = Core.v04.Player.MediaPlayerState.Playing;
        //}

        private void mediaElement1_MediaOpened(object sender, RoutedEventArgs e)
        {
            PositionSlider.Maximum = mediaElement1.NaturalDuration.TimeSpan.TotalSeconds;

            TimeSpan mediaLenght = mediaElement1.NaturalDuration.TimeSpan;
            textBlockMediaLength.Text = String.Format("{0:00}:{1:00}:{2:00}", mediaLenght.Hours, mediaLenght.Minutes, mediaLenght.Seconds);
            _sliderDispatcher.Start();//start updating the slider
            FollowSliderAfterMedia(); //update immediately for the first time.
        }

        private void mediaElement1_MediaEnded_1(object sender, RoutedEventArgs e)
        {
            _sliderDispatcher.Stop(); //stop updating the slider
        }

        private void PositionSlider_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //after a mouse click, propagate the new position to the player
            Position = TimeSpan.FromSeconds(PositionSlider.Value);
        }
    }
}
