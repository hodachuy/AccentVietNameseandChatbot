var url = 'http://localhost:54160/',
    domainApp = 'http://localhost:54160/';

var lacviet = {
    setup: function (channelGroupId) {
        console.log(channelGroupId)
        lacviet.initCSS();
        lacviet.initFormChat(channelGroupId);
        //lacviet.tempPopupQnA();
        lacviet.modalContainer();
        //domainApp = decryptedUrl.slice(0, -1);

        //var modal = document.querySelector(".bot-modal");
        //var closeButton = document.querySelector(".bot-close-button");
        //function toggleModal() {
        //    modal.classList.toggle("bot-show-modal");
        //}
        //closeButton.addEventListener("click", toggleModal);
    },
    initFormChat: function (channelGroupId) {
        //var decryptedUrl = CryptoJS.AES.decrypt(encryptUrl, "Secret Passphrase").toString(CryptoJS.enc.Utf8);
        var styleDiv = 'opacity:1;visibility:visible;z-index:2147483639;position:fixed;bottom:0px;max-width: 100%;max-height:calc(100% - 0px);min-height:0px;min-width: 0px;background-color:transparent; border:0px;overflow:hidden;right: 0px;transition: none 0s ease 0s!important;';
        $("<div id='dialog-form-bot' style=\"" + styleDiv + "\"></div>").appendTo("body");
        var html = '',
        styleIframeCustom = 'style= "width: 100%;';
        styleIframeCustom += 'height: 100%;';//neu mo 100%
        styleIframeCustom += 'min-height: 0px;'
        styleIframeCustom += 'min-width: 0px;'
        styleIframeCustom += 'margin: 0px;'
        styleIframeCustom += 'padding: 0px;'
        styleIframeCustom += 'background-image: none;'
        styleIframeCustom += 'background-position: 0% 0%;'
        styleIframeCustom += 'background-size: initial;'
        styleIframeCustom += 'background-attachment: scroll;'
        styleIframeCustom += 'background-origin: initial;'
        styleIframeCustom += 'background-clip: initial;'
        styleIframeCustom += 'background-color: rgba(0, 0, 0, 0);'
        styleIframeCustom += 'border-width: 0px;'
        styleIframeCustom += 'float: none;'
        styleIframeCustom += 'position: absolute;'
        styleIframeCustom += 'top: 0px;'
        styleIframeCustom += 'left: 0px;'
        styleIframeCustom += 'bottom: 0px;'
        styleIframeCustom += 'right: 0px;'
        styleIframeCustom += 'z-index: 5;'
        styleIframeCustom += 'transition: none 0s ease 0s!important;"';
        url = url + "LcChatBox/Index?channelGroupID=2";
        html += '<span style="vertical-align:bottom;width:0px;height:0px">';
        html += '<iframe name="f12691cd05677d" frameborder="0"allowtransparency="true"allowfullscreen="true"scrolling="no"';
        html += 'allow="encrypted-media"title=""src="' + url + '"';
        html += styleIframeCustom;
        //html += 'style="border: none;visibility: visible;width: 288pt;height: 378pt;border-radius: 9pt;bottom: 63pt;padding: 0px;';
        //html += 'position: fixed;right: 9pt;top: auto;z-index: 2147483646;max-height:0px;"';
        html += 'class="fb_customer_chat_bounce_out_v2" id="dialog_iframe"></iframe>';
        html += '</span>';
        html += '<div class="fb_dialog fb_dialog_advanced fb_customer_chat_bubble_pop_in fb_customer_chat_bubble_animated_with_badge fb_customer_chat_bubble_animated_no_badge" style="background: white; border-radius: 50%; bottom: 18pt; display: inline; height: 45pt; padding: 0px; position: fixed; right: 18pt; top: auto; width: 45pt; z-index: 3;">';
        html += '<div class="fb_dialog_content" style="background: none;">';
        html += '<div tabindex="0" role="button" style="cursor: pointer; outline: none;">';
        //html += '<svg width="60px" height="60px" viewBox="0 0 60 60">';
        //html += '<svg x="0" y="0" width="60px" height="60px"><g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd"><g><circle fill="#5969ff" style="fill:' + color + '" cx="30" cy="30" r="30"></circle><svg x="10" y="10"><g transform="translate(0.000000, -10.000000)" fill="#FFFFFF"><g id="logo" transform="translate(0.000000, 10.000000)"><path d="M20,0 C31.2666,0 40,8.2528 40,19.4 C40,30.5472 31.2666,38.8 20,38.8 C17.9763,38.8 16.0348,38.5327 14.2106,38.0311 C13.856,37.9335 13.4789,37.9612 13.1424,38.1098 L9.1727,39.8621 C8.1343,40.3205 6.9621,39.5819 6.9273,38.4474 L6.8184,34.8894 C6.805,34.4513 6.6078,34.0414 6.2811,33.7492 C2.3896,30.2691 0,25.2307 0,19.4 C0,8.2528 8.7334,0 20,0 Z M7.99009,25.07344 C7.42629,25.96794 8.52579,26.97594 9.36809,26.33674 L15.67879,21.54734 C16.10569,21.22334 16.69559,21.22164 17.12429,21.54314 L21.79709,25.04774 C23.19919,26.09944 25.20039,25.73014 26.13499,24.24744 L32.00999,14.92654 C32.57369,14.03204 31.47419,13.02404 30.63189,13.66324 L24.32119,18.45264 C23.89429,18.77664 23.30439,18.77834 22.87569,18.45674 L18.20299,14.95224 C16.80079,13.90064 14.79959,14.26984 13.86509,15.75264 L7.99009,25.07344 Z"></path></g></g></svg></g></g></svg>';
        //html += '</svg>';

        html += '<svg width="60px" height="60px" color="rgb(67, 132, 245);" class="lc-1mpchac" viewBox="0 0 32 32"><path d="M11.9,26H8.6c-3.3,0-6.2-2.4-6.4-5.8C1.9,17,2,12,2.2,8.9c0.3-3.1,2.8-5.4,5.8-5.6c4.8-0.3,11-0.3,15.9,0 c3.1,0.2,5.6,2.6,5.8,5.7c0.2,3.1,0.2,8.2,0,11.2c-0.3,3.4-3.2,5.8-6.4,5.8h-2.5c-0.4,0-0.9,0.2-1.2,0.4l-6.1,4.4 C12.9,31.3,12,30.9,12,30v-4H11.9z"></path></svg>'

        html += '</div>';
        html += '</div>';
        html += '</div>';
        $("#dialog-form-bot").empty().append(html);
    },
    initCSS: function () {
        //var decryptedUrl = CryptoJS.AES.decrypt(encryptUrl, "Secret Passphrase").toString(CryptoJS.enc.Utf8);
        var headID = document.getElementsByTagName('head')[0];
        var link = document.createElement('link');
        link.type = 'text/css';
        link.rel = 'stylesheet';
        headID.appendChild(link);
        link.href = domainApp + "assets/livechat/css/chatbox-dialog.css";
    },
    modalContainer: function () {
        var tempModalContainer = '';
        tempModalContainer += '<div class="bot-modal" style="visibility:hidden; z-index:999999999999">';
        tempModalContainer += '        <div class="bot-modal-content large_bg">';
        tempModalContainer += '            <span class="bot-close-button">×</span>';
        tempModalContainer += '            <div id="bot-iframe">';
        tempModalContainer += '            </div>';
        tempModalContainer += '        </div>';
        tempModalContainer += '</div>';
        $(tempModalContainer).appendTo("body");
    },
    tempPopupQnA: function () {
        $('<div class="modal fade" id="cb-ques-popup" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" style="display:none;z-index:999999999999"></div>').appendTo("body");
        var html = '';
        html += ' <div class="modal-dialog modal-question-answer modal-sm modal-lg modal-md" role="document">';
        html += '    <div class="modal-content">';
        html += '        <div class="modal-header">';
        html += '            <button type="button" class="close" data-dismiss="modal">&times;</button>';
        html += '            <h5 class="modal-title" id="exampleModalLabel">Chi tiết câu hỏi</h5>';
        html += '        </div>';
        html += '        <div class="modal-body">';
        html += '            <form>';
        html += '                <div class="form-group">';
        html += '                    <i style="margin-right:14px" class="fa fa-question-circle icon-question-detail"></i>';
        html += '                    <label for="exampleFormControlTextarea1">CÂU HỎI</label>';
        html += '                    <div class="question-detail" id="cb-question">';
        html += '                    </div>';
        html += '                </div>';
        html += '                <div class="form-group">';
        html += '                    <i style="margin-right:10px" class="fa fa-comments icon-question-detail"></i>';
        html += '                    <label for="exampleFormControlTextarea1">TRẢ LỜI</label>';
        html += '                    <div class="answer-detail" id="cb-answer">';
        html += '                    </div>';
        html += '                </div>';
        html += '                <div class="form-group" id="cb-lst-article">';
        html += '                    <i style="margin-right:12px" class="fa fa-book icon-question-detail"></i>';
        html += '                    <label for="exampleFormControlTextarea1">ĐIỀU LUẬT LIÊN QUAN THAM KHẢO</label>';
        html += '                    <div class="text-question-detail">';
        html += '                        <div id="cb-msg"></div>';
        html += '                        <div id="cb-info-article"></div>';
        html += '                    </div>';
        html += '                </div>';
        html += '            </form>';
        html += '        </div>';
        html += '    </div>';
        html += '</div>';
        $("#cb-ques-popup").empty().append(html);
    }
};
$('body').on('click', '.fb_dialog', function (e) {
    if ($("#dialog_iframe").hasClass("fb_customer_chat_bounce_out_v2")) {
        $("#dialog_iframe").removeClass('fb_customer_chat_bounce_out_v2').addClass('fb_customer_chat_bounce_in_v2');
        //$("#dialog_iframe").removeClass('fb_customer_chat_bounce_out_v2').addClass('fb_customer_chat_bounce_in_v2');
        if ($(parent.window).width() <= 768) {
            //console.log($(parent.window).width())
            $('#dialog-form-bot').css('max-height', '100%');
            $('#dialog-form-bot').css('width', '100%');
            $('#dialog-form-bot').css('height', '100%');
            $('#dialog-form-bot').css('right', '0');
            $('#dialog-form-bot').css('top', '0');
            $('#dialog-form-bot').css('bottom', '0');
            var frame = document.getElementById('dialog_iframe');
            frame.contentWindow.postMessage($(parent.window).width(), domainApp);
        }
        else {
            $('#dialog-form-bot').css('width', '382px');
            $('#dialog-form-bot').css('height', '652px');
        }

        setTimeout(function () {
            // bung chiều cao ô chatbox
            $('#dialog_iframe').css('max-height', '100%');
            // init message card getstarted
            var frame = document.getElementById('dialog_iframe');
            frame.contentWindow.postMessage('init', domainApp);
        }, 200)
    }
    else if ($("#dialog_iframe").hasClass("fb_customer_chat_bounce_in_v2")) {
        $("#dialog_iframe").removeClass('fb_customer_chat_bounce_in_v2').addClass('fb_customer_chat_bounce_out_v2');
        setTimeout(function () {
            $('#dialog_iframe').css('max-height', '0px');
        }, 200)
    }
})

var eventMethod = window.addEventListener ? "addEventListener" : "attachEvent";
var eventer = window[eventMethod];
var messageEvent = eventMethod == "attachEvent" ? "onmessage" : "message";
// Nghe tin nhắn từ iframe gởi tới trang cha khi bấm close 
eventer(messageEvent, function (e) {
    var key = e.message ? "message" : "data";
    var data = e[key];
    //console.log(e.data)
    //console.log(e.origin)
    //console.log(domainApp)
    if (e.data == 'close') {
        // có tín hiệu đóng
        $('.fb_dialog').click();
    } else {
        //console.log(e.data)
        //if (e.data != undefined) {
        //    var modal = document.querySelector(".bot-modal");
        //    $("#bot-iframe").empty().append('<iframe style="width:49rem" width="100" height="378"frameborder="0"allowtransparency="true"allowfullscreen="true" src="' + e.data + '"></iframe>');
        //    modal.classList.toggle("bot-show-modal");
        //}
    }
    //if (e.origin === domainApp.replace("/tiengviet", "")) {
    //    if (e.data != 'close') {
    //        //call function page QnA
    //        window.GetQuesDetailPopup(event.data)
    //    }
    //};
}, false);