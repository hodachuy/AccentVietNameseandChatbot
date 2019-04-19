// Nhận dữ liệu trả về list json từ Ajax.
LoadComboBoxWithServices = function (element, url, param, valueField, textField, value, placeholder, template, isSearch, FuncChange, valueDelete, isMultiSelect) {
    if (isSearch == undefined) isSearch = false;
    if (FuncChange == undefined) FuncChange = null;

    var combobox = $(element).kendoComboBox({
        dataTextField: textField,
        dataValueField: valueField,
        template: template,
        dataSource: {
            transport: {
                read: function (options) {
                    var svr = new AjaxCall(url, param);
                    svr.callService(function (data) {
                        if (data != null && data.length > 0) {
                            if (data.Table != null) {
                                options.success(data.Table);
                            } else {
                                options.success(data);
                            }

                        } else {
                            options.success("");
                        }
                    });

                }
            }
        }
        ,
        filter: "contains",
        suggest: true,
        change: FuncChange
    });

    //if (!isSearch) {
    //    combobox.data("kendoComboBox").input.attr("readonly", true)
    //       .on("keydown", function (e) {
    //           if (e.keyCode === 8) {
    //               e.preventDefault();
    //           }
    //       });
    //}
    combobox.data("kendoComboBox").input.click(function () {
        combobox.data("kendoComboBox").open();
    });

    if (placeholder != undefined)
        combobox.data("kendoComboBox").input.attr("placeholder", placeholder);
    if (value != undefined) {
        combobox.data("kendoComboBox").value(value);
    } else {
        combobox.data("kendoComboBox").value(null);
    }

    if (valueDelete)
        DeleteDataComboBoxMultiSelection(id, valueField, valueDelete);
}
LoadComboBoxWithServices2 = function (element, url, param, valueField, textField, value, placeholder, template, isSearch, FuncChange, valueDelete) {
    if (isSearch == undefined) isSearch = false;
    if (FuncChange == undefined) FuncChange = null;

    var combobox = $(element).kendoComboBox({
        dataTextField: textField,
        dataValueField: valueField,
        template: template,
        dataSource: {
            transport: {
                read: function (options) {
                    var svr = new AjaxCall(url, param);
                    svr.callService(function (data) {
                        data = JSON.parse(data);
                        if (data.Table != null) {
                            if (data.Table != null) {
                                options.success(data.Table);
                            } else {
                                options.success(data);
                            }

                        } else {
                            options.success("");
                        }
                    });

                }
            }
        }
        ,
        filter: "contains",
        suggest: true,
        change: FuncChange
    });

    //if (!isSearch) {
    //    combobox.data("kendoComboBox").input.attr("readonly", true)
    //       .on("keydown", function (e) {
    //           if (e.keyCode === 8) {
    //               e.preventDefault();
    //           }
    //       });
    //}
    combobox.data("kendoComboBox").input.click(function () {
        combobox.data("kendoComboBox").open();
    });

    if (placeholder != undefined)
        combobox.data("kendoComboBox").input.attr("placeholder", placeholder);
    if (value != undefined) {
        combobox.data("kendoComboBox").value(value);
    } else {
        combobox.data("kendoComboBox").value(null);
    }

    if (valueDelete)
        DeleteDataComboBoxMultiSelection(id, valueField, valueDelete);
}
LoadComboBoxWithServices3 = function (element, url, param, valueField, textField, value, placeholder, template, isSearch, FuncChange, valueDelete,valueID) {
	if (isSearch == undefined) isSearch = false;
	if (FuncChange == undefined) FuncChange = null;

	var combobox = $(element).kendoComboBox({
		dataTextField: textField,
		dataValueField: valueField,
		template: template,
		dataSource: {
			transport: {
				read: function (options) {
					var svr = new AjaxCall(url, param);
					svr.callService(function (data) {
						if (data != null && data.length > 0) {
							if (data.Table != null) {
								options.success(data.Table);
							} else {
								for (var i = data.length - 1; i >= 0; i--) {
									if (data[i].BU_ID === valueID) {
										data.splice(i, 1);
									}
									options.success(data);
								}
							}
						} else {
							options.success("");
						}
					});

				}
			}
		}
        ,
		filter: "contains",
		suggest: true,
		change: FuncChange
	});

	//if (!isSearch) {
	//    combobox.data("kendoComboBox").input.attr("readonly", true)
	//       .on("keydown", function (e) {
	//           if (e.keyCode === 8) {
	//               e.preventDefault();
	//           }
	//       });
	//}
	combobox.data("kendoComboBox").input.click(function () {
		combobox.data("kendoComboBox").open();
	});

	if (placeholder != undefined)
		combobox.data("kendoComboBox").input.attr("placeholder", placeholder);
	if (value != undefined) {
		combobox.data("kendoComboBox").value(value);
	} else {
		combobox.data("kendoComboBox").value(null);
	}

	if (valueDelete)
		DeleteDataComboBoxMultiSelection(id, valueField, valueDelete);
}

