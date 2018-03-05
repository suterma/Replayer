namespace Replayer.WinForms.Ui.Views.KeyboardController
{
    partial class KeyboardControllerView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._tbKeys = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // _tbKeys
            // 
            this._tbKeys.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._tbKeys.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tbKeys.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._tbKeys.Location = new System.Drawing.Point(0, 0);
            this._tbKeys.Name = "_tbKeys";
            this._tbKeys.ReadOnly = true;
            this._tbKeys.Size = new System.Drawing.Size(141, 21);
            this._tbKeys.TabIndex = 1;
            this._tbKeys.WordWrap = false;
            // 
            // KeyboardControllerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this._tbKeys);
            this.Name = "KeyboardControllerView";
            this.Size = new System.Drawing.Size(141, 53);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.TextBox _tbKeys;

        #endregion
    }
}
