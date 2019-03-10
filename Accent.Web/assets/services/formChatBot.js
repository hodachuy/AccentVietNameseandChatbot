var _color = "rgb(234, 82, 105);",
    _srcLogo = "https://scontent.fsgn5-5.fna.fbcdn.net/v/t1.0-1/p200x200/24232656_1919691618058903_6510274581421009217_n.png?_nc_cat=100&_nc_oc=AQmDZcqvDR6pErTFfpYzh6zOPijTq8pPEzhl1fiYF3LPRU4055YYVX2YzBiATxqqdfY&_nc_ht=scontent.fsgn5-5.fna&oh=640bca2a8956c9770fc0b391498e79e9&oe=5CDC1307";

var MESSAGE = {
    ERROR_01: "Xin lỗi, Tôi không hiểu",
    ERROR_02: "Bạn có thể giải thích thêm được không?",
    ERROR_03:"Tôi không thể tìm thấy, bạn có thể nói rõ hơn?",
    ERROR_04:"Xin lỗi, Bạn có thể giải thích thêm được không?",
    ERROR_05:"Tôi không thể tìm thấy",
    ERROR_06: "Tôi chưa hiểu?"
}
$(document).ready(function () {
    //$('._5f0v').mouseenter(function () {
    //    $('.uiScrollableAreaTrack').removeClass('hidden_elem');
    //    $('.uiScrollableAreaTrack').css('opacity', '1');
    //})
    //    .mouseleave(function () {
    //        $('.uiScrollableAreaTrack').addClass('hidden_elem');
    //        $('.uiScrollableAreaTrack').css('opacity', '0');
    //    });

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
                submitMessage(text,'');
            }
        }
    })
    $('body').on('click', '._4bqf_btn_submit', function (e) {
        var text = $("#58al-input-text").val();
        if (text !== "") {
            submitMessage(text,'');
        }
    })

    // view datetime message
    //$('body').on('click', '._4xko', function () {
    //    $('.datebreak').removeClass('hide').addClass('hide');
    //    $('.viewed').removeClass('hide').addClass('hide');
    //    if ($(this).parent().prev().hasClass('hide')) {
    //        $(this).parent().prev().removeClass('hide');
    //        $(this).parent().next().removeClass('hide');
    //    }
    //})
    $('body').click(function (e) {
        if (!$(e.target).closest('._4xko').length) {
            $('.datebreak').removeClass('hide').addClass('hide');
            $('.viewed').removeClass('hide').addClass('hide');
        } else {
            $('.datebreak').removeClass('hide').addClass('hide');
            $('.viewed').removeClass('hide').addClass('hide');
            if ($(e.target).closest('._4xko').parent().prev().hasClass('hide')) {
                $(e.target).closest('._4xko').parent().prev().removeClass('hide');
                $(e.target).closest('._4xko').parent().next().removeClass('hide');
            }
        }
    });

    // postback
    $('body').on('click', '._2zgz', function (e) {
        var dataText = $(this).children().eq(0).text();
        var dataPostback = $(this).children().eq(0).attr('data-postback');
        submitMessage(dataText, dataPostback);
        e.stopPropagation();
    })
    //menu
    $('body').on('click', '._6ir5', function (e) {
        var dataText = $(this).children().children().eq(0).text();
        var dataPostback = $(this).children().children().eq(0).attr('data-postback');
        submitMessage(dataText, dataPostback);
        // chặn ảnh hưởng tới thẻ a href next
        e.stopPropagation();
    })
})

