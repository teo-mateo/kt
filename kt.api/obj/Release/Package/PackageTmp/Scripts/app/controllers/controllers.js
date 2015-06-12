/// <reference path="../../lib/jquery-1.9.1.js" />
/// <reference path="../../lib/angular.js" />

//var ktControllers = angular.module('ktControllers', []);

app.controller('DeckListCtrl', ['$scope', 'decksService', '$rootScope',
    function ($scope, decksService, $rootScope) {

        var loadDecks = function () {
            decksService.getAllDecks()
                .success(function (data) {
                    $scope.decks = data;
                });
        };

        $scope.delete = function (id) {
            decksService.deleteDeck(id)
                .success(function (data) {
                    loadDecks();
                });
        };

        loadDecks();

        $scope.$on('decks.changed', function () {
            loadDecks();
        });
    }]);

app.controller('DeckCardsCtrl', ['$scope', 'decksService', '$routeParams',
    function ($scope, decksService, $routeParams) {
        $scope.deckId = $routeParams.deckId;
        decksService.getDeck($routeParams.deckId)
            .success(function (data) {
                $scope.deck = data;
            });

        $scope.selectCard = function (id) {
            var elemBack = "#cardBack" + id;
            $(elemBack).toggle();
        };
        
    }]);

app.controller("DeckEditCtrl", ['$scope', 'decksService', '$routeParams', '$location', '$rootScope',
    function ($scope, decksService, $routeParams, $location, $rootScope) {
        if ($routeParams.deckId !== undefined) { //editing a deck
            $scope.deckId = $routeParams.deckId;
            decksService.getDeck($routeParams.deckId)
                .success(function (data) {
                    $scope.deck = data;
                });
        } else { // new deck
            $scope.deck = {
                Id: 0,
                CreationDate: new Date()
            };
        };

        $scope.save = function () {
            decksService.saveDeck($scope.deck)
            .success(function (data) {
                $location.path('/decks/' + data.Id, false);
                $rootScope.$broadcast('decks.changed', '');
                $scope.$apply();
            });
        };
    }
]);


app.controller("CardEditCtrl", ["$scope", "cardsService", "$routeParams", "$location", '$rootScope',
    function ($scope, cardsService, $routeParams, $location, $rootScope) {
        if ($routeParams.cardId !== undefined) { // editing a card
            $scope.cardId = $routeParams.cardId;
            cardsService.getCard($routeParams.cardId)
                .success(function (data) {
                    $scope.card = data;
                });
        }
        else { // new card
            $scope.card = {
                DeckId: $routeParams.deckId,
                Id: 0,
                Status: 1,
                Front: {
                    Id: 0,
                    Content: "",
                },
                Back: {
                    Id: 0,
                    Content: ""
                }
            };
        };

        $scope.save = function () {
            cardsService.saveCard($scope.card)
                .success(function (data) {
                    var newloc = $location.path("/decks/" + $scope.card.DeckId);
                    $rootScope.$broadcast('decks.changed', '');
                });
        }
    }
]);