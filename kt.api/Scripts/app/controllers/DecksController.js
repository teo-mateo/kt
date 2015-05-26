function makeid() {
    var text = "";
    var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    for (var i = 0; i < 5; i++)
        text += possible.charAt(Math.floor(Math.random() * possible.length));

    return text;
}


app.controller('DecksController', [
    '$scope','$http',
    function ($scope, $http) {
        //this method instantiates the scope.

        //$scope.ktservice = 'http://ktapi.azurewebsites.net/';
        $scope.ktservice = 'http://localhost:63611/';

        $scope.deckActionAddNew = "Add a new deck";
        $scope.deckActionEdit = "Change deck";
        $scope.deckActionHeader = "";

        $scope.cardActionAddNew = "Add a new card";
        $scope.cardActionEdit = "Change card";
        $scope.cardActionHeader = "";


        $scope.decks = undefined;
        $scope.editDeck = undefined;
        $scope.selectedDeck = undefined;

        $scope.editCard = undefined;

        var _scope = $scope;

        /*
        Load all decks
        */
        $scope.loadDecks = function loadDecks() {
            var url = $scope.ktservice + 'api/Decks';
            $http.get(url).success(function (data) {
                _scope.decks = data;
            }).error(function (err) {
                alert(err);
            });

            $('.deckViewArea').hide();
        };

        /*
        Delete a deck
        */
        $scope.deleteDeck = function (deckId) {
            bootbox.confirm("delete?", function (result) {
                if (result) {
                    //do the actual deletion
                    var url = _scope.ktservice + 'api/Decks/' + deckId;
                    $http.delete(url).success(function (data) {
                        _scope.loadDecks();
                    })
                    .error(function (err) {
                        alert(err);
                    });
                }
            });
        };

        /*
        Show ui to create/edit deck
        */
        $scope.showDeckEditor = function (deckId) {
            if (deckId == undefined || deckId == 0) {
                $scope.editDeck = {
                    "Id": 0,
                    "Name": "",
                    "CreationDate": new Date()
                };
                //change header
                $scope.deckActionHeader = $scope.deckActionAddNew;
            }
            else {
                $scope.editDeck = $scope.decks.filter(function (element) {
                    return element.Id === deckId;
                })[0];
                //change header
                $scope.deckActionHeader = $scope.deckActionEdit;
            }
        };

        /*
        Send deck to server to be saved (new or edited)
        */
        $scope.saveDeck = function () {

            if ($scope.editDeck.Id == 0) {
                var url = $scope.ktservice + 'api/Decks';
                $http.post(url, $scope.editDeck)
                    .success(function () { _scope.loadDecks(); })
                    .error(function (err) { alert(err); });
            }
            else {
                var url = $scope.ktservice + 'api/Decks/' + $scope.editDeck.Id;
                $http.put(url, $scope.editDeck)
                    .success(function () { _scope.loadDecks(); })
                    .error(function (err) { alert(err); });
            }

            $scope.editDeck = undefined;
        };

        //display the questions that belong to a deck
        $scope.viewDeck = function (deckId) {
            $('.deckViewArea').show();
            $scope.selectedDeck = $scope.decks.filter(function (element) {
                return element.Id === deckId;
            })[0];
        };

        $scope.showCardEditor = function (cardId) {
            
            if (cardId == undefined || cardId == 0) {
                $scope.editCard = {
                    "Id": "",
                    "DeckId": $scope.selectedDeck.Id,
                    "Status": 1,
                    "Front": {
                        "Id": 0,
                        "Content": ""
                    },
                    "Back": {
                        "Id": 0,
                        "Content": ""
                    }
                };
            } else {
                $scope.editCard = $scope.selectedDeck.Cards.filter(function (obj) {
                    return obj.Id === cardId;
                })[0];
            }
        };

        $scope.saveCard = function () {
            if ($scope.editCard.Id == "") {
                //new id for this new card
                $scope.editCard.Id = makeid();
                var url = $scope.ktservice + 'api/Cards';
                $http.post(url, $scope.editCard)
                    .success(function () {
                        _scope.selectedDeck.Cards.push(_scope.editCard);
                        _scope.editCard = undefined;
                    })
                    .error(function (err) {
                        alert(err);
                        _scope.editCard = undefined;
                    });
            }
            else {
                var url = $scope.ktservice + 'api/Cards/' + $scope.editCard.Id;
                $http.put(url, $scope.editCard)
                    .success(function () {
                        _scope.editCard = undefined;
                    })
                    .error(function (err) {
                        alert(err);
                        _scope.editCard = undefined;
                    });
            }
        };


        $scope.selectCard = function (cardId) {
            var elemBack = "#cardBack" + cardId;
            $(elemBack).toggle();
        };


        $scope.loadDecks();

    }
]);

