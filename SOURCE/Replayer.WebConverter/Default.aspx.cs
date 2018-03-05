using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;

namespace Replayer.WebConverter
{
    public partial class Default : System.Web.UI.Page
    {

        protected void ButtonUpload_Click(object sender, EventArgs e)
        {
            if (RezFileUpload.HasFile)
            {
                Stream data = RezFileUpload.FileContent;
                // This stream cannot be opened with the ZipFile class because CanSeek is false.

                //unzip the rez compilation to an xml compilation for memory-optimized processing first.
                UnzipFromStream(data, @"c:\temp"); //TODO use OS level temp dir

                //TODO convert the xml compilation into quirli compilation
                var compilation = Replayer.Model.XmlCompilation.Retrieve(@"c:\temp\ZIP-Compilation.rex");


                //return back to client
            }
        }

        public void UnzipFromStream(Stream zipStream, string outFolder)
        {

            ZipInputStream zipInputStream = new ZipInputStream(zipStream);
            ZipEntry zipEntry = zipInputStream.GetNextEntry();
            while (zipEntry != null)
            {
                String entryFileName = zipEntry.Name;
                // to remove the folder from the entry:- entryFileName = Path.GetFileName(entryFileName);
                // Optionally match entrynames against a selection list here to skip as desired.
                // The unpacked length is available in the zipEntry.Size property.

                byte[] buffer = new byte[4096];     // 4K is optimum

                // Manipulate the output filename here as desired.
                String fullZipToPath = Path.Combine(outFolder, entryFileName);
                string directoryName = Path.GetDirectoryName(fullZipToPath);
                if (directoryName.Length > 0)
                    Directory.CreateDirectory(directoryName);

                // Unzip file in buffered chunks. This is just as fast as unpacking to a buffer the full size
                // of the file, but does not waste memory.
                // The "using" will close the stream even if an exception occurs.
                using (FileStream streamWriter = File.Create(fullZipToPath))
                {
                    StreamUtils.Copy(zipInputStream, streamWriter, buffer);
                }
                zipEntry = zipInputStream.GetNextEntry();
            }
        }
    }
}
