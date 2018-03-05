namespace Replayer.WinForms.Ui.Views {
    /// <summary>
    ///     A presenter for a specific view.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Presenter<T> {
        /// <summary>
        ///     Gets or sets the view, this controller presents.
        /// </summary>
        /// <value>The view.</value>
        public T View { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Presenter&lt;T&gt;" /> class.
        /// </summary>
        /// <param name="view">The view.</param>
        public Presenter(T view) {
            View = view;
        }
    }
}