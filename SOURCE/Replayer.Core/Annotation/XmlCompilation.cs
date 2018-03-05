using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Quirli.Api;
using Replayer.Core.v04.Media;
using Microsoft.VisualBasic;

namespace Replayer.Core.v04.Annotation
{
    /// <summary>
    ///     A compilation, that is stored in an xml file,
    ///     without enclosed media files,
    ///     consisting of a set of tracks with their cuepoints.
    /// </summary>
    [Serializable]
    public class XmlCompilation : CompilationBase
    {
        /// <summary>
        ///     Backing field for the URL property.
        /// </summary>
        private string _url;

        /// <summary>
        ///     Initializes a new instance of the <see cref="XmlCompilation" /> class.
        /// </summary>
        public XmlCompilation()
        {
            //create initial list
            Tracks = new ObservableCollection<Track>();
            Id = Guid.NewGuid();
        }

        /// <summary>
        ///     Gets or sets the URL, where this Compilation is stored. This is used for storage and retrieval.
        /// </summary>
        /// <value>The URL.</value>
        [Browsable(false)]
        public override string Url
        {
            get { return _url; }
            set
            {
                _url = Path.ChangeExtension(value, DefaultExtension); //make sure we have the right extension
                IsDirty = true;
            }
        }

        /// <summary>
        ///     Gets the default extension for xml Compilations.
        /// </summary>
        /// <value>The default extension.</value>
        public static string DefaultExtension
        {
            get { return ".rex"; }
        }

        /// <summary>
        ///     Finds the full url for the (probably partially specified url)
        ///     for the media file url, using the media path.
        /// </summary>
        /// <param name="track"></param>
        /// <returns></returns>
        public override String Find(Track track)
        {
            var places = new List<string> {MediaPath};
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
        public override void Store()
        {
            Store(Url);
        }

        /// <summary>
        ///     Stores this instance to the specified url.
        /// </summary>
        public override void Store(String url)
        {
            Url = url; //use this from now on.
            //store back to file
            var CompilationSerializer = new XmlSerializer(typeof (XmlCompilation));
            using (TextWriter writeFileStream = new StreamWriter(Url))
            {
                CompilationSerializer.Serialize(writeFileStream, this);
            }

            //TODO experimental: Write quirli player too

            //Deploy to certain local path
            Quirli.Api.Player.Url = new Uri(String.Format("file:///C:/Users/marcel/Music/{0}/player.html", Path.GetFileNameWithoutExtension(Url)));
            String playerBasePath = Path.GetDirectoryName(Quirli.Api.Player.Url.LocalPath);
            String mediafilesSubdirectory = @"\media\";
            Directory.CreateDirectory(playerBasePath + mediafilesSubdirectory);
            Quirli.Api.Player.Deploy();

            var playlist = new Playlist();
            playlist.Title = this.Title;
                    foreach (Track track in Tracks)
                    {
                        Quirli.Api.Track quirliTrack = QuirliAdapter.CreateFrom(track, Find(track));

                        //copy and adapt media file
                        String mediaFileName = Path.GetFileName(track.Url);
                        String mediaFileDestination = playerBasePath + mediafilesSubdirectory + mediaFileName;
                        FileSystem.FileCopy(track.Url, mediaFileDestination);
                        //quirliTrack.MediaUrl = (new Uri(mediaFileDestination)).MakeRelativeUri(new Uri(playerBasePath));
                        quirliTrack.MediaUrl = (new Uri(playerBasePath + @"\")).MakeRelativeUri(new Uri(mediaFileDestination));

                        //w.WriteLine(String.Format("<a href={0}>{1}</a><br>", quirliTrack.TrackUrl, quirliTrack.Title));
                        playlist.Tracks.Add(quirliTrack);
                    }

                    //Deploy to certain local path
                    playlist.Store();

            IsDirty = false;
        }

        /// <summary>
        ///     Retrieves a stored Compilation and assign it's values to this this instance.
        /// </summary>
        public static XmlCompilation Retrieve(String fileName)
        {
            var CompilationSerializer = new XmlSerializer(typeof (XmlCompilation));
            using (var readFileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var Compilation = (XmlCompilation) CompilationSerializer.Deserialize(readFileStream);
                Compilation.Url = readFileStream.Name;
                    //keep the filename of the storage used (use the path from the filestream, because it is absolute; the current directory was applied if no path was available beforehand)
                Compilation.IsDirty = false; //explicitly set false, since we just have loaded the data
                return Compilation;
            }
        }
    }
}