function allowEmail() {
	let regex = new RegExp('[a-z0-9]+@[a-z]+\.[a-z]{2,3}');
	let email = $("#email").val();

	if (!regex.test(email))
		document.getElementById("emailError").innerHTML = "Given input is not a correct email format";

	else
		document.getElementById("emailError").innerHTML = "";
};

function validate_login() {
	let email = document.getElementById('email').value;
	let pass = document.getElementById('pass').value;

	if (email == "" && pass == "") {
		document.getElementById('emailError').innerHTML = "input email";
		document.getElementById('passError').innerHTML = "input password";
	}
}