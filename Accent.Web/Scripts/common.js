/*
*======================================================================
* Convert non accent vietnamese to accent vietnamese
*======================================================================
*/

var delay = (function () {
    var timer = 0;
    return function (callback, ms) {
        clearTimeout(timer);
        timer = setTimeout(callback, ms);
    };
})();

$('#txtSearch').keyup(function () {
    delay(function () {
        closeOptions();
        getAccentVN('#txtSearch');
    }, 200);
});

var timer = null;
$('#txtSearch').keydown(function () {
    clearTimeout(timer);
    closeOptions();
    timer = setTimeout(function () {
        addTag('#txtSearch');
        getOptions();
    }, 1500)
});

$("#accent-vn").popover({
    title: 'Gợi ý khác',
    html: true,
    container: 'body',
    trigger: "manual",
    animation: false
}).on("mouseenter", function () {
    var _this = this;
    $(this).popover("show");
    $(".popover").on("mouseleave", function () {
        $(_this).popover('hide');
    });
}).on("mouseleave", function () {
    var _this = this;
    setTimeout(function () {
        if (!$(".popover:hover").length) {
            $(_this).popover("hide");
        }
    }, 1500);
});

function getAccentVN(element) {
    var text = $(element).text();
    $.ajax({
        url: _Host + 'api/GetAccentVN?text=' + text,
        contentType: 'application/json; charset=utf-8',
        type: 'GET',
        success: function (result) {
            if (result == "ERROR_400") {
                console.log("Load data accent vietnamese not success")
                return false;
            }
            if (text.trim() === result.Item) {
                $("#accent-vn").empty();
            }
            else {
                $("#accent-vn").html('Ý của bạn là : ' + '<a class="spell-correct-text" href="javascript:getTextAccent(\'' + result.Item + '\',\'' + element + '\')">' + result.Item + '</a>')
                var html = '';
                html += '<ul>';
                $.each(result.ArrItems, function (index, item) {
                    html += '<li><a href="javascript:getTextAccent(\'' + item + '\',\'' + element + '\')">' + item + '</a></li>';
                })
                html += '</ul>';
                $('#accent-vn').data('bs.popover').options.content = html;
            }
        },
    });
}

function getMultiPredictVN(element) {
    var text = $(element).text();
    $.ajax({
        url: _Host + 'api/GetMultiMatchesAccentVN?text=' + text,
        contentType: 'application/json; charset=utf-8',
        type: 'GET',
        success: function (results) {
            var alters = '<ul id ="id-options"class="options clearfix">';
            var choiceId;
            var oddColor = "odd-choice";
            var evenColor = "even-choice";
            var isOdd = true;
            var color;
            for (var i = 0; i < results.length; ++i) {
                if (i % 6 == 0) {
                    if (isOdd) {
                        color = oddColor;
                        isOdd = false;
                    } else {
                        color = evenColor;
                        isOdd = true;
                    }
                }
                choiceId = 'choice';
                alters += '<li id="choice-' + i + '" class="choice ' + color + '">&nbsp;<a style="cursor:pointer;" href="javascript:void(0);" onclick="changeTextError(\'' + results[i] + '\',this)">' + results[i] + '</a></li>';
            }
            alters += '<li id="option-cancel" class="cancel-btn"><i class="fa fa-times"></i><span class="cancel"></span></li>';
            alters += '</ul>';
            $(element).append(alters);
            $(element).css('position', 'relative');

            $('#option-cancel').click(function () {
                closeOptions();
                return false;
            });
        },
    });
}

function changeTextError(text, element) {
    event.stopPropagation;
    $(element).parent().parent().parent().text(text);
    $("#id-options").remove();
    return false;
}

function getTextAccent(text, element) {
    $(element).html(text);
    $("#accent-vn").empty();
    $("#accent-vn").popover("hide");
}

function addTag(element) {
    $("#div-cursor").remove();
    var str = $(element).text();
    str = str.replace("</span> <span class='word'>", " ");
    var i = 0;
    str = str.replace(/\s/g, "</span> <span class='word'>");


    $(element).html("<span class='word'>" + str + "</span>");
}
this.getOptions = function () {
    $("span.word").on('click', function () {
        closeOptions();
        getMultiPredictVN(this);
        return false;
    })
}

function closeOptions() {
    $("#id-options").remove();
}

String.prototype.replaceAllSpace = function (search, replacement) {
    var target = this;
    return target.replace(new RegExp(search, 'g'), replacement);
};

/*
*======================================================================
* CHATBOT
*======================================================================
*/
var html = '';
var editorLegal,
    editorMed,
    editorEdu,
    editorTour,
    mode;
