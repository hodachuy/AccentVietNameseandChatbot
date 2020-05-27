var iconFace = {
    smiley: ':smiley:',//:)
    smile: ':smile:',//^_^,^-^
    grinning: ':grinning:',//:D
    open_mouth: ':open_mouth:',//:o,:0,:O
    disappointed: ':disappointed:',//:(
    expressionless: ':expressionless:',//-_-
    grin: ':grin:',//:v
    heart: ':heart:',//<3 "&lt;3"
    like: ':+1:',//(y)
    confounded: ':confounded:',   //:3
    stuck_out_tongue: ':stuck_out_tongue:'//:P
}

var TYPE_USER_CONNECT = {
    CUSTOMER: 'customer',
    AGENT: 'agent',
    BOT: 'bot'
}

var UserStatus = {
    Online :'200',
    Offline:'201'
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
        getListMessage : "api/lc_message/getByThreadId"
    }

var _channelGroupId = $("#channelGroupId").val(),
    _agentId =  $("#userId").val(),
    _agentName = $("#userName").val();

var AgentModel = {
    ID: _agentId,
    Name: _agentName,
    ChannelGroupID: _channelGroupId
}


var objHub = $.connection.chatHub;
$(function () {
    cHub.register();
    cHub.receivedSignalFromServer();
});

