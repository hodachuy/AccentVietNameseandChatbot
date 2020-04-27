//-----------------------------Question & Answer ----------------------------//
/*
    log
*/
var configs = { idgrid: "#grid" },
    formData,
    api = {
        getListAgentChannel: "api/lc_agent/getListAgentOfChannel",
    },
    alerts = {
        txtEmpty: "[Trống]",
        txtRequired: "* Trường này bắt buộc",
        txtAddUpdateOK: "Cập nhật dữ liệu thành công",
        txtDeleteOK: "Xóa liệu thành công",
        txtDeleteConfirm: "Bạn có chắc muốn xóa dữ liệu này?"
    },
    quesModel = {
        QuesID: '',
        ContentHTML: '',
        AreaID: '',
        UserID: $("#userId").val(),
        UserName: $("#userName").val(),
        IsDelete: false
    },
    ansModel = {
        AnswerID: '',
        ContentHTML: '',
    }
$(function () {
    agentTable.init();
    qnaEvent.init();
    new commonService().loadEditorTinyMce('.editorQnA', true);
});
var qnaEvent = {
    init: function () {
        this.events();
        //this.getPermission()
    },
    events: function () {
        $('body').on('click', '#form-qna-open', function () {
            qnaEvent.refresh();
            $("#modal-form-qna").modal({
                backdrop: 'static',
                keyboard: true,
                show: true
            });
        })
        $('body').on('click', '#form-qna-save', function () {
            if (qnaEvent.validate()) {
                qnaEvent.createUpdate();
            }
        })
        $('body').on('click', '#form-qna-close', function () {
            $("#modal-form-qna").modal('hide');
        })
    },
    refresh: function () {
        quesModel.QuesID = 0;
        ansModel.AnswerID = 0;
        $("#txtEmail").html(alerts.txtEmpty);
        $("#txtAddress").html(alerts.txtEmpty);
        $("#txtPhoneNumber").html(alerts.txtEmpty);
        $("#txtCreatedDate").html(alerts.txtEmpty);
        tinymce.get("txtQuesContent").getBody().innerHTML = "";
        tinymce.get("txtAnsContent").getBody().innerHTML = "";
        LoadComboBoxWithServices("#cboArea", qnaSrc.getListArea, null, "AreaID", "Title", null, "Lĩnh vực", false, null,
                                 function () { }, null);
        $('#form').validationEngine('hide');
    },
    validate: function () {
        var res = $("#form").validationEngine('validate');;
        setTimeout(function () {
            $('#form').validationEngine('hide');
        }, 10000);
        var quesContentHTML = tinymce.get("txtQuesContent").getContent();
        if (quesContentHTML.trim() == "") {
            $('#txtQuesContent').validationEngine('showPrompt', alerts.txtRequired, 'red', 'topRight', true);
            res = false;
        }
        else {
            $("#txtQuesContent").validationEngine('hide');
        }
        return res;
    },
    setModel: function () {
        quesModel.AreaID = $("#cboArea").val();
        quesModel.ContentHTML = tinymce.get("txtQuesContent").getContent();
        ansModel.ContentHTML = tinymce.get("txtAnsContent").getContent();

        formData = new FormData();
        formData.append('question', JSON.stringify(quesModel));
        formData.append('answer', JSON.stringify(ansModel));
    },
    createUpdate: function () {
        qnaEvent.setModel();
        var srv = new AjaxCall(qnaSrc.createUpdate, formData);
        srv.callServicePostForm(function (result) {
            if (result.status) {
                $("#modal-form-qna").modal('hide');
                $('#grid').data('kendoGrid').dataSource.read();
                $('#grid').data('kendoGrid').refresh();
            }
        })
    }
}
var agentTable = {
    init: function () {
        //agentTable.loadAgent();
    },
    loadAgent: function(){
        var param = {
            groupChannelId: ''
        }
        param = JSON.stringify(param)
        $.ajax({
            url: _Host + api.getListAgentChannel,
            contentType: 'application/json; charset=utf-8',
            data: param,
            type: 'POST',
            success: function (result) {
                if (result.length != 0) {
                    new agentTable().renderTempAgent(result);
                }

            },
        });
    },
    renderTempAgent: function (data) {
        var html = '';
        html +='<tr>';
        html += ' <td>';
        html +='      <div size="2" data-test="user-avatar" class="css-1ocrak0 css-7e05130"><div class="css-5r5m5i css-7e05132"></div><span class="css-10zmg9r css-7e05131"></span></div>';
        html +='  </td>';
        html +='  <td>hdhuy@gmail.com</td>';
        html +='  <td>';
        html +='      <span class="css-y4ek3x" title="Desktop"><div class="css-nmb5ix css-j314r80" width="20px" height="20px"><svg width="20px" height="20px" viewBox="0 0 20 18"><g fill="none" fill-rule="evenodd" opacity=".7"><path d="M0-1h20v20H0z"></path><path fill="#000" fill-rule="nonzero" d="M17.5.667h-15c-.917 0-1.667.75-1.667 1.666v10C.833 13.25 1.583 14 2.5 14h5.833v1.667H6.667v1.666h6.666v-1.666h-1.666V14H17.5c.917 0 1.667-.75 1.667-1.667v-10c0-.916-.75-1.666-1.667-1.666zm0 11.666h-15v-10h15v10z"></path></g></svg></div></span>';
        html +='  </td>';
        html +='  <td>';
        html +='      <span class="badge bg-success-bright text-success">Owner</span>';
        html +='  </td>';
        html +='  <td></td>';
        html +='</tr>';
    },
    renderTempChatbot: function (data) {
        var html = '';
        html += '';
    },
    renderTempGroup: function (data) {

    }
};
