var _idgrid = "#grid";
var pageSize = 10;
var pageSizeVoucher = 10;
var startDate;
var endDate;
$(document).ready(function () {
    getDataTableHistory(1, pageSize);
    // select number show table
    $("select#results-pp").change(function () {
        pageSize = $(this).children("option:selected").val();
        getDataTableHistory(1, pageSize);
    });

    //getDataTableVoucher(1, pageSizeVoucher);
    // select number show table
    //$("select#results-pp-voucher").change(function () {
    //    pageSizeVoucher = $(this).children("option:selected").val();
    //    if (pageSizeVoucher == "All"){
    //        pageSizeVoucher = 1000;
    //    }
    //    getDataTableVoucher(1, pageSizeVoucher);
    //});
    LoadGrid();
    LoadDatePicker('txtTungayVoucher');
    LoadDatePicker('txtDenngayVoucher');
})


//date-format:2019-09-05 11:16:20.227
function DateVN(date) {
    if (date == "" || date == null || date == undefined)
        return "";
    date = date.substring(0, date.length - 4);
    let y = date.substring(0, 4),
        m = date.substring(5, 7),
        d = date.substring(8, 10),
        time = date.substring(11, date.length);
    let dateVN = time + " " + d + "-" + m + "-" + y;
    return dateVN;
}

function DateVoucher(date) {
    if (date == "" || date == null || date == undefined)
        return "";
    date = date.substring(0, date.length - 4);
    let y = date.substring(0, 4),
        m = date.substring(5, 7),
        d = date.substring(8, 10),
        time = date.substring(11, date.length);
    let dateVN = d + "-" + m + "-" + y + " " + time;
    return dateVN;
}

