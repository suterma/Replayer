using System;
using System.Collections.Generic;
using System.IO;

namespace Replayer.Core.v04.Media
{
    /// <summary>
    ///     Helper methods for finding media files for tracks.
    /// </summary>
    public static class TrackFinder
    {
        /// <summary>
        ///     Finds the corresponding matching track using the given path.
        ///     If not found directly with the path, it browses the
        ///     given places until it finds a match.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="places">The places.</param>
        /// <returns></returns>
        public static string Find(string path, IEnumerable<String> places)
        {
            //try using the provided path
            if (path != null && //avoid null reference exception
                File.Exists(Path.GetFullPath(path))) //existing?
            {
                return Path.GetFullPath(path);
            }
            else
            {
                //search the places for an exact match
                foreach (string place in places)
                {
                    try
                    {
                        //create a full path with the filename and the place
                        String fileName = Path.Combine(Path.GetDirectoryName(place), Path.GetFileName(path));
                        if (File.Exists(fileName))
                        {
                            return fileName;
                        }
                    }
                    catch (ArgumentException)
                    {
                        continue; //simply with the next place
                    }
                    catch (PathTooLongException)
                    {
                        continue; //simply with the next place
                    }
                }
            }
            return String.Empty; //no match found
        }
    }
}