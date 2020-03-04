var checkAlert = false;
var txtError = 'Lỗi',
	txtAlert = 'Bạn có những từ khóa giống nhau!';
var BotSetting = {
    ID: $("#settingID").val(),
    FormName: $("#formName").val(),
    BotID: $("#botId").val(),
    Color: $("#formColor").val(),
    Logo: $(".file-preview-image").attr('src'),
    CardID: $("#cardID").val(),
    TextIntroductory: ($("#txtIntroduct").val() == null ? "" : $("#txtIntroduct").val()),
    IsActiveIntroductory: $("#isActiveIntroduct").val(),
    IsMDSearch: $('#statusSearch').val(),
    UserID: $('#userID').val(),
    StopWord: $('#stopWord').val(),
    TimeOut:$('#TimeOut').val(),
    ProactiveMessageText: ($("#ProactiveMessageText").val() == null ? "" : $("#ProactiveMessageText").val()),
    PathCssCustom: $("#formPathCss").val()
    //IsProactiveMessage: $('#isProactiveMessage').val(),
}

$('.demo').each(function () {
    $(this).minicolors({
        control: $(this).attr('data-control') || 'hue',
        defaultValue: $(this).attr('data-defaultValue') || '',
        format: $(this).attr('data-format') || 'hex',
        keywords: $(this).attr('data-keywords') || '',
        inline: $(this).attr('data-inline') === 'true',
        letterCase: $(this).attr('data-letterCase') || 'lowercase',
        opacity: $(this).attr('data-opacity'),
        position: $(this).attr('data-position') || 'bottom left',
        swatches: $(this).attr('data-swatches') ? $(this).attr('data-swatches').split('|') : [],
        change: function(value, opacity) {
            if (!value) return;
            if (opacity) value;
            if (typeof console === 'object') {
                parent.$("#frame_chat_setting").contents().find("._6ir4").css('color', value);
                parent.$("#frame_chat_setting").contents().find("._6bir").css({ 'color': value, 'border-color': value });
                parent.$("#frame_chat_setting").contents().find("._4xks").css('background-color', value);
                parent.$("#frame_chat_setting").contents().find("circle").css('stroke', value);
                parent.$("#frame_chat_setting").contents().find("path").css('fill', value);
                parent.$("#frame_chat_setting").contents().find("g").css('fill', value);
                parent.$("#frame_chat_setting").contents().find("._4fsj").css('color', value);
                BotSetting.Color = value + ";";
            }
        },
        theme: 'bootstrap'
    });
});
$("body").on("click", '#formLogo', function (event) { event.target.value = null; });
$('body').on('change', '#formLogo', function (event) {
    var file = $(this)[0].files[0];
    var size = parseInt(file.size / 1024);
    var maxSize = parseInt(5 * 1024) // 5MB
    var el = $(this);
    if (file && file.type.match('image.*')) {
        data = new FormData();
        data.append('file', file);
        data.append('botId', BotSetting.BotID);
        $.ajax({
            url: _Host + "api/setting/importlogo",
            type: "POST",
            data: data,
            enctype: 'multipart/form-data',
            processData: false,
            contentType: false
        })
        .done(function (val) {
            var url = _Host + val;
            console.log(url)
            $("#preview-logo").empty().append('<img src="' + url + '" class="file-preview-image" alt="" width="150" height="150"/>');
            parent.$("#frame_chat_setting").contents().find(".profilePicture").attr("src", url);
        })
    }
})

