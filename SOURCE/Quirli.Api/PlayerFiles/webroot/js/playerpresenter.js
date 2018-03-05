/*! 
    quirli, replay with ease.
    Copyright (C) 2012 by marcel suter, marcel@codeministry.ch

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

/*!
 * player presenter layer
 * This presentes data on the tightly coupled view (the player.html file)
 * It uses known identifiers from the view to access and present various data.
 */
 

//set the view style (used by the menu items on the top menu bar)
function setViewStyle(style) {
    globalScope.ViewStyle = style;
    globalScope.$apply();
}

//visually initialize the page, including tooltips
$(document).ready(function () {
    $("#errordisplay").hide(); //to signal working javascript to the user		
    $("[rel=tooltip]").tooltip();
    $("#playercontrols").hide(); //initially, there is no media to control for playing
    $("#progressdisplay").hide(); //document is ready now

    //Specially handle the enter key on the url entry field, to actually change the model on enter key
    $("#sourceurl").keyup(function (event) {
        if (event.keyCode == 13) {
            $("#sourceurl").blur();
        }
    });
});

//this method is called after sucessfully loading the media from a mediaelement player
//and defines the actions for a media element media
function onMediaelementPlayerReady(mediaelementplayer) {
    mediaelementplayer.load();
    defineMediaPlayerHandling(mediaelementplayer);
    globalScope.PlaybackType = mediaelementplayer.pluginType;
    globalScope.$apply();
}




//removes all exising player controls, which stops them playing and unloads the media content
function removeAllPlayers() {
    try {
        doPause(); //to silence eventual media playing
        doStop();  //to somewhat unload media (helps for flash playing in FF at least)
    }
    catch (e) {
        //just catch em all
    }

    globalScope.PlaybackType = "";
    $("#quirliplayer").empty();
}

//Displays an error text in the error area
function displayError(errortext) {
    $("#errortext").text(errortext);
    $("#errordisplay").show();
}

//Removes any previously displayed error
function removeErrors(errortext) {
    $("#errortext").text("");
    $("#errordisplay").hide();
}



//loads the content of the specified url into a new, matching media player
//later, instead of interpreting the url, actually request the file
function loadUrl(objectURL) {
    if ((objectURL === null) || (objectURL === '')) {
        return false; //nothing to load at all
    }
    //TODO later check the existence of the referenced file first, to create a better user experience
    removeErrors(); 

    //determine media type and handle accordingly
    if (objectURL.indexOf("http://www.youtube.com/watch?v=") !== -1) { //contains the usual youtube prefix
        createPlayerAndLoadSource(objectURL, "youtube");
    }else if (objectURL.indexOf("https://www.youtube.com/watch?v=") !== -1) { //contains the usual HTTPS youtube prefix
        createPlayerAndLoadSource(objectURL, "youtube");
    }
    else if (objectURL.substr(objectURL.length - 4) === ".wav") {
        createPlayerAndLoadSource(objectURL, "audio");
    }
    else if (objectURL.substr(objectURL.length - 4) === ".mp3") {
        createPlayerAndLoadSource(objectURL, "audio");
    }
    else if (objectURL.substr(objectURL.length - 4) === ".ogv") {
        createPlayerAndLoadSource(objectURL, "video");
    }
    else if (objectURL.substr(objectURL.length - 4) === ".wmv") {
        createPlayerAndLoadSource(objectURL, "video");
    }
    else if (objectURL.substr(objectURL.length - 5) === ".webm") {
        createPlayerAndLoadSource(objectURL, "video");
    }
    else if (objectURL.substr(objectURL.length - 4) === ".mp4") {
        createPlayerAndLoadSource(objectURL, "video");
    }
    else { //we dont know, just assume video, because video generally also can play audio
        createPlayerAndLoadSource(objectURL, "video");
    }
}

