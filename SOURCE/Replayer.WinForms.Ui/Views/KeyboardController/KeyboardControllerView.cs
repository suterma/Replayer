using System.Windows.Forms;
using DevExpress.XtraEditors;
using Replayer.WinForms.Ui.Properties;

namespace Replayer.WinForms.Ui.Views.KeyboardController {
    /// <summary>
    ///     A view for a keyboard controller.
    /// </summary>
    public partial class KeyboardControllerView : XtraUserControl, IKeyboardControllerView {
        /// <summary>
        ///     Sets the hotkey sequence.
        /// </summary>
        /// <value>The hotkey sequence.</value>
        /// <remarks>
        ///     This is the key sequence that the user
        ///     entered to navigate to a cue.
        /// </remarks>
        public string KeySequence {
            set {
                if (InvokeRequired) {
                    // Reinvoke the same method if necessary        
                    BeginInvoke(new MethodInvoker(delegate { KeySequence = value; }));
                }
                else {
                    _tbKeys.Text = value;
                }
            }
        }

        /// <summary>
        ///     Gets or sets the presenter.
        /// </summary>
        /// <value>The presenter.</value>
        public KeyboardControllerPresenter Presenter {
            get
; set
;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="KeyboardControllerView" /> class.
        /// </summary>
        public KeyboardControllerView() {
            InitializeComponent();
            Presenter = new KeyboardControllerPresenter(this);

            //apply appearance
            _tbKeys.Font = Settings.Default.UiFont;
        }
    }
}