// Nhận dữ liệu trả về xml tới json từ Ajax.
LoadComboBoxWithServices4 = function (element, url, param, valueField, textField, value, placeholder, template, isSearch, FuncChange, valueDelete, isMultiSelect) {
    if (isSearch == undefined) isSearch = false;
    if (FuncChange == undefined) FuncChange = null;

    var combobox = $(element).kendoComboBox({
        autoBind: false,
        dataTextField: textField,
        dataValueField: valueField,
        template: template,
        dataSource: {
            transport: {
                read: function (options) {
                    var svr = new AjaxCall(url, param);
                    svr.callService(function (data) {
                        data = JSON.parse(data);
                        if (data.Table != null) {
                            if (data.Table != null) {
                                options.success(data.Table);
                            } else {
                                options.success(data);
                            }

                        } else {
                            options.success("");
                        }
                    });

                }
            }
        }
        ,
        filter: "contains",
        suggest: true,
        change: FuncChange
    });

    //if (!isSearch) {
    //    combobox.data("kendoComboBox").input.attr("readonly", true)
    //       .on("keydown", function (e) {
    //           if (e.keyCode === 8) {
    //               e.preventDefault();
    //           }
    //       });
    //}
    combobox.data("kendoComboBox").input.click(function () {
        combobox.data("kendoComboBox").open();
    });

    if (placeholder != undefined)
        combobox.data("kendoComboBox").input.attr("placeholder", placeholder);
    if (value != undefined) {
        combobox.data("kendoComboBox").value(value);
    } else {
        combobox.data("kendoComboBox").value(null);
    }

    if (valueDelete)
        DeleteDataComboBoxMultiSelection(id, valueField, valueDelete);
}
LoadDataComboBoxWithServices = function (id, url, param, value) {
    var combobox = $("#" + id).data('kendoComboBox');
    var dataJS = null;
    $.ajax({
        url: url,
        type: "POST",
        data: param,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data) {
                dataJS = JSON.parse(data).Table;
                if (dataJS == undefined) {
                    dataJS = JSON.parse(data);
                }

                combobox.setDataSource(dataJS);
            } else {
                combobox.setDataSource([]);
            }

            if (value != undefined) {
                combobox.value(value);
            } else {
                combobox.value(null);
            }
        }
    });
}

LoadComboBox = function (id, data, valueField, textField, value, placeholder) {
    var combobox = $("#" + id).kendoComboBox({
        dataTextField: textField,
        dataValueField: valueField,
        dataSource: data,
        filter: "contains",
        suggest: true
    });
    combobox.data("kendoComboBox").input.click(function () {
        combobox.data("kendoComboBox").open();
    });

    if (placeholder != undefined)
        combobox.data("kendoComboBox").input.attr("placeholder", placeholder);
    if (value != undefined)
        combobox.data("kendoComboBox").value(value);
}
LoadDropdownList = function (id, data, valueField, textField, value, placeholder) {
    var combobox = $("#" + id).kendoDropDownList({
        dataTextField: textField,
        dataValueField: valueField,
        dataSource: data,
        filter: "contains",
        suggest: true
    });
    if (placeholder != undefined)
        combobox.data("kendoDropDownList").input.attr("placeholder", placeholder);
    if (value != undefined)
        combobox.data("kendoDropDownList").value(value);
}

LoadDatePicker = function (id, value) {
    $("#" + id).kendoDatePicker({
        format: "dd/MM/yyyy"
    });
    $("#" + id).kendoMaskedTextBox({
        mask: "00/00/0000"
    });
    if (value != undefined) {
        $("#" + id).data("kendoDatePicker").value(value);
    }
    $("#" + id).focusout(function () {
        if ($("#" + id).data("kendoDatePicker").value() == null) {
            $("#" + id).parent("span").parent("span").parent("td").append('<span id="ValidateDate' + id + '" class="k-widget k-tooltip k-tooltip-validation k-invalid-msg" style="position:absolute;top:14px;margin-left:8px" role="alert"><span class="k-icon k-warning"> </span> This field is not valid date</span>');
        }
    });
    $("#" + id).click(function () {
        $("#ValidateDate" + id).remove();
        $("#" + id).data("kendoDatePicker").open();
    });
}


loadEditor = function (element, placeholdertext, value) {

    placeholdertext = (placeholdertext || '')
    $(element).kendoEditor({
        //immutables: true,
        pasteCleanup: {
            msAllFormatting: false,
            msConvertLists: true,
            msTags: true
        },
        encoded: false,
        tools: [
            { name: "insertLineBreak", shift: false },
            { name: "insertParagraph", shift: true },
            "formatting",
            "bold",
            "italic",
            "underline",
            "justifyLeft",
            "justifyCenter",
            "justifyRight",
            "justifyFull",
            "insertUnorderedList",
            "insertOrderedList",
            "indent",
            "outdent",
            "createLink",
                                        "viewHtml",
            "unlink",
            {
                name: "fontName",
                items: [{
                    text: "Courier",
                    value: "Courier"
                }, {
                    text: "Times New Roman",
                    value: "Times New Roman"
                }, {
                    text: "Trebuchet MS",
                    value: "Trebuchet MS"
                }, {
                    text: "Arial",
                    value: "Arial"
                }, {
                    text: "Segoe UI",
                    value: "Segoe UI"
                }]
            },
            {
                name: "fontSize",
                items: [{
                    text: "9px",
                    value: "9pt"
                }, {
                    text: "10px",
                    value: "10pt"
                }, {
                    text: "11px",
                    value: "11pt"
                }, {
                    text: "12px",
                    value: "12pt"
                }, {
                    text: "13px",
                    value: "13pt"
                }, {
                    text: "13.5px",
                    value: "13.5pt"
                }, {
                    text: "14px",
                    value: "14pt"
                }, {
                    text: "16px",
                    value: "16pt"
                }, {
                    text: "18px",
                    value: "18pt"
                }, {
                    text: "20px",
                    value: "20pt"
                }, {
                    text: "22px",
                    value: "22pt"
                }, {
                    text: "24px",
                    value: "24pt"
                }]

            },
            				"tableWizard",
                "createTable",
                "addRowAbove",
                "addRowBelow",
                "addColumnLeft",
                "addColumnRight",
                "deleteRow",
                "deleteColumn"
        ],
        execute: function (e) {
            if (e.name == "createlink") {
                setTimeout(function () {
                    $("#k-editor-link-target").attr("checked", true);
                });
                console.log(e);
            }
        },
    });
    if (value != undefined && value != "") {
        $(element).data("kendoEditor").value(value);
    }
    if (placeholdertext != undefined && placeholdertext != "") {
        PlaceholderEditorKendo(element, placeholdertext);
    }
}

