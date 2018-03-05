using System;
using System.Collections.ObjectModel;

namespace Replayer.Core.v04.Annotation
{
    /// <summary>
    ///     A rehearsal Compilation, consisting of a set of tracks with their cuepoints.
    /// </summary>
    public interface ICompilation
    {
        /// <summary>
        ///     Gets or sets the media path. This is where the media files are
        ///     retrieved from.
        /// </summary>
        /// <value>The media path.</value>
        string MediaPath { get; set; }

        /// <summary>
        ///     Gets or sets the title for this Compilation.
        /// </summary>
        /// <value>The title.</value>
        string Title { get; set; }

        /// <summary>
        ///     Gets or sets the URL, where this Compilation is stored.
        ///     This is used for storage and retrieval of the compilation.
        /// </summary>
        /// <value>The URL.</value>
        string Url { get; set; }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        Guid Id { get; set; }

        /// <summary>
        ///     Gets or sets the tracks.
        /// </summary>
        /// <value>The tracks.</value>
        /// <devdoc>For XML serialization, this is not defined as interfaced set.</devdoc>
        ObservableCollection<Track> Tracks { get; set; }

        /// <summary>
        ///     Gets whether the collection is dirty and needs to be persisted to keep changes permanently.
        /// </summary>
        bool IsDirty { get; set; }

        /// <summary>
        ///     Finds the full url for the (probably partially specified url)
        ///     for the media file url, using the media path.
        /// </summary>
        /// <returns></returns>
        string Find(Track track);

        /// <summary>
        ///     Stores this instance to the same place it was loaded from.
        /// </summary>
        void Store();

        /// <summary>
        ///     Stores this instance to the specified url.
        /// </summary>
        void Store(string fileName);
    }
}