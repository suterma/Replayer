using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quirli.Api.Test
{
    [TestClass]
    public class TrackTest
    {
        /// <summary>
        /// Tests, wheter parsing a track with an YouTube URL works.
        /// </summary>
        [TestMethod]
        public void ParseYoutubeUrlTest()
        {
            //Prepare
            var testTrackUrl = @"http://quir.li/player.html?media=http%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3D0VqTwnAuHws&title=What%20makes%20you%20beautiful&artist=The%20piano%20guys%20covering%20One%20Republic&album=Youtube&6.49=Intro&30.12=Knocking%20part&46.02=Real%20playing&51.5=Piano%20forte&93.32=Stringified&123.35=Vocals&139.38=Key%20cover%20jam&150.16=Good%20morning%20sky&173.96=Final%20chord";
            //Act
            var testTrack = new Track(testTrackUrl);

            //Assert
            Assert.AreEqual("http://www.youtube.com/watch?v=0VqTwnAuHws", testTrack.MediaUrl.ToString(), "Faulty media url parsed");
            Assert.AreEqual("What makes you beautiful", testTrack.Title, "Faulty title parsed");
            Assert.AreEqual("The piano guys covering One Republic", testTrack.Artist, "Faulty artist parsed");
            Assert.AreEqual("Youtube", testTrack.Album, "Faulty album parsed");
            Assert.AreEqual(9, testTrack.Cues.Count, "Wrong number of cues parsed");
            var firstCue = testTrack.Cues.First();
            Assert.AreEqual("Intro", firstCue.Text, "faulty text on first cue");
            Assert.AreEqual(6.49, firstCue.Position, "faulty position on first cue");
        }

        /// <summary>
        /// Tests, wheter creating a track with an MP3 URL works.
        /// </summary>
        [TestMethod]
        public void CreateForMp3Test()
        {
            //Prepare
            var cue1 = new Cue(0, "Intro");
            var cue2 = new Cue(15, "Verse 1");
            var cue3 = new Cue(45.5, "Verse 2");
            var cue4 = new Cue(75.94, "Chorus");
            var cue5 = new Cue(106.75, "Verse 3");
            var cue6 = new Cue(140.75, "Chorus");
            var cue7 = new Cue(172.07, "Bridge");
            var cue8 = new Cue(201.65, "Chorus");
            var cue9 = new Cue(232.65, "Outro");

            var track = new Track();
            track.Cues.Add(cue1);
            track.Cues.Add(cue2);
            track.Cues.Add(cue3);
            track.Cues.Add(cue4);
            track.Cues.Add(cue5);
            track.Cues.Add(cue6);
            track.Cues.Add(cue7);
            track.Cues.Add(cue8);
            track.Cues.Add(cue9);
            
            track.Album = "Not for sale";
            track.Artist = "Lidija Roos";
            track.Title = "Sweet taste";

            track.MediaUrl = new Uri("http://dl.dropbox.com/u/3039972/lidija_roos-sweet_taste.mp3");

            //Act
            var trackUrl = track.TrackUrl;

            //Assert
            Assert.AreEqual("http://quir.li/player.html?media=http%3a%2f%2fdl.dropbox.com%2fu%2f3039972%2flidija_roos-sweet_taste.mp3&title=Sweet%20taste&artist=Lidija%20Roos&album=Not%20for%20sale&0=Intro&15=Verse%201&45.5=Verse%202&75.94=Chorus&106.75=Verse%203&140.75=Chorus&172.07=Bridge&201.65=Chorus&232.65=Outro", trackUrl, "Track URL is wrong.");
        }
    }
}
