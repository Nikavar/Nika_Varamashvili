// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


let isReader = true;

function showHidePositions() {
	if (isReader)
		$('#positionsDiv').hide();
	else {
		$('#positionsDiv').show();
	}
	isReader = !isReader;
};



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