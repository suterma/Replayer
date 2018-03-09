using NAudioDemo.AudioPlaybackDemo;
using System.Globalization;
namespace Replayer.WinForms.Common
{
    public partial class NAudioRePlayer
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

            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WindowsMediaRePlayer));
            this.mediaPlayer = new AudioPlaybackPanel();
            ((System.ComponentModel.ISupportInitialize)(this.mediaPlayer)).BeginInit();
            this.SuspendLayout();

            // 
            // mediaPlayer
            // 
            this.mediaPlayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediaPlayer.Enabled = true;
            this.mediaPlayer.Location = new System.Drawing.Point(0, 0);
            this.mediaPlayer.Name = "mediaPlayer";
            this.mediaPlayer.Size = new System.Drawing.Size(208, 63);
            this.mediaPlayer.TabIndex = 0;
            // 
            // NAudioRePlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mediaPlayer);
            this.MinimumSize = new System.Drawing.Size(208, 63);
            this.Name = "NAudioRePlayer";
            this.Size = new System.Drawing.Size(398, 148);
            ((System.ComponentModel.ISupportInitialize)(this.mediaPlayer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AudioPlaybackPanel mediaPlayer;
    }
}
