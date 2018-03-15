using System;
using System.Collections.Generic;
using System.Linq;
using Replayer.Core.Data;
using Replayer.Core.Input;
using Replayer.Core.Player;
using Replayer.Model;

namespace Replayer.Core {
    /// <summary>
    ///     Translates events from a input handler a change in the model state.
    /// </summary>
    public class CommandToPlayerTranslator {
        /// <summary>
        ///     Gets or sets the keyboard input handler.
        /// </summary>
        /// <value>The keyboard input handler.</value>
        private KeyInputHandler KeyboardInputHandler { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandToPlayerTranslator" /> class.
        /// </summary>
        /// <param name="keyboardInputHandler">The keyboard input handler.</param>
        /// <param name="playerState">State of the player.</param>
        public CommandToPlayerTranslator(KeyInputHandler keyboardInputHandler) {
            KeyboardInputHandler = keyboardInputHandler;
            KeyboardInputHandler.KeySequenceDetected += KeyboardInputHandler_KeySequenceDetected;
            KeyboardInputHandler.CommandIssued += KeyboardInputHandler_CommandIssued;
        }

        /// <summary>
        ///     Handles the KeySequenceDetected event of the KeyboardInputHandler control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///     The <see cref="Replayer.Core.v04.Data.EventArgs&lt;System.String&gt;" /> instance containing the event data.
        /// </param>
        private void KeyboardInputHandler_KeySequenceDetected(object sender, EventArgs<String> e) {
            if
                (
                (e.Data.Length > 0) && //is of any characters?
                (Model.Instance.Compilation != null) //we have a loaded compilation available?
                ) {
                #region try to load the matching cue point

                Cue matchingCue = null;

                //look in the selected track first
                Track selectedTrack = Model.Instance.SelectedTrack;
                if (selectedTrack != null) //any selected?
                {
                    matchingCue = (from point in selectedTrack.Cues
                                   where point.Shortcut.Equals(e.Data)
                                   select point).FirstOrDefault();
                }

                if (matchingCue == null) //no match available?
                {
                    //look over all cues to find the matching cue point
                    matchingCue = (from point in Model.Instance.Compilation.Tracks.SelectMany(a => a.Cues)
                                   where point.Shortcut.Equals(e.Data)
                                   select point).FirstOrDefault();
                }
                if (matchingCue != null) //any found?
                {
                    Model.Instance.Player.State = MediaPlayerState.Paused; //TODO go back to last selected queue.
                    Model.Instance.SelectedCue = matchingCue;
                    //TODO delete below
                    ////select track of this item
                    //Model.Instance.SelectedTrack = (from track in Model.Instance.Compilation.Tracks
                    //                                where track.Cues.Contains(matchingCue)
                    //                                select track).Single();

                    //Model.Instance.UpdateSelectedCueTo(matchingCue);
                    //Model.Instance.Player.Position = new TimeSpan((long)(Model.Instance.SelectedCue.Time * 10000000));
                } //else leave the existing selected cue as is

                #endregion
            }
        }

        /// <summary>
        ///     Handles the CommandIssued event of the KeyboardInputHandler control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///     The <see cref="Replayer.Core.v04.Data.EventArgs&lt;Replayer.Core.v04.Input.InputCommand&gt;" /> instance containing the event data.
        /// </param>
        private void KeyboardInputHandler_CommandIssued(object sender, EventArgs<InputCommand> e) {
            switch (e.Data) {
                case InputCommand.IncreaseVolume:
                    Model.Instance.Player.Volume = Model.Instance.Player.Volume + 5;
                    break;
                case InputCommand.DecreaseVolume:
                    Model.Instance.Player.Volume = Model.Instance.Player.Volume - 5;
                    break;
                case InputCommand.PlayPauseToggled:
                    //change play state
                    Model.Instance.Player.TogglePlayPause();
                    break;
                case InputCommand.SkipForward: {
                        //from the currently used cue point in the track, search the next cue point
                        List<Cue> cuePoints = Model.Instance.Compilation.Tracks.SelectMany(a => a.Cues).ToList();
                        for (int i = 0; i < cuePoints.Count; i++) {
                            if (cuePoints[i].Equals(Model.Instance.SelectedCue))
                            //we are at the currently used cue point?
                            {
                                Model.Instance.SelectedCue = cuePoints[Math.Min(cuePoints.Count - 1, i + 1)];

                                break;
                            }
                        }

                        break;
                    }
                case InputCommand.SkipBackward: {
                        //from the currently used cue point in the track, search the previous cue point
                        List<Cue> cuePoints = Model.Instance.Compilation.Tracks.SelectMany(a => a.Cues).ToList();
                        for (int i = 0; i < cuePoints.Count(); i++) {
                            if (cuePoints[i].Equals(Model.Instance.SelectedCue))
                            //we are at the currently used cue point?
                            {
                                Model.Instance.SelectedCue = cuePoints[Math.Max(0, i - 1)];
                                break;
                            }
                        }

                        break;
                    }
                case InputCommand.SeekBackward: {
                        //explicitly use setter property to have changed event
                        Model.Instance.Player.SeekBackward(0.5);
                        break;
                    }
                case InputCommand.SeekForward: {
                        //explicitly use setter property to have changed event
                        Model.Instance.Player.SeekForward(0.5);
                        break;
                    }
                case InputCommand.Stop: {
                        //go back to last selected queue.
                        Model.Instance.Player.State = MediaPlayerState.Paused;
                        Model.Instance.Player.Position = new TimeSpan((long)(Model.Instance.SelectedCue.Time * 10000000));
                        break;
                    }
                default:
                    break;
            }
        }
    }
}