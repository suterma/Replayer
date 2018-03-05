using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Replayer.Core.v04.Annotation;

namespace Replayer.Core.v04
{
    /// <summary>
    ///     A factory for Compilation handling.
    /// </summary>
    public static class CompilationFactory
    {
        /// <summary>
        ///     Converts the specified Compilation to the specified target type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Compilation">The Compilation.</param>
        /// <returns></returns>
        public static T Convert<T>(ICompilation source) where T : ICompilation, new()
        {
            var converted = new T();
            converted.Url = source.Url;
            converted.MediaPath = source.MediaPath;
            converted.Title = source.Title;
            converted.Tracks = source.Tracks;

            ///special conversion for ZIP to XML:
            ///Create local copies of the media files
            ///TODO later!
            //if ((source is ZipCompilation) && (converted is XmlCompilation))
            //{
            //foreach (var track in converted.Tracks)
            //{
            //    track
            //}
            //}
            return converted;
        }

        /// <summary>
        ///     Retrieves the Compilation at the specified url.
        /// </summary>
        public static ICompilation Retrieve(string url)
        {
            //load initial data
            if (Path.GetExtension(url).Equals(XmlCompilation.DefaultExtension)) //is xml Compilation?
            {
                return XmlCompilation.Retrieve(url);
            }
            else if (Path.GetExtension(url).Equals(ZipCompilation.DefaultExtension)) //is zipped Compilation?
            {
                return ZipCompilation.Retrieve(url);
            }
            else
            {
                //try deprecated format
                try
                {
                    Compilation.ICompilation deprecatedCompilation = Compilation.CompilationFactory.Retrieve(url);

                    //convert to xml compilation with local files
                    ICompilation convertedCompilation = new ZipCompilation();
                    convertedCompilation.MediaPath = deprecatedCompilation.MediaPath;
                    convertedCompilation.Title = deprecatedCompilation.Title;
                    //Do not convert the url, to force "saving as"
                    convertedCompilation.Tracks =
                        new ObservableCollection<Track>
                            (from track in deprecatedCompilation.Tracks
                             select new Track
                                        {
                                            Album = track.TrackInfo.Album,
                                            Artist = track.TrackInfo.Artist,
                                            //let have a new id automatically
                                            Measure = track.TrackInfo.Measure,
                                            Name = track.TrackInfo.Name,
                                            Url = track.TrackInfo.LocalPath,
                                            Cues = new ObservableCollection<Cue>
                                                (from cue in track.CuePoints
                                                 select new Cue
                                                            {
                                                                Description = cue.Description,
                                                                //let have a new id automatically
                                                                Shortcut = cue.Shortcut,
                                                                Time = cue.Time
                                                            })
                                        });

                    //TODO inform the user about the conversion
                    return convertedCompilation;
                }
                catch (Exception ex)
                    //do not allow to propagate anything directly, but throw our own exception as a wrapper to have a defined exception type on the outside
                {
                    throw new ArgumentException("The Compilation with path: " + url + " is not in a known format", ex);
                }
            }
        }

        /// <summary>
        ///     Creates a new compilation of default type, which is Xml.
        /// </summary>
        /// <returns></returns>
        public static ICompilation CreateNew()
        {
            return new XmlCompilation();
        }
    }
}