var e;
$(document).ready(function () {
    
    loadAIML();
   

    //YUI().use('aui-ace-editor', function (Y) {
    //    editorLegal = new Y.AceEditor(
    //      {
    //          boundingBox: '#editorAIML_legal',
    //          mode: 'xml',
    //          value: html,
    //          height: '550',
    //          width: '100%',
    //      }
    //    ).render();
    //    editorMed = new Y.AceEditor(
    //      {
    //          boundingBox: '#editorAIML_med',
    //          mode: 'xml',
    //          value: '<?xml version="1.0" encoding="UTF-8"?>',
    //          height: '1000',
    //          width: '750'
    //      }
    //    ).render();
    //    editorEdu = new Y.AceEditor(
    //    {
    //        boundingBox: '#editorAIML_edu',
    //        mode: 'xml',
    //        value: '<?xml version="1.0" encoding="UTF-8"?>',
    //        height: '1000',
    //        width: '750'
    //    }
    //   ).render();
    //    editorTour = new Y.AceEditor(
    //     {
    //         boundingBox: '#editorAIML_tour',
    //         mode: 'xml',
    //         value: '<?xml version="1.0" encoding="UTF-8"?>',
    //         height: '1000',
    //         width: '750'
    //     }
    //    ).render();
    //});
})
function TestInsert() {

    var xml =  "\n<category>\n"+
                "   <pattern></pattern>\n" +
                "   <template></template>\n" +
                "</category>\n";

    var cursorPosition = e.getCursorPosition();

    e.session.insert(cursorPosition, xml);
   
}
function loadAIML() {
    $.ajax({
        url: _Host + 'api/LoadAIML',
        contentType: 'application/json; charset=utf-8',
        type: 'GET',
        success: function (result) {
            html = result;
            setTimeout(function () {
                e = ace.edit("editorTest");
                e.getSession().setMode("ace/mode/xml");
                e.setTheme("ace/theme/textmate");
                e.setValue(html);


                //var editorMed = ace.edit("editorTest-med");
                //editorMed.getSession().setMode("ace/mode/xml");
                //editorMed.setTheme("ace/theme/textmate");
                //editorMed.setValue("<xml?>");
            }, 1000)
        },
    });
}

function saveAIML() {
    var dataForm = new FormData();
    console.log(e.getValue())
    dataForm.append("formAIML", JSON.stringify(e.getValue()));
    $.ajax({
        type: "POST",
        url: _Host + 'api/SaveAIML',
        data: dataForm,
        contentType: false,
        processData: false,
        success: function (result) {
            alert('Lưu thành công!')
            console.log(result);

        },
    });
}

//$(function () {

//var chatbot_chk_accent = localStorage.getItem("cbot_chk_accent");
//var chatbot_chk_popup = localStorage.getItem("cbot_chk_popup");

//chatbotSetting();

//var INDEX = 0;
//var templateCB = {
//    renderCarousel: function (idxMsg, lstData, numShow) {
//        var html = '',
//            olHtml = '',
//            innerHtml = '';
//        var storageData = lstData.filter(function (x) { return x.answer != null; });
//        if (storageData.length > 0) {
//            lstData = storageData;
//        }
//        if (storageData.length > 1) {
//            generate_message('Tôi tìm thấy ' + storageData.length + ' câu hỏi liên quan đến câu hỏi của bạn', 'bot');
//        }
//        $.each(lstData, function (index, value) {
//            olHtml += '<li data-target="#quote-carousel' + idxMsg + '" data-slide-to="' + index + '" class="' + (index == 0 ? "active" : "") + '"></li>';

//            innerHtml += '<div class="item ' + (index == 0 ? "active" : "") + '">';
//            innerHtml += '<blockquote>';
//            innerHtml += ' <div class="row">';
//            innerHtml += ' <div class="col-sm-12  text-center">';
//            innerHtml += '<p>' + (value.question != null ? add3Dots(value.question, 120) : "") + '</p>';
//            innerHtml += '<small><a href="http://qa.surelrn.vn/cau-hoi-phap-luat-' + value.id + '.html" target="_blank">-xem chi tiết-</a></small>';
//            innerHtml += '</div>';
//            innerHtml += '  </div>';
//            innerHtml += '</blockquote>';
//            innerHtml += '</div>';

//        })

//        html += '<div class="row">';
//        html += '<div class="col-md-12 text-center">';
//        html += '<h5>Có đúng câu hỏi bạn quan tâm ?</h5>';
//        html += '</div>';
//        html += '</div>';
//        html += '<div class="row">';
//        html += '<div class="col-md-12">';
//        html += '<div class="carousel slide" data-ride="carousel" id="quote-carousel' + idxMsg + '">';
//        html += '<ol class="carousel-indicators">';

//        html += olHtml;

