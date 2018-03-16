using System;
using System.Collections.Generic;
using System.Diagnostics;
using Replayer.Model;

namespace Replayer.WinForms.Ui.Views.Cues {
    /// <summary>
    /// A displayable object for a cue.
    /// </summary>
    /// <seealso cref="System.IEquatable{Replayer.WinForms.Ui.Views.Cues.DisplayCue}" />
    [DebuggerDisplay("\\{ Description = {Description}, Item = {Item} \\}")]
    public sealed class DisplayCue : IEquatable<DisplayCue> {
        private readonly string _Description;
        private readonly Cue _Item;

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description {
            get { return _Description; }
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <value>
        /// The item.
        /// </value>
        public Cue Item {
            get { return _Item; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayCue"/> class.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <param name="item">The item.</param>
        public DisplayCue(string description, Cue item) {
            _Description = description;
            _Item = item;
        }

        /// <summary>
        /// Equalses the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public bool Equals(DisplayCue obj) {
            if (obj == null) {
                return false;
            }
            if (!EqualityComparer<string>.Default.Equals(_Description, obj._Description)) {
                return false;
            }
            if (!EqualityComparer<Cue>.Default.Equals(_Item, obj._Item)) {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj) {
            if (obj is DisplayCue) {
                return Equals((DisplayCue)obj);
            }
            return false;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode() {
            int hash = 0;
            hash ^= EqualityComparer<string>.Default.GetHashCode(_Description);
            hash ^= EqualityComparer<Cue>.Default.GetHashCode(_Item);
            return hash;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() {
            return Description;
        }
    }
}