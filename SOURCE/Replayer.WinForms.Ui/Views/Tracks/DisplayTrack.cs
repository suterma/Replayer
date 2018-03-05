using System;
using System.ComponentModel;
using System.Drawing.Design;
using Replayer.Model;
using Replayer.WinForms.Ui.Gui;

namespace Replayer.WinForms.Ui.Views.Tracks {
    /// <summary>
    ///     A wrapper for a track, ready for displaying in
    ///     an ui editor control.
    /// </summary>
    internal class DisplayTrack {
        /// <summary>
        ///     Gets or sets the model.
        /// </summary>
        /// <value>
        ///     The model.
        /// </value>
        [Browsable(false)]
        public Track Model { get; set; }

        /// <summary>
        ///     Gets or sets the path.
        /// </summary>
        /// <value>
        ///     The path.
        /// </value>
        [Browsable(true)]
        [Description("The path to the media file of the track.")]
        [UiPathEditor.OfdParamsAttribute("MP3 Files (*.mp3)|*.mp3", "Select the media file for this track",
            Environment.SpecialFolder.MyMusic)]
        [Editor(typeof (UiPathEditor), typeof (UITypeEditor))]
        public String Path {
            get { return Model.Url; }
            set {
                Model.Url = value;

                //Set default values for the other properties
                Name = System.IO.Path.GetFileNameWithoutExtension(value);
            }
        }


        /// <summary>
        ///     Gets or sets the album.
        /// </summary>
        /// <value>
        ///     The album.
        /// </value>
        [Browsable(true)]
        [Description("Album where the track can be found.")]
        public string Album {
            get { return Model.Album; }
            set { Model.Album = value; }
        }

        /// <summary>
        ///     Gets or sets the artist.
        /// </summary>
        /// <value>
        ///     The artist.
        /// </value>
        [Browsable(true)]
        [Description("The artist performing this track.")]
        public string Artist {
            get { return Model.Artist; }
            set { Model.Artist = value; }
        }

        /// <summary>
        ///     Gets or sets the measure.
        /// </summary>
        /// <value>
        ///     The measure.
        /// </value>
        [Browsable(true)]
        [Description("The measure in beats per minute.")]
        public double Measure {
            get { return Model.Measure; }
            set { Model.Measure = value; }
        }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        [Browsable(true)]
        [Description("The name of the track.")]
        public string Name {
            get { return Model.Name; }
            set { Model.Name = value; }
        }

        /// <summary>
        ///     Create a new instance
        /// </summary>
        /// <param name="track"></param>
        internal DisplayTrack(Track track) {
            Model = track;
        }
    }
}