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