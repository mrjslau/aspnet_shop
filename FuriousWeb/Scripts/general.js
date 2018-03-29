var plugins = '';
(function ($) {


    $('div.top-bar .profile a.toggle-menu').on('touchstart click', function (event) {
        event.preventDefault();
        $('div.top-bar .profile-menu').toggleClass('active');
    });

    $('div.top-bar .mobile-menu .mobile-menu-toggler').on('touchstart click', function (event) {
        event.preventDefault();
        $('div.top-bar').toggleClass('fixed');
        $('div.top-bar .mobile-menu .menu').toggleClass('active');
        $('div.top-bar .mobile-menu .mobile-menu-toggler i').toggleClass('change');
    });

    $('#payment-panel #pay-now').on('touchstart click', function () {
        console.log('click!');
        var amount = 100;
        paymentObject = {
            amount: amount,
            number: $("#card-number").val(),
            holer: $("#card-holder").val(),
            exp_year: $("#exp_year").val(),
            exp_month: $("#exp_month").val(),
            cvv: $("#card-cvv").val()
        };

        /*$.ajax({
            type: "GET",
            url: "https://mock-payment-processor.appspot.com/",
            dataType: 'json',
            async: false,
            headers: {
                "Authorization": "Basic " + btoa('technologines:platformos')
            },
            success: function () {
                alert('Thanks for your comment!');
            }
        });


        $.ajax({
            type: "POST",
            url: "https://mock-payment-processor.appspot.com/v1/payment",
            data: paymentObject,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (result) {
                alert("Returned: " + result);
            }
        });*/
    });



    $(function () {

        $.reject({
            reject: {msie: 9},
            closeCookie: true,
            imagePath: 'js/plugins/jReject-master/images/',
            header: 'Your browser is not supported here', // Header Text
            paragraph1: 'You are currently using an unsupported browser', // Paragraph 1
            paragraph2: 'Please install one of the many optional browsers below to proceed',
            closeMessage: 'Close this window at your own demise!' // Message below close window link
        });

    });

    plugins = {

        fancybox: function (obj) {

            obj.fancybox({helpers: {media: {}, overlay: {locked: false}}});

        },
        swipers: function () {

            //$('video').mediaelementplayer(); //uzkomentavau, nes truksta vieno plugino ir neranda funckijos

            var indexSwiper = new Swiper('.index-swiper', {
                loop: true,
                pagination: '.index-swiper ~ .swiper-pagination',
                nextButton: '.index-swiper ~ .swiper-button-next',
                prevButton: '.index-swiper ~ .swiper-button-prev',
                paginationClickable: true,
                paginationClickable: true,
                autoHeight: true,
                autoplay: 500000,
                effect: 'fade',
                loopAdditionalSlides: 0,
                loopedSlides: 0,
                onSlideChangeEnd: function () {
                    $('video').each(function () {
                        if (this.player) {
                            this.player.pause()
                        }
                    });
                    if ($('.swiper-slide-active').find("video").length > 0) {
                        var video = $('.swiper-slide-active').find("video").clone();
                        $('.swiper-slide-active > *').remove();
                        $('.swiper-slide-active').append(video);
                        indexSwiper.stopAutoplay();
                        var mediaplayer = $('.swiper-slide-active video').mediaelementplayer({
                            success: function (media, node, player) {
                                media.addEventListener('ended', function (e) {
                                    indexSwiper.startAutoplay();
                                }, false);
                            }
                        });
                        mediaplayer[0].player.play();

                    }
                }
            });
            $(window).resize(function () {
                setTimeout(function () {
                    $('.index-swiper-container .mejs-container').height($('.index-swiper-container').height());
                }, 500);

            });

        },
        buildMobile: function () {

            mobileModules.detect_smallest_rez();
            mobileModules.smart_vers();
            mobileModules.desktop_v_switcher();

        }
    };

    /* SCRIPTS INIT */

    plugins.fancybox($('.fancy'));
    plugins.swipers();
    plugins.buildMobile();
    //plugins.formCheck($('#forma'));

    /* SCRIPTS INIT END */

})(jQuery);
