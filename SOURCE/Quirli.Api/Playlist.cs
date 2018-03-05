using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Quirli.Api {
    /// <summary>
    ///     A set of tracks which are related to each other.
    /// </summary>
    public class Playlist {
        /// <summary>
        ///     The tracks in the playlist.
        /// </summary>
        private readonly List<Track> _tracks = new List<Track>();

        /// <summary>
        ///     The tracks in this playlist.
        /// </summary>
        public List<Track> Tracks {
            get { return _tracks; }
        }

        /// <summary>
        ///     Gets the JSONP representation of this playlist
        /// </summary>
        /// <remarks>This JSONP instance is suitable for use the nonobtrusive JSONP handler.</remarks>
        /// <returns>The current content of this instance is returned in JSONP, with the method name "callback_json1".</returns>
        public String Jsonp {
            get { return CreateJsonp(); }
        }

        /// <summary>
        ///     The title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Parses the given JSON string and stores it' information in this instance, wiping
        ///     all existing content.
        /// </summary>
        /// <param name="json"></param>
        private void ParseJson(String json) {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Serializes this instance into JSONP.
        /// </summary>
        /// <remarks>This JSONP instance is suitable for use the nonobtrusive JSONP handler.</remarks>
        /// <returns>The current content of this instance is returned in JSONP, with the method name "callback_json1".</returns>
        private String CreateJsonp() {
            String preamble =
                @"callback_json1(" + Environment.NewLine +
                @"{" + Environment.NewLine +
                @"""title"": """ + Title + @"""," + Environment.NewLine +
                @"""tracks"": [" + Environment.NewLine;

            String body = String.Empty;
            var trackObjects = new List<String>();
            foreach (Track track in Tracks.Take(38)) {
                //escape for JSON
                string escapedTitle = track.Title.Replace(@"""", @"\""");
                string escapedTrackUrl = track.TrackUrl.Replace(@"""", @"\""");

                trackObjects.Add(@"{""title"":""" + escapedTitle + @""",""url"":""" + escapedTrackUrl + @"""}");
            }
            body = String.Join(",", trackObjects.ToArray());
            String postamble =
                @"]" + Environment.NewLine +
                @"}" + Environment.NewLine +
                @");";
            return preamble + body + postamble;
        }


        /// <summary>
        ///     Stores the playlist in a JSON file named playlistcontent.jsonp
        /// </summary>
        public void Store() {
            string playerDirectory = Path.GetDirectoryName(Player.Url.LocalPath);
            Directory.CreateDirectory(playerDirectory);
            using (
                var fs = new FileStream(playerDirectory + @"\" + "playlistcontent" + ".jsonp",
                                        FileMode.Create)) {
                using (var w = new StreamWriter(fs, Encoding.UTF8)) {
                    w.Write(Jsonp);
                }
            }
        }
    }
}