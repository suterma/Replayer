using System;
using System.Threading;
using Replayer.Core.Data;

namespace Replayer.Core.Input {
    /// <summary>
    ///     A set of input commands for the replayer.
    /// </summary>
    public enum InputCommand {
        /// <summary>
        /// </summary>
        IncreaseVolume,

        /// <summary>
        /// </summary>
        DecreaseVolume,

        /// <summary>
        /// </summary>
        PlayPauseToggled,

        /// <summary>
        /// </summary>
        SkipForward,

        /// <summary>
        /// </summary>
        SkipBackward,

        /// <summary>
        /// </summary>
        SeekForward,

        /// <summary>
        /// </summary>
        SeekBackward,

        /// <summary>
        /// </summary>
        Stop
    }

    /// <summary>
    ///     Handles the key input of the user, to trigger specific actions in the replayer tool.
    /// </summary>
    /// <remarks>
    ///     The behaviour is this:
    ///     NumKeypad-Slash: Skip backwards
    ///     NumKeypad-Asterisk: Skip forwards
    ///     NumKeypad-Slash for a long time: Seek backwards
    ///     NumKeypad-Asterisk for a long time: Seek forwards
    ///     NumKeypad-Minus: Decrease volume
    ///     NumKeypad-Plus: Increase volume
    ///     NumKeypad-Enter when the sequence is empty: Toggle play/pause
    ///     NumKeypad-Dot: Reissue last sequence.
    ///     All other keys are added into a sequence, which is terminated by an enter key stroke.
    ///     When no key is pressed for some time, the sequence is cleared.
    /// </remarks>
    public class KeyInputHandler {
        /// <summary>
        ///     A timer for clearing the detected key sequnce after some period.
        /// </summary>
        private readonly Timer _sequenceClearingTimer;

        /// <summary>
        ///     Backing field for the CurrentDetectedKeySequence property.
        /// </summary>
        private String _currentDetectedKeySequence;

