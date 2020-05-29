
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
    isBotActive = false;

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
    isBotActive = checkBotActive();

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
            if (isAgentOnline == true || isBotActive == true) {
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
    var isCheck = false;
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
                isCheck = true;
            }
        }
    })
    return isCheck;
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
                    insertChat("customer", isValidURLandCodeIcon(text),"", "");
                    // gửi tin nhắn
                    objHub.server.sendMessage(_channelGroupId, CustomerModel.ThreadID, isValidURLandCodeIcon(text),"", CustomerModel.ID, "", TYPE_USER_CONNECT.CUSTOMER);
                    $(this).val('');
                    $(this).text('');
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
            appendMessage(elementLastMessageAppend, who, customerId, text);
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
        $("#message-content").scrollTop($("#message-content").prop('scrollHeight'));
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