function getFormName(e){
    var name = $(e).val();
    parent.$("#frame_chat_setting").contents().find("#formSettingName").html(name);
    BotSetting.FormName = name;
}
$(document).ready(function () {
    // init load
    if (BotSetting.TextIntroductory != "") {
        $('#card-introduction').empty().append('<div id="txtIntro" class="txtStartButton form-control" maxlength="640" contenteditable="true" data-ph="Nhập văn bản (Ví dụ: Chào bạn, tôi là chat bot. Tôi có thể giúp bạn khám phá thời tiết hiện tại nơi bạn đang sống. Hãy chia sẻ vị trí của bạn cho tôi nhé!)">'+BotSetting.TextIntroductory+'</div>');
    } else if (BotSetting.CardID != null || BotSetting.CardID != "") {
        $('#card-introduction').empty().append(card());
        $("#sltCard").val(BotSetting.CardID)
    }
    if (BotSetting.StopWord != "") {
        var arrStopWord = BotSetting.StopWord.split(",");
        var html = '';
        $.each(arrStopWord, function (index,value) {
            html += '<li class="addedTag ">' + value + '<span class="tagRemove">x</span><input type="hidden" value="' + value + '"></li>';
        })
        $("#ulTags").prepend(html);
    }

    parent.$("#frame_chat_setting").contents().find("#formSettingName").html(BotSetting.FormName);

    $('body').on('click', '.fileinput-remove', function () {
        $("#preview-logo").empty().append('<img src="/assets/images/user_bot.jpg" class="file-preview-image" alt="" />');
        parent.$("#frame_chat_setting").contents().find(".profilePicture").src = "/assets/images/user_bot.jpg";
    })
    setTimeout(function () {
        parent.$("#frame_chat_setting").contents().find("._6ir4").css('color', $("#formColor").val());
        parent.$("#frame_chat_setting").contents().find("._6bir").css({ 'color': $("#formColor").val(), 'border-color': $("#formColor").val() });
        parent.$("#frame_chat_setting").contents().find("._4xks").css('background-color', $("#formColor").val());
        parent.$("#frame_chat_setting").contents().find("circle").css('stroke', $("#formColor").val());
        parent.$("#frame_chat_setting").contents().find("path").css('fill', $("#formColor").val());
        parent.$("#frame_chat_setting").contents().find("g").css('fill', $("#formColor").val());
        parent.$("#frame_chat_setting").contents().find("._4fsj").css('color', $("#formColor").val());


        parent.$("#frame_chat_setting").contents().find("._1qd1_close_form circle").css('stroke', 'null');
        parent.$("#frame_chat_setting").contents().find("._1qd1_close_form g").css('fill', 'null');

        parent.$("#frame_chat_setting").contents().find("._1qd1_close_form g.g-bg-close").css('fill', 'rgba(0, 0, 0, .1)');
        parent.$("#frame_chat_setting").contents().find("._1qd1_close_form g.g-close").css('fill', '#333');
    }, 1500)
    // stop word
    initEventInputStopWord();
    // load code script deploy setting chatbot
    loadCodeScriptDeployBot();


    $('.selectKeyword').selectpicker();
    $('#BotCategoryID').on('hidden.bs.select', function (e) {
        var valBotID = $(this).selectpicker('val');
        console.log(valBotID)
        getAreaByBotId(valBotID)
    });
})

