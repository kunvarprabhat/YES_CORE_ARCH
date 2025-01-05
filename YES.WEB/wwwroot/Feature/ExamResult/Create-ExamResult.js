var baseUrl = window.location.origin;
"use strict";
var examCount = 0;
$(function () {
    toggleButtonOfMarksheet();
})
toggleButtonOfMarksheet = () => {
    examCount = $("#examModulesCount").val();
    let resultCount = $("#resultCount").val();
    if (resultCount == undefined || resultCount == 0 || examCount > resultCount) {
        $("#genCertificate").hide();
        $("#genMarksheet").hide();
    } else if (examCount >= $("#resultCount").val()) {
        $("#genCertificate").show();
        $("#genMarksheet").show();
    }
}
$(document).on("click", "#submit", function (event) {

    event.preventDefault();
    if (!$("#resultForm").valid()) {
        return false;
    }
    let examResultId = $("#examResultId").val();
    //if ((examResultId == ''|| examResultId == undefined) && examCount <= $("#resultCount").val()) {
    //    toastr.error('Result not allow more. All exam result done!', 'Error Alert', { timeOut: 2000 });
    //    return false;
    //}
    let examResult = $("#resultForm").serialize();
    $.ajax({
        type: "POST",
        url: "../ExamResult/Create",
        data: examResult,
        dataType: "json",
        success: function (response, status) {
            if (response.success && status === "success") {
                $("#submit").attr('disabled', false);
                toastr.success(response.message, 'success Alert', { timeOut: 2000 });
                $("#resultForm").get(0).reset();
                LoadExamResult();
                $('a[href="#timeline"]').click();
                $('a[href="#timeline"]').parent().addClass('active');
            }
            else {
                $("#submit").attr('disabled', false);
                toastr.error(response.message, 'error Alert', { timeOut: 2000 });
            }
        },
        error: function (request, status, error) {
            $("#submit").attr('disabled', false);
            toastr.error(request.responseText, 'error Alert', { timeOut: 2000 });
        }
    });
});
function LoadExamResult() {

    let studentId = $("#StudentId").val();
    $.ajax({
        type: "get",
        url: baseUrl + "/ExamResult/GetAllByStudentId?Id=" + studentId,
        success: function (response) {
            $("#ExamResult").empty().html(response);
            toggleButtonOfMarksheet();
        },
        error: function (request, status, error) {
            toastr.error('Opps,' + request.responseText, 'Error Alert', { timeOut: 5000 });
            console.log(request);
        }
    });
}

editExam = (id, courseId) => {
    $("#examEntryDetails").empty();
    var url = baseUrl + `/ExamResult/getById?id=${id}&CourseId=${courseId}`;
    $('#examEntryDetails').load(url);
    $("#examResultId").val(id);
    $('a[href="#settings"]').click();
    $('a[href="#settings"]').parent().addClass('active');
    $("#submit").text("Update");
}

function DeleteExam(Id) {
    if (confirm("Do you want to delete this row?")) {
        $.ajax({
            type: "Get",
            url: "../ExamResult/Delete/" + Id,
            success: function (r, status) {
                if (r.success && status == "success") {
                    toastr.success(r.message, 'success Alert', { timeOut: 2000 });
                    LoadExamResult();
                }
                else {
                    toastr.error(r.message, 'Error Alert', { timeOut: 3000 });
                }
            },
            error: function (request, status, error) {
                toastr.error(request.responseText, 'Error Alert', { timeOut: 3000 });
            }
        });
    }
}

$(document).ready(function () {
    // Handling change event of the dropdown
    $("#examDropdown").change(function () {
        // Get the selected option
        var selectedOption = $(this).find("option:selected");
        $("#Tearm").val(selectedOption.data("modules"));
    });
});