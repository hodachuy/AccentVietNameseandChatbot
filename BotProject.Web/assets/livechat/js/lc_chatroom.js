const TYPE_CONNECT_AGENT = "agent";

var AgentModel = {
    ID: $("#userId").val(),
    Name: $("#userName").val(),
    ChannelGroupID: $("#channelGroupId").val()
}

var objHub = $.connection.chatHub;

$(document).ready(function () {
    cHub.register();
    cHub.receivedSignalFromServer();
})

var cHub = {
    register: function () {
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
    },
    receivedSignalFromServer: function () {
        objHub.client.receiveNewThreadCustomer = function (channelGroupId, threadID, objCustomerDb) {
            console.log(channelGroupId);
            console.log(threadID);
            console.log(objCustomerDb);
            customerEvent.getCustomerOnlineRealtime(objCustomerDb);
        }
    }
}

var customerEvent = {
    getListCustomerOnlineFromDb : function(){
        var params = {
            channelGroupId: AgentModel.ChannelGroupID
        }
        $.ajax({
            url: _Host + "api/lc_customer/getAll",
            contentType: 'application/json; charset=utf-8',
            data: param,
            type: 'GET',
            success: function (data) {
                console.log(data)
                if (data.length != 0) {
                    var templateCustomer = '';
                    $.each(data, function (index, value) {
                        templateCustomer += new this.render().templateCustomer(value);
                    })
                    $("#div-list-customers").empty().append(templateCustomer);
                    getTimeAgo();
                }
            },
        });
    },
    getCustomerOnlineRealtime: function (customer) {
        var html = new this.render().templateCustomer(customer);
        $("#div-list-customers").prepend(html);
        getTimeAgo();
    },
    render: function () {
        this.templateCustomer = function (customer) {
            let timeCurrent = new Date();
            var templateHtml = '<a href="#" class="list-group-item d-flex">'+
                                    '<div class="pr-3">'+
                                        '<span class="avatar avatar-state-secondary">'+
                                            '<span class="avatar-title bg-warning rounded-circle">W</span>'+
                                        '</span>'+
                                    '</div>'+
                                    '<div class="flex-grow- 1">'+
                                        '<h6 class="mb-1">'+customer.Name+'</h6>'+
                                        '<span class="small text-muted">'+
                                            '<i class="fa fa-image mr-1"></i> Photo'+
                                        '</span>'+
                                    '</div>'+
                                    '<div class="text-right ml-auto">'+
                                        '<span class="small text-muted timeago" datetime="'+timeCurrent+'"></span>' +
                                    '</div>'+
                               '</a>';

            return templateHtml;

        } 
    }
}