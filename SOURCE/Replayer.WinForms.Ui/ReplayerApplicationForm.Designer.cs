﻿namespace Replayer.WinForms.Ui {
    partial class ReplayerApplicationForm {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReplayerApplicationForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._tracksView = new Replayer.WinForms.Ui.Views.Tracks.TracksView();
            this._cuesView = new Replayer.WinForms.Ui.Views.Cues.CuesView();
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.standardMenuView1 = new Replayer.WinForms.Ui.Views.StandardMenu.StandardMenuView();
            this._keyboardControllerView = new Replayer.WinForms.Ui.Views.KeyboardController.KeyboardControllerView();
            this.mediaPlayerView1 = new Replayer.WinForms.Ui.Views.MediaPlayer.MediaPlayerView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 40);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._tracksView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._cuesView);
            this.splitContainer1.Size = new System.Drawing.Size(905, 332);
            this.splitContainer1.SplitterDistance = 295;
            this.splitContainer1.TabIndex = 7;
            // 
            // _tracksView
            // 
            this._tracksView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tracksView.Location = new System.Drawing.Point(0, 0);
            this._tracksView.Margin = new System.Windows.Forms.Padding(0);
            this._tracksView.Name = "_tracksView";
            this._tracksView.Size = new System.Drawing.Size(295, 332);
            this._tracksView.TabIndex = 4;
            // 
            // _cuesView
            // 
            this._cuesView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cuesView.Location = new System.Drawing.Point(0, 0);
            this._cuesView.Margin = new System.Windows.Forms.Padding(0);
            this._cuesView.Name = "_cuesView";
            this._cuesView.Size = new System.Drawing.Size(606, 332);
            this._cuesView.TabIndex = 0;
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "Caramel";
            // 
            // standardMenuView1
            // 
            this.standardMenuView1.Appearance.Font = new System.Drawing.Font("Arial", 14F);
            this.standardMenuView1.Appearance.Options.UseFont = true;
            this.standardMenuView1.AutoSize = true;
            this.standardMenuView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.standardMenuView1.Location = new System.Drawing.Point(0, 0);
            this.standardMenuView1.LookAndFeel.SkinName = "Black";
            this.standardMenuView1.Margin = new System.Windows.Forms.Padding(0);
            this.standardMenuView1.Name = "standardMenuView1";
            this.standardMenuView1.Size = new System.Drawing.Size(905, 40);
            this.standardMenuView1.TabIndex = 6;
            // 
            // _keyboardControllerView
            // 
            this._keyboardControllerView.AutoSize = true;
            this._keyboardControllerView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._keyboardControllerView.Location = new System.Drawing.Point(0, 421);
            this._keyboardControllerView.LookAndFeel.SkinName = "Caramel";
            this._keyboardControllerView.Margin = new System.Windows.Forms.Padding(0);
            this._keyboardControllerView.MinimumSize = new System.Drawing.Size(0, 29);
            this._keyboardControllerView.Name = "_keyboardControllerView";
            this._keyboardControllerView.Size = new System.Drawing.Size(905, 29);
            this._keyboardControllerView.TabIndex = 2;
            // 
            // mediaPlayerView1
            // 
            this.mediaPlayerView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mediaPlayerView1.Location = new System.Drawing.Point(0, 372);
            this.mediaPlayerView1.Margin = new System.Windows.Forms.Padding(0);
            this.mediaPlayerView1.Name = "mediaPlayerView1";
            this.mediaPlayerView1.Size = new System.Drawing.Size(905, 49);
            this.mediaPlayerView1.TabIndex = 8;
            // 
            // ReplayerApplicationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 450);
            this.Controls.Add(this.standardMenuView1);
            this.Controls.Add(this._keyboardControllerView);
            this.Controls.Add(this.mediaPlayerView1);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LookAndFeel.SkinName = "Black";
            this.Name = "ReplayerApplicationForm";
            this.Text = "Replayer";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Views.KeyboardController.KeyboardControllerView _keyboardControllerView;
        private Views.Tracks.TracksView _tracksView;
        private Views.StandardMenu.StandardMenuView standardMenuView1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Views.Cues.CuesView _cuesView;
        private Views.MediaPlayer.MediaPlayerView mediaPlayerView1;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
    }
}

