using System.ComponentModel;
using System.Drawing;
using Replayer.WinForms.Ui.Gui;
using Replayer.WinForms.Ui.Properties;

namespace Replayer.WinForms.Ui.Components {
    /// <summary>
    ///     Displays the application settings in a UI control.
    /// </summary>
    internal class DisplayableUiSettings : DisplayTypeWrapper<Settings> {
        /// <summary>
        ///     Gets or sets the UI font.
        /// </summary>
        /// <value>
        ///     The UI font.
        /// </value>
        [Browsable(true)]
        [Description("The font for visual components in the user interface.")]
        [Category("General")]
        [DisplayName("UI Font")]
        public Font UiFont {
            get { return base.Wrapped.UiFont; }
            set { base.Wrapped.UiFont = value; }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DisplayableUiSettings" /> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public DisplayableUiSettings(Settings settings)
            : base(settings) { }
    }
}