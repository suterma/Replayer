namespace Replayer.WinForms.Ui.Gui {
    partial class Question {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Question));
            this.okCancelPanel1 = new Replayer.WinForms.Ui.Gui.OkCancelPanel();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.SuspendLayout();
            // 
            // okCancelPanel1
            // 
            this.okCancelPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.okCancelPanel1.Location = new System.Drawing.Point(0, 65);
            this.okCancelPanel1.MinimumSize = new System.Drawing.Size(156, 78);
            this.okCancelPanel1.Name = "okCancelPanel1";
            this.okCancelPanel1.Size = new System.Drawing.Size(273, 78);
            this.okCancelPanel1.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.labelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl1.Location = new System.Drawing.Point(0, 0);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(273, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Wassup?";
            // 
            // Question
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 143);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.okCancelPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Question";
            this.Text = "Question";
            this.ResumeLayout(false);

        }

        #endregion

        private OkCancelPanel okCancelPanel1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}