using DevExpress.XtraEditors;
using NAudio.Wave;

namespace NAudioDemo.AudioPlaybackDemo {
    /// <summary>
    /// The Settings panel fo rthe WaveOut Device.
    /// </summary>
    /// <seealso cref="DevExpress.XtraEditors.XtraUserControl" />
    public partial class WaveOutSettingsPanel : XtraUserControl {
        /// <summary>
        /// Initializes a new instance of the <see cref="WaveOutSettingsPanel"/> class.
        /// </summary>
        public WaveOutSettingsPanel() {
            InitializeComponent();
            InitialiseDeviceCombo();
        }

        /// <summary>
        /// Initialises the device combo.
        /// </summary>
        private void InitialiseDeviceCombo() {
            if (WaveOut.DeviceCount <= 0)
                return;
            for (int deviceId = -1; deviceId < WaveOut.DeviceCount; deviceId++) {
                WaveOutCapabilities capabilities = WaveOut.GetCapabilities(deviceId);
                comboBoxWaveOutDevice.Items.Add($"Device {deviceId} ({capabilities.ProductName})");
            }
            comboBoxWaveOutDevice.SelectedIndex = 0;
        }

        /// <summary>
        /// Gets the selected device number.
        /// </summary>
        /// <value>
        /// The selected device number.
        /// </value>
        public int SelectedDeviceNumber => comboBoxWaveOutDevice.SelectedIndex;

    }
}