// send message
function submitMessage(text, textPostback) {
    //return message user
    var messageUser = getMessageUser(text);
    setDateCurrent();
    $(".conversationContainer").append(messageUser);

    //return buble writing
    $("#_12cd_event_button").empty();
    var writing = getMessageWriting();
    $(".conversationContainer").append(writing);

    // return message bot
    setTimeout(function () {
        if (textPostback != "") {
            getMessageBot(textPostback);
        }
        else {
            getMessageBot(text);
        }
    }, 1000)

    //scrollbar to bottom
    scrollBar();
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
            var postback = result.postback[0];
            var resultAPI = result.messageai[0];
            var isMatch = result.isCheck;
            var html = '';
            $("._4xkn_writing").remove();
            if (isMatch)
            {
                if (resultAPI.includes("[{")) {
                    resultAPI = JSON.parse(resultAPI);


                } else {
                    switch (resultAPI) {
                        case "NOT_MATCH_01":
                            html = MESSAGE.ERROR_01;
                            break;
                        case "NOT_MATCH_02":
                            html = MESSAGE.ERROR_02;
                            break;
                        case "NOT_MATCH_03":
                            html = MESSAGE.ERROR_03;
                            break;
                        case "NOT_MATCH_04":
                            html = MESSAGE.ERROR_04;
                            break;
                        case "NOT_MATCH_05":
                            html = MESSAGE.ERROR_05;
                            break;
                        case "NOT_MATCH_06":
                            html = MESSAGE.ERROR_06;
                            break;
                        default:
                            html = MESSAGE.ERROR_02;
                            break;
                    }
                }

                $(".conversationContainer").append(html);

            } else {
                $(".conversationContainer").append(message);
            }

            $("#_12cd_event_button").empty().append(postback);

            scrollBar();

        }
    });
}
function getMessageUser(text) {
    var html = '<div class="_4xkn clearfix">' +
                    '<div class="messages">' +
                    '    <div class="_21c3">' +
                    '        <h4 class="datebreak _497p _2lpt hide"><time class="_3oh-">T6 16:52</time></h4>' +
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
                             '<span class="viewed hide" style="animation: fadeIn 0.1s cubic-bezier(0.4, 0, 0.2, 1) 0s 1 normal both running; clear: both; color: rgba(0, 0, 0, 0.4); float: right; font-size: 12px; font-weight: 500; padding-left: 0px; padding-right: 7px;">Đã xem</span>';

                    '     </div>' +
                    '</div>' +
                '</div>';
    return html;
}

function getMessageBot(text) {
    var html = '<div class="_4xkn _4xkn_writing clearfix">' +
'               <div class="profilePictureColumn" style="bottom:0px;">' +
'                    <div class="_4cqr">' +
'                         <img class="profilePicture img" src="' + _srcLogo + '" alt="">' +
'                         <div class="clearfix"></div>' +
'                     </div>' +
'                </div>' +
'                <div class="messages">' +
                                         '<div class="_21c3">' +
'                                                                                    <div class="clearfix _2a0-">' +
'                                                                                        <div class="_4xko _4xkr" tabindex="0" role="button" style="background-color: rgb(241, 240, 240);">' +
'                                                                                            <span>' +
'                                                                                                <span>' + text + '</span>' +
'                                                                                            </span>' +
'                                                                                        </div>' +
'                                                                                    </div>' +
'                                                                                </div>'+
'                </div>' +
'          </div>';

    return html;
}

