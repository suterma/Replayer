using System;

namespace Replayer.WinForms.Ui.Views.KeyboardController {
    /// <summary>
    ///     Defines a view for a keyboard controller. This component handles keyboard
    ///     input and translates it into cue selections and player actions.
    /// </summary>
    public interface IKeyboardControllerView : IView<KeyboardControllerPresenter> {
        /// <summary>
        ///     Sets the hotkey sequence.
        /// </summary>
        /// <remarks>
        ///     This is the key sequence that the user
        ///     entered to navigate to a cue.
        /// </remarks>
        /// <value>The hotkey sequence.</value>
        String KeySequence { set; }
    }
}