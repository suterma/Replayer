using System;
using System.Linq;
using NAudio.Wave;
using System.Windows.Forms;

namespace NAudioDemo.AudioPlaybackDemo {
    /// <summary>
    /// A definition of an output device Plugin.
    /// </summary>
    /// <devdoc>This is taken from the NAudio demo project.</devdoc>
    public interface IOutputDevicePlugin {
        /// <summary>
        /// Creates the device.
        /// </summary>
        /// <param name="latency">The latency.</param>
        /// <returns></returns>
        IWavePlayer CreateDevice(int latency);
        /// <summary>
        /// Creates the settings panel.
        /// </summary>
        /// <returns></returns>
        UserControl CreateSettingsPanel();
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; }
        /// <summary>
        /// Gets a value indicating whether this instance is available.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is available; otherwise, <c>false</c>.
        /// </value>
        bool IsAvailable { get; }
        /// <summary>
        /// Gets the priority.
        /// </summary>
        /// <value>
        /// The priority.
        /// </value>
        int Priority { get; }
    }
}
