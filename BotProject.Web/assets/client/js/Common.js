var urlCreateBot = "api/bot/create",
    urlCreateFormQnA = "api/formqna/create",
    urlSettingBot = "api/setting/getbybotid",
    urlDeleteBot = "api/bot/deletebot";
var bot = {
    Name: '',
    Alias: '',
    Status: false,
    UserID: '',
}
var formQnA = {
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
        common.eventNavbar();
    },
    eventNavbar: function () {
        $(document).ready(function () {
            $('#' + sessionStorage.getItem("nav-active")).attr('aria-expanded', 'true');
            $('#' + sessionStorage.getItem("nav-active")).next().addClass('show');
            $('#' + sessionStorage.getItem("nav-active")).css('color','white')
        })
        $('body').on('click', 'li.nav-item-bot', function (e) {
            $('.nav-item-bot').each(function (index) {
                $(this).children().eq(0).next().removeClass('show');
                $(this).children().eq(0).removeClass('collapsed');
                $(this).children().eq(0).attr('aria-expanded', 'false');
                $(this).children().eq(0).css('color', '#7a80b4')
            })
            var navBotID = $(this).children().eq(0).attr('id');
            sessionStorage.setItem("nav-active", navBotID);
            $(this).children().eq(0).css('color', 'white');
        })
        $('body').on('click', 'li.nav-item-cog', function (e) {
            sessionStorage.setItem("nav-active", "");
            $('.nav-item-bot').each(function (index) {
                $(this).children().eq(0).next().removeClass('show');
                $(this).children().eq(0).removeClass('collapsed');
                $(this).children().eq(0).attr('aria-expanded', 'false');
                $(this).children().eq(0).css('color', '#7a80b4')
            })
        })
    },
    registerEvents: function () {
        $('#btnLogout').off('click').on('click', function (e) {
            e.preventDefault();
            $('#frmLogout').submit();
        });
        $('body').on('click', '.btn-form-deploy', function (e) {
            e.stopPropagation();
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
                    async: false,//muốn pass data ra ngoài biến nên có asynce
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
            $('#editorTest').empty().text(html);
            $("#modalDeployBot").modal('show');
        });

        $('body').on('click', '.btn-form-delete', function () {
            var botId = $(this).attr('data-botID'),
                botName = $(this).attr('data-botName');
            bootbox.confirm({
                message: "Ban có chắc muốn xóa Bot "+botName+"",
                buttons: {
                    confirm: {
                        label: "Đồng ý",
                        className: 'btn-primary'
                    },
                    cancel: {
                        label: "Hủy",
                        className: 'btn-default'
                    }
                },
                callback: function (result) {
                    if (result) {
                        var params = {
                            botId : botId
                        }
                        params = JSON.stringify(params);
                        var svr = new AjaxCall(urlDeleteBot, params);
                        svr.callServicePOST(function (data) {
                            console.log(data)
                            location.reload();
                        });
                    }
                }
            })
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
            html += '<li class="nav-item nav-item-bot">';
            html += '<a class="nav-link collapsed" id="nav-bot-id-' + data.ID + '" data-id="' + data.ID + '" href="#" data-toggle="collapse" aria-expanded="false" data-target="#submenu-' + data.ID + '" aria-controls="submenu-' + data.ID + '">';
            html += '<i class="fa fa fa-robot" aria-hidden="true"></i> ' + data.Name.toUpperCase() + '';
            html += '</a>';
            html += '<div id="submenu-' + data.ID + '" class="collapse submenu" style="">';
            html += '<ul class="nav flex-column">';
            html += '<li class="nav-item">';
            html += '<a class="nav-link" href="' + _Host + 'bot/' + data.Alias + '/' + data.ID + '/cardcategory?botName=' + data.Name + '"><i class="fa fa-plus-circle" aria-hidden="true"></i>Tạo Thẻ</a>';
            html += '</li>';
            html += '<li class="nav-item">';
            html += '<a class="nav-link" href="' + _Host + 'bot/' + data.Alias + '/' + data.ID + '/module?botName=' + data.Name + '"><i class="fa fa-plug" aria-hidden="true"></i>Tích hợp Module</a>';
            html += '</li>';
            html += '<li class="nav-item">';
            html += '<a class="nav-link" href="javascript:void(0)" id="btnCreateBotQnAnswer" data-botId="' + data.ID + '" data-botName="' + data.Name + '"><i class="fa fa-recycle"></i>Huấn luyện bot';
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
            html += '<a class="nav-link" href="' + _Host + 'bot/searchengine/' + data.Alias + '/' + data.ID + '?botName=' + data.Name + '"><i class="fa fa-search" aria-hidden="true"></i>Search Engineer</a>';
            html += '</li>';
            html += '<li class="nav-item">';
            html += '<a class="nav-link" href="' + _Host + 'bot/setting/' + data.Alias + '/' + data.ID + '?name=' + data.Name + '"><i class="fa fa-cog" aria-hidden="true"></i>Cài đặt</a>';
            html += '</li>';
            html += '<li class="nav-item">';
            html += '<a class="nav-link btn-form-deploy" href="javascript:void(0);" data-botID="' + data.ID + '"><i class="fa fa-rocket" aria-hidden="true"></i>Deploy API</a>';
            html += '</li>';
            html += '<li class="nav-item">';
            html += '<a class="nav-link btn-form-delete" href="javascript:void(0);" data-botID="' + data.ID + '" data-botName="' + data.Name + '"><i class="fa fa-trash" aria-hidden="true"></i>Delete</a>';
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
            bot.Status = true;
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
        var temp = function (data,botId,botName) {
            var html = '';
            html += '<li class="nav-item">';
            html += '<a class="nav-link bot-qna-link" href="' + _Host + 'bot/qna?formQnAId=' + data.ID + '&botId=' + botId + '&botName='+botName+'"><i class="fa fa-file" aria-hidden="true" style="display:unset"></i>' + data.Name + '</a>';
            html += '</li>';
            return html;
        }
        $('body').on('click', '#btnCreateBotQnAnswer', function () {
            var botID = $(this).attr('data-botId');
            var botName = $(this).attr('data-botName');
            $('#txtBotQnAnswerName').val('');
            $('#modalCreateBotQnAnswer').modal('show');
            $("#bot-botQnA-id").val(botID);
            $("#bot-botQnA-name").val(botName);
        })
        $('body').on('click', '#btnSaveBotQnA', function () {
            var formName = $('#txtBotQnAnswerName').val();
            if (formName == '' || formName == undefined)
                return false;

            formQnA.Name = formName;
            formQnA.Alias = common.getSeoTitle(formName);
            formQnA.BotID = $("#bot-botQnA-id").val();
            formQnA.UserID = $("#userId").val();
            console.log(formQnA)
            var svr = new AjaxCall(urlCreateFormQnA, JSON.stringify(formQnA));
            svr.callServicePOST(function (data) {
                var tempHtml = temp(data, formQnA.BotID, $("#bot-botQnA-name").val());
                $('#form-bot-qna-'+formQnA.BotID).append(tempHtml);
                $('#modalCreateBotQnAnswer').modal('hide');
            });
        })
    }
}
common.init();