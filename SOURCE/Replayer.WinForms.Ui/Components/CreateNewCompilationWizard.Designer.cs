namespace Replayer.WinForms.Ui.Gui {
    partial class CreateNewCompilationWizard {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateNewCompilationWizard));
            this.wizardControl1 = new WizardBase.WizardControl();
            this.startStep1 = new WizardBase.StartStep();
            this.intermediateStep1 = new WizardBase.IntermediateStep();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._tbCommonArtist = new System.Windows.Forms.TextBox();
            this._tbCommonAlbum = new System.Windows.Forms.TextBox();
            this._lblCommonAlbum = new DevExpress.XtraEditors.LabelControl();
            this._lblCommonArtist = new DevExpress.XtraEditors.LabelControl();
            this._lblCollectionName = new DevExpress.XtraEditors.LabelControl();
            this._tbCollectionName = new System.Windows.Forms.TextBox();
            this.intermediateStep2 = new WizardBase.IntermediateStep();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this._btnBrowserForTracks = new DevExpress.XtraEditors.SimpleButton();
            this._tbTrackMediaFiles = new System.Windows.Forms.TextBox();
            this.finishStep1 = new WizardBase.FinishStep();
            this.intermediateStep1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.intermediateStep2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizardControl1
            // 
            this.wizardControl1.BackButtonEnabled = false;
            this.wizardControl1.BackButtonVisible = true;
            this.wizardControl1.CancelButtonEnabled = true;
            this.wizardControl1.CancelButtonVisible = true;
            this.wizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardControl1.EulaButtonEnabled = false;
            this.wizardControl1.EulaButtonText = "eula";
            this.wizardControl1.EulaButtonVisible = false;
            this.wizardControl1.HelpButtonEnabled = true;
            this.wizardControl1.HelpButtonVisible = false;
            this.wizardControl1.Location = new System.Drawing.Point(0, 0);
            this.wizardControl1.Name = "wizardControl1";
            this.wizardControl1.NextButtonEnabled = true;
            this.wizardControl1.NextButtonVisible = true;
            this.wizardControl1.Size = new System.Drawing.Size(443, 256);
            this.wizardControl1.WizardSteps.AddRange(new WizardBase.WizardStep[] {
            this.startStep1,
            this.intermediateStep1,
            this.intermediateStep2,
            this.finishStep1});
            this.wizardControl1.CancelButtonClick += new System.EventHandler(this.wizardControl1_CancelButtonClick);
            // 
            // startStep1
            // 
            this.startStep1.BindingImage = null;
            this.startStep1.Icon = global::Replayer.WinForms.Ui.Properties.Resources.CompilationNew_Icon_256x256;
            this.startStep1.Name = "startStep1";
            this.startStep1.Subtitle = "First, specify common properties for all tracks, then select the media files to t" +
    "o generate the tracks from.";
            this.startStep1.Title = "Create a new compilation.";
            // 
            // intermediateStep1
            // 
            this.intermediateStep1.BindingImage = ((System.Drawing.Image)(resources.GetObject("intermediateStep1.BindingImage")));
            this.intermediateStep1.Controls.Add(this.tableLayoutPanel1);
            this.intermediateStep1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.intermediateStep1.Name = "intermediateStep1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.18285F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.81715F));
            this.tableLayoutPanel1.Controls.Add(this._tbCommonArtist, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this._tbCommonAlbum, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this._lblCommonAlbum, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this._lblCommonArtist, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this._lblCollectionName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._tbCollectionName, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(0, 0);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // _tbCommonArtist
            // 
            this._tbCommonArtist.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tbCommonArtist.Location = new System.Drawing.Point(3, 3);
            this._tbCommonArtist.Name = "_tbCommonArtist";
            this._tbCommonArtist.Size = new System.Drawing.Size(1, 20);
            this._tbCommonArtist.TabIndex = 6;
            // 
            // _tbCommonAlbum
            // 
            this._tbCommonAlbum.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tbCommonAlbum.Location = new System.Drawing.Point(3, 3);
            this._tbCommonAlbum.Name = "_tbCommonAlbum";
            this._tbCommonAlbum.Size = new System.Drawing.Size(1, 20);
            this._tbCommonAlbum.TabIndex = 5;
            // 
            // _lblCommonAlbum
            // 
            this._lblCommonAlbum.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this._lblCommonAlbum.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this._lblCommonAlbum.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lblCommonAlbum.Location = new System.Drawing.Point(3, 3);
            this._lblCommonAlbum.Name = "_lblCommonAlbum";
            this._lblCommonAlbum.Size = new System.Drawing.Size(1, 0);
            this._lblCommonAlbum.TabIndex = 4;
            this._lblCommonAlbum.Text = "Common Album of all Tracks (if any)";
            // 
            // _lblCommonArtist
            // 
            this._lblCommonArtist.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this._lblCommonArtist.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lblCommonArtist.Location = new System.Drawing.Point(3, 3);
            this._lblCommonArtist.Name = "_lblCommonArtist";
            this._lblCommonArtist.Size = new System.Drawing.Size(1, 0);
            this._lblCommonArtist.TabIndex = 2;
            this._lblCommonArtist.Text = "Common Artist of all Tracks (if any)";
            // 
            // _lblCollectionName
            // 
            this._lblCollectionName.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this._lblCollectionName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this._lblCollectionName.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lblCollectionName.Location = new System.Drawing.Point(3, 3);
            this._lblCollectionName.Name = "_lblCollectionName";
            this._lblCollectionName.Size = new System.Drawing.Size(1, 0);
            this._lblCollectionName.TabIndex = 0;
            this._lblCollectionName.Text = "Collection name:";
            // 
            // _tbCollectionName
            // 
            this._tbCollectionName.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tbCollectionName.Location = new System.Drawing.Point(3, 3);
            this._tbCollectionName.Name = "_tbCollectionName";
            this._tbCollectionName.Size = new System.Drawing.Size(1, 20);
            this._tbCollectionName.TabIndex = 1;
            // 
            // intermediateStep2
            // 
            this.intermediateStep2.BindingImage = ((System.Drawing.Image)(resources.GetObject("intermediateStep2.BindingImage")));
            this.intermediateStep2.Controls.Add(this.tableLayoutPanel2);
            this.intermediateStep2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.intermediateStep2.Name = "intermediateStep2";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.10723F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 73.89278F));
            this.tableLayoutPanel2.Controls.Add(this._btnBrowserForTracks, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this._tbTrackMediaFiles, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(0, 0);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // _btnBrowserForTracks
            // 
            this._btnBrowserForTracks.Appearance.Options.UseTextOptions = true;
            this._btnBrowserForTracks.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this._btnBrowserForTracks.Dock = System.Windows.Forms.DockStyle.Fill;
            this._btnBrowserForTracks.Location = new System.Drawing.Point(3, 3);
            this._btnBrowserForTracks.Name = "_btnBrowserForTracks";
            this._btnBrowserForTracks.Size = new System.Drawing.Size(1, 1);
            this._btnBrowserForTracks.TabIndex = 2;
            this._btnBrowserForTracks.Text = "Browse for media files...     (Each media file will create a new track)";
            this._btnBrowserForTracks.Click += new System.EventHandler(this._btnBrowserForTracks_Click);
            // 
            // _tbTrackMediaFiles
            // 
            this._tbTrackMediaFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tbTrackMediaFiles.Location = new System.Drawing.Point(3, 3);
            this._tbTrackMediaFiles.Multiline = true;
            this._tbTrackMediaFiles.Name = "_tbTrackMediaFiles";
            this._tbTrackMediaFiles.ReadOnly = true;
            this._tbTrackMediaFiles.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._tbTrackMediaFiles.Size = new System.Drawing.Size(1, 1);
            this._tbTrackMediaFiles.TabIndex = 3;
            this._tbTrackMediaFiles.WordWrap = false;
            // 
            // finishStep1
            // 
            this.finishStep1.BindingImage = global::Replayer.WinForms.Ui.Properties.Resources.Cue_Icon_256x256;
            this.finishStep1.Name = "finishStep1";
            // 
            // CreateNewCompilationWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 256);
            this.Controls.Add(this.wizardControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CreateNewCompilationWizard";
            this.Text = "CreateNewCompilationWizard";
            this.intermediateStep1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.intermediateStep2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private WizardBase.WizardControl wizardControl1;
        private WizardBase.StartStep startStep1;
        private WizardBase.IntermediateStep intermediateStep1;
        private WizardBase.FinishStep finishStep1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.LabelControl _lblCollectionName;
        private System.Windows.Forms.TextBox _tbCollectionName;
        private System.Windows.Forms.TextBox _tbCommonArtist;
        private System.Windows.Forms.TextBox _tbCommonAlbum;
        private DevExpress.XtraEditors.LabelControl _lblCommonAlbum;
        private DevExpress.XtraEditors.LabelControl _lblCommonArtist;
        private WizardBase.IntermediateStep intermediateStep2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private DevExpress.XtraEditors.SimpleButton _btnBrowserForTracks;
        private System.Windows.Forms.TextBox _tbTrackMediaFiles;
    }
}