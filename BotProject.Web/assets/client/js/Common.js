﻿var urlCreateBot = "api/bot/create",
    urlCreateBotQnA = "api/botqna/create",
    urlSettingBot = "api/setting/getbybotid";
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
            var urlApp = _Host + "static/js/app.js";
            var encryptedUrl = CryptoJS.AES.encrypt(_Host, "Secret Passphrase").toString();
            var encryptedUserID = CryptoJS.AES.encrypt($("#userId").val(), "Secret Passphrase").toString();
            var encryptedBotID = CryptoJS.AES.encrypt($(this).attr('data-botID'), "Secret Passphrase").toString();
            // pass variable outside ajax call
            var setting = function (botId) {              
                var tmp = null;
                var param = {
                    botId: botId
                };
                $.ajax({
                    type: 'GET',
                    async: false,
                    global: false,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    url: _Host + urlSettingBot,
                    data: param,
                    success: function (data) {
                        tmp = data;
                    }
                });
                return tmp;
            }($(this).attr('data-botID'));
            var encryptedColor = CryptoJS.AES.encrypt(setting.Color, "Secret Passphrase").toString();
            var html = '';
            html += "<!--Lacviet--><script>(function (l, a, c, v, i, e, t){a[v] = a[v] || function (){";
            html += "a[v].t =+ new Date();(a[v].q = a[v].q || []).push(arguments);};i = l.createElement('script');";
            html += "var ii = l.getElementsByTagName('script')[0];i.async = 1;i.src = c;i.id = 'lacviet-script';ii.parentNode.insertBefore(i, ii);";
            html += "})(document, window, '" + urlApp + "', 'lacviet');";
            html += "setTimeout(function(){lacviet.load('" + encryptedUrl + "','" + encryptedUserID + "','" + encryptedBotID + "','" + encryptedColor + "');},1500)</script>";
            html += "<!-- End Lacviet -->"
            $('#editorTest').text('');
            $('#editorTest').text(html);
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
            html += '<a class="nav-link" href="'+_Host+'bot/' + data.Alias + '/' + data.ID + '/cardcategory"><i class="fa fa-plus-circle" aria-hidden="true"></i>Tạo Thẻ</a>';
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
            html +=             '</div>';
            html += '</li>';
            html += '<li class="nav-item">';
            html += '<a class="nav-link" href="' + _Host + 'bot/setting/' + data.Alias + '/' + data.ID + '?name=' + data.Name + '"><i class="fa fa-cog" aria-hidden="true"></i>Cài đặt</a>';
            html += '</li>';
            html += '<li class="nav-item">';
            html += '<a class="nav-link" id="btn-form-deploy" href="javascript:void(0);" data-botID="' + data.ID + '"><i class="fa fa-rocket" aria-hidden="true"></i>Deploy API</a>';
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
            html += '<a class="nav-link bot-qna-link" href="' + _Host + 'bot/qna/' + data.Alias + '/' + data.ID + '"><i class="fa fa-file" aria-hidden="true"></i>' + data.Name + '</a>';
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