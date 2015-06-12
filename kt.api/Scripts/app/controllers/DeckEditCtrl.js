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