//        html += ' </ol>';
//        html += '<div class="carousel-inner">';

//        html += innerHtml;

//        html += '</div>';
//        if (storageData.length > 1) {
//            html += '<a data-slide="prev" href="#quote-carousel' + idxMsg + '" class="left carousel-control"><i class="fa fa-chevron-left"></i></a>';
//            html += '<a data-slide="next" href="#quote-carousel' + idxMsg + '" class="right carousel-control"><i class="fa fa-chevron-right"></i></a>';
//        }
//        html += ' </div>';
//        html += '</div>';
//        html += '</div>';
//        return html;
//    },
//    renderButton: function () {

//    }
//}

//function chatbotSetting() {
//    if (chatbot_chk_accent)
//        $("#chk-stt-accent").prop('checked', chatbot_chk_accent);
//    if (chatbot_chk_accent)
//        $("#chk-stt-popup").prop('checked', chatbot_chk_popup);
//}

//$("#chk-stt-accent").click(function () {
//    localStorage.setItem("cbot_chk_accent", $(this).prop('checked'));
//});
//$("#chk-stt-popup").click(function () {
//    localStorage.setItem("cbot_chk_popup", $(this).prop('checked'));
//});

//function getMsgBot(text) {
//    var param = {
//        text: text,
//        group: 'leg'
//    }
//    param = JSON.stringify(param)
//    $.ajax({
//        url: _Host + 'api/chatbot',
//        contentType: 'application/json; charset=utf-8',
//        data: param,
//        type: 'POST',
//        success: function (result) {
//            generate_message(result, 'bot');
//        },
//    });
//}

//function add3Dots(string, limit) {
//    var dots = "...";
//    if (string.length > limit) {
//        string = string.substring(0, limit) + dots;
//    }
//    return string;
//}

//function generate_message(msg, type) {
//    INDEX++;

//    if (type == 'bot') {
//        if (msg.includes("[{")) {
//            msg = JSON.parse(msg);
//            console.log(msg);
//            msg = templateCB.renderCarousel(INDEX, msg, 10)
//        }
//    }

//    var str = "";
//    str += "<div id='cm-msg-" + INDEX + "' class='chat-msg " + type + "'>";
//    if (type == 'bot') {
//        str += "          <span class='msg-avatar'>";
//        str += "            <img src='" + _Host + "Content/img/user_bot.jpg'>";
//        str += "          </span>";
//    }
//    str += "          <div class='cm-msg-text'>";
//    str += msg;
//    str += "          </div>";
//    str += "        </div>";
//    $(".chat-logs").append(str);

//    $("#cm-msg-" + INDEX).hide().fadeIn(300);
//    if (type == 'self') {
//        $("#chat-input").val('');
//    }

//    $(".chat-logs").stop().animate({ scrollTop: $(".chat-logs")[0].scrollHeight }, 1000);

//    clickPostback();
//}

//function clickPostback() {
//    $(".btn-postback-area").off().on('click', function (e) {
//        e.preventDefault();
//        var postback = $(this).attr('data-postback');
//        var text = $(this).text();

//        generate_message(text, 'self');

//        setTimeout(function () {
//            getMsgBot(postback);
//        }, 1000)

//        return false;
//    })

//}

//$(document).delegate(".chat-btn", "click", function () {
//    var value = $(this).attr("chat-value");
//    var name = $(this).html();
//    $("#chat-input").attr("disabled", false);
//    generate_message(name, 'self');
//})


//$("#chat-submit").click(function (e) {
//    e.preventDefault();
//    var msg = $("#chat-input").val();
//    if (msg.trim() == '') {
//        return false;
//    }
//    generate_message(msg, 'self');
//    setTimeout(function () {
//        getMsgBot(msg);
//    }, 1000)
//})

//$("#chat-circle").click(function () {
//    $("#chat-circle").toggle('scale');
//    $(".chat-box").toggle('scale');
//})

