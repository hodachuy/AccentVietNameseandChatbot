var TYPE_USER_CONNECT = {
    CUSTOMER: 'customer',
    AGENT: 'agent',
    BOT: 'bot'
}

var UserStatus = {
    Online: '200',
    Offline: '201'
}

var ApplicationChannel = {
    Web: "0",
    Facebook: "1",
    Zalo: "2",
    Kiosk: "3"
}
var configs = {},
    apiChatRoom = {
        getListCustomerJoin: "api/lc_chatroom/getListCustomerJoinChatChannel",
        getCustomerById: "api/lc_customer/getById",
        getListMessage: "api/lc_message/getByThreadId"
    }

var _channelGroupId = $("#channelGroupId").val(),
    _agentId = $("#userId").val(),
    _agentName = $("#userName").val();

var AgentModel = {
    ID: _agentId,
    Name: _agentName,
    ChannelGroupID: _channelGroupId
}

//var isRenewConversation = false;

var objHub = $.connection.chatHub;

$(function () {
    cHub.register();
    cHub.receivedSignalFromServer();
});

var cHub = {
    register: function () {
        $.connection.hub.logging = true;
        $.connection.hub.qs = 'isAgentConnected=true';
        $.connection.hub.start({
            transport: ['longPolling', 'webSockets']
        });
        $.connection.hub.start().done(function () {
            console.log("signalr started")
            //objHub.server.connectAgentToListCustomer(_agentId, _channelGroupId);
            objHub.server.connectAgentToChannelChat(_agentId, _channelGroupId)

            new customerEvent.customerJoin().GetListCustomerFromDb();
        });
        $.connection.hub.error(function (error) {
            console.log('SignalR error: ' + error)
        });

        let tryingToReconnect = false;
        $.connection.hub.reconnecting(function () {
            tryingToReconnect = true;
            console.log('SingalR connect đang kết nối lại')
        });

        $.connection.hub.reconnected(function () {
            tryingToReconnect = false;
            console.log('SingalR connect đã kết nối lại')
        });

        $.connection.hub.disconnected(function () {
            console.log('SingalR connect ngắt kết nối')
            if ($.connection.hub.lastError) {
                console.log("Disconnected. Reason: " + $.connection.hub.lastError.message);
            }

            if (tryingToReconnect) {
                setTimeout(function () {
                    console.log('SingalR connect đang khởi động lại')
                    $.connection.hub.start({
                        transport: ['longPolling', 'webSockets']
                    });
                    $.connection.hub.start().done(function () { });
                }, 5000); // Restart connection after 5 seconds.          
            }
        });
    },
    receivedSignalFromServer: function () {
        objHub.client.receiveNewCustomerToAgent = function (channelGroupId, threadID, objCustomerDb) {
            // Kiểm tra tín hiệu trả về theo đúng kênh chat
            if (channelGroupId == _channelGroupId) {
                // customer mới tham gia
                console.log('customer-' + objCustomerDb.ID + ' new join')

                new customerEvent.customerJoin().GetCustomerJoinRealtime(threadID, objCustomerDb);
            }
        };
        objHub.client.getStatusCustomerOffline = function (channelGroupId, customerId) {
            if (channelGroupId == _channelGroupId) {
                console.log('customer-' + customerId + ' offline')
                var $elemCustomer = $("#customer-" + customerId + "");
                $elemCustomer.find('span.avatar').removeClass("avatar-state-online").addClass("avatar-state-offline");
            }

        };
        objHub.client.getStatusCustomerOnline = function (channelGroupId, customerId) {
            if (channelGroupId == _channelGroupId) {
                console.log('customer-' + customerId + ' online')
                var $elemCustomer = $("#customer-" + customerId + "");
                $elemCustomer.find('span.avatar').removeClass("avatar-state-offline").addClass("avatar-state-online");
                isRenewConversation = true;
            }
        };
        objHub.client.receiveMessages = function (channelGroupId, threadId, message, userId, userName, typeUser) {
            if (channelGroupId == _channelGroupId) {
                console.log('threadId:' + threadId + '  customer-' + userId + ' : ' + message)
                var $elmCustomer = $("#customer-" + userId);
                if (typeUser == TYPE_USER_CONNECT.CUSTOMER) {
                    if ($elmCustomer.hasClass("active")) {
                        userName = (userName == "" ? "W" : userName);
                        insertChat("customer", userId, isValidURLandCodeIcon(message), userName, "");
                    }
                }
            }
        };
        objHub.client.receiveTyping = function (channelGroupId, customerId) {
            if (channelGroupId == _channelGroupId) {
                console.log('customer-' + customerId + ' typing')
            }
        };
    }
}