$("#btnSaveSettings").on('click', function () {
    var strTag = "";
    if ($("ul#ulTags li"). length > 1) {
        $(".addedTag").each(function (index) {
            var valTag = decodeEntities($(this).children('input').val());
            strTag += valTag.trim() + ",";
        })
        if (strTag.length > 0) {
            strTag = strTag.replace(/(^,)|(,$)/g, "");
        }
    }

    BotSetting.FacebookPageToken = $("#txtFacebookPageToken").val();
    BotSetting.FacebookAppSecrect = $("#txtFacebookSecretKey").val();
    BotSetting.ZaloPageToken = $("#txtZaloPageToken").val();

    BotSetting.TimeOut = $('#TimeOut').val();
    BotSetting.ProactiveMessageText = $("#txtProactiveMessageText").val();
    BotSetting.IsProactiveMessageFacebook = $('#isProactiveMessageFacebook').val();
    BotSetting.IsProactiveMessageZalo = $('#isProactiveMessageZalo').val();

    BotSetting.IsHaveMaintenance = $('#isHaveMaintenance').val();
    BotSetting.MessageMaintenance = $('#txtMessageMaintenance').val();

    //OTP
    BotSetting.TimeOutOTP = $('#TimeOutOTP').val();
    BotSetting.IsHaveTimeoutOTP = $('#isHaveTimeoutOTP').val();
    BotSetting.MessageTimeoutOTP = $('#txtMessageTimeoutOTP').val();

    BotSetting.PathCssCustom = $('#formPathCss').val();

    BotSetting.StopWord = strTag;
    BotSetting.TextIntroductory = $("#txtIntro").html();
    BotSetting.IsMDSearch = $("#statusSearch").val();
    BotSetting.Logo = $(".file-preview-image").attr('src').replace("" + _Host + "", "");
    BotSetting.CardID = $("#sltCard").val();
    if (BotSetting.IsMDSearch == "true") {
        if ($("#BotCategoryID").val() == "") {
            swal({
                title: "Thông báo",
                text: "Vui lòng chọn điều kiện tìm kiếm theo thể loại",
                confirmButtonColor: "#EF5350",
                type: "error"
            }, function () { $("#model-notify").modal('show'); });
            return false;
        }
    }
    console.log(BotSetting)

    var lstBotSystemConfig = [];
    console.log($("#BotCategoryID").val())
    if ($("#UrlAPI").val() != "") {
        var BotSystemConfig = {};
        BotSystemConfig.BotID = $("#botId").val();
        BotSystemConfig.Code = "UrlAPI";
        BotSystemConfig.ValueString = $("#UrlAPI").val();
        BotSystemConfig.ValueInt = "";
        lstBotSystemConfig.push(BotSystemConfig);
    }
    if ($("#BotCategoryID").val() != "") {
        var BotSystemConfig = {};
        BotSystemConfig.BotID = $("#botId").val();
        BotSystemConfig.Code = "ParamBotID";
        BotSystemConfig.ValueString = $("#BotCategoryID").val();
        BotSystemConfig.ValueInt = $("#BotCategoryID").val();
        lstBotSystemConfig.push(BotSystemConfig);
    }
    if ($("#NumberReponse").val() != "") {
        var BotSystemConfig = {};
        BotSystemConfig.BotID = $("#botId").val();
        BotSystemConfig.Code = "ParamNumberResponse";
        BotSystemConfig.ValueString = $("#NumberReponse").val();
        BotSystemConfig.ValueInt = $("#NumberReponse").val();
        lstBotSystemConfig.push(BotSystemConfig);
    }

    if ($("#AreaID").val() != "") {
        var BotSystemConfig = {};
        BotSystemConfig.BotID = $("#botId").val();
        BotSystemConfig.Code = "ParamAreaID";
        BotSystemConfig.ValueString = $("#AreaID").val();
        BotSystemConfig.ValueInt = $("#AreaID").val();
        lstBotSystemConfig.push(BotSystemConfig);
    } else {
        var BotSystemConfig = {};
        BotSystemConfig.BotID = $("#botId").val();
        BotSystemConfig.Code = "ParamAreaID";
        BotSystemConfig.ValueString = "0";
        BotSystemConfig.ValueInt = "0";
        lstBotSystemConfig.push(BotSystemConfig);
    }


    var formData = new FormData();
    formData.append('bot-setting', JSON.stringify(BotSetting));
    formData.append('bot-systemconfig', JSON.stringify(lstBotSystemConfig));

    $.ajax({
        url: _Host + "api/setting/createupdate",
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false,
        dataType: "json",
        success: function (result) {
            if (result) {
                $("#model-notify").modal('hide');
                swal({
                    title: "Thông báo",
                    text: "Đã lưu",
                    confirmButtonColor: "#EF5350",
                    type: "success"
                }, function () { $("#model-notify").modal('show'); });
            } else {
                $("#model-notify").modal('hide');
                swal({
                    title: "Thông báo",
                    text: "Lỗi dữ liệu",
                    confirmButtonColor: "#EF5350",
                    type: "error"
                }, function () { $("#model-notify").modal('show'); });
            }
        },
    });
})
$('body').on('click', '.switchery', function () {
    var tempNonChecked = '<span class="switchery switchery-default" style="box-shadow: rgb(223, 223, 223) 0px 0px 0px 0px inset; border-color: rgb(223, 223, 223); background-color: rgb(255, 255, 255); transition: border 0.4s ease 0s, box-shadow 0.4s ease 0s;"><small style="left: 0px; transition: background-color 0.4s ease 0s, left 0.2s ease 0s;background-color: #ddd;"></small></span>';
    var tempChecked = '<span class="switchery switchery-default" style="background-color: rgb(100, 189, 99); border-color: rgb(100, 189, 99); box-shadow: rgb(100, 189, 99) 0px 0px 0px 8px inset; transition: border 0.4s ease 0s, box-shadow 0.4s ease 0s, background-color 1.2s ease 0s;"><small style="left:20px; transition: background-color 0.4s ease 0s, left 0.2s ease 0s; background-color: rgb(255, 255, 255);"></small></span>';
    $(this).next().remove();
    if ($(this).prop('checked')) {
        $(this).next().remove();
        $(tempChecked).insertAfter($(this))
        $($(this).parent().parent().next().next()).removeClass("hidden");
    } else {
        $(this).next().remove();
        $(tempNonChecked).insertAfter($(this))
        $($(this).parent().parent().next().next()).addClass("hidden");
    }
})
$('body').on('click', '.btn-add-condition', function () {

})
$('#statusSearch').change(function () {
    if ($(this).is(":checked")) {
        $(this).val('true');
    } else {
        $(this).val('false');
    }
});
$('#isProactiveMessageZalo').change(function () {
    if ($(this).is(":checked")) {
        $(this).val('true');
    } else {
        $(this).val('false');
    }
});
$('#isProactiveMessageFacebook').change(function () {
    if ($(this).is(":checked")) {
        $(this).val('true');
    } else {
        $(this).val('false');
    }
});
$('#isHaveMaintenance').change(function () {
    if ($(this).is(":checked")) {
        $(this).val('true');
    } else {
        $(this).val('false');
    }
});
$('#isHaveTimeoutOTP').change(function () {
    if ($(this).is(":checked")) {
        $(this).val('true');
    } else {
        $(this).val('false');
    }
});
$("#startedButton").change(function () {
    if (this.checked) {
        $('#card-introduction').empty().append('<div id="txtIntro" class="txtStartButton form-control" maxlength="640" contenteditable="true" data-ph="Nhập văn bản (Ví dụ: Chào bạn, tôi là chat bot. Tôi có thể giúp bạn khám phá thời tiết hiện tại nơi bạn đang sống. Hãy chia sẻ vị trí của bạn cho tôi nhé!)"></div>');
    } else {
        $('#card-introduction').empty().append(card());
    }
});


