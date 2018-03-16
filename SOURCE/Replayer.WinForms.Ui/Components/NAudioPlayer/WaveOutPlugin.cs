using System;
using System.Linq;
using NAudio.Wave;
using System.Windows.Forms;

namespace NAudioDemo.AudioPlaybackDemo {
    /// <summary>
    /// An output device plugin implementation for the WaveOut device.
    /// </summary>
    /// <devdoc>This is taken from the NAudio demo project.</devdoc>
    /// <seealso cref="NAudioDemo.AudioPlaybackDemo.IOutputDevicePlugin" />
    class WaveOutPlugin : IOutputDevicePlugin {
        private WaveOutSettingsPanel waveOutSettingsPanel;

        /// <summary>
        /// Creates the device.
        /// </summary>
        /// <param name="latency">The latency.</param>
        /// <returns></returns>
        public IWavePlayer CreateDevice(int latency) {
            IWavePlayer device;
            //Existing Window is the default, probably most suitable for Replayer.
            //TODO: later cleanup this code with only leaving this default.
            var strategy = WaveCallbackStrategy.ExistingWindow;
            if (strategy == WaveCallbackStrategy.Event) {
                var waveOut = new WaveOutEvent {
                    DeviceNumber = waveOutSettingsPanel.SelectedDeviceNumber,
                    DesiredLatency = latency
                };
                device = waveOut;
            }
            else {
                WaveCallbackInfo callbackInfo = strategy == WaveCallbackStrategy.NewWindow ? WaveCallbackInfo.NewWindow() : WaveCallbackInfo.FunctionCallback();
                WaveOut outputDevice = new WaveOut(callbackInfo) {
                    DeviceNumber = waveOutSettingsPanel.SelectedDeviceNumber,
                    DesiredLatency = latency
                };
                device = outputDevice;
            }
            return device;
        }

        /// <summary>
        /// Creates the settings panel.
        /// </summary>
        /// <returns></returns>
        public UserControl CreateSettingsPanel() {
            waveOutSettingsPanel = new WaveOutSettingsPanel();
            return waveOutSettingsPanel;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name {
            get { return "WaveOut"; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is available.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is available; otherwise, <c>false</c>.
        /// </value>
        public bool IsAvailable {
            get { return WaveOut.DeviceCount > 0; }
        }

        /// <summary>
        /// Gets the priority.
        /// </summary>
        /// <value>
        /// The priority.
        /// </value>
        public int Priority {
            get { return 1; }
        }
    }
}