function PlaceholderEditorKendo(idorclass, placeholdertext) {
    var editor = $(idorclass);
    var el = editor.data('kendoEditor');
    var $thish = editor.parent().find(".placeholderkd");
    editor.on('blur', function (e) {
        var $thisPlace = $(this).parent().find(".placeholderkd");
        if (el.value() == "") {
            $thisPlace.show();
        }
        else {
            $thisPlace.hide();
        }
    });
    editor.on('focus', function (e) {
        var $thisPlace = $(this).parent().find(".placeholderkd");
        $thisPlace.hide();
    });
    var $thisPlace = $(this).parent().find(".placeholderkd");
    $thisPlace.on('click', function (e) {
        editor.focus();
    })


    if ($thish.length == 0) {
        $thish = $("<div for=\"" + idorclass + "\" class=\"placeholderkd\" style=\"display:none;position:absolute;\">" + placeholdertext + "</div>").insertBefore(idorclass)
            .click(function () {
                editor.focus();
            });
    }
    if (el.value() == "") {
        $thish.show();
    }
    else {
        $thish.hide();
    }
}


//---------------------------------------------------------- NEW 8/12/2016 -----------------------------------------------------

//------------------------------------------------------------ INIT NUMERIC TEXTBOX --------------------------------------------------
/*

var element = "#TagID";
var obj = {
    min: null,
    max: null,
    step: null,
    value: null,
    placeholder: null,
    change: null,
    format: null,
    decimals: null
}

InitNumericTextBox(element, obj);

*/

InitNumericTextBox = function (element, obj) {
    if (!obj) obj = {};

    if ($(element).data("kendoNumericTextBox")) {
        $(element).data("kendoNumericTextBox").value(null);
        $(element).data("kendoNumericTextBox").destroy();
    }

    $(element).kendoNumericTextBox({
        min: ((obj.min === 0 || obj.min) ? obj.min : 1),
        max: ((obj.max === 0 || obj.max) ? obj.max : 1000),
        step: (obj.step ? obj.step : 1),
        value: ((obj.value === 0 || obj.value) ? obj.value : null),
        placeholder: (obj.placeholder ? obj.placeholder : "Số lượng"),
        change: (obj.change ? obj.change : false),
        format: (obj.format ? obj.format : "n0"),   // n0 : 1000000 -> 1.000.000
        decimals: (obj.decimals ? obj.decimals : 0), // 0 : 10.00 -> 10
    });
}


//------------------------------------------------------------ INIT DROPDOWN LIST --------------------------------------------------
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
    enable: null,
    template: null,
    isLevel: null,          // true: phân cấp cây theo Level(template)
    disableLevel: null          //  level disable       VD: '1,2,...'     
    disableValueField: null,    //  field disable       VD: 'Value'
    disableValueData: null,     //  giá trị disable     VD: '123,456'

    dataSource: null,

    trigger: null       //change, ...
} 

InitDropDownList(element, obj);

*/

InitDropDownList = function (element, obj) {
    if (!obj) obj = {};

    if ($(element).data("kendoDropDownList")) {
        $(element).data("kendoDropDownList").value(null);
        $(element).data("kendoDropDownList").destroy();
    }

    if (obj.isLevel) {
        obj.template = '#if(data.Level){#<div style="'
                    + 'padding-left:#=(parseInt(Level)-1)*20#px;'
                    + '#if(parseInt(data.Level) == 1){# font-weight:bold; #}#'
                    + '#if(("' + (obj.disableLevel ? obj.disableLevel : "") + '").split(",").indexOf(data.Level + "") != -1){# color:rgb(181,181,181); #}#'
                    + '">#=Text#</div>#}#';
    }

    $(element).kendoDropDownList({
        dataValueField: (obj.dataValueField ? obj.dataValueField : null),
        dataTextField: (obj.dataTextField ? obj.dataTextField : null),
        value: (obj.value ? obj.value : null),
        optionLabel: (obj.placeholder ? obj.placeholder : ""),
        change: (obj.change ? obj.change : false),
        template: (obj.template ? obj.template : null),
        noDataTemplate: "Không tìm thấy dữ liệu!",
        enable: (obj.enable != undefined ? obj.enable : true),
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

                    var svr = new AjaxCall(obj.url, (obj.param ? obj.param : ""), true);
                    svr.callService(function (data) {
                        if (data) {
                            if (data.models != undefined || data.models === null) {
                                options.success(data.models);
                            }
                            else if (data.Table != undefined || data.Table === null) {
                                options.success(data.Table);
                            }
                            else {
                                options.success(data);
                            }

                            if (obj.trigger) {
                                $(element).data("kendoDropDownList").trigger(obj.trigger.toLowerCase());
                            }
                        }
                        else {
                            options.success([]);
                        }
                    });

                }
            }
        },
        select: function (e) {
            if (e.item) {
                var dataItem = this.dataItem(e.item.index() + (obj.placeholder ? 1 : 0));

                if (obj.disableLevel && (obj.disableLevel.split(',')).indexOf(dataItem.Level + "") != -1) {
                    e.preventDefault();
                    this.value(null);
                }

                if (obj.disableValueField && obj.disableValueData && (obj.disableValueData.split(',')).indexOf(dataItem[obj.disableValueField] + "") != -1) {
                    e.preventDefault();
                    this.value(null);
                }
            }
        }
    });
}


//------------------------------------------------------------ INIT COMBOBOX --------------------------------------------------

/* Notes: riêng với trường hợp dùng ComboBox, 
nếu sử dụng hàm Change thì phải kèm theo function đoạn "if (!this.dataItem()) { this.value(null); this.text(null)}"

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
    enable : null,
    template: null,
    isLevel: null,              // true: phân cấp cây theo Level(template)
    disableLevel: null          //  level disable       VD: '1,2,...'     
    disableValueField: null,    //  field disable       VD: 'Value'
    disableValueData: null,     //  giá trị disable     VD: '123,456'

    dataSource: null,

    trigger: null       //change, ...
}

InitComboBox(element, obj);

*/

