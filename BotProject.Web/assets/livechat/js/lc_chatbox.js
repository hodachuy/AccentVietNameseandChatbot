
console.log(
    'OS: ' + jscd.os + ' ' + jscd.osVersion + '\n' +
    'Browser: ' + jscd.browser + ' ' + jscd.browserMajorVersion +
    ' (' + jscd.browserVersion + ')\n' +
    'Mobile: ' + jscd.mobile + '\n' +
    'Flash: ' + jscd.flashVersion + '\n' +
    'Cookies: ' + jscd.cookies + '\n' +
    'Screen Size: ' + jscd.screen + '\n\n' +
    'Full User Agent: ' + navigator.userAgent+ '\n\n' +
    'IP: ' + ipInfo.ip + '\n' +
    'City: ' + ipInfo.city + '\n' +
    'Region: ' + ipInfo.region + '\n' +
    'Latitude: ' + ipInfo.latitude + '\n' +
    'Longtitude: ' + ipInfo.longtitude
);

var CustomerModel = {
    ID: _customerId,
    ApplicationChannels: 0, //[0: Web, 1: Facebook, 2: Zalo, 3: Kiosk]
    ChannelGroupID: _channelGroupId,
    ThreadID:''
};

var AgentModel = {
    ID:'',
    ChannelGroupID: _channelGroupId,
    Status:'',
}
var DeviceModel = {
        ID: 0,
        OS: jscd.os + ' ' + jscd.osVersion,
        Browser: jscd.browser + ' ' + jscd.browserMajorVersion+ ' (' + jscd.browserVersion + ')',
        IsMobile: jscd.mobile,
        FullUserAgent: navigator.userAgent,
        IPAddress: ipInfo.ip,
        City: ipInfo.city,
        Region: ipInfo.region, 
        Latitude: ipInfo.latitude,
        Longtitude: ipInfo.longtitude
}

function saveCustomer() {
    var formData = new FormData();
    formData.append('customer', JSON.stringify(CustomerModel));
    formData.append('device', JSON.stringify(DeviceModel));
    $.ajax({
        url: _Host + "api/lc_customer/create",
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false,
        dataType: "json",
        success: function (result) {
            if (result) {
                console.log('OK')
                window.localStorage.setItem("lc_isSaveCustomer", true);
            }
        },
        error: function (e) {
            console.log(e)
        }
    });
};

$(document).ready(function () {
    // nếu lưu rồi k nên gọi vào tiếp
    _isSaveCustomer = window.localStorage.getItem("lc_isSaveCustomer");
    if (_isSaveCustomer == 'false') {
        saveCustomer();
    }
})

//----------------------------EVENT CHATBOX CUSTOMER ----------------------------//
/*
    
*/

var isAgentOnline = false,
    chatBot = {
        botId: '',
        isActive: false
    };

var isStopTyping = false;

var TYPE_USER_CONNECT = {
    CUSTOMER: 'customer',
    AGENT: 'agent',
    BOT:'bot'
}

var intervalReconnectId,
    timeReconnecting = 6;

var interval_focus_tab_id;

var objHub = $.connection.chatHub;

$(document).ready(function () {

    //check agent online
    isAgentOnline = checkAgentOnline();

    //check have bot active
    chatBot = checkBotActive();
    console.log(chatBot)
    // Dang ky su kien chatHub
    cBoxHub.register();
    cBoxHub.receivedSignalFromServer();
    cBoxHub.validateFocusTabChat();

    cBoxMessage.event();

})

