using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using codeministry.TiddlyMaky;
using RePlayer.Core.v03.Annotation;

namespace RePlayer.Compilation.Html
{
    /// <summary>
    /// A HTML compilation that produces a TiddlyWiki HTML file.
    /// </summary>
    /// <devdoc>This does currently not work. With Firefox,
    /// the tiddlers are presented correctly, but the javascript part
    /// for setting the position (using links with javascript action) does not work.
    /// </devdoc>
    public class TiddlyWikiCompilation: HtmlCompilation
    {
        /// <summary>
        /// Stores this instance to the same place it was loaded from.
        /// </summary>
        public override void Store()
        {
            TiddlyWiki wiki = new TiddlyWiki(this.Url);//prepare a new Wiki for writing

            foreach (Track item in Tracks)
            //TrackAnnotation item = Tracks[1];
            {
                //copy the track's media file to the directory where this compilation lives
                File.Copy(Find(item), String.Format(@"{0}\{1}", Path.GetDirectoryName(this.Url), Path.GetFileName(item.Url)), true);

                //write raw html, representing the annotated track, to a string, using a memory stream and a writer
                string rawHtml = GetRawHtmlRepresentation(item);

                //with the raw html, create and add a tiddler
                        Tiddler tiddler = new Tiddler
                        {
                            UnescapedContent = rawHtml,
                            Title = item.Name
                        };

                        wiki.Add(tiddler);

                    
                
            }
            wiki.Save();
        }

