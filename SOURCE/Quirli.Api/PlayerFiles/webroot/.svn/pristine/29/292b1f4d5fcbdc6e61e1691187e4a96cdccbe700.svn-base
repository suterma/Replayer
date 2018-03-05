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
 * Controller for the playlist, using AngularJS.
 */

function playlistcontroller($scope) {
   
    $scope.tracks = [];    
     
    $scope.addTrack = function(title, url) {
    $scope.tracks.push({ title: title, url: url});
    };

    $scope.loadTrack = function (url) {
    window.location.href = url;
    }

    $scope.title = "";
    
    //Get the playlist content
    //This will use the function name 'callback_json1' as callback method.
    JSONP.get('./playlistcontent.jsonp', {}, function (data) {
        //do something with data, which is the JSON object received from the url            
        $scope.title = data.title;
        $scope.tracks = data.tracks;
        $scope.$apply();
    });        
}
