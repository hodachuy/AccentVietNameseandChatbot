//############ Table and Search Engine #############//
var pageSize = 10;
//var allowed = true;
var debounceTimeout = null;

var moduleVm = {
    ID: "",
    Title:"",
    Text:"",
    Key:"",
    Value:"",
    Payload: "",
    BotID: $("#botId").val()
}
var arrKey = [];
$(document).ready(function () {
    // init data
    getModuleByBotId();
    setTimeout(function () {
       getDataTable(1, pageSize);
    },1000)
})

function getModuleByBotId() {
    var param = {
        botId: $("#botId").val(),
    }
    //param = JSON.stringify(param)
    $.ajax({
        url: _Host + 'api/module/getbybotid',
        contentType: 'application/json; charset=utf-8',
        data: param,
        type: 'GET',
        success: function (result) {
            arrKey = []
            console.log(result)
            if (result.length != 0) {
                $.each(result, function (index,value) {
                    arrKey.push(value.Payload.replace("postback_module_",""));
                })
            }
            console.log(arrKey)
        },
    });
}

function getDataTable(page, pageSize) {
    var param = {
        page: page,
        pageSize: pageSize
    }
    //param = JSON.stringify(param)
    $.ajax({
        url: _Host + 'api/modulecategory/getall',
        contentType: 'application/json; charset=utf-8',
        data: param,
        type: 'GET',
        success: function (result) {
            console.log(result)
            new renderTemplate(result).Table();
        },
    });
}
renderTemplate = function (data) {
    this.Table = function () {

        var dataTable = data.Items;
        var html = '';

        var paginationListHtml = '';
        paginationListHtml += '<li class="pagination__group"><a href="javascript:void(0);" class="pagination__item pagination__control pagination__control_prev">prev</a></li>';
        paginationListHtml += '<li class="pagination__group"><a href="#0" class="pagination__item">1</a></li>';
        paginationListHtml += '<li class="pagination__group"><a href="#0" class="pagination__item pagination__control pagination__control_next">next</a></li>';

        var paginationTextHtml = '';
        var fromPP = 0;
        var toPP = 0;
        var totalPP = 0;

        if (dataTable.length != 0) {
            // đếm số index
            var iCount = 1;
            if (data.Page > 1) {
                iCount = ((data.Page - 1) * pageSize) + 1;
            }

            $.each(dataTable, function (index, item) {
                var arraycontainsturtles;
                if (arrKey.length != 0) {
                    var arraycontainsturtles = (arrKey.indexOf(item.Alias) > -1);
                    console.log(arraycontainsturtles)
                }

                //index = index + 1;
                html += '<tr>';
                html += '<td>' + (iCount++) + '</td>';
                html += '<td>' + item.Name + '</td>';
                html += '<td><input type="checkbox" class="chkAddModule" ' + (arraycontainsturtles == true ? "disabled checked":'') + ' value="' + item.Alias + '"/></td>';
                html += '</tr>';
            })

            // render pagination : phân trang table
            var startPageIndex = Math.max(1, (data.Page - data.MaxPage / 2));
            var endPageIndex = Math.min(data.TotalPages, (data.Page + data.MaxPage / 2));
            var firstPage = 1;
            var lastPage = data.TotalPages;
            var previousPage = data.Page - 1;
            var nextPage = data.Page + 1;
            paginationListHtml = '';

            if (data.Page > firstPage) {
                paginationListHtml += '<li class="pagination__group"><a href="javascript:void(0);" onclick="getDataTable(' + (data.Page - 1) + ',' + pageSize + ')" class="pagination__item pagination__control pagination__control_prev">prev</a></li>';
            }
            for (var i = startPageIndex; i <= endPageIndex; i++) {
                if (data.Page == i) {
                    paginationListHtml += '<li class="pagination__group"><span class="pagination__item pagination__item_active">' + i + '</span></li>';
                }
                else {
                    paginationListHtml += '<li class="pagination__group"><a href="javascript:void(0);" onclick="getDataTable(' + i + ',' + pageSize + ')" class="pagination__item">' + i + '</a></li>';
                }
            }
            if (data.Page < lastPage) {
                paginationListHtml += '<li class="pagination__group"><a href="javascript:void(0);" onclick="getDataTable(' + (data.Page + 1) + ',' + pageSize + ')" class="pagination__item pagination__control pagination__control_next">next</a></li>';
            }

            // thông tin số trang
            fromPP = ((data.Page - 1) * pageSize) + 1;
            toPP = ((data.Page - 1) * pageSize) + dataTable.length;
            totalPP = data.TotalCount;
        }

        $("#table-data-training").empty().append(html);
        $("#pagination-list").empty().append(paginationListHtml);

        paginationTextHtml = 'từ <span style="font-weight: bold">' + fromPP + '</span> đến <span style="font-weight: bold">' + toPP + ' </span>trong <span style="font-weight: bold">' + totalPP + ' </span>mục';
        $("#pagination-text-number-show").empty().append(paginationTextHtml);

        // scroll top table
        document.getElementById("table-qna").scrollTop = 0;
    }  
}
$('body').on('click', 'input.chkAddModule', function () {
    if ($(this).prop('checked') == false) {
        return;
    } else {
        var moduleName = $(this).prop('value');
        $(this).attr("disabled", true);
        if (moduleName == "phone") {
            moduleVm.Title = "Xử lý số điện thoại";
            moduleVm.Text = "Điện thoại";
            moduleVm.Name = "phone";
            moduleVm.Payload = "postback_module_phone";
            moduleVm.Type = "handle"
        }
        if (moduleName == "engineer_name") {
            moduleVm.Title = "Xử lý tên kỹ sư";
            moduleVm.Text = "Tên";
            moduleVm.Name = "engineer_name";
            moduleVm.Payload = "postback_module_engineer_name";
            moduleVm.Type = "handle"
        }
        if (moduleName == "email") {
            moduleVm.Title = "Xử lý email";
            moduleVm.Text = "Email";
            moduleVm.Name = "email";
            moduleVm.Payload = "postback_module_email";
            moduleVm.Type = "handle"
        }
        if (moduleName == "age") {
            moduleVm.Title = "Xử lý tuổi";
            moduleVm.Name = "age";
            moduleVm.Text = "Tuổi";
            moduleVm.Payload = "postback_module_age";
            moduleVm.Type = "handle"
        }
        if (moduleName == "voucher") {
            moduleVm.Title = "Xử lý voucher";
            moduleVm.Name = "voucher";
            moduleVm.Text = "Voucher";
            moduleVm.Payload = "postback_module_voucher";
            moduleVm.Type = "handle"
        }
        // tri thức hỏi đáp pháp luật
        if (moduleName == "qna_legal") {
            moduleVm.Title = "Tri thức hỏi đáp pháp luật";
            moduleVm.Text = "";
            moduleVm.Name = "qna_legal";
            moduleVm.Payload = "postback_module_qna_legal";
            moduleVm.Type = "knowledge"
        }
        // tri thức chuẩn đoán bệnh
        if (moduleName == "med_diagnostic") {
            moduleVm.Title = "Tri thức chuẩn đoán bệnh";
            moduleVm.Text = "";
            moduleVm.Name = "med_diagnostic";
            moduleVm.Payload = "postback_module_med_diagnostic";
            moduleVm.Type = "knowledge"
        }

        // tri thức lấy thông tin bệnh nhân
        if (moduleName == "med_get_info_patient") {
            moduleVm.Title = "Tri thức lấy thông tin bệnh nhân";
            moduleVm.Text = "";
            moduleVm.Name = "med_get_info_patient";
            moduleVm.Payload = "postback_module_med_get_info_patient";
            moduleVm.Type = "knowledge"
        }

        // tri thức tìm kiếm với api
        if (moduleName == "api_search") {
            moduleVm.Title = "Tri thức tìm kiếm";
            moduleVm.Text = "";
            moduleVm.Name = "api_search";
            moduleVm.Payload = "postback_module_api_search";
            moduleVm.Type = "knowledge"
        }
        console.log(moduleVm)

        var svr = new AjaxCall("api/module/create", JSON.stringify(moduleVm));
        svr.callServicePOST(function (data) {
            console.log(data)
        });     
    }
})

