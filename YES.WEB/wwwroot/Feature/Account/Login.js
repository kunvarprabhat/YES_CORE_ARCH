var baseUrl = window.location.origin;
"use strict";

$("#btnSubmit").click(function (event) {
    event.preventDefault();
    if (!$("#loginForm").valid()) {
        return false;
    }
    let loginForm = $("#loginForm").serialize();
    Login.userLogin(loginForm);
});
Login = {
    userLogin: (loginForm) => {
        $("#Load").html("<i class='fa fa-spinner fa-spin'></i>Please Wait..")
        $("#Load").addClass('active');

        $.ajax({
            type: "POST",
            url: "../Account/Login",
            data: loginForm,
            dataType: "json",
            beforeSend: function () {
                $("#btnSubmit").attr('disabled', true);
            },
            success: function (response) {
                $("#Load").html('Login');
                $("#Load").removeClass('active');
                $("#btnSubmit").attr('disabled', false);
                $('.page-loader').fadeOut('slow');
                if (response.success) {
                    toastr.success(response.message, 'success Alert', { timeOut: 5000 });
                    window.location.href = "../Dashboard/Index";
                }
                else {

                    toastr.error(response.message, 'Error Alert', { timeOut: 5000 });
                }

            },

            error: function (xhr, status, error) {
                $('.page-loader').fadeOut('slow');
                $("#Load").html('Login');
                $("#Load").removeClass('active');
                $("#btnSubmit").attr('disabled', false);
                toastr.error(error, 'Error Alert', { timeOut: 5000 });
            }
        });
    },

    backToLoginPage: () => {
        $("#authDiv").show();
        $("#forgotDiv").hide();
    },
    goToForgotPage: () => {
        $("#authDiv").hide();
        $("#forgotDiv").show();
    }
}
