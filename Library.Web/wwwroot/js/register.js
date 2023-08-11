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

//function fullValidation(){
//	if ($(#firstNameError).val() == "")
//		document.getElementById("firstNameError").innerHTML = "Input First Name";

//	if ($(#lastNameError).val() == "")
//		document.getElementById("lastNameError").innerHTML = "Input Last Name";
//};
