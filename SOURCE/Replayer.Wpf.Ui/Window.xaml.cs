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
using System.Windows.Shapes;
using Fluent;
using Replayer.Core.v04;
using Replayer.Core.v04.Annotation;
using System.Windows.Threading;
using Replayer.Core.v04.Player;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace Replayer.Wpf.Ui
{
    /// <summary>
    /// Interaction logic for Window.xaml
    /// </summary>
    public partial class Window : RibbonWindow
    {
        public Window()
        {
            InitializeComponent();

            //IsUiUpdateAllowed = true; //start with allowance

            //TODO manual startup
            Model.Instance.Compilation = CompilationFactory.Retrieve(@"C:\Documents and Settings\msuter\My Documents\Replayer\test_corev03.rez");
            Model.Instance.Player = mediaReplayer1;
            Model.Instance.Player.Volume = 50;
            Model.Instance.Player.Position = new TimeSpan(0);

            this.accordion1.ItemsSource = Model.Instance.Compilation.Tracks;

            Model.Instance.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Model_PropertyChanged);
        }

        void Model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("SelectedTrack"))
            {
                //if (IsUiUpdateAllowed)
                {
                    accordion1.SelectedItem = Model.Instance.SelectedTrack;
                }
            }
        }

  
        /// <summary>
        /// Handles the selection changed event on the cues listbox control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CuesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (
                (e.AddedItems == null) ||
                (e.AddedItems.Count == 0)
                )
            {
                return; //no new item, no different item selected!
        }
            var cue = (e.AddedItems[0] as Cue);
            if (cue != null) //one is selected?
            {
                Model.Instance.SelectedCue = cue;
             }

        }

        private void accordion1_SelectedItemsChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems == null) return; //no new item, no different item selected!
             var track = (e.NewItems[0] as Track);
            if (track != null) //one is selected?
            {
                var tododeleteitems = accordion1.Items;
                //if (IsUiUpdateAllowed) //we are allowed to update the ui (and vice versa)?
                {
                    Model.Instance.SelectedTrack = track;
                }//else do nothing
            }
        }

        //private void ButtonPause_Click(object sender, RoutedEventArgs e)
        //{
        //    Model.Instance.Player.State = Core.v04.Player.MediaPlayerState.Paused;
        //}

        //private void ButtonStart_Click(object sender, RoutedEventArgs e)
        //{
        //    Model.Instance.Player.State = Core.v04.Player.MediaPlayerState.Playing;
        //}     

        /// <summary>
        /// Handles the click event of the create cue here button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCreateCueHere_Click(object sender, RoutedEventArgs e)
        {
            //automatically create a new cue at the current position
            Model.Instance.CreateCueHere();
        }

        #region commanding

        /// <summary>
        /// Executes the action on the play command.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="e"></param>
        void PlayCmdExecuted(object target, ExecutedRoutedEventArgs e)
        {
            Model.Instance.Player.State = MediaPlayerState.Playing;
        }
        /// <summary>
        /// Determines whether the play command can execute.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PlayCmdCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ( (Model.Instance.SelectedCue != null) && //only allow playing if a cue is selected
                (Model.Instance.Player.State != MediaPlayerState.Playing)); //and not yet playing
        }


        /// <summary>
        /// Executes the action on the pause command
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PauseCmdExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Model.Instance.Player.State = MediaPlayerState.Paused;
        }

        /// <summary>
        /// Determines whether the pause command can execute
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PauseCmdCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (Model.Instance.Player.State == MediaPlayerState.Playing);
        }

        public ObservableCollection<Track> GetTracks()
        {
            return Model.Instance.Compilation.Tracks;
        }

        /// <summary>
        /// Executes the action on the editing delete (selected item) command.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditingDeleteCmdExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (
                (e.Parameter is Cue) || //we expect that this command is executed only on the selected cue
                (e.Parameter.Equals("SelectedCue")) //from the button in the edit menu
                )
            {
                Model.Instance.RemoveSelectedCue(); 
            }
            else if (
                (e.Parameter is Track) || //we expect that this command is executed only on the selected cue
                (e.Parameter.Equals("SelectedTrack")) //from the button in the edit menu
            )
            {
                ///devdoc: Deletion of an item in the accordion can only work if it is not expanded.
                ///Thus, first unelect all, and then delete the item, in a separate call on the same 
                ///thread. This is probably a bug, or missing feature at least

                //IsUiUpdateAllowed = false; //do not update the ui during the deletion itself (Otherwise some bad exceptions may occurr in the accordion)
                accordion1.UnselectAll(); //make sure the accordion has it's items closed (Otherwise some bad exceptions may occurr in the accordion)
            (new Task(() =>
            {
                Model.Instance.RemoveSelectedTrack();
            })).Start(_uiScheduler); //this will start the task after this call finishes, on the same thread

        
                //IsUiUpdateAllowed = true; //allow the ui to follow the model again
            }
        }

        /// <summary>
        /// Allows execution of tasks on the ui thread
        /// </summary>
        private TaskScheduler _uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();

        /// <summary>
        /// Determines whether the editing delete command can execute.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditingDeleteCmdCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //TODOdelete            e.CanExecute = true;
                e.CanExecute = 
                    (Model.Instance.SelectedCue != null) ||
                    (Model.Instance.SelectedTrack != null);
        }    
        #endregion


        /// <summary>
        /// Handles the lost focus event of the cues listbox control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CuesListBox_LostFocus(object sender, RoutedEventArgs e)
        {
            (sender as ListBox).UnselectAll(); //to keep the listbox ready to select again for the next time it gets focused (otherwise the selected item would not get fired upon new focus of another list)
        }







        /// <summary>
        /// Indicates whether the UI is allowed to update.
        /// </summary>
        /// <devdoc>This is currently used to avoid exceptions during track deletion.</devdoc>
        //private bool IsUiUpdateAllowed { get; set; }
    }
}
