﻿function showHidePositions() {
	
	var x = document.getElementById("positionsDiv");

	if (x.style.display === "none") {
		x.style.display = "block";
	} else {
		x.style.display = "none";
	}
};

function allowPosition() {
	var isChecked = $("input[id=staff]:checked").val();	
	var selectedPosition = $('#positionsDiv :selected').text();

	if (selectedPosition.toLowerCase().includes('--')) {
		$("#positionError").html("U have to choose a position");
	}

	else {
		$("#positionError").html("");
	}
}

function setDatePickerFormat() {
	var x = document.getElementById("datepicker");
	x.datepicker({ format: "yyyy/mm//dd" });
};

//function allowFirstName() {
//	let fname = $("#firstName").val();

//	if (isValidName(fname)) {
//		$("#firstNameError").html("");
//	}

//	else {
//		var errorMsg = "First Name is not a number";
//		$("#firstNameError").html(errorMsg); 
//	}
//};

//function allowLastName() {
//	let lname = $("#lastName").val();

//	if (isValidName(lname)) {
//		$("#lastNameError").html("");
//	}

//	else {
//		$("#lastNameError").html("Last Name is not a number");
//	}
//};

function isValidName(name) {
	if (
		typeof name !== "string" ||
		/[0-9]+/g.test(name)
	) {
		return false;
	}
	return true;
};

function allowNumbers() {
	const pattern = new RegExp('^(\\d|\\+)[ \\d-]+$');
	let phoneNumber = $("#phone").val();
	let phoneError = document.getElementById("phoneError");

	if (!pattern.test(phoneNumber) && phoneNumber != "")
		phoneError.innerHTML = "Given input has not a correct format for phone number ";

	else
		phoneError.innerHTML = "";
};


function allowEmail() {
	let regex = new RegExp('[a-z0-9]+@[a-z]+\.[a-z]{2,3}');
	let email = $("#email").val();

	if (!regex.test(email))
		document.getElementById("emailError").innerHTML = "Given input has not a correct format for email format";

	else
		document.getElementById("emailError").innerHTML = "";
};

//function allowPersonalId() {
//	if ($("#personalId").val() == "")
//		document.getElementById("personalIdError").innerHTML = "";

//	else if (!($("#personalId").val() >= 0 || $("#personalId").val() <= 9)) {
//		document.getElementById("personalIdError").innerHTML = "Given input must be the digits";
//	}

//	else if ($("#personalId").val().length != 11) {
//		document.getElementById("personalIdError").innerHTML = "personal id must contain 11 digits";
//	}

//	else {
//		document.getElementById("personalIdError").innerHTML = "";
//	}
//};



//function validate_password() {
//	let pass = document.getElementById('pass').value;
//	let confirm_pass = document.getElementById('confirm_pass').value;

//	if (pass == "" && confirm_pass == "") {
//			document.getElementById('wrong_pass_alert').innerHTML = "";
//	}

//	else if (pass == "" && confirm_pass != "") {
//			document.getElementById('wrong_pass_alert').style.color = 'red';
//			document.getElementById('wrong_pass_alert').innerHTML
//				= '☒ input pass above to compare them!';
//	}

//	else if (pass != "" && confirm_pass == "")
//	{
//		document.getElementById('wrong_pass_alert').innerHTML = "";
//	}

//	else if (pass != confirm_pass) {
//		document.getElementById('wrong_pass_alert').style.color = 'red';
//		document.getElementById('wrong_pass_alert').innerHTML
//			= '☒ Use same password';
//	}

//	else {
//		document.getElementById('wrong_pass_alert').style.color = 'green';
//		document.getElementById('wrong_pass_alert').innerHTML =
//			'🗹 Password Matched';
//	}

//}
//function validate_confirm_password() {
//	var pass = document.getElementById('pass').value;
//	var confirm_pass = document.getElementById('confirm_pass').value;

//	if (confirm_pass == "") {
//		document.getElementById('wrong_pass_alert').innerHTML = "";
//	}


//	else if (pass == "" && confirm_pass != "") {
//		document.getElementById('wrong_pass_alert').style.color = 'red';
//		document.getElementById('wrong_pass_alert').innerHTML
//			= '☒ input pass above to compare them!';
//	}

//	else if (pass != confirm_pass) {
//		document.getElementById('wrong_pass_alert').style.color = 'red';
//		document.getElementById('wrong_pass_alert').innerHTML
//			= '☒ Use same password';
//	}


//	else {
//		document.getElementById('wrong_pass_alert').style.color = 'green';
//		document.getElementById('wrong_pass_alert').innerHTML =
//			'🗹 Password Matched';
//	}
//}