function getDataTableHistory(page, pageSize) {
    var param = {
        page: page,
        pageSize: pageSize,
        botId: $("#botId").val()
    }
    //param = JSON.stringify(param)
    $.ajax({
        url: _Host + 'api/history/getbybot',
        contentType: 'application/json; charset=utf-8',
        data: param,
        type: 'GET',
        success: function (result) {
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
                //index = index + 1;
                html += '<tr>';
                html += '<td>' + DateVN(item.CreatedDate) + '</td>';
                html += '<td>' + item.UserName + '</td>';
                html += '<td>' + (item.UserSay.length > 100 ? item.UserSay.substring(0, 100) + '...' : item.UserSay) + '</td>';
                if (item.BotUnderStands == null || item.BotUnderStands == 'unknown') {
                    html += '<td></td>';
                } else {
                    html += '<td>' + item.BotUnderStands + '</td>';
                }
                if (item.BotHandle != null && item.BotHandle == "Bot không hiểu") {
                    html += '<td class="colorbot_understands">' + item.BotHandle + '</td>';
                } else {
                    html += '<td>' + (item.BotHandle == null ? '' : item.BotHandle) + '</td>';
                }
                html += '<td>' + (item.Type == null ? "" : item.Type) + '</td>';
                html += '</tr>';
            })

            // khoản cách mRangePage <--> " so page chọn" <--> mRangePage
            var mRangePage;
            if (data.MaxPage == 5) {
                mRangePage = data.MaxPage / 2;
            } else {
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
                paginationListHtml += '<li class="pagination__group"><a href="javascript:void(0);" onclick="getDataTableHistory(' + (data.Page - 1) + ',' + pageSize + ')" class="pagination__item pagination__control pagination__control_prev">prev</a></li>';
            }
            for (var i = startPageIndex; i <= endPageIndex; i++) {
                if (data.Page == i) {
                    paginationListHtml += '<li class="pagination__group"><span class="pagination__item pagination__item_active">' + i + '</span></li>';
                } else {
                    paginationListHtml += '<li class="pagination__group"><a href="javascript:void(0);" onclick="getDataTableHistory(' + i + ',' + pageSize + ')" class="pagination__item">' + i + '</a></li>';
                }
            }
            if (data.Page < lastPage) {
                paginationListHtml += '<li class="pagination__group"><a href="javascript:void(0);" onclick="getDataTableHistory(' + (data.Page + 1) + ',' + pageSize + ')" class="pagination__item pagination__control pagination__control_next">next</a></li>';
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

//-----GetDataVoucher ------



function getDataTableVoucher(page, pageSizeVoucher) {
    var param = {
        page: page,
        pageSize: pageSizeVoucher,
        botId: $("#botId").val()
    }
    //param = JSON.stringify(param)
    $.ajax({
        url: _Host + 'api/voucher/gettelephone',
        contentType: 'application/json; charset=utf-8',
        data: param,
        type: 'GET',
        success: function (result) {
            new renderTemplateVoucher(result).Table();
        },
    });
}
renderTemplateVoucher = function (data) {
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
                iCount = ((data.Page - 1) * pageSizeVoucher) + 1;
            }

            $.each(dataTable, function (index, item) {
                index = index + 1;
                html += '<tr>';
                html += '<td>' + index + '</td>';
                html += '<td>' + (item.NumberOrder == null ? "" : item.NumberOrder) + '</td>';
                if (item.IsReceived == true) {
                    html += '<td> Đã nhận </td>';
                } else {
                    html += '<td class="colorbot_understands"> Chưa nhập OTP </td>';
                }

                html += '<td>' + item.TelephoneNumber + '</td>';
                html += '<td>' + (item.CodeOTP == null ? "" : item.CodeOTP) + '</td>';
                html += '<td>' + (item.SerialNumber == null ? "" : item.SerialNumber) + '</td>';
                html += '<td>' + item.Title + '</td>';
                //html += '<td><img src="' + _Host + item.Image +'" alt="" width="100" height="50"></td>';
                html += '<td>' + DateVoucher(item.CreatedDate) + '</td>';
                html += '<td>' + (item.Type == null ? "" : item.Type) + '</td>';
                html += '</tr>';
            })

            // khoản cách mRangePage <--> " so page chọn" <--> mRangePage
            var mRangePage;
            if (data.MaxPage == 5) {
                mRangePage = data.MaxPage / 2;
            } else {
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
                paginationListHtml += '<li class="pagination__group"><a href="javascript:void(0);" onclick="getDataTableVoucher(' + (data.Page - 1) + ',' + pageSizeVoucher + ')" class="pagination__item pagination__control pagination__control_prev">prev</a></li>';
            }
            for (var i = startPageIndex; i <= endPageIndex; i++) {
                if (data.Page == i) {
                    paginationListHtml += '<li class="pagination__group"><span class="pagination__item pagination__item_active">' + i + '</span></li>';
                } else {
                    paginationListHtml += '<li class="pagination__group"><a href="javascript:void(0);" onclick="getDataTableVoucher(' + i + ',' + pageSizeVoucher + ')" class="pagination__item">' + i + '</a></li>';
                }
            }
            if (data.Page < lastPage) {
                paginationListHtml += '<li class="pagination__group"><a href="javascript:void(0);" onclick="getDataTableVoucher(' + (data.Page + 1) + ',' + pageSizeVoucher + ')" class="pagination__item pagination__control pagination__control_next">next</a></li>';
            }

            // thông tin số trang
            fromPP = ((data.Page - 1) * pageSizeVoucher) + 1;
            toPP = ((data.Page - 1) * pageSizeVoucher) + dataTable.length;
            totalPP = data.TotalCount;
        }

        $("#table-data-item-voucher").empty().append(html);
        $("#pagination-list-voucher").empty().append(paginationListHtml);

        paginationTextHtml = 'từ <span style="font-weight: bold">' + fromPP + '</span> đến <span style="font-weight: bold">' + toPP + ' </span>trong <span style="font-weight: bold">' + totalPP + ' </span>mục';
        $("#pagination-text-number-show-voucher").empty().append(paginationTextHtml);

        // scroll top table
        document.getElementById("table-voucher").scrollTop = 0;
    }
}

