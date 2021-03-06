﻿using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Replayer.Core;
using Replayer.Core.Data;
using Replayer.Model;
using Replayer.WinForms.Ui.Components;
using Replayer.WinForms.Ui.Gui;
using Replayer.WinForms.Ui.Properties;
using Replayer.WinForms.Ui.Views.Tracks;

namespace Replayer.WinForms.Ui {
    /// <summary>
    ///     Handles the events with visual elements in the Replayer application
    /// </summary>
    internal class ReplayerApplicationEventHandler {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ReplayerApplicationEventHandler" /> class.
        /// </summary>
        public ReplayerApplicationEventHandler() {
            EventBroker.Instance.EventOccured += EventBroker_EventOccured;
        }

        /// <summary>
        ///     Handles an event from the event broker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventBroker_EventOccured(object sender, EventArgs<string> e) {
            switch (e.Data) {
                case "Menu:OpenCompilationClicked": {
                        HandleOpenCompilationClickedEvent();
                        break;
                    }
                case "Menu:EditCompilationClicked": {
                        HandleEditCompilationClickedEvent();
                        break;
                    }
                case "Menu:AboutClicked": {
                        HandleAboutClickedEvent();
                        break;
                    }
                case "Menu:OnlineHelpClicked": {
                        HandleOnlineHelpClickedEvent();
                        break;
                    }
                case "Menu:ExitClicked": {
                        HandleExitClickedEvent();
                        break;
                    }
                case "Menu:SettingsClicked": {
                        HandleSettingsClickedEvent();
                        break;
                    }
                case "Menu:UiSettingsClicked": {
                        HandleUiSettingsClickedEvent();
                        break;
                    }
                case "Menu:CreateNewCompilationClicked": {
                        HandleCreateNewCompilationClickedEvent();
                        break;
                    }
                case "Menu:AddCueClicked": {
                        HandleAddCueClickedEvent();
                        break;
                    }

                case "Menu:CreateCueHereClicked": {
                        Core.Model.Instance.CreateCueHere();
                        Core.Model.Instance.Compilation.IsDirty = true;
                        break;
                    }
                case "Menu:DeleteSelectedCue": {
                        Core.Model.Instance.RemoveSelectedCue();
                        Core.Model.Instance.Compilation.IsDirty = true;
                        break;
                    }
                case "Menu:MoveUpSelectedCue": {
                        Core.Model.Instance.MoveSelectedCue(-1);
                        Core.Model.Instance.Compilation.IsDirty = true;
                        break;
                    }
                case "Menu:MoveDownSelectedCue": {
                        Core.Model.Instance.MoveSelectedCue(+1);
                        Core.Model.Instance.Compilation.IsDirty = true;
                        break;
                    }
                case "Menu:MoveUpSelectedTrack": {
                        Core.Model.Instance.MoveSelectedTrack(-1);
                        Core.Model.Instance.Compilation.IsDirty = true;
                        break;
                    }
                case "Menu:MoveDownSelectedTrack": {
                        Core.Model.Instance.MoveSelectedTrack(+1);
                        Core.Model.Instance.Compilation.IsDirty = true;
                        break;
                    }
                case "Menu:DeleteSelectedTrack": {
                        Core.Model.Instance.RemoveSelectedTrack();
                        Core.Model.Instance.Compilation.IsDirty = true;
                        break;
                    }
                case "Menu:CloneSelectedTrack": {
                        Core.Model.Instance.CloneSelectedTrack();
                        Core.Model.Instance.Compilation.IsDirty = true;
                        break;
                    }
                case "Menu:EditSelectedCue": {
                        if (Core.Model.Instance.SelectedCue != null) //there is any selected?
                        {
                            Cue cloneCue = Core.Model.Instance.SelectedCue.Clone(); //clone for private manipulation
                            using (PropertyDialog propertyDialog = new PropertyDialog(cloneCue, "Edit cue")) {
                                propertyDialog.Height = 353;
                                if (propertyDialog.ShowDialog() == DialogResult.OK) {
                                    Core.Model.Instance.SelectedTrack.Cues.Replace(Core.Model.Instance.SelectedCue, cloneCue);
                                    //assign back, causing an update of the model
                                    Core.Model.Instance.SelectedCue = cloneCue; //use the new as selected
                                    Core.Model.Instance.Compilation.IsDirty = true;
                                }
                            }
                        }
                        break;
                    }

                case "Menu:AddTrackClicked": {
                        HandleAddTrackClickedEvent();
                        break;
                    }
                case "Menu:EditSelectedTrack": {
                        if (Core.Model.Instance.SelectedTrack != null) //there is any selected?
                        {
                            DisplayTrack displayTrack = new DisplayTrack(Core.Model.Instance.SelectedTrack.Clone());
                            //clone for private manipulation
                            using (PropertyDialog propertyDialog = new PropertyDialog(displayTrack, "Edit track")) {
                                propertyDialog.Height = 393;
                                propertyDialog.Width = 800; //to have more room for the file name.
                                if (propertyDialog.ShowDialog() == DialogResult.OK) {
                                    Core.Model.Instance.Compilation.Tracks.Replace(Core.Model.Instance.SelectedTrack,
                                                                                   displayTrack.Model);
                                    //assign back, causing an update of the model
                                    Core.Model.Instance.SelectedTrack = displayTrack.Model; //use the new as selected
                                    Core.Model.Instance.Compilation.IsDirty = true;
                                }
                            }
                        }
                        break;
                    }

                case "Menu:SaveFile": {
                        HandleSaveFileEvent();
                        break;
                    }
                case "Menu:SaveFileAs": {
                        HandleSaveFileAsEvent();
                        break;
                    }
                case "Menu:ExportFile": {
                        HandleExportFileEvent();
                        break;
                    }

                default:
                    break;
            }
        }

