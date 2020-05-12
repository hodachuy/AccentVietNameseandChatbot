//---------------------------- CHATBOX CUSTOMER EVENT ----------------------------//
/*
    
*/
var configs = {
    color: $("#botColor").val(),
    srcLogo: _Host + $("#botLogo").val(),
    isActiveGetStarted: false,
    isActiveAccentVN: JSON.parse(localStorage.getItem("cbot_chk_accent")),
    isActiveViewDetailPopup: JSON.parse(localStorage.getItem("cbot_chk_popup"))
    },
    api = {
        getAccentVN: "apiv1/convertVN",
        getMessageBot: "apiv1/chatbot"
    },
    message_nf = {
        T_01: "Xin lỗi, Tôi không hiểu",
        T_02: "Bạn có thể giải thích thêm được không?",
        T_03: "Tôi không thể tìm thấy, bạn có thể nói rõ hơn?",
        T_04: "Xin lỗi, Bạn có thể giải thích thêm được không?",
        T_05: "Rất tiếc! Tôi chưa được học để hiểu câu hỏi này",
        T_06: "Tôi chưa hiểu ạ, bạn nói rõ hơn được không?"
    },
    timeCurrent = new Date();

var TypeMessage = {
    Text: 'Text',
    Generic: 'Generic',
    Image: 'Image',
    File: 'File',
    Video: 'Video',
    QuickReply: 'QuickReply',
}

var ChatMessages = {
    Name: "ms1",
    MessageText: {
        Content: 'abc',
        Type: TypeMessage.Text
    },
    Delay: 1000,
    Align: "right",
    ShowTime: true,
    Time: timeCurrent
}

var objHub = $.connection.chatHub;

$(document).ready(function () {
    initChatHub();
})

var timeReconnecting = 5;
var vary = function intervalFunc() {
    document.getElementById("reconeting-time").innerHTML = timeReconnecting;
    timeReconnecting--;
    console.log(timeReconnecting);
    if (timeReconnecting == parseInt("1")) {
        clearInterval(this);
        $('.box-reconecting').removeClass('showing');
    }
}

function initChatHub() {
    $.connection.hub.logging = true;
    $.connection.hub.start({ transport: ['longPolling', 'webSockets'] });
    $.connection.hub.start().done(function () {
        console.log("signalr started")
    });
    $.connection.hub.error(function (error) {
        console.log('SignalR error: ' + error)
    });
    let tryingToReconnect = false;
    $.connection.hub.reconnecting(function () {
        tryingToReconnect = true;
        console.log('SingalR connect đang kết nối lại')
        $('.box-reconecting').addClass('showing');
        setInterval(vary, 1500);
    });

    $.connection.hub.reconnected(function () {
        tryingToReconnect = false;
        console.log('SingalR connect đã kết nối lại')
    });

    $.connection.hub.disconnected(function () {
        console.log('SingalR connect ngắt kết nối')
        if (tryingToReconnect) {
            setTimeout(function () {
                console.log('SingalR connect đang khởi động lại')
                $.connection.hub.start({ transport: ['longPolling', 'webSockets'] });
                $.connection.hub.start().done(function () {
                });
            }, 5000); // Restart connection after 5 seconds.          
        }
    });
}


