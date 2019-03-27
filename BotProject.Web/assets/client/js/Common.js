var urlCreateBot = "api/bot/create",
    urlCreateBotQnA = "api/botqna/create";
var bot = {
    Name: '',
    Alias: '',
    Status: false,
    UserID: '',
}
var botQnA = {
    Name: '',
    Alias: '',
    Status: false,
    BotID: '',
    UserID:''
}
var e;
var common = {
    init: function () {
        common.registerEvents();
        common.createBot();
        common.createFormBotQnA();
    },
    registerEvents: function () {
        $('#btnLogout').off('click').on('click', function (e) {
            e.preventDefault();
            $('#frmLogout').submit();
        });
        $('body').on('click', '#btn-form-deploy', function () {
            var url = _Host + "apiv1/FormChat?token=" + $("#userId").val() + "&botID=" + $(this).attr('data-botID');
            var html = '';
            html += '<link href="'+_Host+'assets/client/css/chatbot-dialog.css" rel="stylesheet"/>';
            html += '<span style="vertical-align:bottom;width:0px;height:0px">';
            html += '<iframe name="f12691cd05677d"width="288"height="378"frameborder="0"allowtransparency="true"allowfullscreen="true"scrolling="no"';
            html += 'allow="encrypted-media"title=""src="' + url + '"';
            html += 'style="border: none;visibility: visible;width: 288pt;height: 378pt;border-radius: 9pt;bottom: 63pt;padding: 0px;';
            html += 'position: fixed;right: 9pt;top: auto;z-index: 2147483647;max-height:0px;"';
            html += 'class="fb_customer_chat_bounce_out_v2"';
            html += 'id="dialog_iframe"></iframe>';
            html += '</span>';
            html += '<div class="fb_dialog  fb_dialog_advanced fb_customer_chat_bubble_pop_in fb_customer_chat_bubble_animated_with_badge fb_customer_chat_bubble_animated_no_badge" style="background: none; border-radius: 50%; bottom: 18pt; display: inline; height: 45pt; padding: 0px; position: fixed; right: 18pt; top: auto; width: 45pt; z-index: 2147483646;">';
            html += '<div class="fb_dialog_content" style="background: none;">';
            html += '<div tabindex="0" role="button" style="cursor: pointer; outline: none;">';
            html += '<svg width="60px" height="60px" viewBox="0 0 60 60">';
            html += '<svg x="0" y="0" width="60px" height="60px"><g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd"><g><circle fill="#5969ff" cx="30" cy="30" r="30"></circle><svg x="10" y="10"><g transform="translate(0.000000, -10.000000)" fill="#FFFFFF"><g id="logo" transform="translate(0.000000, 10.000000)"><path d="M20,0 C31.2666,0 40,8.2528 40,19.4 C40,30.5472 31.2666,38.8 20,38.8 C17.9763,38.8 16.0348,38.5327 14.2106,38.0311 C13.856,37.9335 13.4789,37.9612 13.1424,38.1098 L9.1727,39.8621 C8.1343,40.3205 6.9621,39.5819 6.9273,38.4474 L6.8184,34.8894 C6.805,34.4513 6.6078,34.0414 6.2811,33.7492 C2.3896,30.2691 0,25.2307 0,19.4 C0,8.2528 8.7334,0 20,0 Z M7.99009,25.07344 C7.42629,25.96794 8.52579,26.97594 9.36809,26.33674 L15.67879,21.54734 C16.10569,21.22334 16.69559,21.22164 17.12429,21.54314 L21.79709,25.04774 C23.19919,26.09944 25.20039,25.73014 26.13499,24.24744 L32.00999,14.92654 C32.57369,14.03204 31.47419,13.02404 30.63189,13.66324 L24.32119,18.45264 C23.89429,18.77664 23.30439,18.77834 22.87569,18.45674 L18.20299,14.95224 C16.80079,13.90064 14.79959,14.26984 13.86509,15.75264 L7.99009,25.07344 Z"></path></g></g></svg></g></g></svg>';
            html += '</svg>';
            html += '</div>';
            html += '</div>';
            html += '</div>';
            $('#editorTest').text('');
            $('#editorTest').text(html);

            //e = ace.edit("editorTest");
            //e.getSession().setMode("ace/mode/html");
            //e.setTheme("ace/theme/textmate");
            //e.setValue(html);

            $("#modalDeployBot").modal('show');
        })      
    },
    getSeoTitle :function(input) {
            if (input == undefined || input == '')
                return '';
            //Đổi chữ hoa thành chữ thường
            var slug = input.toLowerCase();

            //Đổi ký tự có dấu thành không dấu
            slug = slug.replace(/á|à|ả|ạ|ã|ă|ắ|ằ|ẳ|ẵ|ặ|â|ấ|ầ|ẩ|ẫ|ậ/gi, 'a');
            slug = slug.replace(/é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ/gi, 'e');
            slug = slug.replace(/i|í|ì|ỉ|ĩ|ị/gi, 'i');
            slug = slug.replace(/ó|ò|ỏ|õ|ọ|ô|ố|ồ|ổ|ỗ|ộ|ơ|ớ|ờ|ở|ỡ|ợ/gi, 'o');
            slug = slug.replace(/ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự/gi, 'u');
            slug = slug.replace(/ý|ỳ|ỷ|ỹ|ỵ/gi, 'y');
            slug = slug.replace(/đ/gi, 'd');
            //Xóa các ký tự đặt biệt
            slug = slug.replace(/\`|\~|\!|\@|\#|\||\$|\%|\^|\&|\*|\(|\)|\+|\=|\,|\.|\/|\?|\>|\<|\'|\"|\:|\;|_/gi, '');
            //Đổi khoảng trắng thành ký tự gạch ngang
            slug = slug.replace(/ /gi, "-");
            //Đổi nhiều ký tự gạch ngang liên tiếp thành 1 ký tự gạch ngang
            //Phòng trường hợp người nhập vào quá nhiều ký tự trắng
            slug = slug.replace(/\-\-\-\-\-/gi, '-');
            slug = slug.replace(/\-\-\-\-/gi, '-');
            slug = slug.replace(/\-\-\-/gi, '-');
            slug = slug.replace(/\-\-/gi, '-');
            //Xóa các ký tự gạch ngang ở đầu và cuối
            slug = '@' + slug + '@';
            slug = slug.replace(/\@\-|\-\@|\@/gi, '');

            return slug;
    },
    createBot: function () {
        var temp = function (data) {
            var html = '';
            html += '<li class="nav-item">';
            html += '<a class="nav-link" id="dialog-bot" data-id="'+data.ID+'" href="#" data-toggle="collapse" aria-expanded="false" data-target="#submenu-' + data.ID + '" aria-controls="submenu-' + data.ID + '">';
            html += '<i class="fa fa fa-robot" aria-hidden="true"></i> ' + data.Name.toUpperCase() + '';
            html += '</a>';
            html += '<div id="submenu-' + data.ID + '" class="collapse submenu" style="">';
            html += '<ul class="nav flex-column">';
            html += '<li class="nav-item">';
            html += '<a class="nav-link" href="/bot/' + data.Alias + '/' + data.ID + '/aiml"><i class="fa fa-pen-square" aria-hidden="true"></i>Chỉnh sửa</a>';
            html += '</li>';
            html += '<li class="nav-item">';
            html += '<a class="nav-link" href="/bot/' + data.Alias + '/' + data.ID + '/cardcategory"><i class="fa fa-plus-circle" aria-hidden="true"></i>Tạo Thẻ</a>';
            html += '</li>';

            html += '<li class="nav-item">';
            html += '<a class="nav-link" href="javascript:void(0)" id="btnCreateBotQnAnswer" data-botId="' + data.ID + '"><i class="fa fa-recycle"></i>Huấn luyện bot';
            html +=                '<span style="float: right;color: lightgray;cursor: pointer;">';
            html +=                    '<i class="fa fa-plus-circle fa-icons-right" aria-hidden="true"></i>';
            html +=                '</span>';
            html +=            '</a>';
            html +=            '<div class="submenu" style="">';
            html +=                '<ul class="nav flex-column" id="form-bot-qna-' + data.ID + '">';
            html +=                '</ul>';
            html += '</div>';
            html += '</li>';
            html += '<li class="nav-item">';
            html += '<a class="nav-link" id="btn-form-deploy" href="javascript:void(0);" data-botID="' + data.ID + '"><i class="fa fa-rocket" aria-hidden="true"></i>Deploy API</a>';
            html += '</li>';
            html += '<li class="nav-item">';
            html += '<a class="nav-link" href="#"><i class="fa fa-cog" aria-hidden="true"></i>Thiết lập</a>';
            html += '</li>';
            html += '</ul>';
            html += '</div>';
            html += '</li>';
            return html;
        }
        $('body').on('click', '#btnCreateBot', function () {
            $('#txtBotName').val('');
            $('#modalCreateBot').modal('show');
        })
        $('body').on('click', '#btnSaveBot', function () {
            var botName = $('#txtBotName').val();
            if (botName == '' || botName == undefined)
                return false;

            bot.Name = botName;
            bot.Alias = common.getSeoTitle(botName);
            bot.UserID = $('#userId').val();
            var svr = new AjaxCall(urlCreateBot, JSON.stringify(bot));
            svr.callServicePOST(function (data) {
                var tempHtml = temp(data);
                $('#bot-category').append(tempHtml);
                $('#modalCreateBot').modal('hide');    
            });
        })
    },
    createFormBotQnA: function () {
        var temp = function (data) {
            var html = '';
            html += '<li class="nav-item">';
            html += '<a class="nav-link bot-qna-link" href="/bot/qna/' + data.Alias + '/' + data.ID + '"><i class="fa fa-file" aria-hidden="true"></i>' + data.Name + '</a>';
            html += '</li>';
            return html;
        }
        $('body').on('click', '#btnCreateBotQnAnswer', function () {
            var botID = $(this).attr('data-botId');
            $('#txtBotQnAnswerName').val('');
            $('#modalCreateBotQnAnswer').modal('show');
            $("#bot-botQnA-id").val(botID);
        })
        $('body').on('click', '#btnSaveBotQnA', function () {
            var botQnAName = $('#txtBotQnAnswerName').val();
            if (botQnAName == '' || botQnAName == undefined)
                return false;

            botQnA.Name = botQnAName;
            botQnA.Alias = common.getSeoTitle(botQnAName);
            botQnA.BotID = $("#bot-botQnA-id").val();
            botQnA.UserID = $("#userId").val();
            console.log(botQnA)
            var svr = new AjaxCall(urlCreateBotQnA, JSON.stringify(botQnA));
            svr.callServicePOST(function (data) {
                var tempHtml = temp(data);
                $('#form-bot-qna-'+botQnA.BotID).append(tempHtml);
                $('#modalCreateBotQnAnswer').modal('hide');
            });
        })
    }
}
common.init();