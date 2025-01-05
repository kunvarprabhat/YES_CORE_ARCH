var baseUrl = window.location.origin;
"use strict";
var News = {
    Init: () => {
        News.getAll();
    },
    getAll: () => {
        $.ajax({
            url: baseUrl + "/News/getAll",
            type: "Get",
            beforeSend: function () {
                $('.page-loader').fadeIn('slow');
            },
            success: function (response) {
                $('.page-loader').fadeOut('slow');
                $('#tblNewsList').DataTable().destroy();
                var table = $('#tblNewsList').DataTable({
                    data: response.data,
                    columns: [
                        { data: 'id' },
                        { data: 'title' },
                        { data: 'details' },
                        {
                            "render": function (data, type, row, meta) {
                                return `<div class="tools">
                                                                    <a href="../News/Create?id=${row.id}" class="btn btn-primary" title="Edit"><i class="fa fa-edit"></i></a>
                                                                    <a onclick="News.DeleteRecord(${row.id})" class="btn btn-danger" title="Delete"><i class="fa fa-trash-o"></i></a>
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
    DeleteRecord: (Id) => {
        if (confirm("Do you want to delete this row?")) {
            $.ajax({
                type: "Get",
                url: "../News/Delete/" + Id,
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

News.Init();
