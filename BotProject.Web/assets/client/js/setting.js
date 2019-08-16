var checkAlert = false;
var txtError = 'Lỗi',
	txtAlert = 'Bạn có những từ khóa giống nhau!';
var BotSetting = {
    ID: $("#settingID").val(),
    FormName: $("#formName").val(),
    BotID: $("#botID").val(),
    Color: $("#formColor").val(),
    Logo: $(".file-preview-image").attr('src'),
    CardID: $("#cardID").val(),
    TextIntroductory: $("#txtIntroduct").val(),
    IsActiveIntroductory: "",
    IsMDSearch: $('#IsMdSearch').val(),
    UserID: $('#userID').val(),
    StopWord:$('#stopWord').val(),
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
        console.log(arrStopWord)
        var html = '';
        $.each(arrStopWord, function (index,value) {
            html += '<li class="addedTag ">' + value + '<span class="tagRemove">x</span><input type="hidden" value="' + value + '"></li>';
        })
        $("#ulTags").prepend(html);
    }


    parent.$("#frame_chat_setting").contents().find("#formSettingName").html(BotSetting.FormName);

    if (BotSetting.IsMDSearch == true) {
        $('#statusSearch').prop('checked', true);
    }

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
    BotSetting.StopWord = strTag;
    BotSetting.TextIntroductory = $("#txtIntro").html();
    BotSetting.IsMDSearch = $("#statusSearch").val();
    BotSetting.Logo = $(".file-preview-image").attr('src').replace("" + _Host + "", "");
    BotSetting.CardID = $("#sltCard").val();
    console.log(BotSetting)
    var urlSetting = "api/setting/createupdate";
    var svr = new AjaxCall(urlSetting, JSON.stringify(BotSetting));
    svr.callServicePOST(function (data) {
        console.log(data)
        if (data) {
            $("#model-notify").modal('hide');
            swal({
                title: "Thông báo",
                text: "Đã lưu",
                confirmButtonColor: "#EF5350",
                type: "success"
            }, function () { $("#model-notify").modal('show'); });
        }
    });
})

$('#statusSearch').change(function () {
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