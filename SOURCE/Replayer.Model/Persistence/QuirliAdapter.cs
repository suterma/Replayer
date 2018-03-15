using System;

namespace Replayer.Model.Persistence {
    /// <summary>
    ///     Bridge between the Quirli API and the Replayer objects.
    /// </summary>
    public class QuirliAdapter {
        /// <summary>
        ///     Creates a Quirli track from the Replayer track.
        /// </summary>
        /// <param name="track">The track to  adapt.</param>
        /// <param name="uri">The URI.</param>
        /// <returns>
        ///     A Quirli Track
        /// </returns>
        public static Quirli.Api.Track CreateFrom(Track track, Uri uri = null) {
            var quirliTrack = new Quirli.Api.Track();
            quirliTrack.Album = track.Album;
            quirliTrack.Artist = track.Artist;
            quirliTrack.Title = track.Name;
            if (uri == null) {
                quirliTrack.MediaUrl = new Uri(track.Url);
            }
            else {
                quirliTrack.MediaUrl = uri;
            }
            foreach (Cue cue in track.Cues) {
                quirliTrack.Cues.Add(CreateFrom(cue));
            }
            return quirliTrack;
        }

        /// <summary>
        ///     Creates a Quirli cue from the replayer cue.
        /// </summary>
        /// <param name="cue"></param>
        /// <returns></returns>
        public static Quirli.Api.Cue CreateFrom(Cue cue) {
            return new Quirli.Api.Cue(cue.Time, cue.Description);
        }
    }
}