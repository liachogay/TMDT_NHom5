$().ready(function () {
	$.validator.addMethod("maxDate", function (value) {
		curDate = new Date('01/01/2010')
		var inputDate = new Date(value);
		if (inputDate < curDate)
			return true;
		return false;
	}, "Ngày nhập không hợp lệ");
	$("#register-form").validate({
		rules: {
			"customer.Name": {
				required: true,
				minlength: 10,
				maxlength: 50
			},
			"customer.Address": {
				required: true,
				minlength: 15,
				maxlength: 150
			},
			"customer.Phone": {
				required: true,
				number: true,
				minlength: 10,
				maxlength: 11
			},
			"customer.Date": {
				required: true,
				date: true,
				maxDate:true
			},
			"account.UserName": {
				required: true,
				minlength: 10,
			},
			"account.PassWord": {
				required: true,
				minlength: 5,
			},
			"account.PasswordConfirm": {
				required: true,
				equalTo: "#account_PassWord",
				minlength: 5
			}
		},
		messages: {
			"customer.Name": {
				required: "* Bắt buộc nhập Tên Họ",
				minlength:"* Tối thiểu 10 ký tự",
				maxlength: "* Tối đa chỉ 50 ký tự"
			},
			"customer.Address": {
				required: "* Bắt buộc nhập Địa chỉ",
				minlength: "* Tối Thiểu 15 ký tự",
				maxlength: "* Tối đa 50 ký tự"
			},
			"customer.Phone": {
				required: "* Bắt buộc nhập SDT ",
				minlength: "* Tối Thiểu 10 số",
				maxlength: "* Tối đa chỉ 11 ký tự"
			},
			"customer.Date": {
				required: "* Bắt buộc nhập Ngày Sinh ",
			},
			"account.UserName": {
				required: "* Vui lòng nhập địa chỉ Email",
				minlength: "* Tối Thiểu 10 ký tự",
			},
			"account.PassWord": {
				required: "* Vui lòng nhập Password",
				minlength: "* Tối Thiểu 5 ký tự",
			},
			"account.PasswordConfirm": {
				required: "* Vui lòng nhập Password",
				equalTo: "* Hai password phải giống nhau",
				minlength: "* Tối Thiểu 5 ký tự",
			}
		}
	});
})