        /// <summary>
        ///     Gets or sets the current detected key sequence.
        /// </summary>
        /// <value>The current detected key sequence.</value>
        public String CurrentDetectedKeySequence {
            get { return _currentDetectedKeySequence; }
            private set {
                _currentDetectedKeySequence = value;
                OnKeySequenceChanged();
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether [key was up].
        /// </summary>
        /// <remarks>This is used for detecting multiple key presses.</remarks>
        /// <value>
        ///     <c>true</c> if [key was up]; otherwise, <c>false</c>.
        /// </value>
        private bool KeyWasUp { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [key was pressed repeatedly].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [key was pressed repeatedly]; otherwise, <c>false</c>.
        /// </value>
        private bool KeyWasPressedRepeatedly { get; set; }

        /// <summary>
        ///     Gets or sets the pressed character. This is used to act specifically at the next
        ///     key-up event.
        /// </summary>
        /// <value>The pressed character.</value>
        private char PressedCharacter { get; set; }

        /// <summary>
        ///     Gets or sets the sequence clearing timeout.
        /// </summary>
        /// <value>The sequence clearing timeout.</value>
        private double SequenceClearingTimeout { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="KeyInputHandler" /> class.
        /// </summary>
        /// <param name="sequenceClearingTimout">The sequence clearing timout in [seconds].</param>
        public KeyInputHandler(double sequenceClearingTimout) {
            SequenceClearingTimeout = sequenceClearingTimout;
            CurrentDetectedKeySequence = String.Empty;

            KeyWasPressedRepeatedly = false;
            KeyWasUp = true;

            //prepare timer
            //AutoResetEvent autoEvent = new AutoResetEvent(false);
            // Create the delegate that invokes methods for the timer.
            TimerCallback timerDelegate =
                _sequenceClearingTimer_Elapsed;
            _sequenceClearingTimer = new Timer(timerDelegate, null, (int)(SequenceClearingTimeout * 1000),
                                               (int)(SequenceClearingTimeout * 1000));
        }

        /// <summary>
        ///     Occurs when [command issued].
        /// </summary>
        public event EventHandler<EventArgs<InputCommand>> CommandIssued;

        /// <summary>
        ///     Occurs when [key sequence detected].
        /// </summary>
        /// <remarks>
        ///     A key sequece is considered as detected, when it is
        ///     terminated with the enter key after a short time.
        /// </remarks>
        public event EventHandler<EventArgs<String>> KeySequenceDetected;

        /// <summary>
        ///     Occurs when the current key sequence has changed,
        ///     even if it does not yet count as detected.
        /// </summary>
        public event EventHandler<EventArgs<String>> KeySequenceChanged;

        /// <summary>
        ///     Called when [key sequence changed].
        /// </summary>
        private void OnKeySequenceChanged() {
            if (KeySequenceChanged != null) //anyone listening
            {
                var args = new EventArgs<String>();
                args.Data = CurrentDetectedKeySequence;
                KeySequenceChanged(this, args);
            }
        }


        /// <summary>
        ///     Handles the Elapsed event of the _sequenceClearingTimer control.
        /// </summary>
        /// <param name="state">The state.</param>
        private void _sequenceClearingTimer_Elapsed(object state) {
            StopTimer(_sequenceClearingTimer);
            if (!CurrentDetectedKeySequence.Equals(String.Empty)) //not yet empty?
            {
                ///clear sequence
                CurrentDetectedKeySequence = String.Empty;
            }
        }

        /// <summary>
        ///     Stops the timer.
        /// </summary>
        /// <param name="timer">The timer.</param>
        private void StopTimer(Timer timer) {
            timer.Change(Timeout.Infinite, Timeout.Infinite); //do not automatically reissue ticks
        }

        /// <summary>
        ///     Starts the timer, using the predefined settings.
        /// </summary>
        /// <param name="timer">The timer.</param>
        private void StartTimer(Timer timer) {
            timer.Change((int)(SequenceClearingTimeout * 1000), (int)(SequenceClearingTimeout * 1000));
        }

        /// <summary>
        ///     Handles a key press event.
        /// </summary>
        /// <param name="e">
        ///     The <see cref="Replayer.Core.v04.Input.SimpleKeyPressEventArgs" /> instance containing the event data.
        /// </param>
        public void HandleKeyPress(SimpleKeyPressEventArgs e) {
            StopTimer(_sequenceClearingTimer);

            PressedCharacter = e.KeyChar; //remember for keyup event

            ///Handle short command
            switch (e.KeyChar) {
                case '+':
                    OnCommandIssued(InputCommand.IncreaseVolume);
                    e.Handled = true;
                    break;
                case '-':
                    OnCommandIssued(InputCommand.DecreaseVolume);
                    e.Handled = true;
                    break;
                case '/':
                    if (KeyWasUp) //this key has been newly pressed
                    {
                        KeyWasUp = false;
                        KeyWasPressedRepeatedly = false; //however, not yet pressed repeatedly
                    }
                    else {
                        //key was pressed repeatedly
                        KeyWasPressedRepeatedly = true;
                        PressedCharacter = e.KeyChar;
                        OnCommandIssued(InputCommand.SeekBackward);
                    }
                    e.Handled = true;
                    break;
                case '*':
                    if (KeyWasUp) //this key has been newly pressed
                    {
                        KeyWasUp = false;
                        KeyWasPressedRepeatedly = false; //however, not yet pressed repeatedly
                    }
                    else {
                        //key was pressed repeatedly
                        KeyWasPressedRepeatedly = true;
                        PressedCharacter = e.KeyChar;
                        OnCommandIssued(InputCommand.SeekForward);
                    }
                    e.Handled = true;
                    break;
                case '.':
                    OnCommandIssued(InputCommand.Stop);
                    e.Handled = true;
                    break;
                case '\r':
                    if (CurrentDetectedKeySequence.Length > 0) //there is a typed key sequence available?
                    {
                        OnKeySequenceDetected();
                    }
                    else {
                        OnCommandIssued(InputCommand.PlayPauseToggled);
                    }
                    e.Handled = true;
                    break;
                ///handle sequence character
                default:
                    CurrentDetectedKeySequence += e.KeyChar;
                    StartTimer(_sequenceClearingTimer); //start waiting a short time before clearing the sequence
                    break;
            }
        }

        /// <summary>
        ///     Called when [key sequence detected].
        /// </summary>
        private void OnKeySequenceDetected() {
            if (KeySequenceDetected != null) //anyone listening
            {
                var args = new EventArgs<String>();
                args.Data = CurrentDetectedKeySequence;
                KeySequenceDetected(this, args);
            }

            CurrentDetectedKeySequence = String.Empty; //immediately get ready for a new sequence
        }

        /// <summary>
        ///     Called when [command issued].
        /// </summary>
        /// <param name="action">The action.</param>
        private void OnCommandIssued(InputCommand action) {
            if (CommandIssued != null) //anyone listening
            {
                var args = new EventArgs<InputCommand>();
                args.Data = action;
                CommandIssued(this, args);
            }
        }


        /// <summary>
        ///     Handles a key up event.
        /// </summary>
        /// <param name="e">
        ///     The <see cref="Replayer.Core.v04.Input.SimpleKeyEventArgs" /> instance containing the event data.
        /// </param>
        public void HandleKeyUp(SimpleKeyEventArgs e) {
            //for the keys that have specific long press behaviour, check the single press case
            if (!KeyWasPressedRepeatedly) //a key has been explicitly pressed only once
            {
                switch (PressedCharacter) {
                    case '/': {
                            OnCommandIssued(InputCommand.SkipBackward);
                        }
                        break;
                    case '*': {
                            OnCommandIssued(InputCommand.SkipForward);
                        }
                        break;
                }
            }
            e.Handled = true;
            //set back states
            PressedCharacter = '!'; //set to unused char
            KeyWasPressedRepeatedly = false;
            KeyWasUp = true;
        }

        /// <summary>
        ///     Handles a key down event.
        /// </summary>
        /// <param name="e">
        ///     The <see cref="Replayer.Core.v04.Input.SimpleKeyEventArgs" /> instance containing the event data.
        /// </param>
        public void HandleKeyDown(SimpleKeyEventArgs e) { }
    }
}