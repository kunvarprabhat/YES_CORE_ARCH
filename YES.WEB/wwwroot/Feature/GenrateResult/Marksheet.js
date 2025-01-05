var baseUrl = window.location.origin;
"use strict";
updateDate = (id, courseId) => {
    let issueDate = $("#dateOfIssue").val();
    let percentage = $("#txtPercentage").html().trim();
    let grad = $("#txtGrade").html().trim();
    let performance = $("#txtPerformance").html().trim();
    let certificateDto = {
        StudentId: id,
        Percentage: percentage,
        Grad: grad,
        Performance: performance,
        MarksheetIssueDate: issueDate,
        CourseId: courseId
    };

    $.ajax({
        type: "POST",
        url: baseUrl + "/Marksheet/UpdateDate",
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
DownloadMarksheet = (id,stdId, imagePath, resultCount) => {
    let Html = $(`#${id}`).html();
    let signaturePath = $("#branchSignature").val();
    var requestData = {
        Html: Html, 
        StdId: stdId,
        ImagePath: imagePath,
        ResultCount: resultCount,
        SignaturePath: signaturePath
    };
    $.ajax({
        type: "POST",
        url: baseUrl + "/Marksheet/GeneratePDF",
        contentType: 'application/json',
        data: JSON.stringify(requestData),
        beforeSend: function () {
            $("#Load").html("<i class='fa fa-spinner fa-spin'></i>Please Wait..")
            $("#Load").addClass('active');
            $("#btnGenerate").attr('disabled', true);
        },
        success: function (response) {
            $("#btnGenerate").attr('disabled', false);
            if (response.success) {

                $('#generateDiv').hide();
                $('#downloadDiv').show();
                $('#btnDownload').attr('href', "../Marksheet/GeneratePDF?fileName=" + response.data.fileName);

                var link = document.createElement('a');
                link.href = "../Marksheet/GeneratePDF?fileName=" + response.data.fileName;
                link.target = "_blank";
                document.body.appendChild(link);
                link.click();
                document.body.removeChild(link);
                toastr.success(response.message, 'success Alert', { timeOut: 2000 });
                $("#Load").html("<i class='fa-fa-download'></i>&nbsp;download");
                $("#Load").removeClass('active');
                /*  $('#btnDownload').click();*/

            }
            else {
                $('#generateDiv').show();
                $('#downloadDiv').hide();
                $("#Load").html("&nbsp;Generate");
                $("#Load").removeClass('active');
            }

        },
        error: function (request, status, error) {
            $("#btnGenerate").attr('disabled', false);
            toastr.error(request.responseText, 'error Alert', { timeOut: 2000 });
        }
    });
}

marksheetVerify = () => {
    let sId = $("#studentId").val();
    if (sId == undefined || sId == "" || sId == null) {
        toastr.error("Please enter Student Id", 'error Alert', { timeOut: 5000 });
        return false
    }
    $.ajax({
        type: "Get",
        url: baseUrl + "/Student/Verification?RegId=" + sId,
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

function printContent(el) {
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