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
        
    }]);

app.controller("DeckEditCtrl", ['$scope', 'decksService', '$routeParams', '$location', '$rootScope',
    function ($scope, decksService, $routeParams, $location, $rootScope) {
        if ($routeParams.deckId !== undefined) {
            $scope.deckId = $routeParams.deckId;
            decksService.getDeck($routeParams.deckId)
                .success(function (data) {
                    $scope.deck = data;
                });
        } else {
            $scope.deck = {
                Id: 0,
                CreationDate: new Date()
            };
        }

        $scope.save = function () {
            decksService.saveDeck($scope.deck)
            .success(function (data) {
                $location.path('#/decks/' + data.Id, false);
                $rootScope.$broadcast('decks.changed', '');
            });
        };
    }
]);