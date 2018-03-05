namespace Replayer.WinForms.Ui
{
    partial class ReplayerApplicationForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReplayerApplicationForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.standardMenuView1 = new Replayer.WinForms.Ui.Views.StandardMenu.StandardMenuView();
            this._keyboardControllerView = new Replayer.WinForms.Ui.Views.KeyboardController.KeyboardControllerView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._tracksView = new Replayer.WinForms.Ui.Views.Tracks.TracksView();
            this._cuesView = new Replayer.WinForms.Ui.Views.Cues.CuesView();
            this.mediaPlayerView1 = new Replayer.WinForms.Ui.Views.MediaPlayer.MediaPlayerView();
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.standardMenuView1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._keyboardControllerView, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.mediaPlayerView1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(678, 333);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // standardMenuView1
            // 
            this.standardMenuView1.Appearance.Font = new System.Drawing.Font("Arial", 14F);
            this.standardMenuView1.Appearance.Options.UseFont = true;
            this.standardMenuView1.AutoSize = true;
            this.standardMenuView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.standardMenuView1.Location = new System.Drawing.Point(0, 0);
            this.standardMenuView1.Margin = new System.Windows.Forms.Padding(0);
            this.standardMenuView1.Name = "standardMenuView1";
            this.standardMenuView1.Size = new System.Drawing.Size(678, 40);
            this.standardMenuView1.TabIndex = 6;
            // 
            // _keyboardControllerView
            // 
            this._keyboardControllerView.AutoSize = true;
            this._keyboardControllerView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._keyboardControllerView.Location = new System.Drawing.Point(0, 304);
            this._keyboardControllerView.Margin = new System.Windows.Forms.Padding(0);
            this._keyboardControllerView.MinimumSize = new System.Drawing.Size(0, 29);
            this._keyboardControllerView.Name = "_keyboardControllerView";
            this._keyboardControllerView.Size = new System.Drawing.Size(678, 29);
            this._keyboardControllerView.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.splitContainer1.Size = new System.Drawing.Size(678, 200);
            this.splitContainer1.SplitterDistance = 223;
            this.splitContainer1.TabIndex = 7;
            // 
            // _tracksView
            // 
            this._tracksView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tracksView.Location = new System.Drawing.Point(0, 0);
            this._tracksView.Margin = new System.Windows.Forms.Padding(0);
            this._tracksView.Name = "_tracksView";
            this._tracksView.Size = new System.Drawing.Size(223, 200);
            this._tracksView.TabIndex = 4;
            // 
            // _cuesView
            // 
            this._cuesView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cuesView.Location = new System.Drawing.Point(0, 0);
            this._cuesView.Margin = new System.Windows.Forms.Padding(0);
            this._cuesView.Name = "_cuesView";
            this._cuesView.Size = new System.Drawing.Size(451, 200);
            this._cuesView.TabIndex = 0;
            // 
            // mediaPlayerView1
            // 
            this.mediaPlayerView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediaPlayerView1.Location = new System.Drawing.Point(0, 240);
            this.mediaPlayerView1.Margin = new System.Windows.Forms.Padding(0);
            this.mediaPlayerView1.MinimumSize = new System.Drawing.Size(208, 63);
            this.mediaPlayerView1.Name = "mediaPlayerView1";
            this.mediaPlayerView1.Size = new System.Drawing.Size(678, 64);
            this.mediaPlayerView1.TabIndex = 8;
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "Blue";
            // 
            // ReplayerApplicationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 333);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LookAndFeel.SkinName = "Black";
            this.Name = "ReplayerApplicationForm";
            this.Text = "Replayer";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Views.KeyboardController.KeyboardControllerView _keyboardControllerView;
        private Views.Tracks.TracksView _tracksView;
        private Views.StandardMenu.StandardMenuView standardMenuView1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Views.Cues.CuesView _cuesView;
        private Views.MediaPlayer.MediaPlayerView mediaPlayerView1;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
     }
}

