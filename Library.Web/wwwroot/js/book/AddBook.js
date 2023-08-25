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

$(function () {
    $('form').submit(function () {
        //Logo Image
        var selectedFile = $('#imgfile').val();
        if (selectedFile == "") {
            $("imgError") = 'please select cover image for your book';
            return false;
        }
        else {
            var fileExtension = ['jpeg', 'jpg', 'png', 'gif', 'bmp'];
            if ($.inArray($('#imgfile').val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                $("imgError") = "You are permited to select only '\n.jpeg','.jpg', '.png', '.gif', '.bmp' formats are allowed.";
                return false;
            }
        }
    })
});
           