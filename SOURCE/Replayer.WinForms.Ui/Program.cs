﻿using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Microsoft.SqlServer.MessageBox;

namespace Replayer.WinForms.Ui {


    internal static class Program {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args) {
            // Add the event handler for handling UI thread exceptions to the event.
            Application.ThreadException += Form1_UIThreadException;

            // Set the unhandled exception mode to force all Windows Forms errors to go through
            // our handler.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // Add the event handler for handling non-UI thread exceptions to the event. 
            AppDomain.CurrentDomain.UnhandledException +=
                CurrentDomain_UnhandledException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            new Startup().Run(args);
        }

        // Handle the UI exceptions by showing a dialog box, and asking the user whether
        // or not they wish to abort execution.
        private static void Form1_UIThreadException(object sender, ThreadExceptionEventArgs t) {
            DialogResult result = DialogResult.Cancel;
            try {
                result =
                    new ExceptionMessageBox(t.Exception, ExceptionMessageBoxButtons.AbortRetryIgnore,
                                            ExceptionMessageBoxSymbol.Error).Show(null);
            }
            catch {
                try {
                    MessageBox.Show("Fatal Windows Forms Error",
                                    "Fatal Windows Forms Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
                }
                finally {
                    Application.Exit();
                }
            }

            // Exits the program when the user clicks Abort.
            if (result == DialogResult.Abort) {
                Application.Exit();
            }
        }

        // Handle the UI exceptions by showing a dialog box, and asking the user whether
        // or not they wish to abort execution.
        // NOTE: This exception cannot be kept from terminating the application - it can only 
        // log the event, and inform the user about it. 
        private static void CurrentDomain_UnhandledException(object sender, System.UnhandledExceptionEventArgs e) {
            try {
                Exception ex = (Exception)e.ExceptionObject;
                string errorMsg = "An application error occurred. Please contact the adminstrator " +
                                  "with the following information:\n\n";

                new ExceptionMessageBox(ex, ExceptionMessageBoxButtons.AbortRetryIgnore, ExceptionMessageBoxSymbol.Error)
                    .Show(null);

                // Since we can't prevent the app from terminating, log this to the event log.
                if (!EventLog.SourceExists("ThreadException")) {
                    EventLog.CreateEventSource("ThreadException", "Application");
                }

                // Create an EventLog instance and assign its source.
                EventLog myLog = new EventLog();
                myLog.Source = "ThreadException";
                myLog.WriteEntry(errorMsg + ex.Message + "\n\nStack Trace:\n" + ex.StackTrace);
            }
            catch (Exception exc) {
                try {
                    MessageBox.Show("Fatal Non-UI Error",
                                    "Fatal Non-UI Error. Could not write the error to the event log. Reason: "
                                    + exc.Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                finally {
                    Application.Exit();
                }
            }
        }
    }
}