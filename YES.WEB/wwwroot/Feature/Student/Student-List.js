var baseUrl = window.location.origin;
"use strict";
StudentList = {
    Init: () => {
        StudentList.getAll();
    },
    getAll: () => {
        $.ajax({
            url: baseUrl + "/Student/getAll",
            type: "Get",
            beforeSend: function () {
                $('.page-loader').fadeIn('slow');
            },
            success: function (response) {
                $('.page-loader').fadeOut('slow');
                $('#tblStudentList').DataTable().destroy();
                var table = $('#tblStudentList').DataTable({
                    data: response,
                    columns: [
                        { data: 'id' },
                        {
                            data: null,
                            "render": function (data, type, row, meta) {
                                return ` <a href="../Student/StudentProfile?id=${row.id}" title="Student-Profile">${row.studentId}</a>`
                            }
                        },
                        { data: 'registrationDto.registrationNo' },
                        { data: 'registrationDto.name' },
                        { data: 'registrationDto.gender' },
                        { data: 'registrationDto.mobileNo' },
                        { data: 'registrationDto.emailId' },
                        { data: 'registrationDto.address' },
                        {
                            data: null,
                            "render": function (data, type, row, meta) {
                                return `<div class="tools">
                                                                    <a href="../Student/Application?id=${row.id}" class="btn btn-primary" title="Edit"><i class="fa fa-edit"></i></a>
                                                                    <a class="btn btn-danger" title="Delete"><i class="fa fa-trash-o"></i></a>
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
    }
}

StudentList.Init();