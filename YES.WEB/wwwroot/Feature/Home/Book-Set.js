"use strict";
$("#btnSubmit").click(function (event) {
    event.preventDefault();
    if (!$("#bookYouSetForm").valid()) {
        return false;
    }
    $("#btnSubmit").attr('disabled', true);
    $("#Load").html("<i class='fa fa-spinner fa-spin'></i>Please Wait..")
    $("#Load").addClass('active');
    let contactus = $("#bookYouSetForm").serialize();
    console.log(contactus);
    $.ajax({
        type: "POST",
        url: "/book-set",
        data: contactus,
        dataType: "json",
        success: function (response, status) {
            if (response.success && status === "success") {
                $("#bookYouSetForm").get(0).reset();
                $("#btnSubmit").attr('disabled', false);
                $("#Load").html('Get a Call Back');
                $("#Load").removeClass('active');
                toastr.success('Your set booked Successfully, We will Contact you as soon as possible', 'success Alert', { timeOut: 5000 });
            }
            else {
                $("#btnSubmit").attr('disabled', false);
                $("#Load").html('Get a Call Back');
                $("#Load").removeClass('active');
                toastr.error(response.message, 'Error Alert', { timeOut: 5000 });

            }

        },
        error: function (request, status, error) {
            $("#btnSubmit").attr('disabled', false);
            $("#Load").html('Get a Call Back');
            $("#Load").removeClass('active');
            toastr.success(request.responseText, 'Error Alert', { timeOut: 5000 });
        }
    });
});
