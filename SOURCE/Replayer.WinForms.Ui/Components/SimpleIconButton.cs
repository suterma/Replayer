using System.Drawing;
using DevExpress.XtraEditors;

namespace Replayer.WinForms.Ui.Gui {
    /// <summary>
    /// A simple button with an icon.
    /// </summary>
    /// <seealso cref="DevExpress.XtraEditors.SimpleButton" />
    public partial class SimpleIconButton : SimpleButton {
        /// <summary>
        ///     The size of this button control
        /// </summary>
        private static readonly Size _buttonSize = new Size(72, 72);

        /// <summary>
        /// Gets or sets the size that is the lower limit that <see cref="M:System.Windows.Forms.Control.GetPreferredSize(System.Drawing.Size)" /> can specify.
        /// </summary>
        public override Size MinimumSize {
            get { return _buttonSize; }
            set {
                //keep
            }
        }


        /// <summary>
        /// Gets or sets the size that is the upper limit that <see cref="M:System.Windows.Forms.Control.GetPreferredSize(System.Drawing.Size)" /> can specify.
        /// </summary>
        public override Size MaximumSize {
            get { return _buttonSize; }
            set {
                //keep
            }
        }

        /// <summary>
        /// Gets or sets the height and width of the control.
        /// </summary>
        public new Size Size {
            get { return _buttonSize; }
            set {
                //keep
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleIconButton"/> class.
        /// </summary>
        public SimpleIconButton() {
            InitializeComponent();
        }
    }
}