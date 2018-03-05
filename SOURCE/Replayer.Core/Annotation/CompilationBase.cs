using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Replayer.Core.v04.Annotation
{
    /// <summary>
    ///     A basic implementation for a compilation, not intended for instantiation
    /// </summary>
    public abstract class CompilationBase : ICompilation
    {
        /// <summary>
        ///     Backing field for the Id property.
        /// </summary>
        private Guid _id;

        /// <summary>
        ///     Backing field for the the IsDirty property.
        /// </summary>
        /// <devdoc>Assume as non-dirty at start, since it never was never changed yet.</devdoc>
        [field: NonSerialized] private bool _isDirty;

        /// <summary>
        ///     Backing field for the MediaPath property.
        /// </summary>
        private String _mediaPath = String.Empty;

        /// <summary>
        ///     Backing field for the Title property
        /// </summary>
        private String _title = "Untitled";

        /// <summary>
        ///     Backing field for the Tracks property.
        /// </summary>
        protected ObservableCollection<Track> _tracks;

        /// <summary>
        ///     Finds the full url for the (probably partially specified url)
        ///     for the media file url, using the media path.
        /// </summary>
        /// <param name="track"></param>
        /// <returns></returns>
        public abstract string Find(Track track);

        /// <summary>
        ///     Gets or sets the media path. This is where the media files are retrieved from.
        /// </summary>
        /// <value>The media path.</value>
        [Browsable(false)]
        public virtual String MediaPath
        {
            get { return _mediaPath; }
            set
            {
                if (MediaPath != value) //really changes?
                {
                    _mediaPath = value;
                    IsDirty = true;
                }
            }
        }

        public abstract void Store();

        public abstract void Store(string fileName);

        /// <summary>
        ///     Gets or sets the title for this Compilation.
        /// </summary>
        /// <value>The title.</value>
        [Description("The title of the compilation.")]
        public virtual String Title
        {
            get { return _title; }
            set
            {
                if (_title != value) //really changes?
                {
                    _title = value;
                    IsDirty = true;
                }
            }
        }

        /// <summary>
        ///     Gets or sets the URL, where this Compilation is stored. This is used for storage and retrieval.
        /// </summary>
        /// <value>The URL.</value>
        /// <devdoc>
        ///     As each implementation probably has it's own extension or naming scheme, this is
        ///     held abstract.
        /// </devdoc>
        [Browsable(false)]
        public abstract string Url { get; set; }


        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        [Browsable(false)]
        public virtual Guid Id
        {
            get { return _id; }
            set
            {
                if (!_id.Equals(value)) //really changes?
                {
                    _id = value;
                    IsDirty = true;
                }
            }
        }

        /// <summary>
        ///     Gets or sets the tracks.
        /// </summary>
        /// <value>The tracks.</value>
        public virtual ObservableCollection<Track> Tracks
        {
            get { return _tracks; }
            set
            {
                if (value != _tracks) //reference really changes?
                {
                    if (Tracks != null) //there were any?
                    {
                        Tracks.CollectionChanged -= Tracks_CollectionChanged; //unregister from old collection
                    }
                    _tracks = value; //change
                    IsDirty = true; //the new tracks collection value is not saved yet
                    if (Tracks != null) //there is actually one now?
                    {
                        Tracks.CollectionChanged += Tracks_CollectionChanged; //register on new collection
                    }
                }
            }
        }

        /// <summary>
        ///     Gets whether the collection is dirty and needs to be persisted to keep changes permanently.
        /// </summary>
        [XmlIgnore]
        public virtual bool IsDirty
        {
            get { return _isDirty; }
            set { _isDirty = value; }
        }

        /// <summary>
        ///     Honor tracks changes to the dirty property.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tracks_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            IsDirty = true;
        }
    }
}