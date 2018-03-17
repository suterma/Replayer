using Replayer.WinForms.Ui.Components.NAudioPlayer;
using System;

namespace NAudioDemo.AudioPlaybackDemo {
    /// <summary>
    /// A panel for audio playback using NAudio
    /// </summary>
    /// <seealso cref="System.Windows.Forms.UserControl" />
    /// <seealso cref="Replayer.Core.Player.IMediaPlayer" />
    partial class AudioPlaybackPanel {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            CloseWaveOut();
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
            this.trackBarPosition = new System.Windows.Forms.TrackBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.volumeMeter2 = new NAudio.Gui.VolumeMeter();
            this.volumeMeter1 = new NAudio.Gui.VolumeMeter();
            this.panelOutputDeviceSettings = new System.Windows.Forms.Panel();
            this.btn_play = new DevExpress.XtraEditors.SimpleButton();
            this.btn_pause = new DevExpress.XtraEditors.SimpleButton();
            this.btn_stop = new DevExpress.XtraEditors.SimpleButton();
            this.label_TotalTime = new System.Windows.Forms.Label();
            this.label_CurrentTime = new System.Windows.Forms.Label();
            this.volumePot = new Replayer.WinForms.Ui.Components.NAudioPlayer.VolumePot();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPosition)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBarPosition
            // 
            this.trackBarPosition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarPosition.Enabled = false;
            this.trackBarPosition.LargeChange = 1;
            this.trackBarPosition.Location = new System.Drawing.Point(126, 0);
            this.trackBarPosition.Maximum = 100;
            this.trackBarPosition.Name = "trackBarPosition";
            this.trackBarPosition.Size = new System.Drawing.Size(360, 45);
            this.trackBarPosition.TabIndex = 16;
            this.trackBarPosition.Tag = "Position";
            this.trackBarPosition.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarPosition.Scroll += new System.EventHandler(this.TrackBarPosition_Scroll);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 141;
            this.timer1.Tick += new System.EventHandler(this.OnTimerTick);
            // 
            // volumeMeter2
            // 
            this.volumeMeter2.Amplitude = 0F;
            this.volumeMeter2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.volumeMeter2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.volumeMeter2.Location = new System.Drawing.Point(514, 0);
            this.volumeMeter2.MaxDb = 3F;
            this.volumeMeter2.MinDb = -60F;
            this.volumeMeter2.Name = "volumeMeter2";
            this.volumeMeter2.Size = new System.Drawing.Size(16, 48);
            this.volumeMeter2.TabIndex = 18;
            this.volumeMeter2.Text = "volumeMeter1";
            // 
            // volumeMeter1
            // 
            this.volumeMeter1.Amplitude = 0F;
            this.volumeMeter1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.volumeMeter1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.volumeMeter1.Location = new System.Drawing.Point(493, 0);
            this.volumeMeter1.MaxDb = 3F;
            this.volumeMeter1.MinDb = -60F;
            this.volumeMeter1.Name = "volumeMeter1";
            this.volumeMeter1.Size = new System.Drawing.Size(16, 48);
            this.volumeMeter1.TabIndex = 18;
            this.volumeMeter1.Text = "volumeMeter1";
            // 
            // panelOutputDeviceSettings
            // 
            this.panelOutputDeviceSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelOutputDeviceSettings.Location = new System.Drawing.Point(175, 23);
            this.panelOutputDeviceSettings.Name = "panelOutputDeviceSettings";
            this.panelOutputDeviceSettings.Size = new System.Drawing.Size(262, 25);
            this.panelOutputDeviceSettings.TabIndex = 1;
            // 
            // btn_play
            // 
            this.btn_play.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_play.Image = global::Replayer.WinForms.Ui.Properties.Resources.AudioPlay_Icon_16x16;
            this.btn_play.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btn_play.Location = new System.Drawing.Point(0, 0);
            this.btn_play.Name = "btn_play";
            this.btn_play.Size = new System.Drawing.Size(36, 48);
            this.btn_play.TabIndex = 22;
            this.btn_play.Click += new System.EventHandler(this.Btn_play_Click);
            // 
            // btn_pause
            // 
            this.btn_pause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_pause.Image = global::Replayer.WinForms.Ui.Properties.Resources.AudioPause_Icon_16x16;
            this.btn_pause.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btn_pause.Location = new System.Drawing.Point(42, 0);
            this.btn_pause.Name = "btn_pause";
            this.btn_pause.Size = new System.Drawing.Size(36, 48);
            this.btn_pause.TabIndex = 23;
            this.btn_pause.Click += new System.EventHandler(this.Btn_pause_Click);
            // 
            // btn_stop
            // 
            this.btn_stop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_stop.Image = global::Replayer.WinForms.Ui.Properties.Resources.AudioStop_Icon_16x16;
            this.btn_stop.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btn_stop.Location = new System.Drawing.Point(84, 0);
            this.btn_stop.LookAndFeel.SkinName = "Caramel";
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(36, 48);
            this.btn_stop.TabIndex = 24;
            this.btn_stop.Click += new System.EventHandler(this.Btn_stop_Click);
            // 
            // label_TotalTime
            // 
            this.label_TotalTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label_TotalTime.AutoSize = true;
            this.label_TotalTime.Location = new System.Drawing.Point(444, 23);
            this.label_TotalTime.Name = "label_TotalTime";
            this.label_TotalTime.Size = new System.Drawing.Size(43, 15);
            this.label_TotalTime.TabIndex = 25;
            this.label_TotalTime.Text = "00:00.0";
            // 
            // label_CurrentTime
            // 
            this.label_CurrentTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_CurrentTime.AutoSize = true;
            this.label_CurrentTime.Location = new System.Drawing.Point(126, 23);
            this.label_CurrentTime.Name = "label_CurrentTime";
            this.label_CurrentTime.Size = new System.Drawing.Size(43, 15);
            this.label_CurrentTime.TabIndex = 26;
            this.label_CurrentTime.Text = "00:00.0";
            // 
            // volumePot
            // 
            this.volumePot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.volumePot.Location = new System.Drawing.Point(537, 0);
            this.volumePot.Maximum = 1D;
            this.volumePot.Minimum = 0D;
            this.volumePot.Name = "volumePot";
            this.volumePot.Size = new System.Drawing.Size(63, 48);
            this.volumePot.TabIndex = 21;
            this.volumePot.Value = 50D;
            this.volumePot.ValueChanged += new System.EventHandler(this.VolumePot_ValueChanged);
            // 
            // AudioPlaybackPanel
            // 
            this.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label_CurrentTime);
            this.Controls.Add(this.label_TotalTime);
            this.Controls.Add(this.btn_stop);
            this.Controls.Add(this.btn_pause);
            this.Controls.Add(this.btn_play);
            this.Controls.Add(this.panelOutputDeviceSettings);
            this.Controls.Add(this.volumeMeter2);
            this.Controls.Add(this.volumeMeter1);
            this.Controls.Add(this.trackBarPosition);
            this.Name = "AudioPlaybackPanel";
            this.Size = new System.Drawing.Size(600, 48);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPosition)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Form1_Load(object sender, EventArgs e) {
        }

        #endregion
        private System.Windows.Forms.TrackBar trackBarPosition;
        private System.Windows.Forms.Timer timer1;
        private NAudio.Gui.VolumeMeter volumeMeter1;
        private NAudio.Gui.VolumeMeter volumeMeter2;
        private VolumePot volumePot;
        private System.Windows.Forms.Panel panelOutputDeviceSettings;
        private DevExpress.XtraEditors.SimpleButton btn_play;
        private DevExpress.XtraEditors.SimpleButton btn_pause;
        private DevExpress.XtraEditors.SimpleButton btn_stop;
        private System.Windows.Forms.Label label_TotalTime;
        private System.Windows.Forms.Label label_CurrentTime;
    }
}