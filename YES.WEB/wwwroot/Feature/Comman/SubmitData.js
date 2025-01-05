var _baseUrl = window.location.origin;
"use strict";
submitData = async (url, formId) => {
    return new Promise((resolve, reject) => {
        var form = document.getElementById(formId);
        var formData = new FormData(form);
        var xhr = new XMLHttpRequest();
        xhr.onload = function () {
            if (xhr.status >= 200 && xhr.status < 300) {
                resolve(JSON.parse(xhr.response));
            } else {
                resolve(JSON.parse(xhr.response));
            }
        };
        xhr.onerror = function (request) {
            toastr.error(request.responseText, 'error Alert', { timeOut: 2000 });
            console.error('Request failed');
        };

        // Set up the request
        xhr.open('POST', url, true);

        // Send the FormData object
        xhr.send(formData);
    });
} 