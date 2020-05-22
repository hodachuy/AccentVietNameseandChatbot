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
        getCustomerById: "api/lc_customer/getById"
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
        objHub.client.getStatusCustomerOffline = function (customerId) {
            console.log('customer-' + customerId + ' offline')
            var $elemCustomer = $("#customer-" + customerId + "");
            $elemCustomer.find('span.avatar').removeClass("avatar-state-online").addClass("avatar-state-offline");
        };

    }
}

var customerEvent = {
    actions :function(){
        $('body').on('click','.list-customer-item', function () {
            var elmCustomer = $(this);

        })

    },
    getFormChat : function(objCustomer){
        var getFormMessage = function () {
            var templateDivMessage = '';
            console.log('formchat')
        }();
        var getFormDeviceInfo = function () {
            var device = objCustomer.Device;
            var templateDivDevice = '';
            console.log('device')
        }();
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
                    new customerEvent.getFormChat(data);
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

//Get Map Device IP
function initLatiLongMap(latitude, longitude) {
    var posVietNam = { lat: 16.4498, lng: 107.5624 };
    var zoomSize = 5;
    if (latitude && longitude) {
        posVietNam = { lat: parseFloat(latitude), lng: parseFloat(longitude) };
        zoomSize = 18;
    }
    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: zoomSize,
        center: posVietNam
    });
    marker = new google.maps.Marker({
        map: map,
        draggable: true,
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
        zoom: 18,
        center: pos
    }); var image = {
        url: "/assets/client/img/gmap_marker.png",
        anchor: new google.maps.Point(25, 25),
        scaledSize: new google.maps.Size(45, 45)
    };
    marker = new google.maps.Marker({
        map: map,
        draggable: true,
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
