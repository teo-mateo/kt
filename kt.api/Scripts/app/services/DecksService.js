app.factory('decks', ['$http', function ($http) {
    var url = "/api/Decks";
    return $http.get(url)
        .success(function (data) {
            return data;
        })
        .error(function (data) {
            return data;
        });
}]);

app.factory('singleDeck', ['$resource', function ($resource) {
    var url = "/api/Decks/:id";
    return $resource(url, { id: "@deckId" });
}]);