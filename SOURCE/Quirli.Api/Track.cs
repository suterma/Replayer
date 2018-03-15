using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Quirli.Api {
    /// <summary>
    ///     Manipulates quirli tracks.
    /// </summary>
    public class Track {
        /// <summary>
        ///     The cues within this track.
        /// </summary>
        private readonly List<Cue> _cues = new List<Cue>();

        /// <summary>
        ///     The URL to the media.
        /// </summary>
        /// <remarks>
        ///     This URL is relative to the deployed/used player and thus is also allowed to be
        ///     relative, if applicable.
        /// </remarks>
        public Uri MediaUrl { get; set; }

        /// <summary>
        ///     The title of the track.
        /// </summary>
        public String Title { get; set; }

        /// <summary>
        ///     The artist of the track.
        /// </summary>
        public String Artist { get; set; }

        /// <summary>
        ///     The name of the Album or Store this Track is found on.
        /// </summary>
        public String Album { get; set; }

        /// <summary>
        ///     Gets the set of cues for this track.
        /// </summary>
        public List<Cue> Cues {
            get { return _cues; }
        }

        /// <summary>
        ///     Gets the track as URL.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Example:
        ///         <code>
        /// http://quir.li/player.html?media=http%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3D0VqTwnAuHws&title=What%20makes%20you%20beautiful&artist=The%20piano%20guys%20covering%20One%20Republic&album=Youtube&6.49=Intro&30.12=Knocking%20part&46.02=Real%20playing&51.5=Piano%20forte&93.32=Stringified&123.35=Vocals&139.38=Key%20cover%20jam&150.16=Good%20morning%20sky&173.96=Final%20chord
        /// </code>
        ///     </para>
        /// </remarks>
        /// <returns>The content of this instance is serialized into a track url.</returns>
        public String TrackUrl {
            get {
                var trackUrl = new StringBuilder();
                trackUrl.Append(Player.Url + "?");

                var parameters = new List<String>();
                if (MediaUrl != null) {
                    string uri = MediaUrl.IsAbsoluteUri ? MediaUrl.AbsoluteUri : MediaUrl.OriginalString;
                    parameters.Add(String.Format("media={0}", HttpUtility.UrlEncode(uri)));
                }
                parameters.Add(String.Format("title={0}", HttpUtility.UrlPathEncode(Title)));
                parameters.Add(String.Format("artist={0}", HttpUtility.UrlPathEncode(Artist)));
                parameters.Add(String.Format("album={0}", HttpUtility.UrlPathEncode(Album)));

                foreach (Cue cue in Cues) {
                    parameters.Add(String.Format("{0}={1}", cue.Position, HttpUtility.UrlPathEncode(cue.Text)));
                }

                trackUrl.Append(String.Join("&", parameters.ToArray()));
                return trackUrl.ToString();
            }
        }

        /// <summary>
        ///     Creates an new Track instance from the given track URL.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Example:
        ///         <code>
        /// http://quir.li/player.html?media=http%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3D0VqTwnAuHws&title=What%20makes%20you%20beautiful&artist=The%20piano%20guys%20covering%20One%20Republic&album=Youtube&6.49=Intro&30.12=Knocking%20part&46.02=Real%20playing&51.5=Piano%20forte&93.32=Stringified&123.35=Vocals&139.38=Key%20cover%20jam&150.16=Good%20morning%20sky&173.96=Final%20chord
        /// </code>
        ///     </para>
        /// </remarks>
        /// <param name="trackUrl">The track URL to create this instance from.</param>
        public Track(string trackUrl) {
            Parse(trackUrl);
        }

        /// <summary>
        ///     Creates an empty new Track instance.
        /// </summary>
        public Track() { }

        /// <summary>
        ///     Parses the given track URL and initializes this instance with it's data.
        /// </summary>
        /// <param name="trackUrl">The track URL to create this instance from.</param>
        private void Parse(string trackUrl) {
            var url = new Uri(trackUrl);
            string query = url.Query.Substring(1); //omit the question mark

            string[] items = query.Split('&');
            Dictionary<string, string> parameters = items.ToDictionary(item => item.Split('=').First(),
                                                                       item => item.Split('=').Last());

            //now go thru the parameters
            foreach (var parameter in parameters) {
                string decodedKey = HttpUtility.UrlDecode(parameter.Key);
                string decodedValue = HttpUtility.UrlDecode(parameter.Value);

                if (decodedKey == "media") {
                    MediaUrl = new Uri(decodedValue);
                }
                else if (decodedKey == "title") {
                    Title = decodedValue;
                }
                else if (decodedKey == "artist") {
                    Artist = decodedValue;
                }
                else if (decodedKey == "album") {
                    Album = decodedValue;
                }
                else {
                    double position;
                    if (Double.TryParse(decodedKey, out position)) {
                        Cues.Add(new Cue(position, decodedValue));
                    }
                }
            }
        }
    }
}