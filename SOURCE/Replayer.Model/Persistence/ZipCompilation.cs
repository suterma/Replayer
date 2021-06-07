using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using ICSharpCode.SharpZipLib.Zip;

namespace Replayer.Model.Persistence {
    /// <summary>
    ///     A compilation, consisting of an ordinary xml Compilation
    ///     file, plus respective media files,
    ///     packed in a single zipped archive.
    /// </summary>
    [Serializable]
    public class ZipCompilation : CompilationBase {
        /// <summary>
        ///     The implicit name of the serialized Compilation within this archive.
        /// </summary>
        private static readonly string EmbeddedXmlCompilationFilename = "ZIP-Compilation" +
                                                                        XmlCompilation.DefaultExtension;

        /// <summary>
        ///     Backing field for the URL property.
        /// </summary>
        private string _url;

        /// <summary>
        ///     This compilation is of type ZIP.
        /// </summary>
        public override CompilationType Type {
            get { return CompilationType.Zip; }
        }

        /// <summary>
        ///     Gets or sets the inner Compilation.
        /// </summary>
        /// <remarks>
        ///     This is the Compilation that is stored inside the ZIP file.
        /// </remarks>
        /// <value>The inner Compilation.</value>
        /// <devdoc>
        ///     The properties of this compilation are propagated forth and back from
        ///     this inner compilation, except the URL, which gets stored separately
        ///     to have a specific extension.
        /// </devdoc>
        private XmlCompilation InnerCompilation { get; set; }

        /// <summary>
        ///     Gets or sets the media path. This is where the media files are retrieved from.
        /// </summary>
        /// <value>The media path.</value>
        [Browsable(false)]
        public override String MediaPath {
            get { return InnerCompilation.MediaPath; }
            set { InnerCompilation.MediaPath = value; }
        }

        /// <summary>
        ///     Gets or sets the URL, where this Compilation is stored. This is used for storage and retrieval.
        /// </summary>
        /// <value>The URL.</value>
        [Browsable(false)]
        public override string Url {
            get { return _url; }
            set {
                _url = Path.ChangeExtension(value, DefaultExtension); //make sure we have the right extension
                IsDirty = true;
            }
        }

        /// <summary>
        ///     Gets or sets the tracks.
        /// </summary>
        /// <value>The tracks.</value>
        [Browsable(false)]
        public override ObservableCollection<Track> Tracks {
            get { return InnerCompilation.Tracks; }
            set { InnerCompilation.Tracks = value; }
        }

        /// <summary>
        ///     Gets or sets the title for this Compilation.
        /// </summary>
        /// <value>The title.</value>
        [Description("The title of the compilation.")]
        public override String Title {
            get { return InnerCompilation.Title; }
            set { InnerCompilation.Title = value; }
        }

        public override bool IsDirty {
            get { return InnerCompilation.IsDirty; }
            set { InnerCompilation.IsDirty = value; }
        }

        /// <summary>
        ///     Gets or sets the extraction path.
        /// </summary>
        /// <remarks>
        ///     path to temporarily extract the zipped media files to
        ///     The decompression of the media files is necessary because
        ///     the used media player will need a single file, not a zipped archive.
        /// </remarks>
        /// <value>The extraction path.</value>
        private string ExtractionPath { get; set; }


        /// <summary>
        ///     Gets the default extension for zip Compilations.
        /// </summary>
        /// <value>The default extension.</value>
        public static string DefaultExtension {
            get { return ".rez"; }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ZipCompilation" /> class.
        /// </summary>
        public ZipCompilation() {
            //create the inner Compilation
            InnerCompilation = new XmlCompilation();
            Id = Guid.NewGuid();

            ExtractionPath = Path.GetTempPath(); //set extraction path for this Compilation
        }


        /// <summary>
        ///     Finds the full url for the (probably partially specified url)
        ///     for the media file url, using the media path and
        ///     user input if necessary.
        /// </summary>
        /// <param name="track"></param>
        /// <returns></returns>
        [Browsable(false)]
        public override String Find(Track track) {
            List<string> places = new List<string> { MediaPath };
            string path = TrackFinder.Find(track.Url, places);

            //before returning the path, check if found, otherwise request user input, if not found successfully
            if (!File.Exists(path)) {
                throw new FileNotFoundException("Media file not found");
            }

            //we now have the best guess about where that media file is, so save that path here
            if (!track.Url.Equals(path)) //we have now found a location that is different to what we first knew?
            {
                //use this one from now on with this track.
                track.Url = path;
            }
            return path;
        }


        /// <summary>
        ///     Diese Funktion komprimiert Dateien zu einem ZIP-Archiv.
        /// </summary>
        /// <param name="InputFiles">Die Liste mit Dateien die komprimiert werden soll.</param>
        /// <param name="FileName">Der Dateiname der ZIP-Datei (ohne Pfad).</param>
        /// <param name="OutputDir">Das Ausgabeverzeichnis wo die ZIP Datei gespeichert werden soll.</param>
        /// <remarks></remarks>
        public void CompressFiles(IEnumerable<string> InputFiles, string FileName, string OutputDir) {
            FileStream ZFS = new FileStream(OutputDir + "\\" + Path.GetFileNameWithoutExtension(FileName) + DefaultExtension,
                                     FileMode.Create);
            ZipOutputStream ZOut = new ZipOutputStream(ZFS);

            ZOut.SetLevel(0); //Store only, do not compress. This ensures best performance with the zip archive

            ZipEntry ZipEntry = default(ZipEntry);

            byte[] Buffer = new byte[4097];
            int ByteLen = 0;
            FileStream FS = null;


            //for (int i = 0; i <= InputFiles.Count - 1; i++)
            foreach (string file in InputFiles) {
                //ZipEntry erstellen
                ZipEntry = new ZipEntry(Path.GetFileName(file));
                ZipEntry.DateTime = DateTime.Now;

                //Eintrag hinzufügen
                ZOut.PutNextEntry(ZipEntry);

                //Datei in den Stream schreiben
                FS = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);
                do {
                    ByteLen = FS.Read(Buffer, 0, Buffer.Length);
                    ZOut.Write(Buffer, 0, ByteLen);
                } while (!(ByteLen <= 0));
                FS.Close();
            }

            ZOut.Finish();
            ZOut.Close();
            ZFS.Close();
        }

