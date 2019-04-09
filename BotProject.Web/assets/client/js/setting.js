var BotSetting = {
    ID:"",
    FormName: $("#formName").val(),
    BotID: $("#botID").val(),
    Color: $("#formColor").val(),
    Logo: $(".file-preview-image").attr('src'),
    CardID: "",
    TextIntroductory: "",
    IsActiveIntroductory: "",
    IsMDSearch: $('#statusSearch').val(),
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
                console.log(value);
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
            $("#preview-logo").empty().append('<img src="'+url+'" class="file-preview-image" alt="" />');
        })
    }
})

function getFormName(e){
    var name = $(e).val();
    parent.$("#frame_chat_setting").contents().find("#formSettingName").html(name);          
}


$("#startedButton").change(function () {
    if (this.checked) {
        $('#card-introduction').empty().append('<div class="txtStartButton form-control" maxlength="640" contenteditable="true" data-ph="Nhập văn bản (Ví dụ: Chào bạn, tôi là chat bot. Tôi có thể giúp bạn khám phá thời tiết hiện tại nơi bạn đang sống. Hãy chia sẻ vị trí của bạn cho tôi nhé!)">@Model.TextIntroductory</div>');
    } else {
        $('#card-introduction').empty().append(card());
    }
});
