"use strict";
$("#btnSubmit").click(function (event) {
    event.preventDefault();
    if (!$("#registrationForm").valid()) {
        return false;
    }
    let Dob = $("#ddlday").val() + "/" + $("#ddlmonth").val() + "/" + $("#ddlyear").val();
    $("#txtDob").val(Dob);
    $("#RegistrationNo").val('0');
    let regitration = $("#registrationForm").serialize();
    $.ajax({
        type: "POST",
        url: "../Registration/Registration",
        data: regitration,
        dataType: "json",
        beforeSend: function () {
            $("#btnSubmit").prop('disabled', 'true');
        },
        success: function (response, status) {
            if (status === "success") {
                $("#registrationForm")[0].reset();
                $("#btnSubmit").prop('disabled', '');
                $("#Load").removeClass('active');
                toastr.success(response.message, 'success Alert', { timeOut: 5000 });
                if (window.location.href.indexOf("regitration-form") == -1 && response.data != undefined) {
                    window.location.href = "../Student/Application?RId=" + response.data.registrationNo;
                }
                else {
                    window.location.reload(true);
                }

            }
            else {
                toastr.error('Opps, Something worng ', 'Error Alert', { timeOut: 5000 });
            }

        },
        error: function (request, status, error) {
            $("#btnSubmit").prop('disabled', '');
            toastr.error(request.responseText, 'Error Alert', { timeOut: 5000 });
        }
    })
});