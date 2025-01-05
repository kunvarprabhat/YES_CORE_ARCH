var baseUrl = window.location.origin;
"use strict";
var ExamList = {
    Init: () => {
        Comman.getAllCourse("Course");
        ExamList.getAll();
    },
    getAll: () => {
        $.ajax({
            url: baseUrl + "/Exam/getAll",
            type: "Get",
            beforeSend: function () {
                $('.page-loader').fadeIn('slow');
            },
            success: function (response) {
                $('.page-loader').fadeOut('slow');
                $('#tblCourseList').DataTable().destroy();
                var table = $('#tblCourseList').DataTable({
                    data: response.data,
                    columns: [
                        { data: 'id' },
                        { data: 'course.courseShortName' },
                        { data: 'course.duration' },
                        { data: 'examName' },
                        { data: 'modules' },
                        {
                            "render": function (data, type, row, meta) {
                                return `<div class="tools">
                                                                    <a onclick="ExamList.getById(${row.id})" class="btn btn-primary" title="Edit"><i class="fa fa-edit"></i></a>
                                                                    <a onclick="ExamList.DeleteRecord(${row.id})" class="btn btn-danger" title="Delete"><i class="fa fa-trash-o"></i></a>
                                                                </div>`
                            }
                        },
                    ],
                    columnDefs: [
                        { "orderable": false, "targets": 0 }
                        //{ targets: [2], orderable: false }
                    ]
                });
            },
            error: function (xhr, status, error) {
                $('.page-loader').fadeOut('slow');
                toastr.error(error, 'Error Alert', { timeOut: 5000 });
            }
        });
    },
    getById: (id) => {

        var url = baseUrl + "/Exam/getById/" + id;
        $('#exam-modal').load(url);
        $('#exam-modal').modal('show');
    },

    DeleteRecord: (Id) => {
        if (confirm("Do you want to delete this row?")) {
            $.ajax({
                type: "Get",
                url: "../Exam/Delete/" + Id,
                success: function (r, status) {
                    if (r.success && status == "success") {
                        toastr.success(r.message, 'success Alert', { timeOut: 2000 });
                        ExamList.getAll();
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
}

ExamList.Init();
$(document).ready(function () {
    var Id, rowCount;
    $(document).on("click", ".classAdd", function () { //
        rowCount = $('.data-exam').length;
        var contactdiv = `<tr class="data-exam">
            <td><select name="examDtos[${rowCount}].CourseId" class="form-control" id="Course${rowCount}">${Comman.getAllCourse(`Course${rowCount}`)}</select></td>
            <td><input type="text" name="examDtos[${rowCount}].ExamName" class="form-control" /></td>
            <td><input type="text" name="examDtos[${rowCount}].Modules" class="form-control" /></td>
            <td><button type="button" id="btnAdd" class="btn btn-xs btn-primary classAdd"><i class="fa fa-plus"></i></button>&nbsp
            <button type="button" id="btnDelete" class="deleteContact btn btn btn-danger btn-xs"><i class="fa fa-minus"></i></button></td>
            </tr>`;
        $('#maintable').append(contactdiv); // Adding these controls to Main table class  
    });
    $(document).on("click", ".deleteContact", function () {
        var rowCount = $('.data-exam').length;
        if (rowCount > 1) {
            $(this).closest("tr").remove(); // closest used to remove the respective 'tr' in which I have my controls   
        }
    });

});


var btnSubmitExamData = () => {

    if (!$("#examForm").valid())
        return false;
    var formData = $(`#examForm`).serialize();
    $.ajax({
        type: "POST",
        url: "../Exam/Create",
        data: formData,
        dataType: "json",
        beforeSend: function () {
            $("#btnSubmit").attr('disabled', false);
        },
        success: function (response) {
            $('.page-loader').fadeOut('slow');
            toastr.success(response.message, 'success Alert', { timeOut: 5000 });
            window.location.reload(true);
        },
        error: function (xhr, status, error) {
            $('.page-loader').fadeOut('slow');
            toastr.error(error, 'Error Alert', { timeOut: 5000 });
        }
    });

}