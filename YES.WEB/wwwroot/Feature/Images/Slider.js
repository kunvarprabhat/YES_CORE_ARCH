var baseUrl = window.location.origin;
"use strict";

GalleryImage = {
    Init: () => {
        GalleryImage.getAll();
    },
    getAll: () => {
        $.ajax({
            url: baseUrl + "/Gallery/getAll",
            type: "Get",
            beforeSend: function () {
                $('.page-loader').fadeIn('slow');
            },
            success: function (response) {
                $('.page-loader').fadeOut('slow');
                $('#tblImagesList').DataTable().destroy();
                var table = $('#tblImagesList').DataTable({
                    data: response,
                    columns: [ 
                        {
                            data: null,
                            "render": function (data, type, row, meta) {
                                return meta.row + 1
                            }
                        },
                        { data: 'title' },
                        { data: 'description' },
                        { data: 'imageName' },
                        { data: 'imagePath' },
                        { data: 'createdDate' },
                        {
                            data: null,
                            "render": function (data, type, row, meta) {
                                return `<div class="tools">
                                                                    <a onclick="Batch.getById(${row.id})" class="btn btn-primary" title="Edit"><i class="fa fa-edit"></i></a>
                                                                    <a onclick="GalleryImage.DeleteRecord(${row.id})" class="btn btn-danger" title="Delete"><i class="fa fa-trash-o"></i></a>
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
        $.ajax({
            url: baseUrl + `/Gallery/GetById?id=${id}`,
            type: "Get",
            beforeSend: function () {
                $('.page-loader').fadeIn('slow');
            },
            success: function (response) {
                if (response && response.data) {
                    $("#CourseId").val(response.data.courseId);
                    $("#Size").val(response.data.size);
                    $("#UpcomingBatch").val(response.data.upcomingBatch);
                    toastr.success(response.message, 'success Alert', { timeOut: 5000 });
                    $(".inter-box .box").removeClass('collapsed-box');
                    $(".box-body").show();
                    $("#btnSubmit").text("Update");
                }
                else {
                    toastr.error(response.message, 'Error Alert', { timeOut: 5000 });
                }

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
                url: "../Gallery/Delete/" + Id,
                success: function (r, status) {
                    if (r.success && status == "success") {
                        toastr.success(r.message, 'success Alert', { timeOut: 2000 });
                        GalleryImage.getAll();
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

GalleryImage.Init();
$("#btnSubmit").click(function (event) {
    event.preventDefault();
    if (!$("#gallerImageForm").valid()) {
        return false;
    }
    let imageType = $("#ImageType").val();
    submitData('../Gallery/Create', 'gallerImageForm')
        .then((responseData) => {
            if (responseData.success) {
                $("#gallerImageForm")[0].reset();
                $("#btnSubmit").attr('disabled', false);
                $("#Load").removeClass('active');
                toastr.success(responseData.message, 'success Alert', { timeOut: 5000 });
                window.location.href = `../GalleryImages?galleryType=${imageType}`;
            }
            else {
                toastr.error(responseData.message, 'error Alert', { timeOut: 5000 });
            }
        })
        .catch((error) => {
            // Handle errors here
            console.error(error);
        });

});