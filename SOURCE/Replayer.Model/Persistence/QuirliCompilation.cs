using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using DotLiquid;
using DotLiquid.NamingConventions;
using Quirli.Api;

namespace Replayer.Model.Persistence {
    /// <summary>
    ///     A compilation of type Quirli
    /// </summary>
    /// <remarks>
    ///     This is quite similar to an XML compilation, however, insead of XML a specifically crafted
    ///     sort of HTML document (The quirli playlist) is used as persistence. The media files are copied and
    ///     renamed to the location of the playlist.
    /// </remarks>
    public class QuirliCompilation : XmlCompilation, ICompilation {
        private string _url;

        /// <summary>
        ///     This compilation is of type Quirli.
        /// </summary>
        public override CompilationType Type {
            get { return CompilationType.Quirli; }
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
        public new static string DefaultExtension {
            get { return ".html"; }
        }

        private String GetPlaylistTemplate() {
            Assembly assembly = Assembly.GetExecutingAssembly();
            const string resourceName = "Replayer.Model.Persistence.quirliplaylist.inlinetemplate.html";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName)) {
                using (var reader = new StreamReader(stream)) {
                    string result = reader.ReadToEnd();
                    return result;
                }
            }
        }

        private void DeployPlayer() {
            Assembly assembly = Assembly.GetExecutingAssembly();
            const string resourceName = "Replayer.Model.Persistence.quirliplayer.inline.html";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName)) {
                using (var reader = new StreamReader(stream)) {
                    string player = reader.ReadToEnd();

                    //get the path to store the player
                    string playerDirectory = Path.GetDirectoryName(Url);
                    string playerFileName = playerDirectory + "/player.html";

                    using (TextWriter writeFileStream = new StreamWriter(playerFileName)) {
                        writeFileStream.Write(player);
                    }
                }
            }
        }


        /// <summary>
        ///     Stores this instance to the specified url.
        /// </summary>
        public override void Store(String url) {
            Url = url; //use this from now on.
            var quirliPlaylist = new List<Quirli.Api.Track>();
            string targetDirectory = Path.GetDirectoryName(url) + @"\";
            var urlRoot = new Uri(targetDirectory);

            //create a deployment information with all relevant stuff per track
            int index = 0;
            var mediaDeployment = (from track in Tracks
                                   let trackSource = Find(track)
                                   let i = ++index
                                   //let targetFileName = Path.GetFileName(trackSource)
                                   let targetFileName = Path.GetFileNameWithoutExtension(url) + String.Format("-{0:00}", i) + Path.GetExtension(trackSource) //alternative with prefix number instead of just name
                                   select new {
                                       Track = track,
                                       TrackSource = trackSource,
                                       TargetFileName = targetFileName,
                                       TrackTarget = targetDirectory + targetFileName
                                   }
                                  ).ToList(); //track media files

            //store the files along the url
            foreach (var mediaFile in mediaDeployment) {
                File.Copy(mediaFile.TrackSource, mediaFile.TrackTarget, true);
                Uri relativeMediaUrl = urlRoot.MakeRelativeUri(new Uri(mediaFile.TrackTarget));

                //Quirli.Api.Track quirliTrack = QuirliAdapter.CreateFrom(track, this.MediaPath + track.Url);
                Quirli.Api.Track quirliTrack = QuirliAdapter.CreateFrom(mediaFile.Track, relativeMediaUrl);
                quirliPlaylist.Add(quirliTrack);
            }

            //store the playlist into a template HTML file
            Uri relativePlayerUrl = urlRoot.MakeRelativeUri(new Uri(targetDirectory + "player.html"));
            Player.Url = relativePlayerUrl;
            string templateContent = GetPlaylistTemplate();
            Template.RegisterSafeType(typeof(Quirli.Api.Track), new[] { "Title", "Album", "Artist", "TrackUrl" });
            Template.NamingConvention = new CSharpNamingConvention();
            Template template = Template.Parse(templateContent); // Parses and compiles the template
            string htmlPlaylist = template.Render(Hash.FromAnonymousObject(new {
                CompilationTitle = Title,
                Tracks = quirliPlaylist
            }));
            using (TextWriter writeFileStream = new StreamWriter(Url)) {
                writeFileStream.Write(htmlPlaylist);
            }
            DeployPlayer();
            IsDirty = false;
        }

        /// <summary>
        ///     Retrieves the Compilation at the specified url.
        /// </summary>
        /// <remarks>This is not currently supported. An empty compilation will be returned.</remarks>
        public override ICompilation Retrieve(string url) {
            return new QuirliCompilation();
        }
    }
}