InitComboBox = function (element, obj) {
    if (!obj) obj = {};

    if ($(element).data("kendoComboBox")) {
        $(element).data("kendoComboBox").value(null);
        $(element).data("kendoComboBox").destroy();
    }

    if (obj.isLevel) {
        obj.template = '#if(data.Level){#<div style="'
                    + 'padding-left:#=(parseInt(Level)-1)*20#px;'
                    + '#if(parseInt(data.Level) == 1){# font-weight:bold; #}#'
                    + '#if(("' + (obj.disableLevel ? obj.disableLevel : "") + '").split(",").indexOf(data.Level + "") != -1){# color:rgb(181,181,181); #}#'
                    + '">#=Text#</div>#}#';
    }

    $(element).kendoComboBox({
        dataValueField: (obj.dataValueField ? obj.dataValueField : null),
        dataTextField: (obj.dataTextField ? obj.dataTextField : null),
        value: (obj.value ? obj.value : null),
        placeholder: (obj.placeholder ? obj.placeholder : "Chọn dữ liệu"),
        change: (obj.change ? obj.change : function () { if (!this.dataItem()) { this.value(null); this.text(null) } }),
        template: (obj.template ? obj.template : null),
        noDataTemplate: "Không tìm thấy dữ liệu!",
        enable: (obj.enable != undefined ? obj.enable : true),
        suggest: true,
        filter: "contains",
        valuePrimitive: true,
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

                    var svr = new AjaxCall(obj.url, (obj.param ? obj.param : ""), true);
                    svr.callService(function (data) {
                        data = JSON.parse(data);
                        if (data) {
                            if (data.models != undefined || data.models === null) {
                                options.success(data.models);
                            }
                            else if (data.Table != undefined || data.Table === null) {
                                options.success(data.Table);
                            }
                            else {
                                options.success(data);
                            }

                            if (obj.trigger) {
                                $(element).data("kendoComboBox").trigger(obj.trigger.toLowerCase());
                            }
                        }
                        else {
                            options.success([]);
                        }
                    });
                }
            }
        },
        select: function (e) {
            if (e.item) {
                var dataItem = this.dataItem(e.item.index());

                if (obj.disableLevel && (obj.disableLevel.split(',')).indexOf(dataItem.Level + "") != -1) {
                    e.preventDefault();
                    this.value(null);
                    this.text(null);
                }

                if (obj.disableValueField && obj.disableValueData && (obj.disableValueData.split(',')).indexOf(dataItem[obj.disableValueField] + "") != -1) {
                    e.preventDefault();
                    this.value(null);
                    this.text(null);
                }
            }
        }
    });

    $(element).data("kendoComboBox").input.click(function () {
        $(element).data("kendoComboBox").open();
    });
}


//------------------------------------------------------------ INIT MULTI COMBOBOX --------------------------------------------------
/*

var element = "#TagID";
var obj = {
    url: null,
    param: null,
    dataValueField: null,
    dataTextField: null,
    value: null,            //'11,22,33,44'
    placeholder: null,
    change: null,
    isLevel:null
} 

InitMultiComboBox(element, obj);

*/