DataSource = function () {
    this.MasterDatasource = function (url) {
        var total = 0;
        var data = new kendo.data.DataSource({
            transport: {
                read: function (options) {
                    console.log(options)
                    // Custome set lại object filter 2 ngày từ - ngày đến khi được chọn sau.
                    startDate = "";
                    endDate = "";
                    if (options.data.filter != null) {
                        for (var i = 0; i < options.data.filter.filters.length; i++) {
                            if (options.data.filter.filters[i].field == "CreatedDate") {
                                if (options.data.filter.filters[i].operator == "gte") {
                                    if (options.data.filter.filters[i].value != undefined) {
                                        startDate = new Date(options.data.filter.filters[i].value);
                                        startDate = kendo.toString(startDate, "dd-MM-yyyy");
                                        console.log(startDate);
                                    }
                                }
                                if (options.data.filter.filters[i].operator == "lte")
                                {
                                    if (options.data.filter.filters[i].value != undefined) {
                                        endDate = new Date(options.data.filter.filters[i].value);
                                        endDate = kendo.toString(endDate, "dd-MM-yyyy")
                                        console.log(endDate)
                                    }
                                }
                            }
                            ////console.log(options.data.filter.filters[i]);
                            if (options.data.filter.filters[i].logic == 'and') {
                                for (var m = 0; m < options.data.filter.filters[i].filters.length; m++) {
                                    options.data.filter.filters.push(options.data.filter.filters[i].filters[m]);
                                }
                                options.data.filter.filters.splice(i, 1);
                            }
                        }
                    }

                    var form = new FormData();
                    form.append('requestFilter', JSON.stringify(options.data))
                    form.append('botId', JSON.stringify($("#botId").val()))
                    $.ajax({
                        url: url,
                        type: "POST",
                        contentType: false,
                        processData: false,
                        cache: false,
                        data: form,
                        beforeSend: function () {
                            //console.log(1);
                            $(block).block({
                                message: '<i class="icon-spinner4 fa fa-spinner fa-pulse spinner"></i>',
                                overlayCSS: {
                                    backgroundColor: '#efeff6',
                                    opacity: 0.8,
                                    cursor: 'wait',
                                    zIndex: 99999999999999
                                },
                                css: {
                                    border: 0,
                                    padding: 0,
                                    backgroundColor: 'transparent',
                                }
                            });
                        },
                        complete: function (e) {
                            $(block).unblock();
                        },
                        success: function (result) {
                            //console.log(result)
                            if (result != null) {
                                if (result.Items != null) {
                                    options.success(result);
                                }
                            } else {
                                options.success([]);
                            }
                        },
                        error: function (result) {
                            options.success([]);
                        }
                    })
                },
            },
            schema: {
                data: function (data) {
                    //console.log(data)
                    if (data != null) {
                        if (data.Items != null)
                            return data.Items;
                    }
                    return [];
                },
                total: function (data) {
                    //console.log(data)
                    if (data != null) {
                        if (data.Items != null)
                            return data.TotalCount;
                    }
                    return 0;
                },
                model: {
                    fields: {
                        CreatedDate: {
                            type: "date"
                        },
                    }
                }
            },
            pageSize: 20,
            serverPaging: true,
            serverFiltering: true,
            serverSorting: true,
        });
        return data;
    }
}
var Columns = [{
    title: "Stt",
    template: '#= record++ #',
    filterable: false,
    width: 50,
    attributes: {
        style: "text-align: center"
    },
    headerAttributes: {
        style: "text-align: center"
    },
},
    //{
    //    template: templateForAction,
    //    filterable: false,
    //    width: 90,
    //    title: "Tiện ích",
    //    attributes: {
    //        style: "text-align: center; overflow : visible; cursor: pointer",
    //    },
    //    headerAttributes: {
    //        style: "text-align: center"
    //    },
    //},
    {
        template: '#=data.NumberOrder#',
        field: "u.NumberOrder",
        title: "Mã Phiếu",
        filterable: false,
        width: 100,
    },
    {
        template: '#if(data.IsReceived == true){#<span style="color:blue">Đã nhận</span>#} else if(data.IsReceived == false){#<span style="color:red">Chưa nhập OTP</span>#}#',
        field: "u.IsReceive",
        title: "Trạng thái",
        filterable: {
            ui: StatusFilter
        },
        width: 120,
    },
    {
        template: '#=data.TelephoneNumber#',
        field: "u.TelephoneNumber",
        title: "Số điện thoại",
        width: 120,
    },
    {
        template: '#=data.CodeOTP#',
        field: "u.Code",
        title: "Mã OTP",
        filterable: false,
        width: 120,
    },
    {
        template: '#=data.SerialNumber#',
        field: "u.SerialNumber",
        title: "Kỹ Thuật Viên",
        width: 150,
    },
    {
        template: '#=data.Title#',
        field: "u.Title",
        title: "Voucher",
        filterable: false,
        width: 150,
    },
    {
        template: '#if(data.CreatedDate != null){#<div>#=kendo.toString(new Date(data.CreatedDate), "dd/MM/yyyy hh:mm:ss")#</div>#}#',
        field: "CreatedDate",
        title: "Ngày nhận",
        width: 150,
        parseFormat: "{0: dd/MM/yyyy}",
        filterable: {
            ui: function (element) {
                element.kendoDatePicker({
                    format: "{0: dd/MM/yyyy}",
                });
            },
            extra: true,
            messages: {
                "info": "Ngày nhận"
            }
        },
    },
    {
        template: '#=data.Type#',
        field: "u.Type",
        title: "Ứng dụng",
        filterable: {
            ui: StatusType
        },
        width: 150,
    }

];
LoadGrid = function () {
    InitVoucherKendoGrid(_idgrid, Columns, new DataSource().MasterDatasource("" + _Host + "api/voucher/gettelephone"), null, false, '')
}

