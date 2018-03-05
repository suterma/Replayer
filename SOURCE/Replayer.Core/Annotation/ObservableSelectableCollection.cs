//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Collections.ObjectModel;
//using System.Xml.Serialization;
//using System.Collections.Specialized;

//namespace Replayer.Core.v04.Annotation
//{
//    /// <summary>
//    /// An observable collection of objects, where a single item can be selected. A value
//    /// of null means, that no item is selected.
//    /// </summary>
//    /// <remarks>
//    /// <para>
//    /// The type parameter may only be a class, not a value type, since for value types,
//    /// a selected value may be ambiguous between the items.</para>
//    /// What the meaning of the selected item for the collection actually
//    /// represents, is up to the user of the class.</remarks>    
//    [Serializable]
//    public class ObservableSelectableCollection<T> : ObservableCollection<T> where T : class
//    {

//        /// <summary>
//        /// Inizializes an instance with the specified enumerable.
//        /// </summary>
//        /// <remarks>No item is selected.</remarks>
//        /// <param name="enumerable"></param>
//       public    ObservableSelectableCollection(IEnumerable<T> enumerable)
//           :base(enumerable)
//    {      

//    }

//        /// <summary>
//        /// Initializes a new instance with an empty collection.
//        /// </summary>
//        public ObservableSelectableCollection()
//        : base()
//    {
//    }
//        /// <summary>
//        /// Backing store.
//        /// </summary>
//        private T _SelectedItem;

//        /// <summary>
//        /// Gets or sets the selected item.
//        /// </summary>
//        /// <remarks><para>The selected item, (if the value is set to another
//        /// value than null), must be part of
//        /// the underlying collection, otherwise an exception
//        /// is thrown. If after setting, the item gets removed
//        /// from the underlying collection, the selected item is set 
//        /// to null.
//        /// </para>
//        /// <para>The reference to the selected item is not serialized
//        /// with the XML serializer.</para></remarks>
//        /// <value>The selected item.</value>
//        [XmlIgnore()]
//        public T SelectedItem
//        {
//            get
//            {
//                return _SelectedItem;
//            }
//            set
//            {
//                if (_SelectedItem == value)
//                    return;

//                //check existence of selected item
//                if (
//                    (value != null) //null is always allowed as value, as it represents no selection
//                    && (!this.Contains(value))
//                    )
//                {
//                    throw new ArgumentOutOfRangeException("The specified element to consider as selected is not part of the collection");
//                }

//                _SelectedItem = value;

//                OnSelectedChanged();
//                OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("SelectedItem"));
//            }
//        }

//        /// <summary>
//        /// Called when [selected changed].
//        /// </summary>
//        private void OnSelectedChanged()
//        {
//            if (SelectedChanged != null) //Anyone listening?
//            {
//                SelectedChanged(this, new EventArgs());
//            }
//        }

//        /// <summary>
//        /// Occurs when [selected changed].
//        /// </summary>
//        /// <remarks>This is provided for convenience, additional
//        /// to the PropertyChanged Event.</remarks>
//        /// <devdoc>Do not serialize the listeners to this event.</devdoc>
//        [field: NonSerialized]
//        public event EventHandler SelectedChanged;

//        //
//        // Summary:
//        //     Raises the System.Collections.ObjectModel.ObservableCollection<T>.CollectionChanged
//        //     event with the provided arguments.
//        //
//        // Parameters:
//        //   e:
//        //     Arguments of the event being raised.
//        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
//        {
//            base.OnCollectionChanged(e); //propagate

//            //but now check, whether the selected item is still in the collection
//            if (!this.Contains(SelectedItem))
//            {
//                SelectedItem = null; //unselect, if not part of the collection anymore
//            }
//        }

//        /// <summary>
//        /// Replaces the specified old item with a new one.
//        /// </summary>
//        /// <param name="oldItem"></param>
//        /// <param name="newItem"></param>
//        public virtual void Replace(T oldItem, T newItem)
//        {
//            var replacementItemIndex = this.IndexOf(oldItem);
//            this.RemoveAt(replacementItemIndex);
//            this.InsertItem(replacementItemIndex, newItem);
//        }

//        /// <summary>
//        /// Moves the specified item by the given amount of steps.
//        /// </summary>
//        /// <remarks>Limitation is automatically applied to avoid out of bounds contidions.</remarks>
//        /// <param name="item"></param>
//        /// <param name="steps"></param>
//        public void Move(T item, int steps)
//        {
//            if (item == null)
//            { return; }
//            int itemIndex = this.IndexOf(item);
//            int newItemIndex = itemIndex + steps; //calculate new required index

//            //limit the move to avoid out of bounds problem
//            newItemIndex = Math.Max(0, newItemIndex);
//            newItemIndex = Math.Min(this.Count - 1, newItemIndex);

//            if (newItemIndex != itemIndex) //real change?
//            {
//                //execute move
//                this.Move(itemIndex, newItemIndex);
//            }
//        }
//    }
//}

