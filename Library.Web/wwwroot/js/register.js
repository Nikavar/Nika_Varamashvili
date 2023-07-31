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

//function phonenumber(inputtxt) {
//	var phoneno = /^\+?([0-9]{2})\)?[-. ]?([0-9]{4})[-. ]?([0-9]{4})$/;
//	if ((inputtxt.value.match(phoneno))
//        {
//		return true;
//	}
//	else {
//		alert("message");
//		return false;
//	}
//}