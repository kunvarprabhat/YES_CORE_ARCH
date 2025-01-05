"use strict";
$("#btnSubmit").click(function () {
    var Name = $("#txtName").val();
    var Mobile = $("#txtMobile").val();
    var Email = $("#txtEmail").val();
    var Message = $("#txtMessage").val();
    if (Name == "") {
        toastr.error('Please Fill Name*, Mandatory!!', 'Error Alert', { timeOut: 5000 });
        return false;
    }
    else if (Mobile == "") {
        toastr.error('Please Fill Mobile*, Mandatory!!', 'Error Alert', { timeOut: 5000 });
        return false;
    } else if (Email == "") {
        toastr.error('Please Fill Email*, Mandatory!!', 'Error Alert', { timeOut: 5000 });
        return false;
    } else if (Message == "") {
        toastr.error('Please Fill Message*, Mandatory', 'Error Alert', { timeOut: 5000 });
        return false;
    } else {
        $("#btnSubmit").attr('disabled', true);
        $("#Load").html("<i class='fa fa-spinner fa-spin'></i>Please Wait..")
        $("#Load").addClass('active');
        let contactus = $("#ContactUsForm").serialize();
        console.log(contactus);
        $.ajax({
            type: "POST",
            url: "/contact",
            data: contactus,
            dataType: "json",
            success: function (response, status) {
                if (status === "success") {
                    $("#txtName").val("");
                    $("#txtMobile").val("");
                    $("#txtEmail").val("");
                    $("#txtMessage").val("");
                    $("#btnSubmit").attr('disabled', false);
                    $("#Load").html('Get a Call Back');
                    $("#Load").removeClass('active');
                    toastr.success('Your Enquiry Submitted Successfully, We will Contact you as soon as possible', 'success Alert', { timeOut: 5000 });
                }
                else {
                    toastr.error('Opps, Something worng ', 'Error Alert', { timeOut: 5000 });

                }

            },
            error: function (request, status, error) {
                $("#btnSubmit").attr('disabled', false);
                $("#Load").html('Get a Call Back');
                $("#Load").removeClass('active');
                toastr.success(request.responseText, 'Error Alert', { timeOut: 5000 });
            }
        });
    }

});