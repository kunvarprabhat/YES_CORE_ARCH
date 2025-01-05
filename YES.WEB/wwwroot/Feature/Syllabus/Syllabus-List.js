var baseUrl = window.location.origin;
"use strict";
SyllabusList = {
    Init: () => {
        SyllabusList.getAll();
    },
    getAll: () => {
        $.ajax({
            url: baseUrl + "/Syllabus/getAll",
            type: "Get",
            beforeSend: function () {
                $('.page-loader').fadeIn('slow');
            },
            success: function (response) {
                $('.page-loader').fadeOut('slow');
                $('#tblSyllabusList').DataTable().destroy();
                var table = $('#tblSyllabusList').DataTable({
                    data: response.data,
                    columns: [
                        { data: 'id' },
                        { data: 'heading' },
                        { data: 'course.courseShortName' },
                        { data: 'description' },
                        { data: 'createdDate' },
                        {
                            data: 'id',
                            "render": function (data, type, row, meta) {
                                return `<div class="tools">
                                                                    <a href="../Syllabus/Create?id=${row.id}" class="btn btn-primary" title="Edit"><i class="fa fa-edit"></i></a>
                                                                    <a onclick="SyllabusList.DeleteRecord(${row.id})" class="btn btn-danger" title="Delete"><i class="fa fa-trash-o"></i></a>
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
    DeleteRecord: (Id) => {
        if (confirm("Do you want to delete this row?")) {
            $.ajax({
                type: "Get",
                url: "../Syllabus/Delete/" + Id,
                success: function (r, status) {
                    if (r.success && status == "success") {
                        toastr.success(r.message, 'success Alert', { timeOut: 2000 });
                        SyllabusList.getAll();
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

SyllabusList.Init();

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