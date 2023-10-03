function UpdateProfile() {
	var fname = $("#firstName").Val();
	var lname = $("#lastName").Val();

	$("#btnUpdate").click(function (e) {
		alert("button click");
		e.preventDefault();

		var model = @Html.Raw(Json.Encode(Model))
		$.ajax({
			type: 'post',
			url: '@Url.Action("Send", "Home")',
			data: JSON.stringify({ contact: model }),
			contentType: 'application/json; charset=utf-8',
			dataType: "json",
			success: function (data) {
				alert(data);
			}
		});
	});
});