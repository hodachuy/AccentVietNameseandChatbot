var _color = $("#botColor").val(),
    _srcLogo = _Host + $("#botLogo").val(),//_Host http://113.161.108.36
    _isActiveIntroduct = false;

var MESSAGE = {
    ERROR_01: "Xin lỗi, Tôi không hiểu",
    ERROR_02: "Bạn có thể giải thích thêm được không?",
    ERROR_03: "Tôi không thể tìm thấy, bạn có thể nói rõ hơn?",
    ERROR_04: "Xin lỗi, Bạn có thể giải thích thêm được không?",
    ERROR_05: "Tôi không thể tìm thấy",
    ERROR_06: "Tôi chưa hiểu?"
}
$(document).ready(function () {
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

    $('body').on('click', '._1qd3_btn_setting', function () {
        $('._4fsi').toggle();
    })

    // close form
    $('body').on('click', '._1qd1_close_form', function (e) {
        parent.postMessage("close", "*");
    })

    // INPUT TEXT
    $('#58al-input-text').keydown(function (e) {
        var text = $(this).val();
        $("._4bqf_btn_submit").show();
        if (e.which == 13) {
            e.preventDefault(e);
            if (text.trim() !== "") {
                $("._4bqf_btn_submit").hide();
                $(this).val('');                
                submitMessage(text, '');              
            }
        }
    })
    $('body').on('click', '._4bqf_btn_submit', function (e) {
        var text = $("#58al-input-text").val();
        if (text.trim() !== "") {
            submitMessage(text, '');
        }
        $('#58al-input-text').val('');
    })
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
    $('body').on('click', '._2zgz_postback', function (e) {
        var dataText = $(this).children().eq(0).text();
        var dataPostback = $(this).children().eq(0).attr('data-postback');
        submitMessage(dataText, dataPostback);
        e.stopPropagation();
    })
    //menu
    $('body').on('click', '._6ir4_menu', function (e) {
        e.preventDefault();
        var dataText = $(this).text();
        var dataPostback = $(this).attr('data-postback');
        submitMessage(dataText, dataPostback);
        // chặn ảnh hưởng tới thẻ a href next
        e.stopPropagation();
    })
    //module
    $('body').on('click', '._6ir4_module', function (e) {
        e.preventDefault();
        var dataText = $(this).text();
        var dataPostback = $(this).attr('data-postback');
        var mdInfoPatientID = $(this).attr('data-id');
        $('.chk-opt-module-' + mdInfoPatientID + '').each(function(){
            if ($(this).prop('checked')) {
                console.log($(this).val());
            }
        })

        submitMessage(dataText, dataPostback);
        e.stopPropagation();
    })
    //popup
    $('body').on('click', '._6ir4_popup', function (e) {
        e.preventDefault();
        //show 1 popup ngoài iframe
        var quesID = $(this).attr('data-id');
        var domain = 'http://qa.surelrn.vn';//http://localhost:54160;
        parent.postMessage(quesID, domain);
    })

    //setting accent vn
    chatbotSetting();
})

// setting accent vn
var chatbot_chk_accent = JSON.parse(localStorage.getItem("cbot_chk_accent"));
var chatbot_chk_popup = JSON.parse(localStorage.getItem("cbot_chk_popup"));
function chatbotSetting() {
    chatbot_chk_accent = true;
    $('#chk-stt-accent').prop('checked', chatbot_chk_accent);
    $("#chk-stt-popup").prop('checked', chatbot_chk_popup);
}

$("#chk-stt-accent").click(function () {
    localStorage.setItem("cbot_chk_accent", $(this).prop('checked'));
});
$("#chk-stt-popup").click(function () {
    localStorage.setItem("cbot_chk_popup", $(this).prop('checked'));
});

