// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkID=397704
// To debug code on page load in Ripple or on Android devices/emulators: launch your app, set breakpoints, 
// and then run "window.location.reload()" in the JavaScript Console.
(function () {
    "use strict";

    document.addEventListener( 'deviceready', onDeviceReady.bind( this ), false );

    function onDeviceReady() {
        // Handle the Cordova pause and resume events
        document.addEventListener( 'pause', onPause.bind( this ), false );
        document.addEventListener( 'resume', onResume.bind( this ), false );
        
        // TODO: Cordova has been loaded. Perform any initialization that requires Cordova here.
    };

    function onPause() {
        // TODO: This application has been suspended. Save application state here.
    };

    function onResume() {
        // TODO: This application has been reactivated. Restore application state here.
    };
})();

function searchTwitter() {

    //clear the results
    var list = $("#results");
    list.empty();
    if (navigator.connection.type !== 'none') {
        var whatToSearch = $("#searchTerm").val();
        var twit = new twitter();
        twit.search(whatToSearch, function (results) {
            //add results to the list
            $.each(results.statuses, function (i, item) {
                list.append($("<dt>" + item.user.name + "</dt>"));
                list.append($("<dd>" + item.text + "</dd>").appendTo(list));
            });
        },
        function (error) {
            window.alert("Search failed: " + error);
        });
    } else {
        navigator.notification.alert("No connection, try later", null, "Error!", "OK");
    }
}

function takePicture() {

    navigator.camera.getPicture(function (imageData) {
        $("#audienceSmiling").attr("src", "data:image/jpeg;base64," + imageData);
    }, function (error) {
        navigator.notification.alert(error, null, "Error capturing picture", "Ok");
    },
    { destinationType: Camera.DestinationType.DATA_URL, correctOrientation: true, sourceType: Camera.PictureSourceType.CAMERA, encodingType: Camera.EncodingType.JPEG }
    );
}