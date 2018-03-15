using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;

namespace Replayer.Model.Persistence {
    /// <summary>
    ///     A compilation, that is stored in an xml file,
    ///     without enclosed media files,
    ///     consisting of a set of tracks with their cuepoints.
    /// </summary>
    [Serializable]
    public class XmlCompilation : CompilationBase {
        /// <summary>
        ///     Backing field for the URL property.
        /// </summary>
        private string _url;

        /// <summary>
        ///     This compilation is of type XML.
        /// </summary>
        public override CompilationType Type {
            get { return CompilationType.Xml; }
        }

        /// <summary>
        ///     Gets or sets the URL, where this Compilation is stored. This is used for storage and retrieval.
        /// </summary>
        /// <value>The URL.</value>
        [Browsable(false)]
        public override string Url {
            get { return _url; }
            set {
                _url = Path.ChangeExtension(value, DefaultExtension); //make sure we have the right extension
                IsDirty = true;
            }
        }

        /// <summary>
        ///     Gets the default extension for xml Compilations.
        /// </summary>
        /// <value>The default extension.</value>
        public static string DefaultExtension {
            get { return ".rex"; }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="XmlCompilation" /> class.
        /// </summary>
        public XmlCompilation() {
            //create initial list
            Tracks = new ObservableCollection<Track>();
            Id = Guid.NewGuid();
        }

        /// <summary>
        ///     Finds the full url for the (probably partially specified url)
        ///     for the media file url, using the media path.
        /// </summary>
        /// <param name="track"></param>
        /// <returns></returns>
        public override String Find(Track track) {
            var places = new List<string> { MediaPath, Url }; //first use the specified media path, but as backup also the place where the compliation is stored
            string path = TrackFinder.Find(track.Url, places);

            //we now have the best guess about where that media file is, so save it here
            if (!track.Url.Equals(path)) //we have now found a location that is different to what we first knew?
            {
                //use this one from now on with this track.
                track.Url = path;
            }
            return path;
        }

        /// <summary>
        ///     Stores this instance to the same place it was loaded from.
        /// </summary>
        public override void Store() {
            Store(Url);
        }

        /// <summary>
        ///     Stores this instance to the specified url.
        /// </summary>
        public override void Store(String url) {
            Url = url; //use this from now on.
            //store back to file
            var CompilationSerializer = new XmlSerializer(typeof(XmlCompilation));
            using (TextWriter writeFileStream = new StreamWriter(Url)) {
                CompilationSerializer.Serialize(writeFileStream, this);
            }
            IsDirty = false;
        }

        /// <summary>
        ///     Retrieves the Compilation at the specified url.
        /// </summary>
        public override ICompilation Retrieve(String fileName) {
            var CompilationSerializer = new XmlSerializer(typeof(XmlCompilation));
            using (var readFileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read)) {
                var Compilation = (XmlCompilation)CompilationSerializer.Deserialize(readFileStream);
                Compilation.Url = readFileStream.Name;
                //keep the filename of the storage used (use the path from the filestream, because it is absolute; the current directory was applied if no path was available beforehand)
                Compilation.IsDirty = false; //explicitly set false, since we just have loaded the data
                return Compilation;
            }
        }
    }
}