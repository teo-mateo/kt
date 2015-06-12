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