        /// <summary>
        ///     Diese Funktion dekomprimiert eine ZIP-Datei.
        /// </summary>
        /// <param name="FileName">Die Datei die dekomprimiert werden soll.</param>
        /// <param name="OutputDir">Das Verzeichnis in dem die Dateien dekomprimiert werden sollen.</param>
        public static IEnumerable<string> DecompressFile(string FileName, string OutputDir) {
            List<string> fileNames = new List<string>();
            FileStream ZFS = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            ZipInputStream ZIN = new ZipInputStream(ZFS);

            ZipEntry ZipEntry = default(ZipEntry);

            byte[] Buffer = new byte[4097];
            int ByteLen = 0;
            FileStream FS = null;

            string InZipDirName = null;
            string InZipFileName = null;
            string TargetFileName = null;

            do {
                ZipEntry = ZIN.GetNextEntry();
                if (ZipEntry == null) {
                    break;
                }


                InZipDirName = Path.GetDirectoryName(ZipEntry.Name) + "\\";
                InZipFileName = Path.GetFileName(ZipEntry.Name);

                if (Directory.Exists(OutputDir + "\\" + InZipDirName) == false) {
                    Directory.CreateDirectory(OutputDir + "\\" + InZipDirName);
                }

                if (InZipDirName == "\\") {
                    InZipDirName = "";
                }
                TargetFileName = Path.GetFullPath(OutputDir + "\\" + InZipDirName + InZipFileName);
                //use GetFullPath to ensure correct formatting of the composed path
                fileNames.Add(TargetFileName);

                //check whether this exact file was already decopressed
                if (File.Exists(TargetFileName)) //file with same name is there
                {
                    if (new FileInfo(TargetFileName).Length == ZipEntry.Size) //exactly equal size when decompressed?
                    {
                        //we most probably already have done this file
                        continue;
                    }
                }

                using (FS = new FileStream(TargetFileName, FileMode.Create)) {
                    do {
                        ByteLen = ZIN.Read(Buffer, 0, Buffer.Length);
                        FS.Write(Buffer, 0, ByteLen);
                    } while (!(ByteLen <= 0));
                }
            } while (true);

            ZIN.Close();
            ZFS.Close();

            return fileNames;
        }


        /// <summary>
        ///     Stores this instance to the same place it was loaded from.
        /// </summary>
        public override void Store() {
            Store(Url);
        }

        /// <summary>
        ///     Stores this instance to the specified place.
        /// </summary>
        public override void Store(String url) {
            if (String.IsNullOrEmpty(url)) {
                throw new ArgumentException(
                    "The storage url is empty or null. Please provide a valid URL to store the compilation to.");
            }

            Url = url; //use this from now on.

            //store Compilation back to file
            XmlSerializer CompilationSerializer = new XmlSerializer(typeof(XmlCompilation));
            using (TextWriter writeFileStream = new StreamWriter(ExtractionPath + EmbeddedXmlCompilationFilename)) {
                CompilationSerializer.Serialize(writeFileStream, InnerCompilation);
            }

            //get the files to zip
            IList<String> files = (from track in Tracks
                                   select Find(track)).ToList(); //track media files
            files.Add(ExtractionPath + EmbeddedXmlCompilationFilename); //this Compilation file

            CompressFiles(files, Path.GetFileName(Url), Path.GetDirectoryName(Url));
            IsDirty = false;
        }

        /// <summary>
        ///     Retrieves the stored Compilation from the specified url.
        /// </summary>
        public override ICompilation Retrieve(String url) {
            ZipCompilation retrieved = new ZipCompilation();
            retrieved.ExtractionPath = Path.GetTempPath(); //set extraction path for the retrieved Compilation

            IEnumerable<string> filenames = DecompressFile(url, retrieved.ExtractionPath);

            //find the Compilation file
            string CompilationUnzippedFileName = (from unzipfileName in filenames
                                                  where
                                                      Path.GetExtension(unzipfileName)
                                                          .Equals(XmlCompilation.DefaultExtension)
                                                  select unzipfileName).First();


            XmlSerializer CompilationSerializer = new XmlSerializer(typeof(XmlCompilation));
            using (
                FileStream readFileStream = new FileStream(CompilationUnzippedFileName, FileMode.Open, FileAccess.Read,
                                                    FileShare.Read)) {
                XmlCompilation xmlCompilation = (XmlCompilation)CompilationSerializer.Deserialize(readFileStream);

                //now with the retrived XML Compilation, fill the properties of the retrieved instance
                retrieved.Tracks = xmlCompilation.Tracks;

                //make sure, the path to the compilation is absolute
                retrieved.Url = Path.GetFullPath(url);


                ///set the Compilation media path to the temporary directory, so that the track media
                ///files are searched and found in there.
                retrieved.MediaPath = retrieved.ExtractionPath;
                retrieved.Title = xmlCompilation.Title;
            }
            ///remove the path information from the track media files, so that they are not searched with
            ///that absolute path.
            foreach (Track item in retrieved.Tracks) {
                item.Url = Path.GetFileName(item.Url); //remember only filename (with extension)
            }
            retrieved.IsDirty = false; //explicitly set false, since we just have loaded the data
            return retrieved;
        }
    }
}