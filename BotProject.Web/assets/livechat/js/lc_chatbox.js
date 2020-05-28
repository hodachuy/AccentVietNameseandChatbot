//---------------------------- TRACKING CUSTOMER ----------------------------//
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
        flashVersion: flashVersion
    };
    // ip device access
    window.ipInfo = {
        ip:'',
        city:'',
        region:'',
        latitude:'',
        longtitude:''
    };
    var getIP = function () {
        var temp = null;
        $.ajax({
            type: 'GET',
            async: false,//muốn pass data ra ngoài biến nên có asynce
            global: false,
            url: 'https://ipinfo.io?token=d4b73a8d673d31',
            success: function (data) {
                tmp = data;
            }
        });
        return tmp;
    }();
    if (getIP != null) {
        ipInfo.ip = getIP.ip;
        ipInfo.city = getIP.city;
        ipInfo.region = getIP.region;
        ipInfo.latitude = getIP.loc.split(',')[0];
        ipInfo.longtitude = getIP.loc.split(',')[1];
    }

}(this));
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

var objHub = $.connection.chatHub;

$(document).ready(function () {

    //check agent online
    isAgentOnline = checkAgentOnline();

    //check have bot active
    isBotActive = checkBotActive();

    // Dang ky su kien chatHub
    cBoxHub.register();
    cBoxHub.receivedSignalFromServer();

    cBoxMessage.event();
    // close form
    $('body').on('click', '#btn-cbox-close', function (e) {
        parent.postMessage("close", "*");
    })
})

var cBoxHub = {
    eventConnect : function(){
        // set time reconecting singnalR
        var vary = function intervalFunc() {
            timeReconnecting--;
            document.getElementById("reconeting-time").innerHTML = timeReconnecting;
            console.log(timeReconnecting);
            if (timeReconnecting == 0) {
                clearInterval(intervalReconnectId);
                $('.box-reconecting').removeClass('showing');
            }
        }
    },
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
            intervalReconnectId = setInterval(eventConnect.vary(), 1500);
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
        objHub.client.receiveMessages = function (channelGroupId, threadId, message, agentId, agentName, typeUser) {
            console.log('threadId:' + threadId + '  agentId-agentName' + agentId + ' : ' + message)

        };
        objHub.client.receiveTyping = function (channelGroupId, agentId) {
            console.log('agent-' + agentId + ' typing')
        };
        objHub.client.receiveThreadChat = function (threadId, customerId) {
            console.log('revice- thread' + threadId)
            CustomerModel.ThreadID = threadId;
        };
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
                    //insertChat("customer", CustomerModel.ID, isValidURLandCodeIcon(text), "", "");
                    // gửi tin nhắn
                    objHub.server.sendMessage(_channelGroupId, CustomerModel.ThreadID, isValidURLandCodeIcon(text), CustomerModel.ID, "", TYPE_USER_CONNECT.CUSTOMER);
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
                //insertChat("customer", CustomerModel.ID, isValidURLandCodeIcon(text), "","");
                // gửi tin nhắn
                objHub.server.sendMessage(_channelGroupId, CustomerModel.ThreadID, isValidURLandCodeIcon(text), CustomerModel.ID, "", TYPE_USER_CONNECT.CUSTOMER);
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



function insertChat(who, customerId, text, userName, avatar) {
    let user_class_chat = (who == "agent" ? "me" : "");
    let date_current = new Date();

    content = '<div class="message-item ' + user_class_chat + '">';
    content += message.getUserIcon(userName, avatar);
    content += message.add(userName, text);
    content += '</div>';

    // insert body chat
    $("#message-container-" + customerId).append(content);

    // scroll to bottom
    setTimeout(function () {
        $(".messages").getNiceScroll(0).doScrollTop($("#message-container-" + customerId).prop('scrollHeight'));
    }, 100)
    return false;
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
        var templateMsg = '';
        templateMsg += '<div class="message-body">';
        templateMsg += '<div>';
        templateMsg += '<div class ="message-align">';
        templateMsg += '<span class="font-size-08">' + userName + ' </span>';
        templateMsg += '<span class="font-size-08">' + showTimeChat() + '</span>';
        templateMsg += '</div>';
        templateMsg += '</div>';
        templateMsg += '<div class="message-item-content">' + text + '</div>';
        templateMsg += '<div class="txt-align-left font-size-08">Delivered</div>';//Message not sent, , Read
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
