var baseUrl = window.location.origin;
"use strict";
var TopMessage = {

}

TopMessage.Init();


$("#btnSubmit").click(function (event) {
    event.preventDefault();
    let offterText = $("#txtOfferText").val();
    if (offterText == "" || offterText == undefined) {
        toastr.error('Offer/message is required field!', 'Error Alert', { timeOut: 5000 });
        return false
    }
    let topMessage = $("#topMessageForm").serialize();
    $.ajax({
        type: "POST",
        url: baseUrl + "/TopMessage/Create",
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
                window.location.reload(true);
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