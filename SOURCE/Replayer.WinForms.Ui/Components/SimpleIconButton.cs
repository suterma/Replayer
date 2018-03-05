using System.Drawing;
using DevExpress.XtraEditors;

namespace Replayer.WinForms.Ui.Gui {
    public partial class SimpleIconButton : SimpleButton {
        /// <summary>
        ///     The size of this button control
        /// </summary>
        private static readonly Size _buttonSize = new Size(72, 72);

        public override Size MinimumSize {
            get { return _buttonSize; }
            set {
                //keep
            }
        }


        public override Size MaximumSize {
            get { return _buttonSize; }
            set {
                //keep
            }
        }

        public new Size Size {
            get { return _buttonSize; }
            set {
                //keep
            }
        }

        public SimpleIconButton() {
            InitializeComponent();
        }
    }
}