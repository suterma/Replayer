namespace Replayer.Core.Input {
    /// <summary>
    ///     Provides a simplified form of key data, similar to the System.Windows.Forms.KeyPressEventArgs.
    /// </summary>
    /// <remarks>This is explicitly used to remove dependendy to Windows forms code.</remarks>
    public class SimpleKeyPressEventArgs {
        // Summary:
        //     Gets or sets a value indicating whether the System.Windows.Forms.Control.KeyPress
        //     event was handled.
        //
        // Returns:
        //     true if the event is handled; otherwise, false.
        public bool Handled { get; set; }
        //
        // Summary:
        //     Gets or sets the character corresponding to the key pressed.
        //
        // Returns:
        //     The ASCII character that is composed. For example, if the user presses SHIFT
        //     + K, this property returns an uppercase K.
        public char KeyChar { get; set; }
    }
}