InitMultiComboBox = function (element, obj) {
    if (!obj) obj = {};

    if ($(element).data("kendoComboBox")) {
        $(element).data("kendoComboBox").value(null);
        $(element).data("kendoComboBox").destroy();
    }
    var IdRandom = Math.floor(Math.random() * 1000000000);
    if (obj.isLevel) {
        obj.template = '#if(data.Level){#<div style="float:left;padding-left:#=(parseInt(Level)-1)*20#px;"><input type="checkbox" style="margin:0;padding: 0;vertical-align: middle;"'
                + ' id="' + IdRandom + '#=data.' + obj.dataValueField + '#' + '"'
                + ' data-id="#=data.' + obj.dataValueField + '#"'
                + ' data-level="#=data.Level#"'
                + ' data-parent="#=data.Parent#"'
                + ' class="ace ' + '#if(data.ClassControl){##=ClassControl##}else{#' + IdRandom + '#}#' + '"'
                + '/>'
                + '<span class="lbl"></span></div>'
                + '<div style="float:left;width:calc(100% - #=(parseInt(Level)-1)*20 +20#px);"><label for="' + IdRandom + '#=data.' + obj.dataValueField + '#' + '" style="padding-left: 5px;margin:0;display: block;left: 0;top: 0;right: 0;">#=data.' + obj.dataTextField + '#</label></div><div style="clear:both;"></div>#}#';
    }
    $(element).kendoComboBox({
        dataValueField: (obj.dataValueField ? obj.dataValueField : null),
        dataTextField: (obj.dataTextField ? obj.dataTextField : null),
        placeholder: (obj.placeholder ? obj.placeholder : "Chọn dữ liệu"),
        template: (obj.isLevel) ? obj.template : '<div style="float:left"><input type="checkbox" style="margin:0;padding: 0;vertical-align: middle;"'
                + ' id="' + IdRandom + '#=data.' + obj.dataValueField + '#' + '"'
                + ' data-id="#=data.' + obj.dataValueField + '#"'
                + ' class="ace ' + '#if(data.ClassControl){##=ClassControl##}else{#' + IdRandom + '#}#' + '"'
                + '/>'
                + '<span class="lbl"></span></div>'
                + '<div style="float:left;width:calc(100% - 20px);"><label for="' + IdRandom + '#=data.' + obj.dataValueField + '#' + '" style="padding-left: 5px;margin:0;display: block;left: 0;top: 0;right: 0;">#=data.' + obj.dataTextField + '#</label></div><div style="clear:both;"></div>',
        noDataTemplate: "Không tìm thấy dữ liệu!",
        close: function (e) {
            //e.preventDefault();
        },
        dataSource: {
            transport: {
                read: function (options) {
                    if (!obj.url) {
                        options.success([]);
                        return;
                    }

                    var svr = new AjaxCall(obj.url, (obj.param ? obj.param : ""), true);
                    svr.callService(function (data) {
                        if (data) {
                            var dataList = [];

                            if (data.models != undefined || data.models === null) {
                                dataList = data.models;
                            }
                            else if (data.Table != undefined || data.Table === null) {
                                dataList = data.Table;
                            }
                            else {
                                dataList = data;
                            }

                            if (dataList) {
                                var objTemp = {};
                                objTemp[obj.dataValueField] = "-1";
                                objTemp[obj.dataTextField] = "Chọn tất cả";
                                objTemp["Level"] = "1";
                                objTemp["ClassControl"] = "CheckAll" + IdRandom;
                                dataList.splice(0, 0, objTemp);

                                options.success(dataList);
                            }
                            else {
                                options.success([]);
                            }
                        } else {
                            options.success([]);
                        }
                    });

                }
            }
        },
        dataBound: function (e) {
            $(e.sender.ul).children("li").css("position", "relative");

            $(e.sender.ul).children("li").click(function (event) {
                if ($(this).find('input').hasClass('CheckAll' + IdRandom) == false) {
                    if ((event.target.nodeName + "").toUpperCase() != 'INPUT') {
                        event.preventDefault();
                        $(this).find('input').prop('checked', !$(this).find('input').is(":checked"));
                        if (obj.isLevel) CheckTreeCombobox($(this).find('input'), true);
                        SetValueMultiComboBox();
                    }
                }
            });

            if (obj.value) {
                $.each(obj.value.split(','), function (index, value) {
                    $("#" + IdRandom + value).prop('checked', true);
                    if (obj.isLevel) CheckTreeCombobox("#" + IdRandom + value, true);
                });

                SetValueMultiComboBox();
            }

            $(".CheckAll" + IdRandom).click(function () {
                $("input." + IdRandom).prop("checked", $(this).is(":checked"));

                SetValueMultiComboBox();
            });
            $("input." + IdRandom).click(function () {
                if (obj.isLevel) CheckTreeCombobox($(this), true);
                SetValueMultiComboBox();
            });
            //check treelevel
            function CheckTreeCombobox(ele, isParent) {

                if (isParent) {
                    //nếu tất cả thằng con check thì check thằng cha
                    var parid = $(ele).attr('data-parent');
                    var flag = true;
                    $(element + '-list').find('input[data-parent="' + parid + '"]').each(function () {
                        flag = $(this).is(":checked");
                        if (flag == false) {
                            return false;
                        }
                    });
                    $(element + '-list').find('input[data-id="' + parid + '"]').prop('checked', flag);
                }

                //tìm tất cả thằng con check
                var id = $(ele).attr('data-id');
                $(element + '-list').find('input[data-parent="' + id + '"]').each(function () {
                    $(this).prop('checked', $(ele).is(":checked"));
                    CheckTreeCombobox($(this));
                });
            }
            //select
            function SetValueMultiComboBox() {
                var numChecked = $("input." + IdRandom + ":checked").length;
                var numNotCheced = $("input." + IdRandom + ":not(:checked)").length;
                console.log(numNotCheced, numChecked);
                if (numNotCheced == 0) {
                    $(".CheckAll" + IdRandom).prop("checked", true);
                    $(element).data("kendoComboBox").text('Chọn tất cả (' + numChecked + ')');
                }
                else {
                    $(".CheckAll" + IdRandom).prop("checked", false);

                    if (numChecked == 0) {
                        $(element).data("kendoComboBox").text(null);
                    }
                    else if (numChecked == 1) {
                        $(element).data("kendoComboBox").text($("input." + IdRandom + ":checked").parents('li').find('label').text());
                    }
                    else {
                        $(element).data("kendoComboBox").text(numChecked + " mục được chọn");
                    }
                }

                var arrValue = [];
                $("input." + IdRandom + ":checked").each(function () {
                    arrValue.push($(this).attr("data-id"));
                });
                $(element).data("kendoComboBox").multiValue = arrValue.join(",");

                //change
                var ExcuteFunc = (obj.change ? obj.change : function () { });
                ExcuteFunc();
            }
        },
        select: function (e) {
            e.preventDefault();
            //multi ko dùng chính thống được
        },
        change: function (e) {
            //e.preventDefault();

            //multi ko dùng chính thống được
        }
    });

    var objComboBox = $(element).data("kendoComboBox");

    //objComboBox.readonly(true);

    objComboBox.input.click(function () {
        objComboBox.open();
    });

    objComboBox.input.parent().find('*[aria-label="select"]').click(function () {
        objComboBox.open();
    });
}

GetValueMultiComboBox = function (element) {
    if (!$(element).data("kendoComboBox"))
        return "";

    var value = $(element).data("kendoComboBox").multiValue;
    if (value) {
        return value;
    }

    return "";
}