//$("#chat-bot-close").click(function () {
//    $("#chat-circle").toggle('scale');
//    $(".chat-box").toggle('scale');
//})



    var INDEX = 0;
    var templateCB = {
        renderCarousel: function (idxMsg, lstData, numShow) {
            var html = '',
            olHtml = '',
            innerHtml = '';
            var storageData = lstData.filter(function (x) { return x.answer != null; });
            if (storageData.length > 0) {
                lstData = storageData;
            }
            if (storageData.length > 1) {
                generate_message('Tôi tìm thấy ' + storageData.length + ' câu hỏi liên quan đến câu hỏi của bạn', 'bot');
            }


            html += '<div class="row">';
            html += '<div class="col-md-12 text-center">';
            html += '<p>Có đúng câu hỏi bạn quan tâm ?</p>';
            html += '</div>';
            html += '</div>';
          
            return html;
        },
        renderButton: function () {

        },
        renderCarousel2: function (idxMsg, lstData, numShow) {
        var html = '',
        olHtml = '',
        innerHtml = '';
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

        //html += '<div class="row">';
        html += '<div class="col-md-12" style="background:white;border-radius:15px;margin-bottom: 15px;">';
        html += '<div class="carousel slide" data-ride="carousel" id="quote-carousel' + idxMsg + '">';
        html += '<ol class="carousel-indicators">';

        html += olHtml;

        html += ' </ol>';
        html += '<div class="carousel-inner">';

        html += innerHtml;

        html += '</div>';
        if (storageData.length > 1) {
            html += '<a data-slide="prev" href="#quote-carousel' + idxMsg + '" class="left carousel-control"><i class="fa fa-chevron-left"></i></a>';
            html += '<a data-slide="next" href="#quote-carousel' + idxMsg + '" class="right carousel-control"><i class="fa fa-chevron-right"></i></a>';
        }
        html += ' </div>';
        html += '</div>';
        //html += '</div>';
        return html;
    }
    }

    var chatbot_chk_accent = JSON.parse(localStorage.getItem("cbot_chk_accent"));
    var chatbot_chk_popup = JSON.parse(localStorage.getItem("cbot_chk_popup"));

    chatbotSetting();

    function chatbotSetting() {
        $('#chk-stt-accent').prop('checked', chatbot_chk_accent);
        $("#chk-stt-popup").prop('checked', chatbot_chk_popup);
    }

    $("#chk-stt-accent").click(function () {
        localStorage.setItem("cbot_chk_accent", $(this).prop('checked'));
    });
    $("#chk-stt-popup").click(function () {
        localStorage.setItem("cbot_chk_popup", $(this).prop('checked'));
    });


    function get_message_bot(text) {
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
                    generate_message(result, 'bot');
                },
            });
    }

    function generate_message(msg, type) {
        var msg2 = '';
        INDEX++;
        if (type == 'bot') {
            if (msg.includes("[{")) {
                msg = JSON.parse(msg);
                msg2 = templateCB.renderCarousel2(INDEX, msg, 10);
                msg = templateCB.renderCarousel(INDEX, msg, 10)

            }
        }

        var str = "";
        str += "<div id='cm-msg-" + INDEX + "' class='chat-msg " + type + "'>";
        if (type == 'bot') {
            str += "<span class='msg-avatar'>";
            str += "<img src='" + _Host + "Content/img/user_bot.jpg'>";
            str += "</span>";
        }
        str += "<div class='cm-msg-text'>";
        str += msg;
        str += "</div>";
        str += "</div>";
        str += msg2;
        $(".chat-logs").append(str);

        $("#cm-msg-" + INDEX).hide().fadeIn(300);

        if (type == 'self') {
            $("#chat-input").val('');
        }

        $(".chat-logs").stop().animate({ scrollTop: $(".chat-logs")[0].scrollHeight }, 1000);

        $(".btn-postback-area").off().on('click', function (e) {
            e.preventDefault();
            var postback = "";
            if ($(this).text().trim() == "Sở hữu trí tuệ") {
                postback = 'SHTT';
            }
            else if ($(this).text().trim() == "Thuế-Phí-Lệ Phí") {
                postback = 'Thue';
            }
            else if ($(this).text().trim() == "Du lịch") {
                postback = 'DULICH';
            }
            generate_message(postback, 'self');
            setTimeout(function () {
                get_message_bot(postback);
            }, 500)
            console.log($(this).text());
            return false;
        })
    }

    function add3Dots(string, limit) {
        var dots = "...";
        if (string.length > limit) {
            string = string.substring(0, limit) + dots;
        }

        return string;
    }

    $("#chat-submit").click(function (e) {
        e.preventDefault();
        var msg = $("#chat-input").val();
        if (msg.trim() == '') {
            return false;
        }
        generate_message(msg, 'self');
        setTimeout(function () {
            if ($("#chk-stt-accent").prop('checked') == true) {
                get_message_bot_accent(msg);
            } else {
                get_message_bot(msg);
            }
        }, 500)

    })

    // icon setting non accent chatbo   
    function get_message_bot_accent(text) {
        $.ajax({
            url: _Host + '/api/convertVN?text=' + text,
            contentType: 'application/json; charset=utf-8',
            type: 'GET'
        }).done(function (response) {
            if (response == "ERROR_400") {
                console.log("Load data accent vietnamese not success")
                return false;
            }
            get_message_bot(response);
        })
    }


function show_close_cb_box() {
    $("#chat-circle").toggle('scale');
    $(".chat-box").toggle(800, "easeOutQuint");
}