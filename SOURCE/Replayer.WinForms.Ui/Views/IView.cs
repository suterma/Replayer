namespace Replayer.WinForms.Ui.Views {
    /// <summary>
    ///     Defines a view, using a presenter of the specified type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IView<T> {
        /// <summary>
        ///     Gets or sets the presenter.
        /// </summary>
        /// <value>The presenter.</value>
        T Presenter { get; set; }
    }
}