function loadCodeScriptDeployBot() {
    var urlApp = _Host + "static/js/app.js";
    var encryptedUrl = CryptoJS.AES.encrypt(_Host, "Secret Passphrase").toString();
    var encryptedUserID = CryptoJS.AES.encrypt($("#userID").val(), "Secret Passphrase").toString();
    var encryptedBotID = CryptoJS.AES.encrypt($("#botId").val(), "Secret Passphrase").toString();
    var encryptedColor = CryptoJS.AES.encrypt($("#formColor").val(), "Secret Passphrase").toString();
    var html = '';
    html += "   ---Thêm đoạn mã vào trang HTML - trong thẻ body---\n";
    html += "   <!--Lacviet-->\n";
    html += "    <script>\n";
    html += "       (function (l, a, c, v, i, e, t){\n";
    html += "         a[v] = a[v] || function (){\n";
    html += "         a[v].t =+ new Date();\n";
    html += "         (a[v].q = a[v].q || []).push(arguments);};\n";
    html += "         i = l.createElement('script');\n";
    html += "         var ii = l.getElementsByTagName('script')[0];\n";
    html += "         i.async = 1;\n";
    html += "         i.src = c;\n";
    html += "         i.id = 'lacviet-script';\n";
    html += "         ii.parentNode.insertBefore(i, ii);\n";
    html += "       })(document, window, '" + urlApp + "', 'lacviet');\n";
    html += "       setTimeout(function(){lacviet.load('" + encryptedUrl + "','" + encryptedUserID + "','" + encryptedBotID + "','" + encryptedColor + "');},1500);\n";
    html += "   </script>\n";  
    html += "   <!-- End Lacviet -->"
    $('#deploy-bot').empty().text(html);
}
function initEventInputStopWord() {
    $('.wrap-content .addedTag').each(function (index, el) {
        if (!$(this).hasClass('error-tag')) {
            if ($('.wrap-content input[type="hidden"][value="' + $(this).children('input').val() + '"]').length > 1) {
                $(this).addClass('error-tag');
            }
        }
    });

    $('.wrap-content').on('click', '.tagRemove', function (event) {
        event.preventDefault();
        if ($(this).parent().hasClass('error-tag')) {
            var val = $(this).siblings('input').val();
            if ($('.wrap-content input[type="hidden"][value="' + val + '"]').length <= 2) {
                $('.wrap-content input[type="hidden"][value="' + val + '"]').parent('.addedTag').removeClass('error-tag');
            }
        }

        $(this).parent().remove();
    });

    $('.wrap-content').on('click', 'ul.tags', function (event) {
        $(this).find('.search-field').show();
        $(this).find('.search-field').focus();
    });

    $('.wrap-content').on('keypress', '.search-field', function (event) {
        var elParent = $(this).parents('.tags');
        if (event.which == '13') {
            if (($(this).val().trim() != '') && ($(".tags .addedTag input[value=\"" + $(this).val().trim() + "\"]").length == 0)) {
                $("<li class=\"addedTag\">" + $(this).val().toLowerCase().trim() + "<span class=\"tagRemove\">x</span><input type=\"hidden\" value=\"" + $(this).val().toLowerCase().trim() + "\"></li>").insertBefore($(this).parents('.tagAdd'));

                var attr = $(this).attr('attr-data');
                if (typeof attr !== typeof undefined && attr !== false) {
                    if ($('.wrap-content input[value="' + attr + '"]').length <= 1) {
                        $('.wrap-content input[value="' + attr + '"]').parent('.addedTag').removeClass('error-tag');
                    }
                    $(this).removeAttr('attr-data');
                }

                $(this).val('');
                $(this).parents('.tags').append($(this).parent().clone());
                $(this).parent().remove();
                elParent.find('.search-field').focus();
            } else if ($(".tags .addedTag input[value=\"" + $(this).val().trim() + "\"]").length > 0) {
                if (!checkAlert) {
                    checkAlert = true;
                    bootbox.alert({
                        message: txtAlert,
                        callback: function () {
                            checkAlert = false;
                        }
                    });
                }
            } else {
                $(this).val('');
            }
        }
    });

    $('.wrap-content').on('click', '.addedTag', function (event) {
        event.preventDefault();
        var elParent = $(this).parents('.tags');
        var elSearch = $(this).parents('.tags').find('.tagAdd.taglist .search-field').val();

        if (typeof elSearch != 'undefined') {
            if ($(this).parents('.tags').find('.tagAdd.taglist .search-field').val().trim() == '') {
                $(this).parents('.tags').find('.tagAdd.taglist').remove();
            } else {
                var html1 = "<li class=\"addedTag\">" + $(this).parents('.tags').find('.tagAdd.taglist .search-field').val().toLowerCase().trim() + "<span class=\"tagRemove\">x</span><input type=\"hidden\" value=\"" + $(this).parents('.tags').find('.tagAdd.taglist .search-field').val().toLowerCase().trim() + "\" data-ques-id=\"\" data-ques-symbol=\"\"></li>";
                $(this).parents('.tags').find('.tagAdd.taglist').replaceWith(html1);
            }

            var val = $(this).children('input').val().trim();
            var html = '<li class="tagAdd taglist"><input type="text" autocomplete="off" attr-data="' + val + '" class="search-field" value="" style="display: inline-block;"></li>';
            $(this).replaceWith(html);
            elParent.find('.search-field').focus().val(val);
        }

    });

    $('.wrap-content').on('focusout', '.tagAdd.taglist', function (event) {
        var elParent = $(this).parents('.tags');
        if ($(this).children('input').val().trim() != '') {
            var classTag = '';
            if ($(".tags .addedTag input[value=\"" + $(this).children('input').val().trim() + "\"]").length > 0) {
                if (!checkAlert) {
                    checkAlert = true;
                    bootbox.alert({
                        message: txtAlert,
                        callback: function () {
                            checkAlert = false;
                        }
                    });
                }
                classTag = 'error-tag';
            }
            $("<li class=\"addedTag " + classTag + "\">" + $(this).children('input').val().toLowerCase().trim() + "<span class=\"tagRemove\">x</span><input type=\"hidden\" value=\"" + $(this).children('input').val().toLowerCase().trim() + "\"></li>").insertBefore($(this));
            $(this).children('input').removeAttr('attr-data');
            $(this).children('input').val('');
            $(this).parents('.tags').append($(this).clone());
            $(this).remove();
            elParent.find('.search-field').focus();
        } else {
            $(this).children('input').val('');
        }
    })
}