function StatusFilter(element) {
    element.kendoDropDownList({
        dataSource: [{
            IsReceive: 1,
            Text: "Đã nhận"
        }, {
            IsReceive: 0,
            Text: "Chưa nhập OTP"
        }],
        dataTextField: "Text",
        dataValueField: "IsReceive",
        optionLabel: "--Chọn tình trạng--"
    });
}

function StatusType(element) {
    element.kendoDropDownList({
        dataSource: [{
            Type: 1,
            Text: "facebook"
        }, {
            Type: 2,
            Text: "zalo"
        }],
        dataTextField: "Text",
        dataValueField: "Type",
        optionLabel: "--Chọn ứng dụng--"
    });
}

function InitVoucherKendoGrid(id, Column, DataSource, DetailInit, selectable, isReload) {
    ////console.log(Column);
    if (isReload)
        $(id).empty();
    $(id).kendoGrid({
        height: 700,
        //sortable: true,
        selectable: selectable,
        scrollable: true,
        resizable: true,
        lockedTable: false,
        reorderable: true,
        //reoderColumn:true,
        columnMenu: false,
        noRecords: {
            template: "Không có dữ liệu"
        },
        columns: (Column != undefined ? Column : myColumn),
        dataSource: (DataSource != undefined ? DataSource : myDataSource),
        detailInit: (DetailInit != undefined ? DetailInit : myDetailInit),
        pageable: {
            refresh: true,
            buttonCount: 5,
            pageSizes: [5, 10, 20, 50, 100, 'all'],
            messages: {
                display: 'từ <span style="font-weight: bold">{0}</span> đến <span style="font-weight: bold">{1}</span> trong <span style="font-weight: bold">{2}</span> mục',
                itemsPerPage: "số lượng hiển thị",
                first: "Trang đầu tiên",
                previous: "Trang trước",
                next: "Trang tiếp theo",
                last: "Trang cuối cùng",
                refresh: "Tải lại dữ liệu",
                empty: "Không có dữ liệu",
                allPages: "All"
            }
        },
        filterable: {
            extra: false,
            operators: {
                string: {
                    startswith: "Bắt đầu với",
                    contains: "Có chứa",
                    eq: "Bằng với"
                }
            },
            messages: {
                info: "Tìm giá trị",
                filter: "Tìm",
                clear: "Hủy",
            },
            //cell: {
            //    showOperators: false
            //},
        },
        filterMenuInit: function (e) {
            var fieldModel = {};
            var arrField = [];
            var arrType = [];

            try {
                fieldModel = e.sender.dataSource.reader.model.fields;
            }
            catch (e) { }

            $.each(fieldModel, function (index, value) {
                arrField.push(index);
                arrType.push(value.type);
            });

            if (arrField.indexOf(e.field) != -1 && arrType[arrField.indexOf(e.field)] == "date") {

                var firstValueDropDown = e.container.find("select:eq(0)").data("kendoDropDownList");
                firstValueDropDown.dataSource.data([{ value: "gte", text: "Lớn hơn hoặc bằng" }]);
                firstValueDropDown.select(0);
                firstValueDropDown.trigger("change");
                firstValueDropDown.enable(false);
                firstValueDropDown.wrapper.hide();

                var logicDropDown = e.container.find("select:eq(1)").data("kendoDropDownList");
                logicDropDown.value("and");
                logicDropDown.trigger("change");
                logicDropDown.enable(false);
                logicDropDown.wrapper.hide();

                var secondValueDropDown = e.container.find("select:eq(2)").data("kendoDropDownList");
                secondValueDropDown.dataSource.data([{ value: "lte", text: "Nhỏ hơn hoặc bằng" }]);
                secondValueDropDown.select(0);
                secondValueDropDown.trigger("change");
                secondValueDropDown.enable(false);
                secondValueDropDown.wrapper.hide();

                var firstValueDatePicker = e.container.find("input:eq(0)").data("kendoDatePicker");
                firstValueDatePicker.wrapper.context.placeholder = "Từ ngày";
                firstValueDatePicker.setOptions({
                    change: function () {
                        firstValueDropDown.select(0);
                        firstValueDropDown.trigger("change");
                    }
                });

                var secondValueDatePicker = e.container.find("input:eq(1)").data("kendoDatePicker");
                secondValueDatePicker.wrapper.context.placeholder = "Đến ngày";
                secondValueDatePicker.setOptions({
                    change: function () {
                        secondValueDropDown.select(0);
                        secondValueDropDown.trigger("change");
                    }
                });
            }
        },
        dataBinding: function (e) {
            record = (this.dataSource.page() - 1) * this.dataSource.pageSize() + 1;
            var obj = e.sender.dataSource;
            _Stt = obj._pageSize * (obj._page - 1);
            if (_Stt < 0 || !_Stt) _Stt = 0;
            $(id + ' input[name="checkAll"]').change(function () {
                var val = $(this).is(":checked");
                if (val)
                    $(id + ' input[name="checkThis"]').prop("checked", true);
                else
                    $(id + ' input[name="checkThis"]').prop("checked", false);
            });
        },
        dataBound: function () {
            //this.expandRow(this.tbody.find("tr.k-master-row").first());  

            var obj = $(id + " .k-header.k-hierarchy-cell");
            obj.css("vertical-align", " middle");
            obj.html('<a class="k-icon k-i-expand" href="#" tabindex="-1" title="Mở/Đóng xem chi tiết tất cả nội dung"></a>');

            obj.off("click").on('click', '.k-icon', function () {
                if ($(this).attr("class").indexOf("k-i-expand") != -1) {
                    $(this).removeClass("k-i-expand").addClass("k-i-collapse");
                    $(id).data("kendoGrid").expandRow(".k-master-row");
                }
                else {
                    $(this).removeClass("k-i-collapse").addClass("k-i-expand");
                    $(id).data("kendoGrid").collapseRow(".k-master-row");
                }
            });
        },
        detailExpand: function (e) {
            if ($(id + " .k-grid-content .k-hierarchy-cell .k-i-expand").length == 0) {
                $(id + " .k-header.k-hierarchy-cell .k-icon").removeClass("k-i-expand").addClass("k-i-collapse");
            }
        },
        detailCollapse: function (e) {
            if ($(id + " .k-grid-content .k-hierarchy-cell .k-i-collapse").length == 0) {
                $(id + " .k-header.k-hierarchy-cell .k-icon").removeClass("k-i-collapse").addClass("k-i-expand");
            }
        }
    });
}