window.addEventListener('message', function (event) {
    var widthParent = parseInt(event.data);
    if (widthParent <= 425) {
        $("._3-8j").css('margin', '0px 0px 0px');
        $("._6atl").css('height', '100%');
        $("._6atl").css('width', event.data);
        $("._6ati").css('height', '100%');
        $("._6ati").css('width', event.data);
    }

    startMessageBotIntroduct();
    //if (event.origin === 'http://localhost:47887') {
    //    console.log(event.origin)
    //    console.log('message received:  ' + event.data, event);
    //};
}, false);


// init message introductory
function startMessageBotIntroduct() {
    if ($("#haveIntroduct").val() == "True") {
        if (_isActiveIntroduct == false) {
            // buble writing
            $("#_12cd_event_button").empty();
            var writing = getMessageWriting();
            $(".conversationContainer").append(writing);

            //start
            var content = '';
            if ($("#botTextIntro").val() != '') {
                content = $("#botTextIntro").val();
            }
            if ($("#cardID").val() != null || $("#cardID").val() != '') {
                content = "postback_card_" + $("#cardID").val();
            }
            setTimeout(function () {
                getMessageBot(content);
            },1500)
            _isActiveIntroduct = true;
        }
    }
}

// send message
function submitMessage(text, textPostback) {
    //return message user
    var messageUser = getMessageUser(text);
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
            if ($("#chk-stt-accent").prop('checked') == true) {
                get_message_bot_accent(text);
            } else {
                getMessageBot(text);
            }

        }
    }, 1000)
    //scrollbar to bottom
    scrollBar();
}

// icon setting non accent chatbo   
function get_message_bot_accent(text) {
    $.ajax({
        url: _Host + '/apiv1/convertVN?text=' + text,
        contentType: 'application/json; charset=utf-8',
        type: 'GET'
    }).done(function (response) {
        console.log(response)
        if (response == "ERROR_400") {
            console.log("Load data accent vietnamese not success")
            return false;
        }
        if (response != "") {
            response = response.replace('\n', '.');
            if (text != response) {
                var messageDidYouMean = tempDidYouMeanBot(response);
                $(".conversationContainer").append(messageDidYouMean);
            }
            setTimeout(function () {
                getMessageBot(response);
            }, 1500)
        } else {
            setTimeout(function () {
                getMessageBot(text);
            }, 1500)
        }
    })
}

function submitMessageBot(text, delay) {
    //return buble writing
    $("#_12cd_event_button").empty();
    var writing = getMessageWriting();
    $(".conversationContainer").append(writing);

    // return message bot
    setTimeout(function () {
        $("._4xkn_writing").remove();
        $(".conversationContainer").append(text);
        //scrollbar to bottom
        scrollBar();
    }, delay)
}