////
// function load multi select
LoadComboBoxMultiSelection = function (element, url, textField, valueField, value, placeholder, valueDelete, funcChange, param) {
    if (funcChange == undefined || funcChange == null || funcChange == "") funcChange = function (e) { e.preventDefault(); };

    var combobox = element.kendoComboBox({
        dataTextField: textField,
        dataValueField: valueField,
        dataSource: {
            transport: {
                read: function (options) {
                    var svr = new AjaxCall(url, param);
                    svr.callService(function (data) {
                        if (data != null && data.length > 0) {
                            if (data.Table != null) {
                                var ds = data.Table;
                                var obj = {};
                                obj[textField] = "Chọn tất cả";
                                obj[valueField] = "-1";
                                ds.splice(0, 0, obj);
                                options.success(ds);
                            } else {
                                var ds = data;
                                var obj = {};
                                obj[textField] = "Chọn tất cả";
                                obj[valueField] = "-1";
                                ds.splice(0, 0, obj);
                                options.success(ds);
                            }

                        } else {
                            options.success("");
                        }
                    })
                }
            }
        },
        template: "<input type='checkbox' id='chk_product_#=data." + valueField + " #' "
            + "value='#=data." + valueField + " #' name='product' class='mulCheckbox' />" + " " + "<span>${ data." + textField + " }</span>",
        dataBound: function (e) {
            function checkMulti(ele) {
                var ds = element.data("kendoComboBox").dataSource.data();
                dsLength = ds.length;
                var elementCheck = $(ele).children("input[type=checkbox]");
                var value = elementCheck.val();
                if (!elementCheck.is(":checked")) {
                    elementCheck.prop('checked', true);
                    if (value == "-1") {
                        for (var i = 1; i < dsLength; i++) {
                            ds[i].Selected = true;
                            //$("#chk_product_" + ds[i][valueField]).prop('checked', true);
                            $($(ele).parent().find("#chk_product_" + ds[i][valueField])).prop('checked', true);
                        }
                    }
                    else {
                        for (var i = 1; i < dsLength; i++) {
                            if (ds[i][valueField] == value) {
                                ds[i].Selected = true;
                            }
                        }
                    }
                }
                else {
                    elementCheck.prop('checked', false);
                    if (value == "-1") {
                        for (var i = 1; i < dsLength; i++) {
                            ds[i].Selected = false;
                            //$("#chk_product_" + ds[i][valueField]).prop('checked', false);
                            $((ele).parent().find("#chk_product_" + ds[i][valueField])).prop('checked', false);
                        }
                    }
                    else {
                        for (var i = 1; i < dsLength; i++) {
                            if (ds[i][valueField] == value) {
                                ds[i].Selected = false;
                            }
                        }
                    }
                }
                if (!CheckSelectAll(ds, textField, combobox))
                    $(ele).parent("ul").children("li").first().children("input[type=checkbox]").first().prop('checked', false);
                else {
                    $(ele).parent("ul").children("li").first().children("input[type=checkbox]").first().prop('checked', true);
                    combobox.text("Chọn tất cả");
                }
            }

            $(e.sender.ul).children("li").click(function (e) {
                e.preventDefault();
                checkMulti(this);
            });
            if (value != undefined) {
                var ds = combobox.dataSource.data(), dsLength = ds.length;
                var vaSplit = value.split(','), vaSplitLength = vaSplit.length;
                for (var i = 0; i < vaSplitLength; i++) {
                    for (var j = 0; j < dsLength; j++) {
                        if (vaSplit[i] == ds[j][valueField]) {
                            ds[j].Selected = true;
                            $("#chk_product_" + ds[j][valueField]).prop('checked', true);
                        }
                    }
                }
                if (CheckSelectAll(ds, textField, combobox)) {
                    $(e.sender.ul).children("li").first().children("input[type=checkbox]").prop('checked', true);
                    combobox.text("Chọn tất cả");
                }
            } else {
                combobox.text("");
            }

            $(e.sender.ul).children("li").find(".mulCheckbox").click(function (fe) {
                fe.stopPropagation();
                if ($(this).is(":checked"))
                    $(this).prop('checked', false);
                else
                    $(this).prop('checked', true);
                checkMulti($(this).parent("li"));
            });
        },
        autobind: true,
        change: funcChange,
        select: function (e) {
            e.preventDefault();
        }
    }).data("kendoComboBox");
    combobox.input.click(function () {
        combobox.open();
    });
    if (placeholder != undefined)
        combobox.input.attr("placeholder", placeholder);
}

// function check all checkbox
function CheckSelectAll(data, textField, combobox) {
    var dataLength = data.length;
    var bool = true;
    var text = [];
    for (var i = 1; i < dataLength; i++) {
        if (data[i].Selected == undefined || data[i].Selected == false)
            bool = false;
        else
            text.push(data[i][textField]);
    }
    combobox.text(text.join(','));
    return bool;
}

// function GetDataComboBoxMultiSelection
GetDataComboBoxMultiSelection = function (element, valueField) {
    ctrComboBox = element.data("kendoComboBox");
    var value = [];
    if (ctrComboBox != undefined) {
        var dataItem = ctrComboBox.dataSource.data(),
            dataItemLength = dataItem.length;
        for (var i = 0; i < dataItemLength; i++) {
            if (dataItem[i].Selected)
                value.push(dataItem[i][valueField]);
        }
        return value.join(',')
    }
    else
        return null;
};

// LoadComboBoxMultiSelectionChangeDataSource
LoadComboBoxMultiSelectionChangeDataSource = function (element, dataSource, textField, valueField) {
    if (dataSource.length > 0) {
        var obj = {};
        obj[textField] = "Chọn tất cả";
        obj[valueField] = "-1";
        dataSource.splice(0, 0, obj);
        element.dataSource.data(dataSource);
        if (CheckSelectAll(dataSource, textField, element)) {
            $(e.sender.ul).children("li").first().children("input[type=checkbox]").prop('checked', true);
            combobox.text("Chọn tất cả");
        }
    }
    else {
        element.dataSource.data(dataSource);
    }
}

//------------------------------------------------------------ INIT WINDOW --------------------------------------------------
/*

    var obj = {
        url: null,
        param: null,
        content: null,         // null
        width: null,           // 600
        height: null,          // 90%
        action: null,          // ["Maximize", "Close"]         // ["Pin", "Minimize", "Maximize", "Close"]
        isMathJax: null        //false                          //true: sử dụng Mathjax
    }
*/

InitWindow = function (obj) {
    if (!obj) { obj = {} };

    var id = "k-window-custom";
    var element = "#" + id;
    if ($(element).length > 0) {
        $(element).remove();
    }

    $("body").append('<div id="' + id + '" class="k-window-custom" style="padding: 20px 30px;"></div>');

    $(element).kendoWindow({
        width: (obj.width ? obj.width : 600),
        height: (obj.height ? obj.height : "90%"),
        title: (obj.title ? obj.title : "Thông tin chi tiết"),
        visible: (obj.visible ? obj.visible : false),
        actions: (obj.action ? obj.action : ["Maximize", "Close"]),
        open: function () {
            $(element).parents(".k-window").css('max-width', '100%');
        }

    })
    .data("kendoWindow").center().open();

    if (obj.content) {
        $(element).data("kendoWindow").content(obj.content);

        if (obj.isMathJax)
            MathJax.Hub.Queue(["Typeset", MathJax.Hub, '.k-window-custom']);
        return;
    }

    if (!obj.url) {
        $(element).data("kendoWindow").content("");
        return;
    }

    $.ajax({
        url: _RootBase + obj.url,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: (obj.param ? obj.param : ""),
        beforeSend: function () {
            $(element).data("kendoWindow").content('<div class="loadwindow wt-waiting wt-absolute wt-medium"></div>');
        },
        success: function (data) {
            if (data) {
                if (data.models != undefined || data.models === null) {
                    $(element).data("kendoWindow").content(data.models);
                }
                else if (data.Table != undefined || data.Table === null) {
                    $(element).data("kendoWindow").content(data.Table);
                }
                else {
                    $(element).data("kendoWindow").content(data);
                }

                if (obj.isMathJax)
                    MathJax.Hub.Queue(["Typeset", MathJax.Hub, '.k-window-custom']);
            }
            else {
                $(element).data("kendoWindow").content("Không lấy được dữ liệu");
            }
        },
        error: function (e) {
            $(element).data("kendoWindow").content("Không lấy được dữ liệu");
        },
        complete: function (e) {
            $(".loadwindow").remove();
        }
    });
}