var customerEvent = {
    getFormChat: function (objCustomer, threadId) {
        let isStopTyping = false;

        // lấy thông tin thiết bị khách hàng truy cập
        var renderFormDeviceInfo = function () {
            $("#chat-sidebar-device").show();
            $("#chat-sidebar-template-loading").hide();
            var device = objCustomer.Devices[0];
            $("#device-city").empty().append(device.City == null ? "" : " "+device.City);
            $("#device-ip").empty().append(device.IPAddress == null ? "" : device.IPAddress);
            $("#device-os").empty().append(device.OS == null ? "" : device.OS);
            $("#device-browser").empty().append(device.Browser == null ? "" : device.Browser);
            $("#device-user-agent").empty().append(device.FullUserAgent == null ? "" : device.FullUserAgent);
            if (device.Latitude != "") {
                let latinglongTude = device.Latitude + "," + device.Longtitude;
                $("#LatiLongTude").val(latinglongTude);
                initLatiLongMap(device.Latitude, device.Longtitude);
            }

            $('.chat-sidebar-content').niceScroll();
        }();

        // lấy danh sách tin nhắn
        var renderFormMessage = function () {
            $(".customer-name").html(objCustomer.Name);
            $(".chat-header").show();
            var htmlChatSetting = `<a class="dropdown-item" href="javascript:void(0);">
                                        <label class="container">
                                            Chuyển bot trả lời
                                            <input type="checkbox" id="chkIsBotChat-` + objCustomer.ID + `" checked="checked">
                                            <span class="checkmark"></span>
                                        </label>
                                    </a>`;
            $("#form-message-setting").empty().append(htmlChatSetting);

            $(".messages").show();
            $('div.messages').attr('id', 'message-container-' + objCustomer.ID + '');

            $(".chat-footer").show();
            var htmlChatFooter = `<div class="flex-grow-1" style="position:relative">
                                        <input type="text" class ="form-control" id= "input-chat-message-` + objCustomer.ID + `" placeholder="Nhập tin nhắn..." data-emojiable="true">
                                    </div>
                                    <div class="chat-footer-buttons d-flex">
                                        <button type="submit" id= "btn-chat-submit-` + objCustomer.ID + `" >
                                            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round" class="feather feather-send width-15 height-15"><line x1="22" y1="2" x2="11" y2="13"></line><polygon points="22 2 15 22 11 13 2 9 22 2"></polygon></svg>
                                        </button>
                                   </div>`
            $("#chat-sidebar-message-footer").empty().append(htmlChatFooter)

            $(function () {
                window.emojiPicker = new EmojiPicker({
                    emojiable_selector: '[data-emojiable=true]',
                    //assetsPath: '../asset/emoji-picker/img',
                    assetsPath: _Host + 'assets/client/libs/emoji-picker/img',
                    popupButtonClasses: 'fa fa-smile'
                });
                window.emojiPicker.discover();
            });

            var templateDivMessage = '';

            // get list message 
            //var params = {

            //}
            //$.ajax({
            //    url: _Host + apiChatRoom.getListMessage,
            //    contentType: 'application/json; charset=utf-8',
            //    data: params,
            //    type: 'GET',
            //    success: function (data) {
            //    },
            //})
            // event input text
        }();

        $($($("#input-chat-message-" + objCustomer.ID).next()).eq(0)).keyup(function (e) {
            var edValue = $(this);
            var text = edValue.text();
            if (text.length > 0) {
                if (isStopTyping == false) {
                    objHub.server.sendTyping(_channelGroupId, threadId, AgentModel.ID);
                    isStopTyping = true;
                }
            } else {
                isStopTyping = false;
            }
        })

        $($($("#input-chat-message-" + objCustomer.ID).next()).eq(0)).keydown(function (e) {
            var edValue = $(this);
            var text = edValue.html();
            if (e.which == 13) {
                e.preventDefault(e);
                if (text !== "") {

                    insertChat("agent", objCustomer.ID, isValidURLandCodeIcon(text), _agentName, "");

                    var indexPosition = $("#div-list-customers a#customer-" + objCustomer.ID).prevAll().length;
                    // đổi vị trí lên top khi focus chat với customer
                    changePositon("#div-list-customers a:eq(" + indexPosition + ")")
                    // gửi tin nhắn
                    objHub.server.sendMessage(_channelGroupId, threadId, isValidURLandCodeIcon(text), _agentId, _agentName, TYPE_USER_CONNECT.AGENT);

                    $(this).val('');
                    $(this).text('');
                    isStopTyping = false;
                    return;
                }
            }
        })

        $("#btn-chat-submit-" + objCustomer.ID).on('click', function () {
            var edValue = $("#input-chat-message-" + objCustomer.ID);
            var text = edValue.val();
            console.log(text)
            if (text !== "") {

                insertChat("agent", objCustomer.ID, isValidURLandCodeIcon(text), _agentName, "");

                // gửi tin nhắn
                objHub.server.sendMessage(_channelGroupId, threadId, isValidURLandCodeIcon(text), _agentId, _agentName, TYPE_USER_CONNECT.AGENT);
                $(this).val('');
            }
            return;
        })
    },
    customerJoin: function () {
        this.GetListCustomerFromDb = function () {
            var params = {
                channelGroupId: _channelGroupId
            }
            $.ajax({
                url: _Host + apiChatRoom.getListCustomerJoin,
                contentType: 'application/json; charset=utf-8',
                data: params,
                type: 'GET',
                success: function (data) {
                    if (data.length != 0) {
                        var templateCustomer = '';
                        $.each(data, function (index, value) {
                            let templateHtml = new customerEvent.render(value.ThreadID).templateListCustomer(value);
                            templateCustomer += templateHtml;
                        })
                        $("#div-list-customers").empty().append(templateCustomer);

                        getTimeAgo();

                        // auto focus customer đầu tiên
                        setTimeout(function () {
                            new customerEvent.customerJoin().ViewFormChatById(data[0].ID, data[0].ThreadID)
                        },500)
                    }
                },
            });
        },
            this.GetCustomerJoinRealtime = function (threadID, customer) {
                var html = new customerEvent.render(threadID).templateListCustomer(customer);
                $("#div-list-customers").prepend(html);
                getTimeAgo();
            }
        this.ViewFormChatById = function (customerId, threadId) {
            var $elemCustomer = $("#customer-" + customerId + "");
            if ($elemCustomer.hasClass("active")) {
                return;
            }
            // hightlight active
            $(".list-customer-item").removeClass("active");
            $elemCustomer.addClass("active");
            // gửi tín hiệu kết nối tới nhóm có customer mới
            if ($elemCustomer.find('span.avatar').hasClass("avatar-state-online")) {
                console.log('customer has online')
                objHub.server.agentJoinChatCustomer(threadId);
            }
            var params = {
                customerId: customerId
            }
            $.ajax({
                url: _Host + apiChatRoom.getCustomerById,
                contentType: 'application/json; charset=utf-8',
                data: params,
                type: 'GET',
                success: function (data) {
                    console.log(data)
                    new customerEvent.getFormChat(data, threadId);
                },
            });
        }
    },
    render: function (threadID) {
        this.templateListCustomer = function (customer) {
            var templateHtml = '';
            if (customer.ApplicationChannels == ApplicationChannel.Web) {
                templateHtml += '<a class="list-group-item d-flex list-customer-item" id="customer-' + customer.ID + '" data-customer-id="' + customer.ID + '" data-thread-id="' + threadID + '" href="javascript:new customerEvent.customerJoin().ViewFormChatById(\'' + customer.ID + '\',\'' + threadID + '\')">';
                templateHtml +=    '<div class="pr-3">';
                templateHtml +=    '<span class="avatar ' + (customer.StatusChatValue == 200 ? "avatar-state-online" : "avatar-state-offline") + '">';
                templateHtml +=    '<span class="avatar-title bg-warning rounded-circle">W</span>';
                templateHtml +=    '</span>';
                templateHtml +=    '</div>';
                templateHtml +=    '<div class="flex-grow- 1">';
                templateHtml +=    '<h6 class="mb-1">' + customer.Name + '</h6>';
                templateHtml +=     '<span class="small text-muted">';
                templateHtml +=    '<span id="msg-preview-' + customer.ID + '"></span>';
                templateHtml +=    '</span>';
                templateHtml += '</div>';
                if (customer.LogoutDate != null) {
                    templateHtml += '<div class="text-right ml-auto">';
                    templateHtml += '<span class="small text-muted timeago" datetime="' + customer.LogoutDate + '"></span>';
                    templateHtml += '</div>';
                }
                templateHtml +=    '</a>';
            } else {
                templateHtml += '<a class="list-group-item d-flex list-customer-item" id="customer-' + customer.ID + '" data-customer-id="' + customer.ID + '" data-thread-id="' + threadID + '" href="javascript:void(0)">';
                templateHtml += '<div class="pr-3">';
                templateHtml += '<span class="avatar ' + (customer.StatusChatValue == 200 ? "avatar-state-online" : "avatar-state-offline") + '">';
                templateHtml += '<img src="' + _Host + customer.Avatar + '" class="rounded-circle" alt="image">';
                templateHtml += '</span>';
                templateHtml += '</div>';
                templateHtml += '<div class="flex-grow- 1">';
                templateHtml += '<h6 class="mb-1">';
                templateHtml += '<span>';
                if (customer.ApplicationChannels == ApplicationChannel.Facebook) {
                    templateHtml += '<img class="css-gkenkv" src="~/assets/client/img/fb-msg-icon-960x960.png" alt="channel-icon">';
                } else if (customer.ApplicationChannels == ApplicationChannel.Zalo) {
                    templateHtml += '<img class="css-gkenkv" src="~/assets/client/img/zalo-msg-icon.png" alt="channel-icon">';
                }
                templateHtml += '</span>';
                templateHtml += '</h6>';
                templateHtml += '<span class="small text-muted">';
                templateHtml += '<span id="msg-preview-' + customer.ID + '"></span>';
                templateHtml += '</span>';
                templateHtml += '</div>';
                templateHtml += '<div class="text-right ml-auto">';
                templateHtml += '<span class="small text-muted timeago" datetime="' + customer.CreatedDate + '"></span>';
                templateHtml += '</div>';
                templateHtml += '</a>';
            }
            return templateHtml;
        }
    }
}

