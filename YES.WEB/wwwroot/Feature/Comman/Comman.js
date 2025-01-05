"use strict";
var fileReader = new FileReader();
var filterType = /^(?:image\/bmp|image\/cis\-cod|image\/gif|image\/ief|image\/jpeg|image\/jpeg|image\/jpeg|image\/pipeg|image\/png|image\/svg\+xml|image\/tiff|image\/x\-cmu\-raster|image\/x\-cmx|image\/x\-icon|image\/x\-portable\-anymap|image\/x\-portable\-bitmap|image\/x\-portable\-graymap|image\/x\-portable\-pixmap|image\/x\-rgb|image\/x\-xbitmap|image\/x\-xpixmap|image\/x\-xwindowdump)$/i;

var Comman = {
    Init: () => {
        if ($("#CourseDll").length != 0) {
            Comman.getAllCourseDDl();
        }

    },

    getAllCourseDDl: () => {
        $.ajax({
            type: "get",
            url: "../Course/getAll",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                var dropdown = $('#CourseDll');
                dropdown.empty();
                $.each(data.data, function (i, item) {
                    dropdown.append($(`<li><a href="/course-details/${item.courseId}">${item.courseShortName}</a></li>`));
                });
            },
            error: function (request, status, error) {
                toastr.error('Opps, Something worng', 'Error Alert', { timeOut: 5000 });
                console.log(request);
            }
        });
    },
    getAllCourse: (id) => {
        $.ajax({
            type: "get",
            url: "../Course/getAll",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {

                var dropdown = $('.Course');
                if (id != undefined) {
                    dropdown = $(`#${id}`);
                }
                dropdown.empty();
                dropdown.append($("<option value='0'>Select Course</option>"));
                $.each(response.data, function (i, item) {
                    dropdown.append($(`<option value="${item.courseId}">${item.courseShortName}</option>`));
                });
            },
            error: function (request, status, error) {
                toastr.error('Opps, Something worng', 'Error Alert', { timeOut: 5000 });
                console.log(request);
            }
        });
    },
}
$(function () {
    Comman.Init();
})
function restrictAlphabets(e, msg) {
    var x = e.which || e.keycode;
    if ((x >= 48 && x <= 57)) {
        return true;
    }
    else {
        return false;
    }
}
function restrictnimonic(e) {

    var x = e.which || e.keycode;
    if ((x >= 65 && x <= 90) || (x >= 97 && x <= 122) || (x == 32) || (x == 46)) {
        return true;
    }
    else {
        return false;
    }
}
function Address(e) {

    var x = e.which || e.keycode;
    if ((x >= 65 && x <= 90) || (x >= 44 && x <= 47) || (x >= 97 && x <= 122) || (x == 32) || (x >= 48 && x <= 57)) {
        // document.getElementById(msg).innerHTML = "";
        return true;
    }
    else {
        //document.getElementById(msg).innerHTML = "Invalid Name format";

        return false;
    }
}
function TextOn() {
    $("#contactus").addClass("tallyslider");
}
function TextOff() {
    $("#contactus").removeClass("tallyslider");
}
function TextFormOn() {
    $("#ContactForm").addClass("tallyslider");
}
function TextFormOff() {
    $("#ContactForm").removeClass("tallyslider");
}
var readURL = function (ele, srcId) {
    //check and retuns the length of uploded file.
    if (ele.files.length.length === 0) {
        return false;
    }
    //Is Used for validate a valid file.
    var uploadFile = ele.files[0];
    if (!filterType.test(uploadFile.type)) {
        alert("Please select a valid image.");
        return;
    }
    fileReader.readAsDataURL(uploadFile);
    fileReader.onload = function (event) {
        var image = new Image();
        image.onload = function () {
            //document.getElementById("original-Img").src = image.src;
            var canvas = document.createElement("canvas");
            var context = canvas.getContext("2d");
            canvas.width = image.width / 4;
            canvas.height = image.height / 4;
            context.drawImage(image, 0, 0, image.width, image.height, 0, 0, canvas.width, canvas.height);
            document.getElementById(srcId).src = canvas.toDataURL();
        }
        image.src = event.target.result;
    };
}