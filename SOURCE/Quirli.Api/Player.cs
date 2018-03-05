using System;
using System.IO;
using System.Reflection;
using Microsoft.VisualBasic.FileIO;

namespace Quirli.Api {
    /// <summary>
    ///     Encapsulates the quirli player
    /// </summary>
    public class Player {
        /// <summary>
        ///     The Url of the player, this API should work on. All subsequent API calls refer to this player instance.
        /// </summary>
        /// <remarks>
        ///     This could be a location on the web (with the http protocol)
        ///     or a local directory (with the file protocol). Default is
        ///     <code>http://quir.li/player.html</code>
        /// </remarks>
        public static Uri Url { get; set; }

        /// <summary>
        ///     Initializes the player.
        /// </summary>
        static Player() {
            Url = new Uri("http://quir.li/player.html");
        }


        /// <summary>
        ///     Deploys the player to the directory from URL mentioned in the Url property.
        /// </summary>
        /// <remarks>This only works for local locations with the file protocol.</remarks>
        public static void Deploy() {
            String apiPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            String playerPath = apiPath + @"\player";
            String deployPath = Path.GetDirectoryName(Url.LocalPath);

            //copy the files from the api player directory to the Url            
            FileSystem.CopyDirectory(playerPath, deployPath, true);
        }
    }
}