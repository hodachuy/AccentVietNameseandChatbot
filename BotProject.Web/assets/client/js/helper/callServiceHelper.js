var AjaxCall = function (servicePathName, serviceParams, typeMethode, serviceAsync) {
    this._servicePathName = servicePathName;
    this._serviceParams = serviceParams;
    this._typeMethod = typeMethode;
    this._serviceAsync = (serviceAsync == undefined ? false : serviceAsync);
    return this;
};

AjaxCall.prototype = {
    callService: function (serviceCallSuccess) {
        var root = _Host;
        $.ajax({
            type: this._serviceParams,
            url: root + this._servicePathName,
            data: this._serviceParams,
            contentType: "application/json; charset=utf-8",
            //dataType: "json",
            //async: this._serviceAsync,
            success: serviceCallSuccess,
            error: function (e) {
                $(block).unblock();
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