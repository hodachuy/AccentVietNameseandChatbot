/************************ Symptoms ********************
    
*/
var pageSize = 10;

var symptomsVm = {
    ID: "",
    Name: "",
    Description: "",
    Cause: "",
    Treament: "",
    Advice:"",
    BotID: "",
    ContentHTML:"",
    Symptoms: "",
    Predict: "",
    Protect: "",
    DoctorCanDo: "",
    ContentHTML: "",
}

$(document).ready(function () {
    // init data
    getDataTable(1, pageSize);
    loadToolEditor();
    $("#btnAdd").off().on('click', function () {
        addSymptoms();
    })
    $("#openSymptomsModel").off().on('click', function () {
        $("#symptomsID").val('');
        $('#txtSymptomsName').val('');
        $("#txtSymptomsDes").data("kendoEditor").value('');
        $("#txtSymptomsCause").data("kendoEditor").value('');
        $("#txtSymptomsTreament").data("kendoEditor").value('');
        $("#txtSymptomsAdvice").data("kendoEditor").value('');
        $("#txtSymptomsContent").data("kendoEditor").value('');
        $("#txtSymptomsPredict").data("kendoEditor").value('');
        $("#txtSymptomsProtect").data("kendoEditor").value('');
        $("#txtSymptomsDoctorCanDo").data("kendoEditor").value('');
        $("#txtSymptomsContentHTML").val('');
        $("#addSymptomsModal").modal({
            backdrop: 'static',
            keyboard: true,
            show: true
        });
    })
    // select number show table
    $("select#results-pp").change(function () {
        pageSize = $(this).children("option:selected").val();
        getDataTable(1, pageSize);
    });
    if (typeof (Storage) !== "undefined") {
        sessionStorage.setItem("_search", "");
    } else {
        console.log("Sorry, your browser does not support Web Storage...");
    }
})

loadToolEditor = function () {
    loadEditor("#txtSymptomsDes", " ");
    loadEditor("#txtSymptomsCause", " ");
    loadEditor("#txtSymptomsTreament", " ");
    loadEditor("#txtSymptomsAdvice", " ");
    loadEditor("#txtSymptomsContent", " ");
    loadEditor("#txtSymptomsPredict", " ");
    loadEditor("#txtSymptomsProtect", " ");
    loadEditor("#txtSymptomsDoctorCanDo", " ");
}

function getDataTable(page, pageSize) {
    var param = {
        page: page,
        pageSize: pageSize
    }
    //param = JSON.stringify(param)
    $.ajax({
        url: _Host + 'api/medical/symptoms/getall',
        contentType: 'application/json; charset=utf-8',
        data: param,
        type: 'GET',
        success: function (result) {
            new renderTemplate(result).Table();
        },
    });
}

