var objHub = $.connection.chatHub;
$.connection.hub.logging = true;
$.connection.hub.start({ transport: ['longPolling', 'webSockets'] });
$.connection.hub.start().done(function () {
    console.log('Agent: signalR started')
    objHub.server.connectChat('customerId', 'agentId', '1','1', 'CONNECT_QUEUE');

});
$.connection.hub.error(function (error) {
    console.log('Agent: SignalR error: ' + error)
});
var tryingToReconnect = false;
$.connection.hub.reconnecting(function () {
    tryingToReconnect = true;
    console.log('Agent: SingalR connect đang kết nối lại')
    $.connection.hub.start().done(function () {
    });
});
$.connection.hub.reconnected(function () {
    tryingToReconnect = false;
    console.log('Agent: SingalR connect đã kết nối lại')
});
$.connection.hub.disconnected(function () {
    console.log('Agent: SingalR connect ngắt kết nối')
    if (tryingToReconnect) {
        setTimeout(function () {
            console.log('Agent: SingalR connect đang khởi động lại')
            $.connection.hub.start({ transport: ['longPolling', 'webSockets'] });
            $.connection.hub.start().done(function () { });
        }, 5000); // Restart connection after 5 seconds.
    }
});
objHub.client.onConnected = function (message, customerID) {
    console.log(message)
}

objHub.client.addCustomerGotoChat = function (user, threadId) {
    console.log('Agent: admin ' + threadId);
}