using System;

namespace Quirli.Api {
    /// <summary>
    ///     A cue which is a playing position in the media.
    /// </summary>
    public class Cue {
        /// <summary>
        ///     The temporal position.
        /// </summary>
        public double Position { get; set; }

        /// <summary>
        ///     The text, which can be a part of the lyrics, a caption, or a description of the scene.
        /// </summary>
        public String Text { get; set; }

        /// <summary>
        ///     The shortcut associated.
        /// </summary>
        /// <remarks>This is currently not used.</remarks>
        [Obsolete("This is currently not supported")]
        public String Shortcut { get; set; }

        /// <summary>
        ///     Creates a new cue with position and text initialized.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="text"></param>
        public Cue(double position, string text) {
            Position = position;
            Text = text;
        }
    }
}