//------------------------------------------------------------- INIT DIALOG --------------------------------------------------


/*
    var obj = {
        width: null,            //auto
        height: null,           //auto
        closable: null,         //true                          //false: ẩn nút 'x' close góc phải trên
        modal: null,            //true                          //true: dạng popup che màn hình, false: dạng thông báo
        title: null,            //'Thông báo'                   //nội dung tiêu đề (false: ko hiện title)
        content : null,         //'Không có nội dung hiển thị'  //nội dung thông báo

        actions: [
            { text: 'Đồng ý', primary: true, action: function (e) { alert(e) } },
            { text: 'Đóng', action: function () { return true; } },
            { text: 'Không đóng', action: function () { return false; } }
        ],

        isMathJax: null         //false                         //true: sử dụng Mathjax
    }


*/

InitDialog = function (obj) {
    if (!obj) { obj = {} };
    var id = "k-dialog-custom";
    var element = "#" + id;
    if ($(element).length > 0) {
        $(element).remove();
    }
    $("body").append('<div id="' + id + '" class="k-dialog-custom"></div>');

    $(element).kendoDialog({
        minWidth: 300,
        maxWidth: ($(window).width() * 0.9),
        minHeight: 200,
        maxHeight: ($(window).height() * 0.9),
        width: (obj.width ? obj.width : 'auto'),
        height: (obj.height ? obj.height : 'auto'),

        closable: (obj.closable || obj.closable === false ? obj.closable : true),
        modal: (obj.modal || obj.modal === false ? obj.modal : true),

        title: (obj.title || obj.title === false ? obj.title : "Thông báo"),
        content: (obj.content ? obj.content : "Không có nội dung hiển thị"),
        actions: (obj.actions ? obj.actions : false),
        buttonLayout: "normal"
    });

    $(element).css({
        'overflow': 'auto'
    });

    if (obj.isMathJax)
        MathJax.Hub.Queue(["Typeset", MathJax.Hub, '.k-dialog-custom']);
}
//



//------------------------------------------------------------- INIT EDITOR --------------------------------------------------
/*
    var obj = {
        height:null,
        value : null,
        placeholder: null,
        tools: null,
        
        isFile : null,          // default: false
        isEquation : null,      // default: false
    }
    
    var element = "#Editor"
*/

