function twitter() {
    "use strict";
    var bearerToken ="";
    var TWITTER_CLIENT_KEY = "";
    var TWITTER_CLIENT_SECRET = "";

    function doSearch(searchTerm, success, error) {
        var searchReq = new XMLHttpRequest();
 
        searchReq.onreadystatechange = function () {
            if (searchReq.readyState === 4) {
                if (searchReq.status === 200) {
                    success(JSON.parse(searchReq.responseText));
                } else {
                    error("Error processing search: (" + searchReq.status + ") " + searchReq.statusText);
                }
            }
        };

        searchReq.open("GET", "https://api.twitter.com/1.1/search/tweets.json?q=" + encodeURIComponent(searchTerm), true);
        searchReq.setRequestHeader("Authorization", "Bearer " + bearerToken);
        searchReq.send();

    };
    
    var search = function (searchTerm, success, error) {
        if (bearerToken === '') {
            login(function () {
                doSearch(searchTerm,
                    function (results) {
                        success(results);
                    },
                    function (errorMessage) {
                        error(errorMessage);
                    })
            }, function (errorMessage) {
                error(errorMessage);
            });
        } else {
            doSearch(searchTerm,
                function (results) {
                    success(results);
                },
                function (errorMessage) {
                    error(errorMe);
                });
        }
    };

    function login(success, error) {

        if (TWITTER_CLIENT_KEY === "" || TWITTER_CLIENT_SECRET === "") {
            error("Set the twitter key and secret in the TwitterService.js file to proceed. You'll need a Twitter Application setup to get these.");
            return;
        }
        var req = new XMLHttpRequest();
        req.onreadystatechange = function () {
            if (req.readyState == 4) {
                if (req.status == 200) {
                    var body = JSON.parse(req.responseText);
                    if (body.token_type === "bearer") {
                        bearerToken = body.access_token;
                        success();
                    } else {
                        error("invalid token type returned");
                    }
                } else {
                    error("Error logging in to Twitter: (" + req.status + ") " + req.statusText);
                }
            }
        };
        req.onerror = function (httpErr) {
            error(httpErr)
        };
        req.onabort = function () {
            error("Login request aborted");
        }

        req.open("POST", "https://api.twitter.com/oauth2/token", true);

        req.setRequestHeader("Authorization", "Basic " + createAppKey());
        req.setRequestHeader("Content-Type", "application/x-www-form-urlencoded;charset=UTF-8");

        req.send("grant_type=client_credentials");
    };

    function createAppKey() {
        var combined = TWITTER_CLIENT_KEY + ":" + TWITTER_CLIENT_SECRET;
        return btoa(combined);
    };

    return {
        search: search
    }

}