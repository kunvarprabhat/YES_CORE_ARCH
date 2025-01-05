"use strict";
$("#btnSubmit").click(function (event) {
    event.preventDefault();
    if (!$("#syllabusForm").valid()) {
        return false;
    }
    submitData('../Syllabus/Create', 'syllabusForm')
        .then((responseData) => {
            if (responseData.success) {
                $("#syllabusForm")[0].reset();
                $("#btnSubmit").attr('disabled', false);
                $("#Load").removeClass('active');
                toastr.success(responseData.message, 'success Alert', { timeOut: 5000 });
                window.location.href = "../Syllabus/Index";
            }
            else {
                toastr.error(responseData.message, 'error Alert', { timeOut: 5000 });
            }
        })
        .catch((error) => {
            // Handle errors here
            toastr.error(error, 'error Alert', { timeOut: 5000 }); 
        });

});