var _Stt = 0;
var _FilterName = [];
var _SortName = [];
var _GradeLevelID = '';

//data default
var myColumn = [{ title: "STT", width: 45, sortable: false, filterable: false, template: '#=++_Stt#', attributes: { style: "text-align: right;" } }];
var myDataSource = [];
var myDetailInit = false;
var myDataSort = false;
//data default

$(document).load(function () {

});


function InitKendoTreeList(id) {
    $(id).kendoTreeList({
        height: 457,
        sortable: true,
        selectable: true,
        scrollable: true,
        resizable: true,
        lockedTable: false,
        columns: myColumn,
        dataSource: myDataSource,
        //detailInit: myDetailInit,
        //pageable: {
        //    refresh: true,
        //    buttonCount: 5,
        //    pageSizes: [5, 10, 20, 50, 100],
        //    messages: {
        //        display: 'từ <span style="font-weight: bold">{0}</span> đến <span style="font-weight: bold">{1}</span> trong <span style="font-weight: bold">{2}</span> mục',
        //        itemsPerPage: "số lượng hiển thị",
        //        first: "Trang đầu tiên",
        //        previous: "Trang trước",
        //        next: "Trang tiếp theo",
        //        last: "Trang cuối cùng",
        //        refresh: "Tải lại dữ liệu",
        //        empty: "Không có dữ liệu"
        //    }
        //},
        //groupable: {
        //    messages: {
        //        empty: "Kéo một tiêu đề cột và thả nó vào đây để nhóm theo cột đó!"
        //    }
        //},
        filterable: { extra: false, operators: { boolean: { eq: "On", neq: "Off" } } },
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
                firstValueDatePicker.wrapper.context.placeholder = "Tư ngày";
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
            else if (arrField.indexOf(e.field) != -1 && arrType[arrField.indexOf(e.field)] == "number") {
                var firstValueDropDown = e.container.find("select:eq(0)").data("kendoDropDownList");
                firstValueDropDown.dataSource.data([{ value: "gte", text: "Nhỏ hơn hoặc bằng" }]);
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
                secondValueDropDown.dataSource.data([{ value: "lte", text: "Lớn hơn hoặc bằng" }]);
                secondValueDropDown.select(0);
                secondValueDropDown.trigger("change");
                secondValueDropDown.enable(false);
                secondValueDropDown.wrapper.hide();


                var firstValueNumeric = e.container.find("input:eq(1)").data("kendoNumericTextBox").options;
                firstValueNumeric.min = 0
                firstValueNumeric.step = 100;
                firstValueNumeric.format = "n0"

                e.container.find("input:eq(1)").data("kendoNumericTextBox").bind('change', function () {
                    firstValueDropDown.select(0);
                    firstValueDropDown.trigger("change");
                });

                var secondValueNumeric = e.container.find("input:eq(3)").data("kendoNumericTextBox").options;
                secondValueNumeric.min = 0
                secondValueNumeric.step = 100;
                secondValueNumeric.format = "n0"

                e.container.find("input:eq(3)").data("kendoNumericTextBox").bind('change', function () {
                    secondValueDropDown.select(0);
                    secondValueDropDown.trigger("change");
                });

                $(e.container.find("input:eq(0)")[0]).attr('placeholder', "Từ");
                $(e.container.find("input:eq(2)")[0]).attr('placeholder', "Đến");
            }
            else if (e.field == "Subject_DGCID") {
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


                var firstValueDatePicker = e.container.find("input:eq(0)").removeClass('k-textbox');
                var secondValueDatePicker = e.container.find("input:eq(1)").removeClass('k-textbox');

                GridFilter_Combobox(firstValueDatePicker, {
                    url: "Combobox/GetSubjectJS",
                    dataValueField: "SubjectID",
                    dataTextField: "SubjectName",
                    placeholder: "Chọn môn học",
                    param: JSON.stringify({ GradeLevelID: _GradeLevelID }),
                    change: function () {
                        if (this.value()) {
                            GridFilter_Combobox(secondValueDatePicker,
                                {
                                    url: "ComboBox/GetDGeCurriculumJS",
                                    param: JSON.stringify({ GradeLevelID: _GradeLevelID, SubjectID: this.value() }),
                                    dataValueField: "Value",
                                    dataTextField: "Text",
                                    placeholder: "Chọn bài học",
                                    isLevel: true,
                                    autoWidth: true,
                                    value: "",
                                    change: function () {
                                        secondValueDropDown.select(0);
                                        secondValueDropDown.trigger("change");
                                    }
                                });
                        } else {
                            GridFilter_Combobox(secondValueDatePicker,
                                 {
                                     placeholder: "Chọn bài học",
                                 });
                        }

                        firstValueDropDown.select(0);
                        firstValueDropDown.trigger("change");
                    }
                });
                GridFilter_Combobox(secondValueDatePicker,
                                 {
                                     placeholder: "Chọn bài học",
                                 });

            } else {
                var firstValueDropDown = e.container.find("select:eq(0)").data("kendoDropDownList");
                setTimeout(function () {
                    if (firstValueDropDown != undefined)
                        firstValueDropDown.wrapper.hide();
                });
            }
            //reset page = 1
            //e.container.find("button:eq(0)").on('click', function () { _Page = 1; });
        },
        dataBinding: function (e) {
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
                    $(id).data("kendoTreeList").expandRow(".k-master-row");
                }
                else {
                    $(this).removeClass("k-i-collapse").addClass("k-i-expand");
                    $(id).data("kendoTreeList").collapseRow(".k-master-row");
                }
            });
        },
        //detailExpand: function (e) {
        //    if ($(id + " .k-grid-content .k-hierarchy-cell .k-i-expand").length == 0) {
        //        $(id + " .k-header.k-hierarchy-cell .k-icon").removeClass("k-i-expand").addClass("k-i-collapse");
        //    }
        //},
        //detailCollapse: function (e) {
        //    if ($(id + " .k-grid-content .k-hierarchy-cell .k-i-collapse").length == 0) {
        //        $(id + " .k-header.k-hierarchy-cell .k-icon").removeClass("k-i-collapse").addClass("k-i-expand");
        //    }
        //}

    });
}

