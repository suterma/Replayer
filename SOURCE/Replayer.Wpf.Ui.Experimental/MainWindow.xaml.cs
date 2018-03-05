using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Replayer.Core.v04;
using Replayer.Core.v04.Annotation;
using System.Threading;
using System.Threading.Tasks;

namespace Replayer.Wpf.Ui.Experimental
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            //TODO manual startup
            Model.Instance.Compilation = CompilationFactory.Retrieve(@"C:\Documents and Settings\msuter\My Documents\Replayer\test_corev03.rez");
            //Model.Instance.Player; // mediaReplayer1;
            Model.Instance.Player.Volume = 50;
            Model.Instance.Player.Position = new TimeSpan(0);

            this.accordion1.ItemsSource = Model.Instance.Compilation.Tracks;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //TODO test: make sure the accordion has it's items closed
            //Thread.Sleep(1999);

            //accordion1.SelectionChanged += new SelectionChangedEventHandler(accordion1_SelectionChanged);
            accordion1.UnselectAll();


            //Model.Instance.SelectedTrack = Model.Instance.Compilation.Tracks[0]; //simple: user first
            //more advanced use the currently selected one
            //Model.Instance.SelectedTrack = accordion1.SelectedItem as Track;

            //The one to delete MUST NOT BE EXPANDED at the moment of deletion. How to achieve that?
            //like this: 
            (new Task(() =>
            {
                Model.Instance.SelectedTrack = Model.Instance.Compilation.Tracks[0];
                Model.Instance.RemoveSelectedTrack();
            })).Start(uiScheduler);

            //copy this code to the default wpf ui!!!
        }

        //void accordion1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    Model.Instance.SelectedTrack = Model.Instance.Compilation.Tracks[0];           
        //    Model.Instance.RemoveSelectedTrack();
        //}


        /// <summary>
        /// Allows execution of tasks on the ui thread
        /// </summary>
        TaskScheduler uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();
    }
}
