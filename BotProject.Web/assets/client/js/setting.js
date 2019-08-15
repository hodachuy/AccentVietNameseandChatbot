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
    },1500)
})

$("#btnSaveSettings").on('click', function () {
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
