﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using log4net;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using Replayer.Core.Player;
using Replayer.WinForms.Ui.Gui;

namespace NAudioDemo.AudioPlaybackDemo {
    public partial class AudioPlaybackPanel : XtraUserControl, IMediaPlayer {

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

        /// <summary>
        /// Occurs when a property has changed it's value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Finalizes an instance of the <see cref="AudioPlaybackPanel"/> class.
        /// </summary>
        ~AudioPlaybackPanel() {
            Log.Info("AudioPlaybackPanel finalized.");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioPlaybackPanel"/> class.
        /// </summary>
        public AudioPlaybackPanel() {
            Log.Info("AudioPlaybackPanel construction.");


            InitializeComponent();
            trackBarPosition.Maximum = trackbarMax;
            trackBarPosition.Minimum = trackbarMin;
            trackBarPosition.LargeChange = trackbarMax / 20;
            volumePot.Maximum = 100; //0dBFS for the potentiometer, according to the Volume property used here.
            volumePot.Value = 71; //approx. -3 dB
            volumePot.ValueChanged += VolumePot_ValueChanged;

            LoadOutputPlugin();
        }

        /// <summary>
        /// Loads the output plugin.
        /// </summary>
        private void LoadOutputPlugin() {
            Log.Info("WaveOutPlugin loading...");
            _outputDevicePlugin = new WaveOutPlugin();

            Control settingsPanel;
            if (_outputDevicePlugin.IsAvailable) {
                settingsPanel = _outputDevicePlugin.CreateSettingsPanel();
            }
            else {
                settingsPanel = new Label() { Text = "This output device is unavailable on your system", Dock = DockStyle.Fill };
            }
            settingsPanel.Dock = DockStyle.Fill;
            Log.Info("WaveOutPlugin loaded.");

        }

        /// <summary>
        /// Prepares to play, if required, by creating the output device and
        /// loading the file (if available) and getting the wave output ready.
        /// </summary>
        private void PrepareToPlay() {
            if (!_outputDevicePlugin.IsAvailable) {
                string message = "Preparing to play failed. The selected output driver is not available on this system.";
                Log.Error(message);
                ErrorBox.Show(message);
                return;
            }

            if (waveOut != null) {
                //Log.Info("Nothing to prepare - Ready to play!");
                return; //because there is nothing to prepare anymore.
            }

            Log.Info("Preparing to play...");

            if (String.IsNullOrEmpty(fileName)) {
                Log.Warn("Nothing to prepare, no file available.");
                return; //because there is nothing to prepare when no file is available.
            }

            try {
                CreateWaveOut();
            }
            catch (Exception driverCreateException) {
                string message = $"Preparing to play failed. Message: {driverCreateException.Message}";
                Log.Error(message, driverCreateException);
                ErrorBox.Show(message);
                return;
            }

            ISampleProvider sampleProvider;
            try {
                sampleProvider = CreateInputStream(fileName);
            }
            catch (Exception createException) {
                string message = null;
                if (createException.Message.Contains("NoDriver")) {
                    //there was probably a codec missing
                    message = $"The file {fileName} could not be loaded. Probably there is no matching codec available for the content. You may try to use different content or install a suitable codec from an online resource.";
                }
                else {
                    message = $"The file {fileName} could not be loaded. Message: {createException.Message}";
                }
                Log.Error(message, createException);
                ErrorBox.Show(message);
                return;
            }


            label_TotalTime.Text = String.Format("{0:00}:{1:00}", (int)audioFileReader.TotalTime.TotalMinutes,
                audioFileReader.TotalTime.Seconds);

            try {
                waveOut.Init(sampleProvider);
                UpdateWaveoutVolume();
            }
            catch (Exception initException) {
                //TODO logging all these
                string message = $"Preparing to play failed. The output could not be initialized. Message: {initException.Message}";
                Log.Error(message, initException);
                ErrorBox.Show(message);
                return;
            }
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
        public TimeSpan Position {
            get {
                if (waveOut == null) {
                    return TimeSpan.Zero;
                }
                if (audioFileReader == null) {
                    return TimeSpan.Zero;
                }
                return audioFileReader.CurrentTime;
            }
            set {
                PrepareToPlay();

                if (waveOut.PlaybackState == PlaybackState.Paused) {
                    waveOut.Stop(); //This will flush any outstanding buffers. See https://stackoverflow.com/a/32115661/79485
                }

                audioFileReader.CurrentTime = value;
                UpdateTimerDisplays();
                Log.Info($"Position skipped to {value}.");

            }
        }
        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        /// <exception cref="NotSupportedException"></exception>
        public MediaPlayerState State {
            get {
                if (waveOut != null) {
                    if (waveOut.PlaybackState == PlaybackState.Playing) {
                        return MediaPlayerState.Playing;
                    }
                    else if (waveOut.PlaybackState == PlaybackState.Paused) {
                        return MediaPlayerState.Paused;
                    }
                }
                return MediaPlayerState.Paused;

            }
            set {
                if (value == MediaPlayerState.Playing) {
                    PrepareToPlay();
                    if (waveOut != null) {
                        UpdateWaveoutVolume();
                        try {
                            waveOut.Play();
                        }
                        catch (NullReferenceException nrex) {
                            string message = "Playing does not work. Message: " + nrex.Message;
                            Log.Error(message, nrex);
                            ErrorBox.Show(message);
                        }
                    }
                }
                else if (value == MediaPlayerState.Paused) {
                    try {
                        if (waveOut != null) {
                            if (waveOut.PlaybackState == PlaybackState.Playing) {
                                waveOut.Pause();
                            }
                        }
                        UpdateAmplitudeDisplay(0, 0);
                    }
                    catch (NAudio.MmException mex) {
                        string message = "Pausing does not work. Message: " + mex.Message;
                        Log.Error(message, mex);
                        ErrorBox.Show(message);
                    }
                }
                else {
                    throw new NotSupportedException($"State {value} is not supported for MediaPlayerState.");
                }
                Log.Info($"State set to {value.ToString()}.");
            }
        }


        /// <summary>
        /// Gets or sets the URL, which represents the current media to use.
        /// </summary>
        /// <value>
        /// The URL, or null if no media should get used now.
        /// </value>
        public string Url {

            get {
                return fileName;
            }
            set {
                if (!string.IsNullOrEmpty(value) && value != fileName) {
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
        public double Volume {
            get {
                return volumePot.Value;
            }
            set {
                //limit
                double limitedValue = Math.Max(0, Math.Min(100, value));

                //apply change if necessary
                //if (volumePot?.Value != limitedValue)
                {
                    Log.Info($"Setting pot value to {value.ToString()}");
                    volumePot.Value = limitedValue;
                    UpdateWaveoutVolume();
                }
            }
        }

        /// <summary>
        /// Updates the waveout volume.
        /// </summary>
        private void UpdateWaveoutVolume() {
            if (waveOut != null) {
                float volume = (float)volumePot.Volume;
                if (waveOut.Volume != volume) {
                    waveOut.Volume = volume;
                    Log.Info($"Volume set to {volume}");
                }
            }
        }

        private ISampleProvider CreateInputStream(string fileName) {
            audioFileReader = new AudioFileReader(fileName);

            SampleChannel sampleChannel = new SampleChannel(audioFileReader, true);
            MeteringSampleProvider postVolumeMeter = new MeteringSampleProvider(sampleChannel);
            postVolumeMeter.StreamVolume += OnPostVolumeMeter;

            return postVolumeMeter;
        }

        void OnPostVolumeMeter(object sender, StreamVolumeEventArgs e) {
            UpdateAmplitudeDisplay(e.MaxSampleValues[0], e.MaxSampleValues[1]);
        }

        /// <summary>
        /// Updates the amplitude display.
        /// </summary>
        /// <param name="leftAmplitude">The left amplitude.</param>
        /// <param name="rightAmplitude">The right amplitude.</param>
        private void UpdateAmplitudeDisplay(float leftAmplitude, float rightAmplitude) {
            volumeMeter1.Amplitude = leftAmplitude;
            volumeMeter2.Amplitude = rightAmplitude;
        }

        private void CreateWaveOut() {
            Log.Info($"Creating WaveOut...");
            CloseWaveOut();
            waveOut = _outputDevicePlugin.CreateDevice(latency);
            waveOut.PlaybackStopped += OnPlaybackStopped;
            trackBarPosition.Enabled = true;//Allow scrolling only with a ready-to play situation
            Log.Info($"WaveOut created.");
        }

        void OnPlaybackStopped(object sender, StoppedEventArgs e) {
            UpdateAmplitudeDisplay(0, 0);
            if (e.Exception != null) {
                ErrorBox.Show(e.Exception.Message, "Playback Device Error");
            }
            Log.Info($"Playback stopped.");
        }

        private void CloseWaveOut() {
            if (waveOut != null) {
                waveOut.Stop();
                waveOut.PlaybackStopped -= OnPlaybackStopped;

            }
            if (audioFileReader != null) {
                // this one really closes the file and ACM conversion
                audioFileReader.Dispose();
                audioFileReader = null;
            }
            if (waveOut != null) {
                waveOut.Dispose();
                waveOut = null;
            }
            trackBarPosition.Enabled = false;//Not allow scrolling when not able to play
            trackBarPosition.Value = 0;
        }




        private void OnTimerTick(object sender, EventArgs e) {
            if (waveOut != null && audioFileReader != null) {
                UpdateTimerDisplays();
            }
            else {
                trackBarPosition.Value = 0;
                //UpdateTimerDisplays();

            }
        }

        private void UpdateTimerDisplays() {
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
        private void TrackBarPosition_Scroll(object sender, EventArgs e) {
            double secondsPosition = audioFileReader.TotalTime.TotalSeconds * trackBarPosition.Value / trackbarMax;
            TimeSpan position = TimeSpan.FromSeconds(secondsPosition);
            if (Position != position)
                Position = position;

        }

        /// <summary>
        /// Seeks backward within the currently loaded media track.
        /// </summary>
        /// <param name="interval">The interval, in [seconds].</param>
        public void SeekBackward(double interval) {
            Position -= TimeSpan.FromSeconds(interval);
        }

        /// <summary>
        /// Seeks forward within the currently loaded media track.
        /// </summary>
        /// <param name="interval">The interval, in [seconds].</param>
        public void SeekForward(double interval) {
            Position += TimeSpan.FromSeconds(interval);
        }

        /// <summary>
        /// Toggles the play/pause state.
        /// </summary>
        public void TogglePlayPause() {
            if (State == MediaPlayerState.Playing) {
                State = MediaPlayerState.Paused;
            }
            else {
                State = MediaPlayerState.Playing;
            }
        }

        /// <summary>
        /// Handles the ValueChanged event of the volumePot control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void VolumePot_ValueChanged(object sender, EventArgs e) {
            Volume = volumePot.Value;
        }

        /// <summary>
        /// Handles the Click event of the btn_play control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Btn_play_Click(object sender, EventArgs e) {
            State = MediaPlayerState.Playing;
        }

        /// <summary>
        /// Handles the Click event of the btn_pause control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Btn_pause_Click(object sender, EventArgs e) {
            State = MediaPlayerState.Paused;
        }

        /// <summary>
        /// Handles the Click event of the btn_stop control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Btn_stop_Click(object sender, EventArgs e) {
            if (waveOut != null) {
                waveOut.Stop();
            }
            UpdateAmplitudeDisplay(0, 0);
        }
    }
}