function tempModuleSearchAPI(lstData) {

    var innerHtml = '';
    var storageData = lstData.filter(function (x) { return x.answer != null; });
    if (storageData.length > 0) {
        lstData = storageData;
    }

    $.each(lstData, function (index, value) {
        olHtml += '<li data-target="#quote-carousel' + idxMsg + '" data-slide-to="' + index + '" class="' + (index == 0 ? "active" : "") + '"></li>';

        innerHtml += '<div class="item ' + (index == 0 ? "active" : "") + '">';
        innerHtml += '<blockquote>';
        innerHtml += ' <div class="row">';
        innerHtml += ' <div class="col-sm-12  text-center">';
        innerHtml += '<p>' + (value.question != null ? add3Dots(value.question, 120) : "") + '</p>';
        //innerHtml += '<small><a href="http://qa.surelrn.vn/cau-hoi-phap-luat-' + value.id + '.html" target="_blank">-xem chi tiết-</a></small>';
        innerHtml += '<small><a href="javascript:void(0);" onclick="ShowQuesPopup(\'' + value.question + '\',\'' + value.answer + '\')">-xem chi tiết-</a></small>';
        innerHtml += '</div>';
        innerHtml += '  </div>';
        innerHtml += '</blockquote>';
        innerHtml += '</div>';

    })

    var html = '<div class="_4xkn clearfix">' +
    '                                                                            <div class="profilePictureColumn" style="bottom: 0px;">' +
    '                                                                                <div class="_4cqr">' +
    '                                                                                    <img class="profilePicture img" src="https://scontent.fsgn5-5.fna.fbcdn.net/v/t1.0-1/p200x200/24232656_1919691618058903_6510274581421009217_n.png?_nc_cat=100&_nc_oc=AQmDZcqvDR6pErTFfpYzh6zOPijTq8pPEzhl1fiYF3LPRU4055YYVX2YzBiATxqqdfY&_nc_ht=scontent.fsgn5-5.fna&oh=640bca2a8956c9770fc0b391498e79e9&oe=5CDC1307" alt="">' +
    '                                                                                    <div class="clearfix"></div>' +
    '                                                                                </div>' +
    '                                                                            </div>' +
    '                                                                            <div class="messages">' +
    '                                                                                <div class="_21c3">' +
    '                                                                                    <div class="clearfix _2a0-">' +
    '                                                                                        <div class="_4xko _2k7w _4xkr bot_reply">' +
    '                                                                                            <div class="">' +
    '                                                                                                <div currentselectedindex="0" maxchangeamount="1" class="_23n- form_carousel">' +
    '                                                                                                    <div class="_4u-c">' +
    '                                                                                                        <div index="0" class="_a28">' +



    //'                                                                                                            <div class="_a2e">' +
    //'                                                                                                                <div class="_2zgz">' +
    //'                                                                                                                    <div class="_6j2h">' +
    //'                                                                                                                        <div class="_6j2i">' +
    //'                                                                                                                            <div class="_6j0s" style="background-image: url("https://external.xx.fbcdn.net/safe_image.php?d=AQCSBbhBgqlNZyhj&url=http%3A%2F%2Fgames.hekate.ai%2Fcolorvalley%2Fcolor.png&_nc_hash=AQCUwB4E9I_H34we"); background-position: center center; height: 150px; width: 100%;">' +
    //'                                                                                                                            </div>' +
    //'                                                                                                                            <div class="_6j2g">' +
    //'                                                                                                                                <div class="_6j0t _4ik4 _4ik5" style="-webkit-line-clamp: 3;">Color valley</div>' +
    //'                                                                                                                                <div class="_6j0v">' +
    //'                                                                                                                                    <div class="_6j0u _6j0w">Thể hiện cho mọi người thấy IQ của bạn đi nào</div>' +
    //'                                                                                                                                    <div class="_6j0u _6j0x _4ik4 _4ik5" style="-webkit-line-clamp: 2;">' +
    //'                                                                                                                                        <div>Thử tài khéo léo của bạn đi</div>' +
    //'                                                                                                                                    </div>' +
    //'                                                                                                                                </div>' +
    //'                                                                                                                            </div>' +
    //'                                                                                                                            <div class="_6ir5">' +
    //'                                                                                                                                <div class="_4bqf _6ir3">' +
    //'                                                                                                                                    <div class="_6ir4 _6ir6">' +
    //'                                                                                                                                        <span data-hover="tooltip" id="js_3yi" data-tooltip-content="Để sử dụng tính năng này, hãy dùng ứng dụng Messenger.">Chia sẻ</span>' +
    //'                                                                                                                                    </div>' +
    //'                                                                                                                                </div>' +
    //'                                                                                                                            </div>' +
    //'                                                                                                                            <div class="_6ir5">' +
    //'                                                                                                                                <div class="_4bqf _6ir3">' +
    //'                                                                                                                                    <a class="_6ir4" target="_blank" href="https://l.facebook.com/l.php?u=http%3A%2F%2Fgames.hekate.ai%2Fcolorvalley%3Fid%3DU2FsdGVkX1%25209YZDNYz%2520uv5G1C73Nf1Ioircg5kpDrbWNCJg%25202pmGS8k3pioJ6Vs2%26fbclid%3DIwAR0WPCRVNUIbx2Jvh0jPukiQiArUxSkGfPB5HP_R5E5LlpHlZTywB5lhZdw&h=AT1cl6uhE8iLAiq2MHon0PdXFmgoqbdOhj-an_9k2UZLZQG-xf-7OI70zznq83fD0fGoOif88rsER62xZnVpTa2lHTkxOMKTG1ivTvwl-nQ7HGOIw08QdILs3mlSW3I8tpRPiExstb-AM3b-" rel="nofollow noopener" data-lynx-mode="hover" style="color: rgb(234, 82, 105);">Bắt đầu</a>' +
    //'                                                                                                                                </div>' +
    //'                                                                                                                            </div>' +
    //'                                                                                                                            <div class="_6ir5">' +
    //'                                                                                                                                <div class="_4bqf _6ir3">' +
    //'                                                                                                                                    <a class="_6ir4" href="#" style="color: rgb(234, 82, 105);">Xếp hạng</a>' +
    //'                                                                                                                                </div>' +
    //'                                                                                                                            </div>' +
    //'                                                                                                                        </div>' +
    //'                                                                                                                    </div>' +
    '                                                                                                                </div>' +
    '                                                                                                            </div>' +
    '                                                                                                        </div>' +
    '                                                                                                        <div class="_4u-f">' +
    '                                                                                                            <iframe aria-hidden="true" class="_1_xb" tabindex="-1"></iframe>' +
    '                                                                                                        </div>' +
    '                                                                                                    </div>' +
    '' +
    '                                                                                                    <a class="_32rk _32rg _1cy6 gl_back_carousel" href="#" style="display:none;">' +
    '                                                                                                        <div direction="backward" class="_10sf _5x5- _5x60">' +
    '                                                                                                            <div class="_5x6d">' +
    '                                                                                                                <div class="_3bwv _3bww">' +
    '                                                                                                                    <div class="_3bwy">' +
    '                                                                                                                        <div class="_3bwx"><i class="_3-8w img sp_bfeq6p sx_c4c7bc" alt=""></i></div>' +
    '                                                                                                                    </div>' +
    '                                                                                                                </div>' +
    '                                                                                                            </div>' +
    '                                                                                                        </div>' +
    '                                                                                                    </a>' +
    '' +
    '                                                                                                    <a class="_32rk _32rh _1cy6 gl_next_carousel" href="#">' +
    '                                                                                                        <div direction="forward" class="_10sf _5x5_">' +
    '                                                                                                            <div class="_5x6d">' +
    '                                                                                                                <div class="_3bwv _3bww">' +
    '                                                                                                                    <div class="_3bwy">' +
    '                                                                                                                        <div class="_3bwx">' +
    '                                                                                                                            <i class="_3-8w img sp_RQ3p_x3xMG3 sx_dbbd74" alt=""></i>' +
    '                                                                                                                        </div>' +
    '                                                                                                                    </div>' +
    '                                                                                                                </div>' +
    '                                                                                                            </div>' +
    '                                                                                                        </div>' +
    '                                                                                                    </a>' +
    '                                                                                                </div>' +
    '                                                                                            </div>' +
    '                                                                                        </div>' +
    '                                                                                    </div>' +
    '                                                                                </div>' +
    '                                                                            </div>' +
    '                                                                        </div>';


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


/*
############################################
            Template Carousel With API
############################################
*/
templateCarousel = function () {
    var html = '';

}


/*
#####################################
            SCROLLBAR
#####################################
*/
function scrollBar() {
    $(".uiScrollableAreaWrap").scrollTop($(".uiScrollableAreaWrap").prop('scrollHeight'));
}


/*
#####################################
       GET DATECURRENT MESSAGE
#####################################
*/
var date, hours, minutes;

function formatAMPM(date) {
    hours = date.getHours();
    minutes = date.getMinutes();
    var ampm = hours >= 12 ? 'PM' : 'AM';
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    minutes = minutes < 10 ? '0' + minutes : minutes;
    var strTime = hours + ':' + minutes;
    return strTime;
}

// append time cho tin nhắn tiếp theo khi trò chuyện không trao đổi trong phút đó

function setDateCurrent() {
    var html = '';
    date = new Date()

    if (minutes != date.getMinutes()) {
        html +='<h4 class="datebreak _497p _2lpt">';
        html += '    <time class="_3oh-">' + formatAMPM(date) + '</time>';
        html += '</h4>';
    }
    return html;
}

// mai lam` click text message show thoi` gian va` da~ xem.

//<div class="_21c3">
//   <h4 class="datebreak _497p _2lpt"><time class="_3oh-">T6 16:52</time></h4>
//   <div class="clearfix _2a0-">
//      <div class="_4xko _4xks" tabindex="0" role="button" style="background-color: rgb(234, 82, 105);"><span><span><img alt="↩️" class="_1ift _2560 img" src="https://static.xx.fbcdn.net/images/emoji.php/v9/t20/1/16/21a9.png"> Khởi Động Lại</span></span></div>
//      <a class="_6934 noDisplay" href="#">This message didn't send. Click to try again.<span class="_21c6 error" title="Đã chuyển"></span></a>
//   </div>
//   <span style="animation: fadeIn 0.1s cubic-bezier(0.4, 0, 0.2, 1) 0s 1 normal both running; clear: both; color: rgba(0, 0, 0, 0.4); float: right; font-size: 12px; font-weight: 500; padding-left: 0px; padding-right: 7px;">Đã xem</span>
//</div>