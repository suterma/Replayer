using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using Replayer.Core.Properties;
using Replayer.WinForms.Ui.Gui;

namespace Replayer.WinForms.Ui.Components {
    internal class DisplayableCoreSettings : DisplayTypeWrapper<Settings> {
        /// <summary>
        ///     Gets or sets the pre play duration in seconds.
        /// </summary>
        /// <value>
        ///     The pre play duration_ seconds.
        /// </value>
        [Browsable(true)]
        [Description("The amount of time in [seconds] the playing start before the actual cue time.")]
        [Category("Behaviour")]
        [DisplayName("Pre-play duration")]
        public double PrePlayDuration_Seconds {
            get { return Wrapped.PrePlayDuration_Seconds; }
            set { Wrapped.PrePlayDuration_Seconds = value; }
        }

        /// <summary>
        ///     Gets or sets the keyboard shortcut character timeout in sec.
        /// </summary>
        /// <value>
        ///     The keyboard shortcut character timeout_sec.
        /// </value>
        [Browsable(true)]
        [Description("The amount of time in [seconds] after which the shortcut input area is cleared.")]
        [Category("Behaviour")]
        [DisplayName("Keyboard shortcut timeout")]
        public double KeyboardShortcutCharacterTimeout_sec {
            get { return Wrapped.KeyboardShortcutCharacterTimeout_sec; }
            set { Wrapped.KeyboardShortcutCharacterTimeout_sec = value; }
        }

        /// <summary>
        ///     Gets or sets the default compilation lookup directory.
        /// </summary>
        /// <value>
        ///     The default compilation lookup directory.
        /// </value>
        [Browsable(true)]
        [Description("Default directory for browsing for collections.")]
        [Category("Behaviour")]
        [DisplayName("Default compilation directory")]
        [Editor(typeof(FolderNameEditor), typeof(UITypeEditor))]
        public string DefaultCompilationLookupDirectory {
            get { return Wrapped.DefaultCompilationLookupDirectory; }
            set { Wrapped.DefaultCompilationLookupDirectory = value; }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DisplayTypeWrapper&lt;T&gt;"></see> class.
        /// </summary>
        /// <param name="wrapped">The wrapped object.</param>
        public DisplayableCoreSettings(Settings wrapped)
            : base(wrapped) { }
    }
}