var TYPE_USER_CONNECT = {
    CUSTOMER: 'customer',
    AGENT: 'agent',
    BOT: 'bot'
}

var ApplicationChannel = {
    Web: "0",
    Facebook: "1",
    Zalo: "2",
    Kiosk: "3"
}
var configs = {},
    apiChatRoom = {
        getListCustomerJoin: "api/lc_chatroom/getListCustomerJoinChatChannel"
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
$(document).ready(function () {
    //get list customer join chat
    new customerEvent.getCustomer().GetListCustomerJoinChat();
})

var cHub = {
    register: function () {
        $.connection.hub.logging = true;
        $.connection.hub.start({ transport: ['longPolling', 'webSockets'] });
        $.connection.hub.start().done(function () {
            console.log("signalr started")
            objHub.server.connectAgentToListCustomer(_channelGroupId);
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
                new customerEvent.getCustomer().GetCustomerRealtime(threadID, objCustomerDb);
            }
        };
        objHub.client.getStatusCustomerOffline = function (customerId, statusOffline) {
            console.log('customer-' + customerId + ' offline')
            var $elemCustomer = $("#customer-" + customerId + "");
            $elemCustomer.find('span.avatar').removeClass("avatar-state-online").addClass("avatar-state-offline");
        };
        objHub.client.getStatusCustomerOnline = function (customerId, statusOnline) {
            console.log('customer-' + customerId + ' online')
            var $elemCustomer = $("#customer-" + customerId + "");
            $elemCustomer.find('span.avatar').removeClass("avatar-state-offline").addClass("avatar-state-online");
        }
    }
}

var customerEvent = {
    actions :function(){
        $('body').on('click','.list-customer-item', function () {
            var elmCustomer = $(this);

        })

    },
    formChat : function(){
        this.getFormByCustomer = function (customerId) {

        }

    },
    getCustomer : function(){
        this.GetListCustomerJoinChat = function () {
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
        this.GetCustomerRealtime = function (threadID, customer) {
            var html = new customerEvent.render(threadID).templateListCustomer(customer);
            $("#div-list-customers").prepend(html);
            getTimeAgo();
        }
    },
    render: function (threadID) {
        this.templateListCustomer = function (customer) {
            var templateHtml = '';
            if (customer.ApplicationChannels == ApplicationChannel.Web) {
                templateHtml = '<a class="list-group-item d-flex list-customer-item" id="customer-' + (customer.ID == undefined ? customer.CustomerID : customer.ID) + '" data-customer-id="' + (customer.ID == undefined ? customer.CustomerID : customer.ID) + '" data-thread-id="' + threadID + '" href="javascript:void(0)">' +
                                    '<div class="pr-3">' +
                                        '<span class="avatar ' + (customer.StatusChatValue == 200 ? "avatar-state-online" : "avatar-state-offline") + '">' +
                                            '<span class="avatar-title bg-warning rounded-circle">W</span>' +
                                        '</span>' +
                                    '</div>' +
                                    '<div class="flex-grow- 1">' +
                                        '<h6 class="mb-1">' + customer.Name + '</h6>' +
                                        '<span class="small text-muted">' +
                                            '<i class="fa fa-image mr-1"></i> Photo' +
                                        '</span>' +
                                    '</div>' +
                                    '<div class="text-right ml-auto">' +
                                        '<span class="small text-muted timeago" datetime="' + customer.CreatedDate + '"></span>' +
                                    '</div>' +
                               '</a>';
            }
            else{
                templateHtml = '<a class="list-group-item d-flex list-customer-item" id="customer-' + (customer.ID == undefined ? customer.CustomerID : customer.ID) + '" data-customer-id="' + (customer.ID == undefined ? customer.CustomerID : customer.ID) + '" data-thread-id="' + threadID + '" href="javascript:void(0)">' +
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
                                            '<i class="fa fa-image mr-1"></i> Photo' +
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