var varyReconnected = function intervalFunc() {
    timeReconnecting--;
    document.getElementById("reconeting-time").innerHTML = timeReconnecting;
    console.log(timeReconnecting);
    if (timeReconnecting == 0) {
        clearInterval(intervalReconnectId);
        $('.box-reconecting').removeClass('showing');
    }
}
var cBoxHub = {
    register: function () {
        $.connection.hub.logging = true;
        $.connection.hub.qs = 'isCustomerConnected=true';
        $.connection.hub.start({ transport: ['longPolling', 'webSockets'] });
        $.connection.hub.start().done(function () {
            console.log("signalr started")
            // kết nối chat khi agent hoặc bot active
            if (isAgentOnline == true || chatBot.isActive == true) {
                objHub.server.connectCustomerToChannelChat(_customerId, _channelGroupId);
            }
        });
        $.connection.hub.error(function (error) {
            console.log('SignalR error: ' + error)
        });

        let tryingToReconnect = false;

        $.connection.hub.reconnecting(function () {
            tryingToReconnect = true;
            console.log('SingalR connect đang kết nối lại')
            $('.box-reconecting').addClass('showing');
            intervalReconnectId = setInterval(varyReconnected, 1500);
        });

        $.connection.hub.reconnected(function () {
            tryingToReconnect = false;
            console.log('SingalR connect đã kết nối lại')
        });

        $.connection.hub.disconnected(function () {
            console.log('SingalR connect ngắt kết nối')
            if ($.connection.hub.lastError) {
                console.log("Disconnected. Reason: " + $.connection.hub.lastError.message);

                $('.box-reconecting').addClass('showing');
                intervalReconnectId = setInterval(varyReconnected, 1500);

                setTimeout(function () {
                    console.log('SingalR connect đang khởi động lại')
                    $.connection.hub.start({
                        transport: ['longPolling', 'webSockets']
                    });
                    $.connection.hub.start().done(function () { });
                }, 5000); // Restart connection after 5 seconds.          
            }
            if (tryingToReconnect) {
                setTimeout(function () {
                    console.log('SingalR connect đang khởi động lại')
                    $.connection.hub.start({ transport: ['longPolling', 'webSockets'] });
                    $.connection.hub.start().done(function () {

                    });
                }, 5000); // Restart connection after 5 seconds.          
            }
        });

        // gọi Onconnected
        objHub.client.connected = function () {
            console.log('connected')
        }
        // gọi Onconnected
        objHub.client.disconnected = function () {
            console.log('disconnected')
        }
    },
    receivedSignalFromServer: function () {
        objHub.client.receiveMessages = function (channelGroupId, threadId, message, agentId, customerId, agentName, typeUser) {
            console.log('threadId:' + threadId + '  agentId-agentName' + agentId + ' : ' + message)
            insertChat("agent", isValidURLandCodeIcon(message), agentName, "");

        };
        objHub.client.receiveTyping = function (channelGroupId, agentId) {
            console.log('agent-' + agentId + ' typing')
        };
        objHub.client.receiveThreadChat = function (threadId, customerId) {
            console.log('revice- thread' + threadId)
            CustomerModel.ThreadID = threadId;
        };
        objHub.client.receiveSingalChatWithBot = function (channelGroupId, threadId, customerId, botId) {
            console.log('revice- signal chat with bot' + botId)
            CustomerModel.ThreadID = threadId;
            chatBot.botId = botId;
            chatBot.isActive = true;
        };
    },
    validateFocusTabChat: function () {
        // Kiểm tra customer có hoạt dộng trên tab trình duyệt chat
        $(window).focus(function () {
            if (!interval_focus_tab_id) {
                interval_focus_tab_id = setInterval(function () {
                    console.log("customer hoat dong");
                    let isFocusTab = true;
                    objHub.server.checkCustomerFocusTabChat(_channelGroupId, CustomerModel.ThreadID, CustomerModel.ID, isFocusTab);
                    clearInterval(interval_focus_tab_id);
                }, 1000);
            }
        });
        // Nếu không xem màn hình
        $(window).blur(function () {
            clearInterval(interval_focus_tab_id);
            interval_focus_tab_id = 0;
            let isFocusTab = false;
            objHub.server.checkCustomerFocusTabChat(_channelGroupId, CustomerModel.ThreadID, CustomerModel.ID, isFocusTab);
            console.log("customer k hoat dong")
        });
    }
}

var checkAgentOnline = function () {
    var isCheck = false;
    var params = {
        channelGroupId: _channelGroupId
    }
    params = JSON.stringify(params);
    $.ajax({
        url: _Host + "api/lc_agent/getListAgentOnline",
        contentType: 'application/json; charset=utf-8',
        data: params,
        async: false,
        global: false,
        type: 'POST',
        success: function (data) {
            if (data.length != 0) {
                isCheck = true;
            }
        }
    })
    return isCheck;
}
var checkBotActive = function () {
    var chatBot = {
            botId: '',
            isActive: false
    };

    var params = {
        channelGroupId: _channelGroupId
    }
    params = JSON.stringify(params);
    $.ajax({
        url: _Host + "api/bot/getBotActiveLiveChat",
        contentType: 'application/json; charset=utf-8',
        data: params,
        async: false,
        global: false,
        type: 'POST',
        success: function (data) {
            if (data.status) {
                chatBot.botId = data.botId;
                chatBot.isActive = true;
            }
        }
    })
    return chatBot;
}


