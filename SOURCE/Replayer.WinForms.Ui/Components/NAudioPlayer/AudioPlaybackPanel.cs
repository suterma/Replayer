using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using NAudioDemo.Utils;
using Replayer.Core.Player;

namespace NAudioDemo.AudioPlaybackDemo
{
    public partial class AudioPlaybackPanel : UserControl, IMediaPlayer
    {
    
        private IWavePlayer waveOut;
        private string fileName;
        private AudioFileReader audioFileReader;
        private Action<float> setVolumeDelegate;

        /// <summary>
        /// The latency for playback.
        /// </summary>
        /// <remarks>300ms is the default latency to use with NAudio</remarks>
        private int latency = 300;

        /// <summary>
        /// The output device plugin to use.
        /// </summary>
        private WaveOutPlugin _outputDevicePlugin;

        public event PropertyChangedEventHandler PropertyChanged;

        public AudioPlaybackPanel()
        {

            InitializeComponent();
            LoadOuputPlugin();
        }

        private void LoadOuputPlugin()
        {
            _outputDevicePlugin = new WaveOutPlugin();

            Control settingsPanel;
            if (_outputDevicePlugin.IsAvailable)
            {
                settingsPanel = _outputDevicePlugin.CreateSettingsPanel();
            }
            else
            {
                settingsPanel = new Label() { Text = "This output device is unavailable on your system", Dock = DockStyle.Fill };
            }
            panelOutputDeviceSettings.Controls.Add(settingsPanel);
        }


      

      

        public TimeSpan Position
        {
            get {
                if (waveOut == null)
                {
                    return TimeSpan.Zero;
                }
                if (audioFileReader == null)
                {
                    return TimeSpan.Zero;
                }
                return (waveOut.PlaybackState == PlaybackState.Stopped) ? TimeSpan.Zero : audioFileReader.CurrentTime; }
            set {
                //TODO when Waveout Null, initialize it
                //otherwise the first set of the position gets missed
                if (waveOut != null)
                {
                    audioFileReader.CurrentTime = value;
                }
            }
        }
        public MediaPlayerState State {
            get
            {
                if (waveOut != null)
                {
                    if (waveOut.PlaybackState == PlaybackState.Playing)
                    {
                        return MediaPlayerState.Playing;
                    }
                    else if (waveOut.PlaybackState == PlaybackState.Paused)
                    {
                        return MediaPlayerState.Paused;
                    }
                }
                return MediaPlayerState.Paused;

            }
            set
            {
                if (value == MediaPlayerState.Playing)
                {
                    OnButtonPlayClick(this, new EventArgs());
                }
                else if (value == MediaPlayerState.Paused)
                {
                    OnButtonPauseClick(this, new EventArgs());
                }
                else
                {
                    throw new NotSupportedException($"State {value} is not supported for MediaPlayerState.");
                }
            }
        }


