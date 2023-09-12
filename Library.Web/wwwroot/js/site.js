//document.getElementById('register-form').addEventListener('submit', function (evt) {
//    evt.preventDefault();
//    const firstname = document.getElementById('first_name');
//    const lastname = document.getElementById('last_name');


//    if (firstname === '') {
//        showError(firstname, 'FirstName is required');
//    }

//    if (lastname === '') {
//        showError(lastname, 'LastName is required');
//    }
//})


//const form = document.getElementById('register-form');
//const firstname = document.getElementById('first_name');
//const lastname = document.getElementById('last_name');

//function showError(input, message) {
//    const formGroup = input.parentElement;
//    formGroup.className = 'form-control error';
//    const small = formGroup.querySelector('small');
//    small.innerText = message
//};

//if (firstname === null) {
//   // showError(firstname, 'FirstName is required');

//    const formGroup = input.parentElement;
//    formGroup.className = 'form-control error';
//    const small = formGroup.querySelector('small');
//    small.innerText = message


//    }

//form.addEventListener('submit', function(e) {
//    e.preventDefault();

//});

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