// EVENT BOX
var cBoxMessage = {
    init :function(){

    },
    event: function () {
        // close box
        $('body').on('click', '#btn-cbox-close', function (e) {
            parent.postMessage("close", "*");
        })

        $($($("#input-chat-message").next()).eq(0)).keyup(function (e) {
            var edValue = $(this);
            var text = edValue.text();
            if (text.length > 0) {
                if (isStopTyping == false) {
                    objHub.server.sendTyping(_channelGroupId, CustomerModel.ThreadID, CustomerModel.ID);
                    isStopTyping = true;
                }
            } else {
                isStopTyping = false;
            }
        })

        $($($("#input-chat-message").next()).eq(0)).keydown(function (e) {
            var edValue = $(this);
            var text = edValue.html();
            if (e.which == 13) {
                e.preventDefault(e);
                if (text !== "") {


                    insertChat("customer", isValidURLandCodeIcon(text), "", "");
                    // gửi tin nhắn
                    objHub.server.sendMessage(_channelGroupId, CustomerModel.ThreadID, isValidURLandCodeIcon(text), "", CustomerModel.ID, "", TYPE_USER_CONNECT.CUSTOMER);

                    $(this).val('');
                    $(this).text('');

                    if (chatBot.isActive == true) {
                        new messageBot.getMessage(text, CustomerModel.ThreadID, botId);
                    }

                    isStopTyping = false;
                    return;
                }
            }
        })

        $("#btn-submit-chat-message").on('click', function () {
            var edValue = $("#input-chat-message");
            var text = edValue.val();
            console.log(text)
            if (text !== "") {
                insertChat("customer", isValidURLandCodeIcon(text),"","");
                // gửi tin nhắn
                objHub.server.sendMessage(_channelGroupId, CustomerModel.ThreadID, isValidURLandCodeIcon(text),"", CustomerModel.ID, "", TYPE_USER_CONNECT.CUSTOMER);
                $(this).val('');
            }
            return;
        })
    },
    callAction: function () {
        this.sendMessage = function () {
        }
    }
    // call api
    // render template
}

function insertChat(who, text, userName, avatar) {
    let user_class_chat = (who == "customer" ? "me" : "agent");
    let date_current = showTimeChat();

    var $elementMessage = document.getElementsByClassName('message-item');
    if ($elementMessage !== undefined || $elementMessage !== null) {
        let $elementLastMessage = $($elementMessage[$elementMessage.length - 1]);
        let timeLastMessage = $elementLastMessage.find('.message-user-time').html();
        let identity_user = $elementLastMessage.attr('data-user');
        if ((identity_user == who) && (date_current == timeLastMessage)) {
            let elementLastMessageAppend = $elementLastMessage.find('.message-item-content').last();
            appendMessage(elementLastMessageAppend, who, text);
            return;
        }
    }

    content = '<div class="message-item ' + user_class_chat + '" data-user="' + who + '">';
    content += message.getUserIcon(who, userName, avatar);
    content += message.getHtmlMessageBody(who, userName, text, date_current);
    content += '</div>';

    // insert body chat
    appendMessage("", who, content);
    return false;
}

function appendMessage(elementLastMessageAppend, who, text) {
    if (elementLastMessageAppend !== "") {
        var content = '<div class="message-item-content">' + text + '</div>';
        $(elementLastMessageAppend).after($(content));
    } else {
        $("#message-content").append(text);
    }

    // remove action read, delivered, message not send
    if (who == TYPE_USER_CONNECT.AGENT) {
        $("#message-content").find('.message-item-action').remove();
    }
    // scroll to bottom
    setTimeout(function () {
        scrollBar();
    }, 200)
}

