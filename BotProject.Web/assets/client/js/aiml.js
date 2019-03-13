var html = '';
var editorLegal,
    editorMed,
    editorEdu,
    editorTour,
    mode;
var e;
$(document).ready(function () {

    loadAIML();


    //YUI().use('aui-ace-editor', function (Y) {
    //    editorLegal = new Y.AceEditor(
    //      {
    //          boundingBox: '#editorAIML_legal',
    //          mode: 'xml',
    //          value: html,
    //          height: '550',
    //          width: '100%',
    //      }
    //    ).render();
    //    editorMed = new Y.AceEditor(
    //      {
    //          boundingBox: '#editorAIML_med',
    //          mode: 'xml',
    //          value: '<?xml version="1.0" encoding="UTF-8"?>',
    //          height: '1000',
    //          width: '750'
    //      }
    //    ).render();
    //    editorEdu = new Y.AceEditor(
    //    {
    //        boundingBox: '#editorAIML_edu',
    //        mode: 'xml',
    //        value: '<?xml version="1.0" encoding="UTF-8"?>',
    //        height: '1000',
    //        width: '750'
    //    }
    //   ).render();
    //    editorTour = new Y.AceEditor(
    //     {
    //         boundingBox: '#editorAIML_tour',
    //         mode: 'xml',
    //         value: '<?xml version="1.0" encoding="UTF-8"?>',
    //         height: '1000',
    //         width: '750'
    //     }
    //    ).render();
    //});
})

var templateAIML = {
    category: "\n<category>\n" +
                "   <pattern></pattern>\n" +
                "   <template></template>\n" +
                "</category>\n",

    btnUrl: "\n<button>\n" +
                "   <text></text>\n" +
                "   <url></url>\n" +
                "</button>\n",
    btnPostback: "\n<button>\n" +
            "   <text></text>\n" +
            "   <postback></postback>\n" +
            "</button>\n",
    btnMenu: "\n<button>\n" +
            "   <text></text>\n" +
            "   <menu></menu>\n" +
            "</button>\n",

    link: "\n<link>\n" +
            "   <text></text>\n" +
            "   <url></url>\n" +
            "</link>\n",

    image: "\n<image></image>\n",

    video: "\n<video></video>\n",

    card: "\n<card>\n" +
            "   <image></image>\n" +
            "   <title></title>\n" +
            "   <subtitle></subtitle>\n" +
            "   <button>\n" +
            "       <text></text>\n" +
            "       <postback></postback>\n" +
            "   </button>\n" +
            "</card>\n",

    carousel: "\n<carousel>\n" +
            "   <card>\n" +
            "       <image></image>\n" +
            "       <title></title>\n" +
            "       <subtitle></subtitle>\n" +
            "       <button>\n" +
            "           <text></text>\n" +
            "           <postback></postback>\n" +
            "       </button>\n" +
            "   </card>\n" +
            "</carousel>\n",
}

$(".btn-insert-template").off().on('click', function () {
    var cursorPosition = e.getCursorPosition();
    var type = $(this).attr("data-value");
    switch (type) {
        case "category":
            return e.session.insert(cursorPosition, templateAIML.category);
            break;
        case "btn-url":
            return e.session.insert(cursorPosition, templateAIML.btnUrl);
            break;
        case "btn-postback":
            return e.session.insert(cursorPosition, templateAIML.btnPostback);
            break;
        case "btn-menu":
            return e.session.insert(cursorPosition, templateAIML.btnMenu);
            break;
        case "link":
            return e.session.insert(cursorPosition, templateAIML.link);
            break;
        case "image":
            return e.session.insert(cursorPosition, templateAIML.image);
            break;
        case "video":
            return e.session.insert(cursorPosition, templateAIML.video);
            break;
        case "card":
            return e.session.insert(cursorPosition, templateAIML.card);
            break;
        case "carousel":
            return e.session.insert(cursorPosition, templateAIML.carousel);
            break;
        default:
            break;
    }
})

function loadAIML() {
    $.ajax({
        url: _Host + 'api/LoadAIML',
        contentType: 'application/json; charset=utf-8',
        type: 'GET',
        success: function (result) {
            html = result;
            setTimeout(function () {
                e = ace.edit("editorTest");
                e.getSession().setMode("ace/mode/xml");
                e.setTheme("ace/theme/textmate");
                e.setValue(html);

                //var editorMed = ace.edit("editorTest-med");
                //editorMed.getSession().setMode("ace/mode/xml");
                //editorMed.setTheme("ace/theme/textmate");
                //editorMed.setValue("<xml?>");
            }, 1000)
        },
    });
}

function saveAIML() {
    var dataForm = new FormData();
    console.log(e.getValue())
    dataForm.append("formAIML", JSON.stringify(e.getValue()));
    $.ajax({
        type: "POST",
        url: _Host + 'api/SaveAIML',
        data: dataForm,
        contentType: false,
        processData: false,
        success: function (result) {
            alert('Lưu thành công!')
            console.log(result);

        },
    });
}