function addSymptoms() {
    var txtName = $("#txtSymptomsName").val();
    var txtSymptomsDes = $("#txtSymptomsDes").data("kendoEditor").value();
    var txtSymptomsCause = $("#txtSymptomsCause").data("kendoEditor").value();
    var txtSymptomsTreament = $("#txtSymptomsTreament").data("kendoEditor").value();
    var txtSymptomsAdvice = $("#txtSymptomsAdvice").data("kendoEditor").value();
    var txtSymptomsContent = $("#txtSymptomsContent").data("kendoEditor").value();
    var txtSymptomsPredict = $("#txtSymptomsPredict").data("kendoEditor").value();
    var txtSymptomsProtect = $("#txtSymptomsProtect").data("kendoEditor").value();
    var txtSymptomsDoctorCanDo = $("#txtSymptomsDoctorCanDo").data("kendoEditor").value();
    var txtSymptomsContentHTML = $("#txtSymptomsContentHTML").val();

    if (txtName == "") {
        alert('Vui lòng nhập tên triệu chứng');
        return false;
    };
    if (txtSymptomsDes == "")
    {
        alert('Vui lòng nhập mô tả');
        return false;
    };
    symptomsVm.ID = $("#symptomsID").val() == "" ? 0 : $("#symptomsID").val();
    symptomsVm.Name = txtName;
    symptomsVm.Description = txtSymptomsDes;
    symptomsVm.Cause = txtSymptomsCause;
    symptomsVm.Treament = txtSymptomsTreament;
    symptomsVm.Advice = txtSymptomsAdvice;
    symptomsVm.Symptoms = txtSymptomsContent;
    symptomsVm.Predict = txtSymptomsPredict;
    symptomsVm.Protect = txtSymptomsProtect;
    symptomsVm.DoctorCanDo = txtSymptomsDoctorCanDo;
    symptomsVm.ContentHTML = txtSymptomsContentHTML;

    console.log(symptomsVm)
    var svr = new AjaxCall("api/medical/symptoms/createUpdateSymptoms", JSON.stringify(symptomsVm));
    svr.callServicePOST(function (data) {
        if (data) {
            console.log(data)
            $("#addSymptomsModal").modal('hide');
            $("#model-notify").modal('hide');
            swal({
                title: "Thông báo",
                text: "Thành công",
                confirmButtonColor: "#EF5350",
                type: "success"
            }, function () { $("#model-notify").modal('show'); });

            getDataTable(1, pageSize);
        }
    });
}
function viewQnA(symptomsId) {
    if (symptomsId == null) {
        swal({
            title: "Thông báo",
            text: "Lỗi không tìm thấy dữ liệu",
            confirmButtonColor: "#EF5350",
            type: "error"
        }, function () { $("#model-notify").modal('show'); });
    }
    var params = {
        Id: symptomsId
    }
    var svr = new AjaxCall("api/medical/symptoms/getbyid", params);
    svr.callServiceGET(function (data) {
        $("#symptomsID").val(data.ID);
        $("#txtSymptomsName").val(data.Name);
        $("#txtSymptomsDes").data("kendoEditor").value(data.Description);
        $("#txtSymptomsCause").data("kendoEditor").value(data.Cause);
        $("#txtSymptomsTreament").data("kendoEditor").value(data.Treament);
        $("#txtSymptomsAdvice").data("kendoEditor").value(data.Advice);
        $("#txtSymptomsContent").data("kendoEditor").value(data.Symptoms);
        $("#txtSymptomsPredict").data("kendoEditor").value(data.Predict);
        $("#txtSymptomsProtect").data("kendoEditor").value(data.Protect);
        $("#txtSymptomsDoctorCanDo").data("kendoEditor").value(data.DoctorCanDo);
        $("#txtSymptomsContentHTML").val(data.ContentHTML);
        $("#addSymptomsModal").modal({
            backdrop: 'static',
            keyboard: true,
            show: true
        });
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
                //index = index + 1;
                html += '<tr>';
                html += '<td>' + (iCount++) + '</td>';
                html += '<td onclick="viewQnA(' + item.ID + ')" style="cursor:pointer;color:#0056b3">' + item.Name + '</td>';
                html += '<td>' + (item.Description.length > 300 ? item.Description.substring(0, 300) + '...' : item.Description) + '</td>';
                html += '<td>' + (item.Cause == null ? '' : (item.Cause.length > 300 ? item.Cause.substring(0, 300) + '...' : item.Cause)) + '</td>';
                html += '<td>' + (item.Treament == null ? '' : (item.Treament.length > 300 ? item.Treament.substring(0, 300) + '...' : item.Treament)) + '</td>';
                html += '<td>' + (item.Advice == null ? '' : (item.Advice.length > 300 ? item.Advice.substring(0, 300) + '...' : item.Advice)) + '</td>';
                html += '</tr>';
            })

            // khoản cách mRangePage <--> " so page chọn" <--> mRangePage
            var mRangePage;
            if (data.MaxPage == 5) {
                mRangePage = data.MaxPage / 2;
            }else{
                mRangePage = data.MaxPage / (data.MaxPage / 2);
            }
            // render pagination : phân trang table
            var startPageIndex = Math.max(1, (data.Page - mRangePage));
            var endPageIndex = Math.min(data.TotalPages, (data.Page + mRangePage));
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
