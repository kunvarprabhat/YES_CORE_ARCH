var baseUrl = window.location.origin;
"use strict";
var removeIndex = 0;


getPercentage = () => {
    var mark = $("#HighMark").val();
    var percentage = parseFloat((mark * 100) / 600);
    $("#HighMarks").val(percentage.toFixed(2));
}

var chkeductionvalue = (rowIndex, data) => {
    var checked_checkboxes = $("#chkQualifyingExam input");
    checked_checkboxes.each(function (index, data) {
        if (rowIndex > index || rowIndex == index) {
            if (rowIndex == index && !$(`#chkQualifyingExam_${index}`).is(":checked")) {
                $(`#chkQualifyingExam_${index}`).prop("checked", false);
                generateEductionDetailsHtml(index, data);

            } else {
                $(`#chkQualifyingExam_${index}`).prop("checked", true);

                generateEductionDetailsHtml(index, data);

            }

        }
        else {
            $(`#chkQualifyingExam_${index}`).prop("checked", false);
            generateEductionDetailsHtml(index, data);

        }
    });
}
var generateEductionDetailsHtml = (index, data) => {
    let value = data.value;
    if (data.checked && $("#eductionDetailsId").find("tr").eq(index).attr('id') != value) {
        $("#eductionDetailsId").append(`<tr id=${value}>
                               <td>
                                   <span>${value}</span>
                                   <input type="hidden" name="EducationalDetailDtos[${index}].ExamName" value=${value} />
                                   <span asp-validation-for="EducationalDetailDtos[${index}].ExamName" class="text-danger"></span>
                               <td>
                                   <input id="highSchool" type="text" name="EducationalDetailDtos[${index}].BoardName" />

                                   <span asp-validation-for="EducationalDetailDtos[${index}].BoardName" class="text-danger"></span>
                               </td>
                               <td>
                                   <input id="highYearPass" type="text" name="EducationalDetailDtos[${index}].YearOfPassing" />
                                   <span asp-validation-for="EducationalDetailDtos[${index}].YearOfPassing" class="text-danger"></span>
                               </td>
                               <td>
                                   <input id="highobtMark" type="text" name="EducationalDetailDtos[${index}].ObtainedMarks" />
                                   <span asp-validation-for="EducationalDetailDtos[${index}].ObtainedMarks" class="text-danger"></span>

                               </td>
                                <td>
                                   <input id="highobtMark" type="text" name="EducationalDetailDtos[${index}].TotalMarks" />
                                   <span asp-validation-for="EducationalDetailDtos[${index}].TotalMarks" class="text-danger"></span>

                               </td>
                               <td>
                                   <input id="highPercentage" type="text" name="EducationalDetailDtos[${index}].Percentage" />
                                   <span asp-validation-for="EducationalDetailDtos[${index}].Percentage" class="text-danger"></span>
                               </td>

                            /tr>`)
    }

    if (!data.checked) {
        $(`#${data.value}`).remove();
    }

}

function getRegistration(_this) {
    let regId = _this.value;

    if (regId != 0 && regId != '') {
        $.ajax({
            url: baseUrl + "/Student/GetRegistrationById/" + regId,
            type: "Get",
            beforeSend: function () {
                $('#wait').fadeIn('slow');
            },
            success: function (response, status) {
                $('#wait').fadeOut('slow');
                if (response && status === "success") {
                    $("#RegistrationDto_Name").val(response.name);
                    $("#RegistrationDto_Dob").val(response.dob);
                    $("#RegistrationDto_FatherName").val(response.fatherName);
                    $("#RegistrationDto_MotherName").val(response.motherName);
                    $("#RegistrationDto_Gender").val(response.gender);
                }
                else {
                    toastr.error("Opps!", 'Error Alert', { timeOut: 5000 });
                }

            },

            error: function (xhr, status, error) {
                $('.page-loader').fadeOut('slow');
                toastr.error(error, 'Error Alert', { timeOut: 5000 });
            }
        });
    }

}
$("#btnSubmit").click(function (event) {
    event.preventDefault();
    if (!$("#applicationForm").valid()) {
        return false;
    }
    let regitration = $("#applicationForm").serialize();
    $.ajax({
        type: "POST",
        url: baseUrl + "/Student/Create",
        data: regitration,
        dataType: "json",
        beforeSend: function () {
            $("#btnSubmit").attr('disabled', false);
        },
        success: function (response, status) {
            if (status === "success") {
                $("#btnSubmit").attr('disabled', false);
                $("#Load").removeClass('active');
                toastr.success('Your Registration Submitted Successfully', 'success Alert', { timeOut: 5000 });
                window.location.href = baseUrl + '/Student/Index';
            }
            else {
                toastr.error('Opps, Something worng ', 'Error Alert', { timeOut: 5000 });
            }

        },
        error: function (request, status, error) {
            $("#btnSubmit").attr('disabled', false);
            toastr.success(request.responseText, 'Error Alert', { timeOut: 5000 });
        }
    })
});