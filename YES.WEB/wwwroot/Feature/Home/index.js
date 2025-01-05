var baseUrl = window.location.origin;
"use strict";
var Home = {
    Init: () => {
        Home.getAllNews();
        Home.getAllUpcomingBatches();
        Home.getAllSliderImage();
        Home.getAllGallery();
    },
    getAllNews: () => {
        $("#newsAndUpdates").load(baseUrl + "/News/getNewsAndUpdates");
    },
    getAllUpcomingBatches: () => {
        $("#upcomingBatches").load(baseUrl + "/Batches/GetUpcomingBtaches");
    },
    getAllGallery: () => {
        $("#galleryImages").load(baseUrl + "/getAll");
    },
    getAllFeedBack: () => {

    },
     getAllSliderImage: () => {
        $("#sliderImages").load(baseUrl + "/slider-images");
    },
}

$(function () {
    Home.Init();
})
