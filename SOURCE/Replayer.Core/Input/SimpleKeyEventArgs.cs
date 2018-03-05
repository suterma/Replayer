using System;

namespace Replayer.Core.Input {
    /// <summary>
    ///     Provides a simplified form of key data, similar to the System.Windows.Forms.KeyEventArgs.
    /// </summary>
    /// <remarks>This is explicitly used to remove dependendy to Windows forms code.</remarks>
    public class SimpleKeyEventArgs : EventArgs {
        //
        // Summary:
        //     Gets or sets a value indicating whether the event was handled.
        //
        // Returns:
        //     true to bypass the control's default handling; otherwise, false to also pass
        //     the event along to the default control handler.
        public bool Handled { get; set; }
    }
}