var cHub = {
    register: function () {
        $.connection.hub.logging = true;
        $.connection.hub.start({ transport: ['longPolling', 'webSockets'] });
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
            if ($.connection.hub.lastError)
            {
                console.log("Disconnected. Reason: " + $.connection.hub.lastError.message);
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
    },
    receivedSignalFromServer: function () {
        objHub.client.receiveNewThreadCustomer = function (channelGroupId, threadID, objCustomerDb) {
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
            }
        };
        objHub.client.receiveMessages = function (channelGroupId, threadId, message, customerId, agentName, typeUser) {
            if (channelGroupId == _channelGroupId) {
                console.log('threadId:' + threadId +'  customer-' + customerId + ' : '  + message)
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
            $("#device-city").empty().append(device.City == null ? "" : device.City);
            $("#device-ip").empty().append(device.IPAddress == null ? "" : device.IPAddress);
            $("#device-os").empty().append(device.OS == null ? "" : device.OS);
            $("#device-browser").empty().append(device.Browser == null ? "" : device.Browser);
            $("#device-user-agent").empty().append(device.FullUserAgent == null ? "" : device.FullUserAgent);
            if (device.Latitude != "") {
                let latinglongTude = device.Latitude + "," + device.Longtitude;
                $("#LatiLongTude").val(latinglongTude);
                initLatiLongMap(device.Latitude, device.Longtitude);
            }
        }();

        // lấy danh sách tin nhắn
        var renderFormMessage = function () {
            $(".customer-name").html(objCustomer.Name);
            $(".chat-header").show();
            var htmlChatSetting = `<a class="dropdown-item" href="javascript:void(0);">
                                        <label class="container">
                                            Chuyển bot trả lời
                                            <input type="checkbox" id="chkIsBotChat-`+ objCustomer.ID + `" checked="checked">
                                            <span class="checkmark"></span>
                                        </label>
                                    </a>`;
            $("#form-message-setting").empty().append(htmlChatSetting);

            $(".messages").show();
            $(".messages").empty().append('<div id="container-message-'+objCustomer.ID+'"></div>')

            $(".chat-footer").show();
            var htmlChatFooter = `<div class="flex-grow-1" style="position:relative">
                                        <input type="text" class ="form-control" id= "input-chat-message-`+ objCustomer.ID + `" placeholder="Nhập tin nhắn..." data-emojiable="true">
                                    </div>
                                    <div class="chat-footer-buttons d-flex">
                                        <button class ="btn-primary" type="submit" id= "btn-chat-submit-`+ objCustomer.ID + `" >
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

                    insertChat("agent", objCustomer.ID, isValidURLandCodeIcon(text), _agentName,"");

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

                insertChat("agent", objCustomer.ID, isValidURLandCodeIcon(text), _agentName);

                // gửi tin nhắn
                objHub.server.sendMessage(_channelGroupId, threadId, isValidURLandCodeIcon(text), _agentId, _agentName, TYPE_USER_CONNECT.AGENT);
                $(this).val('');
            }
            return;
        })
    },
    customerJoin : function(){
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
                templateHtml = '<a class="list-group-item d-flex list-customer-item" id="customer-' + customer.ID + '" data-customer-id="' + customer.ID + '" data-thread-id="' + threadID + '" href="javascript:new customerEvent.customerJoin().ViewFormChatById(\'' + customer.ID + '\',\'' + threadID + '\')">' +
                                    '<div class="pr-3">' +
                                        '<span class="avatar ' + (customer.StatusChatValue == 200 ? "avatar-state-online" : "avatar-state-offline") + '">' +
                                            '<span class="avatar-title bg-warning rounded-circle">W</span>' +
                                        '</span>' +
                                    '</div>' +
                                    '<div class="flex-grow- 1">' +
                                        '<h6 class="mb-1">' + customer.Name + '</h6>' +
                                        '<span class="small text-muted">' +
                                            '<span id="msg-preview-' + customer.ID + '"></span>' +
                                        '</span>' +
                                    '</div>' +
                                    '<div class="text-right ml-auto">' +
                                        '<span class="small text-muted timeago" datetime="' + customer.CreatedDate + '"></span>' +
                                    '</div>' +
                               '</a>';
            }
            else{
                templateHtml = '<a class="list-group-item d-flex list-customer-item" id="customer-' + customer.ID + '" data-customer-id="' + customer.ID + '" data-thread-id="' + threadID + '" href="javascript:void(0)">' +
                                    '<div class="pr-3">' +
                                        '<span class="avatar ' + (customer.StatusChatValue == 200 ? "avatar-state-online" : "avatar-state-offline") + '">' +
                                               '<img src="'+ _Host + customer.Avatar +'" class="rounded-circle" alt="image">'+
                                         '</span>'+
                                    '</div>' +
                                    '<div class="flex-grow- 1">' +
                                        '<h6 class="mb-1">' +
                                            '<span>'
                                                if(customer.ApplicationChannels == ApplicationChannel.Facebook){
                                                    '<img class="css-gkenkv" src="~/assets/client/img/fb-msg-icon-960x960.png" alt="channel-icon">'
                                                }
                                                else if(customer.ApplicationChannels == ApplicationChannel.Zalo){
                                                    '<img class="css-gkenkv" src="~/assets/client/img/zalo-msg-icon.png" alt="channel-icon">'
                                                }
                                            '</span>'+
                                        '</h6>' +
                                        '<span class="small text-muted">' +
                                           '<span id="msg-preview-' + customer.ID + '"></span>' +
                                        '</span>' +
                                    '</div>' +
                                    '<div class="text-right ml-auto">' +
                                        '<span class="small text-muted timeago" datetime="' + customer.CreatedDate + '"></span>' +
                                    '</div>' +
                               '</a>';
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
    avatar:"",
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
    }
    ,
    QuickReply: function () {
        var html = '';
        return html;
    }
}



function insertChat(who, customerId, text, userName, avatar) {
    let user_chat_preview = who == "agent" ? "bạn" : "khách";
    let user_class_chat = who == "agent" ? "me" : "";
    let date_current = new Date();

    content = '<div class="message-item ' + user_class_chat + '">';
    content += message.getUserIcon(userName, avatar);
    content += message.add(userName, text);
    content += '</div>'
    $("#container-message-" + customerId).append(content);

    $(".messages").getNiceScroll(0).doScrollTop($("#container-message-" + customerId).prop('scrollHeight'));
    return false;
}

var message = {
    getUserIcon : function(userName, avatar){
        var firstNameCharacter = userName.substring(0, 1).toUpperCase();
        var templateAvatar = '<div class="message-avatar">' +
                                    '<div class="message-avatar-customer">' +
                                        '<div class="pr-3">' +
                                            '<span class ="message-avatar-item avatar">'
                                                if (avatar == "") {
                                                    '<span class ="avatar-title bg-warning rounded-circle">"' +  firstNameCharacter + '"</span>'
                                                } else {
                                                    '<img src="~/assets/client/img/avatar-admin.jpg" class="rounded-circle" alt="image">'
                                                }
                                            '</span>' +
                                        '</div>' +
                                    '</div>' +
                                '</div>';
                                            console.log(templateAvatar)
        return templateAvatar;
    },
    add: function (userName, text) {
        var template = `<div>
                            <div class="txt-align-left">
                                <span class ="font-size-08">Hỗ trợ bởi `+userName+` </span>
                                <span class="font-size-08">15:11</span>
                            </div>
                        </div>
                        <div class="message-item-content">`+text+`</div>
                        <div class ="txt-align-left font-size-08">Message not sent, Delivered, Read</div>`
        return template;
    }
}


//Get Map Device IP
function initLatiLongMap(latitude, longitude) {
    var posVietNam = { lat: 16.4498, lng: 107.5624 };
    var zoomSize = 5;
    if (latitude && longitude) {
        posVietNam = { lat: parseFloat(latitude), lng: parseFloat(longitude) };
        zoomSize = 9;
    }
    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: zoomSize,
        center: posVietNam,
        mapTypeControl: false,
        zoomControl: false,
        scaleControl: false,
        streetViewControl: false,
        rotateControl: false,
        fullscreenControl: false
    });
    marker = new google.maps.Marker({
        map: map,
        draggable: false,
        animation: google.maps.Animation.DROP,
        position: posVietNam
    });
    google.maps.event.addListener(marker, 'dragend', function (event) {
        document.getElementById("Latitude").value = this.getPosition().lat();
        document.getElementById("Longitude").value = this.getPosition().lng();
    });
    //autocomplete = new google.maps.places.Autocomplete(
    //    (document.getElementById('address')), { types: ['geocode'] });
    var input = document.getElementById('Address');
    autocomplete = new google.maps.places.Autocomplete(input);
    autocomplete.addListener('place_changed', fillInAddress);
}

function fillInAddress() {
    // Get the place details from the autocomplete object.
    var place = autocomplete.getPlace();
    document.getElementById("LatiLongTude").value = place.geometry.location.lat() + "," + place.geometry.location.lng();
    var pos = { lat: place.geometry.location.lat(), lng: place.geometry.location.lng() };
    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 9,
        center: pos,
        mapTypeControl: false,
        zoomControl: false,
        scaleControl: false,
        streetViewControl: false,
        rotateControl: false,
        fullscreenControl: false
    }); var image = {
        url: "/assets/client/img/gmap_marker.png",
        anchor: new google.maps.Point(25, 25),
        scaledSize: new google.maps.Size(45, 45)
    };
    marker = new google.maps.Marker({
        map: map,
        draggable: false,
        animation: google.maps.Animation.DROP,
        position: pos,
        //icon: image
    });
    google.maps.event.addListener(marker, 'dragend', function (event) {
        document.getElementById("LatiLongTude").value = this.getPosition().lat() + "," + this.getPosition().lng();
    });
}

function getLatitudeLongitude(callback, address) {
    address = address || 'Ferrol, Galicia, Spain';
    geocoder = new google.maps.Geocoder();
    if (geocoder) {
        geocoder.geocode({
            'address': address
        }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                callback(results[0]);
            }
        });
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
    temp.animate(
                  { 'top': (posLiFirst.top + 30), 'left': posLiFirst.left }, 0, function () {// thẻ ảo di chuyển
                      temp.remove();// di chuyen xong remove
                      elementSelect.remove();
                      newLiFirst.show()
                  });
    $(".chat-sidebar-content").getNiceScroll(0).doScrollTop(0);
    return;
};

function isValidURLandCodeIcon(string) {
    var res = string.match(/(http(s)?:\/\/.)?(www\.)?[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)/g);
    if (res != null) {
        string = string.replace(res, "<a href='" + res + "' target='_blank' style='text-decoration:underline'>" + res + "</a>");
    }
    else if (string.includes(":)H")) {
        string = string.replace(':)H', '<img style="display:inline-block;width:25px;height:25px;border-radius:30px" src="' + _Host + 'assets/client/libs/emoji-picker/img/me.jpg" alt="" />');
    }
    else if (string.includes(":)")) {
        string = string.replace(':)', returnIcon(iconFace.smiley, -25, 0));
    }
    else if (string.includes("^-^") || string.includes("^_^")) {
        string = string.replace('^-^', returnIcon(iconFace.smile, 0, 0));
        string = string.replace('^_^', returnIcon(iconFace.smile, 0, 0));
    }
    else if (string.includes(":D")) {
        string = string.replace(':D', returnIcon(iconFace.grinning, -50, 0));
    }
    else if (string.includes(":o") || string.includes(":0") || string.includes(":O")) {
        string = string.replace(':o', returnIcon(iconFace.open_mouth, -550, 25));
        string = string.replace(':0', returnIcon(iconFace.open_mouth, -550, 25));
        string = string.replace(':O', returnIcon(iconFace.open_mouth, -550, 25));
    }
    else if (string.includes(":(")) {
        string = string.replace(':(', returnIcon(iconFace.disappointed, -475, 0));
    }
    else if (string.includes("-_-")) {
        string = string.replace('-_-', returnIcon(iconFace.expressionless, -75, -50));
    }
    else if (string.includes(":v")) {
        string = string.replace(':v', returnIcon(iconFace.grin, -375, 0));
    }
    else if (string.includes("&lt;3")) {
        string = string.replace('&lt;3', returnIcon(iconFace.heart, -250, -150));
    }
    else if (string.includes("(y)")) {
        string = string.replace('(y)', returnIcon(iconFace.like, -600, -75));
    }
    else if (string.includes(":3")) {
        string = string.replace(':3', returnIcon(iconFace.confounded, -225, -25));
    }
    else if (string.includes(":P")) {
        string = string.replace(':P', returnIcon(iconFace.stuck_out_tongue, -325, 0));
    }
    return string;
};
function returnIcon(code, positionX, positionY) {
    var imageIcon = '<img src="' + _Host + 'assets/client/libs/emoji-picker/img/blank.gif" class="img" style="display:inline-block;width:25px;height:25px;background:url(' + "'assets/client/libs/emoji-picker/img/emoji_spritesheet_0.png'" + ') ' + positionX + 'px ' + positionY + 'px no-repeat;background-size:675px 175px;" alt="' + code + '">';
    return imageIcon
}