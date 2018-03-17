using Replayer.WinForms.Ui.Gui;
using DevExpress.XtraEditors;
namespace Replayer.WinForms.Ui.Views.StandardMenu {
    partial class StandardMenuView {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StandardMenuView));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.compilationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openCompilationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createNewCompilationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editCompilationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCompilationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCompilationAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addTrackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteSelectedTrackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editSelectedTrackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createCueHereToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addCueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteSelectedCueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editSelectedCueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uISettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuStrip1.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.compilationToolStripMenuItem,
            this.trackToolStripMenuItem,
            this.cueToolStripMenuItem,
            this.applicationToolStripMenuItem});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(266, 172);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // compilationToolStripMenuItem
            // 
            this.compilationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openCompilationToolStripMenuItem,
            this.createNewCompilationToolStripMenuItem,
            this.editCompilationToolStripMenuItem,
            this.saveCompilationToolStripMenuItem,
            this.saveCompilationAsToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.compilationToolStripMenuItem.Image = global::Replayer.WinForms.Ui.Properties.Resources.Compilation_Icon_256x256;
            this.compilationToolStripMenuItem.Name = "compilationToolStripMenuItem";
            this.compilationToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.compilationToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
            this.compilationToolStripMenuItem.Size = new System.Drawing.Size(177, 36);
            this.compilationToolStripMenuItem.Text = "Compilation";
            this.compilationToolStripMenuItem.ToolTipText = "Actions for creating and manipulating compilations";
            // 
            // openCompilationToolStripMenuItem
            // 
            this.openCompilationToolStripMenuItem.Image = global::Replayer.WinForms.Ui.Properties.Resources.CompilationOpen_Icon_256x256;
            this.openCompilationToolStripMenuItem.Name = "openCompilationToolStripMenuItem";
            this.openCompilationToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.openCompilationToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.O)));
            this.openCompilationToolStripMenuItem.Size = new System.Drawing.Size(238, 38);
            this.openCompilationToolStripMenuItem.Text = "Open...";
            // 
            // createNewCompilationToolStripMenuItem
            // 
            this.createNewCompilationToolStripMenuItem.Image = global::Replayer.WinForms.Ui.Properties.Resources.CompilationNew_Icon_256x256;
            this.createNewCompilationToolStripMenuItem.Name = "createNewCompilationToolStripMenuItem";
            this.createNewCompilationToolStripMenuItem.Size = new System.Drawing.Size(238, 38);
            this.createNewCompilationToolStripMenuItem.Text = "Create New...";
            // 
            // editCompilationToolStripMenuItem
            // 
            this.editCompilationToolStripMenuItem.Image = global::Replayer.WinForms.Ui.Properties.Resources.CompilationEdit_Icon_256x256;
            this.editCompilationToolStripMenuItem.Name = "editCompilationToolStripMenuItem";
            this.editCompilationToolStripMenuItem.Size = new System.Drawing.Size(238, 38);
            this.editCompilationToolStripMenuItem.Text = "Edit...";
            this.editCompilationToolStripMenuItem.Click += new System.EventHandler(this.editCompilationToolStripMenuItem_Click);
            // 
            // saveCompilationToolStripMenuItem
            // 
            this.saveCompilationToolStripMenuItem.Image = global::Replayer.WinForms.Ui.Properties.Resources.CompilationSave_Icon_256x256;
            this.saveCompilationToolStripMenuItem.Name = "saveCompilationToolStripMenuItem";
            this.saveCompilationToolStripMenuItem.Size = new System.Drawing.Size(238, 38);
            this.saveCompilationToolStripMenuItem.Text = "Save...";
            // 
            // saveCompilationAsToolStripMenuItem
            // 
            this.saveCompilationAsToolStripMenuItem.Image = global::Replayer.WinForms.Ui.Properties.Resources.CompilationSaveAs_Icon_256x256;
            this.saveCompilationAsToolStripMenuItem.Name = "saveCompilationAsToolStripMenuItem";
            this.saveCompilationAsToolStripMenuItem.Size = new System.Drawing.Size(238, 38);
            this.saveCompilationAsToolStripMenuItem.Text = "Save As...";
            this.saveCompilationAsToolStripMenuItem.Click += new System.EventHandler(this.saveCompilationAsToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Image = global::Replayer.WinForms.Ui.Properties.Resources.CompilationSaveAs_Icon_256x256;
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(238, 38);
            this.exportToolStripMenuItem.Text = "Export...";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // trackToolStripMenuItem
            // 
            this.trackToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addTrackToolStripMenuItem,
            this.deleteSelectedTrackToolStripMenuItem,
            this.editSelectedTrackToolStripMenuItem});
            this.trackToolStripMenuItem.Image = global::Replayer.WinForms.Ui.Properties.Resources.Track_Icon_256x256;
            this.trackToolStripMenuItem.Name = "trackToolStripMenuItem";
            this.trackToolStripMenuItem.Size = new System.Drawing.Size(108, 36);
            this.trackToolStripMenuItem.Text = "Track";
            // 
            // addTrackToolStripMenuItem
            // 
            this.addTrackToolStripMenuItem.Image = global::Replayer.WinForms.Ui.Properties.Resources.TrackAdd_Icon_256x256;
            this.addTrackToolStripMenuItem.Name = "addTrackToolStripMenuItem";
            this.addTrackToolStripMenuItem.Size = new System.Drawing.Size(305, 38);
            this.addTrackToolStripMenuItem.Text = "Add Track...";
            this.addTrackToolStripMenuItem.Click += new System.EventHandler(this.addTrackToolStripMenuItem_Click);
            // 
            // deleteSelectedTrackToolStripMenuItem
            // 
            this.deleteSelectedTrackToolStripMenuItem.Image = global::Replayer.WinForms.Ui.Properties.Resources.TrackRemove_Icon_256x256;
            this.deleteSelectedTrackToolStripMenuItem.Name = "deleteSelectedTrackToolStripMenuItem";
            this.deleteSelectedTrackToolStripMenuItem.Size = new System.Drawing.Size(305, 38);
            this.deleteSelectedTrackToolStripMenuItem.Text = "Delete Selected Track";
            this.deleteSelectedTrackToolStripMenuItem.Click += new System.EventHandler(this.DeleteSelectedTrackToolStripMenuItemClick);
            // 
            // editSelectedTrackToolStripMenuItem
            // 
            this.editSelectedTrackToolStripMenuItem.Image = global::Replayer.WinForms.Ui.Properties.Resources.TrackEdit_Icon_256x256;
            this.editSelectedTrackToolStripMenuItem.Name = "editSelectedTrackToolStripMenuItem";
            this.editSelectedTrackToolStripMenuItem.Size = new System.Drawing.Size(305, 38);
            this.editSelectedTrackToolStripMenuItem.Text = "Edit Selected Track";
            this.editSelectedTrackToolStripMenuItem.Click += new System.EventHandler(this.EditSelectedTrackToolStripMenuItemClick);
            // 
            // cueToolStripMenuItem
            // 
            this.cueToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createCueHereToolStripMenuItem,
            this.addCueToolStripMenuItem,
            this.deleteSelectedCueToolStripMenuItem,
            this.editSelectedCueToolStripMenuItem});
            this.cueToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cueToolStripMenuItem.Image")));
            this.cueToolStripMenuItem.Name = "cueToolStripMenuItem";
            this.cueToolStripMenuItem.Size = new System.Drawing.Size(95, 36);
            this.cueToolStripMenuItem.Text = "Cue";
            // 
            // createCueHereToolStripMenuItem
            // 
            this.createCueHereToolStripMenuItem.Image = global::Replayer.WinForms.Ui.Properties.Resources.CueCreateHere_Icon_256x256;
            this.createCueHereToolStripMenuItem.Name = "createCueHereToolStripMenuItem";
            this.createCueHereToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.createCueHereToolStripMenuItem.Size = new System.Drawing.Size(306, 38);
            this.createCueHereToolStripMenuItem.Text = "Create Cue here!";
            this.createCueHereToolStripMenuItem.Click += new System.EventHandler(this.CreateCueHereToolStripMenuItemClick);
            // 
            // addCueToolStripMenuItem
            // 
            this.addCueToolStripMenuItem.Image = global::Replayer.WinForms.Ui.Properties.Resources.CueAdd_Icon_256x256;
            this.addCueToolStripMenuItem.Name = "addCueToolStripMenuItem";
            this.addCueToolStripMenuItem.Size = new System.Drawing.Size(306, 38);
            this.addCueToolStripMenuItem.Text = "Add Cue...";
            // 
            // deleteSelectedCueToolStripMenuItem
            // 
            this.deleteSelectedCueToolStripMenuItem.Image = global::Replayer.WinForms.Ui.Properties.Resources.CueDelete_Icon_256x256;
            this.deleteSelectedCueToolStripMenuItem.Name = "deleteSelectedCueToolStripMenuItem";
            this.deleteSelectedCueToolStripMenuItem.Size = new System.Drawing.Size(306, 38);
            this.deleteSelectedCueToolStripMenuItem.Text = "Delete Selected Cue";
            this.deleteSelectedCueToolStripMenuItem.Click += new System.EventHandler(this.DeleteSelectedCueToolStripMenuItemClick);
            // 
            // editSelectedCueToolStripMenuItem
            // 
            this.editSelectedCueToolStripMenuItem.Image = global::Replayer.WinForms.Ui.Properties.Resources.CueEdit_Icon_256x256;
            this.editSelectedCueToolStripMenuItem.Name = "editSelectedCueToolStripMenuItem";
            this.editSelectedCueToolStripMenuItem.Size = new System.Drawing.Size(306, 38);
            this.editSelectedCueToolStripMenuItem.Text = "Edit Selected Cue";
            this.editSelectedCueToolStripMenuItem.Click += new System.EventHandler(this.EditSelectedCueToolStripMenuItemClick);
            // 
            // applicationToolStripMenuItem
            // 
            this.applicationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem1,
            this.settingsToolStripMenuItem,
            this.uISettingsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.applicationToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("applicationToolStripMenuItem.Image")));
            this.applicationToolStripMenuItem.Name = "applicationToolStripMenuItem";
            this.applicationToolStripMenuItem.Size = new System.Drawing.Size(169, 36);
            this.applicationToolStripMenuItem.Text = "Application";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Image = global::Replayer.WinForms.Ui.Properties.Resources.About_Icon_256x256;
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(219, 38);
            this.aboutToolStripMenuItem1.Text = "About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.AboutToolStripMenuItem1Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Image = global::Replayer.WinForms.Ui.Properties.Resources.Settings_Icon_256x256;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(219, 38);
            this.settingsToolStripMenuItem.Text = "Settings...";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.SettingsToolStripMenuItemClick);
            // 
            // uISettingsToolStripMenuItem
            // 
            this.uISettingsToolStripMenuItem.Image = global::Replayer.WinForms.Ui.Properties.Resources.Settings_Icon_256x256;
            this.uISettingsToolStripMenuItem.Name = "uISettingsToolStripMenuItem";
            this.uISettingsToolStripMenuItem.Size = new System.Drawing.Size(219, 38);
            this.uISettingsToolStripMenuItem.Text = "UI Settings...";
            this.uISettingsToolStripMenuItem.Click += new System.EventHandler(this.uISettingsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::Replayer.WinForms.Ui.Properties.Resources.cancel_64x64;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(219, 38);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // StandardMenuView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.menuStrip1);
            this.Name = "StandardMenuView";
            this.Size = new System.Drawing.Size(266, 172);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.ToolStripMenuItem editSelectedCueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editSelectedTrackToolStripMenuItem;

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem compilationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openCompilationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveCompilationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveCompilationAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createNewCompilationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addTrackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteSelectedTrackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addCueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteSelectedCueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createCueHereToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem applicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editCompilationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uISettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
    }
}