function getMessageBot(text) {
    var param = {
        text: text,
        group: 'leg',
        token: $('#userID').val(),
        botId: $('#botID').val(),
        isMdSearch: $('#isMDSearch').val()
    }
    param = JSON.stringify(param)
    $.ajax({
        url: _Host + 'apiv1/chatbot',
        contentType: 'application/json; charset=utf-8',
        data: param,
        type: 'POST',
        success: function (result) {
            console.log(result)
            var message = result.message[0];
            var postback = result.postback[0];
            var resultAPI = result.messageai.trim();
            var isMatch = result.isCheck;
            var html = '';
            if (!isMatch) {
                if (resultAPI.includes("[{")) {
                    resultAPI = JSON.parse(resultAPI);
                    var data = tempModuleSearchAPI(resultAPI);
                    new Promise((resolve, reject) => {
                        submitMessageBot(tempTextBot('Tôi tìm thấy ' + data.count + ' câu hỏi liên quan đến câu hỏi của bạn.'), 0)
                        resolve();
                    })
                    //.then(() => {
                    //    submitMessageBot(tempTextBot('Có đúng câu hỏi bạn quan tâm ?'), 1000)
                    //})
                    .then(() => {
                        submitMessageBot(data.dataHtml, 1500);
                        setTimeout(function () {
                            if (postback !== null) {
                                postback = postback.replace(/{{color}}/g, _color);
                                $("#_12cd_event_button").empty().append(postback);
                                scrollBar();
                            }
                        },1500)
                    });
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
                            html = "NOT_MATCH_01";
                            break;
                    }
                    html = tempTextBot(html);
                    $("._4xkn_writing").remove();
                    $(".conversationContainer").append(html);
                }
            } else {
                $("._4xkn_writing").remove();
                message = message.replace(/{{color}}/g, _color);
                message = message.replace(/{{image_logo}}/g, _srcLogo);
                $(".conversationContainer").append(message);
            }

            if (postback !== null) {
                postback = postback.replace(/{{color}}/g, _color);
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
                    '            <div class="_4xko _4xks" tabindex="0" role="button" style="background-color: ' + _color + 'font-family: Segoe UI Light;">' +
                    '                 <span>' +
                    '                      <span>' +
                    '                          ' + text + '' +
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

function tempTextBot(text) {
    var htmlText = '<div class="_4xkn clearfix">' +
'               <div class="profilePictureColumn" style="bottom:0px;">' +
'                    <div class="_4cqr">' +
'                         <img class="profilePicture img" src="' + _srcLogo + '" alt="">' +
'                         <div class="clearfix"></div>' +
'                     </div>' +
'                </div>' +
'                <div class="messages">' +
                      '<div class="_21c3">' +
'                          <div class="clearfix _2a0-">' +
'                               <div class="_4xko _4xkr _tmpB" tabindex="0" role="button" style="background-color: rgb(241, 240, 240); font-family: Segoe UI Light;">' +
'                                   <span>' +
'                                       <span>' + text + '</span>' +
'                                   </span>' +
'                               </div>' +
'                          </div>' +
'                     </div>' +
'                </div>' +
'          </div>';
    return htmlText;
}

function tempDidYouMeanBot(text) {
    var htmlText = '<div class="_4xkn clearfix">' +
'               <div class="profilePictureColumn" style="bottom:0px;">' +
'                    <div class="_4cqr">' +
'                         <img class="profilePicture img" src="' + _srcLogo + '" alt="">' +
'                         <div class="clearfix"></div>' +
'                     </div>' +
'                </div>' +
'                <div class="messages">' +
                      '<div class="_21c3">' +
'                          <div class="clearfix _2a0-">' +
'                               <div class="_4xko _4xkr _tmpB" tabindex="0" role="button" style="font-style:italic;font-family: Segoe UI Light">' +
'                                   <span>' +
'                                       <span> Ý bạn là: ' + text + '</span>' +
'                                   </span>' +
'                               </div>' +
'                          </div>' +
'                     </div>' +
'                </div>' +
'          </div>';
    return htmlText;
}

function tempModuleSearchAPI(lstData) {
    var tempModuleHtml = '';
    var itemHtml = '';
    var storageData = lstData.filter(function (x) { return x.answer != null; });
    if (storageData.length > 0) {
        lstData = storageData;
    }
    $.each(lstData, function (index, value) {
        itemHtml += '<div class="_2zgz">';
        itemHtml += '<div class="_6j2h">';
        itemHtml += '<div class="_6j2i">';
        itemHtml += '<div class="_6j2g">';
        itemHtml += '<div class="_6j0t _4ik4 _4ik5" style="-webkit-line-clamp: 3;">' + (value.question != null ? add3Dots(value.question, 120) : "") + '</div>';
        itemHtml += '<div class="_6j0v">';
        itemHtml += '<div class="_6j0u _6j0w">' + (value.field != null ? value.field : "") + '</div>';
        itemHtml += '<div class="_6j0u _6j0x _4ik4 _4ik5" style="-webkit-line-clamp: 2;">';
        itemHtml += '<div>' + (value.field != null ? value.field : "") + '</div>';
        itemHtml += '</div>';
        itemHtml += '</div>';
        itemHtml += '</div>';
        itemHtml += '<div class="_6ir5">';
        itemHtml += '<div class="_4bqf _6ir3">';
        itemHtml += '<a class="_6ir4 _6ir4_popup qa_law" data-id="' + value.id + '" href="javascript:void(0)" rel="nofollow noopener" data-lynx-mode="hover" style="color: rgb(234, 82, 105);">Xem chi tiết</a>';
        itemHtml += '</div>';
        itemHtml += '</div>';
        itemHtml += '</div>';
        itemHtml += '</div>';
        itemHtml += '</div>';
    })
    tempModuleHtml += ' <div class="_4xkn clearfix">';
    tempModuleHtml += '<div class="profilePictureColumn" style="bottom: 0px;">';
    tempModuleHtml += '<div class="_4cqr">';
    tempModuleHtml += '<img class="profilePicture img" src="' + _srcLogo + '" alt="">';
    tempModuleHtml += '<div class="clearfix"></div>';
    tempModuleHtml += '</div>';
    tempModuleHtml += '</div>';
    tempModuleHtml += '<div class="messages">';
    tempModuleHtml += '<div class="_21c3">';
    tempModuleHtml += '<div class="clearfix _2a0-">';
    tempModuleHtml += '<div class="_4xko _2k7w _4xkr bot_reply">';
    tempModuleHtml += '<div class="">';
    tempModuleHtml += '<div currentselectedindex="0" maxchangeamount="1" class="_23n- form_carousel">';
    tempModuleHtml += '<div class="_4u-c">';
    tempModuleHtml += '<div index="0" class="_a28">';
    tempModuleHtml += '<div class="_a2e">';

    tempModuleHtml += itemHtml;

    tempModuleHtml += '</div>';
    tempModuleHtml += '</div>';
    tempModuleHtml += '<div class="_4u-f">';
    tempModuleHtml += '<iframe aria-hidden="true" class="_1_xb" tabindex="-1"></iframe>';
    tempModuleHtml += '</div>';
    tempModuleHtml += '</div>';
    if (storageData.length > 1) {
        tempModuleHtml += '<a class="_32rk _32rg _1cy6 gl_back_carousel" href="#" style="display:none;">';
        tempModuleHtml += '<div direction="backward" class="_10sf _5x5- _5x60">';
        tempModuleHtml += '<div class="_5x6d">';
        tempModuleHtml += '<div class="_3bwv _3bww">';
        tempModuleHtml += '<div class="_3bwy">';
        tempModuleHtml += '<div class="_3bwx"><i class="_3-8w img sp_bfeq6p sx_c4c7bc" alt=""></i></div>';
        tempModuleHtml += '</div>';
        tempModuleHtml += '</div>';
        tempModuleHtml += '</div>';
        tempModuleHtml += '</div>';
        tempModuleHtml += '</a>';
        tempModuleHtml += '<a class="_32rk _32rh _1cy6 gl_next_carousel" href="#">';
        tempModuleHtml += '<div direction="forward" class="_10sf _5x5_">';
        tempModuleHtml += '<div class="_5x6d">';
        tempModuleHtml += '<div class="_3bwv _3bww">';
        tempModuleHtml += '<div class="_3bwy">';
        tempModuleHtml += '<div class="_3bwx">';
        tempModuleHtml += '<i class="_3-8w img sp_RQ3p_x3xMG3 sx_dbbd74" alt=""></i>';
        tempModuleHtml += '</div>';
        tempModuleHtml += '</div>';
        tempModuleHtml += '</div>';
        tempModuleHtml += '</div>';
        tempModuleHtml += '</div>';
        tempModuleHtml += '</a>';
    }
    tempModuleHtml += '</div>';
    tempModuleHtml += '</div>';
    tempModuleHtml += '</div>';
    tempModuleHtml += '</div>';
    tempModuleHtml += '</div>';
    tempModuleHtml += '</div>';
    tempModuleHtml += '</div>';
    return data = { dataHtml: tempModuleHtml, count: storageData.length };
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
        html += '<h4 class="datebreak _497p _2lpt">';
        html += '    <time class="_3oh-">' + formatAMPM(date) + '</time>';
        html += '</h4>';
    }
    return html;
}

function add3Dots(string, limit) {
    var dots = "...";
    if (string.length > limit) {
        string = string.substring(0, limit) + dots;
    }

    return string;
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


