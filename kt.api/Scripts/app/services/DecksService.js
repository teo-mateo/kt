/// <reference path="../../lib/jquery-1.9.1.js" />
/// <reference path="../../lib/angular.js" />


//the 'decksService' service exposes the following methods: 
// getAllDecks() = returns a list of all decks (cards not loaded)
// getDeck(id) - returns the deck with the given id, cards loaded.
// deleteDeck(id) - TBD
// createNewDeck(deck) - TBD
// updateDeck(deck) - TBD
app.factory('decksService', ['$http', function ($http) {
    var URL_DECKS = "/api/Decks";
    var url_decks_get_one = function (id) { return URL_DECKS + "/" + id; }

    var fnGetAllDecks = function () {
        return $http.get(URL_DECKS)
            .success(function (data) {
                return data;
            })
            .error(function (data) {
                toastr.error("getAllDecks() error");
            })
    };
    var fnGetDeck = function (id) {
        return $http.get(url_decks_get_one(id))
        .success(function (data) {
            return data;
        })
        .error(function (data) {
            toastr.error("getDeck(" + id + ") error");
        })
    };
    var fnSaveDeck = function (deck) {
        if (deck.Id == 0) {
            //new deck
            return $http.post(URL_DECKS, deck)
            .success(function (data) {
                toastr.success("new deck was successfully saved.");
                return data;
            })
            .error(function (data) {
                toastr.error("error saving new deck");
                return data;
            });
        } else {
            //update deck
            return $http.put(URL_DECKS + "/" + deck.Id, deck)
            .success(function (data) {
                toastr.success("deck was successfully updated.");
                return data;
            })
            .error(function (data) {
                toastr.error("error updating deck.");
                return data;
            });
        }
    };
    var fnDeleteDeck = function (id) {
        return $http.delete(URL_DECKS + "/" + id)
        .success(function (data) {
            toastr.success("deck was deleted.");
            return data;
        })
        .error(function (data) {
            toastr.error("error deleting deck");
            return data;
        });
    }


    var o =
        {
            getAllDecks: fnGetAllDecks,
            getDeck: fnGetDeck,
            saveDeck: fnSaveDeck,
            deleteDeck: fnDeleteDeck
        };
    return o;
}]);