////////////////////////////////////////////////////// INIT GRID /////////////////////////////////////////////////
var rowNumber = 0;
function renderNumber(data) {
    return ++rowNumber;
}
function InitKendoGrid(id, Column, DataSource, DetailInit, selectable, isReload,dataSort) {
    //console.log(Column);
    if(isReload)
        $(id).empty();
    $(id).kendoGrid({
        height: 500,
        sortable: (dataSort != undefined ? dataSort : myDataSort),
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
            pageSizes: [5, 10, 20, 50, 100,'all'],
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
            rowNumber = 0;
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

//HTML DETAIL init from a controller
function GridLoadDataDetail(element, url, param, id, isMathJax, excuteFunc) {
    var obj = {
        url: url,
        param: param,
        id: id,
        isMathJax: isMathJax,
        excuteFunc: excuteFunc
    }
    GridLoadDataRowDetail(element, obj);
}

function GridLoadDataRowDetail(element, obj) {
    if (!obj) {
        obj = {};
        //excuteFunc
        //url, param, id, isMathJax
    }
    if (!obj.id) {
        obj.id = _GridID;
    }

    element.detailCell.find(".grid-detail-init").remove();
    element.detailCell.append('<div class="grid-detail-init" style="width:' + ($(obj.id).width() - 70) + 'px"></div>');
    var objInit = element.detailCell.find(".grid-detail-init");

    $.ajax({
        url: obj.url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        type: "POST",
        data: JSON.stringify(obj.param ? obj.param : ""),
        beforeSend: function () {
            objInit.html('<div class="k-waiting-ajax"></div>');
        },
        success: function (data) {
            if (data.models) {
                objInit.append(data.models);

                if (obj.isMathJax)
                    MathJax.Hub.Queue(["Typeset", MathJax.Hub, '.grid-detail-init']);

                if (obj.excuteFunc) {
                    var excuteFunc = obj.excuteFunc;
                    excuteFunc();
                }
            }
            else {
                var idRandom = Math.floor(Math.random() * 1000000000);
                objInit.append('Không tìm thấy dữ liệu <span id="' + idRandom + '" class="k-icon k-i-refresh" style="cursor:pointer;margin-top: -3px;" title="Tải lại dữ liệu"></span>');

                $("#" + idRandom).click(function () {
                    GridLoadDataRowDetail(element, obj);
                });
            }
        },
        error: function (err) {
            var idRandom = Math.floor(Math.random() * 1000000000);

            objInit.append('Không tìm thấy dữ liệu <span id="' + idRandom + '" class="k-icon k-i-refresh" style="cursor:pointer;margin-top: -3px;" title="Tải lại dữ liệu"></span>');

            $("#" + idRandom).click(function () {
                GridLoadDataRowDetail(element, obj);
            });
        },
        complete: function (e) {
            objInit.find(".k-waiting-ajax").fadeOut(1000, function () {
                $(this).remove();
            });
        }
    });
}




////////////////////////////////////////////////////// DATA FILTER ///////////////////////////////////////////////
function GridFilter_LoadDropDownList_RefLookup(element, ObjectName, optionLabel) {
    GridFilter_DropDownList(element,
        {
            url: "Ajax/GetRefLookup",
            param: JSON.stringify({ ObjectName: ObjectName }),
            dataValueField: "LookupID",
            dataTextField: "ObjectValue",
            placeholder: placeholder
        });
}

function GridFilter_LoadCombobox_RefLookup(element, ObjectName, placeholder) {
    GridFilter_Combobox(element,
        {
            url: "Ajax/GetRefLookup",
            param: JSON.stringify({ ObjectName: ObjectName }),
            dataValueField: "LookupID",
            dataTextField: "ObjectValue",
            placeholder: placeholder
        });
}

function GridFilter_LoadDropDownList(element, url, param, dataTextField, dataValueField, optionLabel, objData) {
    //element.kendoDropDownList({
    //    dataTextField: dataTextField,
    //    dataValueField: dataValueField,
    //    optionLabel: optionLabel
    //});
    //if (objData != undefined && objData != null && objData != "")
    //    element.data('kendoDropDownList').dataSource.data(objData);
    //else {
    //    $.ajax({
    //        type: "POST",
    //        url: url,
    //        contentType: "application/json; charset=utf-8",
    //        dataType: "json",
    //        data: JSON.stringify(param ? param : {}),
    //        success: function (msg) {
    //            var data = [];
    //            if (msg) {
    //                try {
    //                    data = msg.models;
    //                } catch (e) {
    //                    //arrString = JSON.parse(msg.d);
    //                }
    //            }
    //            element.data('kendoDropDownList').dataSource.data(data);
    //        }
    //    });
    //}


    var obj = {
        url: url,
        param: JSON.stringify(param ? param : {}),
        dataValueField: dataValueField,
        dataTextField: dataTextField,
        value: null,
        placeholder: optionLabel,
        change: null,
        template: null,
        dataSource: objData
    }

    GridFilter_DropDownList(element, obj);
}

function GridFilter_LoadCombobox(element, url, param, dataTextField, dataValueField, placeholder, objData) {
    //element.kendoComboBox({
    //    dataTextField: dataTextField,
    //    dataValueField: dataValueField,
    //    placeholder: placeholder,
    //    filter: "contains",
    //    suggest: true
    //});
    //if (objData != undefined && objData != null && objData != "")
    //    element.data('kendoComboBox').dataSource.data(objData);
    //else {
    //    $.ajax({
    //        type: "POST",
    //        url: url,
    //        contentType: "application/json; charset=utf-8",
    //        data: JSON.stringify(param ? param : {}),
    //        success: function (msg) {
    //            var data = [];
    //            if (msg) {
    //                try {
    //                    if (msg.models)
    //                        data = msg.models;
    //                    else
    //                        data = msg;

    //                } catch (e) {
    //                    //arrString = JSON.parse(msg.d);
    //                }
    //            }
    //            element.data('kendoComboBox').dataSource.data(data);
    //        }
    //    });
    //}

    var obj = {
        url: url,
        param: JSON.stringify(param ? param : {}),
        dataValueField: dataValueField,
        dataTextField: dataTextField,
        value: null,
        placeholder: placeholder,
        change: null,
        template: null,
        dataSource: objData
    }

    GridFilter_Combobox(element, obj);
}

/*

var element = "#TagID";
var obj = {
    url: null,
    param: null,
    dataValueField: null,
    dataTextField: null,
    value: null,
    placeholder: null,
    change: null,
    template: null,
    dataSource: null
} 

GridFilter_DropDownList(element, obj);

*/

function GridFilter_DropDownList(element, obj) {
    if (!obj) obj = {};

    element.kendoDropDownList({
        dataValueField: (obj.dataValueField ? obj.dataValueField : "Value"),
        dataTextField: (obj.dataTextField ? obj.dataTextField : "Text"),
        value: (obj.value ? obj.value : null),
        optionLabel: (obj.placeholder ? obj.placeholder : ""),
        change: (obj.change ? obj.change : false),
        template: (obj.template ? obj.template : null),
        noDataTemplate: "Không tìm thấy dữ liệu!",
        dataSource: {
            transport: {
                read: function (options) {
                    if (obj.dataSource) {
                        options.success([obj.dataSource]);
                        return;
                    }

                    if (!obj.url) {
                        options.success([]);
                        return;
                    }

                    $.ajax({
                        type: "POST",
                        url: _RootBase + obj.url,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: (obj.param ? obj.param : ""),
                        success: function (e) {
                            if (e) {
                                if (e.models != undefined || e.models === null) {
                                    options.success(e.models);
                                }
                                else if (e.Table != undefined || e.Table === null) {
                                    options.success(e.Table);
                                }
                                else {
                                    options.success(e);
                                }
                            }
                            else {
                                options.success([]);
                            }
                        },
                        error: function (e) {
                            options.success([]);
                        }
                    });
                }
            }
        }
    });
}


/*

var element = "#TagID";
var obj = {
    url: null,
    param: null,
    dataValueField: null,
    dataTextField: null,
    value: null,
    placeholder: null,
    change: function () {
        if (!this.dataItem()) { this.value(null); this.text(null)}
    },
    template: null,
    dataSource: null
}

GridFilter_Combobox(element, obj);

*/

function GridFilter_Combobox(element, obj) {
    if (!obj) obj = {};

    if (obj.isLevel) {
        obj.template = '#if(data.Level){#<div style="'
                    + 'padding-left:#=(parseInt(Level)-1)*20#px;'
                    + '#if(parseInt(data.Level) == 1){# font-weight:bold; #}#'
                    + '#if(("' + (obj.disableLevel ? obj.disableLevel : "") + '").split(",").indexOf(data.Level + "") != -1){# color:rgb(181,181,181); #}#'
                    + '">#=Text#</div>#}#';
    }

    element.kendoComboBox({
        dataValueField: (obj.dataValueField ? obj.dataValueField : "Value"),
        dataTextField: (obj.dataTextField ? obj.dataTextField : "Text"),
        value: (obj.value ? obj.value : null),
        placeholder: (obj.placeholder ? obj.placeholder : "Chọn dữ liệu"),
        change: (obj.change ? obj.change : function () { if (!this.dataItem()) { this.value(null); this.text(null) } }),
        template: (obj.template ? obj.template : null),
        noDataTemplate: "Không tìm thấy dữ liệu!",
        suggest: true,
        filter: "contains",
        autoWidth: (obj.autoWidth ? obj.autoWidth : false),
        dataSource: {
            transport: {
                read: function (options) {
                    if (obj.dataSource) {
                        options.success([obj.dataSource]);
                        return;
                    }

                    if (!obj.url) {
                        options.success([]);
                        return;
                    }

                    $.ajax({
                        type: "POST",
                        url: _RootBase + obj.url,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: (obj.param ? obj.param : ""),
                        success: function (e) {
                            if (e) {
                                if (e.models != undefined || e.models === null) {
                                    options.success(e.models);
                                }
                                else if (e.Table != undefined || e.Table === null) {
                                    options.success(e.Table);
                                }
                                else {
                                    options.success(e);
                                }
                            }
                            else {
                                options.success([]);
                            }
                        },
                        error: function (e) {
                            options.success([]);
                        }
                    });
                }
            }
        }
    });
}


////////////////////////////////////////////////////// KEEP FILTER ///////////////////////////////////////////////
var HasStorage = (function () {
    try {
        var ls = window.localStorage,
            t1 = "check_storage",
            t2 = null;
        ls.setItem(t1, t1);
        t2 = ls.getItem(t1);
        ls.removeItem(t1);
        return (t1 === t2);
    } catch (exception) {
        return false;
    }
}());

function KeepParameter(name, objJS) {
    if (HasStorage) {
        if (objJS == undefined) {
            //get
            return JSON.parse(localStorage.getItem(name));
        }
        else {
            //set
            localStorage.setItem(name, JSON.stringify(objJS));
        }
    } else {

        // Sorry! No Web Storage support..
    }
    return null;
}

function KeepParameterID(nameStorage, SelectedID) {
    if (SelectedID || SelectedID == "") {
        var objDataJS = KeepParameter(nameStorage);
        if (objDataJS != null) {
            objDataJS.SelectedID = SelectedID;
            KeepParameter(nameStorage, objDataJS);
        }
    }
}

function KeepFilter(name_storage_session_change) {
    var obj = KeepParameter(name_storage_session_change);
    if (obj) {
        try {
            if (obj.SelectedID && obj.data.filter.filters)
                return obj.data.filter.filters;
        }
        catch (e) { }
    }
    return [];
}

function KeepSort(name_storage_session_change) {
    var obj = KeepParameter(name_storage_session_change);
    if (obj) {
        try {
            if (obj.SelectedID && obj.data.sort)
                return obj.data.sort;
        }
        catch (e) { }
    }
    return [];
}

function KeepPage(name_storage_session_change) {
    var obj = KeepParameter(name_storage_session_change);
    if (obj) {
        try {
            if (obj.SelectedID && obj.data.page)
                return obj.data.page;
        }
        catch (e) { }
    }
    return 1;
}

function KeepPageSize(name_storage_session_change) {
    var obj = KeepParameter(name_storage_session_change);
    if (obj) {
        try {
            if (obj.SelectedID && obj.data.pageSize)
                return obj.data.pageSize;
        }
        catch (e) { }
    }
    return 20;
}

function SaveFilter(id, name_storage_session_change, TextField) {
    if (!id)
        id = _GridID;

    if (!name_storage_session_change)
        name_storage_session_change = NAME_STORAGE_SESSION;

    if (TextField) {
        var grid = $(id).data("kendoGrid");
        try {
            var SelectedItem = grid.dataItem(grid.select());
            if (SelectedItem) {
                var SelectID = SelectedItem[TextField];

                //keep ID
                if (SelectID) {
                    KeepParameterID(name_storage_session_change, SelectID);
                    return;
                }
            }
        }
        catch (e) { }
    }
    KeepParameterID(name_storage_session_change, -1);

    console.log("keep Successfull");
}




////////////////////////////////////////////////////// GET FILTER ////////////////////////////////////////////////
function GridSelectedID(grid, name_storage_session_change, options, ArrFilter, ArrSort, FieldSelected) {
    var KeepOld = KeepParameter(name_storage_session_change);
    if (KeepOld && KeepOld.SelectedID) {
        var view = grid.dataSource.view();
        for (var i = 0; i < view.length; i++) {
            if (view[i][FieldSelected] == KeepOld.SelectedID) {
                grid.select(grid.table.find("tr[data-uid='" + view[i].uid + "']"));
                //console.log(grid.table.find("tr[data-uid='" + view[i].uid + "']"));
                //grid.table.find("tr[data-uid='" + view[i].uid + "']").scrollTop();
                
                grid.element.find(".k-grid-content").animate({  // use $('html, body') if you want to scroll the body and not the k-grid-content div
                    scrollTop: grid.select().offset().top  //  scroll to the selected row given by 'this.select()'
                }, 500);
                break;
            }
        }
        KeepParameterID(name_storage_session_change, "");
    }

    KeepParameter(NAME_STORAGE_SESSION, {
        data: options.data,
        ArrFilter: ArrFilter,
        ArrSort: ArrSort
    });
    //end
}

function GridGetParamFilter(grid, OptionData) {
    var FieldModel = {};

    try {
        FieldModel = grid.dataSource.reader.model.fields;
    }
    catch (e) { }

    var ArrFieldModel = [];
    var ArrTypeModel = [];

    $.each(FieldModel, function (index, value) {
        ArrFieldModel.push(index);
        ArrTypeModel.push(value.type);
    });

    var ArrField = [];
    var ArrValue = [];
    _FilterName = [];

    var objFilter = OptionData.filter;

    if (objFilter) {
        var valueFilter = objFilter.filters;

        for (var i = 0; i < valueFilter.length; i++) {
            //push _FilterName
            var column = grid.columns;
            for (var j = 0; j < column.length; j++) {
                if (column[j].field == valueFilter[i].field && (valueFilter[i].value || valueFilter[i].value === false))
                    if (column[j].title)
                        _FilterName.push(column[j].title);
            }
            //end push _FilterName

            var valueFilter_1 = valueFilter[i].filters;

            if (valueFilter_1 == undefined) {
                var field = valueFilter[i].field;
                var value = valueFilter[i].value;
                var operator = valueFilter[i].operator;

                if (ArrFieldModel.indexOf(field) != -1 && ArrTypeModel[ArrFieldModel.indexOf(field)] == "date") {
                    if (operator == "gte") {
                        ArrField.push(field + "From");
                    }
                    else //if (operator == "lte")
                    {
                        ArrField.push(field + "To");
                    }

                    ArrValue.push(kendo.toString(kendo.parseDate(value), "dd/MM/yyyy"));
                }
                else if (ArrFieldModel.indexOf(field) != -1 && ArrTypeModel[ArrFieldModel.indexOf(field)] == "number") {
                    if (operator == "gte") {
                        ArrField.push(field + "From");
                    }
                    else //if (operator == "lte")
                    {
                        ArrField.push(field + "To");
                    }

                    ArrValue.push(value);
                }//e.field == "Subject_DGCID
                else if (field == "SubjectName") {
                    if (operator == "gte") {
                        ArrField.push("SubjectID");
                    }
                    else //if (operator == "lte")
                    {
                        ArrField.push("DGCID");
                    }

                    ArrValue.push(value);
                }
                else {
                    ArrField.push(field);
                    ArrValue.push(value);
                }
            }
            else {
                for (var x = 0; x < valueFilter_1.length; x++) {
                    //push _FilterName
                    for (var y = 0; y < column.length; y++) {
                        if (column[y].field == valueFilter_1[x].field && (valueFilter_1[x].value || valueFilter_1[x].value === false)) {
                            if (column[y].title)
                                _FilterName.push(column[y].title);
                        }
                    }

                    var field = valueFilter_1[x].field;
                    var value = valueFilter_1[x].value;
                    var operator = valueFilter_1[x].operator;

                    if (ArrFieldModel.indexOf(field) != -1 && ArrTypeModel[ArrFieldModel.indexOf(field)] == "date") {
                        if (operator == "gte") {
                            ArrField.push(field + "From");
                        }
                        else //if (operator == "lte") 
                        {
                            ArrField.push(field + "To");
                        }

                        ArrValue.push(kendo.toString(kendo.parseDate(value), "dd/MM/yyyy"));
                    }
                    else if (ArrFieldModel.indexOf(field) != -1 && ArrTypeModel[ArrFieldModel.indexOf(field)] == "number") {
                        if (operator == "gte") {
                            ArrField.push(field + "From");
                        }
                        else //if (operator == "lte")
                        {
                            ArrField.push(field + "To");
                        }

                        ArrValue.push(value);
                    }
                    else if (field == "SubjectName") {
                        if (operator == "gte") {
                            ArrField.push("SubjectID");
                        }
                        else //if (operator == "lte")
                        {
                            ArrField.push("DGCID");
                        }

                        ArrValue.push(value);
                    }
                    else {
                        ArrField.push(field);
                        ArrValue.push(value);
                    }
                }
            }
        }
    }

    return [ArrField, ArrValue];
}

function GridGetParamSort(grid, OptionData) {
    var SortField = "";
    var SortType = "";
    _SortName = [];

    if (OptionData && OptionData.sort && OptionData.sort.length > 0) {
        SortField = OptionData.sort[0].field;
        SortType = OptionData.sort[0].dir;

        if (SortType)
            SortType = SortType.toUpperCase();

        //push _SortName
        var column = grid.columns;
        for (var i = 0; i < column.length; i++) {
            if (column[i].field == SortField && (SortType == "ASC" || SortType == "DESC") && column[i].title)
                _SortName.push(column[i].title);
        }
        //end push _SortName
    }

    return { SortField: SortField, SortType: SortType };
}

//TextField : field in column view
function GridGetValueFilter(ArrFilter, TextField) {
    if (ArrFilter[0].length == 0)
        return "";
    if (ArrFilter[0].length != ArrFilter[1].length)
        return "";

    var arrTextField = TextField.split(",");

    var valueReturn = "";
    $.each(arrTextField, function (index, value) {
        var indexField = ArrFilter[0].indexOf(value);
        if (indexField != -1)
            valueReturn = ArrFilter[1][indexField];
    });


    return valueReturn;
}

//TextField : field in column sort
function GridGetValueSort(ArrSort, TextField, ConstantType) {
    if (!ArrSort[TextField]) {
        return "";
    }
    else {
        if (TextField == "SortType") {
            return ArrSort[TextField];
        }
        else {
            var SortFieldID = "";
            switch (ConstantType + "_" + ArrSort[TextField]) {
                //Ngan hang cau hoi
                case 'question_V_QuestionTypeName': SortFieldID = "50"; break;
                case 'question_Contents': SortFieldID = "51"; break;
                case 'question_SubjectName': SortFieldID = "52"; break;
                case 'question_GradelevelName': SortFieldID = "53"; break;
                case 'question_V_QuestionLevelName': SortFieldID = "54"; break;
                case 'question_ApprovalStatus': SortFieldID = "55"; break;

                    //Vo de
                case 'examlibrary_QuestionnaireTemplateName': SortFieldID = "184"; break;
                case 'examlibrary_SpecName': SortFieldID = "185"; break;
                case 'examlibrary_SubjectName': SortFieldID = "186"; break;
                case 'examlibrary_EduSysName': SortFieldID = "187"; break;
            }
            return SortFieldID;
        }
    }
}




////////////////////////////////////////////////////// PAGE INFO /////////////////////////////////////////////////
function PageInfo(page, numrow, total, isgridchild) {
    if (page > 0) {
        $('#startIndex').html((page - 1) * numrow + 1);
        if (page * numrow > total)
            $('#endIndex').html(total);
        else
            $('#endIndex').html(page * numrow);
        $('#totalRow').html(total);
    }
    else {
        $('#startIndex').html(0);
        $('#endIndex').html(0);
        $('#totalRow').html(0);
    }

    var html = '';
    if (_FilterName.length > 0) {
        html += ' || Đang lọc trên các cột: <b>' + _FilterName.join(', ') + '</b>';
    }

    if (_SortName.length > 0) {
        html += ' || Đang sắp xếp trên cột: <b>' + _SortName.join(', ') + '</b>';
    }
    if (isgridchild == undefined) {
        $("#gridInfoName").html(html);
        GridResize();
    }
}

function DataNull(id) {
    if ($(id).data("kendoGrid") != undefined) {
        $(id).find('tbody').append('<div style="text-align:left;position:absolute;margin:20px;">Không tìm thấy dữ liệu!</div>');
    }
    else if ($(id).data("kendoTreeList") != undefined) {
        $(id).find('.k-status').text("Không tìm thấy dữ liệu!");
    }
}




////////////////////////////////////////////////////// TOOLTIP /////////////////////////////////////////////////////
//hàm kiểm tra xem có xuất hiện dấu 3 chấm mới hiện tooltip
function isEllipsisActive(e) {
    return (e.offsetWidth < e.scrollWidth);
}
//custom show tooltip
kendo.ui.Tooltip.fn._show = function (show) {
    return function (target) {
        if (isEllipsisActive(target[0])) {
            show.call(this, target);
        }
        else {
            if (target.hasClass("glyphicon-ok") || target.hasClass("user-infor")) {
                show.call(this, target);
            };
        }
    };
}(kendo.ui.Tooltip.fn._show);

function InitToolTip(id) {
    $(id).kendoTooltip({
        filter: "td:not(.not-tooltip)",
        position: "bottom",
        content: function (e) {
            var target = e.target;
            var text = target.text();
            if (text == "") {
                return "Không có dữ liệu";
            }
            return text;
        },
        show: function (e) {
            this.popup.wrapper.width("400px");
        }
    }).data("kendoTooltip");
}




////////////////////////////////////////////////////// SUPPORT ///////////////////////////////////////////////////
//delete tag html
function HtmlDeltag(value) {
    //return value.replace(/&quot;/g, '"')
    //            .replace(/&#39;/g, "'")
    //            .replace(/&lt;/g, '<')
    //            .replace(/&gt;/g, '>')
    //            .replace(/&amp;/g, '&')
    //            .replace(/(<([^>]+)>)/ig, ""); // clear Tag

    return value.replace(/(<([^>]+)>)/ig, ""); // clear Tag
}




////////////////////////////////////////////////////// RESIZE /////////////////////////////////////////////////////
function GridResize() {
    var heightGrid = $(window).height() - 180;

    //if (heightGrid < 457)
    //    heightGrid = 457;

    $(_GridID + ".k-grid").height(heightGrid + 20);
    $(_GridID + ".k-grid .k-grid-content").height(heightGrid - 110);

    $(_GridID + ".k-treelist .k-grid-content").height(heightGrid - 17);
}


/* 
neu co hon 1 cot can resize thi truyen kieu chuoi cac cot lien nhau cach nhau boi dau phay
neu co hon 1 cot resize khac width thi width truyen kieu chuoi nhu cot ( so cot = so width )
( cot can resize , width , width limit )
*/
//function resizeRowGrid(colum, width, maxWidth) {
//    //var contentWidth = $('#mainCnt').width();
//    var offsetLeft = $('#mainCnt').offset().left;
//    var contentWidth = $(window).width() - (offsetLeft * 2) - 20; // trừ thêm 20px cho scroll dọc

//    var regex = /^(\+|-)?((\d+(\.\d+)?)|(\[0].\d+))$/;

//    if (regex.test(width)) {    // khi truyen 1 width cho nhieu cot
//        if (regex.test(colum)) {    // khi truyen 1 cot        
//            if (contentWidth < maxWidth) {
//                setResize(colum, width);
//            }
//            else {
//                width = contentWidth - (maxWidth - width);
//                setResize(colum, width);
//            }
//        }
//        else {  // khi truyen nhieu cot
//            var arrColum = colum.split(',');

//            for (var i = 0; i < arrColum.length; i++) {
//                if (contentWidth < maxWidth) {
//                    setResize(arrColum[i], width);
//                }
//                else {
//                    width = width + (contentWidth - maxWidth - 1) / arrColum.length;    // cong them width khi vuot man hinh lon hon width gioi han
//                    setResize(arrColum[i], width);
//                    width = width - (contentWidth - maxWidth - 1) / arrColum.length;    // can bang width khi vong loop xoay vong
//                }
//            }
//        }
//    }
//    else { // khi truyen nhieu width cho nhieu cot
//        var arrWidth = width.split(',');
//        var arrColum = colum.split(',');
//        if (arrWidth.length == arrColum.length) {
//            for (var i = 0; i < arrColum.length; i++) {
//                if (contentWidth < maxWidth) {
//                    setResize(arrColum[i], arrWidth[i]);
//                }
//                else {
//                    width = parseInt(arrWidth[i]) + (contentWidth - maxWidth - 1) / arrColum.length;    // cong them width khi vuot man hinh lon hon width gioi han
//                    setResize(arrColum[i], width);
//                }
//            }
//        } else alert("arrColum.length != arrWidth.length ,nếu truyền nhiều cột phải truyền vào số width tương ứng!");
//    }
//    fixheightGrid_all();
//}

//function setResize(col, w) {
//    $(".k-grid table tr td:nth-child(" + col + ")").css("width", (w - 17) + "px");
//    $(".k-grid-header .k-header:nth-child(" + col + ")").css("width", (w - 24) + "px");
//}