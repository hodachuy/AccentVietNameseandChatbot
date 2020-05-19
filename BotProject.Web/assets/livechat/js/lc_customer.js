//-----------------------------Customer ----------------------------//
/*
    log
*/
var configs = { idgrid: "#grid" }, formData,
    apiCustomer = {
        getAll: "api/lc_customer/getAll",
    },
    alerts = {
        txtEmpty: "[Trống]",
        txtRequired: "* Trường này bắt buộc",
        txtAddUpdateOK: "Cập nhật dữ liệu thành công",
        txtDeleteOK: "Xóa liệu thành công",
        txtDeleteConfirm: "Bạn có chắc muốn xóa dữ liệu này?"
    }

$(function () {
    customerTable.init();
});

var customerTable = {
    init: function () {
        customerTable.loadGrid();
    },
    templateGrid: function (data) {
        if (data.length != 0) {
            var html = '';
            $.each(data, function (index, value) {
                html += `<tr>
                                                <td>$(value.Name)</td>
                                                <td>hdhuy1991@gmail.com</td>
                                                <td>0375348328</td>
                                                <td>
                                                    <span class="badge bg-secondary-bright text-secondary">Go to chat</span>
                                                </td>
                                                <td>
                                                    <span class="badge bg-success-bright text-success">Đã tiếp nhận</span>
                                                </td>
                                                <td>
                                                    HuyHo
                                                </td>
                                                <td>
                                                    192.168.0.1
                                                </td>
                                                <td>
                                                    <a href="#" class="d-flex align-items-center">
                                                        <img width="40" src="~/assets/client/img/vietnam-icon.png"
                                                             class="rounded mr-3" alt="cherry">
                                                        <span>Việt Nam</span>
                                                    </a>
                                                </td>
                                            </tr>  `

            })
        }
    },
    templateAction: function (e) {
        var html = '';
        var permission = {
            IsView: true,
            IsAdd: true,
            IsUpdate: true,
            IsDelete: true,
            IsApprove: true,
        };
        if (permission.IsView || permission.IsAdd || permission.IsUpdate || permission.IsDelete || permission.IsApprove) {
            html += '<div class="btn-group">';
            html += '<i class="ace-icon fa fa-cog icon-only bigger-120 dropdown-toggle" data-toggle="dropdown"></i>';
            html += '<ul class="dropdown-menu dropdown-white dropdown-menu-right">';
            html += '<li>';
            html += '<a href="javascript:new customerTable.eventAction(' + e.QuesID + ').Edit(\'' + e.AreaID + '\')">Xem/Chỉnh sửa</a>';
            html += '</li>';
            html += '<li>';
            html += '<a href="javascript:new customerTable.eventAction(' + e.QuesID + ').Delete()">Xóa</a>';
            html += '</li>';
            html += '</ul></div>';
        }
        return html;
    },
    eventAction: function (quesId) {
        this.Edit = function (areaId) {
            qnaEvent.refresh();
            var params = {
                quesId: quesId
            }
            var srv = new AjaxCall(qnaSrc.getById, params);
            srv.callServiceGET(function (data) {
                if (data != undefined) {
                    $("#modal-form-qna").modal({
                        backdrop: 'static',
                        keyboard: true,
                        show: true
                    });
                    if (areaId != null) {
                        $("#cboArea").data("kendoComboBox").value(areaId);
                    }
                    tinymce.get("txtQuesContent").getBody().innerHTML = data.QuesContentHTML;
                    tinymce.get("txtAnsContent").getBody().innerHTML = data.AnsContentHTML;
                    quesModel.QuesID = data.QuesID;
                    ansModel.AnswerID = data.AnswerID || 0;
                    //độc giả
                    $("#txtEmail").html(data.Email == null ? alerts.txtEmpty : data.Email);
                    $("#txtAddress").html(data.Address == null ? alerts.txtEmpty : data.Address)
                    $("#txtPhoneNumber").html(data.PhoneNumber == null ? alerts.txtEmpty : data.PhoneNumber)
                    $("#txtCreatedDate").html(data.QuesCreatedDate == null ? alerts.txtEmpty : kendo.toString(new Date(data.QuesCreatedDate), "dd/MM/yyyy"))
                }
            });
        }
        this.Delete = function () {
            //
        }
    },
    loadGrid: function () {
        var channelGroupId = $("#channelGroupId").val();
        var params = {
            channelGroupID: channelGroupId
        }
        //params = JSON.stringify(params);
        $.ajax({
            url: apiCustomer.getAll,
            contentType: 'application/json; charset=utf-8',
            data: param,
            type: 'GET',
            success: function (result) {
                console.log(result)
                if (result.length != 0) {
                    //new customerTable.renderTempChatbot(result);
                }
            },
        })
    }
};
