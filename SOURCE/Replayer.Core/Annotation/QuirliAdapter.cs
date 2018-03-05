using System;

namespace Replayer.Core.v04.Annotation
{
    /// <summary>
    ///     Bridge between the Quirli API and the Replayer objects.
    /// </summary>
    internal class QuirliAdapter
    {
        /// <summary>
        ///     Creates a Quirli track from the Replayer track.
        /// </summary>
        /// <param name="track">The track to  adapt.</param>
        /// <param name="absoluteUrl">The absolute URL to the media file for this track. The track itself may only contain a relative URL.</param>
        /// <returns>A Quirli Track</returns>
        internal static Quirli.Api.Track CreateFrom(Track track, String absoluteUrl)
        {
            var quirliTrack = new Quirli.Api.Track();
            quirliTrack.Album = track.Album;
            quirliTrack.Artist = track.Artist;
            quirliTrack.Title = track.Name;
            quirliTrack.MediaUrl = new Uri(absoluteUrl);
            foreach (Cue cue in track.Cues)
            {
                quirliTrack.Cues.Add(CreateFrom(cue));
            }
            return quirliTrack;
        }

        /// <summary>
        ///     Creates q Quirli cue from the replayer cue.
        /// </summary>
        /// <param name="cue"></param>
        /// <returns></returns>
        internal static Quirli.Api.Cue CreateFrom(Cue cue)
        {
            return new Quirli.Api.Cue(cue.Time, cue.Description);
        }
    }
}