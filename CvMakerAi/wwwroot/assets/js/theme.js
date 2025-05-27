/*
 Theme Name: Shapro
 Theme URI: https://themeforest.net/user/themewar/portfolio
 Author: themewar
 Author URI: 
 Description: Shapro - Multipurpose Landing Page HTML5 Responsive Templates
 Version: 1.0
 License:
 License URI:
 */
 
/*=======================================================================
 [Table of contents]
 =========================================================================
 1. Init Obj
 2. All Carousel
 3. All Popup Popup
 4. Search Toggler
 5. Fun Fact Count
 6. All Skill Circle
 7. Toggle Pricing
 8. Back To Top
 9. Preloader
 10. Tw Strech Column Inside
 11. Skills Bar
 12. PopUp Menu
 13. Custom Google Maps
 14. Contact Form Submission
 15. Suffle Filter
 16. Qty
 17. Mobile Menu
 */

(function ($) {
    'use strict';
    /*--------------------------------------------------------------------------
    / 1. Init Obj
    /--------------------------------------------------------------------------*/
    var video_slide = $('.video-slide'), 
        professional_slide = $('.professional-slide'),
        testimonial_slider = $('.testimonial-slider'),
        popup_video = $('.popup-video'),
        popup_img = $('.popup-image'),
        client_slider = $('.client-slider'),
        client_slider_two = $('.client-slider-two'),
        testimonial_two = $('.testimonial-slider-two'),
        testimonial_three = $('.testimonial-slider-three'),
        team_slider = $('.team-slider'),
        team_slider_two = $('.team-slider-two');
    /*--------------------------------------------------------
     / 2. All Carousel
     /----------------------------------------------------------*/
    /*--- Video Slider ---*/
    video_slide.owlCarousel({
        items: 1,
        animateIn: 'fadeInRight',
        animateOut: 'fadeOut',
        margin: 0,
        autoplay: false,
        nav: false,
        navText: false,
        dots: true
    });
    /*--- Professional Slider ---*/
    $(window).on('load', function (event) {
        professional_slide.owlCarousel({
            loop: false,
            responsiveClass: true,
            smartSpeed: 800,
            margin: 15,
            autoplay: false,
            nav: true,
            dots: false,
            navText: ['<i class="fal fa-arrow-left"></i>','<i class="fal fa-arrow-right"></i>'],
            items: 3,
            responsive:{
                0:{
                    items: 1
                },
                700:{
                    items: 2
                },
                1200:{
                    items: 3
                }
            }
        });
    });
    /*--- Testimonial Slider ----*/
    testimonial_slider.owlCarousel({
        loop: false,
        responsiveClass: true,
        smartSpeed: 800,
        margin: 30,
        autoplay: false,
        nav: false,
        dots: true,
        items: 2,
        responsive:{
            0:{
                items: 1
            },
            992:{
                items: 2
            }
        }
    });
    /*---- Client Logo Slider ----*/
    client_slider.owlCarousel({
        loop: false,
        responsiveClass: true,
        smartSpeed: 800,
        margin: 30,
        autoplay: true,
        nav: false,
        dots: false,
        items: 6,
        responsive:{
            0:{
                items: 1
            },
            768:{
                items: 4
            },
            992:{
                items: 6
            }
        }
    });
    client_slider_two.owlCarousel({
        loop: false,
        responsiveClass: true,
        smartSpeed: 800,
        margin: 30,
        autoplay: true,
        nav: false,
        dots: false,
        items: 5,
        responsive:{
            0:{
                items: 1
            },
            768:{
                items: 3
            },
            900:{
                items: 4
            },
            1200:{
                items: 5
            }
        }
    });
    /*--- Testimonial Slider 2 ----*/
    testimonial_two.slick({
        slidesToShow: 1,
        centerMode: true,
        centerPadding: '0px',
        infinite: true,
        dots: false,
        arrows: true,
        prevArrow: '<button type="button" class="slick-prev"><i class="shapro-left-arrow"></i></button>',
        nextArrow: '<button type="button" class="slick-next"><i class="shapro-right"></i></button>',
        autoplay: true
    });
    /*--- Team Slider ---*/
    team_slider.owlCarousel({
        loop: false,
        responsiveClass: true,
        smartSpeed: 600,
        margin: 0,
        autoplay: false,
        nav: false,
        dots: true,
        items: 2,
        responsive:{
            0:{
                items: 1
            },
            768:{
                items: 2
            }
        }
    });
    /*-- Testimonial Slider 03 ----*/
    testimonial_three.owlCarousel({
        loop: false,
        responsiveClass: true,
        smartSpeed: 600,
        margin: 0,
        animateIn: 'bounceIn',
        autoplay: false,
        nav: false,
        dots: true,
        items: 1
    });
    /*---- Team Slider 2 -----*/
    team_slider_two.owlCarousel({
        loop: true,
        responsiveClass: true,
        smartSpeed: 600,
        margin: 0,
        autoplay: true,
        nav: false,
        dots: false,
        items: 3,
        responsive:{
            0:{
                items: 1
            },
            768:{
                items: 2
            },
            991:{
                items: 3
            }
        }
    });

    /*--------------------------------------------------------
    / 3. All Popup Popup
    /----------------------------------------------------------*/
    popup_video.lightcase({
        transition: 'elastic',
        showSequenceInfo: false,
        slideshow: false,
        swipe: true,
        showTitle: false,
        controls: true
    });
    popup_img.lightcase({
        transition: 'elastic',
        showSequenceInfo: false,
        slideshow: true,
        swipe: true,
        showTitle: false,
        controls: true
    });
    
    /*--------------------------------------------------------------------------
    / 4. Search Toggler
    /------------------------------------------------------------------------*/
    $('.src_btn').on('click', function (e) {
        e.preventDefault();
        $('.popup_search_sec').toggleClass('active');
    });
    $('.popup_search_overlay').on('click', function () {
        $('.popup_search_sec').removeClass('active');
    });
    $('.popup_search_form input').on('focus', function(){
        $('.popup_search_form').addClass('focused');
    });
    $('.popup_search_form input').on('blur', function(){
        $('.popup_search_form').removeClass('focused');
    });
    /*--------------------------------------------------------
    / 5. Fun Fact Count
    /----------------------------------------------------------*/
    var skl = true;
    $('.funfact').appear();
    $('.funfact').on('appear', function () {
        if (skl)
        {
            $('.timer').each(function () {
                var $this = $(this);
                jQuery({Counter: 0}).animate({Counter: $this.attr('data-counter')}, {
                    duration: 4000,
                    easing: 'swing',
                    step: function () {
                        var num = Math.ceil(this.Counter).toString();
                        /*if (Number(num) > 999) {
                         while (/(\d+)(\d{3})/.test(num)) {
                         num = num.replace(/(\d+)(\d{3})/, '$1' + '' + '$2');
                         }
                         }*/
                        $('span', $this).html(num);
                    }
                });
            });
            skl = false;
        }
    });

    /*--------------------------------------------------------
    / 6. All Skill Circle
    /----------------------------------------------------------*/
    $(window).on('load', function (event) {
        if ($(".circle-skill").length > 0) {
            var ast1 = true;
            $('.circle-skill').appear();
            $('.circle-skill').on('appear', function () {
                if (ast1 == true) {
                    $(".circle-skill").each(function () {
                        var pint = $(this).attr('data-skills');
                        var decs = pint * 100;
                        var grs = $(this).attr('data-gradientstart');
                        var gre = $(this).attr('data-gradientend');
                        var bg = $(this).attr('data-bg');

                        $(this).circleProgress({
                            value: pint,
                            startAngle: -Math.PI / 3 * 1,
                            fill: {gradient: [[grs, 1], [gre, .2]], gradientAngle: Math.PI / 4 * 2},
                            lineCap: 'square',
                            thickness: 9,
                            animation: {duration: 1800},
                            size: 165,
                            emptyFill: bg
                        }).on('circle-animation-progress', function (event, progress) {
                            $(this).find('strong').html(parseInt(decs * progress) + '<span>%</span>');
                        });
                    });
                    ast1 = false;
                }
            });
        }
        if ($(".circle-skill-2").length > 0) {
            var ast2 = true;
            $('.circle-skill-2').appear();
            $('.circle-skill-2').on('appear', function () {
                if (ast2 == true) {
                    $(".circle-skill-2").each(function () {
                        var pint = $(this).attr('data-skills');
                        var decs = pint * 100;
                        var grs = $(this).attr('data-gradientstart');
                        var gre = $(this).attr('data-gradientend');
                        var bg = $(this).attr('data-bg');

                        $(this).circleProgress({
                            value: pint,
                            startAngle: -Math.PI / 2 * 1,
                            fill: {gradient: [[grs, 1], [gre, .2]], gradientAngle: Math.PI / 4 * 1},
                            lineCap: 'square',
                            thickness: 20,
                            animation: {duration: 2000},
                            size: 195,
                            emptyFill: bg
                        }).on('circle-animation-progress', function (event, progress) {
                            $(this).find('strong').html(parseInt(decs * progress) + '<span>%</span>');
                        });
                    });
                    ast2 = false;
                }
            });
        }
        if ($(".cs-in").length > 0) {
            var csin = true;
            $('.cs-in').appear();
            $('.cs-in').on('appear', function () {
                if (csin == true) {
                    $(".cs-in").each(function () {
                        var pint = $(this).attr('data-skills');
                        var decs = pint * 100;
                        var grd = $(this).attr('data-gradient');
                        var bg = $(this).attr('data-bg');

                        $(this).circleProgress({
                            value: pint,
                            startAngle: -Math.PI / 2 * 1,
                            fill: grd,
                            lineCap: 'round',
                            thickness: 2,
                            animation: {duration: 2000},
                            size: 176,
                            emptyFill: bg
                        }).on('circle-animation-progress', function (event, progress) {
                            $(this).find('strong').html(parseInt(decs * progress) + '<span>%</span>');
                        });
                    });
                    csin = false;
                }
            });
        }
        if ($(".skill-item-3").length > 0) {
            var ast3 = true;
            $('.skill-item-3').appear();
            $('.skill-item-3').on('appear', function () {
                if (ast3 == true) {
                    $(".skill-item-3").each(function () {
                        var pint = $(this).attr('data-skills');
                        var decs = pint * 100;
                        var grs = $(this).attr('data-gradientstart');
                        var gre = $(this).attr('data-gradientend');
                        var bg = $(this).attr('data-bg');

                        $(this).circleProgress({
                            value: pint,
                            startAngle: -Math.PI / 100,
                            fill: {gradient: [[grs, 1], [gre, .2]], gradientAngle: Math.PI / 1},
                            lineCap: 'round',
                            thickness: 8,
                            animation: {duration: 2000},
                            size: 116,
                            radius: 50,
                            emptyFill: bg
                        }).on('circle-animation-progress', function (event, progress) {
                            $(this).find('strong').html(parseInt(decs * progress) + '<span>%</span>');
                        });
                    });
                    ast3 = false;
                }
            });
        }
    });
    /*--------------------------------------------------------
    / 7. Toggle Pricing
    /----------------------------------------------------------*/
    if ($('#switch-toggle-tab').length) {
        var toggleSwitch = $('#switch-toggle-tab label.switch');
        var TabTitle = $('#switch-toggle-tab li');
        var monthTabTitle = $('#switch-toggle-tab li.month');
        var yearTabTitle = $('#switch-toggle-tab li.year');
        var monthTabContent = $('#month');
        var yearTabContent = $('#year');
        // hidden show deafult;
        monthTabContent.fadeIn();
        yearTabContent.fadeOut();

        function toggleHandle() {
            if (toggleSwitch.hasClass('on')) {
                yearTabContent.fadeOut();
                monthTabContent.fadeIn();
                monthTabTitle.addClass('active');
                yearTabTitle.removeClass('active');
            } else {
                monthTabContent.fadeOut();
                yearTabContent.fadeIn();
                yearTabTitle.addClass('active');
                monthTabTitle.removeClass('active');
            }
        };
        monthTabTitle.on('click', function () {
            toggleSwitch.addClass('on').removeClass('off');
            toggleHandle();
            return false;
        });
        yearTabTitle.on('click', function () {
            toggleSwitch.addClass('off').removeClass('on');
            toggleHandle();
            return false;
        });
        toggleSwitch.on('click', function () {
            toggleSwitch.toggleClass('on off');
            toggleHandle();
        });
    }
    /*--------------------------------------------------------
    / 8. Back To Top
    /----------------------------------------------------------*/
    var back = $("#back-to-top"),
        body = $("body, html");
    $(window).on('scroll', function () {
        if ($(window).scrollTop() > $(window).height())
        {
            back.css({bottom: '20px', opacity: '1', visibility: 'visible'});
        } else
        {
            back.css({bottom: '-20px', opacity: '0', visibility: 'hidden'});
        }
    });
    body.on("click", "#back-to-top", function (e) {
        e.preventDefault();
        body.animate({scrollTop: 0}, 800);
        return false;
    });
    /*--------------------------------------------------------
    / 9. Preloader
    /----------------------------------------------------------*/
    $(window).on('load', function (event) {
        $('.preloader').delay(800).fadeOut(500);
    })
    /*--------------------------------------------------------
    / 10. Tw Strech Column Inside
    /----------------------------------------------------------*/
    function tw_stretch() {
        var i = $(window).width();
        $(".row .tw-stretch-element-inside-column").each(function() {
            var $this = $(this),
                row = $this.closest(".row"),
                cols = $this.closest('[class^="col-"]'),
                colsheight = $this.closest('[class^="col-"]').height(),
                rect = this.getBoundingClientRect(),
                l = row[0].getBoundingClientRect(),
                s = cols[0].getBoundingClientRect(),
                r = rect.left,
                d = i - rect.right,
                c = l.left + (parseFloat(row.css("padding-left")) || 0),
                u = i - l.right + (parseFloat(row.css("padding-right")) || 0),
                p = s.left,
                f = i - s.right,
                styles = {
                    "margin-left": 0,
                    "margin-right": 0
                };
            if (Math.round(c) === Math.round(p)) {
                var h = parseFloat($this.css("margin-left") || 0);
                styles["margin-left"] = h - r;
            }
            if (Math.round(u) === Math.round(f)) {
                var w = parseFloat($this.css("margin-right") || 0);
                styles["margin-right"] = w - d;
            }
            $this.css(styles);
        });
    }
    tw_stretch();
    /*--------------------------------------------------------
    / 11. Skills Bar
    /----------------------------------------------------------*/
    if ($(".sk-item").length > 0)
    {
        $('.sk-item').appear();
        $('.sk-item').on('appear', loadSkills);
    }
    var coun = true;
    function loadSkills()
    {
        $(".sk-item").each(function () {
            var datacount = $(this).attr("data-limit");
            $(".skillbar", this).animate({'width': datacount + '%'}, 3000);
            $(".rounds", this).animate({'left': datacount + '%'}, 3000);
            if (coun)
            {
                $(this).find('.parcent').each(function () {
                    var $this = $(this);
                    $({Counter: 0}).animate({Counter: datacount}, {
                        duration: 3000,
                        easing: 'swing',
                        step: function () {
                            $this.text(Math.ceil(this.Counter) + '%');
                        }
                    });
                });

            }
        });
        coun = false;
    }
    
    /*--------------------------------------------------------
    / 12. PopUp Menu
    /---------------------------------------------------------*/
    $('#hamburger').on('click', function (e) {
        e.preventDefault();
        $('.popup_menu').addClass('active').fadeIn();
        setTimeout(function () {
            if ($('.popup_menu').hasClass('active')) {
                var tlMenu = new TimelineLite();
                tlMenu.set($(".animated_menu"), {y: 80, opacity: 0});
                $(".animated_menu").each(function (index, element) {
                    tlMenu.to(element, 0.5, {y: 0, opacity: 1, delay: 0.4, ease: Power2.easeOut}, index * 0.1)
                });
            } else {
                var tlMenu = new TimelineLite();
                $(".animated_menu").each(function (index, element) {
                    tlMenu.to(element, 0.25, {y: -80, opacity: 0, ease: Power2.easeIn}, index * 0.05)
                });
            }

        }, 20)
    });
     if ($(".menu_popup").length > 0)
    {
        $(".menu_popup ul li.menu-item-has-children > a").on('click', function (e) {
            e.preventDefault();
            if ($(this).parent('li').hasClass('active'))
            {
                $(this).parent('li').removeClass('active');
                $(this).next('ul.sub-menu').slideUp('slow');
            } else
            {
                $(".menu-item-has-children ul.sub-menu").slideUp('slow');
                $(".menu-item-has-children.active").removeClass("active");
                $(this).parent().toggleClass('active');
                $(this).next('ul.sub-menu').slideToggle('slow');
            }
        });
    }
    $('#close_menu').on('click', function () {
        var tlMenu = new TimelineLite();
        $(".animated_menu").each(function (index, element) {
            tlMenu.to(element, 0.25, {y: -80, opacity: 0, ease: Power2.easeIn}, index * 0.05)
        });
        $(".popup_menu ul.sub-menu").slideUp('slow', function () {
            $(".menu-item-has-children.active").removeClass("active");
            $('.popup_menu').removeClass('active');
        });
        setTimeout(function () {
            $('.popup_menu').fadeOut()
        }, 500)
    });

    /*--------------------------------------------------------
    / 13. Custom Google Maps
    /----------------------------------------------------------*/
    if ($("#shapro_map").length > 0)
    {
        var map;
        map = new GMaps({
            el: "#shapro_map",
            lat: -37.815340,
            lng: 144.963230,
            zoom: 10,
        });
        var image = "";
        map.addMarker({
            lat: -37.815340,
            lng: 144.963230,
            icon: "assets/images/marker.png",
            animation: google.maps.Animation.DROP,
            verticalAlign: "bottom",
            horizontalAlign: "center",
            backgroundColor: "#d3cfcf"
        });
        var styles = [
            {
                "featureType": "road",
                "stylers": [
                    {"color": "#f2b95e"}
                ]
            }, {
                "featureType": "water",
                "stylers": [
                    {"color": "#aacbd9"}
                ]
            }, {
                "featureType": "landscape",
                "stylers": [
                    {"color": "#ffffee"}
                ]
            }, {
                "elementType": "labels.text.fill",
                "stylers": [
                    {"color": "#2c2c2c"}
                ]
            }, {
                "featureType": "poi",
                "stylers": [
                    {"color": "#f5eac5"}
                ]
            }, {
                "elementType": "labels.text",
                "stylers": [
                    {"saturation": 1},
                    {"weight": 0.1},
                    {"color": "#2c2c2c"}
                ]
            }

        ];
        map.addStyle({
            styledMapName: "Styled Map",
            styles: styles,
            mapTypeId: "map_style"
        });

        map.setStyle("map_style");
    }

    /*--------------------------------------------------------
    / 14. Contact Form Submission
    /--------------------------------------------------------*/
    $('#contact_form').on('submit', function (e) {
        e.preventDefault();
        var $this = $(this);

        $('input[type="submit"]', this).attr('disabled', 'disabled');
        $('.shapro_loader', this).fadeIn();

        var form_data = $this.serialize();

        var required = 0;
        $(".required", this).each(function () {
            if ($(this).val() === '')
            {
                $(this).addClass('reqError');
                required += 1;
            } else
            {
                if ($(this).hasClass('reqError'))
                {
                    $(this).removeClass('reqError');
                    if (required > 0)
                    {
                        required -= 1;
                    }
                }
            }
        });
        if (required === 0) {
            $.ajax({
                type: 'POST',
                url: 'assets/ajax/mail.php',
                data: {form_data: form_data},
                success: function (data) {
                    $('input[type="submit"]', $this).removeAttr('disabled');
                    $('.shapro_loader', $this).fadeOut();

                    $this.remove('.shapro_con_message');
                    $('.shapro_con_message', $this).fadeIn().html('Congratulation! Message successfully sent.');
                    setTimeout(function () {
                        $('.shapro_con_message', $this).fadeOut().html('');
                    }, 5000);
                }
            });
        } else {
            $('input[type="submit"]', $this).removeAttr('disabled');
            $('.shapro_loader', $this).fadeOut();
            $('.shapro_con_message', $this).fadeOut().html('');
        }

    });
    $(".required").on('keyup', function () {
        $(this).removeClass('reqError');
    });
    /*--------------------------------------------------------
    / 15. Suffle Filter
    /--------------------------------------------------------*/
    $(window).on('load', function () {
        if ($(".shafull-container").length > 0)
        {
            var $grid = $('.shafull-container');
            $grid.shuffle({
                itemSelector: '.shaf-item',
                sizer: '.shaf-sizer'
            });
            /* reshuffle when user clicks a filter item */
            $('.shaf-filter li').on('click', function () {
                // set active class
                $('.shaf-filter li').removeClass('active');
                $(this).addClass('active');
                // get group name from clicked item
                var groupName = $(this).attr('data-group');
                // reshuffle grid
                $grid.shuffle('shuffle', groupName);
            });
        }
    });

    /*--------------------------------------------------------
    / 16. Qty
    /----------------------------------------------------------*/
    if ($(".qtyBtn").length > 0)
    {
        $(".btnMinus").on('click', function () {
            var vals = parseInt($(this).next(".carqty").val(), 10);
            if (vals > 1)
            {
                vals -= 1;
                $(this).next(".carqty").val(vals);
            } else
            {
                $(this).next(".carqty").val(vals);
            }
            return false;
        });
        $(".btnPlus").on('click', function () {
            var vals = parseInt($(this).prev(".carqty").val(), 10);
            vals += 1;
            $(this).prev(".carqty").val(vals);
            return false;
        });
    }

    /*--------------------------------------------------------
    / 17. Mobile Menu
    /----------------------------------------------------------*/
    $(window).on("load resize", function (e) {
        if ($(window).width() < 991) {
            $('.menuButton a').on('click', function (e) {
                e.preventDefault();
                $('.menu_1 > ul').stop(true, true).slideToggle();
            });
            $('.menu_1 ul li.menu-item-has-children').each(function () {
                var $this = $(this);
                $this.append('<span class="submenu_toggler"><i class="fas fa-arrow-down"></i></span>');
            });
            $('.menu_1 ul li.menu-item-has-children > span.submenu_toggler').on('click', function () {
                var $this = $(this);
                
                if ($(this).hasClass('active-span')) {
                    $('i', $this).removeClass('fa-arrow-up').addClass('fa-arrow-down');
                } else {
                    $('i', $this).addClass('fa-arrow-up').removeClass('fa-arrow-down');
                }

                $(this).prev('ul.sub-menu').slideToggle();
                $(this).toggleClass('active-span');
            });
        };
    });

})(jQuery);