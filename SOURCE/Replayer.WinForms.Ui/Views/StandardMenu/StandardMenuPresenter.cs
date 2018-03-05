namespace Replayer.WinForms.Ui.Views.StandardMenu {
    /// <summary>
    ///     A presenter for the Standard menu view.
    /// </summary>
    public class StandardMenuPresenter : Presenter<IStandardMenuView> {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Presenter&lt;T&gt;"></see> class.
        /// </summary>
        /// <param name="view">The view.</param>
        public StandardMenuPresenter(IStandardMenuView view)
            : base(view) {}
    }
}