using System;
using System.ComponentModel;

namespace Replayer.Core.v04.Annotation
{
    /// <summary>
    ///     A cue in a track.
    /// </summary>
    public class Cue
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Cue" /> class.
        /// </summary>
        /// <devdoc>
        ///     Define an id explicitly here to be able to distinguish
        ///     between cues, even if their other properties are the same. This
        ///     could be well the case, because multiple tracks could possibly
        ///     have similar cue points, and their only distinctiveness is the
        ///     belonging to different tracks.
        /// </devdoc>
        public Cue()
        {
            Id = Guid.NewGuid();
            Description = String.Empty;
            Shortcut = String.Empty;
        }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        [Browsable(false)]
        public Guid Id { get; set; }

        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public String Description { get; set; }

        /// <summary>
        ///     Gets or sets the shortcut.
        /// </summary>
        /// <value>The shortcut.</value>
        public String Shortcut { get; set; }

        /// <summary>
        ///     Gets or sets the time.
        /// </summary>
        /// <value>The time.</value>
        public Double Time { get; set; }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        ///     A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            int idHash = default(int);
            if (Id != null)
            {
                idHash = Id.GetHashCode();
            }
            return
                base.GetHashCode() +
                Description.GetHashCode() +
                Shortcut.GetHashCode() +
                Time.GetHashCode() +
                idHash; //TODO fix this code. We have a designer prob if the other lines are used
        }

        /// <summary>
        ///     Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">
        ///     The <see cref="System.Object" /> to compare with this instance.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="T:System.NullReferenceException">
        ///     The <paramref name="obj" /> parameter is null.
        /// </exception>
        public override bool Equals(Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast return false.
            var p = obj as Cue;
            if (p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return Equals(p);
        }

        /// <summary>
        ///     Determines whether the specified object is equal to this instance.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <returns></returns>
        public bool Equals(Cue p)
        {
            // If parameter is null return false:
            if (p == null)
            {
                return false;
            }

            return
                (Description.Equals(p.Description)) &&
                (Shortcut.Equals(p.Shortcut)) &&
                (Time.Equals(p.Time)) &&
                (Id.Equals(p.Id));
        }

        /// <summary>
        ///     Creates a fully identical clone (except the Id property, which gets a new Id) of this instance.
        /// </summary>
        /// <returns></returns>
        public Cue Clone()
        {
            var clone = new Cue();
            clone.Description = Description;
            clone.Id = Guid.NewGuid();
            clone.Shortcut = Shortcut;
            clone.Time = Time;
            return clone;
        }
    }
}