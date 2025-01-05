var baseUrl = window.location.origin;
"use strict";

$("#btnSubmit").click(function (event) {
    event.preventDefault();
    if (!$("#changePassword").valid()) {
        return false;
    }
    let changePasswordForm = $("#changePassword").serialize();
    Account.changePassword(changePasswordForm);
});
Account = {
    changePassword: (loginForm) => {
        $("#Load").html("<i class='fa fa-spinner fa-spin'></i>Please Wait..")
        $("#Load").addClass('active');

        $.ajax({
            type: "POST",
            url: "../Account/ChangePassword",
            data: loginForm,
            dataType: "json",
            beforeSend: function () {
                $("#btnSubmit").attr('disabled', true);
            },
            success: function (response) {
                $("#btnSubmit").attr('disabled', false);
                $("#Load").html('Change Password');
                $("#Load").removeClass('active');
                if (response.success) {
                    toastr.success(response.message, 'success Alert', { timeOut: 5000 });
                }
                else {
                  
                    toastr.error(response.message, 'Error Alert', { timeOut: 5000 });
                }

            },

            error: function (xhr, status, error) {
                $('.page-loader').fadeOut('slow');
                $("#Load").html('Change Password');
                $("#Load").removeClass('active');
                $("#btnSubmit").attr('disabled', false);
                toastr.error(error, 'Error Alert', { timeOut: 5000 });
            }
        });
    }
}
