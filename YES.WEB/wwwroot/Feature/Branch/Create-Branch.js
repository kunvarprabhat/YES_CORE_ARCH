"use strict";
$("#btnSubmit").click(function (event) {
    event.preventDefault();
    if (!$("#branchForm").valid()) {
        return false;
    }
    $("#btnSubmit").attr('disabled', true);
    submitData('../Branch/Create', 'branchForm')
        .then((responseData) => {
            if (responseData.success) {
                $("#branchForm")[0].reset();
                $("#btnSubmit").attr('disabled', false);
                $("#Load").removeClass('active');
                toastr.success(responseData.message, 'success Alert', { timeOut: 5000 });
                window.location.href = "../Branch/Index";
            }
            else {
                $("#btnSubmit").attr('disabled', false);
                toastr.error(responseData.message, 'error Alert', { timeOut: 5000 });
            }
        })
        .catch((error) => {
            $("#btnSubmit").attr('disabled', false);
            // Handle errors here
            console.error(error);
        });

});