var message = {
    getUserIcon: function (who, userName, avatar) {
        if (who == "customer")
            return "";

        var firstNameCharacter = userName.substring(0, 1).toUpperCase();
        var templateAvatar = '';
        templateAvatar += '<div class="message-avatar">';
        templateAvatar += '<div class="message-avatar-customer">';
        templateAvatar += '<div class="pr-3">';
        templateAvatar += '<span class ="message-avatar-item avatar">';
        if (avatar == "") {
            //templateAvatar += '<span class ="avatar-title bg-primary rounded-circle">' + firstNameCharacter + '</span>';
            templateAvatar += '<img src="' + _Host + 'assets/livechat/images/user_agent-default.png" class="rounded-circle" alt="image">';
        } else {
            templateAvatar += '<img src="~/assets/client/img/avatar-admin.jpg" class="rounded-circle" alt="image">';
        }
        templateAvatar += '</span>';
        templateAvatar += '</div>';
        templateAvatar += '</div>';
        templateAvatar += '</div>';
        return templateAvatar;
    },
    getHtmlMessageBody: function (who, userName, text, date_current) {
        var templateMsg = '';
        templateMsg += '<div class="message-body">';
        templateMsg += '<div>';
        templateMsg += '<div class ="message-align">';
        templateMsg += '<span class="message-user-name font-size-08">' + userName + ' </span>';
        templateMsg += '<span class="message-user-time font-size-08">' + date_current + '</span>';
        templateMsg += '</div>';
        templateMsg += '</div>';
        templateMsg += '<div class="message-item-content">' + text + '</div>';
        if (who == "customer") {
            templateMsg += '<div class="message-item-action txt-align-left font-size-08">Delivered</div>';//Message not sent, , Read
        }
        templateMsg += '</div>';
        return templateMsg;
    }
}