var chatMessages = {
    name: "ms1",
    messageText: {
        Content: 'abc',
        Type: 'Text'
    },
    avatar: "",
    showTime: true,
    time: ''
}
var templateTypeMessage = {
    Text: function () {
        var html = '';
        return html;
    },
    TextAndButton: function () {
        var html = '';
        return html;
    },
    Generic: function () {
        var html = '';
        return html;
    },
    Image: function () {
        var html = '';
        return html;
    },
    File: function () {
        var html = '';
        return html;
    },
    Video: function () {
        var html = '';
        return html;
    },
    QuickReply: function () {
        var html = '';
        return html;
    }
}


function insertChat(who, customerId, text, userName, avatar) {
    let user_class_chat = (who == "agent" ? "me" : "");
    let date_current = new Date();

    content = '<div class="message-item ' + user_class_chat + '">';
    content += message.getUserIcon(userName, avatar);
    content += message.add(userName, text);
    content += '</div>';

    // insert body chat
    $("#message-container-" + customerId).append(content);

    // insert preview
    insertPreviewChat(who, customerId, text)
    // scroll to bottom
    setTimeout(function () {
        $(".messages").getNiceScroll(0).doScrollTop($("#message-container-" + customerId).prop('scrollHeight'));
    },100)
    return false;
}

