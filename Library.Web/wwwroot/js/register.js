function showHidePositions() {
	
	var x = document.getElementById("positionsDiv");
	if (x.style.display === "none") {
		x.style.display = "block";
	} else {
		x.style.display = "none";
	}
};

function setDatePickerFormat() {
	var x = document.getElementById("datepicker");
	x.datepicker({ format: "yyyy/mm//dd" });
};

function allowFirstName(elementId) {
	let fname = document.getElementById(elementId).value

	if (isValidName(fname)) {
		document.getElementById("firstNameError").innerHTML = "";
	}

	else {
		document.getElementById("firstNameError").innerHTML = "First Name is not a number";
	}
};

function allowLastName(elementId) {
	let lname = document.getElementById(elementId).value

	if (isValidName(lname)) {
		document.getElementById("lastNameError").innerHTML = "";
	}

	else {
		document.getElementById("lastNameError").innerHTML = "Last Name is not a number";
	}
};

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
	if (!($("#phone").val() >= 0 || $("#phone").val() <= 9)) {
		document.getElementById("phoneError").innerHTML = "Given input is not a number";
	}

	else {
		document.getElementById("phoneError").innerHTML = "";
	}
};

function allowEmail() {
	let regex = new RegExp('[a-z0-9]+@[a-z]+\.[a-z]{2,3}');
	let email = $("#email").val();

	if (!regex.test(email))
		document.getElementById("emailError").innerHTML = "Given input is not a correct email format";

	else
		document.getElementById("emailError").innerHTML = "";
};

function validate_password() {
	let pass = document.getElementById('pass').value;
	let confirm_pass = document.getElementById('confirm_pass').value;

	if (pass == "" && confirm_pass == "") {
			document.getElementById('wrong_pass_alert').innerHTML = "";
	}

	else if (pass == "" && confirm_pass != "") {
			document.getElementById('wrong_pass_alert').style.color = 'red';
			document.getElementById('wrong_pass_alert').innerHTML
				= '☒ input pass above to compare them!';
	}

	else if (pass != "" && confirm_pass == "")
	{
		document.getElementById('wrong_pass_alert').innerHTML = "";
	}

	else if (pass != confirm_pass) {
		document.getElementById('wrong_pass_alert').style.color = 'red';
		document.getElementById('wrong_pass_alert').innerHTML
			= '☒ Use same password';
	}

	else {
		document.getElementById('wrong_pass_alert').style.color = 'green';
		document.getElementById('wrong_pass_alert').innerHTML =
			'🗹 Password Matched';
	}

}
function validate_confirm_password() {
	var pass = document.getElementById('pass').value;
	var confirm_pass = document.getElementById('confirm_pass').value;

	if (confirm_pass == "") {
		document.getElementById('wrong_pass_alert').innerHTML = "";
	}


	else if (pass == "" && confirm_pass != "") {
		document.getElementById('wrong_pass_alert').style.color = 'red';
		document.getElementById('wrong_pass_alert').innerHTML
			= '☒ input pass above to compare them!';
	}

	else if (pass != confirm_pass) {
		document.getElementById('wrong_pass_alert').style.color = 'red';
		document.getElementById('wrong_pass_alert').innerHTML
			= '☒ Use same password';
	}


	else {
		document.getElementById('wrong_pass_alert').style.color = 'green';
		document.getElementById('wrong_pass_alert').innerHTML =
			'🗹 Password Matched';
	}
}

//function fullValidation(){
//	if ($(#firstNameError).val() == "")
//		document.getElementById("firstNameError").innerHTML = "Input First Name";

//	if ($(#lastNameError).val() == "")
//		document.getElementById("lastNameError").innerHTML = "Input Last Name";
//};
