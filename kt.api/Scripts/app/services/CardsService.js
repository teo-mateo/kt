/// <reference path="../../lib/jquery-1.9.1.js" />
/// <reference path="../../lib/angular.js" />

//the 'cardsService' service exposes the following methods: 
// getCard(id) = returns the card with the given id
// deleteCard(id) - deletes the card with the given id
// saveCard(card) - saves a new or updated card
app.factory('cardsService', ['$http', function ($http) {
    var URL_CARDS = '/api/Cards';
    var url_card = function (id) { return URL_CARDS + "/" + id; }

    //GET /api/Cards
    var fnGetCard = function (id) {
        return $http.get(url_card(id))
        .success(function (data) {
            return data;
        })
        .error(function (data) {
            toastr.error("getCard() error");
        });
    };
    //POST /api/Cards
    //PUT /api/Cards/5
    var fnSaveCard = function (card) {
        if (card.Id == 0) { // new card
            card.id = app.ktMakeId();
            return $http.post(URL_CARDS, card)
            .success(function (data) {
                toastr.success("new card was successfully saved.");
                return data;
            })
            .error(function (data) {
                toastr.error("error saving new card.");
                return data;
            });
        } else { //update card
            return $http.put(url_card(card.Id), card)
            .success(function (data) {
                toastr.success("card was successfully updated.")
                return data;
            })
            .error(function (data) {
                toastr.error("error updating card.");
                return data;
            });
        }
    };
    //DELETE /api/Cards/5
    var fnDeleteCard = function (id) {
        return $http.delete(url_card(id))
        .success(function (data) {
            toastr.success("card was deleted.");
            return data;
        })
        .error(function (data) {
            toastr.error("error deleting card.");
            return data;
        });
    };

    var o = {
        getCard: fnGetCard,
        saveCard: fnSaveCard,
        deleteCard: fnDeleteCard
    };
    return o;
    

}]);