        /// <summary>
        ///     Removes all non-alphanumeric characters and replaces them with a dash.
        /// </summary>
        /// <remarks>
        ///     This keeps the filename usable over most operating systems, either when used
        ///     as URL or as local filename.
        /// </remarks>
        /// <param name="filename"></param>
        /// <returns></returns>
        private String EncodeFileName(String filename) {
            Regex toAlphanumeric = new Regex("[^a-zA-Z0-9]");
            string encoded = toAlphanumeric.Replace(filename, "-");
            Regex toSingleDash = new Regex("--+");
            string simplified = toSingleDash.Replace(encoded, "-");
            return simplified;
        }

        /// <summary>
        ///     Handles the export file clicked event.
        /// </summary>
        private void HandleExportFileEvent() {
            if (Core.Model.Instance.Compilation != null) //there is any?
            {
                // Displays a ExportFileDialog so the user can export the compilaton
                using (SaveFileDialog exportFileDialog = new SaveFileDialog {
                    Filter = "Quirli compilation (EXPERIMENTAL)|*.html",
                    Title = "Export the compilation to a location.",
                    InitialDirectory = Core.Model.Instance.Settings.DefaultCompilationLookupDirectory,
                    FileName = EncodeFileName(Core.Model.Instance.Compilation.Title) + ".html"
                }) {
                    exportFileDialog.ShowDialog();
                    // If the file name is not an empty string open it for exporting.
                    if (exportFileDialog.FileName != "") {
                        // Exports the compilation in the appropriate format based upon the
                        // File type selected in the dialog box.
                        // NOTE that the FilterIndex property is one-based.
                        switch (exportFileDialog.FilterIndex) {
                            case 1: //Quirli

                                if (Core.Model.Instance.Compilation.Type != CompilationType.Quirli) //conversion needed?
                                {
                                    CompilationFactory.Convert(Core.Model.Instance.Compilation, CompilationType.Quirli)
                                                      .Store(exportFileDialog.FileName);
                                }
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     Handles the edit compilation clicked event.
        /// </summary>
        private void HandleEditCompilationClickedEvent() {
            if (Core.Model.Instance.Compilation != null) //there is any?
            {
                //create visual copy
                DisplayCompilation displayCompilation = new DisplayCompilation(Core.Model.Instance.Compilation);
                using (PropertyDialog propertyDialog = new PropertyDialog(displayCompilation, "Edit compilation")) {
                    if (propertyDialog.ShowDialog() == DialogResult.OK) {
                        Core.Model.Instance.Compilation.Title = displayCompilation.Title; //use the changed data
                        Core.Model.Instance.Compilation.IsDirty = true;
                    }
                }
            }
        }

        /// <summary>
        ///     Handles the save file as event.
        /// </summary>
        private void HandleSaveFileAsEvent() {
            if (Core.Model.Instance.Compilation != null) //there is any?
            {
                // Displays a SaveFileDialog so the user can save the compilaton
                using (SaveFileDialog saveFileDialog = new SaveFileDialog {
                    Filter = "Zip compilation|*.rez|XML compilation|*.rex|Quirli compilation (EXPERIMENTAL)|*.html",
                    Title = "Save a compilation to a new location.",
                    InitialDirectory =
                        Core.Model.Instance.Settings.DefaultCompilationLookupDirectory
                }) {
                    saveFileDialog.ShowDialog();
                    // If the file name is not an empty string open it for saving.
                    if (saveFileDialog.FileName != "") {
                        // Saves the compilation in the appropriate format based upon the
                        // File type selected in the dialog box.
                        // NOTE that the FilterIndex property is one-based.
                        switch (saveFileDialog.FilterIndex) {
                            case 1: //ZIP

                                if (Core.Model.Instance.Compilation.Type != CompilationType.Zip) //conversion needed?
                                {
                                    Core.Model.Instance.Compilation =
                                        CompilationFactory.Convert(Core.Model.Instance.Compilation, CompilationType.Zip);
                                }
                                break;
                            case 2: //XML

                                if (Core.Model.Instance.Compilation.Type != CompilationType.Xml) //conversion needed?
                                {
                                    Core.Model.Instance.Compilation =
                                        CompilationFactory.Convert(Core.Model.Instance.Compilation, CompilationType.Xml);
                                }
                                break;
                            case 3: //Quirli

                                if (Core.Model.Instance.Compilation.Type != CompilationType.Quirli) //conversion needed?
                                {
                                    Core.Model.Instance.Compilation =
                                        CompilationFactory.Convert(Core.Model.Instance.Compilation, CompilationType.Quirli);
                                }
                                break;
                        }
                        Core.Model.Instance.Compilation.Store(saveFileDialog.FileName);
                        //save the eventually converted session to the specified filename now
                        Core.Model.Instance.Settings.DefaultCompilationLookupDirectory =
                            Path.GetDirectoryName(saveFileDialog.FileName);
                    }
                }
            }
        }

        /// <summary>
        ///     Handles the save file event.
        /// </summary>
        private void HandleSaveFileEvent() {
            try {
                if (Core.Model.Instance.Compilation != null) //there is any?
                {
                    Core.Model.Instance.Compilation.Store();
                    Core.Model.Instance.Compilation.IsDirty = false;
                }
            }
            catch (ArgumentException) //if the file path is invalid
          {
                //try to save as...
                EventBroker.Instance.IssueEvent("Menu:SaveFileAs");
            }
        }


        /// <summary>
        /// Handles the create new compilation clicked event.
        /// </summary>
        private void HandleCreateNewCompilationClickedEvent() {
            PreventDiscadingUnsavedCompilation();
            CreateNewCompilationWizard wiz = new CreateNewCompilationWizard();
            wiz.ShowDialog();
            return;
        }

        /// <summary>
        ///     Prevents discarding of an unsaved compilation by asking the user to save first.
        /// </summary>
        private void PreventDiscadingUnsavedCompilation() {
            //prevent the user from having the old compilation overwritten
            if (
                (Core.Model.Instance.Compilation != null) &&
                (Core.Model.Instance.Compilation.IsDirty)
                ) {
                if (new Question("Do you want to save the existing compilation?").Ask().Equals(DialogResult.OK)) {
                    HandleSaveFileEvent(); //invoke the save event handler
                }
            }
        }

        /// <summary>
        ///     Handles the add track clicked event.
        /// </summary>
        private void HandleAddTrackClickedEvent() {
            //check preconditions
            if (Core.Model.Instance.Compilation == null) //no compilation
            {
                //nothing to add
                MessageBox.Show("There is no compilation to add tracks to. Create or load a compilation first.");
                return;
            }

            Track newTrack = new Track();
            DisplayTrack displayTrack = new DisplayTrack(newTrack);

            PropertyDialog propDialog = new PropertyDialog(displayTrack, "Enter track details");

            if (propDialog.ShowDialog() == DialogResult.OK) {
                //create initial cue
                newTrack.Cues.Add(
                    new Cue {
                        Description = "Intro (autogenerated)",
                        Shortcut = "1",
                        Time = 0
                    });
                Core.Model.Instance.Compilation.Tracks.Add(newTrack);
                Core.Model.Instance.Compilation.IsDirty = true;
            }
            return;
        }

        /// <summary>
        ///     Handles the add cue clicked event.
        /// </summary>
        private void HandleAddCueClickedEvent() {
            //check preconditions
            if (
                (Core.Model.Instance.Compilation == null) || //no compilation
                (Core.Model.Instance.SelectedTrack == null) //no selected track.
                ) {
                //nothing to add
                MessageBox.Show(
                    "There is no track to add cues to. Create or load a compilation and create select a track first.");
                return;
            }

            Cue newCue = new Cue();
            //DisplayCue displayCue = new DisplayCue(String.Empty, newCue);

            PropertyDialog propDialog = new PropertyDialog(newCue, "Enter cue details");

            if (propDialog.ShowDialog() == DialogResult.OK) {
                //add cue
                Core.Model.Instance.SelectedTrack.Cues.Add(newCue);
                Core.Model.Instance.Compilation.IsDirty = true;
            }
            return;
        }


        /// <summary>
        ///     Handles the settings clicked event.
        /// </summary>
        private void HandleSettingsClickedEvent() {
            Core.Model.Instance.Settings.Save(); //be sure to let everything persist before any possible change
            using (
                PropertyDialog dlg = new PropertyDialog(new DisplayableCoreSettings(Core.Model.Instance.Settings), "Behavioral Settings")
                ) {
                DialogResult result = dlg.ShowDialog();
                if (result.Equals(DialogResult.OK)) //confirmed?
                {
                    Core.Model.Instance.Settings.Save(); //save what was changed
                }
                else //not confirmed
              {
                    Core.Model.Instance.Settings.Reload(); //revert to saved
                }
            }
        }

        /// <summary>
        ///     Handles the ui settings clicked event.
        /// </summary>
        private void HandleUiSettingsClickedEvent() {
            Settings.Default.Save(); //save any previous changes, to be sure the most current settings are persisted.
            using (PropertyDialog dlg = new PropertyDialog(new DisplayableUiSettings(Settings.Default), "UI Settings")) {
                DialogResult result = dlg.ShowDialog();
                if (result.Equals(DialogResult.OK)) //confirmed?
                {
                    Settings.Default.Save(); //save what was changed
                }
                else //not confirmed
              {
                    Settings.Default.Reload(); //restore back
                }
            }
        }

        /// <summary>
        ///     Handles the exit clicked event.
        /// </summary>
        private void HandleExitClickedEvent() {
            //Application.Exit();
        }

        /// <summary>
        ///     Handles the online help clicked event.
        /// </summary>
        private void HandleOnlineHelpClickedEvent() {
            Process.Start("iexplore.exe", "replayer.ch");
        }

        /// <summary>
        ///     Handles the about clicked event.
        /// </summary>
        private void HandleAboutClickedEvent() {
            using (AboutBox aboutBox = new AboutBox()) {
                aboutBox.ShowDialog();
            }
        }


        /// <summary>
        ///     Handles the open compilation clicked event.
        /// </summary>
        private void HandleOpenCompilationClickedEvent() {
            PreventDiscadingUnsavedCompilation();

            OpenFileDialog openFileDialog = new OpenFileDialog {
                InitialDirectory = Core.Model.Instance.Settings.DefaultCompilationLookupDirectory,
                Filter =
                "Compilation files (*.rez (zipped), *.rex(xml), *.zip(deprecated))|*.rez;*.rex;*.zip",
                FilterIndex = 1
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                Core.Model.Instance.Retrieve(openFileDialog.FileName);
                //use directory as default
                Core.Model.Instance.Settings.DefaultCompilationLookupDirectory =
                    Path.GetDirectoryName(openFileDialog.FileName);
                Core.Model.Instance.Settings.Save(); //immediately save for next use.
            }
        }
    }
}