        /// <summary>
        /// Gets the raw HTML representation for the specified track.
        /// </summary>
        /// <param name="track">The track.</param>
        /// <returns></returns>
        private static string GetRawHtmlRepresentation(Track track)
        {
            using (MemoryStream memStream = new MemoryStream())
            using (StreamWriter writer = new StreamWriter(memStream))
            {
                writer.WriteLine("{0}", "<html>");

                writer.WriteLine("<h1>{0}</h1>", track.Name); //write a title
                string objectId = HtmlCompilation.Encode(Guid.NewGuid()); //get a object id for referencing at the javascript links
                string mediaUri = Uri.EscapeDataString(Path.GetFileName(track.Url)); //get the uri to the media file. This is a relative URI, consisting only of the file name
                writer.WriteLine(String.Format(@"<object id=""{1}"" classid=""clsid:02BF25D5-8C17-4B23-BC80-D3488ABDDC6B"" codebase=""http://www.apple.com/qtactivex/qtplugin.cab#version=6,0,2,0"" height=""16"" width=""100%"">    <param name=""SRC"" value=""{0}"">    <param name=""autoplay"" value=""false"">    <param name=""controller"" value=""true"">    <param name=""type"" value=""video/quicktime"">    <param name=""pluginspage"" value=""http://www.apple.com/quicktime/download/index.html"">  <embed name=""{1}"" src=""{0}"" type=""video/quicktime"" autoplay=""false"" controller=""true"" enablejavascript=""true"" pluginspage=""http://www.apple.com/quicktime/download/index.html"" height=""16"" width=""100%"">    </object>", mediaUri, objectId)); //Create quicktime player instance, to the media uri and the specified object id

                //create control links
                writer.WriteLine(String.Format(@"<a href=""javascript:document.{0}.Play();"">play</a> <a href=""javascript:document.{0}.Stop();"">stop</a>, <a href=""javascript:document.{0}.Rewind();"">rewind</a>, <a href=""javascript:document.{0}.SetVolume(100);"">soft</a> <a href=""javascript:document.{0}.SetVolume(200);"">loud</a> <a href=""javascript:document.{0}.SetTime(8 * document.{0}.GetTimeScale())"">goto after 10 seconds</a>. <a href=""javascript:alert(document.{0}.GetTime() / document.{0}.GetTimeScale());"">get position</a>",
                    objectId
                    ));

                writer.WriteLine("<table>");
                writer.WriteLine("  <tr>    <th>Time</th>    <th>Shortcut</th>    <th>Description</th>  </tr>");
                foreach (var cue in track.Cues)
                {
                    DateTime positionTime = new DateTime((long)cue.Time * 10000000);
                    //convert using ticks
                    //create a linke to the position of this cue in the media file
                    String positionLink = String.Format(@"<a href=""javascript:document.{0}.SetTime({1} * document.{0}.GetTimeScale())"">{2}</a>", objectId, cue.Time, positionTime.ToString("mm:ss"));
                    writer.WriteLine(" <tr>    <td>{0}</td>    <td>{1}</td>    <td>{2}</td>  </tr>", positionLink, cue.Shortcut, cue.Description);
                }
                writer.WriteLine("</table>");

                writer.WriteLine("{0}", "</html>");
                writer.Flush();

                memStream.Flush();
                memStream.Position = 0; //go to start


                //get the raw html content for this track's representation
                using (StreamReader reader = new StreamReader(memStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        //        {
        //    using (MemoryStream memStream = new MemoryStream())
        //    {
        //        //write html to a string
        //        using (StreamWriter writer = new StreamWriter(memStream))
        //        {
        //            writer.WriteLine("{0}", "<html>");
        //            //for each track, write a title
        //            foreach (TrackAnnotation item in Tracks)
        //            {
        //                //copy the track's media file to the directory where this compilation lives
        //                File.Copy(Find(item.TrackInfo), String.Format(@"{0}\{1}", Path.GetDirectoryName(this.Url), Path.GetFileName(item.TrackInfo.LocalPath)), true);
        //                writer.WriteLine("<h1>{0}</h1>", item.TrackInfo.Name);
        //                string objectId = GuidEncoder.Encode(Guid.NewGuid());
        //                var mediaUri = Uri.EscapeDataString(Path.GetFileName(item.TrackInfo.LocalPath));
        //                ///Create quicktime player instance
        //                writer.WriteLine(String.Format(@"<object id=""{1}"" classid=""clsid:02BF25D5-8C17-4B23-BC80-D3488ABDDC6B"" codebase=""http://www.apple.com/qtactivex/qtplugin.cab#version=6,0,2,0"" height=""16"" width=""100%"">    <param name=""SRC"" value=""{0}"">    <param name=""autoplay"" value=""false"">    <param name=""controller"" value=""true"">    <param name=""type"" value=""video/quicktime"">    <param name=""pluginspage"" value=""http://www.apple.com/quicktime/download/index.html"">  <embed name=""{1}"" src=""{0}"" type=""video/quicktime"" autoplay=""false"" controller=""true"" enablejavascript=""true"" pluginspage=""http://www.apple.com/quicktime/download/index.html"" height=""16"" width=""100%"">    </object>", mediaUri, objectId));
        //                //create control links
        //                writer.WriteLine(String.Format(@"<a href=""javascript:document.{0}.Play();"">play</a> <a href=""javascript:document.{0}.Stop();"">stop</a>, <a href=""javascript:document.{0}.Rewind();"">rewind</a>, <a href=""javascript:document.{0}.SetVolume(100);"">soft</a> <a href=""javascript:document.{0}.SetVolume(200);"">loud</a> <a href=""javascript:document.{0}.SetTime(8 * document.{0}.GetTimeScale())"">goto after 10 seconds</a>. <a href=""javascript:alert(document.{0}.GetTime() / document.{0}.GetTimeScale());"">get position</a>",
        //                    objectId
        //                    ));
        //                writer.WriteLine("<table>");
        //                writer.WriteLine("  <tr>    <th>Time</th>    <th>Shortcut</th>    <th>Description</th>  </tr>");
        //                foreach (var cue in item.CuePoints)
        //                {
        //                    DateTime positionTime = new DateTime((long)cue.Time * 10000000);
        //                    //convert using ticks
        //                    //create a linke to the position of this cue in the media file
        //                    String positionLink = String.Format(@"<a href=""javascript:document.{0}.SetTime({1} * document.{0}.GetTimeScale())"">{2}</a>", objectId, cue.Time, positionTime.ToString("mm:ss"));
        //                    writer.WriteLine(" <tr>    <td>{0}</td>    <td>{1}</td>    <td>{2}</td>  </tr>", positionLink, cue.Shortcut, cue.Description);
        //                }
        //                writer.WriteLine("</table>");
        //            }
        //            writer.WriteLine("{0}", "</html>");
        //            writer.Flush();
                
        //        memStream.Flush();
        //        memStream.Position = 0; //go to start

        //        using (StreamReader reader = new StreamReader(memStream))
        //        {
        //            Tiddler allTiddler = new Tiddler 
        //            { 
        //                UnescapedContent = reader.ReadToEnd(), 
        //                Title = this.Title 
        //            };
        //            TiddlyWiki wiki = new TiddlyWiki(this.Url);
        //            //prepare a new Wiki for writing
        //            wiki.Add(allTiddler);
        //            wiki.Save();
        //        }
        //        }
        //    }
        //}


    }
}
