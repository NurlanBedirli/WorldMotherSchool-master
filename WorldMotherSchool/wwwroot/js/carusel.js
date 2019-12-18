    $('.multiple-items').slick({
        infinite: true,
        slidesToShow: 3,
        autoplay: true,
    autoplaySpeed: 2000,
    slidesToScroll: 1,
    responsive: [
                {
        breakpoint: 1024,
                    settings: {
        slidesToShow: 3,
    slidesToScroll: 3,
    infinite: true,
    dots: true
}
},
                {
        breakpoint: 968,
                    settings: {
        slidesToShow: 2,
    slidesToScroll: 1
}
},
                {
        breakpoint: 668,
                    settings: {
        slidesToShow: 1,
    slidesToScroll: 1
}
}
]
});

    $('.owl-carousel').owlCarousel({
        items: 1,
    loop: true,
    autoplay: true,
    margin: 10,
        autoplayTimeout: 5000,
        autoHeight: false,
    });



