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