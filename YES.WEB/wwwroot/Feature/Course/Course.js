var baseUrl = window.location.origin;
"use strict";
var Course = {
    Init: () => {
        Course.getAll();
    },
    getAll: () => {
        $.ajax({
            url: baseUrl + "/Course/getAll",
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
                        { data: 'courseId' },
                        { data: 'courseShortName' },
                        { data: 'courseFullName' },
                        { data: 'duration' },
                        {
                            data: 'courseId',
                            "render": function (data, type, row, meta) {
                                return `<div class="tools">
                                                                    <a onclick="Course.getById(${row.courseId})" class="btn btn-primary" title="Edit"><i class="fa fa-edit"></i></a>
                                                                    <a onclick="Course.DeleteRecord(${row.courseId})" class="btn btn-danger" title="Delete"><i class="fa fa-trash-o"></i></a>
                                                                </div>`
                            }
                        },
                    ],
                    columnDefs: [
                        { "orderable": false, "targets": 0 }
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
        var url = baseUrl + "/Course/getById/" + id;
        $('#modal-course').load(url);
        $('#modal-course').modal('show');
    },
    DeleteRecord: (Id) => {
        if (confirm("Do you want to delete this row?")) {
            $.ajax({
                type: "Get",
                url: "../Course/Delete/" + Id,
                success: function (r, status) {
                    if (r.success && status == "success") {
                        toastr.success(r.message, 'success Alert', { timeOut: 2000 });
                        Course.getAll();
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

Course.Init();
$(document).on("click", ".classAdd", function () {
    var rowCount = $('.data-course').length;
    var contactdiv = `<tr class="data-course"> 
            <td><input type="text" name="CourseDtos[${rowCount}].CourseShortName" class="form-control" /></td>
            <td><input type="text" name="CourseDtos[${rowCount}].CourseFullName" class="form-control" /></td>
            <td><input type="text" name="CourseDtos[${rowCount}].Duration" class="form-control" /></td>
            <td><button type="button" id="btnAdd" class="btn btn-xs btn-primary classAdd" onclick="AddDropDownList()"><i class="fa fa-plus"></i></button>&nbsp
            <button type="button" id="btnDelete" class="deleteContact btn btn btn-danger btn-xs"><i class="fa fa-minus"></i></button></td>
            </tr>`

    $('#maintable').append(contactdiv);
});
$(document).on("click", ".deleteContact", function () {
    var rowCount = $('.data-course').length;
    if (rowCount > 1) {
        $(this).closest("tr").remove(); // closest used to remove the respective 'tr' in which I have my controls   
    }
});

var btnSubmitCourseData = () => {

    if (!$("#courseForm").valid())
        return false;
    var formData = $(`#courseForm`).serialize();
    $.ajax({
        type: "POST",
        url: "../Course/CreateCourse",
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