function getAreaByBotId(botId) {
    var html = "";
    if (botId == "") {
        html = '<select id="AreaID" data-live-search="true" class="form-control selectKeyword checkvalid"><option value="" selected="selected" data-msgid="Select variable" data-current-language="vi">---Tất cả---</option></select>';
        $("#TempAreaID").empty().append(html);
        return;
    }
    var params = {
        botId: botId,
    };
    params = JSON.stringify(params);
    var url = "api/modulesearchengine/getareabybotid";
    var svr = new AjaxCall(url, params);
    svr.callServicePOST(function (data) {
        if (data.length != 0) {
            html += '<select id="AreaID" data-live-search="true" class="form-control selectKeyword checkvalid"><option value="" selected="selected" data-msgid="Select variable" data-current-language="vi">---Tất cả---</option>';
                $.each(data, function (index, value) {
                    html += '<option value="' + value.ID + '">' + value.Name + '</option>';
                })
            html += '</select>';
            $("#TempAreaID").empty().append(html);
        } else {
            html = '<select id="AreaID" data-live-search="true" class="form-control selectKeyword checkvalid"><option value="" selected="selected" data-msgid="Select variable" data-current-language="vi">---Tất cả---</option></select>';
            $("#TempAreaID").empty().append(html);
        }
        $("#AreaID").selectpicker();
        $('#AreaID').on('hidden.bs.select', function (e) {
            var val = $(this).selectpicker('val');
        });
    });
}

var decodeEntities = (function () {
    // this prevents any overhead from creating the object each time
    var element = document.createElement('div');
    function decodeHTMLEntities(str) {
        if (str && typeof str === 'string') {
            // strip script/html tags
            str = str.replace(/<script[^>]*>([\S\s]*?)<\/script>/gmi, '');
            str = str.replace(/<\/?\w(?:[^"'>]|"[^"]*"|'[^']*')*>/gmi, '');
            // replace special character in string
            str = str.replace(/[&\/\\#,+()$~%.'":?<>!]/g, ' ');///[&\/\\#,+()$~%.'":*?<>!]/g
            str = str.replace(/  +/g, ' ');
            //console.log(str)
            element.innerHTML = str;
            str = element.textContent;
            element.textContent = '';
        }
        return str;
    }
    return decodeHTMLEntities;
})();