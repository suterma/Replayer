using DevExpress.XtraEditors;
using log4net;
using NAudio.Gui;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Replayer.WinForms.Ui.Components.NAudioPlayer {
    /// <summary>
    /// A variant of a Pot, specifically to control volume.
    /// </summary>
    /// <remarks>Features a volume property with float type, instead of double and allows to have a logarithmic response.</remarks>
    public class VolumePot : Pot {

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The current value of the pot
        /// </summary>
        public new double Value {
            get {
                return base.Value;
            }
            set {
                base.Value = value;
                Transform(value);
            }
        }

        private LabelControl _valueLabel;

        /// <summary>
        /// Provides a volume number, using the transform function.
        /// </summary>
        /// <remarks>Uses input values in the range 0-100 and transfers them into the range 0-1, using a function.</remarks>
        public double Volume {
            get; private set;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.UserControl.Load" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);

            //Add a label
            var boxSize = Size;

            _valueLabel = new DevExpress.XtraEditors.LabelControl();
            _valueLabel.Text = "- dB";
            _valueLabel.Top = boxSize.Height - 10;
            _valueLabel.Height = 10;
            _valueLabel.Left = boxSize.Width / 2 - 10;
            _valueLabel.Font = new Font(FontFamily.GenericSansSerif, 6.0F, FontStyle.Regular);
            Controls.Add(_valueLabel);

            //Initialize the display
            Transform(Value);
        }

        /// <summary>
        /// Handles the ValueChanged event of the VolumePot control.
        /// </summary>
        /// <remarks>Updates the Volume property and the dB level display.</remarks>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="NotImplementedException"></exception>
        private void VolumePot_ValueChanged(object sender, EventArgs e) {
            Transform(Value);
        }

        /// <summary>
        /// The upper bound to use for the input
        /// </summary>
        private const double upperInputBound = 100;

        /// <summary>
        /// The lower bound to use for the input
        /// </summary>
        private const double lowerInputBound = 0;





        /// <summary>
        /// Transforms the specified value into a volume representation.
        /// </summary>
        /// <remarks>This uses a logarithmic formula. A value of 0 to 100 gets transformed from 0 to 1 in a logarithimic fashion.</remarks>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private void Transform(double value) {
            //limit to 0-100
            var input = Math.Min(Math.Max(value, lowerInputBound), upperInputBound);

            //Log with base 2
            var volume = Math.Pow(input / upperInputBound, 2);

            //limit the output to 0-1
            var limited = Math.Min(Math.Max(volume, 0), 1);

            //Show the dB level
            var dbLevel = 10 * Math.Log10(limited / 1);
            if (_valueLabel != null) {
                _valueLabel.Text = $"{dbLevel:#.#}dB";
            }

            Log.Debug($"VolumePot: Transformed value {value} to {limited}");

            Volume = limited;
        }

        private void InitializeComponent() {
            this.SuspendLayout();
            // 
            // VolumePot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "VolumePot";
            this.Size = new System.Drawing.Size(200, 161);
            this.ResumeLayout(false);

        }
    }
}
