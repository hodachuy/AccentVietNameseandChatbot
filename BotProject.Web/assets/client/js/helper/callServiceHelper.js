var block = $('body');

var AjaxCall = function (servicePathName, serviceParams, serviceAsync) {
    this._servicePathName = servicePathName;
    this._serviceParams = serviceParams;
    this._serviceAsync = (serviceAsync == undefined ? false : serviceAsync);
    return this;
};

AjaxCall.prototype = {
    callServicePOST: function (serviceCallSuccess) {
        var root = _Host;
        $.ajax({
            type: 'POST',
            url: root + this._servicePathName,
            data: this._serviceParams,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            //async: this._serviceAsync,
            success: serviceCallSuccess,
            error: function (error) {
                $(block).unblock();
                console.log(error.responseJSON)
            },
            beforeSend: function () {
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
            }
        });
    },
    callServicePUT: function (serviceCallSuccess) {
        var root = _Host;
        $.ajax({
            type: 'PUT',
            url: root + this._servicePathName,
            data: this._serviceParams,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            //async: this._serviceAsync,
            success: serviceCallSuccess,
            error: function (error) {
                $(block).unblock();
                console.log(error)
            },
            beforeSend: function () {
                $(block).block({
                    message: '<i class="icon-spinner4 fa fa-spinner fa-pulse spinner"></i>',
                    overlayCSS: {
                        backgroundColor: '#efeff6',
                        opacity: 0.8,
                        cursor: 'wait',
                        zIndex: 9999999999999
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
            }
        });
    },
    callServiceGET: function (serviceCallSuccess) {
        var root = _Host;
        $.ajax({
            type: 'GET',
            url: root + this._servicePathName,
            cache:false,
            data: this._serviceParams,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            //async: this._serviceAsync,
            success: serviceCallSuccess,
            error: function (error) {
                $(block).unblock();
                console.log(error.responseJSON)
            },
            beforeSend: function () {
                $(block).block({
                    message: '<i class="icon-spinner4 fa fa-spinner fa-pulse spinner"></i>',
                    overlayCSS: {
                        backgroundColor: '#efeff6',
                        opacity: 0.8,
                        cursor: 'wait',
                        zIndex: 9999999999999
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
            }
        });
    },
    callServiceDELETE: function (serviceCallSuccess) {
        var root = _Host;
        $.ajax({
            type: 'DELETE',
            url: root + this._servicePathName,
            data: this._serviceParams,
            contentType: "application/json; charset=utf-8",
            //dataType: "json",
            //async: this._serviceAsync,
            success: serviceCallSuccess,
            error: function (error) {
                $(block).unblock();
                console.log(error)
            },
            beforeSend: function () {
                $(block).block({
                    message: '<i class="icon-spinner4 fa fa-spinner fa-pulse spinner"></i>',
                    overlayCSS: {
                        backgroundColor: '#efeff6',
                        opacity: 0.8,
                        cursor: 'wait',
                        zIndex: 9999999999999
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
            }
        });
    }
}