var messageBot = {
    getMessage:function(text, threadId, botId){
        var params = {
            senderId: _customerId,
            botId: botId,
            text: text,
            channelGroupId: _channelGroupId,
            threadId: threadId
        }
        params = JSON.stringify(params);
        $.ajax({
            url: _Host + "api/lc_bot/receive",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: params,
            type: 'POST',
            success: function (reponse) {
                console.log(reponse)
                if (reponse.status == 2) {
                    var templateHtml = [];
                    $(response.data, function (index, value) {

                    })
                }
            }
        })
    },
    appendMessage: function (element, timeout, text, isSendTyping) {
        if (!isSendTyping) {
            isSendTyping = false;
        }
        $(element).delay(timeout).queue(function (next) {
            $(".message-item-typing").remove();
            $(this).append(text);
            //console.log(isSendTyping)
            if (isSendTyping) msgEvent.typing();
            scrollBar();
            next();
        });
    },
    getIcon: function (avatarBot) {
        var firstNameCharacter = userName.substring(0, 1).toUpperCase();
        var templateAvatar = '';
        templateAvatar += '<div class="message-avatar">';
        templateAvatar += '<div class="message-avatar-customer">';
        templateAvatar += '<div class="pr-3">';
        templateAvatar += '<span class ="message-avatar-item avatar">';
        templateAvatar += '<img src="' + _Host + 'assets/images/logo/icon-bot-lc.png" class="rounded-circle" alt="image">';
        templateAvatar += '</span>';
        templateAvatar += '</div>';
        templateAvatar += '</div>';
        templateAvatar += '</div>';
        return templateAvatar;
    },
    renderTemplate: function (date_current, avatarBot) {
        this.Typing = function () {
            var tmpText = '<div class="message-item message-item-typing">';
            tmpText += new messageBot.getIcon(avatarBot);
            tmpText += `<div class="message-item-content writing">
                                <div class="_4xko _13y8">
                                    <div class="_4a0v _1x3z">
                                        <div class="_4b0g">
                                            <div class="_5pd7"></div>
                                            <div class="_5pd7"></div>
                                            <div class="_5pd7"></div>
                                        </div>
                                    </div>
                                </div>
                          </div>`;
            tmpText += '</div>';
            $('#message-content').append(html);
        },
        this.Text = function (text) {
            var tmpText = '<div class="message-item message-item-text">';
            tmpText += new messageBot.getIcon(avatarBot);
            tmpText +=              '<div class="message-body">';
            tmpText +='                    <div>';
            tmpText +='                        <div class="message-align">';
            tmpText += '                            <span class="message-user-name font-size-08">Support Bot </span>';
            tmpText += '                            <span class="message-user-time font-size-08">' + date_current + '</span>';
            tmpText +='                        </div>';
            tmpText +='                    </div>';
            tmpText += '                    <div class="message-item-content">' + text + '</div>';
            tmpText += '                </div>';
            tmpText += '</div>';
            return tmpText;
        },
        this.Image = function (urlImage) {
            var tmpText = '<div class="message-item message-item-image">';
            tmpText += new messageBot.getIcon(avatarBot);
            tmpText += '<div class="message-body">';
            tmpText += '                    <div>';
            tmpText += '                        <div class="message-align">';
            tmpText += '                            <span class="message-user-name font-size-08">Support Bot </span>';
            tmpText += '                            <span class="message-user-time font-size-08">' + date_current + '</span>';
            tmpText += '                        </div>';
            tmpText += '                    </div>';
            tmpText += '                    <img src="' + urlImage + '"/>';
            tmpText += '                </div>';
            tmpText += '</div>';
            return tmpText;
        },
        this.TextAndButton = function (text, calbackButton) {
            var tmpText = '<div class="message-item message-item-image">';
            tmpText += new messageBot.getIcon(avatarBot);
            tmpText += '<div class="message-body">';
            tmpText += '                    <div>';
            tmpText += '                        <div class="message-align">';
            tmpText += '                            <span class="message-user-name font-size-08">Support Bot </span>';
            tmpText += '                            <span class="message-user-time font-size-08">' + date_current + '</span>';
            tmpText += '                        </div>';
            tmpText += '                    </div>';
            tmpText += '                    <div class="message-item-content">';
            tmpText +=                          '<span>' + text + '</span>';
            tmpText += calbackButton();
            tmpText += '                    </div>';
            tmpText += '  </div>';
            tmpText += '  </div>';
            return tmpText;
        },
        this.ContainerGeneric = function (text, calbackGenericIndex) {
            var tmpText = '<div class="message-item message-item-image">';
            tmpText += new messageBot.getIcon(avatarBot);
            tmpText += '<div class="message-body">';
            tmpText += '                    <div>';
            tmpText += '                        <div class="message-align">';
            tmpText += '                            <span class="message-user-name font-size-08">Support Bot </span>';
            tmpText += '                            <span class="message-user-time font-size-08">' + date_current + '</span>';
            tmpText += '                        </div>';
            tmpText += '                    </div>';


            tmpText += '                <div class="message-container" index="0" style="overflow:hidden">';
            tmpText += '                                   <div class="message-template" style="left: 0;position: relative;transition: left 500ms ease-out;white-space: nowrap; display: flex; width: 100%; flex-direction: row;">';
            tmpText += calbackGenericIndex();
            tmpText += '                                   </div>';
            tmpText += '                </div>';
            tmpText += '</div>';
            tmpText += '</div>';
            return tmpText;
        },
        this.GenericIndex = function (urlBanner, title, subTitle, subLink, calbackButton) {
            var tmpText = '<div class="message-item-template-generic">';
            tmpText += '                               <div class="message-banner _6j0s" style="background-image: url('+urlBanner+'); background-position: center center; height: 150px; width: 100%;">';
            tmpText += '                                </div>';
            tmpText += '                                <div class="message-template-generic-container _6j2g">';
            tmpText += '                                    <div class="message-generic-title _6j0t _4ik4 _4ik5" style="-webkit-line-clamp: 3;">';
            tmpText += '                                        '+title+''
            tmpText += '                                    </div>';
            tmpText += '                                    <div class="message-generic-subtitle _6j0u">';
            tmpText += '                                        <div>';
            tmpText += '                                            '+subTitle+''
            tmpText += '                                        </div>';
            tmpText += '                                    </div>';
            tmpText += '                                    <div class="message-generic-sublink _6j0y">';
            tmpText += '                                        <a target="_blank" href="' + subLink + '">';
            tmpText += '                                            '+subLink+''
            tmpText += '                                        </a>';
            tmpText += '                                    </div>';
            tmpText += '                                </div>';
            tmpText += calbackButton();
            tmpText += '  </div>';
        },
        this.BackNextGeneric = function () {
            var tmpText ='<a class="btn_back_generic _32rk _32rg _1cy6" href="#" style="display: none;">';
            tmpText += '                    <div direction="forward" class="_10sf _5x5_">';
            tmpText += '                        <div class="_5x6d">';
            tmpText += '                            <div class="_3bwv _3bww">';
            tmpText += '                                <div class="_3bwy">';
            tmpText += '                                    <div class="_3bwx">';
            tmpText += '                                        <i class="_3-8w img sp_RQ3p_x3xMG2 sx_c4c7bc" alt=""></i>';
            tmpText += '                                    </div>';
            tmpText += '                                </div>';
            tmpText += '                            </div>';
            tmpText += '                        </div>';
            tmpText += '                    </div>';
            tmpText += '                 </a>';
            tmpText += '                <a class="btn_next_generic _32rk _32rh _1cy6" href="#" style="display: block;">';
            tmpText += '                    <div direction="forward" class="_10sf _5x5_">';
            tmpText += '                        <div class="_5x6d">';
            tmpText += '                            <div class="_3bwv _3bww">';
            tmpText += '                                <div class="_3bwy">';
            tmpText += '                                    <div class="_3bwx">';
            tmpText += '                                        <i class="_3-8w img sp_RQ3p_x3xMG3 sx_dbbd74" alt=""></i>';
            tmpText += '                                    </div>';
            tmpText += '                                </div>';
            tmpText += '                            </div>';
            tmpText += '                        </div>';
            tmpText += '                    </div>';
            tmpText += '                </a>';
        },
        this.Button = function (lstButton) {
            var tmpText = '<div class="message-item-button">';
            $.each(lstButton, function (index, value) {
                if (value.type == "postback") {
                    tmpText += '<a class="message-btn-postback lc-6qcmqf" data-postback="'+value.payload+'">'+value.Title+'</a>';
                }
                if (value.type == "web_url") {
                    tmpText += '<a href="' + value.url + '" class="message-btn-link lc-6qcmqf" target="_blank">' + value.Title + '</a>';
                }
            })
            tmpText += '</div>';
            return tmpText;
        },
        this.QuickReply = function (lstQuickReply) {
            var tmpText = '        <div class="message-quickreply">';
            tmpText +='                <div class="message-quickreply-container">';
            tmpText += '                    <div class="message-quickreply-item" style="position:relative;" index="2">';
            $.each(lstQuickReply, function (index, value) {
                tmpText += '                        <button value="0" class="btn-quickreply" data-postback="'+value.payload+'">'+value.title+'</button>';
            })
            tmpText +='                    </div>';
            tmpText += '                </div>';
            if (lstQuickReply.length > 3) {
                tmpText += '                <a class="_32rk _32rg _1cy6  btn_back_quickreply" href="#" style="display: none;">';
                tmpText += '                    <div direction="forward" class="_10sf _5x5_">';
                tmpText += '                        <div class="_5x6d">';
                tmpText += '                            <div class="_3bwv _3bww">';
                tmpText += '                                <div class="_3bwy">';
                tmpText += '                                    <div class="_3bwx">';
                tmpText += '                                        <i class="_3-8w img sp_RQ3p_x3xMG2 sx_c4c7bc" alt=""></i>';
                tmpText += '                                    </div>';
                tmpText += '                                </div>';
                tmpText += '                            </div>';
                tmpText += '                        </div>';
                tmpText += '                    </div>';
                tmpText += '                </a>';
                tmpText += '                <a class="_32rk _32rh _1cy6  btn_next_quickreply" href="#" style="display: block;">';
                tmpText += '                    <div direction="forward" class="_10sf _5x5_">';
                tmpText += '                        <div class="_5x6d">';
                tmpText += '                            <div class="_3bwv _3bww">';
                tmpText += '                                <div class="_3bwy">';
                tmpText += '                                    <div class="_3bwx">';
                tmpText += '                                        <i class="_3-8w img sp_RQ3p_x3xMG3 sx_dbbd74" alt=""></i>';
                tmpText += '                                    </div>';
                tmpText += '                                </div>';
                tmpText += '                            </div>';
                tmpText += '                        </div>';
                tmpText += '                    </div>';
                tmpText += '                </a>';
                tmpText += '            </div>';
            }
            return tmpText;
        }
    }
}

function scrollBar() {
    $("#message-content").scrollTop($("#message-content").prop('scrollHeight'));
}

window.addEventListener('message', function (event) {
    var widthParent = parseInt(event.data);
    if (widthParent <= 425) {
        $("._3-8j").css('margin', '0px 0px 0px');
        $("._6atl").css('height', '100%');
        $("._6atl").css('width', event.data);
        $("._6ati").css('height', '100%');
        $("._6ati").css('width', event.data);
    }
}, false);
