using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using log4net;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using Replayer.Core.Player;
using Replayer.WinForms.Ui.Gui;

namespace NAudioDemo.AudioPlaybackDemo
{
    public partial class AudioPlaybackPanel : UserControl, IMediaPlayer
    {

        //TODO add log4net logging

        int trackbarMax = 100000; //reasonably larger than estimated width in pixels
        int trackbarMin = 0;

        private IWavePlayer waveOut;
        private string fileName;
        private AudioFileReader audioFileReader;

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

        //public event PropertyChangedEventHandler PropertyChanged;

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Finalizes an instance of the <see cref="AudioPlaybackPanel"/> class.
        /// </summary>
        ~AudioPlaybackPanel()
        {
            Log.Info("AudioPlaybackPanel finalized.");

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioPlaybackPanel"/> class.
        /// </summary>
        public AudioPlaybackPanel()
        {
            Log.Info("AudioPlaybackPanel construction.");


            InitializeComponent();
            trackBarPosition.Maximum = trackbarMax;
            trackBarPosition.Minimum = trackbarMin;
            volumePot.Maximum = 100; //0dBFS for the potentiometer, according to the Volume property used here.
            volumePot.Value = 71; //approx. -3 dB
            //volumePot.ValueChanged += volumePot_ValueChanged;

            LoadOutputPlugin();
        }

        /// <summary>
        /// Loads the output plugin.
        /// </summary>
        private void LoadOutputPlugin()
        {
            Log.Info("WaveOutPlugin loading...");
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
            Log.Info("WaveOutPlugin loaded.");

        }

        /// <summary>
        /// Prepares to play by loading the file (if available) and getting the wave output ready.
        /// </summary>
        private void PrepareToPlay()
        {
            Log.Info("Preparing to play...");

            if (!_outputDevicePlugin.IsAvailable)
            {
                string message = "Preparing to play failed. The selected output driver is not available on this system.";
                Log.Error(message);
                ErrorBox.Show(message);
                return;
            }

            if (waveOut != null)
            {
                Log.Info("Nothing to prepare - Ready to play!");

                return; //because there is nothing to prepare anymore.
            }


            if (String.IsNullOrEmpty(fileName))
            {
                Log.Warn("Nothing to prepare, no file available.");
                return; //because there is nothing to prepare when no file is available.
            }

            try
            {
                CreateWaveOut();
            }
            catch (Exception driverCreateException)
            {
                string message = $"Preparing to play failed. Message: {driverCreateException.Message}";
                Log.Error(message, driverCreateException);
                ErrorBox.Show(message);
                return;
            }

            ISampleProvider sampleProvider;
            try
            {
                sampleProvider = CreateInputStream(fileName);
            }
            catch (Exception createException)
            {
                string message = null;
                if (createException.Message.Contains("NoDriver"))
                {
                    //there was probably a codec missing
                    message = $"The file {fileName} could not be loaded. Probably there is no matching codec available for the content. You may try to use different content or install a suitable codec from an online resource.";
                }
                else
                {
                    message = $"The file {fileName} could not be loaded. Message: {createException.Message}";
                }
                Log.Error(message, createException);
                ErrorBox.Show(message);
                return;
            }


            label_TotalTime.Text = String.Format("{0:00}:{1:00}", (int)audioFileReader.TotalTime.TotalMinutes,
                audioFileReader.TotalTime.Seconds);

            try
            {
                waveOut.Init(sampleProvider);
                UpdateWaveoutVolume();
            }
            catch (Exception initException)
            {
                //TODO logging all these
                string message = $"Preparing to play failed. The output could not be initialized. Message: {initException.Message}";
                Log.Error(message,initException);
                ErrorBox.Show(message);
                return;
            }
            panelOutputDeviceSettings.Enabled = false;
            Log.Info("Ready to play!");
        }

        /// <summary>
        /// Gets or sets the position within the currently loaded media track.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        /// <remarks>
        /// While playing, the position is expected to get updated
        /// at about every half a second.
        /// </remarks>
        public TimeSpan Position
        {
            get
            {
                if (waveOut == null)
                {
                    return TimeSpan.Zero;
                }
                if (audioFileReader == null)
                {
                    return TimeSpan.Zero;
                }
                //return (waveOut.PlaybackState == PlaybackState.Stopped) ? TimeSpan.Zero : audioFileReader.CurrentTime;
                return audioFileReader.CurrentTime;
            }
            set
            {
                PrepareToPlay();
                if (audioFileReader != null)
                {
                    audioFileReader.CurrentTime = value;
                    UpdateTimerDisplays();
                    Log.Info($"Position skipped to {value}.");
                }
            }
        }
        public MediaPlayerState State
        {
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
                    PrepareToPlay();
                    if (waveOut != null)
                    {
                        UpdateWaveoutVolume();
                        try
                        {
                            waveOut.Play();
                        }
                        catch (NullReferenceException nrex)
                        {
                            string message = "Playing does not work. Message: " + nrex.Message;
                            Log.Error(message,nrex);
                            ErrorBox.Show(message);
                        }
                    }
                }
                else if (value == MediaPlayerState.Paused)
                {
                    try
                    {
                        if (waveOut != null)
                        {
                            if (waveOut.PlaybackState == PlaybackState.Playing)
                            {
                                waveOut.Pause();
                            }
                        }
                        UpdateAmplitudeDisplay(0, 0);
                    }
                    catch (NAudio.MmException mex)
                    {
                        string message = "Pausing does not work. Message: " + mex.Message;
                        Log.Error(message, mex);
                        ErrorBox.Show(message);
                    }
                }
                else
                {
                    throw new NotSupportedException($"State {value} is not supported for MediaPlayerState.");
                }
                Log.Info($"State set to {value.ToString()}.");
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
                    CloseWaveOut();
                    fileName = value;
                    Log.Info($"URL set to {value.ToString()}");
                    PrepareToPlay();
                }
            }
        }
        /// <summary>
        /// Gets or sets the volume. The value is expected to be in the range
        /// of 0 to 100.
        /// </summary>
        /// <remarks>The value gets limited to the expected range.</remarks>
        /// <value>
        /// The volume.
        /// </value>
        public double Volume
        {
            get
            {
                return volumePot.Value;
            }
            set
            {
                Log.Info($"Setting pot value to {value.ToString()}");

                //limit
                var limitedValue = Math.Max(0, Math.Min(100, value));

                    volumePot.Value = limitedValue;       
                UpdateWaveoutVolume();
            }
        }

        private void UpdateWaveoutVolume()
        {
            var volume = (float)volumePot.Volume;
            waveOut.Volume = volume;
            Log.Info($"Volume set to {volume}");

        }

        private ISampleProvider CreateInputStream(string fileName)
        {
            audioFileReader = new AudioFileReader(fileName);

            var sampleChannel = new SampleChannel(audioFileReader, true);
            //TODO is sampleChannel used still ? sampleChannel.PreVolumeMeter+= OnPreVolumeMeter;
            var postVolumeMeter = new MeteringSampleProvider(sampleChannel);
            postVolumeMeter.StreamVolume += OnPostVolumeMeter;

            return postVolumeMeter;
        }

        void OnPostVolumeMeter(object sender, StreamVolumeEventArgs e)
        {
            UpdateAmplitudeDisplay(e.MaxSampleValues[0], e.MaxSampleValues[1]);
        }

        /// <summary>
        /// Updates the amplitude display.
        /// </summary>
        /// <param name="leftAmplitude">The left amplitude.</param>
        /// <param name="rightAmplitude">The right amplitude.</param>
        private void UpdateAmplitudeDisplay(float leftAmplitude, float rightAmplitude)
        {
            volumeMeter1.Amplitude = leftAmplitude;
            volumeMeter2.Amplitude = rightAmplitude;
        }

        private void CreateWaveOut()
        {
            Log.Info($"Creating WaveOut...");
            CloseWaveOut();
            waveOut = _outputDevicePlugin.CreateDevice(latency);
            waveOut.PlaybackStopped += OnPlaybackStopped;
            Log.Info($"WaveOut created.");
        }

        void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            UpdateAmplitudeDisplay(0, 0);
            panelOutputDeviceSettings.Enabled = true;
            if (e.Exception != null)
            {
                ErrorBox.Show(e.Exception.Message, "Playback Device Error");
            }
            if (audioFileReader != null)
            {
                audioFileReader.Position = 0;
            }
            Log.Info($"Playback stopped.");
        }

        private void CloseWaveOut()
        {
            if (waveOut != null)
            {
                waveOut.Stop();
                waveOut.PlaybackStopped -= OnPlaybackStopped;

            }
            if (audioFileReader != null)
            {
                // this one really closes the file and ACM conversion
                audioFileReader.Dispose();
                audioFileReader = null;
            }
            if (waveOut != null)
            {
                waveOut.Dispose();
                waveOut = null;
            }
        }




        private void OnTimerTick(object sender, EventArgs e)
        {
            if (waveOut != null && audioFileReader != null)
            {
                UpdateTimerDisplays();
            }
            else
            {
                trackBarPosition.Value = 0;
                //UpdateTimerDisplays();

            }
        }

        private void UpdateTimerDisplays()
        {
            TimeSpan currentTime = Position;
            trackBarPosition.Value = Math.Min(trackBarPosition.Maximum, (int)(trackbarMax * currentTime.TotalSeconds / audioFileReader.TotalTime.TotalSeconds));
            label_CurrentTime.Text = String.Format("{0:00}:{1:00}.{2:0}", (int)currentTime.TotalMinutes,
                currentTime.Seconds, currentTime.Milliseconds / 100 /*show only tenths of a second */);

        }


        /// <summary>
        /// Handles the Scroll event of the trackBarPosition control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void trackBarPosition_Scroll(object sender, EventArgs e)
        {
            var secondsPosition = audioFileReader.TotalTime.TotalSeconds * trackBarPosition.Value / trackbarMax;
            var position = TimeSpan.FromSeconds(secondsPosition);
            if (Position != position)
                Position = position;
        }

        //private void UpdateTrackBarPosition()
        //{
        //    trackBarPosition.Value = 
        //    //if (audioFileReader != null)
        //    //{
        //    //    var secondsPosition = audioFileReader.TotalTime.TotalSeconds * trackBarPosition.Value / trackbarMax;
        //    //    var position = TimeSpan.FromSeconds(secondsPosition);
        //    //    if (Position != position)
        //    //    Position = position;
        //    //}
        //}

        public void SeekBackward(double interval)
        {
            throw new NotImplementedException();
        }

        public void SeekForward(double interval)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Toggles the play/pause state.
        /// </summary>
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

        /// <summary>
        /// Handles the ValueChanged event of the volumePot control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void volumePot_ValueChanged(object sender, EventArgs e)
        {
            Volume = volumePot.Value;
            //UpdateWaveoutVolume();
        }

        /// <summary>
        /// Handles the Click event of the btn_play control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btn_play_Click(object sender, EventArgs e)
        {

            State = MediaPlayerState.Playing;
        }

        /// <summary>
        /// Handles the Click event of the btn_pause control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btn_pause_Click(object sender, EventArgs e)
        {
            State = MediaPlayerState.Paused;
        }

        /// <summary>
        /// Handles the Click event of the btn_stop control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btn_stop_Click(object sender, EventArgs e)
        {
            if (waveOut != null)
            {
                waveOut.Stop();
            }
            UpdateAmplitudeDisplay(0, 0);
        }


    }
}

