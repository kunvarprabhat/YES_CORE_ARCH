var baseUrl = window.location.origin;
"use strict";
BranchList = {
    Init: () => {
        BranchList.getAll();
    },
    getAll: () => {
        $.ajax({
            url: baseUrl + "/Branch/getAll",
            type: "Get",
            beforeSend: function () {
                $('.page-loader').fadeIn('slow');
            },
            success: function (response) {
                $('.page-loader').fadeOut('slow');
                $('#tblBranchList').DataTable().destroy();
                var table = $('#tblBranchList').DataTable({
                    data: response,
                    columns: [
                        { data: 'id' },
                        {
                            data: null,
                            "render": function (data, type, row, meta) {
                                return ` <a href="../Branch/BranchDetails/${row.id}" class="btn btn-primary" title="Details">${row.centerId}</a>`


                            }
                        }, 
                        { data: 'name' },
                        { data: 'centerName' },
                        { data: 'phoneNo' },
                        { data: 'emailId' },
                        { data: 'address' },
                        {
                            data: null,
                            "render": function (data, type, row, meta) {
                                return `<div class="tools">
                                                                    <a href="../Branch/Create?id=${row.id}" class="btn btn-primary" title="Edit"><i class="fa fa-edit"></i></a>
                                                                    <a onclick="BranchList.DeleteRecord(${row.id})" class="btn btn-danger" title="Delete"><i class="fa fa-trash-o"></i></a>
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
                url: "../Branch/Delete/" + Id,
                success: function (r, status) {
                    if (r.success && status == "success") {
                        toastr.success(r.message, 'success Alert', { timeOut: 2000 });
                        News.getAll();
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

BranchList.Init();