        public string Url
        {

            get
            {
                return fileName;
            }
            set
            {
                if (!string.IsNullOrEmpty(value) && value != fileName)
                {
                    //TODO irgendwie wird immer das gleiche File abgespielt, muss da noch was gesetzt werden??
                    OnButtonStopClick(this, new EventArgs());
                    fileName = value;
                }
            }
        }
        public double Volume { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private void OnButtonPlayClick(object sender, EventArgs e)
        {
            if (!_outputDevicePlugin.IsAvailable)
            {
                MessageBox.Show("The selected output driver is not available on this system");
                return;
            }

            if (waveOut != null)
            {
                if (waveOut.PlaybackState == PlaybackState.Playing)
                {
                    return;
                }
                else if (waveOut.PlaybackState == PlaybackState.Paused)
                {
                    waveOut.Play();
                    groupBoxDriverModel.Enabled = false;
                    return;
                }
            }
            
            // we are in a stopped state
            // TODO: only re-initialise if necessary

            if (String.IsNullOrEmpty(fileName))
            {
                OnOpenFileClick(sender, e);
            }
            if (String.IsNullOrEmpty(fileName))
            {
                return;
            }

            try
            {
                CreateWaveOut();
            }
            catch (Exception driverCreateException)
            {
                MessageBox.Show(String.Format("{0}", driverCreateException.Message));
                return;
            }

            ISampleProvider sampleProvider;
            try
            {
                sampleProvider = CreateInputStream(fileName);
            }
            catch (Exception createException)
            {
                MessageBox.Show(String.Format("{0}", createException.Message), "Error Loading File");
                return;
            }


            labelTotalTime.Text = String.Format("{0:00}:{1:00}", (int)audioFileReader.TotalTime.TotalMinutes,
                audioFileReader.TotalTime.Seconds);

            try
            {
                waveOut.Init(sampleProvider);
            }
            catch (Exception initException)
            {
                MessageBox.Show(String.Format("{0}", initException.Message), "Error Initializing Output");
                return;
            }

            setVolumeDelegate(volumeSlider1.Volume); 
            groupBoxDriverModel.Enabled = false;
            waveOut.Play();
        }

        private ISampleProvider CreateInputStream(string fileName)
        {
            audioFileReader = new AudioFileReader(fileName);
            
            var sampleChannel = new SampleChannel(audioFileReader, true);
            //TODO is sampleChannel used still ? sampleChannel.PreVolumeMeter+= OnPreVolumeMeter;
            setVolumeDelegate = vol => sampleChannel.Volume = vol;
            var postVolumeMeter = new MeteringSampleProvider(sampleChannel);
            postVolumeMeter.StreamVolume += OnPostVolumeMeter;

            return postVolumeMeter;
        }

        void OnPostVolumeMeter(object sender, StreamVolumeEventArgs e)
        {
            // we know it is stereo
            volumeMeter1.Amplitude = e.MaxSampleValues[0];
            volumeMeter2.Amplitude = e.MaxSampleValues[1];
        }

        private void CreateWaveOut()
        {
            CloseWaveOut();
            waveOut = _outputDevicePlugin.CreateDevice(latency);
            waveOut.PlaybackStopped += OnPlaybackStopped;
        }

        void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            groupBoxDriverModel.Enabled = true;
            if (e.Exception != null)
            {
                MessageBox.Show(e.Exception.Message, "Playback Device Error");
            }
            if (audioFileReader != null)
            {
                audioFileReader.Position = 0;
            }
        }

        private void CloseWaveOut()
        {
            if (waveOut != null)
            {
                waveOut.Stop();
            }
            if (audioFileReader != null)
            {
                // this one really closes the file and ACM conversion
                audioFileReader.Dispose();
                setVolumeDelegate = null;
                audioFileReader = null;
            }
            if (waveOut != null)
            {
                waveOut.Dispose();
                waveOut = null;
            }
        }

        private void OnButtonPauseClick(object sender, EventArgs e)
        {
            if (waveOut != null)
            {
                if (waveOut.PlaybackState == PlaybackState.Playing)
                {
                    waveOut.Pause();
                }
            }
        }

        private void OnVolumeSliderChanged(object sender, EventArgs e)
        {
            if (setVolumeDelegate != null)
            {
                setVolumeDelegate(volumeSlider1.Volume);
            }
        }

        private void OnButtonStopClick(object sender, EventArgs e)
        {
            if (waveOut != null)
            {
                waveOut.Stop();
            }
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            if (waveOut != null && audioFileReader != null)
            {
                TimeSpan currentTime = Position;
                trackBarPosition.Value = Math.Min(trackBarPosition.Maximum, (int)(100 * currentTime.TotalSeconds / audioFileReader.TotalTime.TotalSeconds));
                labelCurrentTime.Text = String.Format("{0:00}:{1:00}", (int)currentTime.TotalMinutes,
                    currentTime.Seconds);
            }
            else
            {
                trackBarPosition.Value = 0;
            }
        }

        private void trackBarPosition_Scroll(object sender, EventArgs e)
        {
            Position = TimeSpan.FromSeconds(audioFileReader.TotalTime.TotalSeconds * trackBarPosition.Value / 100.0);           
        }

        private void OnOpenFileClick(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            string allExtensions = "*.wav;*.aiff;*.mp3;*.aac";
            openFileDialog.Filter = String.Format("All Supported Files|{0}|All Files (*.*)|*.*", allExtensions);
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog.FileName;
            }
        }

        public void SeekBackward(double interval)
        {
            throw new NotImplementedException();
        }

        public void SeekForward(double interval)
        {
            throw new NotImplementedException();
        }

        public void TogglePlayPause()
        {
            if (State == MediaPlayerState.Playing)
            {
                State = MediaPlayerState.Paused;
            }
            else
            {
                State = MediaPlayerState.Playing;
            }
        }


    }
}

