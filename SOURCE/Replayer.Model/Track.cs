using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Replayer.Model {
    /// <summary>
    ///     A track with cues.
    /// </summary>
    public class Track : INotifyPropertyChanged {
        /// <summary>
        ///     Backing field
        /// </summary>
        private String _album;

        /// <summary>
        ///     Backing field
        /// </summary>
        private String _artist;

        /// <summary>
        ///     Backing field.
        /// </summary>
        private Guid _id;

        /// <summary>
        ///     Backing field
        /// </summary>
        private double _measure;

        /// <summary>
        ///     Backing field
        /// </summary>
        private String _name;

        /// <summary>
        ///     Backing field
        /// </summary>
        private String _url;

        /// <summary>
        ///     Gets or sets the cues.
        /// </summary>
        /// <value>The cues.</value>
        [Browsable(false)]
        public ObservableCollection<Cue> Cues { get; set; }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        [Browsable(false)]
        public Guid Id {
            get { return _id; }
            set {
                if (_id == value) {
                    return; //no change? forget about it!
                }
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        /// <summary>
        ///     Gets or sets the artist.
        /// </summary>
        /// <value>The artist.</value>
        public String Artist {
            get { return _artist; }
            set {
                if (_artist == value) {
                    return; //no change? forget about it!
                }
                _artist = value;
                OnPropertyChanged("Artist");
            }
        }

        /// <summary>
        ///     Gets or sets the track name.
        /// </summary>
        /// <value>The name.</value>
        [Browsable(true)]
        [Description("The name of the track.")]
        public String Name {
            get { return _name; }
            set {
                if (_name == value) {
                    return; //no change? forget about it!
                }
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        /// <summary>
        ///     Gets or sets the album name where this track was taken from.
        /// </summary>
        /// <remarks>This is a more descriptive information and not intended to uniquely identify an album.</remarks>
        /// <value>The album.</value>
        [Browsable(true)]
        [Description("Album where the track can be found.")]
        public String Album {
            get { return _album; }
            set {
                if (_album == value) {
                    return; //no change? forget about it!
                }
                _album = value;
                OnPropertyChanged("Album");
            }
        }

        /// <summary>
        ///     Gets or sets the measure.
        /// </summary>
        /// <value>The measure.</value>
        [Browsable(true)]
        [Description("The measure in beats per minute.")]
        public double Measure {
            get { return _measure; }
            set {
                if (_measure == value) {
                    return; //no change? forget about it!
                }
                _measure = value;
                OnPropertyChanged("Measure");
            }
        }

        /// <summary>
        ///     Gets or sets the URL for the media file.
        /// </summary>
        /// <remarks>Currently, only local file paths are supported.</remarks>
        /// <value>The URL.</value>
        /// <devdoc>If it is relative, it may get made absolute using the compilation's media path.</devdoc>
        [DisplayName("Path/URL")]
        [Browsable(true)]
        [Description("The media file path or URL for this track.")]
        public String Url {
            get { return _url; }
            set {
                if (_url == value) {
                    return; //no change? forget about it!
                }
                _url = value;
                OnPropertyChanged("Url");
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Track" /> class.
        /// </summary>
        public Track() {
            Cues = new ObservableCollection<Cue>(); //create initial empty collection.
            Id = Guid.NewGuid();
        }

        /// <summary>
        ///     Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        ///     Creates a fully identical deep clone (except the Id property, which gets a new Id) of this instance.
        /// </summary>
        /// <returns>The clone</returns>
        public Track Clone() {
            Track clone = new Track();
            clone.Album = Album;
            clone.Artist = Artist;
            clone.Cues = Clone(Cues);
            clone.Id = Guid.NewGuid();
            clone.Measure = Measure;
            clone.Name = Name;
            clone.Url = Url;
            return clone;
        }

        /// <summary>
        ///     Clones a set of observable cues (except the Id property, which gets a new Id)
        /// </summary>
        /// <returns>The clone</returns>
        private static ObservableCollection<Cue> Clone(ObservableCollection<Cue>  cues) {
            ObservableCollection<Cue> clone = new ObservableCollection<Cue>(); 
            foreach (Cue cue in cues) {
                clone.Add(cue.Clone());
            }
            return clone;
        }

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() {
            return Artist + "/" + Album + ":" + Name + "(" + Url + ")";
        }

        /// <summary>
        ///     Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        private void OnPropertyChanged(String propertyName) {
            if (PropertyChanged != null) //anyone listening?
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}