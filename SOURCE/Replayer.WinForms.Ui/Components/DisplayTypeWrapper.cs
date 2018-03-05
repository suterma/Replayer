using System.ComponentModel;

namespace Replayer.WinForms.Ui.Gui {
    /// <summary>
    ///     A type wrapper for a non-ui type to a displayable
    ///     object.
    ///     An inherited class is expected to provide
    ///     specifically attributed properties for using in a UI control.
    /// </summary>
    /// <devdoc>
    ///     To use it, inherit from this using a concrete
    ///     type parameter and implement the wrapper properties
    ///     with the attributes you need.
    /// </devdoc>
    /// <typeparam name="T"></typeparam>
    internal class DisplayTypeWrapper<T> where T : class, new() {
        /// <summary>
        ///     Gets or sets the wrapped object.
        /// </summary>
        /// <value>
        ///     The wrapped object.
        /// </value>
        [Browsable(false)]
        internal T Wrapped { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DisplayTypeWrapper&lt;T&gt;" /> class.
        /// </summary>
        /// <param name="wrapped">The wrapped object.</param>
        public DisplayTypeWrapper(T wrapped) {
            Wrapped = wrapped;
        }
    }
}