InitEditor = function (element, obj) {
    if (!obj) {
        obj = {};
    }

    var domain = _RootBase;

    var tools = (obj.tools ?
        obj.tools :
        [
            { name: "insertParagraph", shift: true },   // giữ Shift để có khoảng trống Enter rộng hơn  //xuống dòng bằng chèn thẻ <p></p>.
            { name: "insertLineBreak", shift: false },  // ngược lại                                    //dùng <br/>
            {
                name: "fontName",
                items: [{ text: "Segoe UI", value: "Segoe UI" }, { text: "Segoe UI Light", value: "Segoe UI Light" }]
            },
            "fontSize",
            //"foreColor",
            //"backColor",
            "bold",
            "italic",
            "underline",
            //"strikethrough",
            "justifyLeft",
            "justifyCenter",
            "justifyRight",
            //"justifyFull",
            "insertUnorderedList",
            "insertOrderedList",
            "indent",
            "outdent",
            "createLink",
            "unlink",
            //"subscript",
            //"superscript",
            //"createTable",
            //"addRowAbove",
            //"addRowBelow",
            //"addColumnLeft",
            //"addColumnRight",
            //"deleteRow",
            //"deleteColumn",
            //"viewHtml",
            //"formatting",
            "cleanFormatting",
            //"print",
            //"pdf",
            "insertImage"
        ]);

    if (obj.isFile) {
        tools.push("insertFile");
    }

    if (obj.isEquation) {
        tools.push({
            name: "Equation",
            tooltip: "Công thức toán học",//"Equation Editor",
            exec: function (e) {
                var editor = $(this).data("kendoEditor");

                InitDialog({
                    width: ($(window).width() * 0.8),
                    height: ($(window).height() * 0.8),
                    modal: true,
                    title: "Công thức | Ký hiệu toán học",
                    content: "<iframe id='iKendoEditor' src='" + domain + "MathJax/Default.aspx' style='border:unset; padding-top: 0px; width: 100%; height: 99%;'></iframe>",
                    actions: [
                        {
                            text: 'Chèn vào', primary: true, action: function (e) {
                                var userinput = "$" + $("#iKendoEditor").contents().find("#result").val() + "$";
                                editor.exec("inserthtml", { value: userinput });
                                $(editor.body).parent().find('.placeholderkd').hide();


                            }
                        },
                        {
                            text: 'Hủy', action: function () {
                                return true;
                            }
                        }
                    ]
                });
            }
        });
    }

    tools.push({
        name: "Preview",
        tooltip: "Xem trước dữ liệu",
        exec: function (e) {
            var editor = $(this).data("kendoEditor");

            InitDialog({
                width: ($(window).width() * 0.8),
                height: ($(window).height() * 0.8),
                modal: true,
                title: "Xem trước dữ liệu",
                content: editor.value(),
                actions: [
                    {
                        text: 'Đóng', action: function () {
                            return true;
                        }
                    }
                ],
                isMathJax: true
            });
        }
    });

    var FileUrl = "";
    $(element).kendoEditor({
        value: (obj.value ? obj.value : null),
        encoded: false,  //value = <p>foo</p>   =>   true -> &lt;p&gt;foo&lt;/p&gt;   ||   false   ->   <p>foo</p>
        resizable: {
            toolbar: true,
            content: true,
            min: 100,
            max: 1000
        },
        tools: tools,
        value: (obj.value ? obj.value : ""),
        imageBrowser: {
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: domain + "ImageBrowser/Read",
                        data: JSON.stringify({
                            path: options.data.path
                        }),
                        success: function (e) {
                            if (e && e.length > 0) {
                                FileUrl = e[0].FileUrl;
                                options.success(e);
                            }
                            else
                                options.success([]);
                        },
                        error: function (e) {
                            options.success([]);
                        }
                    });
                },
                destroy: {
                    url: domain + "ImageBrowser/Destroy",
                    type: "POST"
                },
                create: {
                    url: domain + "ImageBrowser/Create",
                    type: "POST"
                },
                thumbnailUrl: domain + "ImageBrowser/Thumbnail",
                uploadUrl: domain + "ImageBrowser/Upload",
                imageUrl: function (fileName) {
                    return FileUrl + fileName;
                }
            }
        },
        fileBrowser: {
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: domain + "FileBrowser/Read",
                        data: JSON.stringify({
                            path: options.data.path
                        }),
                        success: function (e) {
                            if (e && e.length > 0) {
                                FileUrl = e[0].FileUrl;
                                options.success(e);
                            }
                            else
                                options.success([]);
                        },
                        error: function (e) {
                            options.success([]);
                        }
                    });
                },
                destroy: {
                    url: domain + "FileBrowser/Destroy",
                    type: "POST"
                },
                create: {
                    url: domain + "FileBrowser/Create",
                    type: "POST"
                },
                uploadUrl: domain + "FileBrowser/Upload",
                fileUrl: function (fileName) {
                    return FileUrl + fileName;
                }
            }
        }
    });

    if (obj.placeholder) {
        var editor = $(element).data('kendoEditor');
        var $this = $(editor.body).parent().find(".placeholderkd");
        if ($this.length == 0) {
            $this = $('<div class="placeholderkd" style="display:none; position:absolute;position:absolute;top:5px;left:5px;color:#aaa;font-size: 14px; font-family: Segoe UI">' + obj.placeholder + '</div>')
                .appendTo($(editor.body).parent())
                .click(function () {
                    $(element).data('kendoEditor').focus();
                });
        }

        if (editor.value().trim() == "") {
            $this.show();
        }
        else {
            $this.hide();
        }

        editor.bind("keyup", function () {
            if (editor.value().trim() == "") {
                $this.show();
            }
            else {
                $this.hide();
            }
        });

        editor.bind("change", function () {
            if (editor.value().trim() == "") {
                $this.show();
            }
            else {
                $this.hide();
            }
        });
    }

    if (obj.height) {
        $(element).parents(".k-editor-widget").css({
            height: obj.height
        });
    }
}

InitDropdownBoxMathjax = function () {


    var html = '<div id="drd-boxsearch hidden">{0}</div>';
    html = html.replace('{0}', 'dtcuong');

    var drdboxsearch = $('#drd-boxsearch');

    if ($('#drd-boxsearch').length > 0)
        $('#drd-boxsearch').remove();

    var ele = $('#btn-dropdown-box');

    drdboxsearch.css({
        position: 'absolute',
        left: 0,
        top: 0
    });

    $('body').append(html);


    return html;
}


//------------------------------------------------------------- INIT NOTIFICATION --------------------------------------------------
/*
    var obj = {
        width: null                 //300
        autoHideAfter: null,        //3000
        hide : null,                //function (e) { }
        show : null                 //function (e) { }
    }
    
    InitNotification(element, obj);
*/

InitNotification = function (obj) {
    if (!obj) { obj = {}; }

    var idRandom = Math.floor(Math.random() * 1000000000);
    $("body").append('<span id="' + idRandom + '"></span>');
    var element = "#" + idRandom;

    return $(element).kendoNotification({
        width: (obj.width ? obj.width : 300),
        position: {
            pinned: true,
            top: 45,
            right: 10
        },
        autoHideAfter: (obj.autoHideAfter ? obj.autoHideAfter : 5000),
        stacking: "down",
        templates: [
            {
                type: "warning",
                template: '<div class="warning"><img src="' + _RootBase + 'Content/themes/admin/icon/notify/error-icon.png' + '" /><h3>#= title #</h3><p>#= message #</p></div>'
            },
            {
                type: "info",
                template: '<div class="info"><img src="' + _RootBase + 'Content/themes/admin/icon/notify/success-icon.png' + '" /><h3>#= title #</h3><p>#= message #</p></div>'
            },
            {
                type: "error",
                template: '<div class="wrong-pass"><img src="' + _RootBase + 'Content/themes/admin/icon/notify/error-icon.png' + '" /><h3>#= title #</h3><p>#= message #</p></div>'
            },
            {
                type: "success",
                template: '<div class="success"><img src="' + _RootBase + 'Content/themes/admin/icon/notify/success-icon.png' + '" /><h3>#= title #</h3><p>#= message #</p></div>'
            },
            {
                type: "upload-success",
                template: '<div class="upload-success"><img src="' + _RootBase + 'Content/themes/admin/icon/notify/success-icon.png' + '" /><h3>#= title #</h3><p>#= message #</p></div>'
            },
            {
                type: "new-mail",
                template: '<div class="new-mail"><img src="' + _RootBase + 'Content/themes/admin/icon/notify/envelope.png' + '" /><h3>#= title #</h3><p>#= message #</p></div>'
            }
        ],
        hide: (obj.hide ? obj.hide : function (e) { }),
        show: (obj.show ? obj.show : function (e) { })
    }).data("kendoNotification");
}