$(function () {
    cboxEvent.init();
});
var cboxEvent = {
    init: function () {
        this.events();
        //this.setting();
    },
    setting: function () {
        configs.isActiveAccentVN = true;
        $('#chk-stt-accent').prop('checked', configs.isActiveAccentVN);
        $("#chk-stt-popup").prop('checked', configs.isActiveViewDetailPopup);
    },
    events: function () {
        // input text
        $('#input-msg-text').keyup(function (e) {
            if (Readers.AccountID == "") {
                return false;
            }
            if (Readers.GroupID != GROUP_READER) {
                return false;
            }
            var edValue = $(this);
            var text = edValue.text();
            var threadID = $("#input-thread-" + id).val();
            if (objLawyer[0].IsStopWriting == false) {
                if (text.length > 0) {
                    objHub.server.onWritingOfReader(threadID, Readers.AccountID, false);
                    objLawyer[0].IsStopWriting = true;
                }
            } else {
                if (text.length == 0) {
                    objHub.server.onWritingOfReader(threadID, Readers.AccountID, true);
                    objLawyer[0].IsStopWriting = false;
                }
            }
        })

        $('#input-msg-text').keydown(function (e) {
            if (Readers.AccountID == "") {
                if (document.getElementById('windowpopup2') === null) {
                    AlertDialog("Thông báo", "Vui lòng đăng nhập để sử dụng tính năng này!", function () {
                    });
                }
                return false;
            }
            if (Readers.GroupID != GROUP_READER) {
                if (Readers.GroupID == GROUP_ADMIN) {
                    if (document.getElementById('windowpopup2') === null) {
                        AlertDialog("Thông báo", "Bạn đang sử dụng tài khoản quản trị,vui lòng đăng nhập tài khoản độc giả để sự dụng tính năng này!", function () {
                        });
                    }
                }
                if (Readers.GroupID == GROUP_LAWYER) {
                    if (document.getElementById('windowpopup2') === null) {
                        AlertDialog("Thông báo", "Bạn đang sử dụng tài luật sư,vui lòng đăng nhập tài khoản độc giả để sự dụng tính năng này!", function () {
                        });
                    }
                }
                return false;
            }

            var edValue = $(this);
            var text = edValue.html();
            if (e.which == 13) {
                e.preventDefault(e);
                $($(this).next().eq(0)).css("display", "none");
                if (text !== "") {
                    insertChat("reader", objLawyer[0].Image, isValidURLandCodeIcon(text), 0, "" + id + "");
                    $(this).text('');
                    $("#btn-input-" + id).val('');
                    // gửi tin nhắn
                    objHub.server.sendMessageToGroup(Readers.FullName, isValidURLandCodeIcon(text), $("#input-thread-" + id).val(), Readers.AccountID, "true");
                }
            }
        })
       

        // close form
        $('body').on('click', '.cbox-close', function (e) {
            parent.postMessage("close", "*");
        })
        window.addEventListener('message', function (event) {
            var widthParent = parseInt(event.data);
            if (widthParent <= 768) {
                $('.chatbox-main').css('padding', '0em');
            }
        }, false);

        // event click option cbox
        $("#chk-stt-accent").click(function () {
            localStorage.setItem("cbot_chk_accent", $(this).prop('checked'));
        });
        $("#chk-stt-popup").click(function () {
            localStorage.setItem("cbot_chk_popup", $(this).prop('checked'));
        });
    }
}
var msgEvent = {
    sendMessage: function (text) {
        //connection.invoke("SendMessage", "user 1", text).catch(function (err) {
        //    return console.error(err.toString());
        //});
        //  $(msginner).delay(chatDelay3).fadeIn(); append them fadeIn() thêm hiệu ứng hiển thị chậm chậm
    },
    appendMessage: function (element, timeout, text, isSendTyping) {
        if (!isSendTyping) {
            isSendTyping = false;
        }
        $(element).delay(timeout).queue(function (next) {
            $("._4xkn_writing").remove();
            $(this).append(text);
            //console.log(isSendTyping)
            if (isSendTyping) msgEvent.typing();
            scrollBar();
            next();
        });
    },
    getMessageAccentVN: function (text) {
        var rs = "";
        $.ajax({
            url: "https://bot.digipro.vn/" + api.getAccentVN + '?text=' + text,
            contentType: 'application/json; charset=utf-8',
            type: 'GET',
            async: false,
            global: false,
            dataType: "json",
        }).done(function (response) {
            //console.log(response)
            if (response == "ERROR_400") {
                //console.log("Load data accent vietnamese not success")
                return rs;
            }
            if (response != "") {
                response = response.replace('\n', '.');
                if (text.toLowerCase() != response) {
                    rs = response;
                }
            }
        })
        return rs;
    },
    getMessageStarted: function () {
        if ($("#haveIntroduct").val() == "True") {
            if (configs.isActiveGetStarted == false) {
                // typing
                msgEvent.typing();
                //start
                var content = '';
                if ($("#botTextIntro").val() != '') {
                    content = $("#botTextIntro").val();
                }
                if ($("#cardID").val() != null || $("#cardID").val() != '') {
                    content = "postback_card_" + $("#cardID").val();
                }
                setTimeout(function () {
                    msgEvent.getMessageBot(content);
                }, 1500)
                configs.isActiveGetStarted = true;
            }
        }
    },
    getMessageBot: function (text, titlePostback) {
        var param = {
            text: text,
            token: $('#userID').val(),
            botId: $('#botID').val(),
            titlePostback: titlePostback
        }
        param = JSON.stringify(param)
        $.ajax({
            url: _Host + api.getMessageBot,
            contentType: 'application/json; charset=utf-8',
            data: param,
            type: 'POST',
            success: function (result) {
                //console.log(result)
                var isHaveSymptomsAndNotMatch = false;
                var lstMessage = result.message,
                    tplPostback = result.postback[0],
                    resultAPI = result.messageLstSearchNLP,
                    resultLstMedSymptoms = result.messageLstSymptoms,
                    isSearchNLP = result.isSearchNLP;
                if (result.isHaveSymptomsAndNotMatch) {
                    isHaveSymptomsAndNotMatch = result.isHaveSymptomsAndNotMatch || false;
                }
                if (isHaveSymptomsAndNotMatch == true && resultLstMedSymptoms != null && resultLstMedSymptoms.length != 0) {
                    var tempCarouselMedSymptoms = new msgEvent.renderBot().TemplateTextCarouselMedSymptoms(resultLstMedSymptoms);
                    msgEvent.appendMessage(".conversationContainer", 1500, new msgEvent.renderBot().TemplateText('Bạn vui lòng xem thêm thông tin triệu chứng bên dưới'), true)
                    msgEvent.appendMessage(".conversationContainer", 2000, tempCarouselMedSymptoms)
                    setTimeout(function () {
                        if (tplPostback !== null) {
                            tplPostback = tplPostback.replace(/{{color}}/g, configs.color);
                            $("#_12cd_event_button").empty().append(tplPostback);
                            scrollBar();
                        }
                    }, 2000)
                }
                if (resultAPI != null && resultAPI.length != 0 && isSearchNLP == true) {
                    var tempCarouselQnA = new msgEvent.renderBot().TemplateTextCarouselQnA(resultAPI);
                    msgEvent.appendMessage(".conversationContainer", 1500, new msgEvent.renderBot().TemplateText('Tôi tìm thấy ' + resultAPI.length + ' câu hỏi liên quan đến câu hỏi của bạn.'), true)
                    msgEvent.appendMessage(".conversationContainer", 2000, tempCarouselQnA)
                    setTimeout(function () {
                        if (tplPostback !== null) {
                            tplPostback = tplPostback.replace(/{{color}}/g, configs.color);
                            $("#_12cd_event_button").empty().append(tplPostback);
                            scrollBar();
                        }
                    }, 2000)
                    return;
                }
                if (isHaveSymptomsAndNotMatch == false) {
                    if (lstMessage.length == 1) {
                        let msg = lstMessage[0];
                        switch (msg) {
                            case "NOT_MATCH_01":
                                msg = new msgEvent.renderBot().TemplateText(message_nf.T_01);
                                break;
                            case "NOT_MATCH_02":
                                msg = new msgEvent.renderBot().TemplateText(message_nf.T_02);
                                break;
                            case "NOT_MATCH_03":
                                msg = new msgEvent.renderBot().TemplateText(message_nf.T_03);
                                break;
                            case "NOT_MATCH_04":
                                msg = new msgEvent.renderBot().TemplateText(message_nf.T_04);
                                break;
                            case "NOT_MATCH_05":
                                msg = new msgEvent.renderBot().TemplateText(message_nf.T_05);
                                break;
                            case "NOT_MATCH_06":
                                msg = new msgEvent.renderBot().TemplateText(message_nf.T_06);
                                break;
                        }

                        msg = msg.replace(/{{color}}/g, configs.color);
                        msg = msg.replace(/{{image_logo}}/g, configs.srcLogo);
                        msgEvent.appendMessage(".conversationContainer", 1500, msg)
                        if (tplPostback !== null) {
                            tplPostback = tplPostback.replace(/{{color}}/g, configs.color);
                            setTimeout(function () {
                                $("#_12cd_event_button").empty().append(tplPostback);
                                scrollBar();
                            }, 1500)
                        }
                    }
                    if (lstMessage.length > 1) {
                        $.each(lstMessage, function (index, value) {
                            index = index + 1;
                            let isSendTyping = true;
                            let msg = value;
                            msg = msg.replace(/{{color}}/g, configs.color);
                            msg = msg.replace(/{{image_logo}}/g, configs.srcLogo);
                            if (index == lstMessage.length) {
                                isSendTyping = false;
                            }
                            msgEvent.appendMessage(".conversationContainer", 1500, msg, isSendTyping)
                        })
                        var newTimeDelay = parseInt(lstMessage.length * 1500);
                        setTimeout(function () {
                            $("._4xkn_writing").remove();
                            if (tplPostback !== null) {
                                tplPostback = tplPostback.replace(/{{color}}/g, configs.color);
                                $("#_12cd_event_button").empty().append(tplPostback);
                                scrollBar();
                            }
                        }, newTimeDelay)
                    }

                    if (resultLstMedSymptoms != null && resultLstMedSymptoms.length != 0) {
                        var tempCarouselMedSymptoms = new msgEvent.renderBot().TemplateTextCarouselMedSymptoms(resultLstMedSymptoms);
                        msgEvent.appendMessage(".conversationContainer", 1500, new msgEvent.renderBot().TemplateText('Bạn vui lòng xem thêm thông tin triệu chứng bên dưới'), true)
                        msgEvent.appendMessage(".conversationContainer", 2000, tempCarouselMedSymptoms)
                        setTimeout(function () {
                            if (tplPostback !== null) {
                                tplPostback = tplPostback.replace(/{{color}}/g, configs.color);
                                $("#_12cd_event_button").empty().append(tplPostback);
                                scrollBar();
                            }
                        }, 2000)
                    }
                }
                scrollBar();
            }
        });
    },
    typing: function () {
        $("#_12cd_event_button").empty();
        var html = '<div class="_4xkn _4xkn_writing clearfix">' +
            '               <div class="profilePictureColumn" style="bottom:0px;">' +
            '                    <div class="_4cqr">' +
            '                         <img class="profilePicture img" src="' + configs.srcLogo + '" alt="">' +
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
        //return html;
        $(".conversationContainer").append(html);
    },
    renderBot: function () {
        this.TemplateText = function (text) {
            var htmlText = '<div class="_4xkn clearfix">' +
                '               <div class="profilePictureColumn" style="bottom:0px;">' +
                '                    <div class="_4cqr">' +
                '                         <img class="profilePicture img" src="' + configs.srcLogo + '" alt="">' +
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
        },
            this.TemplateTextConvertAccentVN = function (text) {
                var htmlText = '<div class="_4xkn clearfix">' +
                    '               <div class="profilePictureColumn" style="bottom:0px;">' +
                    '                    <div class="_4cqr">' +
                    '                         <img class="profilePicture img" src="' + configs.srcLogo + '" alt="">' +
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
            },
            this.TemplateTextCarouselQnA = function (lstData) {
                var tempModuleHtml = '';
                var itemHtml = '';
                $.each(lstData, function (index, value) {
                    itemHtml += '<div class="_2zgz">';
                    itemHtml += '<div class="_6j2h">';
                    itemHtml += '<div class="_6j2i">';
                    itemHtml += '<div class="_6j2g">';
                    itemHtml += '<style>_4ik5._4ik4:nth-child(1)::after{content:""}</style>'
                    itemHtml += '<div class="_6j0t _4ik4 _4ik5" style="-webkit-line-clamp: 3;visibility: unset !important;">' + (value.question != null ? add3Dots(value.question, 120) : "") + '</div>';
                    itemHtml += '<div class="_6j0v">';
                    itemHtml += '<div class="_6j0u _6j0w">' + (value.field != null ? "" : "") + '</div>';
                    itemHtml += '<div class="_6j0u _6j0x _4ik4 _4ik5" style="-webkit-line-clamp: 2;">';
                    itemHtml += '<div>' + (value.field != null ? "" : "") + '</div>';
                    itemHtml += '</div>';
                    itemHtml += '</div>';
                    itemHtml += '</div>';
                    itemHtml += '<div class="_6ir5">';
                    itemHtml += '<div class="_4bqf _6ir3">';
                    itemHtml += '<a class="_6ir4 _6ir4_popup qa_law" data-id="' + value.id + '" href="https://bot.surelrn.vn/home/faq?id=' + value.id + '" target="_blank"  rel="nofollow noopener" data-lynx-mode="hover" style="color: rgb(234, 82, 105);">Xem chi tiết</a>';
                    itemHtml += '</div>';
                    itemHtml += '</div>';
                    itemHtml += '</div>';
                    itemHtml += '</div>';
                    itemHtml += '</div>';
                })
                tempModuleHtml += ' <div class="_4xkn clearfix">';
                tempModuleHtml += '<div class="profilePictureColumn" style="bottom: 0px;">';
                tempModuleHtml += '<div class="_4cqr">';
                tempModuleHtml += '<img class="profilePicture img" src="' + configs.srcLogo + '" alt="">';
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
                if (lstData.length > 1) {
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
                return tempModuleHtml;
            },
            this.TemplateTextCarouselMedSymptoms = function (lstData) {
                var tempModuleHtml = '';
                var itemHtml = '';
                $.each(lstData, function (index, value) {
                    itemHtml += '<div class="_2zgz">';
                    itemHtml += '<div class="_6j2h">';
                    itemHtml += '<div class="_6j2i">';
                    itemHtml += '<div class="_6j2g">';
                    itemHtml += '<style>_4ik5._4ik4:nth-child(1)::after{content:""}</style>'
                    itemHtml += '<div class="_6j0t _4ik4 _4ik5" style="-webkit-line-clamp: 3;visibility: unset !important">' + (value.description != null ? add3Dots(value.description, 120) : "") + '</div>';
                    itemHtml += '<div class="_6j0v">';
                    itemHtml += '<div class="_6j0u _6j0w">' + (value.name != null ? "" : "") + '</div>';
                    itemHtml += '<div class="_6j0u _6j0x _4ik4 _4ik5" style="-webkit-line-clamp: 2;">';
                    itemHtml += '<div>' + (value.name != null ? "" : "") + '</div>';
                    itemHtml += '</div>';
                    itemHtml += '</div>';
                    itemHtml += '</div>';
                    itemHtml += '<div class="_6ir5">';
                    itemHtml += '<div class="_4bqf _6ir3">';
                    itemHtml += '<a class="_6ir4 _6ir4_popup qa_law" data-id="' + value.id + '" href="https://bot.surelrn.vn/home/FaqMedSymptoms?id=' + value.id + '" target="_blank"  rel="nofollow noopener" data-lynx-mode="hover" style="color: rgb(234, 82, 105);">Xem chi tiết</a>';
                    itemHtml += '</div>';
                    itemHtml += '</div>';
                    itemHtml += '</div>';
                    itemHtml += '</div>';
                    itemHtml += '</div>';
                })
                tempModuleHtml += ' <div class="_4xkn clearfix">';
                tempModuleHtml += '<div class="profilePictureColumn" style="bottom: 0px;">';
                tempModuleHtml += '<div class="_4cqr">';
                tempModuleHtml += '<img class="profilePicture img" src="' + configs.srcLogo + '" alt="">';
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
                if (lstData.length > 1) {
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
                return tempModuleHtml;
            }
    },
    renderUser: function (text) {
        var html = '<div class="_4xkn clearfix">' +
            '<div class="messages">' +
            '    <div class="_21c3">' +
            '        <h4 class="datebreak _497p _2lpt hide"><time class="_3oh-">T6 16:52</time></h4>' +
            '        <div class="clearfix _2a0-">' +
            '            <div class="_4xko _4xks" tabindex="0" role="button" style="background-color: ' + configs.color + 'font-family: Segoe UI Light;">' +
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
        $(".conversationContainer").append(html);
        //return html;
    }
}


function scrollBar() {
    $(".uiScrollableAreaWrap").scrollTop($(".uiScrollableAreaWrap").prop('scrollHeight'));
}

function add3Dots(string, limit) {
    var dots = "...";
    if (string.length > limit) {
        string = string.substring(0, limit) + dots;
    }
    return string;
}

window.addEventListener('message', function (event) {
    var widthParent = parseInt(event.data);
    //console.log(event.data)
    if (widthParent <= 425) {
        $("._3-8j").css('margin', '0px 0px 0px');
        $("._6atl").css('height', '100%');
        $("._6atl").css('width', event.data);
        $("._6ati").css('height', '100%');
        $("._6ati").css('width', event.data);
    }
    msgEvent.getMessageStarted();
    //if (event.origin === 'http://localhost:47887') {
    //    console.log(event.origin)
    //    console.log('message received:  ' + event.data, event);
    //};
}, false);




//------------------TRACKING--------------------//


(function (window) {
    {
        var unknown = '-';

        // screen
        var screenSize = '';
        if (screen.width) {
            width = (screen.width) ? screen.width : '';
            height = (screen.height) ? screen.height : '';
            screenSize += '' + width + " x " + height;
        }

        // browser
        var url = window.location.href;
        var nVer = navigator.appVersion;
        var nAgt = navigator.userAgent;
        var browser = navigator.appName;
        var version = '' + parseFloat(navigator.appVersion);
        var majorVersion = parseInt(navigator.appVersion, 10);
        var nameOffset, verOffset, ix;

        // Opera
        if ((verOffset = nAgt.indexOf('Opera')) != -1) {
            browser = 'Opera';
            version = nAgt.substring(verOffset + 6);
            if ((verOffset = nAgt.indexOf('Version')) != -1) {
                version = nAgt.substring(verOffset + 8);
            }
        }
        // Opera Next
        if ((verOffset = nAgt.indexOf('OPR')) != -1) {
            browser = 'Opera';
            version = nAgt.substring(verOffset + 4);
        }
            // Edge
        else if ((verOffset = nAgt.indexOf('Edge')) != -1) {
            browser = 'Microsoft Edge';
            version = nAgt.substring(verOffset + 5);
        }
            // MSIE
        else if ((verOffset = nAgt.indexOf('MSIE')) != -1) {
            browser = 'Microsoft Internet Explorer';
            version = nAgt.substring(verOffset + 5);
        }
            // Chrome
        else if ((verOffset = nAgt.indexOf('Chrome')) != -1) {
            browser = 'Chrome';
            version = nAgt.substring(verOffset + 7);
        }
            // Safari
        else if ((verOffset = nAgt.indexOf('Safari')) != -1) {
            browser = 'Safari';
            version = nAgt.substring(verOffset + 7);
            if ((verOffset = nAgt.indexOf('Version')) != -1) {
                version = nAgt.substring(verOffset + 8);
            }
        }
            // Firefox
        else if ((verOffset = nAgt.indexOf('Firefox')) != -1) {
            browser = 'Firefox';
            version = nAgt.substring(verOffset + 8);
        }
            // MSIE 11+
        else if (nAgt.indexOf('Trident/') != -1) {
            browser = 'Microsoft Internet Explorer';
            version = nAgt.substring(nAgt.indexOf('rv:') + 3);
        }
            // Other browsers
        else if ((nameOffset = nAgt.lastIndexOf(' ') + 1) < (verOffset = nAgt.lastIndexOf('/'))) {
            browser = nAgt.substring(nameOffset, verOffset);
            version = nAgt.substring(verOffset + 1);
            if (browser.toLowerCase() == browser.toUpperCase()) {
                browser = navigator.appName;
            }
        }
        // trim the version string
        if ((ix = version.indexOf(';')) != -1) version = version.substring(0, ix);
        if ((ix = version.indexOf(' ')) != -1) version = version.substring(0, ix);
        if ((ix = version.indexOf(')')) != -1) version = version.substring(0, ix);

        majorVersion = parseInt('' + version, 10);
        if (isNaN(majorVersion)) {
            version = '' + parseFloat(navigator.appVersion);
            majorVersion = parseInt(navigator.appVersion, 10);
        }

        // mobile version
        var mobile = /Mobile|mini|Fennec|Android|iP(ad|od|hone)/.test(nVer);

        // cookie
        var cookieEnabled = (navigator.cookieEnabled) ? true : false;

        if (typeof navigator.cookieEnabled == 'undefined' && !cookieEnabled) {
            document.cookie = 'testcookie';
            cookieEnabled = (document.cookie.indexOf('testcookie') != -1) ? true : false;
        }

        // system
        var os = unknown;
        var clientStrings = [
            { s: 'Windows 10', r: /(Windows 10.0|Windows NT 10.0)/ },
            { s: 'Windows 8.1', r: /(Windows 8.1|Windows NT 6.3)/ },
            { s: 'Windows 8', r: /(Windows 8|Windows NT 6.2)/ },
            { s: 'Windows 7', r: /(Windows 7|Windows NT 6.1)/ },
            { s: 'Windows Vista', r: /Windows NT 6.0/ },
            { s: 'Windows Server 2003', r: /Windows NT 5.2/ },
            { s: 'Windows XP', r: /(Windows NT 5.1|Windows XP)/ },
            { s: 'Windows 2000', r: /(Windows NT 5.0|Windows 2000)/ },
            { s: 'Windows ME', r: /(Win 9x 4.90|Windows ME)/ },
            { s: 'Windows 98', r: /(Windows 98|Win98)/ },
            { s: 'Windows 95', r: /(Windows 95|Win95|Windows_95)/ },
            { s: 'Windows NT 4.0', r: /(Windows NT 4.0|WinNT4.0|WinNT|Windows NT)/ },
            { s: 'Windows CE', r: /Windows CE/ },
            { s: 'Windows 3.11', r: /Win16/ },
            { s: 'Android', r: /Android/ },
            { s: 'Open BSD', r: /OpenBSD/ },
            { s: 'Sun OS', r: /SunOS/ },
            { s: 'Chrome OS', r: /CrOS/ },
            { s: 'Linux', r: /(Linux|X11(?!.*CrOS))/ },
            { s: 'iOS', r: /(iPhone|iPad|iPod)/ },
            { s: 'Mac OS X', r: /Mac OS X/ },
            { s: 'Mac OS', r: /(MacPPC|MacIntel|Mac_PowerPC|Macintosh)/ },
            { s: 'QNX', r: /QNX/ },
            { s: 'UNIX', r: /UNIX/ },
            { s: 'BeOS', r: /BeOS/ },
            { s: 'OS/2', r: /OS\/2/ },
            { s: 'Search Bot', r: /(nuhk|Googlebot|Yammybot|Openbot|Slurp|MSNBot|Ask Jeeves\/Teoma|ia_archiver)/ }
        ];
        for (var id in clientStrings) {
            var cs = clientStrings[id];
            if (cs.r.test(nAgt)) {
                os = cs.s;
                break;
            }
        }

        var osVersion = unknown;

        if (/Windows/.test(os)) {
            osVersion = /Windows (.*)/.exec(os)[1];
            os = 'Windows';
        }

        switch (os) {
            case 'Mac OS X':
                osVersion = /Mac OS X (10[\.\_\d]+)/.exec(nAgt)[1];
                break;

            case 'Android':
                osVersion = /Android ([\.\_\d]+)/.exec(nAgt)[1];
                break;

            case 'iOS':
                osVersion = /OS (\d+)_(\d+)_?(\d+)?/.exec(nVer);
                osVersion = osVersion[1] + '.' + osVersion[2] + '.' + (osVersion[3] | 0);
                break;
        }

        // flash (you'll need to include swfobject)
        /* script src="//ajax.googleapis.com/ajax/libs/swfobject/2.2/swfobject.js" */
        var flashVersion = 'no check';
        if (typeof swfobject != 'undefined') {
            var fv = swfobject.getFlashPlayerVersion();
            if (fv.major > 0) {
                flashVersion = fv.major + '.' + fv.minor + ' r' + fv.release;
            }
            else {
                flashVersion = unknown;
            }
        }
    }
    window.jscd = {
        screen: screenSize,
        browser: browser,
        browserVersion: version,
        browserMajorVersion: majorVersion,
        mobile: mobile,
        os: os,
        osVersion: osVersion,
        cookies: cookieEnabled,
        flashVersion: flashVersion,
        url: url
    };
}(this));

console.log(
    'OS: ' + jscd.os + ' ' + jscd.osVersion + '\n' +
    'Browser: ' + jscd.browser + ' ' + jscd.browserMajorVersion +
    ' (' + jscd.browserVersion + ')\n' +
    'Mobile: ' + jscd.mobile + '\n' +
    'Flash: ' + jscd.flashVersion + '\n' +
    'Cookies: ' + jscd.cookies + '\n' +
    'Screen Size: ' + jscd.screen + '\n\n' +
    'Full User Agent: ' + navigator.userAgent + '\n\n' +
    'Log url: ' + jscd.url
);

var latilongTude = '';
$.get("https://ipinfo.io?token=d4b73a8d673d31", function (response) {
    console.log(response);
    latilongTude = response.loc;
}, "jsonp");

// SEND CUSTOMER TO DB
