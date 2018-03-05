using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using RePlayer.Core.Data;
using RePlayer.Core.Media;
using System.Collections.ObjectModel;
using RePlayer.Core.v03.Annotation;

namespace RePlayer.Compilation.Html
{
    /// <summary>
    /// A compilation that is usable via a single html document.
    /// </summary>
    public class HtmlCompilation : ICompilation
    {
        public HtmlCompilation()
        {
            //create initial list
            Tracks = new ObservableSelectableCollection<Track>();
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Occurs when a media file for the track is missing by the find method.
        /// </summary>
        /// <devdoc>Do not serialize the listeners to this event.</devdoc>
         public event EventHandler<EventArgs<string>> FileMissing;


         /// <summary>
         /// Finds the full url for the (probably partially specified url)
         /// for the media file url, using the media path and
         /// user input if necessary.
         /// </summary>
         /// <param name="track"></param>
         /// <returns></returns>
        public string Find(Track track)
        {
            List<string> places = new List<string> { MediaPath };
            string path = TrackFinder.Find(track.Url, places);

            //before returning the path, check if found, otherwise request user input, if not found successfully
            if (!File.Exists(path))
            {
                //request user input                        
                path = OnFileMissing(track.Name);

                if (!File.Exists(path)) //still not found?
                {
                    throw new FileNotFoundException(String.Format("Media file for track {0} not found", track.Name));
                }
            }

            //we now have the best guess about where that media file is, so save it here
            if (!track.Url.Equals(path)) //we have now found a location that is different to what we first knew?
            {
                //use this one from now on with this track.
                track.Url = path;
            }
            return path;
        }


        /// <summary>
        /// Called when [file missing].
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        private string OnFileMissing(string fileName)
        {
            EventArgs<string> args = new EventArgs<string>();
            if (FileMissing != null) //anyone listening?
            {
                args.Data = fileName;
                FileMissing(this, args); //try to get the path
            }
            return args.Data; //in any case return the path, empty or not
        }

        /// <summary>
        /// Gets or sets the media path. This is where the media files are
        /// retrieved from.
        /// </summary>
        /// <value>The media path.</value>
        public string MediaPath        {            get           ;            set          ;        }

        /// <summary>
        /// Stores this instance to the same place it was loaded from.
        /// </summary>
        public virtual void Store()
        {          
                //write html to file
                using (FileStream htmlFile = File.Open(this.Url, FileMode.Create, FileAccess.Write))
                {

                    using (StreamWriter writer = new StreamWriter(htmlFile))
                    {
                        writer.WriteLine("{0}", "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01//EN\" \"http://www.w3.org/TR/html4/strict.dtd\">");
                        writer.WriteLine("{0}", "<html>");
                        writer.WriteLine("{0}", "<head>");
                        writer.WriteLine("<title>{0}</title>", "RePlayer Compilation");
                      //  writer.WriteLine("{0}", "<link rel=\"stylesheet\" type=\"text/css\" href=\"style.css\">");
                        writer.WriteLine("{0}", "</head>");
                        writer.WriteLine("{0}", "<body>");

                            //create test quicktime player instance TODO test, remove
                            //writer.WriteLine("{0}",@"                            <object id=""musik2"" classid=""clsid:02BF25D5-8C17-4B23-BC80-D3488ABDDC6B"" codebase=""http://www.apple.com/qtactivex/qtplugin.cab#version=6,0,2,0"" height=""16"" width=""100%"">    <param name=""SRC"" value=""{0}"">    <param name=""autoplay"" value=""false"">    <param name=""controller"" value=""true"">    <param name=""type"" value=""video/quicktime"">    <param name=""pluginspage"" value=""http://www.apple.com/quicktime/download/index.html"">  <embed name=""musik2"" src=""Einzug.mp3"" type=""video/quicktime"" autoplay=""false"" controller=""true"" enablejavascript=""true"" pluginspage=""http://www.apple.com/quicktime/download/index.html"" height=""16"" width=""240"">    </object> ");

                        //for each track, write a title
                        foreach (Track item in Tracks)
                        //TrackAnnotation item = Tracks[1];
                        {
                            //copy the track's media file to the directory where this compilation lives
                            File.Copy(Find(item), String.Format(@"{0}\{1}",
                                                                 Path.GetDirectoryName(this.Url),
                                                                 Path.GetFileName(item.Url)), true);

                            writer.WriteLine("<h1>{0}</h1>", item.Name);

                            string objectId = HtmlCompilation.Encode(Guid.NewGuid());
                            var mediaUri = Uri.EscapeDataString(Path.GetFileName(item.Url));
                            ///Create quicktime player instance
                            writer.WriteLine(String.Format(@"<object id=""{1}"" classid=""clsid:02BF25D5-8C17-4B23-BC80-D3488ABDDC6B"" codebase=""http://www.apple.com/qtactivex/qtplugin.cab#version=6,0,2,0"" height=""16"" width=""100%"">    <param name=""SRC"" value=""{0}"">    <param name=""autoplay"" value=""false"">    <param name=""controller"" value=""true"">    <param name=""type"" value=""video/quicktime"">    <param name=""pluginspage"" value=""http://www.apple.com/quicktime/download/index.html"">  <embed name=""{1}"" src=""{0}"" type=""video/quicktime"" autoplay=""false"" controller=""true"" enablejavascript=""true"" pluginspage=""http://www.apple.com/quicktime/download/index.html"" height=""16"" width=""100%"">    </object>",
                                mediaUri,
                                objectId
                                ));


                            //create control links
                            //writer.WriteLine(String.Format(@"<a href=""javascript:document.{0}.Play();"">play</a> <a href=""javascript:document.{0}.Stop();"">stop</a>, <a href=""javascript:document.{0}.Rewind();"">rewind</a>, <a href=""javascript:document.{0}.SetVolume(100);"">soft</a> <a href=""javascript:document.{0}.SetVolume(200);"">loud</a> <a href=""javascript:document.{0}.SetTime(8 * document.{0}.GetTimeScale())"">goto after 10 seconds</a>. <a href=""javascript:alert(document.{0}.GetTime() / document.{0}.GetTimeScale());"">get position</a>",
                            //    objectId
                            //    ));

                            writer.WriteLine("<table>");
                            writer.WriteLine("  <tr>    <th>Time</th>    <th>Shortcut</th>    <th>Description</th>  </tr>");

                            foreach (var cue in item.Cues)
                            {
                                DateTime positionTime = new DateTime((long)cue.Time * 10000000); //convert using ticks

                                //create a linke to the position of this cue in the media file
                                String positionLink = String.Format(@"<a href=""javascript:document.{0}.SetTime({1} * document.{0}.GetTimeScale())"">{2}</a>",
                                objectId,
                                cue.Time,
                                positionTime.ToString("mm:ss")
                                );


                                writer.WriteLine(" <tr>    <td>{0}</td>    <td>{1}</td>    <td>{2}</td>  </tr>",
                                    positionLink,
                               cue.Shortcut, 
                               cue.Description);

                            }
                            writer.WriteLine("</table>");
                        }



                        writer.WriteLine("{0}", "</body>");
                        writer.WriteLine("{0}", "</html>");

                        writer.Flush();
                    }
                }

        }

        /// <summary>
        /// Stores this instance to the specified url.
        /// </summary>
        /// <param name="fileName"></param>
        public void Store(string fileName)
        {
            this.Url = fileName;
            Store();
        }

        /// <summary>
        /// Gets or sets the title for this Compilation.
        /// </summary>
        /// <value>The title.</value>
        public string Title        {            get           ;            set           ;        }

        /// <summary>
        /// Gets or sets the URL, where this Compilation is stored. This is used for storage and retrieval.
        /// </summary>
        /// <value>The URL.</value>
        public string Url        {            get          ;            set           ;        }

        /// <summary>
        /// Gets or sets the tracks.
        /// </summary>
        /// <value>The tracks.</value>
        public ObservableSelectableCollection<Track> Tracks { get; set; }

        /// <summary>
        /// Gets the default extension for the implemented type of Compilation.
        /// </summary>
        /// <value>The default extension.</value>
        public virtual string DefaultExtension
        {
            get { return ".html"; }
        }

        /// <summary>
        /// Encodes the specified GUID to a Java-Script compatible representation.
        /// </summary>
        /// <param name="guid">The GUID.</param>
        /// <returns></returns>
        protected static string Encode(Guid guid)
        {
            string enc = Convert.ToBase64String(guid.ToByteArray());
            enc = enc.Replace("/", "a"); //This replacement is used to make it javascript friendly. This removes roundtrippability and reduces uniqueness
            enc = enc.Replace("+", "b");
            //enc = enc.Replace("=", "c");
            enc = enc.Insert(0, "X"); //makes sure, the encoded GUID does not start with a number
            return enc.Substring(0, 23); //omit the trailing equal signs
        }



        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public Guid Id { get; set; }
    }
}
