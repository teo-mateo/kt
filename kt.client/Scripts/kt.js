var x = 12;

var api_root = "http://localhost:63611/";
var api_decks = api_root + "api/Decks";

$.getJSON(api_decks, function (data) {

    var c = data;

    // Now use this data to update your view models, 
    // and Knockout will update your UI automatically 
})