function insertEventChat(customerId, time) {
    var html = '';
    html +='<div class="message-item message-item-divider">';
    html +='                        <span>Hôm nay</span>';
    html += '                    </div>';
    $("#message-container-" + customerId).append(html);
    isRenewConversation = false;
    return;
}

function insertPreviewChat(who, customerId, text) {
    var user = (who == "agent" ? "Bạn" : "Khách");
    if (text.length > 20) {
        text = text.substring(0, 19) + "...";
    }
    text = user + ": " + text;
    $("#msg-preview-" + customerId).empty().append(text);
    return;
}

var message = {
    getUserIcon: function (userName, avatar) {
        var firstNameCharacter = userName.substring(0, 1).toUpperCase();
        var templateAvatar = '';
        templateAvatar += '<div class="message-avatar">';
        templateAvatar += '<div class="message-avatar-customer">';
        templateAvatar += '<div class="pr-3">';
        templateAvatar += '<span class ="message-avatar-item avatar">';
        if (avatar == "") {
            templateAvatar += '<span class ="avatar-title bg-primary rounded-circle">' + firstNameCharacter + '</span>';
        } else {
            templateAvatar += '<img src="~/assets/client/img/avatar-admin.jpg" class="rounded-circle" alt="image">';
        }
        templateAvatar += '</span>';
        templateAvatar += '</div>';
        templateAvatar += '</div>';
        templateAvatar += '</div>';
        return templateAvatar;
    },
    add: function (userName, text) {
        var templateMsg ='';
        templateMsg += '<div class="message-body">';
        templateMsg +=                '<div>';
        templateMsg +=                    '<div class ="message-align">';
        templateMsg +=                        '<span class="font-size-08">'+userName+' </span>';
        templateMsg +=                        '<span class="font-size-08">' + showTimeChat() + '</span>';
        templateMsg +=                    '</div>';
        templateMsg +=                '</div>';
        templateMsg +=                '<div class="message-item-content">'+text+'</div>';
        templateMsg +=                '<div class="txt-align-left font-size-08">Delivered</div>';//Message not sent, , Read
        templateMsg += '</div>';
        return templateMsg;
    }
}

