using System;
using System.Collections.ObjectModel;

namespace Replayer.Core.Data {
    /// <summary>
    ///     Static extension methods for the ObservableCollection class.
    /// </summary>
    public static class ObservableCollectionExtensions {
        /// <summary>
        ///     Replaces the specified old item with a new one.
        /// </summary>
        /// <param name="oldItem"></param>
        /// <param name="newItem"></param>
        public static void Replace<T>(this ObservableCollection<T> collection, T oldItem, T newItem) {
            int replacementItemIndex = collection.IndexOf(oldItem);
            collection.RemoveAt(replacementItemIndex);
            collection.Insert(replacementItemIndex, newItem);
        }

        /// <summary>
        ///     Moves the specified item by the given amount of steps.
        /// </summary>
        /// <remarks>Limitation is automatically applied to avoid out of bounds contidions.</remarks>
        /// <param name="item"></param>
        /// <param name="steps"></param>
        public static void Move<T>(this ObservableCollection<T> collection, T item, int steps) {
            if (item == null) {
                return;
            } //no item specified?
            int itemIndex = collection.IndexOf(item);
            int newItemIndex = itemIndex + steps; //calculate new required index

            //limit the move to avoid out of bounds problem
            newItemIndex = Math.Max(0, newItemIndex);
            newItemIndex = Math.Min(collection.Count - 1, newItemIndex);

            if (newItemIndex != itemIndex) //real change?
            {
                //execute move
                collection.Move(itemIndex, newItemIndex);
            }
        }
    }
}