exportExcel = function () {
    var startDateEx = (startDate == undefined ? kendo.toString(new Date(), "dd-MM-yyyy"): startDate);
    var endDateEx = (endDate == undefined ? kendo.toString(new Date(), "dd-MM-yyyy") : endDate);;

    var grid = $("#grid").data("kendoGrid");
    var dt = grid.dataSource.view();
    var Data = [];
    for (var i = 0; i < dt.length; i++) {
        var item = dt[i];
        Data.push({
            NumberOrder: item.NumberOrder,
            IsReceived:item.IsReceived,
            TelephoneNumber: item.TelephoneNumber,
            CodeOTP: item.CodeOTP,
            SerialNumber: item.SerialNumber,
            CreatedDate: kendo.toString(new Date(item.CreatedDate), "dd/MM/yyyy hh:mm:ss"),
            Type: item.Type,
            StartDate: startDateEx,
            EndDate: endDateEx,
        });
    }
    console.log(Data)
    var url = _Host + "Bot/ExcelVoucherView";
    $.ajax({
        url: url,
        type: "POST",
        data: { Data: Data },
        beforeSend: function () {
            //console.log(1);
            $("body").append('<div class="wt-waiting wt-fixed wt-large"></div>');
        },
        complete: function (e) {
            $(".wt-waiting").remove();
        },
        success: function (rs) {
            //rs = JSON.parse(rs);
            if (rs != null) {
                if (rs.Table != null && rs.Table != undefined) {
                    DownLoadFile(_Host + "Bot/Download", rs.Table)
                }
            }
        },
        error: function (result) {
        }
    })
    //}
}
DownLoadFile = function (url, data) {
    location.href = url + '?FileName=' + data.FileName + "&FilePath=" + data.FilePath;
}