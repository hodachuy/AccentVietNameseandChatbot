var _color = "rgb(234, 82, 105);",
    _srcLogo = "https://scontent.fsgn5-5.fna.fbcdn.net/v/t1.0-1/p200x200/24232656_1919691618058903_6510274581421009217_n.png?_nc_cat=100&_nc_oc=AQmDZcqvDR6pErTFfpYzh6zOPijTq8pPEzhl1fiYF3LPRU4055YYVX2YzBiATxqqdfY&_nc_ht=scontent.fsgn5-5.fna&oh=640bca2a8956c9770fc0b391498e79e9&oe=5CDC1307";

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

    // INPUT TEXT

    //$('#58al-input-text').keydown(function (e) {
    //    var text = $(this).val();
    //    if (text == "") {
    //        $("._4bqf_btn_submit").hide();
    //    } else {
    //        $("._4bqf_btn_submit").show();
    //    }
    //})

    $('#58al-input-text').keydown(function (e) {
        var text = $(this).val();
        $("._4bqf_btn_submit").show();
        if (e.which == 13) {
            e.preventDefault(e);
            if (text !== "") {
                $("._4bqf_btn_submit").hide();
                $(this).val('');
                submitMessage(text);
            }
        }
    })
    $('body').on('click', '._4bqf_btn_submit', function (e) {
        var text = $("#58al-input-text").val();
        if (text !== "") {
            submitMessage(text);
        }
    })
})

function submitMessage(text) {
    var messageUser = getMessageUser(text);
    $(".conversationContainer").append(messageUser);

    // return message bot
    $("#_12cd_event_button").empty();
    var writing = getMessageWriting();
    $(".conversationContainer").append(writing);
    setTimeout(function () {
        getMessageBot(text)
    },1000)
}

function getMessageBot(text) {    
    var param = {
        text: text,
        group: 'leg'
    }
    param = JSON.stringify(param)
    $.ajax({
        url: _Host + 'api/chatbot',
        contentType: 'application/json; charset=utf-8',
        data: param,
        type: 'POST',
        success: function (result) {
            var message = result.message[0];
            var postback = result.postback[0]
            console.log(result);
            console.log(message);
            console.log(postback);
            $("._4xkn_writing").remove();
            $(".conversationContainer").append(message);
            $("#_12cd_event_button").empty().append(postback);
        }
    });
}
function getMessageUser(text) {
    var html = '<div class="_4xkn clearfix">' +
                    '<div class="messages">' +
                    '    <div class="_21c3">' +
                    '        <div class="clearfix _2a0-">' +
                    '            <div class="_4xko _4xks" tabindex="0" role="button" style="background-color: ' + _color + '">' +
                    '                 <span>' +
                    '                      <span>' +
                    '                          '+text+'' +
                    '                      </span>' +
                    '                 </span>' +
                    '             </div>' +
                    '             <a class="_6934 noDisplay" href="#">' +
                    '                 This message didn\'t send. Click to try again.' +
                    '                 <span class="_21c6 error" title="Đã chuyển"></span>' +
                    '             </a>' +
                    '        </div>' +
                    '     </div>' +
                    '</div>' +
                '</div>';
    return html;
}
function getMessageWriting() {
    var html = '<div class="_4xkn _4xkn_writing clearfix">' +
    '               <div class="profilePictureColumn" style="bottom:0px;">' +
    '                    <div class="_4cqr">' +
    '                         <img class="profilePicture img" src="' + _srcLogo + '" alt="">' +
    '                         <div class="clearfix"></div>' +
    '                     </div>' +
    '                </div>' +
    '                <div class="messages">' +
    '                     <div class="_4xko _13y8">' +
    '                          <div class="_4a0v _1x3z">' +
    '                               <div class="_4b0g">' +
    '                                    <div class="_5pd7"></div>' +
    '                                    <div class="_5pd7"></div>' +
    '                                    <div class="_5pd7"></div>' +
    '                               </div>' +
    '                           </div>' +
    '                      </div>' +
    '                </div>' +
    '          </div>';

    return html;
}