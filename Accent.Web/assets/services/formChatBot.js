$(document).ready(function () {
    $('._5f0v').mouseenter(function () {
        $('.uiScrollableAreaTrack').removeClass('hidden_elem');
        $('.uiScrollableAreaTrack').css('opacity', '1');
    })
        .mouseleave(function () {
            $('.uiScrollableAreaTrack').addClass('hidden_elem');
            $('.uiScrollableAreaTrack').css('opacity', '0');
        });

    $("body").on('click', '.gl_next_carousel', function () {
        var $form = $(this).closest('.form_carousel');
        var currentIndex = $form.find($('div._a28')).attr('index');
        var newIndex = (parseInt(currentIndex) + 1);
        var maxIndex = $form.find($('div._2zgz')).length - 1;
        $form.find($('div._a28')).attr('index', newIndex);
        //$form.find('._2zgz').each(function (index, el) {
        //    maxIndex = parseInt(index);
        //});

        var calPX = -272 * newIndex;
        var leftPX = '' + calPX + 'px';

        $form.find('._a2e').css('left', leftPX);

        // show back
        $form.find('.gl_back_carousel').css('display', 'block');

        if (newIndex == maxIndex) {
            $form.find('.gl_next_carousel').css('display', 'none');
        }

    })
    $("body").on('click', '.gl_back_carousel', function () {
        var $form = $(this).closest('.form_carousel');
        var currentIndex = $form.find($('div._a28')).attr('index');
        var newIndex = (parseInt(currentIndex) - 1);
        $form.find($('div._a28')).attr('index', newIndex);

        var calPX = -272 * newIndex;
        var leftPX = '' + calPX + 'px';

        $form.find('._a2e').css('left', leftPX);
        if (newIndex == 0) {
            $form.find('.gl_back_carousel').css('display', 'none');
        }
        $form.find('.gl_next_carousel').css('display', 'block');
    })

    $("body").on('click', '.btn_next_carousel', function () {
        var $form = $(this).closest('.form_carousel');
        var currentIndex = $form.find($('div._a28')).attr('index');
        var newIndex = (parseInt(currentIndex) + 1);
        var maxIndex = $form.find($('div._2zgz')).length - 1;
        var calPX = -155 * newIndex;
        var leftPX = '' + calPX + 'px';

        $form.find($('div._a28')).attr('index', newIndex);
        $form.find('._a2e').css('left', leftPX);

        // show back
        $form.find('.btn_back_carousel').css('display', 'block');

        if (newIndex == maxIndex) {
            $form.find('.btn_next_carousel').css('display', 'none');
        }

    })
    $("body").on('click', '.btn_back_carousel', function () {
        var $form = $(this).closest('.form_carousel');
        var currentIndex = $form.find($('div._a28')).attr('index');
        var newIndex = (parseInt(currentIndex) - 1);
        $form.find($('div._a28')).attr('index', newIndex);

        var calPX = -155 * newIndex;
        var leftPX = '' + calPX + 'px';

        $form.find('._a2e').css('left', leftPX);
        if (newIndex == 0) {
            $form.find('.btn_back_carousel').css('display', 'none');
        }
        $form.find('.btn_next_carousel').css('display', 'block');
        
    })

    $('body').on('click', '._661n_btn_menu_chat', function () {
        $('.uiContextualLayerPositioner').toggle();
    })

    $('body').on('click', '._1qd3', function () {
        $('._4fsi').toggle();
    })
})

var templateUI = {

}