/// <reference path="../../jquery-1.9.1.js" />
/// <reference path="../../angular.js" />
/// <reference path="../../angular-route.js" />

//toastr options
toastr.options = {
    "closeButton": false,
    "debug": false,
    "newestOnTop": false,
    "progressBar": false,
    "positionClass": "toast-top-center",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "5000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
};

var app = angular.module('ktApp', ['ngRoute', ]);

///function that generates a string identifier.
app.ktMakeId = function () {
    var text = "";
    var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    for (var i = 0; i < 5; i++)
        text += possible.charAt(Math.floor(Math.random() * possible.length));

    return text;
}

//confirm click custom directive
app.directive('ngConfirmClick', [
  function () {
      return {
          priority: -1,
          restrict: 'A',
          link: function (scope, element, attrs) {
              element.bind('click', function (e) {
                  var message = attrs.ngConfirmClick;
                  if (message && !confirm(message)) {
                      e.stopImmediatePropagation();
                      e.preventDefault();
                  }
              });
          }
      }
  }
]);

app.config(['$routeProvider',
    function ($routeProvider) {
        $routeProvider
        .when('/decks/:deckId', {
            templateUrl: 'views/deck-details.html',
            controller: 'DeckCardsCtrl'
        })
        .when('/decks-new', {
            templateUrl: 'views/deck-edit.html',
            controller: "DeckEditCtrl"
        })
        .when('/decks-edit/:deckId', {
            templateUrl: 'views/deck-edit.html',
            controller: "DeckEditCtrl"
        })
        .when('/cards-new/:deckId', {
            templateUrl: 'views/card-edit.html',
            controller: "CardEditCtrl"
        })
        .when('/cards-edit/:cardId', {
            templateUrl: 'views/card-edit.html',
            controller: "CardEditCtrl"
        })
        .otherwise({
            redirectTo: '/'
        });
    }

]);
