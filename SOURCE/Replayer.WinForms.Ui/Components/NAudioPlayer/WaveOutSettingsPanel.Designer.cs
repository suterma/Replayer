namespace NAudioDemo.AudioPlaybackDemo {
    partial class WaveOutSettingsPanel {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.comboBoxWaveOutDevice = new DevExpress.XtraEditors.ComboBox();
            this.SuspendLayout();
            // 
            // comboBoxWaveOutDevice
            // 
            this.comboBoxWaveOutDevice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxWaveOutDevice.Location = new System.Drawing.Point(0, 0);
            this.comboBoxWaveOutDevice.Name = "comboBoxWaveOutDevice";
            this.comboBoxWaveOutDevice.Size = new System.Drawing.Size(117, 21);
            this.comboBoxWaveOutDevice.TabIndex = 19;
            // 
            // WaveOutSettingsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBoxWaveOutDevice);
            this.Name = "WaveOutSettingsPanel";
            this.Size = new System.Drawing.Size(117, 22);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ComboBox comboBoxWaveOutDevice;
    }
}
