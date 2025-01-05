var baseUrl = window.location.origin;
"use strict";
RegistrationList = {
    Init: () => {
        RegistrationList.getAll();
    },
    getAll: () => {
        $.ajax({
            url: baseUrl + "/Registration/getAll",
            type: "Get",
            beforeSend: function () {
                $('.page-loader').fadeIn('slow');
            },
            success: function (response) {
                $('.page-loader').fadeOut('slow');
                $('#tblRegistrationList').DataTable().destroy();
                var table = $('#tblRegistrationList').DataTable({
                    data: response,
                    columns: [
                        {
                            data: 'id',
                            "render": function (data, type, row, meta) {
                                return `${meta.row + 1}`


                            }
                        },
                        {
                            data: null,
                            "render": function (data, type, row, meta) {
                                return ` <a href="../Student/Application?RId=${row.registrationNo}" class="btn btn-primary" title="Application">${row.registrationNo}</a>`


                            }
                        },
                        { data: 'name' },
                        { data: 'gender' },
                        { data: 'mobileNo' },
                        { data: 'emailId' },
                        { data: 'address' },
                        {
                            data: null,
                            "render": function (data, type, row, meta) {
                                return `<div class="tools">
                                                                    <a href="../Registration/Registration?id=${row.id}" class="btn btn-primary" title="Edit"><i class="fa fa-edit"></i></a>
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

RegistrationList.Init();