function showHidePositions() {
	
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

