/*!
 * quirli angular js controller
 * 
 * Created by Marcel Suter, codeministry.ch
 * Licensed under GPL Version 3.
 */
function playerController($scope) {
    
        globalScope = $scope; //TODO to give easy access, but later rework to fit the angular style
    
        $scope.ArtistName = "";
    
        $scope.cues = [];    
     
        $scope.addCue = function() {
            $scope.cues.push({ text: $scope.text, position: $scope.position, recycle: false, shortcut: '' });
            //clean up the container variables for reuse
            $scope.text = '';    
            $scope.position = '';
            $scope.shortcut = '';
        };
     
        $scope.remaining = function() {
            var count = 0;
            angular.forEach($scope.cues, function(cue) {
                count += cue.recycle ? 0 : 1;
                });
            return count;
        };
     
        $scope.archive = function() {
            var oldcues = $scope.cues;
            $scope.cues = [];
            angular.forEach(oldcues, function(cue) {
               if (!cue.recycle) $scope.cues.push(cue);
            });
        };

        //Seeks the currently loaded media to the given position
        //This uses the global do actions.
        $scope.playFromPosition = function (position) {
            if (quirliControlType === "youtube") {
                doSeekTo(position); //invoke global doSeekTo method as defined for the media players
                doPlay(); //for youtube, playing must start after seeking.
            }
            else {
                doPlay(); //playing first seems necessary to make this work after a load of a new file in all other cases than youtube. Seeking and then playing will not work in this case.
                doSeekTo(position); //invoke global doSeekTo method as defined for the media players
            }
        }
    }
