var baseUrl = window.location.origin;
"use strict";
updateDate = (id, courseId) => {
    let issueDate = $("#dateOfIssue").val();
    let percentage = $("#txtPercentage").html().trim();
    let grad = $("#txtGrade").html().trim();
    let certificateDto = {
        StudentId: id,
        Percentage: percentage,
        Grad: grad,
        CertificateIssueDate: issueDate,
        CourseId: courseId
    };

    $.ajax({
        type: "POST",
        url: baseUrl + "/Certificate/UpdateDate",
        data: certificateDto,
        dataType: "json",
        success: function (response, status) {
            if (response.success && status === "success") {
                $("#submit").attr('disabled', false);
                toastr.success('Inserted data Successfully', 'success Alert', { timeOut: 5000 });
                window.location.reload(true);
            }
            else {
                $("#submit").attr('disabled', false);
                toastr.error(response.message, 'error Alert', { timeOut: 5000 });
            }
        },
        error: function (request, status, error) {
            $("#submit").attr('disabled', false);
            toastr.error(request.responseText, 'error Alert', { timeOut: 2000 });
        }
    });
}
certificateVerify = () => {
    let sId = $("#studentId").val();
    if (sId == undefined || sId == "" || sId == null) {
        toastr.error("Please enter Student Id", 'error Alert', { timeOut: 5000 });
        return false
    }
    $.ajax({
        type: "Get",
        url: baseUrl + "/Student/Verification?SId=" + sId,
        beforeSend: function () {
            $("#btnSearch").attr('disabled', true);
        },
        success: function (response, status) {
            $("#btnSearch").attr('disabled', false);
            if (response && status === "success") {
                toastr.success('Record fetch successfully', 'success Alert', { timeOut: 5000 });
                $("#Result").empty().html(response);
            }
            else {
                toastr.error(response.message, 'error Alert', { timeOut: 5000 });
            }
        },
        error: function (request, status, error) {
            $("#btnSearch").attr('disabled', false);
            toastr.error(request.responseText, 'error Alert', { timeOut: 2000 });
        }
    });
}
printContent = (el) => {
    var restorepage = document.body.innerHTML;
    var printcontent = document.getElementById(el).innerHTML;
    var htmlToPrint = '' +
        '<style type="text/css">' +
        'table th, table td {' +
        'border:1px solid #000;' +
        'padding;0.5em;' +
        '}' +
        '</style>';
    htmlToPrint += printcontent.outerHTML;

    document.body.innerHTML = printcontent;

    window.print();
    document.body.innerHTML = restorepage;
}
DownloadCertificate = () => {
    let html = $("#printarea").html();
    $.ajax({
        type: "POST",
        url: baseUrl + "/Certificate/DownloadRecipt",
        contentType: 'application/json',
        data: JSON.stringify(html),
        success: function (data) {
            var blob = new Blob([data], { type: 'application/pdf' });

            // Create a temporary URL for the blob
            var url = window.URL.createObjectURL(blob);

            // Create a temporary anchor element to trigger download
            var link = document.createElement('a');
            link.href = url;
            link.download = 'generated-pdf.pdf';

            // Append the link to the body and trigger the download
            document.body.appendChild(link);
            link.click();

        },
        error: function (request, status, error) {
            $("#submit").attr('disabled', false);
            toastr.error(request.responseText, 'error Alert', { timeOut: 2000 });
        }
    });
}