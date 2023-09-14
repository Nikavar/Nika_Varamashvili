$(document).ready(function () {
    $('.search_select_box select').selectpicker();    
});

$("#authors").change(function () {
    var authorList = $('#authors').val() || [];
});

$("#publishers").change(function () {
    var publisherList = $('#publishers').val() || [];
});

$("#languages").change(function () {
    var languageList = $('#languages').val() || [];
});

$("#genres").change(function () {
    var genreList = $('#genres').val() || [];
});

$("#shelves").change(function () {
    var shelfList = $('#shelves').val() || [];
});


$(document).ready(function () {

    var multipleCancelButton = new Choices('#choices-multiple-remove-button', {
        removeItemButton: true,
        maxItemCount: 3,
        searchResultLimit: 5,
        renderChoiceLimit: 5
    });


});

           