namespace Replayer.WinForms.Ui.Views.Tracks
{
    partial class TracksView
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
            this.components = new System.ComponentModel.Container();
            this._lbcTracks = new DevExpress.XtraEditors.ListBoxControl();
            this._cmsTracks = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this._lbcTracks)).BeginInit();
            this._cmsTracks.SuspendLayout();
            this.SuspendLayout();
            // 
            // _lbcTracks
            // 
            this._lbcTracks.Appearance.BackColor = System.Drawing.Color.White;
            this._lbcTracks.Appearance.Options.UseBackColor = true;
            this._lbcTracks.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lbcTracks.Location = new System.Drawing.Point(0, 0);
            this._lbcTracks.Name = "_lbcTracks";
            this._lbcTracks.Size = new System.Drawing.Size(154, 150);
            this._lbcTracks.TabIndex = 1;
            this._lbcTracks.MouseDown += new System.Windows.Forms.MouseEventHandler(this._lbcTracks_MouseDown);
            // 
            // _cmsTracks
            // 
            this._cmsTracks.ImageScalingSize = new System.Drawing.Size(32, 32);
            this._cmsTracks.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.moveUpToolStripMenuItem,
            this.moveDownToolStripMenuItem});
            this._cmsTracks.Name = "_cmsTracks";
            this._cmsTracks.Size = new System.Drawing.Size(157, 156);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Image = global::Replayer.WinForms.Ui.Properties.Resources.TrackEdit_Icon_256x256;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(156, 38);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.EditToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::Replayer.WinForms.Ui.Properties.Resources.TrackRemove_Icon_256x256;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(156, 38);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
            // 
            // moveUpToolStripMenuItem
            // 
            this.moveUpToolStripMenuItem.Name = "moveUpToolStripMenuItem";
            this.moveUpToolStripMenuItem.Size = new System.Drawing.Size(156, 38);
            this.moveUpToolStripMenuItem.Text = "Move up";
            this.moveUpToolStripMenuItem.Click += new System.EventHandler(this.moveUpToolStripMenuItem_Click);
            // 
            // moveDownToolStripMenuItem
            // 
            this.moveDownToolStripMenuItem.Name = "moveDownToolStripMenuItem";
            this.moveDownToolStripMenuItem.Size = new System.Drawing.Size(156, 38);
            this.moveDownToolStripMenuItem.Text = "Move down";
            this.moveDownToolStripMenuItem.Click += new System.EventHandler(this.moveDownToolStripMenuItem_Click);
            // 
            // TracksView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._lbcTracks);
            this.Name = "TracksView";
            this.Size = new System.Drawing.Size(154, 150);
            ((System.ComponentModel.ISupportInitialize)(this._lbcTracks)).EndInit();
            this._cmsTracks.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ListBoxControl _lbcTracks;
        private System.Windows.Forms.ContextMenuStrip _cmsTracks;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveDownToolStripMenuItem;
    }
}
