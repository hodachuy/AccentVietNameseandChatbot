/*
*======================================================================
* Convert non accent vietnamese to accent vietnamese
*======================================================================
*/

var delay = (function () {
    var timer = 0;
    return function (callback, ms) {
        clearTimeout(timer);
        timer = setTimeout(callback, ms);
    };
})();

$('#txtSearch').keyup(function () {
    delay(function () {
        closeOptions();
        getAccentVN('#txtSearch');
    }, 200);
});

var timer = null;
$('#txtSearch').keydown(function () {
    clearTimeout(timer);
    closeOptions();
    timer = setTimeout(function () {
        addTag('#txtSearch');
        getOptions();
    }, 1500)
});

$("#accent-vn").popover({
    title: 'Gợi ý khác',
    html: true,
    container: 'body',
    trigger: "manual",
    animation: false
}).on("mouseenter", function () {
    var _this = this;
    $(this).popover("show");
    $(".popover").on("mouseleave", function () {
        $(_this).popover('hide');
    });
}).on("mouseleave", function () {
    var _this = this;
    setTimeout(function () {
        if (!$(".popover:hover").length) {
            $(_this).popover("hide");
        }
    }, 1500);
});

function getAccentVN(element) {
    var text = $(element).text();
    $.ajax({
        url: _Host + 'api/GetAccentVN?text=' + text,
        contentType: 'application/json; charset=utf-8',
        type: 'GET',
        success: function (result) {
            if (result == "ERROR_400") {
                console.log("Load data accent vietnamese not success")
                return false;
            }
            if (text.trim() === result.Item) {
                $("#accent-vn").empty();
            }
            else {
                $("#accent-vn").html('Ý của bạn là : ' + '<a class="spell-correct-text" href="javascript:getTextAccent(\'' + result.Item + '\',\'' + element + '\')">' + result.Item + '</a>')
                var html = '';
                html += '<ul>';
                $.each(result.ArrItems, function (index, item) {
                    html += '<li><a href="javascript:getTextAccent(\'' + item + '\',\'' + element + '\')">' + item + '</a></li>';
                })
                html += '</ul>';
                console.log(html)

                var popover = $('#accent-vn').data('popover');
                $('#accent-vn').attr('data-content', html);
                var popover = $('#accent-vn').data('popover');
                popover.setContent();

                ///$(".popover").find("popover-body").empty().append(html);
                //$('#accent-vn').data('bs.popover').options.content = html;
            }
        },
    });
}

function getMultiPredictVN(element) {
    var text = $(element).text();
    $.ajax({
        url: _Host + 'api/GetMultiMatchesAccentVN?text=' + text,
        contentType: 'application/json; charset=utf-8',
        type: 'GET',
        success: function (results) {
            var alters = '<ul id ="id-options"class="options clearfix">';
            var choiceId;
            var oddColor = "odd-choice";
            var evenColor = "even-choice";
            var isOdd = true;
            var color;
            for (var i = 0; i < results.length; ++i) {
                if (i % 6 == 0) {
                    if (isOdd) {
                        color = oddColor;
                        isOdd = false;
                    } else {
                        color = evenColor;
                        isOdd = true;
                    }
                }
                choiceId = 'choice';
                alters += '<li id="choice-' + i + '" class="choice ' + color + '">&nbsp;<a style="cursor:pointer;" href="javascript:void(0);" onclick="changeTextError(\'' + results[i] + '\',this)">' + results[i] + '</a></li>';
            }
            alters += '<li id="option-cancel" class="cancel-btn"><i class="fa fa-times"></i><span class="cancel"></span></li>';
            alters += '</ul>';
            $(element).append(alters);
            $(element).css('position', 'relative');

            $('#option-cancel').click(function () {
                closeOptions();
                return false;
            });
        },
    });
}

function changeTextError(text, element) {
    event.stopPropagation;
    $(element).parent().parent().parent().text(text);
    $("#id-options").remove();
    return false;
}

function getTextAccent(text, element) {
    $(element).html(text);
    $("#accent-vn").empty();
    $("#accent-vn").popover("hide");
}

function addTag(element) {
    $("#div-cursor").remove();
    var str = $(element).text();
    str = str.replace("</span> <span class='word'>", " ");
    var i = 0;
    str = str.replace(/\s/g, "</span> <span class='word'>");


    $(element).html("<span class='word'>" + str + "</span>");
}
this.getOptions = function () {
    $("span.word").on('click', function () {
        closeOptions();
        getMultiPredictVN(this);
        return false;
    })
}

function closeOptions() {
    $("#id-options").remove();
}

String.prototype.replaceAllSpace = function (search, replacement) {
    var target = this;
    return target.replace(new RegExp(search, 'g'), replacement);
};