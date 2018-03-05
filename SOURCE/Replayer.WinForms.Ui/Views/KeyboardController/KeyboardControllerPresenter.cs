namespace Replayer.WinForms.Ui.Views.KeyboardController {
    /// <summary>
    ///     A presenter for a Keyboard controller view.
    /// </summary>
    public class KeyboardControllerPresenter : Presenter<IKeyboardControllerView> {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Presenter&lt;T&gt;"></see> class.
        /// </summary>
        /// <param name="view">The view.</param>
        public KeyboardControllerPresenter(IKeyboardControllerView view)
            : base(view) {
            //register for events on the handler
            Core.Model.Instance.KeyboardInputHandler.KeySequenceChanged += (sender, e) => View.KeySequence = e.Data;
        }
    }
}