// Đổi vị trí user lên top khi chat
function changePositon(selector) {
    var elementSelect = $(selector);
    // Lấy vị trí top,lef của thẻ element tag li lawyer đầu tiên
    var posLiFirst = $("#div-list-customers a:eq(0)").offset();
    // Lấy vị trí top,lef của thẻ element tag li lawyer khi chọn click
    var oldPosLiSelect = $(selector).offset();
    if (posLiFirst.top == oldPosLiSelect.top)
        return;

    // Sao chép element li select thành vị trí đầu tiên
    var newLiFirst = elementSelect.clone().insertBefore('#div-list-customers a:eq(0)');
    newLiFirst.hide();

    // Tạo thẻ li ảo di chuyển
    var temp = elementSelect.clone().appendTo('body')
    temp.css('left', oldPosLiSelect.left)
        .css('top', oldPosLiSelect.top)
        .css('width', $('#div-list-customers a:eq(0)').offsetWidth)
        .css('height', $('#div-list-customers a:eq(0)').offsetHeight)
    temp.addClass('temp');

    //elementSelect.hide();
    temp.animate({
        'top': (posLiFirst.top + 30),
        'left': posLiFirst.left
    }, 0, function () { // thẻ ảo di chuyển
        temp.remove(); // di chuyen xong remove
        elementSelect.remove();
        newLiFirst.show()
    });
    $(".chat-sidebar-content").getNiceScroll(0).doScrollTop(0);
    return;
};


// Kiểm tra user có hoạt dộng trên tab trình duyệt của mình không
// Nếu focus xem màn hình
var interval_id;
$(window).focus(function () {
    if (!interval_id) {
        interval_id = setInterval(function () {
            console.log("hoat dong");
            clearInterval(interval_id);
        }, 1000);
    }
});
// Nếu không xem màn hình
$(window).blur(function () {
    clearInterval(interval_id);
    interval_id = 0;
    console.log("k hoat dong")
});