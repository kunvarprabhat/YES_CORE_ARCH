var baseUrl = window.location.origin;
"use strict";
$("#btnSubmit").click(function (event) {
    event.preventDefault();
    if (!$("#newsForm").valid()) {
        return false;
    }
    let topMessage = $("#newsForm").serialize();
    $.ajax({
        type: "POST",
        url: baseUrl + "/News/Create",
        data: topMessage,
        dataType: "json",

        beforeSend: function () {
            $("#btnSubmit").attr('disabled', false);
        },
        success: function (response, status) {
            if (status === "success") {
                $("#btnSubmit").attr('disabled', false);
                $("#Load").removeClass('active');
                toastr.success(response.message, 'success Alert', { timeOut: 5000 });
                window.location.href = "../News/Index";
            }
            else {
                toastr.error('Opps, ' + response.message, 'Error Alert', { timeOut: 5000 });
            }

        },
        error: function (request, status, error) {
            $("#btnSubmit").attr('disabled', false);
            toastr.success(request.responseText, 'Error Alert', { timeOut: 5000 });
        }
    })
}); 