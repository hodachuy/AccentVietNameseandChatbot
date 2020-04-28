var urlCreateBot = "api/bot/create",
    urlCreateFormQnA = "api/formqna/create",
    urlSettingBot = "api/setting/getbybotid",
    urlDeleteBot = "api/bot/deletebot";
urlGetAllBot = "api/bot/getall"
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
    UserID: ''
}
var e;
var common = {
    init: function () {
        if (window.location.pathname.includes("/bot") == false) {
            sessionStorage.setItem("nav-active", "");
            sessionStorage.setItem("bot-id", "");
            sessionStorage.setItem("nav-active-sub", "");
        }
        common.getBotById();
        common.registerEvents();
        common.createBot();
        common.createFormBotQnA();
        common.eventNavbar();
    },
    eventNavbar: function () {
        //hightlight bot name
        $('#' + sessionStorage.getItem("nav-active")).attr('aria-expanded', 'true');
        $('#' + sessionStorage.getItem("nav-active")).next().addClass('show');
        $('#' + sessionStorage.getItem("nav-active")).addClass('active');
        //hightlight bot name - sub
        //$('#' + sessionStorage.getItem("nav-active-sub")).css('color', 'white');
        $('#' + sessionStorage.getItem("nav-active-sub")).addClass('active');
        //$('#' + sessionStorage.getItem("nav-active-sub")).children().css('color', 'white');

        $('body').on('click', 'li.nav-item-bot', function (e) {
            //if ($(this).children().eq(0).attr('id') !== sessionStorage.getItem("nav-active")) {
            //$('.nav-item-bot').each(function (index) {
            //    $(this).children().eq(0).next().removeClass('show');
            //    $(this).children().eq(0).removeClass('collapsed');
            //    $(this).children().eq(0).attr('aria-expanded', 'false');
            //    $(this).children().eq(0).removeClass('active');
            //    //$(this).children().eq(0).css('color', '#7a80b4')
            //})

            $(this).children().eq(0).next().removeClass('show');
            $(this).children().eq(0).removeClass('collapsed');
            $(this).children().eq(0).attr('aria-expanded', 'false');
            //$(this).children().eq(0).removeClass('active');

            //}
            var navBotID = $(this).children().eq(0).attr('id');
            //var attrBotID = $(this).children().eq(0).attr('data-id');
            if (sessionStorage.getItem("nav-active") === null) {
                sessionStorage.setItem("nav-active", navBotID);
            } else {
                if (sessionStorage.getItem("bot-id") === null) {
                    sessionStorage.setItem("nav-active", navBotID);
                }
                else if (sessionStorage.getItem("bot-id") !== null &&
                        (sessionStorage.getItem("bot-id") === $(this).children().eq(0).attr('data-id'))) {
                    sessionStorage.setItem("nav-active", navBotID);
                }
            }
            //sessionStorage.setItem("attrBotID", attrBotID);
            //$(this).children().eq(0).addClass('active');
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
        $('body').on('click', 'li.nav-item-bot-sub', function (e) {
            e.stopPropagation();
            $('.nav-item-bot-sub').each(function (index) {
                $(this).children().eq(0).css('color', '#7a80b4')
                $(this).children().eq(0).children().css('color', '#7a80b4')
                $(this).children().eq(0).removeClass('active');
            })
            var elmId = $(this).children().eq(0).attr('id');
            var botId = $(this).children().eq(0).attr('data-id');
            sessionStorage.setItem("nav-active-sub", elmId);
            sessionStorage.setItem("bot-id", botId);
            var navActive = "nav-bot-id-" + botId;
            sessionStorage.setItem("nav-active", navActive);
            $(this).children().eq(0).addClass('active');
            //$(this).children().eq(0).css('color', 'white');
            //$(this).children().eq(0).children().css('color', 'white');
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
            console.log(botId)
            console.log(botName)
            bootbox.confirm({
                message: "Ban có chắc muốn xóa Bot " + botName + "",
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
                            botId: botId
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
    getSeoTitle: function (input) {
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
            html += '<li class="nav-item nav-item-bot-sub">';
            html += '<a class="nav-link" data-id="' + data.ID + '" href="' + _Host + 'bot/' + data.Alias + '/' + data.ID + '/cardcategory?botName=' + data.Name + '" id="bot-card-' + data.ID + '"><i class="fa fa-plus-circle" aria-hidden="true"></i>Tạo thẻ tin nhắn</a>';
            html += '</li>';
            html += '<li class="nav-item nav-item-bot-sub">';
            html += '<a class="nav-link" data-id="' + data.ID + '" href="' + _Host + 'bot/' + data.Alias + '/' + data.ID + '/module?botName=' + data.Name + '" id="bot-module-' + data.ID + '"><i class="fa fa-plug" aria-hidden="true"></i>Tích hợp Module</a>';
            html += '</li>';
            html += '<li class="nav-item nav-item-bot-sub">';
            html += '<a class="nav-link" href="javascript:void(0)" id="btnCreateBotQnAnswer" data-botId="' + data.ID + '" data-botName="' + data.Name + '"><i class="fa fa-recycle"></i>Huấn luyện bot';
            html += '<span style="float: right;color: lightgray;cursor: pointer;">';
            html += '<i class="fa fa-plus-circle fa-icons-right" aria-hidden="true"></i>';
            html += '</span>';
            html += '</a>';
            html += '<div class="submenu" style="">';
            html += '<ul class="nav flex-column" id="form-bot-qna-' + data.ID + '">';
            html += '</ul>';
            html += '</div>';
            html += '</li>';
            html += '<li class="nav-item nav-item-bot-sub">';
            html += '<a class="nav-link" data-id="' + data.ID + '" href="' + _Host + 'bot/searchengine/' + data.Alias + '/' + data.ID + '?botName=' + data.Name + '" id="bot-search-' + data.ID + '"><i class="fa fa-search" aria-hidden="true"></i>Search Engineer</a>';
            html += '</li>';
            html += '<li class="nav-item nav-item-bot-sub">';
            html += '<a class="nav-link" data-id="' + data.ID + '" href="' + _Host + 'bot/setting/' + data.Alias + '/' + data.ID + '?name=' + data.Name + '" id="bot-setting-' + data.ID + '"><i class="fa fa-cog" aria-hidden="true"></i>Cài đặt</a>';
            html += '</li>';
            //html += '<li class="nav-item">';
            //html += '<a class="nav-link btn-form-deploy" href="javascript:void(0);" data-botID="' + data.ID + '"><i class="fa fa-rocket" aria-hidden="true"></i>Deploy API</a>';
            //html += '</li>';
            //html += '<li class="nav-item nav-item-bot-sub">';
            //html += '<a class="nav-link btn-form-delete" href="javascript:void(0);" data-botID="' + data.ID + '" data-botName="' + data.Name + '"><i class="fa fa-trash" aria-hidden="true"></i>Xóa bot</a>';
            //html += '</li>';
            html += '</ul>';
            html += '</div>';
            html += '</li>';
            return html;
        }
        var tempBot = function (data) {
            var html = '';
            html += '<div class="col-lg-4 col-md-6 col-12 bot-role-1" style="padding-bottom: 15px;">';
            html += '        <div class="dropdown dropleft btn-bot-setting-home show">';
            html += '            <button data-toggle="dropdown" aria-haspopup="true" aria-expanded="true" title="Menu" class="icon-btn btn-menu-bot">';
            html += '                <i class="icon-dots-vertical fas fa-ellipsis-h"></i>';
            html += '            </button>';
            html += '            <ul class="dropdown-menu dropdown-menu-right hide" x-placement="left-start" style="cursor: auto; box-shadow: rgb(220, 220, 220) 2px 2px 10px; position: absolute; will-change: transform; top: 0px; left: 45px; transform: translate3d(-212px, 3px, 0px);">';
            html += '                <li>';
            html += '                     <i class="icon-scenarios fa fa-file"></i>';
            html += '                    <span class="bot-setting-option">Kịch bản</span>';
            html += '                </li>';
            html += '                <li>';
            html += '                    <i class="icon-nlp fa fa-history"></i>';
            html += '                    <span class="bot-setting-option">Lịch sử</span>';
            html += '                </li>';
            html += '                <li>';
            html += '                    <i class="icon-message1 fa fa-cog"></i>';
            html += '                    <span class="bot-setting-option">Cài đặt</span>';
            html += '                </li>';
            html += '                <li style="color: rgb(255, 0, 0);">';
            html += '                    <i class="icon-trash fa fa-trash" style="color: rgb(255, 0, 0);"></i>';
            html += '                    <span class="bot-setting-option btn-form-delete" data-botID="' + data.ID + '" data-botName="' + data.Name + '" >Xóa Bot</span>';
            html += '                </li>';
            html += '            </ul>';
            html += '        </div>';
            html += '        <a href="/bot/setting/' + data.Alias + '/' + data.ID + '?name=' + data.Name + '">';
            html += '            <div class="bot-style">';
            html += '                <div class="bot-header">';
            html += '                    <div class="bot-icon" style="background-color: Color [A=255, R=224, G=192, B=253]; color: rgb(0, 0, 0);">';
            var arrName = data.Name.split(" ");
            if (arrName.length > 1) {
                var nameAcronym = arrName[0].substring(0, 1).toUpperCase() + arrName[1].substring(0, 1).toUpperCase();
                html += nameAcronym;
            } else {
                html += arrName[0].substring(0, 1).toUpperCase();
            }
            html += '                    </div>';
            html += '                    <span title="' + data.Name.toUpperCase() + '" class="bot-dp-name">' + data.Name.toUpperCase() + '</span>';
            html += '                    <span class="bot-dp-lang"><i class="flag-icon flag-icon-gb"></i> Tiếng Việt</span>';
            html += '                </div>';
            html += '                <div class="bot-content">';
            html += '                    <div class="row">';
            html += '                        <div title="Ý định" class="col-6" style="padding-right: 0px;">Ý định</div>';
            html += '                        <div class="col-6">0</div>';
            html += '                        <div title="Loại thực thể" class="col-6" style="padding-right: 0px;">Loại thực thể</div>';
            html += '                        <div class="col-6">0</div>';
            html += '                        <div title="Câu mẫu" class="col-6" style="border-bottom: none;">Câu kịch bản </div>';
            html += '                        <div class="col-6" style="border-bottom: none;">0</div>';
            html += '                    </div>';
            html += '                </div>';
            html += '                <div class="bot-footer">';
            html += '                    <div class="row">';
            html += '                        <div title="Lần huấn luyện gần nhất" class="col-6">Lần huấn luyện gần nhất</div>';
            html += '                        <div class="col-6"></div>';
            html += '                    </div>';
            html += '                </div>';
            html += '                <div class="bot-footer">';
            html += '                    <div class="row">';
            html += '                        <div title="Hoạt động" class="col-6">Đang hoạt động</div>';
            html += '                        <div class="col-6">';
            html += '                            <span><img title="Website" src="/assets/images/img-website.jpg" class="img-bot-activing"></span>';
            html += '                        </div>';
            html += '                    </div>';
            html += '                </div>';
            html += '            </div>';
            html += '        </a>';
            html += '    </div>';
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
                var tempHtml = tempBot(data);
                $('#collapseDiv_0').append(tempHtml);
                //$('#bot-category').append(tempHtml);
                $('#modalCreateBot').modal('hide');
            });
        })
        $('body').on('click', '.card-tmp-bot', function (e) {
            var txtBotTemplate = $(this).find('.card-title').text();
            $('#txtBotTemplate').val(txtBotTemplate);
            $('#modalCreateBot').modal('show');
        })
        $('body').on('click', '.new-blank-bot', function (e) {
            $('#txtBotTemplate').val('');
            $('#modalCreateBot').modal('show');
        })
        
        $('body').on('click', '#btnCancleSaveBot', function (e) {
            $('#modalCreateBot').modal('hide');
        })
    },
    createFormBotQnA: function () {
        var temp = function (data, botId, botName) {
            var html = '';
            html += '<li class="nav-item">';
            html += '<a class="nav-link bot-qna-link" data-id="' + botId + '" href="' + _Host + 'bot/qna?formQnAId=' + data.ID + '&botId=' + botId + '&botName=' + botName + '" id="bot-scenarios-' + botId + '-' + data.ID + '"><i class="fa fa-file" aria-hidden="true" style="display:unset"></i>' + data.Name + '</a>';
            html += '</li>';
            return html;
        }
        $('body').on('click', '#btnCreateBotQnAnswer', function (e) {
            e.stopPropagation();
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
                $('#form-bot-qna-' + formQnA.BotID).append(tempHtml);
                $('#modalCreateBotQnAnswer').modal('hide');
                window.location.href = _Host + 'bot/qna?formQnAId=' + data.ID + "&botId=" + formQnA.BotID + "&botName=" + $("#bot-botQnA-name").val();
            });
        })
    },
    getBotById: function () {
        var botId = $("#botId").val();
        if (botId != undefined) {
            var param = {
                userID: $("#userId").val()
            }
            var svr = new AjaxCall(urlGetAllBot, param);
            svr.callServiceGET(function (response) {
                if (response.length != 0) {
                    $(".nav-divider").hide();
                    var botVm = response.filter(function (x) { return x.ID == botId; });
                    var data = botVm[0];
                    var html = '';
                    html += '<li class="nav-item nav-item-bot">';
                    html += '<a class="nav-link active" id="nav-bot-id-' + data.ID + '" data-id="' + data.ID + '" href="#" data-toggle="collapse" aria-expanded="false" data-target="#submenu-' + data.ID + '" aria-controls="submenu-' + data.ID + '">';
                    html += '<img src="'+_Host+'/assets/images/logo/icon-bot-v2.png" style="width:40px;height:30px"/>BOT - ' + data.Name.toUpperCase() + '';
                    html += '</a>';
                    html += '<div id="submenu-' + data.ID + '" class="submenu collapse show" style="">';
                    html += '<ul class="nav flex-column">';
                    html += '<li class="nav-item nav-item-bot-sub">';
                    html += '<a class="nav-link" data-id="' + data.ID + '" href="' + _Host + 'bot/' + data.Alias + '/' + data.ID + '/cardcategory?botName=' + data.Name + '" id="bot-card-' + data.ID + '"><i class="fa fa-plus-circle" aria-hidden="true"></i>Tạo thẻ tin nhắn</a>';
                    html += '</li>';
                    html += '<li class="nav-item nav-item-bot-sub">';
                    html += '<a class="nav-link" data-id="' + data.ID + '" href="' + _Host + 'bot/' + data.Alias + '/' + data.ID + '/module?botName=' + data.Name + '" id="bot-module-' + data.ID + '"><i class="fa fa-plug" aria-hidden="true"></i>Tích hợp Module</a>';
                    html += '</li>';
                    html += '<li class="nav-item nav-item-bot-sub">';
                    html += '<a class="nav-link" href="javascript:void(0)" id="btnCreateBotQnAnswer" data-botId="' + data.ID + '" data-botName="' + data.Name + '"><i class="fa fa-recycle"></i>Huấn luyện bot';
                    html += '<span style="float: right;color: lightgray;cursor: pointer;">';
                    html += '<i class="fa fa-plus-circle fa-icons-right" aria-hidden="true"></i>';
                    html += '</span>';
                    html += '</a>';
                    html += '<div class="submenu" style="">';
                    html += '<ul class="nav flex-column" id="form-bot-qna-' + data.ID + '">';
                    if (data.FormQuestionAnswers.length != 0) {
                        $.each(data.FormQuestionAnswers, function (index, value) {
                            html += '<li class="nav-item nav-item-bot-sub">';
                            html += '<a class="nav-link bot-qna-link" data-id="' + value.ID + '" id="bot-scenarios-' + data.ID + '-' + value.ID + '" href="' + _Host + 'bot/qna/?formQnAId=' + value.ID + '&botId=' + data.ID + '&botName=' + data.Name + '"><i class="fa fa-file" aria-hidden="true" style="display:unset"></i>' + value.Name + '</a>';
                            html += '</li>';
                        })
                    }
                    html += '</ul>';
                    html += '</div>';
                    html += '</li>';
                    html += '<li class="nav-item nav-item-bot-sub">';
                    html += '<a class="nav-link" data-id="' + data.ID + '" href="' + _Host + 'bot/searchengine/' + data.Alias + '/' + data.ID + '?botName=' + data.Name + '" id="bot-search-' + data.ID + '"><i class="fa fa-search" aria-hidden="true"></i>Search Engineer</a>';
                    html += '</li>';
                    if (data.ID == 3019) {
                        html += '    <li class="nav-item nav-item-bot-sub">';
                        html += '        <a class="nav-link" href="' + _Host + 'bot/medicalsymptoms/' + data.Alias + '/' + data.ID + '?botName=' + data.Name + '" data-id="' + data.ID + '" id="bot-med-' + data.ID + '"><i class="fa fa-briefcase-medical" aria-hidden="true"></i>Triệu chứng y tế</a>';
                        html += '    </li>';
                    }
                    html += '<li class="nav-item nav-item-bot-sub">';
                    html += '<a class="nav-link" data-id="' + data.ID + '" href="' + _Host + 'bot/setting/' + data.Alias + '/' + data.ID + '?name=' + data.Name + '" id="bot-setting-' + data.ID + '"><i class="fa fa-cog" aria-hidden="true"></i>Cài đặt</a>';
                    html += '</li>';
                    //html += '<li class="nav-item">';
                    //html += '<a class="nav-link btn-form-deploy" href="javascript:void(0);" data-botID="' + data.ID + '"><i class="fa fa-rocket" aria-hidden="true"></i>Deploy API</a>';
                    //html += '</li>';
                    html += '<li class="nav-item nav-item-bot-sub">';
                    html += '    <a class="nav-link" href="' + _Host + 'bot/history/' + data.Alias + '/' + data.ID + '?botName=' + data.Name + '" data-id="' + data.ID + '" id="bot-history-' + data.ID + '"><i class="fa fa-history" aria-hidden="true"></i>Lịch sử</a>';
                    html += '</li>';
                    //html += '<li class="nav-item nav-item-bot-sub">';
                    //html += '<a class="nav-link btn-form-delete" href="javascript:void(0);" data-botID="' + data.ID + '" data-botName="' + data.Name + '"><i class="fa fa-trash" aria-hidden="true"></i>Xóa bot</a>';
                    //html += '</li>';
                    html += '</ul>';
                    html += '</div>';
                    html += '</li>';
                    $('#bot-category').empty().append(html);
                    common.eventNavbar();
                    var tempHeaderBotCategory = '';
                    tempHeaderBotCategory += '<ul class="navbar-nav mr-auto navbar-left-top">';
                    tempHeaderBotCategory += '<li class="nav-item dropdown nav-bot">';
                    tempHeaderBotCategory += '<a class="nav-link nav-user-img" href="#" id="navbarDropdownMenuLink2" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="color:aliceblue;border:1px solid darkgray;margin-left: 10px;">';
                    tempHeaderBotCategory += '<i class="fa fa fa-robot mr-2" aria-hidden="true"></i>';
                    tempHeaderBotCategory += data.Name.toUpperCase();
                    tempHeaderBotCategory += '<i class="fa fa-angle-down ml-2 opacity-5"></i>';
                    tempHeaderBotCategory += '</a>';
                    tempHeaderBotCategory += '<div class="dropdown-menu nav-user-dropdown" aria-labelledby="navbarDropdownMenuLink2" style="left:auto;padding-top: 10px;margin-top: 3px;">';
                    $.each(response, function (index, value) {
                        if (value.ID == botId) {
                            tempHeaderBotCategory += '<a class="dropdown-item" href="javascript:void(0);"><i class="fa fa fa-robot mr-2"></i>' + value.Name.toUpperCase() + '<i class="fa fa fa-check ml-2"></i></a>';
                        } else {
                            tempHeaderBotCategory += '<a class="dropdown-item" href="' + _Host + 'bot/setting/' + value.Alias + '/' + value.ID + '?name=' + value.Name + '"><i class="fa fa fa-robot mr-2"></i>' + value.Name.toUpperCase() + '</a>';
                        }
                    })
                    tempHeaderBotCategory += '</div>';
                    tempHeaderBotCategory += '</li>';
                    tempHeaderBotCategory += '</ul>';
                    $("#navBotCategory").empty().append(tempHeaderBotCategory);
                }
            });
        }
    }
}
common.init();