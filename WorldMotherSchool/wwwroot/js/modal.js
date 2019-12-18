
$("#crss").on("click", "div #myImg", function () {

    var img2 = document.querySelectorAll("#img02");
    for (var i = 0; i < img2.length; i++) {
        img2[i].style.display = "none";
        if (img2[i].src == this.src) {
            img2[i].src = this.src
            img2[i].style.display = "block";
        }
    }
    var modal = document.getElementById("myModal");
    modal.style.display = "block";
    var span = document.getElementsByClassName("close")[0];
    span.onclick = function () {
        modal.style.display = "none";
    }

});

var slideIndex = 1;

function PlusIndex(n) {
    ShowImage(slideIndex += n);
};

function CurrentSlide(n) {
    ShowImage(slideIndex = n);
}

function ShowImage(n) {
    var slide = document.getElementsByClassName("modal-content");


    if (n > slide.length) { slideIndex = 1 };
    if (n < 1) { slideIndex = slide.length }

    for (var i = 0; i < slide.length; i++) {
        slide[i].style.display = "none";
    };
    slide[slideIndex - 1].style.display = "block";
}