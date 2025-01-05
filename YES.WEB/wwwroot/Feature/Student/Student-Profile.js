var baseUrl = window.location.origin;
"use strict";
function btnUpload() {
    submitData('../Student/UploadImage', 'userProfileForm')
        .then((responseData) => {
            if (responseData.success) {
                toastr.success(responseData.message, 'success Alert', { timeOut: 5000 });
                window.location.reload(true);
            }
            else {
                toastr.error(responseData.message, 'error Alert', { timeOut: 5000 });
                window.location.reload(true);
            }
        })
        .catch((error) => {
            toastr.error(error, 'error Alert', { timeOut: 5000 });
            console.error(error);
        });
}