//creates a suitable player, sets the source of the matching media player control, and defines the action handlers.
//this works for real url's only, not for url object of local files (unfortunately these object urls seem to become invalid at the call to this method)
function createPlayerAndLoadSource(objectURL, sourceType) {
    removeAllPlayers(); //to get rid of unused ones
    quirliControlType = sourceType;

    //TODO probably we can simplyfiy this if case
    if (quirliControlType === "video") {
        $("#quirliplayer").append("<video controls src=" + objectURL + "></video>");
        var mediaelementplayer = new MediaElementPlayer($('video'), {
            defaultVideoWidth: '100%',
            videoWidth: '100%',
            pluginWidth: '100%',
            alwaysShowControls: false, //dunno what this actually does. Setting to false does not seem to change anything
            // enables Flash and Silverlight to resize to content size
            enableAutosize: true,
            // the order of controls you want on the control bar (and other plugins below)
            features: ['playpause', 'progress', 'current', 'duration', 'tracks', 'volume', 'fullscreen'],
            // when this player starts, it will pause other players
            pauseOtherPlayers: true,
            success: function (mediaelementplayer) {
                onMediaelementPlayerReady.apply(this, arguments)
            },
            error: function (mediaElement) {
                console.log('medialement problem is detected: %o', mediaElement);
            }
        });
    }
    if (quirliControlType === "audio") {
        $("#quirliplayer").append("<audio controls src=" + objectURL + "></audio>");
        var mediaelementplayer = new MediaElementPlayer($('audio'), {
            alwaysShowControls: true,
            // enables Flash and Silverlight to resize to content size
            enableAutosize: true,
            // width of audio player
            audioWidth: '100%',
            // the order of controls you want on the control bar (and other plugins below)
            features: ['playpause', 'progress', 'current', 'duration', 'tracks', 'volume', 'fullscreen'],
            // when this player starts, it will pause other players
            pauseOtherPlayers: true,
            success: function (mediaelementplayer) {
                onMediaelementPlayerReady.apply(this, arguments)
            },
            error: function (mediaElement) {
                console.log('medialement problem is detected: %o', mediaElement);
            }
        });

    }

    if (quirliControlType === "youtube") {
        $("#quirliplayer").append("<video controls width='640' height='360'>    <source type='video/youtube' src='" + objectURL + "' /></video>");
        var mediaelementplayer = new MediaElementPlayer($('video'), {
            defaultVideoWidth: 640,
            defaultVideoHeight: 360,
            videoWidth: 640,
            videoHeight: 360,
            alwaysShowControls: true,
            // enables Flash and Silverlight to resize to content size
            enableAutosize: true,
            // the order of controls you want on the control bar (and other plugins below)
            features: ['playpause', 'progress', 'current', 'duration', 'tracks', 'volume', 'fullscreen'],
            // when this player starts, it will pause other players
            pauseOtherPlayers: true,
            success: function (mediaelementplayer) {
                onMediaelementPlayerReady.apply(this, arguments)
            },
            error: function (mediaElement) {
                console.log('medialement problem is detected: %o', mediaElement);
            }
        });
    }
}

///Adds a visual representation of a cue
function presentCue(caption, position) {
    //thru the globally availabe scope of the cues, add the new one. This is kind of a hack, but i dont know how to access the cues otherwise
    globalScope.text = caption;
    globalScope.position = position;
    globalScope.addCueAsNewest();
}

//creates an url of the current page, with parameters describing the media and cues
//and presents that in a textbox for copying by the user
function SaveAsLink() {

    var cues = [];
    $.each(globalScope.cues, function (index, item) {
        //alert(index + ': ' + value);
        var cue = item.position + "=" + encodeURIComponent(item.text);
        cues.push(cue);
    });
    var serializedCues = cues.join('&');

    //serialize the media file url
    var pageUrl = window.location.href.split('?')[0]; //get the url without the (probably already existing) query part

    //provide the link url
    var linkUrl = pageUrl + "?media=" + encodeURIComponent(globalScope.MediaUrl) +
            "&title=" + encodeURIComponent(globalScope.TrackTitle) +
            "&artist=" + encodeURIComponent(globalScope.ArtistName) +
            "&album=" + encodeURIComponent(globalScope.AlbumName) +
            "&" + serializedCues;
    $("#savelinkInBox").val(linkUrl);
    $("#savelinkToLoad").attr("href", linkUrl);
}

//defines the concrete MediaelementJs player actions for the standardized do...Actions
function defineMediaPlayerHandling(mediaPlayer) {
    doPlay = function () {
        //The audio flash requires this check below, otherwise it plays more than once when clicked the play button repeatedly
        if (mediaPlayer.paused || mediaPlayer.ended) {
            mediaPlayer.play();
        }
    } //make sure we do not "play twice"
    doPause = function () { mediaPlayer.pause(); }
    doStop = function () { mediaPlayer.pause(); mediaPlayer.stop(); } //To really stop any ongoing flash stuff
    doIncreaseVolume = function () { mediaPlayer.setVolume(mediaPlayer.volume + 0.1); }
    doDecreaseVolume = function () { mediaPlayer.setVolume(mediaPlayer.volume - 0.1); }
    //Seeks to the point in time, but does not autoplay
    doSeekTo = function (position) { mediaPlayer.setCurrentTime(position); }
    doGetPosition = function () { return mediaPlayer.currentTime; }
    doAddCueHere = function () { presentCue('', doGetPosition().toFixed(2)); } //round to two decimal places only, more is not audible anyway
}





