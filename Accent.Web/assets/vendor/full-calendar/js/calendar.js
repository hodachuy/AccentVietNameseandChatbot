/* ========================================================================
 * bootstrap-switch - v3.3.2
 * http://www.bootstrap-switch.org
 * ========================================================================
 * Copyright 2012-2013 Mattia Larentis
 *
 * ========================================================================
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * ========================================================================
 */

(function () { var t = [].slice; !function (e, i) { "use strict"; var n; return n = function () { function t(t, i) { null == i && (i = {}), this.$element = e(t), this.options = e.extend({}, e.fn.bootstrapSwitch.defaults, { state: this.$element.is(":checked"), size: this.$element.data("size"), animate: this.$element.data("animate"), disabled: this.$element.is(":disabled"), readonly: this.$element.is("[readonly]"), indeterminate: this.$element.data("indeterminate"), inverse: this.$element.data("inverse"), radioAllOff: this.$element.data("radio-all-off"), onColor: this.$element.data("on-color"), offColor: this.$element.data("off-color"), onText: this.$element.data("on-text"), offText: this.$element.data("off-text"), labelText: this.$element.data("label-text"), handleWidth: this.$element.data("handle-width"), labelWidth: this.$element.data("label-width"), baseClass: this.$element.data("base-class"), wrapperClass: this.$element.data("wrapper-class") }, i), this.prevOptions = {}, this.$wrapper = e("<div>", { "class": function (t) { return function () { var e; return e = ["" + t.options.baseClass].concat(t._getClasses(t.options.wrapperClass)), e.push(t.options.state ? t.options.baseClass + "-on" : t.options.baseClass + "-off"), null != t.options.size && e.push(t.options.baseClass + "-" + t.options.size), t.options.disabled && e.push(t.options.baseClass + "-disabled"), t.options.readonly && e.push(t.options.baseClass + "-readonly"), t.options.indeterminate && e.push(t.options.baseClass + "-indeterminate"), t.options.inverse && e.push(t.options.baseClass + "-inverse"), t.$element.attr("id") && e.push(t.options.baseClass + "-id-" + t.$element.attr("id")), e.join(" ") } }(this)() }), this.$container = e("<div>", { "class": this.options.baseClass + "-container" }), this.$on = e("<span>", { html: this.options.onText, "class": this.options.baseClass + "-handle-on " + this.options.baseClass + "-" + this.options.onColor }), this.$off = e("<span>", { html: this.options.offText, "class": this.options.baseClass + "-handle-off " + this.options.baseClass + "-" + this.options.offColor }), this.$label = e("<span>", { html: this.options.labelText, "class": this.options.baseClass + "-label" }), this.$element.on("init.bootstrapSwitch", function (e) { return function () { return e.options.onInit.apply(t, arguments) } }(this)), this.$element.on("switchChange.bootstrapSwitch", function (i) { return function (n) { return !1 === i.options.onSwitchChange.apply(t, arguments) ? i.$element.is(":radio") ? e("[name='" + i.$element.attr("name") + "']").trigger("previousState.bootstrapSwitch", !0) : i.$element.trigger("previousState.bootstrapSwitch", !0) : void 0 } }(this)), this.$container = this.$element.wrap(this.$container).parent(), this.$wrapper = this.$container.wrap(this.$wrapper).parent(), this.$element.before(this.options.inverse ? this.$off : this.$on).before(this.$label).before(this.options.inverse ? this.$on : this.$off), this.options.indeterminate && this.$element.prop("indeterminate", !0), this._init(), this._elementHandlers(), this._handleHandlers(), this._labelHandlers(), this._formHandler(), this._externalLabelHandler(), this.$element.trigger("init.bootstrapSwitch", this.options.state) } return t.prototype._constructor = t, t.prototype.setPrevOptions = function () { return this.prevOptions = e.extend(!0, {}, this.options) }, t.prototype.state = function (t, i) { return "undefined" == typeof t ? this.options.state : this.options.disabled || this.options.readonly ? this.$element : this.options.state && !this.options.radioAllOff && this.$element.is(":radio") ? this.$element : (this.$element.is(":radio") ? e("[name='" + this.$element.attr("name") + "']").trigger("setPreviousOptions.bootstrapSwitch") : this.$element.trigger("setPreviousOptions.bootstrapSwitch"), this.options.indeterminate && this.indeterminate(!1), t = !!t, this.$element.prop("checked", t).trigger("change.bootstrapSwitch", i), this.$element) }, t.prototype.toggleState = function (t) { return this.options.disabled || this.options.readonly ? this.$element : this.options.indeterminate ? (this.indeterminate(!1), this.state(!0)) : this.$element.prop("checked", !this.options.state).trigger("change.bootstrapSwitch", t) }, t.prototype.size = function (t) { return "undefined" == typeof t ? this.options.size : (null != this.options.size && this.$wrapper.removeClass(this.options.baseClass + "-" + this.options.size), t && this.$wrapper.addClass(this.options.baseClass + "-" + t), this._width(), this._containerPosition(), this.options.size = t, this.$element) }, t.prototype.animate = function (t) { return "undefined" == typeof t ? this.options.animate : (t = !!t, t === this.options.animate ? this.$element : this.toggleAnimate()) }, t.prototype.toggleAnimate = function () { return this.options.animate = !this.options.animate, this.$wrapper.toggleClass(this.options.baseClass + "-animate"), this.$element }, t.prototype.disabled = function (t) { return "undefined" == typeof t ? this.options.disabled : (t = !!t, t === this.options.disabled ? this.$element : this.toggleDisabled()) }, t.prototype.toggleDisabled = function () { return this.options.disabled = !this.options.disabled, this.$element.prop("disabled", this.options.disabled), this.$wrapper.toggleClass(this.options.baseClass + "-disabled"), this.$element }, t.prototype.readonly = function (t) { return "undefined" == typeof t ? this.options.readonly : (t = !!t, t === this.options.readonly ? this.$element : this.toggleReadonly()) }, t.prototype.toggleReadonly = function () { return this.options.readonly = !this.options.readonly, this.$element.prop("readonly", this.options.readonly), this.$wrapper.toggleClass(this.options.baseClass + "-readonly"), this.$element }, t.prototype.indeterminate = function (t) { return "undefined" == typeof t ? this.options.indeterminate : (t = !!t, t === this.options.indeterminate ? this.$element : this.toggleIndeterminate()) }, t.prototype.toggleIndeterminate = function () { return this.options.indeterminate = !this.options.indeterminate, this.$element.prop("indeterminate", this.options.indeterminate), this.$wrapper.toggleClass(this.options.baseClass + "-indeterminate"), this._containerPosition(), this.$element }, t.prototype.inverse = function (t) { return "undefined" == typeof t ? this.options.inverse : (t = !!t, t === this.options.inverse ? this.$element : this.toggleInverse()) }, t.prototype.toggleInverse = function () { var t, e; return this.$wrapper.toggleClass(this.options.baseClass + "-inverse"), e = this.$on.clone(!0), t = this.$off.clone(!0), this.$on.replaceWith(t), this.$off.replaceWith(e), this.$on = t, this.$off = e, this.options.inverse = !this.options.inverse, this.$element }, t.prototype.onColor = function (t) { var e; return e = this.options.onColor, "undefined" == typeof t ? e : (null != e && this.$on.removeClass(this.options.baseClass + "-" + e), this.$on.addClass(this.options.baseClass + "-" + t), this.options.onColor = t, this.$element) }, t.prototype.offColor = function (t) { var e; return e = this.options.offColor, "undefined" == typeof t ? e : (null != e && this.$off.removeClass(this.options.baseClass + "-" + e), this.$off.addClass(this.options.baseClass + "-" + t), this.options.offColor = t, this.$element) }, t.prototype.onText = function (t) { return "undefined" == typeof t ? this.options.onText : (this.$on.html(t), this._width(), this._containerPosition(), this.options.onText = t, this.$element) }, t.prototype.offText = function (t) { return "undefined" == typeof t ? this.options.offText : (this.$off.html(t), this._width(), this._containerPosition(), this.options.offText = t, this.$element) }, t.prototype.labelText = function (t) { return "undefined" == typeof t ? this.options.labelText : (this.$label.html(t), this._width(), this.options.labelText = t, this.$element) }, t.prototype.handleWidth = function (t) { return "undefined" == typeof t ? this.options.handleWidth : (this.options.handleWidth = t, this._width(), this._containerPosition(), this.$element) }, t.prototype.labelWidth = function (t) { return "undefined" == typeof t ? this.options.labelWidth : (this.options.labelWidth = t, this._width(), this._containerPosition(), this.$element) }, t.prototype.baseClass = function (t) { return this.options.baseClass }, t.prototype.wrapperClass = function (t) { return "undefined" == typeof t ? this.options.wrapperClass : (t || (t = e.fn.bootstrapSwitch.defaults.wrapperClass), this.$wrapper.removeClass(this._getClasses(this.options.wrapperClass).join(" ")), this.$wrapper.addClass(this._getClasses(t).join(" ")), this.options.wrapperClass = t, this.$element) }, t.prototype.radioAllOff = function (t) { return "undefined" == typeof t ? this.options.radioAllOff : (t = !!t, t === this.options.radioAllOff ? this.$element : (this.options.radioAllOff = t, this.$element)) }, t.prototype.onInit = function (t) { return "undefined" == typeof t ? this.options.onInit : (t || (t = e.fn.bootstrapSwitch.defaults.onInit), this.options.onInit = t, this.$element) }, t.prototype.onSwitchChange = function (t) { return "undefined" == typeof t ? this.options.onSwitchChange : (t || (t = e.fn.bootstrapSwitch.defaults.onSwitchChange), this.options.onSwitchChange = t, this.$element) }, t.prototype.destroy = function () { var t; return t = this.$element.closest("form"), t.length && t.off("reset.bootstrapSwitch").removeData("bootstrap-switch"), this.$container.children().not(this.$element).remove(), this.$element.unwrap().unwrap().off(".bootstrapSwitch").removeData("bootstrap-switch"), this.$element }, t.prototype._width = function () { var t, e; return t = this.$on.add(this.$off), t.add(this.$label).css("width", ""), e = "auto" === this.options.handleWidth ? Math.max(this.$on.width(), this.$off.width()) : this.options.handleWidth, t.width(e), this.$label.width(function (t) { return function (i, n) { return "auto" !== t.options.labelWidth ? t.options.labelWidth : e > n ? e : n } }(this)), this._handleWidth = this.$on.outerWidth(), this._labelWidth = this.$label.outerWidth(), this.$container.width(2 * this._handleWidth + this._labelWidth), this.$wrapper.width(this._handleWidth + this._labelWidth) }, t.prototype._containerPosition = function (t, e) { return null == t && (t = this.options.state), this.$container.css("margin-left", function (e) { return function () { var i; return i = [0, "-" + e._handleWidth + "px"], e.options.indeterminate ? "-" + e._handleWidth / 2 + "px" : t ? e.options.inverse ? i[1] : i[0] : e.options.inverse ? i[0] : i[1] } }(this)), e ? setTimeout(function () { return e() }, 50) : void 0 }, t.prototype._init = function () { var t, e; return t = function (t) { return function () { return t.setPrevOptions(), t._width(), t._containerPosition(null, function () { return t.options.animate ? t.$wrapper.addClass(t.options.baseClass + "-animate") : void 0 }) } }(this), this.$wrapper.is(":visible") ? t() : e = i.setInterval(function (n) { return function () { return n.$wrapper.is(":visible") ? (t(), i.clearInterval(e)) : void 0 } }(this), 50) }, t.prototype._elementHandlers = function () { return this.$element.on({ "setPreviousOptions.bootstrapSwitch": function (t) { return function (e) { return t.setPrevOptions() } }(this), "previousState.bootstrapSwitch": function (t) { return function (e) { return t.options = t.prevOptions, t.options.indeterminate && t.$wrapper.addClass(t.options.baseClass + "-indeterminate"), t.$element.prop("checked", t.options.state).trigger("change.bootstrapSwitch", !0) } }(this), "change.bootstrapSwitch": function (t) { return function (i, n) { var o; return i.preventDefault(), i.stopImmediatePropagation(), o = t.$element.is(":checked"), t._containerPosition(o), o !== t.options.state ? (t.options.state = o, t.$wrapper.toggleClass(t.options.baseClass + "-off").toggleClass(t.options.baseClass + "-on"), n ? void 0 : (t.$element.is(":radio") && e("[name='" + t.$element.attr("name") + "']").not(t.$element).prop("checked", !1).trigger("change.bootstrapSwitch", !0), t.$element.trigger("switchChange.bootstrapSwitch", [o]))) : void 0 } }(this), "focus.bootstrapSwitch": function (t) { return function (e) { return e.preventDefault(), t.$wrapper.addClass(t.options.baseClass + "-focused") } }(this), "blur.bootstrapSwitch": function (t) { return function (e) { return e.preventDefault(), t.$wrapper.removeClass(t.options.baseClass + "-focused") } }(this), "keydown.bootstrapSwitch": function (t) { return function (e) { if (e.which && !t.options.disabled && !t.options.readonly) switch (e.which) { case 37: return e.preventDefault(), e.stopImmediatePropagation(), t.state(!1); case 39: return e.preventDefault(), e.stopImmediatePropagation(), t.state(!0) } } }(this) }) }, t.prototype._handleHandlers = function () { return this.$on.on("click.bootstrapSwitch", function (t) { return function (e) { return e.preventDefault(), e.stopPropagation(), t.state(!1), t.$element.trigger("focus.bootstrapSwitch") } }(this)), this.$off.on("click.bootstrapSwitch", function (t) { return function (e) { return e.preventDefault(), e.stopPropagation(), t.state(!0), t.$element.trigger("focus.bootstrapSwitch") } }(this)) }, t.prototype._labelHandlers = function () { return this.$label.on({ click: function (t) { return t.stopPropagation() }, "mousedown.bootstrapSwitch touchstart.bootstrapSwitch": function (t) { return function (e) { return t._dragStart || t.options.disabled || t.options.readonly ? void 0 : (e.preventDefault(), e.stopPropagation(), t._dragStart = (e.pageX || e.originalEvent.touches[0].pageX) - parseInt(t.$container.css("margin-left"), 10), t.options.animate && t.$wrapper.removeClass(t.options.baseClass + "-animate"), t.$element.trigger("focus.bootstrapSwitch")) } }(this), "mousemove.bootstrapSwitch touchmove.bootstrapSwitch": function (t) { return function (e) { var i; if (null != t._dragStart && (e.preventDefault(), i = (e.pageX || e.originalEvent.touches[0].pageX) - t._dragStart, !(i < -t._handleWidth || i > 0))) return t._dragEnd = i, t.$container.css("margin-left", t._dragEnd + "px") } }(this), "mouseup.bootstrapSwitch touchend.bootstrapSwitch": function (t) { return function (e) { var i; if (t._dragStart) return e.preventDefault(), t.options.animate && t.$wrapper.addClass(t.options.baseClass + "-animate"), t._dragEnd ? (i = t._dragEnd > -(t._handleWidth / 2), t._dragEnd = !1, t.state(t.options.inverse ? !i : i)) : t.state(!t.options.state), t._dragStart = !1 } }(this), "mouseleave.bootstrapSwitch": function (t) { return function (e) { return t.$label.trigger("mouseup.bootstrapSwitch") } }(this) }) }, t.prototype._externalLabelHandler = function () { var t; return t = this.$element.closest("label"), t.on("click", function (e) { return function (i) { return i.preventDefault(), i.stopImmediatePropagation(), i.target === t[0] ? e.toggleState() : void 0 } }(this)) }, t.prototype._formHandler = function () { var t; return t = this.$element.closest("form"), t.data("bootstrap-switch") ? void 0 : t.on("reset.bootstrapSwitch", function () { return i.setTimeout(function () { return t.find("input").filter(function () { return e(this).data("bootstrap-switch") }).each(function () { return e(this).bootstrapSwitch("state", this.checked) }) }, 1) }).data("bootstrap-switch", !0) }, t.prototype._getClasses = function (t) { var i, n, o, s; if (!e.isArray(t)) return [this.options.baseClass + "-" + t]; for (n = [], o = 0, s = t.length; s > o; o++) i = t[o], n.push(this.options.baseClass + "-" + i); return n }, t }(), e.fn.bootstrapSwitch = function () { var i, o, s; return o = arguments[0], i = 2 <= arguments.length ? t.call(arguments, 1) : [], s = this, this.each(function () { var t, a; return t = e(this), a = t.data("bootstrap-switch"), a || t.data("bootstrap-switch", a = new n(this, o)), "string" == typeof o ? s = a[o].apply(a, i) : void 0 }), s }, e.fn.bootstrapSwitch.Constructor = n, e.fn.bootstrapSwitch.defaults = { state: !0, size: null, animate: !0, disabled: !1, readonly: !1, indeterminate: !1, inverse: !1, radioAllOff: !1, onColor: "primary", offColor: "default", onText: "ON", offText: "OFF", labelText: "&nbsp;", handleWidth: "auto", labelWidth: "auto", baseClass: "bootstrap-switch", wrapperClass: "wrapper", onInit: function () { }, onSwitchChange: function () { } } }(window.jQuery, window) }).call(this);
/*!
 * bootstrap-select v1.4.3
 * http://silviomoreto.github.io/bootstrap-select/
 *
 * Copyright 2013 bootstrap-select
 * Licensed under the MIT license
 */

!function ($) {

    'use strict';

    $.expr[':'].icontains = function (obj, index, meta) {
        return $(obj).text().toUpperCase().indexOf(meta[3].toUpperCase()) >= 0;
    };

    var Selectpicker = function (element, options, e) {
        if (e) {
            e.stopPropagation();
            e.preventDefault();
        }
        this.$element = $(element);
        this.$newElement = null;
        this.$button = null;
        this.$menu = null;
        this.$lis = null;

        //Merge defaults, options and data-attributes to make our options
        this.options = $.extend({}, $.fn.selectpicker.defaults, this.$element.data(), typeof options == 'object' && options);

        //If we have no title yet, check the attribute 'title' (this is missed by jq as its not a data-attribute
        if (this.options.title === null) {
            this.options.title = this.$element.attr('title');
        }

        //Expose public methods
        this.val = Selectpicker.prototype.val;
        this.render = Selectpicker.prototype.render;
        this.refresh = Selectpicker.prototype.refresh;
        this.setStyle = Selectpicker.prototype.setStyle;
        this.selectAll = Selectpicker.prototype.selectAll;
        this.deselectAll = Selectpicker.prototype.deselectAll;
        this.init();
    };

    Selectpicker.prototype = {

        constructor: Selectpicker,

        init: function () {
            var that = this,
                id = this.$element.attr('id');

            this.$element.hide();
            this.multiple = this.$element.prop('multiple');
            this.autofocus = this.$element.prop('autofocus');
            this.$newElement = this.createView();
            this.$element.after(this.$newElement);
            this.$menu = this.$newElement.find('> .dropdown-menu');
            this.$button = this.$newElement.find('> button');
            this.$searchbox = this.$newElement.find('input');

            if (id !== undefined) {
                this.$button.attr('data-id', id);
                $('label[for="' + id + '"]').click(function (e) {
                    e.preventDefault();
                    that.$button.focus();
                });
            }

            this.checkDisabled();
            this.clickListener();
            if (this.options.liveSearch) this.liveSearchListener();
            this.render();
            this.liHeight();
            this.setStyle();
            this.setWidth();
            if (this.options.container) this.selectPosition();
            this.$menu.data('this', this);
            this.$newElement.data('this', this);
        },

        createDropdown: function () {
            //If we are multiple, then add the show-tick class by default
            var multiple = this.multiple ? ' show-tick' : '';
            var autofocus = this.autofocus ? ' autofocus' : '';
            var header = this.options.header ? '<div class="popover-title"><button type="button" class="close" aria-hidden="true">&times;</button>' + this.options.header + '</div>' : '';
            var searchbox = this.options.liveSearch ? '<div class="bootstrap-select-searchbox"><input type="text" class="input-block-level form-control" /></div>' : '';
            var drop =
                '<div class="btn-group bootstrap-select' + multiple + '">' +
                    '<button type="button" class="btn dropdown-toggle selectpicker" data-toggle="dropdown"' + autofocus + '>' +
                        '<span class="filter-option pull-left"></span>&nbsp;' +
                        '<span class="caret"></span>' +
                    '</button>' +
                    '<div class="dropdown-menu open">' +
                        header +
                        searchbox +
                        '<ul class="dropdown-menu inner selectpicker" role="menu">' +
                        '</ul>' +
                    '</div>' +
                '</div>';

            return $(drop);
        },

        createView: function () {
            var $drop = this.createDropdown();
            var $li = this.createLi();
            $drop.find('ul').append($li);
            return $drop;
        },

        reloadLi: function () {
            //Remove all children.
            this.destroyLi();
            //Re build
            var $li = this.createLi();
            this.$menu.find('ul').append($li);
        },

        destroyLi: function () {
            this.$menu.find('li').remove();
        },

        createLi: function () {
            var that = this,
                _liA = [],
                _liHtml = '';

            this.$element.find('option').each(function () {
                var $this = $(this);

                //Get the class and text for the option
                var optionClass = $this.attr('class') || '';
                var inline = $this.attr('style') || '';
                var text = $this.data('content') ? $this.data('content') : $this.html();
                var subtext = $this.data('subtext') !== undefined ? '<small class="muted text-muted">' + $this.data('subtext') + '</small>' : '';
                var icon = $this.data('icon') !== undefined ? '<i class="' + that.options.iconBase + ' ' + $this.data('icon') + '"></i> ' : '';
                if (icon !== '' && ($this.is(':disabled') || $this.parent().is(':disabled'))) {
                    icon = '<span>' + icon + '</span>';
                }

                if (!$this.data('content')) {
                    //Prepend any icon and append any subtext to the main text.
                    text = icon + '<span class="text">' + text + subtext + '</span>';
                }

                if (that.options.hideDisabled && ($this.is(':disabled') || $this.parent().is(':disabled'))) {
                    _liA.push('<a style="min-height: 0; padding: 0"></a>');
                } else if ($this.parent().is('optgroup') && $this.data('divider') !== true) {
                    if ($this.index() === 0) {
                        //Get the opt group label
                        var label = $this.parent().attr('label');
                        var labelSubtext = $this.parent().data('subtext') !== undefined ? '<small class="muted text-muted">' + $this.parent().data('subtext') + '</small>' : '';
                        var labelIcon = $this.parent().data('icon') ? '<i class="' + $this.parent().data('icon') + '"></i> ' : '';
                        label = labelIcon + '<span class="text">' + label + labelSubtext + '</span>';

                        if ($this[0].index !== 0) {
                            _liA.push(
                                '<div class="div-contain"><div class="divider"></div></div>' +
                                '<dt>' + label + '</dt>' +
                                that.createA(text, 'opt ' + optionClass, inline)
                                );
                        } else {
                            _liA.push(
                                '<dt>' + label + '</dt>' +
                                that.createA(text, 'opt ' + optionClass, inline));
                        }
                    } else {
                        _liA.push(that.createA(text, 'opt ' + optionClass, inline));
                    }
                } else if ($this.data('divider') === true) {
                    _liA.push('<div class="div-contain"><div class="divider"></div></div>');
                } else if ($(this).data('hidden') === true) {
                    _liA.push('');
                } else {
                    _liA.push(that.createA(text, optionClass, inline));
                }
            });

            $.each(_liA, function (i, item) {
                _liHtml += '<li rel=' + i + '>' + item + '</li>';
            });

            //If we are not multiple, and we dont have a selected item, and we dont have a title, select the first element so something is set in the button
            if (!this.multiple && this.$element.find('option:selected').length === 0 && !this.options.title) {
                this.$element.find('option').eq(0).prop('selected', true).attr('selected', 'selected');
            }

            return $(_liHtml);
        },

        createA: function (text, classes, inline) {
            return '<a tabindex="0" class="' + classes + '" style="' + inline + '">' +
                 text +
                 '<i class="' + this.options.iconBase + ' ' + this.options.tickIcon + ' icon-ok check-mark"></i>' +
                 '</a>';
        },

        render: function (updateLi) {
            var that = this;

            //Update the LI to match the SELECT
            if (updateLi !== false) {
                this.$element.find('option').each(function (index) {
                    that.setDisabled(index, $(this).is(':disabled') || $(this).parent().is(':disabled'));
                    that.setSelected(index, $(this).is(':selected'));
                });
            }

            this.tabIndex();

            var selectedItems = this.$element.find('option:selected').map(function () {
                var $this = $(this);
                var icon = $this.data('icon') && that.options.showIcon ? '<i class="' + that.options.iconBase + ' ' + $this.data('icon') + '"></i> ' : '';
                var subtext;
                if (that.options.showSubtext && $this.attr('data-subtext') && !that.multiple) {
                    subtext = ' <small class="muted text-muted">' + $this.data('subtext') + '</small>';
                } else {
                    subtext = '';
                }
                if ($this.data('content') && that.options.showContent) {
                    return $this.data('content');
                } else if ($this.attr('title') !== undefined) {
                    return $this.attr('title');
                } else {
                    return icon + $this.html() + subtext;
                }
            }).toArray();

            //Fixes issue in IE10 occurring when no default option is selected and at least one option is disabled
            //Convert all the values into a comma delimited string
            var title = !this.multiple ? selectedItems[0] : selectedItems.join(this.options.multipleSeparator);

            //If this is multi select, and the selectText type is count, the show 1 of 2 selected etc..
            if (this.multiple && this.options.selectedTextFormat.indexOf('count') > -1) {
                var max = this.options.selectedTextFormat.split('>');
                var notDisabled = this.options.hideDisabled ? ':not([disabled])' : '';
                if ((max.length > 1 && selectedItems.length > max[1]) || (max.length == 1 && selectedItems.length >= 2)) {
                    title = this.options.countSelectedText.replace('{0}', selectedItems.length).replace('{1}', this.$element.find('option:not([data-divider="true"]):not([data-hidden="true"])' + notDisabled).length);
                }
            }

            //If we dont have a title, then use the default, or if nothing is set at all, use the not selected text
            if (!title) {
                title = this.options.title !== undefined ? this.options.title : this.options.noneSelectedText;
            }

            this.$button.attr('title', $.trim(title));
            this.$newElement.find('.filter-option').html(title);
        },

        setStyle: function (style, status) {
            if (this.$element.attr('class')) {
                this.$newElement.addClass(this.$element.attr('class').replace(/selectpicker|mobile-device/gi, ''));
            }

            var buttonClass = style ? style : this.options.style;

            if (status == 'add') {
                this.$button.addClass(buttonClass);
            } else if (status == 'remove') {
                this.$button.removeClass(buttonClass);
            } else {
                this.$button.removeClass(this.options.style);
                this.$button.addClass(buttonClass);
            }
        },

        liHeight: function () {
            var $selectClone = this.$menu.parent().clone().find('> .dropdown-toggle').prop('autofocus', false).end().appendTo('body'),
                $menuClone = $selectClone.addClass('open').find('> .dropdown-menu'),
                liHeight = $menuClone.find('li > a').outerHeight(),
                headerHeight = this.options.header ? $menuClone.find('.popover-title').outerHeight() : 0,
                searchHeight = this.options.liveSearch ? $menuClone.find('.bootstrap-select-searchbox').outerHeight() : 0;

            $selectClone.remove();

            this.$newElement
                .data('liHeight', liHeight)
                .data('headerHeight', headerHeight)
                .data('searchHeight', searchHeight);
        },

        setSize: function () {
            var that = this,
                menu = this.$menu,
                menuInner = menu.find('.inner'),
                selectHeight = this.$newElement.outerHeight(),
                liHeight = this.$newElement.data('liHeight'),
                headerHeight = this.$newElement.data('headerHeight'),
                searchHeight = this.$newElement.data('searchHeight'),
                divHeight = menu.find('li .divider').outerHeight(true),
                menuPadding = parseInt(menu.css('padding-top')) +
                              parseInt(menu.css('padding-bottom')) +
                              parseInt(menu.css('border-top-width')) +
                              parseInt(menu.css('border-bottom-width')),
                notDisabled = this.options.hideDisabled ? ':not(.disabled)' : '',
                $window = $(window),
                menuExtras = menuPadding + parseInt(menu.css('margin-top')) + parseInt(menu.css('margin-bottom')) + 2,
                menuHeight,
                selectOffsetTop,
                selectOffsetBot,
                posVert = function () {
                    selectOffsetTop = that.$newElement.offset().top - $window.scrollTop();
                    selectOffsetBot = $window.height() - selectOffsetTop - selectHeight;
                };
            posVert();
            if (this.options.header) menu.css('padding-top', 0);

            if (this.options.size == 'auto') {
                var getSize = function () {
                    var minHeight;
                    posVert();
                    menuHeight = selectOffsetBot - menuExtras;
                    if (that.options.dropupAuto) {
                        that.$newElement.toggleClass('dropup', (selectOffsetTop > selectOffsetBot) && ((menuHeight - menuExtras) < menu.height()));
                    }
                    if (that.$newElement.hasClass('dropup')) {
                        menuHeight = selectOffsetTop - menuExtras;
                    }
                    if ((menu.find('li').length + menu.find('dt').length) > 3) {
                        minHeight = liHeight * 3 + menuExtras - 2;
                    } else {
                        minHeight = 0;
                    }
                    menu.css({ 'max-height': menuHeight + 'px', 'overflow': 'hidden', 'min-height': minHeight + 'px' });
                    menuInner.css({ 'max-height': menuHeight - headerHeight - searchHeight - menuPadding + 'px', 'overflow-y': 'auto', 'min-height': minHeight - menuPadding + 'px' });
                };
                getSize();
                $(window).resize(getSize);
                $(window).scroll(getSize);
            } else if (this.options.size && this.options.size != 'auto' && menu.find('li' + notDisabled).length > this.options.size) {
                var optIndex = menu.find('li' + notDisabled + ' > *').filter(':not(.div-contain)').slice(0, this.options.size).last().parent().index();
                var divLength = menu.find('li').slice(0, optIndex + 1).find('.div-contain').length;
                menuHeight = liHeight * this.options.size + divLength * divHeight + menuPadding;
                if (that.options.dropupAuto) {
                    this.$newElement.toggleClass('dropup', (selectOffsetTop > selectOffsetBot) && (menuHeight < menu.height()));
                }
                menu.css({ 'max-height': menuHeight + headerHeight + searchHeight + 'px', 'overflow': 'hidden' });
                menuInner.css({ 'max-height': menuHeight - menuPadding + 'px', 'overflow-y': 'auto' });
            }
        },

        setWidth: function () {
            if (this.options.width == 'auto') {
                this.$menu.css('min-width', '0');

                // Get correct width if element hidden
                var selectClone = this.$newElement.clone().appendTo('body');
                var ulWidth = selectClone.find('> .dropdown-menu').css('width');
                selectClone.remove();

                this.$newElement.css('width', ulWidth);
            } else if (this.options.width == 'fit') {
                // Remove inline min-width so width can be changed from 'auto'
                this.$menu.css('min-width', '');
                this.$newElement.css('width', '').addClass('fit-width');
            } else if (this.options.width) {
                // Remove inline min-width so width can be changed from 'auto'
                this.$menu.css('min-width', '');
                this.$newElement.css('width', this.options.width);
            } else {
                // Remove inline min-width/width so width can be changed
                this.$menu.css('min-width', '');
                this.$newElement.css('width', '');
            }
            // Remove fit-width class if width is changed programmatically
            if (this.$newElement.hasClass('fit-width') && this.options.width !== 'fit') {
                this.$newElement.removeClass('fit-width');
            }
        },

        selectPosition: function () {
            var that = this,
                drop = '<div />',
                $drop = $(drop),
                pos,
                actualHeight,
                getPlacement = function ($element) {
                    $drop.addClass($element.attr('class')).toggleClass('dropup', $element.hasClass('dropup'));
                    pos = $element.offset();
                    actualHeight = $element.hasClass('dropup') ? 0 : $element[0].offsetHeight;
                    $drop.css({ 'top': pos.top + actualHeight, 'left': pos.left, 'width': $element[0].offsetWidth, 'position': 'absolute' });
                };
            this.$newElement.on('click', function () {
                getPlacement($(this));
                $drop.appendTo(that.options.container);
                $drop.toggleClass('open', !$(this).hasClass('open'));
                $drop.append(that.$menu);
            });
            $(window).resize(function () {
                getPlacement(that.$newElement);
            });
            $(window).on('scroll', function () {
                getPlacement(that.$newElement);
            });
            $('html').on('click', function (e) {
                if ($(e.target).closest(that.$newElement).length < 1) {
                    $drop.removeClass('open');
                }
            });
        },

        mobile: function () {
            this.$element.addClass('mobile-device').appendTo(this.$newElement);
            if (this.options.container) this.$menu.hide();
        },

        refresh: function () {
            this.$lis = null;
            this.reloadLi();
            this.render();
            this.setWidth();
            this.setStyle();
            this.checkDisabled();
            this.liHeight();
        },

        update: function () {
            this.reloadLi();
            this.setWidth();
            this.setStyle();
            this.checkDisabled();
            this.liHeight();
        },

        setSelected: function (index, selected) {
            if (this.$lis == null) this.$lis = this.$menu.find('li');
            $(this.$lis[index]).toggleClass('selected', selected);
        },

        setDisabled: function (index, disabled) {
            if (this.$lis == null) this.$lis = this.$menu.find('li');
            if (disabled) {
                $(this.$lis[index]).addClass('disabled').find('a').attr('href', '#').attr('tabindex', -1);
            } else {
                $(this.$lis[index]).removeClass('disabled').find('a').removeAttr('href').attr('tabindex', 0);
            }
        },

        isDisabled: function () {
            return this.$element.is(':disabled');
        },

        checkDisabled: function () {
            var that = this;

            if (this.isDisabled()) {
                this.$button.addClass('disabled').attr('tabindex', -1);
            } else {
                if (this.$button.hasClass('disabled')) {
                    this.$button.removeClass('disabled');
                }

                if (this.$button.attr('tabindex') == -1) {
                    if (!this.$element.data('tabindex')) this.$button.removeAttr('tabindex');
                }
            }

            this.$button.click(function () {
                return !that.isDisabled();
            });
        },

        tabIndex: function () {
            if (this.$element.is('[tabindex]')) {
                this.$element.data('tabindex', this.$element.attr('tabindex'));
                this.$button.attr('tabindex', this.$element.data('tabindex'));
            }
        },

        clickListener: function () {
            var that = this;

            $('body').on('touchstart.dropdown', '.dropdown-menu', function (e) {
                e.stopPropagation();
            });

            this.$newElement.on('click', function () {
                that.setSize();
                if (!that.options.liveSearch && !that.multiple) {
                    setTimeout(function () {
                        that.$menu.find('.selected a').focus();
                    }, 10);
                }
            });

            this.$menu.on('click', 'li a', function (e) {
                var clickedIndex = $(this).parent().index(),
                    prevValue = that.$element.val(),
                    prevIndex = that.$element.prop('selectedIndex');

                //Dont close on multi choice menu
                if (that.multiple) {
                    e.stopPropagation();
                }

                e.preventDefault();

                //Dont run if we have been disabled
                if (!that.isDisabled() && !$(this).parent().hasClass('disabled')) {
                    var $options = that.$element.find('option'),
                        $option = $options.eq(clickedIndex),
                        state = $option.prop('selected');

                    //Deselect all others if not multi select box
                    if (!that.multiple) {
                        $options.prop('selected', false);
                        $option.prop('selected', true);
                        that.$menu.find('.selected').removeClass('selected');
                        that.setSelected(clickedIndex, true);
                    }
                        //Else toggle the one we have chosen if we are multi select.
                    else {
                        $option.prop('selected', !state);
                        that.setSelected(clickedIndex, !state);
                    }

                    if (!that.multiple) {
                        that.$button.focus();
                    } else if (that.options.liveSearch) {
                        that.$searchbox.focus();
                    }

                    // Trigger select 'change'
                    if ((prevValue != that.$element.val() && that.multiple) || (prevIndex != that.$element.prop('selectedIndex') && !that.multiple)) {
                        that.$element.change();
                    }
                }
            });

            this.$menu.on('click', 'li.disabled a, li dt, li .div-contain, .popover-title, .popover-title :not(.close)', function (e) {
                if (e.target == this) {
                    e.preventDefault();
                    e.stopPropagation();
                    if (!that.options.liveSearch) {
                        that.$button.focus();
                    } else {
                        that.$searchbox.focus();
                    }
                }
            });

            this.$menu.on('click', '.popover-title .close', function () {
                that.$button.focus();
            });

            this.$searchbox.on('click', function (e) {
                e.stopPropagation();
            });

            this.$element.change(function () {
                that.render(false);
            });
        },

        liveSearchListener: function () {
            var that = this,
                no_results = $('<li class="no-results"></li>');

            this.$newElement.on('click.dropdown.data-api', function () {
                that.$menu.find('.active').removeClass('active');
                if (!!that.$searchbox.val()) {
                    that.$searchbox.val('');
                    that.$menu.find('li').show();
                    if (!!no_results.parent().length) no_results.remove();
                }
                if (!that.multiple) that.$menu.find('.selected').addClass('active');
                setTimeout(function () {
                    that.$searchbox.focus();
                }, 10);
            });

            this.$searchbox.on('input propertychange', function () {
                if (that.$searchbox.val()) {
                    that.$menu.find('li').show().not(':icontains(' + that.$searchbox.val() + ')').hide();

                    if (!that.$menu.find('li').filter(':visible:not(.no-results)').length) {
                        if (!!no_results.parent().length) no_results.remove();
                        no_results.html(that.options.noneResultsText + ' "' + that.$searchbox.val() + '"').show();
                        that.$menu.find('li').last().after(no_results);
                    } else if (!!no_results.parent().length) {
                        no_results.remove();
                    }

                } else {
                    that.$menu.find('li').show();
                    if (!!no_results.parent().length) no_results.remove();
                }

                that.$menu.find('li.active').removeClass('active');
                that.$menu.find('li').filter(':visible:not(.divider)').eq(0).addClass('active').find('a').focus();
                $(this).focus();
            });

            this.$menu.on('mouseenter', 'a', function (e) {
                that.$menu.find('.active').removeClass('active');
                $(e.currentTarget).parent().not('.disabled').addClass('active');
            });

            this.$menu.on('mouseleave', 'a', function () {
                that.$menu.find('.active').removeClass('active');
            });
        },

        val: function (value) {

            if (value !== undefined) {
                this.$element.val(value);

                this.$element.change();
                return this.$element;
            } else {
                return this.$element.val();
            }
        },

        selectAll: function () {
            this.$element.find('option').prop('selected', true).attr('selected', 'selected');
            this.render();
        },

        deselectAll: function () {
            this.$element.find('option').prop('selected', false).removeAttr('selected');
            this.render();
        },

        keydown: function (e) {
            var $this,
                $items,
                $parent,
                index,
                next,
                first,
                last,
                prev,
                nextPrev,
                that,
                prevIndex,
                isActive,
                keyCodeMap = {
                    32: ' ', 48: '0', 49: '1', 50: '2', 51: '3', 52: '4', 53: '5', 54: '6', 55: '7', 56: '8', 57: '9', 59: ';',
                    65: 'a', 66: 'b', 67: 'c', 68: 'd', 69: 'e', 70: 'f', 71: 'g', 72: 'h', 73: 'i', 74: 'j', 75: 'k', 76: 'l',
                    77: 'm', 78: 'n', 79: 'o', 80: 'p', 81: 'q', 82: 'r', 83: 's', 84: 't', 85: 'u', 86: 'v', 87: 'w', 88: 'x',
                    89: 'y', 90: 'z', 96: '0', 97: '1', 98: '2', 99: '3', 100: '4', 101: '5', 102: '6', 103: '7', 104: '8', 105: '9'
                };

            $this = $(this);

            $parent = $this.parent();

            if ($this.is('input')) $parent = $this.parent().parent();

            that = $parent.data('this');

            if (that.options.liveSearch) $parent = $this.parent().parent();

            if (that.options.container) $parent = that.$menu;

            $items = $('[role=menu] li:not(.divider) a', $parent);

            isActive = that.$menu.parent().hasClass('open');

            if (!isActive && /([0-9]|[A-z])/.test(String.fromCharCode(e.keyCode))) {
                that.setSize();
                that.$menu.parent().addClass('open');
                isActive = that.$menu.parent().hasClass('open');
                that.$searchbox.focus();
            }

            if (that.options.liveSearch) {
                if (/(^9$|27)/.test(e.keyCode) && isActive && that.$menu.find('.active').length === 0) {
                    e.preventDefault();
                    that.$menu.parent().removeClass('open');
                    that.$button.focus();
                }
                $items = $('[role=menu] li:not(.divider):visible', $parent);
                if (!$this.val() && !/(38|40)/.test(e.keyCode)) {
                    if ($items.filter('.active').length === 0) {
                        $items = that.$newElement.find('li').filter(':icontains(' + keyCodeMap[e.keyCode] + ')');
                    }
                }
            }

            if (!$items.length) return;

            if (/(38|40)/.test(e.keyCode)) {

                index = $items.index($items.filter(':focus'));
                first = $items.parent(':not(.disabled):visible').first().index();
                last = $items.parent(':not(.disabled):visible').last().index();
                next = $items.eq(index).parent().nextAll(':not(.disabled):visible').eq(0).index();
                prev = $items.eq(index).parent().prevAll(':not(.disabled):visible').eq(0).index();
                nextPrev = $items.eq(next).parent().prevAll(':not(.disabled):visible').eq(0).index();

                if (that.options.liveSearch) {
                    $items.each(function (i) {
                        if ($(this).is(':not(.disabled)')) {
                            $(this).data('index', i);
                        }
                    });
                    index = $items.index($items.filter('.active'));
                    first = $items.filter(':not(.disabled):visible').first().data('index');
                    last = $items.filter(':not(.disabled):visible').last().data('index');
                    next = $items.eq(index).nextAll(':not(.disabled):visible').eq(0).data('index');
                    prev = $items.eq(index).prevAll(':not(.disabled):visible').eq(0).data('index');
                    nextPrev = $items.eq(next).prevAll(':not(.disabled):visible').eq(0).data('index');
                }

                prevIndex = $this.data('prevIndex');

                if (e.keyCode == 38) {
                    if (that.options.liveSearch) index -= 1;
                    if (index != nextPrev && index > prev) index = prev;
                    if (index < first) index = first;
                    if (index == prevIndex) index = last;
                }

                if (e.keyCode == 40) {
                    if (that.options.liveSearch) index += 1;
                    if (index == -1) index = 0;
                    if (index != nextPrev && index < next) index = next;
                    if (index > last) index = last;
                    if (index == prevIndex) index = first;
                }

                $this.data('prevIndex', index);

                if (!that.options.liveSearch) {
                    $items.eq(index).focus();
                } else {
                    e.preventDefault();
                    if (!$this.is('.dropdown-toggle')) {
                        $items.removeClass('active');
                        $items.eq(index).addClass('active').find('a').focus();
                        $this.focus();
                    }
                }

            } else if (!$this.is('input')) {

                var keyIndex = [],
                    count,
                    prevKey;

                $items.each(function () {
                    if ($(this).parent().is(':not(.disabled)')) {
                        if ($.trim($(this).text().toLowerCase()).substring(0, 1) == keyCodeMap[e.keyCode]) {
                            keyIndex.push($(this).parent().index());
                        }
                    }
                });

                count = $(document).data('keycount');
                count++;
                $(document).data('keycount', count);

                prevKey = $.trim($(':focus').text().toLowerCase()).substring(0, 1);

                if (prevKey != keyCodeMap[e.keyCode]) {
                    count = 1;
                    $(document).data('keycount', count);
                } else if (count >= keyIndex.length) {
                    $(document).data('keycount', 0);
                    if (count > keyIndex.length) count = 1;
                }

                $items.eq(keyIndex[count - 1]).focus();
            }

            // Select focused option if "Enter", "Spacebar", "Tab" are pressed inside the menu.
            if (/(13|32|^9$)/.test(e.keyCode) && isActive) {
                if (!/(32)/.test(e.keyCode)) e.preventDefault();
                if (!that.options.liveSearch) {
                    $(':focus').click();
                } else if (!/(32)/.test(e.keyCode)) {
                    that.$menu.find('.active a').click();
                    $this.focus();
                }
                $(document).data('keycount', 0);
            }

            if ((/(^9$|27)/.test(e.keyCode) && isActive && (that.multiple || that.options.liveSearch)) || (/(27)/.test(e.keyCode) && !isActive)) {
                that.$menu.parent().removeClass('open');
                that.$button.focus();
            }

        },

        hide: function () {
            this.$newElement.hide();
        },

        show: function () {
            this.$newElement.show();
        },

        destroy: function () {
            this.$newElement.remove();
            this.$element.remove();
        }
    };

    $.fn.selectpicker = function (option, event) {
        //get the args of the outer function..
        var args = arguments;
        var value;
        var chain = this.each(function () {
            if ($(this).is('select')) {
                var $this = $(this),
                    data = $this.data('selectpicker'),
                    options = typeof option == 'object' && option;

                if (!data) {
                    $this.data('selectpicker', (data = new Selectpicker(this, options, event)));
                } else if (options) {
                    for (var i in options) {
                        data.options[i] = options[i];
                    }
                }

                if (typeof option == 'string') {
                    //Copy the value of option, as once we shift the arguments
                    //it also shifts the value of option.
                    var property = option;
                    if (data[property] instanceof Function) {
                        [].shift.apply(args);
                        value = data[property].apply(data, args);
                    } else {
                        value = data.options[property];
                    }
                }
            }
        });

        if (value !== undefined) {
            return value;
        } else {
            return chain;
        }
    };

    $.fn.selectpicker.defaults = {
        style: 'btn-default',
        size: 'auto',
        title: null,
        selectedTextFormat: 'values',
        noneSelectedText: 'Nothing selected',
        noneResultsText: 'Không tìm th?y',
        countSelectedText: '{0} of {1} selected',
        width: false,
        container: false,
        hideDisabled: false,
        showSubtext: false,
        showIcon: true,
        showContent: true,
        dropupAuto: true,
        header: false,
        liveSearch: false,
        multipleSeparator: ', ',
        iconBase: 'glyphicon',
        tickIcon: 'glyphicon-ok'
    };

    $(document)
        .data('keycount', 0)
        .on('keydown', '.bootstrap-select [data-toggle=dropdown], .bootstrap-select [role=menu], .bootstrap-select-searchbox input', Selectpicker.prototype.keydown)
        .on('focusin.modal', '.bootstrap-select [data-toggle=dropdown], .bootstrap-select [role=menu], .bootstrap-select-searchbox input', function (e) { e.stopPropagation(); });

}(window.jQuery);
/*!
 * jQuery blockUI plugin
 * Version 2.70.0-2014.11.23
 * Requires jQuery v1.7 or later
 *
 * Examples at: http://malsup.com/jquery/block/
 * Copyright (c) 2007-2013 M. Alsup
 * Dual licensed under the MIT and GPL licenses:
 * http://www.opensource.org/licenses/mit-license.php
 * http://www.gnu.org/licenses/gpl.html
 *
 * Thanks to Amir-Hossein Sobhi for some excellent contributions!
 */

; (function () {
    /*jshint eqeqeq:false curly:false latedef:false */
    "use strict";

    function setup($) {
        $.fn._fadeIn = $.fn.fadeIn;

        var noOp = $.noop || function () { };

        // this bit is to ensure we don't call setExpression when we shouldn't (with extra muscle to handle
        // confusing userAgent strings on Vista)
        var msie = /MSIE/.test(navigator.userAgent);
        var ie6 = /MSIE 6.0/.test(navigator.userAgent) && ! /MSIE 8.0/.test(navigator.userAgent);
        var mode = document.documentMode || 0;
        var setExpr = $.isFunction(document.createElement('div').style.setExpression);

        // global $ methods for blocking/unblocking the entire page
        $.blockUI = function (opts) { install(window, opts); };
        $.unblockUI = function (opts) { remove(window, opts); };

        // convenience method for quick growl-like notifications  (http://www.google.com/search?q=growl)
        $.growlUI = function (title, message, timeout, onClose) {
            var $m = $('<div class="growlUI"></div>');
            if (title) $m.append('<h1>' + title + '</h1>');
            if (message) $m.append('<h2>' + message + '</h2>');
            if (timeout === undefined) timeout = 3000;

            // Added by konapun: Set timeout to 30 seconds if this growl is moused over, like normal toast notifications
            var callBlock = function (opts) {
                opts = opts || {};

                $.blockUI({
                    message: $m,
                    fadeIn: typeof opts.fadeIn !== 'undefined' ? opts.fadeIn : 700,
                    fadeOut: typeof opts.fadeOut !== 'undefined' ? opts.fadeOut : 1000,
                    timeout: typeof opts.timeout !== 'undefined' ? opts.timeout : timeout,
                    centerY: false,
                    showOverlay: false,
                    onUnblock: onClose,
                    css: $.blockUI.defaults.growlCSS
                });
            };

            callBlock();
            var nonmousedOpacity = $m.css('opacity');
            $m.mouseover(function () {
                callBlock({
                    fadeIn: 0,
                    timeout: 30000
                });

                var displayBlock = $('.blockMsg');
                displayBlock.stop(); // cancel fadeout if it has started
                displayBlock.fadeTo(300, 1); // make it easier to read the message by removing transparency
            }).mouseout(function () {
                $('.blockMsg').fadeOut(1000);
            });
            // End konapun additions
        };

        // plugin method for blocking element content
        $.fn.block = function (opts) {
            if (this[0] === window) {
                $.blockUI(opts);
                return this;
            }
            var fullOpts = $.extend({}, $.blockUI.defaults, opts || {});
            this.each(function () {
                var $el = $(this);
                if (fullOpts.ignoreIfBlocked && $el.data('blockUI.isBlocked'))
                    return;
                $el.unblock({ fadeOut: 0 });
            });

            return this.each(function () {
                if ($.css(this, 'position') == 'static') {
                    this.style.position = 'relative';
                    $(this).data('blockUI.static', true);
                }
                this.style.zoom = 1; // force 'hasLayout' in ie
                install(this, opts);
            });
        };

        // plugin method for unblocking element content
        $.fn.unblock = function (opts) {
            if (this[0] === window) {
                $.unblockUI(opts);
                return this;
            }
            return this.each(function () {
                remove(this, opts);
            });
        };

        $.blockUI.version = 2.70; // 2nd generation blocking at no extra cost!

        // override these in your code to change the default behavior and style
        $.blockUI.defaults = {
            // message displayed when blocking (use null for no message)
            message: '<h1>Please wait...</h1>',

            title: null,		// title string; only used when theme == true
            draggable: true,	// only used when theme == true (requires jquery-ui.js to be loaded)

            theme: false, // set to true to use with jQuery UI themes

            // styles for the message when blocking; if you wish to disable
            // these and use an external stylesheet then do this in your code:
            // $.blockUI.defaults.css = {};
            css: {
                padding: 0,
                margin: 0,
                width: '30%',
                top: '40%',
                left: '35%',
                textAlign: 'center',
                color: '#000',
                border: '3px solid #aaa',
                backgroundColor: '#fff',
                cursor: 'wait'
            },

            // minimal style set used when themes are used
            themedCSS: {
                width: '30%',
                top: '40%',
                left: '35%'
            },

            // styles for the overlay
            overlayCSS: {
                backgroundColor: '#000',
                opacity: 0.6,
                cursor: 'wait'
            },

            // style to replace wait cursor before unblocking to correct issue
            // of lingering wait cursor
            cursorReset: 'default',

            // styles applied when using $.growlUI
            growlCSS: {
                width: '350px',
                top: '10px',
                left: '',
                right: '10px',
                border: 'none',
                padding: '5px',
                opacity: 0.6,
                cursor: 'default',
                color: '#fff',
                backgroundColor: '#000',
                '-webkit-border-radius': '10px',
                '-moz-border-radius': '10px',
                'border-radius': '10px'
            },

            // IE issues: 'about:blank' fails on HTTPS and javascript:false is s-l-o-w
            // (hat tip to Jorge H. N. de Vasconcelos)
            /*jshint scripturl:true */
            iframeSrc: /^https/i.test(window.location.href || '') ? 'javascript:false' : 'about:blank',

            // force usage of iframe in non-IE browsers (handy for blocking applets)
            forceIframe: false,

            // z-index for the blocking overlay
            baseZ: 1000,

            // set these to true to have the message automatically centered
            centerX: true, // <-- only effects element blocking (page block controlled via css above)
            centerY: true,

            // allow body element to be stetched in ie6; this makes blocking look better
            // on "short" pages.  disable if you wish to prevent changes to the body height
            allowBodyStretch: true,

            // enable if you want key and mouse events to be disabled for content that is blocked
            bindEvents: true,

            // be default blockUI will supress tab navigation from leaving blocking content
            // (if bindEvents is true)
            constrainTabKey: true,

            // fadeIn time in millis; set to 0 to disable fadeIn on block
            fadeIn: 200,

            // fadeOut time in millis; set to 0 to disable fadeOut on unblock
            fadeOut: 400,

            // time in millis to wait before auto-unblocking; set to 0 to disable auto-unblock
            timeout: 0,

            // disable if you don't want to show the overlay
            showOverlay: true,

            // if true, focus will be placed in the first available input field when
            // page blocking
            focusInput: true,

            // elements that can receive focus
            focusableElements: ':input:enabled:visible',

            // suppresses the use of overlay styles on FF/Linux (due to performance issues with opacity)
            // no longer needed in 2012
            // applyPlatformOpacityRules: true,

            // callback method invoked when fadeIn has completed and blocking message is visible
            onBlock: null,

            // callback method invoked when unblocking has completed; the callback is
            // passed the element that has been unblocked (which is the window object for page
            // blocks) and the options that were passed to the unblock call:
            //	onUnblock(element, options)
            onUnblock: null,

            // callback method invoked when the overlay area is clicked.
            // setting this will turn the cursor to a pointer, otherwise cursor defined in overlayCss will be used.
            onOverlayClick: null,

            // don't ask; if you really must know: http://groups.google.com/group/jquery-en/browse_thread/thread/36640a8730503595/2f6a79a77a78e493#2f6a79a77a78e493
            quirksmodeOffsetHack: 4,

            // class name of the message block
            blockMsgClass: 'blockMsg',

            // if it is already blocked, then ignore it (don't unblock and reblock)
            ignoreIfBlocked: false
        };

        // private data and functions follow...

        var pageBlock = null;
        var pageBlockEls = [];

        function install(el, opts) {
            var css, themedCSS;
            var full = (el == window);
            var msg = (opts && opts.message !== undefined ? opts.message : undefined);
            opts = $.extend({}, $.blockUI.defaults, opts || {});

            if (opts.ignoreIfBlocked && $(el).data('blockUI.isBlocked'))
                return;

            opts.overlayCSS = $.extend({}, $.blockUI.defaults.overlayCSS, opts.overlayCSS || {});
            css = $.extend({}, $.blockUI.defaults.css, opts.css || {});
            if (opts.onOverlayClick)
                opts.overlayCSS.cursor = 'pointer';

            themedCSS = $.extend({}, $.blockUI.defaults.themedCSS, opts.themedCSS || {});
            msg = msg === undefined ? opts.message : msg;

            // remove the current block (if there is one)
            if (full && pageBlock)
                remove(window, { fadeOut: 0 });

            // if an existing element is being used as the blocking content then we capture
            // its current place in the DOM (and current display style) so we can restore
            // it when we unblock
            if (msg && typeof msg != 'string' && (msg.parentNode || msg.jquery)) {
                var node = msg.jquery ? msg[0] : msg;
                var data = {};
                $(el).data('blockUI.history', data);
                data.el = node;
                data.parent = node.parentNode;
                data.display = node.style.display;
                data.position = node.style.position;
                if (data.parent)
                    data.parent.removeChild(node);
            }

            $(el).data('blockUI.onUnblock', opts.onUnblock);
            var z = opts.baseZ;

            // blockUI uses 3 layers for blocking, for simplicity they are all used on every platform;
            // layer1 is the iframe layer which is used to supress bleed through of underlying content
            // layer2 is the overlay layer which has opacity and a wait cursor (by default)
            // layer3 is the message content that is displayed while blocking
            var lyr1, lyr2, lyr3, s;
            if (msie || opts.forceIframe)
                lyr1 = $('<iframe class="blockUI" style="z-index:' + (z++) + ';display:none;border:none;margin:0;padding:0;position:absolute;width:100%;height:100%;top:0;left:0" src="' + opts.iframeSrc + '"></iframe>');
            else
                lyr1 = $('<div class="blockUI" style="display:none"></div>');

            if (opts.theme)
                lyr2 = $('<div class="blockUI blockOverlay ui-widget-overlay" style="z-index:' + (z++) + ';display:none"></div>');
            else
                lyr2 = $('<div class="blockUI blockOverlay" style="z-index:' + (z++) + ';display:none;border:none;margin:0;padding:0;width:100%;height:100%;top:0;left:0"></div>');

            if (opts.theme && full) {
                s = '<div class="blockUI ' + opts.blockMsgClass + ' blockPage ui-dialog ui-widget ui-corner-all" style="z-index:' + (z + 10) + ';display:none;position:fixed">';
                if (opts.title) {
                    s += '<div class="ui-widget-header ui-dialog-titlebar ui-corner-all blockTitle">' + (opts.title || '&nbsp;') + '</div>';
                }
                s += '<div class="ui-widget-content ui-dialog-content"></div>';
                s += '</div>';
            }
            else if (opts.theme) {
                s = '<div class="blockUI ' + opts.blockMsgClass + ' blockElement ui-dialog ui-widget ui-corner-all" style="z-index:' + (z + 10) + ';display:none;position:absolute">';
                if (opts.title) {
                    s += '<div class="ui-widget-header ui-dialog-titlebar ui-corner-all blockTitle">' + (opts.title || '&nbsp;') + '</div>';
                }
                s += '<div class="ui-widget-content ui-dialog-content"></div>';
                s += '</div>';
            }
            else if (full) {
                s = '<div class="blockUI ' + opts.blockMsgClass + ' blockPage" style="z-index:' + (z + 10) + ';display:none;position:fixed"></div>';
            }
            else {
                s = '<div class="blockUI ' + opts.blockMsgClass + ' blockElement" style="z-index:' + (z + 10) + ';display:none;position:absolute"></div>';
            }
            lyr3 = $(s);

            // if we have a message, style it
            if (msg) {
                if (opts.theme) {
                    lyr3.css(themedCSS);
                    lyr3.addClass('ui-widget-content');
                }
                else
                    lyr3.css(css);
            }

            // style the overlay
            if (!opts.theme /*&& (!opts.applyPlatformOpacityRules)*/)
                lyr2.css(opts.overlayCSS);
            lyr2.css('position', full ? 'fixed' : 'absolute');

            // make iframe layer transparent in IE
            if (msie || opts.forceIframe)
                lyr1.css('opacity', 0.0);

            //$([lyr1[0],lyr2[0],lyr3[0]]).appendTo(full ? 'body' : el);
            var layers = [lyr1, lyr2, lyr3], $par = full ? $('body') : $(el);
            $.each(layers, function () {
                this.appendTo($par);
            });

            if (opts.theme && opts.draggable && $.fn.draggable) {
                lyr3.draggable({
                    handle: '.ui-dialog-titlebar',
                    cancel: 'li'
                });
            }

            // ie7 must use absolute positioning in quirks mode and to account for activex issues (when scrolling)
            var expr = setExpr && (!$.support.boxModel || $('object,embed', full ? null : el).length > 0);
            if (ie6 || expr) {
                // give body 100% height
                if (full && opts.allowBodyStretch && $.support.boxModel)
                    $('html,body').css('height', '100%');

                // fix ie6 issue when blocked element has a border width
                if ((ie6 || !$.support.boxModel) && !full) {
                    var t = sz(el, 'borderTopWidth'), l = sz(el, 'borderLeftWidth');
                    var fixT = t ? '(0 - ' + t + ')' : 0;
                    var fixL = l ? '(0 - ' + l + ')' : 0;
                }

                // simulate fixed position
                $.each(layers, function (i, o) {
                    var s = o[0].style;
                    s.position = 'absolute';
                    if (i < 2) {
                        if (full)
                            s.setExpression('height', 'Math.max(document.body.scrollHeight, document.body.offsetHeight) - (jQuery.support.boxModel?0:' + opts.quirksmodeOffsetHack + ') + "px"');
                        else
                            s.setExpression('height', 'this.parentNode.offsetHeight + "px"');
                        if (full)
                            s.setExpression('width', 'jQuery.support.boxModel && document.documentElement.clientWidth || document.body.clientWidth + "px"');
                        else
                            s.setExpression('width', 'this.parentNode.offsetWidth + "px"');
                        if (fixL) s.setExpression('left', fixL);
                        if (fixT) s.setExpression('top', fixT);
                    }
                    else if (opts.centerY) {
                        if (full) s.setExpression('top', '(document.documentElement.clientHeight || document.body.clientHeight) / 2 - (this.offsetHeight / 2) + (blah = document.documentElement.scrollTop ? document.documentElement.scrollTop : document.body.scrollTop) + "px"');
                        s.marginTop = 0;
                    }
                    else if (!opts.centerY && full) {
                        var top = (opts.css && opts.css.top) ? parseInt(opts.css.top, 10) : 0;
                        var expression = '((document.documentElement.scrollTop ? document.documentElement.scrollTop : document.body.scrollTop) + ' + top + ') + "px"';
                        s.setExpression('top', expression);
                    }
                });
            }

            // show the message
            if (msg) {
                if (opts.theme)
                    lyr3.find('.ui-widget-content').append(msg);
                else
                    lyr3.append(msg);
                if (msg.jquery || msg.nodeType)
                    $(msg).show();
            }

            if ((msie || opts.forceIframe) && opts.showOverlay)
                lyr1.show(); // opacity is zero
            if (opts.fadeIn) {
                var cb = opts.onBlock ? opts.onBlock : noOp;
                var cb1 = (opts.showOverlay && !msg) ? cb : noOp;
                var cb2 = msg ? cb : noOp;
                if (opts.showOverlay)
                    lyr2._fadeIn(opts.fadeIn, cb1);
                if (msg)
                    lyr3._fadeIn(opts.fadeIn, cb2);
            }
            else {
                if (opts.showOverlay)
                    lyr2.show();
                if (msg)
                    lyr3.show();
                if (opts.onBlock)
                    opts.onBlock.bind(lyr3)();
            }

            // bind key and mouse events
            bind(1, el, opts);

            if (full) {
                pageBlock = lyr3[0];
                pageBlockEls = $(opts.focusableElements, pageBlock);
                if (opts.focusInput)
                    setTimeout(focus, 20);
            }
            else
                center(lyr3[0], opts.centerX, opts.centerY);

            if (opts.timeout) {
                // auto-unblock
                var to = setTimeout(function () {
                    if (full)
                        $.unblockUI(opts);
                    else
                        $(el).unblock(opts);
                }, opts.timeout);
                $(el).data('blockUI.timeout', to);
            }
        }

        // remove the block
        function remove(el, opts) {
            var count;
            var full = (el == window);
            var $el = $(el);
            var data = $el.data('blockUI.history');
            var to = $el.data('blockUI.timeout');
            if (to) {
                clearTimeout(to);
                $el.removeData('blockUI.timeout');
            }
            opts = $.extend({}, $.blockUI.defaults, opts || {});
            bind(0, el, opts); // unbind events

            if (opts.onUnblock === null) {
                opts.onUnblock = $el.data('blockUI.onUnblock');
                $el.removeData('blockUI.onUnblock');
            }

            var els;
            if (full) // crazy selector to handle odd field errors in ie6/7
                els = $('body').children().filter('.blockUI').add('body > .blockUI');
            else
                els = $el.find('>.blockUI');

            // fix cursor issue
            if (opts.cursorReset) {
                if (els.length > 1)
                    els[1].style.cursor = opts.cursorReset;
                if (els.length > 2)
                    els[2].style.cursor = opts.cursorReset;
            }

            if (full)
                pageBlock = pageBlockEls = null;

            if (opts.fadeOut) {
                count = els.length;
                els.stop().fadeOut(opts.fadeOut, function () {
                    if (--count === 0)
                        reset(els, data, opts, el);
                });
            }
            else
                reset(els, data, opts, el);
        }

        // move blocking element back into the DOM where it started
        function reset(els, data, opts, el) {
            var $el = $(el);
            if ($el.data('blockUI.isBlocked'))
                return;

            els.each(function (i, o) {
                // remove via DOM calls so we don't lose event handlers
                if (this.parentNode)
                    this.parentNode.removeChild(this);
            });

            if (data && data.el) {
                data.el.style.display = data.display;
                data.el.style.position = data.position;
                data.el.style.cursor = 'default'; // #59
                if (data.parent)
                    data.parent.appendChild(data.el);
                $el.removeData('blockUI.history');
            }

            if ($el.data('blockUI.static')) {
                $el.css('position', 'static'); // #22
            }

            if (typeof opts.onUnblock == 'function')
                opts.onUnblock(el, opts);

            // fix issue in Safari 6 where block artifacts remain until reflow
            var body = $(document.body), w = body.width(), cssW = body[0].style.width;
            body.width(w - 1).width(w);
            body[0].style.width = cssW;
        }

        // bind/unbind the handler
        function bind(b, el, opts) {
            var full = el == window, $el = $(el);

            // don't bother unbinding if there is nothing to unbind
            if (!b && (full && !pageBlock || !full && !$el.data('blockUI.isBlocked')))
                return;

            $el.data('blockUI.isBlocked', b);

            // don't bind events when overlay is not in use or if bindEvents is false
            if (!full || !opts.bindEvents || (b && !opts.showOverlay))
                return;

            // bind anchors and inputs for mouse and key events
            var events = 'mousedown mouseup keydown keypress keyup touchstart touchend touchmove';
            if (b)
                $(document).bind(events, opts, handler);
            else
                $(document).unbind(events, handler);

            // former impl...
            //		var $e = $('a,:input');
            //		b ? $e.bind(events, opts, handler) : $e.unbind(events, handler);
        }

        // event handler to suppress keyboard/mouse events when blocking
        function handler(e) {
            // allow tab navigation (conditionally)
            if (e.type === 'keydown' && e.keyCode && e.keyCode == 9) {
                if (pageBlock && e.data.constrainTabKey) {
                    var els = pageBlockEls;
                    var fwd = !e.shiftKey && e.target === els[els.length - 1];
                    var back = e.shiftKey && e.target === els[0];
                    if (fwd || back) {
                        setTimeout(function () { focus(back); }, 10);
                        return false;
                    }
                }
            }
            var opts = e.data;
            var target = $(e.target);
            if (target.hasClass('blockOverlay') && opts.onOverlayClick)
                opts.onOverlayClick(e);

            // allow events within the message content
            if (target.parents('div.' + opts.blockMsgClass).length > 0)
                return true;

            // allow events for content that is not being blocked
            return target.parents().children().filter('div.blockUI').length === 0;
        }

        function focus(back) {
            if (!pageBlockEls)
                return;
            var e = pageBlockEls[back === true ? pageBlockEls.length - 1 : 0];
            if (e)
                e.focus();
        }

        function center(el, x, y) {
            var p = el.parentNode, s = el.style;
            var l = ((p.offsetWidth - el.offsetWidth) / 2) - sz(p, 'borderLeftWidth');
            var t = ((p.offsetHeight - el.offsetHeight) / 2) - sz(p, 'borderTopWidth');
            if (x) s.left = l > 0 ? (l + 'px') : '0';
            if (y) s.top = t > 0 ? (t + 'px') : '0';
        }

        function sz(el, p) {
            return parseInt($.css(el, p), 10) || 0;
        }

    }


    /*global define:true */
    if (typeof define === 'function' && define.amd && define.amd.jQuery) {
        define(['jquery'], setup);
    } else {
        setup(jQuery);
    }

})();

/*
 * jQuery FlexSlider v2.6.0
 * Copyright 2012 WooThemes
 * Contributing Author: Tyler Smith
 */
;
(function ($) {

    var focused = true;

    //FlexSlider: Object Instance
    $.flexslider = function (el, options) {
        var slider = $(el);

        // making variables public
        slider.vars = $.extend({}, $.flexslider.defaults, options);

        var namespace = slider.vars.namespace,
            msGesture = window.navigator && window.navigator.msPointerEnabled && window.MSGesture,
            touch = (("ontouchstart" in window) || msGesture || window.DocumentTouch && document instanceof DocumentTouch) && slider.vars.touch,
            // depricating this idea, as devices are being released with both of these events
            eventType = "click touchend MSPointerUp keyup",
            watchedEvent = "",
            watchedEventClearTimer,
            vertical = slider.vars.direction === "vertical",
            reverse = slider.vars.reverse,
            carousel = (slider.vars.itemWidth > 0),
            fade = slider.vars.animation === "fade",
            asNav = slider.vars.asNavFor !== "",
            methods = {};

        // Store a reference to the slider object
        $.data(el, "flexslider", slider);

        // Private slider methods
        methods = {
            init: function () {
                slider.animating = false;
                // Get current slide and make sure it is a number
                slider.currentSlide = parseInt((slider.vars.startAt ? slider.vars.startAt : 0), 10);
                if (isNaN(slider.currentSlide)) { slider.currentSlide = 0; }
                slider.animatingTo = slider.currentSlide;
                slider.atEnd = (slider.currentSlide === 0 || slider.currentSlide === slider.last);
                slider.containerSelector = slider.vars.selector.substr(0, slider.vars.selector.search(' '));
                slider.slides = $(slider.vars.selector, slider);
                slider.container = $(slider.containerSelector, slider);
                slider.count = slider.slides.length;
                // SYNC:
                slider.syncExists = $(slider.vars.sync).length > 0;
                // SLIDE:
                if (slider.vars.animation === "slide") { slider.vars.animation = "swing"; }
                slider.prop = (vertical) ? "top" : "marginLeft";
                slider.args = {};
                // SLIDESHOW:
                slider.manualPause = false;
                slider.stopped = false;
                //PAUSE WHEN INVISIBLE
                slider.started = false;
                slider.startTimeout = null;
                // TOUCH/USECSS:
                slider.transitions = !slider.vars.video && !fade && slider.vars.useCSS && (function () {
                    var obj = document.createElement('div'),
                        props = ['perspectiveProperty', 'WebkitPerspective', 'MozPerspective', 'OPerspective', 'msPerspective'];
                    for (var i in props) {
                        if (obj.style[props[i]] !== undefined) {
                            slider.pfx = props[i].replace('Perspective', '').toLowerCase();
                            slider.prop = "-" + slider.pfx + "-transform";
                            return true;
                        }
                    }
                    return false;
                }());
                slider.ensureAnimationEnd = '';
                // CONTROLSCONTAINER:
                if (slider.vars.controlsContainer !== "") slider.controlsContainer = $(slider.vars.controlsContainer).length > 0 && $(slider.vars.controlsContainer);
                // MANUAL:
                if (slider.vars.manualControls !== "") slider.manualControls = $(slider.vars.manualControls).length > 0 && $(slider.vars.manualControls);

                // CUSTOM DIRECTION NAV:
                if (slider.vars.customDirectionNav !== "") slider.customDirectionNav = $(slider.vars.customDirectionNav).length === 2 && $(slider.vars.customDirectionNav);

                // RANDOMIZE:
                if (slider.vars.randomize) {
                    slider.slides.sort(function () { return (Math.round(Math.random()) - 0.5); });
                    slider.container.empty().append(slider.slides);
                }

                slider.doMath();

                // INIT
                slider.setup("init");

                // CONTROLNAV:
                if (slider.vars.controlNav) { methods.controlNav.setup(); }

                // DIRECTIONNAV:
                if (slider.vars.directionNav) { methods.directionNav.setup(); }

                // KEYBOARD:
                if (slider.vars.keyboard && ($(slider.containerSelector).length === 1 || slider.vars.multipleKeyboard)) {
                    $(document).bind('keyup', function (event) {
                        var keycode = event.keyCode;
                        if (!slider.animating && (keycode === 39 || keycode === 37)) {
                            var target = (keycode === 39) ? slider.getTarget('next') :
                                         (keycode === 37) ? slider.getTarget('prev') : false;
                            slider.flexAnimate(target, slider.vars.pauseOnAction);
                        }
                    });
                }
                // MOUSEWHEEL:
                if (slider.vars.mousewheel) {
                    slider.bind('mousewheel', function (event, delta, deltaX, deltaY) {
                        event.preventDefault();
                        var target = (delta < 0) ? slider.getTarget('next') : slider.getTarget('prev');
                        slider.flexAnimate(target, slider.vars.pauseOnAction);
                    });
                }

                // PAUSEPLAY
                if (slider.vars.pausePlay) { methods.pausePlay.setup(); }

                //PAUSE WHEN INVISIBLE
                if (slider.vars.slideshow && slider.vars.pauseInvisible) { methods.pauseInvisible.init(); }

                // SLIDSESHOW
                if (slider.vars.slideshow) {
                    if (slider.vars.pauseOnHover) {
                        slider.hover(function () {
                            if (!slider.manualPlay && !slider.manualPause) { slider.pause(); }
                        }, function () {
                            if (!slider.manualPause && !slider.manualPlay && !slider.stopped) { slider.play(); }
                        });
                    }
                    // initialize animation
                    //If we're visible, or we don't use PageVisibility API
                    if (!slider.vars.pauseInvisible || !methods.pauseInvisible.isHidden()) {
                        (slider.vars.initDelay > 0) ? slider.startTimeout = setTimeout(slider.play, slider.vars.initDelay) : slider.play();
                    }
                }

                // ASNAV:
                if (asNav) { methods.asNav.setup(); }

                // TOUCH
                if (touch && slider.vars.touch) { methods.touch(); }

                // FADE&&SMOOTHHEIGHT || SLIDE:
                if (!fade || (fade && slider.vars.smoothHeight)) { $(window).bind("resize orientationchange focus", methods.resize); }

                slider.find("img").attr("draggable", "false");

                // API: start() Callback
                setTimeout(function () {
                    slider.vars.start(slider);
                }, 200);
            },
            asNav: {
                setup: function () {
                    slider.asNav = true;
                    slider.animatingTo = Math.floor(slider.currentSlide / slider.move);
                    slider.currentItem = slider.currentSlide;
                    slider.slides.removeClass(namespace + "active-slide").eq(slider.currentItem).addClass(namespace + "active-slide");
                    if (!msGesture) {
                        slider.slides.on(eventType, function (e) {
                            e.preventDefault();
                            var $slide = $(this),
                                target = $slide.index();
                            var posFromLeft = $slide.offset().left - $(slider).scrollLeft(); // Find position of slide relative to left of slider container
                            if (posFromLeft <= 0 && $slide.hasClass(namespace + 'active-slide')) {
                                slider.flexAnimate(slider.getTarget("prev"), true);
                            } else if (!$(slider.vars.asNavFor).data('flexslider').animating && !$slide.hasClass(namespace + "active-slide")) {
                                slider.direction = (slider.currentItem < target) ? "next" : "prev";
                                slider.flexAnimate(target, slider.vars.pauseOnAction, false, true, true);
                            }
                        });
                    } else {
                        el._slider = slider;
                        slider.slides.each(function () {
                            var that = this;
                            that._gesture = new MSGesture();
                            that._gesture.target = that;
                            that.addEventListener("MSPointerDown", function (e) {
                                e.preventDefault();
                                if (e.currentTarget._gesture) {
                                    e.currentTarget._gesture.addPointer(e.pointerId);
                                }
                            }, false);
                            that.addEventListener("MSGestureTap", function (e) {
                                e.preventDefault();
                                var $slide = $(this),
                                    target = $slide.index();
                                if (!$(slider.vars.asNavFor).data('flexslider').animating && !$slide.hasClass('active')) {
                                    slider.direction = (slider.currentItem < target) ? "next" : "prev";
                                    slider.flexAnimate(target, slider.vars.pauseOnAction, false, true, true);
                                }
                            });
                        });
                    }
                }
            },
            controlNav: {
                setup: function () {
                    if (!slider.manualControls) {
                        methods.controlNav.setupPaging();
                    } else { // MANUALCONTROLS:
                        methods.controlNav.setupManual();
                    }
                },
                setupPaging: function () {
                    var type = (slider.vars.controlNav === "thumbnails") ? 'control-thumbs' : 'control-paging',
                        j = 1,
                        item,
                        slide;

                    slider.controlNavScaffold = $('<ol class="' + namespace + 'control-nav ' + namespace + type + '"></ol>');

                    if (slider.pagingCount > 1) {
                        for (var i = 0; i < slider.pagingCount; i++) {
                            slide = slider.slides.eq(i);
                            if (undefined === slide.attr('data-thumb-alt')) { slide.attr('data-thumb-alt', ''); }
                            altText = ('' !== slide.attr('data-thumb-alt')) ? altText = ' alt="' + slide.attr('data-thumb-alt') + '"' : '';
                            item = (slider.vars.controlNav === "thumbnails") ? '<img src="' + slide.attr('data-thumb') + '"' + altText + '/>' : '<a href="#">' + j + '</a>';
                            if ('thumbnails' === slider.vars.controlNav && true === slider.vars.thumbCaptions) {
                                var captn = slide.attr('data-thumbcaption');
                                if ('' !== captn && undefined !== captn) { item += '<span class="' + namespace + 'caption">' + captn + '</span>'; }
                            }
                            slider.controlNavScaffold.append('<li>' + item + '</li>');
                            j++;
                        }
                    }

                    // CONTROLSCONTAINER:
                    (slider.controlsContainer) ? $(slider.controlsContainer).append(slider.controlNavScaffold) : slider.append(slider.controlNavScaffold);
                    methods.controlNav.set();

                    methods.controlNav.active();

                    slider.controlNavScaffold.delegate('a, img', eventType, function (event) {
                        event.preventDefault();

                        if (watchedEvent === "" || watchedEvent === event.type) {
                            var $this = $(this),
                                target = slider.controlNav.index($this);

                            if (!$this.hasClass(namespace + 'active')) {
                                slider.direction = (target > slider.currentSlide) ? "next" : "prev";
                                slider.flexAnimate(target, slider.vars.pauseOnAction);
                            }
                        }

                        // setup flags to prevent event duplication
                        if (watchedEvent === "") {
                            watchedEvent = event.type;
                        }
                        methods.setToClearWatchedEvent();

                    });
                },
                setupManual: function () {
                    slider.controlNav = slider.manualControls;
                    methods.controlNav.active();

                    slider.controlNav.bind(eventType, function (event) {
                        event.preventDefault();

                        if (watchedEvent === "" || watchedEvent === event.type) {
                            var $this = $(this),
                                target = slider.controlNav.index($this);

                            if (!$this.hasClass(namespace + 'active')) {
                                (target > slider.currentSlide) ? slider.direction = "next" : slider.direction = "prev";
                                slider.flexAnimate(target, slider.vars.pauseOnAction);
                            }
                        }

                        // setup flags to prevent event duplication
                        if (watchedEvent === "") {
                            watchedEvent = event.type;
                        }
                        methods.setToClearWatchedEvent();
                    });
                },
                set: function () {
                    var selector = (slider.vars.controlNav === "thumbnails") ? 'img' : 'a';
                    slider.controlNav = $('.' + namespace + 'control-nav li ' + selector, (slider.controlsContainer) ? slider.controlsContainer : slider);
                },
                active: function () {
                    slider.controlNav.removeClass(namespace + "active").eq(slider.animatingTo).addClass(namespace + "active");
                },
                update: function (action, pos) {
                    if (slider.pagingCount > 1 && action === "add") {
                        slider.controlNavScaffold.append($('<li><a href="#">' + slider.count + '</a></li>'));
                    } else if (slider.pagingCount === 1) {
                        slider.controlNavScaffold.find('li').remove();
                    } else {
                        slider.controlNav.eq(pos).closest('li').remove();
                    }
                    methods.controlNav.set();
                    (slider.pagingCount > 1 && slider.pagingCount !== slider.controlNav.length) ? slider.update(pos, action) : methods.controlNav.active();
                }
            },
            directionNav: {
                setup: function () {
                    var directionNavScaffold = $('<ul class="' + namespace + 'direction-nav"><li class="' + namespace + 'nav-prev"><a class="' + namespace + 'prev" href="#">' + slider.vars.prevText + '</a></li><li class="' + namespace + 'nav-next"><a class="' + namespace + 'next" href="#">' + slider.vars.nextText + '</a></li></ul>');

                    // CUSTOM DIRECTION NAV:
                    if (slider.customDirectionNav) {
                        slider.directionNav = slider.customDirectionNav;
                        // CONTROLSCONTAINER:
                    } else if (slider.controlsContainer) {
                        $(slider.controlsContainer).append(directionNavScaffold);
                        slider.directionNav = $('.' + namespace + 'direction-nav li a', slider.controlsContainer);
                    } else {
                        slider.append(directionNavScaffold);
                        slider.directionNav = $('.' + namespace + 'direction-nav li a', slider);
                    }

                    methods.directionNav.update();

                    slider.directionNav.bind(eventType, function (event) {
                        event.preventDefault();
                        var target;

                        if (watchedEvent === "" || watchedEvent === event.type) {
                            target = ($(this).hasClass(namespace + 'next')) ? slider.getTarget('next') : slider.getTarget('prev');
                            slider.flexAnimate(target, slider.vars.pauseOnAction);
                        }

                        // setup flags to prevent event duplication
                        if (watchedEvent === "") {
                            watchedEvent = event.type;
                        }
                        methods.setToClearWatchedEvent();
                    });
                },
                update: function () {
                    var disabledClass = namespace + 'disabled';
                    if (slider.pagingCount === 1) {
                        slider.directionNav.addClass(disabledClass).attr('tabindex', '-1');
                    } else if (!slider.vars.animationLoop) {
                        if (slider.animatingTo === 0) {
                            slider.directionNav.removeClass(disabledClass).filter('.' + namespace + "prev").addClass(disabledClass).attr('tabindex', '-1');
                        } else if (slider.animatingTo === slider.last) {
                            slider.directionNav.removeClass(disabledClass).filter('.' + namespace + "next").addClass(disabledClass).attr('tabindex', '-1');
                        } else {
                            slider.directionNav.removeClass(disabledClass).removeAttr('tabindex');
                        }
                    } else {
                        slider.directionNav.removeClass(disabledClass).removeAttr('tabindex');
                    }
                }
            },
            pausePlay: {
                setup: function () {
                    var pausePlayScaffold = $('<div class="' + namespace + 'pauseplay"><a href="#"></a></div>');

                    // CONTROLSCONTAINER:
                    if (slider.controlsContainer) {
                        slider.controlsContainer.append(pausePlayScaffold);
                        slider.pausePlay = $('.' + namespace + 'pauseplay a', slider.controlsContainer);
                    } else {
                        slider.append(pausePlayScaffold);
                        slider.pausePlay = $('.' + namespace + 'pauseplay a', slider);
                    }

                    methods.pausePlay.update((slider.vars.slideshow) ? namespace + 'pause' : namespace + 'play');

                    slider.pausePlay.bind(eventType, function (event) {
                        event.preventDefault();

                        if (watchedEvent === "" || watchedEvent === event.type) {
                            if ($(this).hasClass(namespace + 'pause')) {
                                slider.manualPause = true;
                                slider.manualPlay = false;
                                slider.pause();
                            } else {
                                slider.manualPause = false;
                                slider.manualPlay = true;
                                slider.play();
                            }
                        }

                        // setup flags to prevent event duplication
                        if (watchedEvent === "") {
                            watchedEvent = event.type;
                        }
                        methods.setToClearWatchedEvent();
                    });
                },
                update: function (state) {
                    (state === "play") ? slider.pausePlay.removeClass(namespace + 'pause').addClass(namespace + 'play').html(slider.vars.playText) : slider.pausePlay.removeClass(namespace + 'play').addClass(namespace + 'pause').html(slider.vars.pauseText);
                }
            },
            touch: function () {
                var startX,
                  startY,
                  offset,
                  cwidth,
                  dx,
                  startT,
                  onTouchStart,
                  onTouchMove,
                  onTouchEnd,
                  scrolling = false,
                  localX = 0,
                  localY = 0,
                  accDx = 0;

                if (!msGesture) {
                    onTouchStart = function (e) {
                        if (slider.animating) {
                            e.preventDefault();
                        } else if ((window.navigator.msPointerEnabled) || e.touches.length === 1) {
                            slider.pause();
                            // CAROUSEL:
                            cwidth = (vertical) ? slider.h : slider.w;
                            startT = Number(new Date());
                            // CAROUSEL:

                            // Local vars for X and Y points.
                            localX = e.touches[0].pageX;
                            localY = e.touches[0].pageY;

                            offset = (carousel && reverse && slider.animatingTo === slider.last) ? 0 :
                                     (carousel && reverse) ? slider.limit - (((slider.itemW + slider.vars.itemMargin) * slider.move) * slider.animatingTo) :
                                     (carousel && slider.currentSlide === slider.last) ? slider.limit :
                                     (carousel) ? ((slider.itemW + slider.vars.itemMargin) * slider.move) * slider.currentSlide :
                                     (reverse) ? (slider.last - slider.currentSlide + slider.cloneOffset) * cwidth : (slider.currentSlide + slider.cloneOffset) * cwidth;
                            startX = (vertical) ? localY : localX;
                            startY = (vertical) ? localX : localY;

                            el.addEventListener('touchmove', onTouchMove, false);
                            el.addEventListener('touchend', onTouchEnd, false);
                        }
                    };

                    onTouchMove = function (e) {
                        // Local vars for X and Y points.

                        localX = e.touches[0].pageX;
                        localY = e.touches[0].pageY;

                        dx = (vertical) ? startX - localY : startX - localX;
                        scrolling = (vertical) ? (Math.abs(dx) < Math.abs(localX - startY)) : (Math.abs(dx) < Math.abs(localY - startY));

                        var fxms = 500;

                        if (!scrolling || Number(new Date()) - startT > fxms) {
                            e.preventDefault();
                            if (!fade && slider.transitions) {
                                if (!slider.vars.animationLoop) {
                                    dx = dx / ((slider.currentSlide === 0 && dx < 0 || slider.currentSlide === slider.last && dx > 0) ? (Math.abs(dx) / cwidth + 2) : 1);
                                }
                                slider.setProps(offset + dx, "setTouch");
                            }
                        }
                    };

                    onTouchEnd = function (e) {
                        // finish the touch by undoing the touch session
                        el.removeEventListener('touchmove', onTouchMove, false);

                        if (slider.animatingTo === slider.currentSlide && !scrolling && !(dx === null)) {
                            var updateDx = (reverse) ? -dx : dx,
                                target = (updateDx > 0) ? slider.getTarget('next') : slider.getTarget('prev');

                            if (slider.canAdvance(target) && (Number(new Date()) - startT < 550 && Math.abs(updateDx) > 50 || Math.abs(updateDx) > cwidth / 2)) {
                                slider.flexAnimate(target, slider.vars.pauseOnAction);
                            } else {
                                if (!fade) { slider.flexAnimate(slider.currentSlide, slider.vars.pauseOnAction, true); }
                            }
                        }
                        el.removeEventListener('touchend', onTouchEnd, false);

                        startX = null;
                        startY = null;
                        dx = null;
                        offset = null;
                    };

                    el.addEventListener('touchstart', onTouchStart, false);
                } else {
                    el.style.msTouchAction = "none";
                    el._gesture = new MSGesture();
                    el._gesture.target = el;
                    el.addEventListener("MSPointerDown", onMSPointerDown, false);
                    el._slider = slider;
                    el.addEventListener("MSGestureChange", onMSGestureChange, false);
                    el.addEventListener("MSGestureEnd", onMSGestureEnd, false);

                    function onMSPointerDown(e) {
                        e.stopPropagation();
                        if (slider.animating) {
                            e.preventDefault();
                        } else {
                            slider.pause();
                            el._gesture.addPointer(e.pointerId);
                            accDx = 0;
                            cwidth = (vertical) ? slider.h : slider.w;
                            startT = Number(new Date());
                            // CAROUSEL:

                            offset = (carousel && reverse && slider.animatingTo === slider.last) ? 0 :
                                (carousel && reverse) ? slider.limit - (((slider.itemW + slider.vars.itemMargin) * slider.move) * slider.animatingTo) :
                                    (carousel && slider.currentSlide === slider.last) ? slider.limit :
                                        (carousel) ? ((slider.itemW + slider.vars.itemMargin) * slider.move) * slider.currentSlide :
                                            (reverse) ? (slider.last - slider.currentSlide + slider.cloneOffset) * cwidth : (slider.currentSlide + slider.cloneOffset) * cwidth;
                        }
                    }

                    function onMSGestureChange(e) {
                        e.stopPropagation();
                        var slider = e.target._slider;
                        if (!slider) {
                            return;
                        }
                        var transX = -e.translationX,
                            transY = -e.translationY;

                        //Accumulate translations.
                        accDx = accDx + ((vertical) ? transY : transX);
                        dx = accDx;
                        scrolling = (vertical) ? (Math.abs(accDx) < Math.abs(-transX)) : (Math.abs(accDx) < Math.abs(-transY));

                        if (e.detail === e.MSGESTURE_FLAG_INERTIA) {
                            setImmediate(function () {
                                el._gesture.stop();
                            });

                            return;
                        }

                        if (!scrolling || Number(new Date()) - startT > 500) {
                            e.preventDefault();
                            if (!fade && slider.transitions) {
                                if (!slider.vars.animationLoop) {
                                    dx = accDx / ((slider.currentSlide === 0 && accDx < 0 || slider.currentSlide === slider.last && accDx > 0) ? (Math.abs(accDx) / cwidth + 2) : 1);
                                }
                                slider.setProps(offset + dx, "setTouch");
                            }
                        }
                    }

                    function onMSGestureEnd(e) {
                        e.stopPropagation();
                        var slider = e.target._slider;
                        if (!slider) {
                            return;
                        }
                        if (slider.animatingTo === slider.currentSlide && !scrolling && !(dx === null)) {
                            var updateDx = (reverse) ? -dx : dx,
                                target = (updateDx > 0) ? slider.getTarget('next') : slider.getTarget('prev');

                            if (slider.canAdvance(target) && (Number(new Date()) - startT < 550 && Math.abs(updateDx) > 50 || Math.abs(updateDx) > cwidth / 2)) {
                                slider.flexAnimate(target, slider.vars.pauseOnAction);
                            } else {
                                if (!fade) { slider.flexAnimate(slider.currentSlide, slider.vars.pauseOnAction, true); }
                            }
                        }

                        startX = null;
                        startY = null;
                        dx = null;
                        offset = null;
                        accDx = 0;
                    }
                }
            },
            resize: function () {
                if (!slider.animating && slider.is(':visible')) {
                    if (!carousel) { slider.doMath(); }

                    if (fade) {
                        // SMOOTH HEIGHT:
                        methods.smoothHeight();
                    } else if (carousel) { //CAROUSEL:
                        slider.slides.width(slider.computedW);
                        slider.update(slider.pagingCount);
                        slider.setProps();
                    }
                    else if (vertical) { //VERTICAL:
                        slider.viewport.height(slider.h);
                        slider.setProps(slider.h, "setTotal");
                    } else {
                        // SMOOTH HEIGHT:
                        if (slider.vars.smoothHeight) { methods.smoothHeight(); }
                        slider.newSlides.width(slider.computedW);
                        slider.setProps(slider.computedW, "setTotal");
                    }
                }
            },
            smoothHeight: function (dur) {
                if (!vertical || fade) {
                    var $obj = (fade) ? slider : slider.viewport;
                    (dur) ? $obj.animate({ "height": slider.slides.eq(slider.animatingTo).height() }, dur) : $obj.height(slider.slides.eq(slider.animatingTo).height());
                }
            },
            sync: function (action) {
                var $obj = $(slider.vars.sync).data("flexslider"),
                    target = slider.animatingTo;

                switch (action) {
                    case "animate": $obj.flexAnimate(target, slider.vars.pauseOnAction, false, true); break;
                    case "play": if (!$obj.playing && !$obj.asNav) { $obj.play(); } break;
                    case "pause": $obj.pause(); break;
                }
            },
            uniqueID: function ($clone) {
                // Append _clone to current level and children elements with id attributes
                $clone.filter('[id]').add($clone.find('[id]')).each(function () {
                    var $this = $(this);
                    $this.attr('id', $this.attr('id') + '_clone');
                });
                return $clone;
            },
            pauseInvisible: {
                visProp: null,
                init: function () {
                    var visProp = methods.pauseInvisible.getHiddenProp();
                    if (visProp) {
                        var evtname = visProp.replace(/[H|h]idden/, '') + 'visibilitychange';
                        document.addEventListener(evtname, function () {
                            if (methods.pauseInvisible.isHidden()) {
                                if (slider.startTimeout) {
                                    clearTimeout(slider.startTimeout); //If clock is ticking, stop timer and prevent from starting while invisible
                                } else {
                                    slider.pause(); //Or just pause
                                }
                            }
                            else {
                                if (slider.started) {
                                    slider.play(); //Initiated before, just play
                                } else {
                                    if (slider.vars.initDelay > 0) {
                                        setTimeout(slider.play, slider.vars.initDelay);
                                    } else {
                                        slider.play(); //Didn't init before: simply init or wait for it
                                    }
                                }
                            }
                        });
                    }
                },
                isHidden: function () {
                    var prop = methods.pauseInvisible.getHiddenProp();
                    if (!prop) {
                        return false;
                    }
                    return document[prop];
                },
                getHiddenProp: function () {
                    var prefixes = ['webkit', 'moz', 'ms', 'o'];
                    // if 'hidden' is natively supported just return it
                    if ('hidden' in document) {
                        return 'hidden';
                    }
                    // otherwise loop over all the known prefixes until we find one
                    for (var i = 0; i < prefixes.length; i++) {
                        if ((prefixes[i] + 'Hidden') in document) {
                            return prefixes[i] + 'Hidden';
                        }
                    }
                    // otherwise it's not supported
                    return null;
                }
            },
            setToClearWatchedEvent: function () {
                clearTimeout(watchedEventClearTimer);
                watchedEventClearTimer = setTimeout(function () {
                    watchedEvent = "";
                }, 3000);
            }
        };

        // public methods
        slider.flexAnimate = function (target, pause, override, withSync, fromNav) {
            if (!slider.vars.animationLoop && target !== slider.currentSlide) {
                slider.direction = (target > slider.currentSlide) ? "next" : "prev";
            }

            if (asNav && slider.pagingCount === 1) slider.direction = (slider.currentItem < target) ? "next" : "prev";

            if (!slider.animating && (slider.canAdvance(target, fromNav) || override) && slider.is(":visible")) {
                if (asNav && withSync) {
                    var master = $(slider.vars.asNavFor).data('flexslider');
                    slider.atEnd = target === 0 || target === slider.count - 1;
                    master.flexAnimate(target, true, false, true, fromNav);
                    slider.direction = (slider.currentItem < target) ? "next" : "prev";
                    master.direction = slider.direction;

                    if (Math.ceil((target + 1) / slider.visible) - 1 !== slider.currentSlide && target !== 0) {
                        slider.currentItem = target;
                        slider.slides.removeClass(namespace + "active-slide").eq(target).addClass(namespace + "active-slide");
                        target = Math.floor(target / slider.visible);
                    } else {
                        slider.currentItem = target;
                        slider.slides.removeClass(namespace + "active-slide").eq(target).addClass(namespace + "active-slide");
                        return false;
                    }
                }

                slider.animating = true;
                slider.animatingTo = target;

                // SLIDESHOW:
                if (pause) { slider.pause(); }

                // API: before() animation Callback
                slider.vars.before(slider);

                // SYNC:
                if (slider.syncExists && !fromNav) { methods.sync("animate"); }

                // CONTROLNAV
                if (slider.vars.controlNav) { methods.controlNav.active(); }

                // !CAROUSEL:
                // CANDIDATE: slide active class (for add/remove slide)
                if (!carousel) { slider.slides.removeClass(namespace + 'active-slide').eq(target).addClass(namespace + 'active-slide'); }

                // INFINITE LOOP:
                // CANDIDATE: atEnd
                slider.atEnd = target === 0 || target === slider.last;

                // DIRECTIONNAV:
                if (slider.vars.directionNav) { methods.directionNav.update(); }

                if (target === slider.last) {
                    // API: end() of cycle Callback
                    slider.vars.end(slider);
                    // SLIDESHOW && !INFINITE LOOP:
                    if (!slider.vars.animationLoop) { slider.pause(); }
                }

                // SLIDE:
                if (!fade) {
                    var dimension = (vertical) ? slider.slides.filter(':first').height() : slider.computedW,
                        margin, slideString, calcNext;

                    // INFINITE LOOP / REVERSE:
                    if (carousel) {
                        margin = slider.vars.itemMargin;
                        calcNext = ((slider.itemW + margin) * slider.move) * slider.animatingTo;
                        slideString = (calcNext > slider.limit && slider.visible !== 1) ? slider.limit : calcNext;
                    } else if (slider.currentSlide === 0 && target === slider.count - 1 && slider.vars.animationLoop && slider.direction !== "next") {
                        slideString = (reverse) ? (slider.count + slider.cloneOffset) * dimension : 0;
                    } else if (slider.currentSlide === slider.last && target === 0 && slider.vars.animationLoop && slider.direction !== "prev") {
                        slideString = (reverse) ? 0 : (slider.count + 1) * dimension;
                    } else {
                        slideString = (reverse) ? ((slider.count - 1) - target + slider.cloneOffset) * dimension : (target + slider.cloneOffset) * dimension;
                    }
                    slider.setProps(slideString, "", slider.vars.animationSpeed);
                    if (slider.transitions) {
                        if (!slider.vars.animationLoop || !slider.atEnd) {
                            slider.animating = false;
                            slider.currentSlide = slider.animatingTo;
                        }

                        // Unbind previous transitionEnd events and re-bind new transitionEnd event
                        slider.container.unbind("webkitTransitionEnd transitionend");
                        slider.container.bind("webkitTransitionEnd transitionend", function () {
                            clearTimeout(slider.ensureAnimationEnd);
                            slider.wrapup(dimension);
                        });

                        // Insurance for the ever-so-fickle transitionEnd event
                        clearTimeout(slider.ensureAnimationEnd);
                        slider.ensureAnimationEnd = setTimeout(function () {
                            slider.wrapup(dimension);
                        }, slider.vars.animationSpeed + 100);

                    } else {
                        slider.container.animate(slider.args, slider.vars.animationSpeed, slider.vars.easing, function () {
                            slider.wrapup(dimension);
                        });
                    }
                } else { // FADE:
                    if (!touch) {
                        //slider.slides.eq(slider.currentSlide).fadeOut(slider.vars.animationSpeed, slider.vars.easing);
                        //slider.slides.eq(target).fadeIn(slider.vars.animationSpeed, slider.vars.easing, slider.wrapup);

                        slider.slides.eq(slider.currentSlide).css({ "zIndex": 1 }).animate({ "opacity": 0 }, slider.vars.animationSpeed, slider.vars.easing);
                        slider.slides.eq(target).css({ "zIndex": 2 }).animate({ "opacity": 1 }, slider.vars.animationSpeed, slider.vars.easing, slider.wrapup);

                    } else {
                        slider.slides.eq(slider.currentSlide).css({ "opacity": 0, "zIndex": 1 });
                        slider.slides.eq(target).css({ "opacity": 1, "zIndex": 2 });
                        slider.wrapup(dimension);
                    }
                }
                // SMOOTH HEIGHT:
                if (slider.vars.smoothHeight) { methods.smoothHeight(slider.vars.animationSpeed); }
            }
        };
        slider.wrapup = function (dimension) {
            // SLIDE:
            if (!fade && !carousel) {
                if (slider.currentSlide === 0 && slider.animatingTo === slider.last && slider.vars.animationLoop) {
                    slider.setProps(dimension, "jumpEnd");
                } else if (slider.currentSlide === slider.last && slider.animatingTo === 0 && slider.vars.animationLoop) {
                    slider.setProps(dimension, "jumpStart");
                }
            }
            slider.animating = false;
            slider.currentSlide = slider.animatingTo;
            // API: after() animation Callback
            slider.vars.after(slider);
        };

        // SLIDESHOW:
        slider.animateSlides = function () {
            if (!slider.animating && focused) { slider.flexAnimate(slider.getTarget("next")); }
        };
        // SLIDESHOW:
        slider.pause = function () {
            clearInterval(slider.animatedSlides);
            slider.animatedSlides = null;
            slider.playing = false;
            // PAUSEPLAY:
            if (slider.vars.pausePlay) { methods.pausePlay.update("play"); }
            // SYNC:
            if (slider.syncExists) { methods.sync("pause"); }
        };
        // SLIDESHOW:
        slider.play = function () {
            if (slider.playing) { clearInterval(slider.animatedSlides); }
            slider.animatedSlides = slider.animatedSlides || setInterval(slider.animateSlides, slider.vars.slideshowSpeed);
            slider.started = slider.playing = true;
            // PAUSEPLAY:
            if (slider.vars.pausePlay) { methods.pausePlay.update("pause"); }
            // SYNC:
            if (slider.syncExists) { methods.sync("play"); }
        };
        // STOP:
        slider.stop = function () {
            slider.pause();
            slider.stopped = true;
        };
        slider.canAdvance = function (target, fromNav) {
            // ASNAV:
            var last = (asNav) ? slider.pagingCount - 1 : slider.last;
            return (fromNav) ? true :
                   (asNav && slider.currentItem === slider.count - 1 && target === 0 && slider.direction === "prev") ? true :
                   (asNav && slider.currentItem === 0 && target === slider.pagingCount - 1 && slider.direction !== "next") ? false :
                   (target === slider.currentSlide && !asNav) ? false :
                   (slider.vars.animationLoop) ? true :
                   (slider.atEnd && slider.currentSlide === 0 && target === last && slider.direction !== "next") ? false :
                   (slider.atEnd && slider.currentSlide === last && target === 0 && slider.direction === "next") ? false :
                   true;
        };
        slider.getTarget = function (dir) {
            slider.direction = dir;
            if (dir === "next") {
                return (slider.currentSlide === slider.last) ? 0 : slider.currentSlide + 1;
            } else {
                return (slider.currentSlide === 0) ? slider.last : slider.currentSlide - 1;
            }
        };

        // SLIDE:
        slider.setProps = function (pos, special, dur) {
            var target = (function () {
                var posCheck = (pos) ? pos : ((slider.itemW + slider.vars.itemMargin) * slider.move) * slider.animatingTo,
                    posCalc = (function () {
                        if (carousel) {
                            return (special === "setTouch") ? pos :
                                   (reverse && slider.animatingTo === slider.last) ? 0 :
                                   (reverse) ? slider.limit - (((slider.itemW + slider.vars.itemMargin) * slider.move) * slider.animatingTo) :
                                   (slider.animatingTo === slider.last) ? slider.limit : posCheck;
                        } else {
                            switch (special) {
                                case "setTotal": return (reverse) ? ((slider.count - 1) - slider.currentSlide + slider.cloneOffset) * pos : (slider.currentSlide + slider.cloneOffset) * pos;
                                case "setTouch": return (reverse) ? pos : pos;
                                case "jumpEnd": return (reverse) ? pos : slider.count * pos;
                                case "jumpStart": return (reverse) ? slider.count * pos : pos;
                                default: return pos;
                            }
                        }
                    }());

                return (posCalc * -1) + "px";
            }());

            if (slider.transitions) {
                target = (vertical) ? "translate3d(0," + target + ",0)" : "translate3d(" + target + ",0,0)";
                dur = (dur !== undefined) ? (dur / 1000) + "s" : "0s";
                slider.container.css("-" + slider.pfx + "-transition-duration", dur);
                slider.container.css("transition-duration", dur);
                xuanxuan = slider;
            }

            slider.args[slider.prop] = target;
            if (slider.transitions || dur === undefined) { slider.container.css(slider.args); }

            slider.container.css('transform', target);
        };

        slider.setup = function (type) {
            // SLIDE:
            if (!fade) {
                var sliderOffset, arr;

                if (type === "init") {
                    slider.viewport = $('<div class="' + namespace + 'viewport"></div>').css({ "overflow": "hidden", "position": "relative" }).appendTo(slider).append(slider.container);
                    // INFINITE LOOP:
                    slider.cloneCount = 0;
                    slider.cloneOffset = 0;
                    // REVERSE:
                    if (reverse) {
                        arr = $.makeArray(slider.slides).reverse();
                        slider.slides = $(arr);
                        slider.container.empty().append(slider.slides);
                    }
                }
                // INFINITE LOOP && !CAROUSEL:
                if (slider.vars.animationLoop && !carousel) {
                    slider.cloneCount = 2;
                    slider.cloneOffset = 1;
                    // clear out old clones
                    if (type !== "init") { slider.container.find('.clone').remove(); }
                    slider.container.append(methods.uniqueID(slider.slides.first().clone().addClass('clone')).attr('aria-hidden', 'true'))
                                    .prepend(methods.uniqueID(slider.slides.last().clone().addClass('clone')).attr('aria-hidden', 'true'));
                }
                slider.newSlides = $(slider.vars.selector, slider);

                sliderOffset = (reverse) ? slider.count - 1 - slider.currentSlide + slider.cloneOffset : slider.currentSlide + slider.cloneOffset;
                // VERTICAL:
                if (vertical && !carousel) {
                    slider.container.height((slider.count + slider.cloneCount) * 200 + "%").css("position", "absolute").width("100%");
                    setTimeout(function () {
                        slider.newSlides.css({ "display": "block" });
                        slider.doMath();
                        slider.viewport.height(slider.h);
                        slider.setProps(sliderOffset * slider.h, "init");
                    }, (type === "init") ? 100 : 0);
                } else {
                    slider.container.width((slider.count + slider.cloneCount) * 200 + "%");
                    slider.setProps(sliderOffset * slider.computedW, "init");
                    setTimeout(function () {
                        slider.doMath();
                        slider.newSlides.css({ "width": slider.computedW, "marginRight": slider.computedM, "float": "left", "display": "block" });
                        // SMOOTH HEIGHT:
                        if (slider.vars.smoothHeight) { methods.smoothHeight(); }
                    }, (type === "init") ? 100 : 0);
                }
            } else { // FADE:
                slider.slides.css({ "width": "100%", "float": "left", "marginRight": "-100%", "position": "relative" });
                if (type === "init") {
                    if (!touch) {
                        //slider.slides.eq(slider.currentSlide).fadeIn(slider.vars.animationSpeed, slider.vars.easing);
                        if (slider.vars.fadeFirstSlide == false) {
                            slider.slides.css({ "opacity": 0, "display": "block", "zIndex": 1 }).eq(slider.currentSlide).css({ "zIndex": 2 }).css({ "opacity": 1 });
                        } else {
                            slider.slides.css({ "opacity": 0, "display": "block", "zIndex": 1 }).eq(slider.currentSlide).css({ "zIndex": 2 }).animate({ "opacity": 1 }, slider.vars.animationSpeed, slider.vars.easing);
                        }
                    } else {
                        slider.slides.css({ "opacity": 0, "display": "block", "webkitTransition": "opacity " + slider.vars.animationSpeed / 1000 + "s ease", "zIndex": 1 }).eq(slider.currentSlide).css({ "opacity": 1, "zIndex": 2 });
                    }
                }
                // SMOOTH HEIGHT:
                if (slider.vars.smoothHeight) { methods.smoothHeight(); }
            }
            // !CAROUSEL:
            // CANDIDATE: active slide
            if (!carousel) { slider.slides.removeClass(namespace + "active-slide").eq(slider.currentSlide).addClass(namespace + "active-slide"); }

            //FlexSlider: init() Callback
            slider.vars.init(slider);
        };

        slider.doMath = function () {
            var slide = slider.slides.first(),
                slideMargin = slider.vars.itemMargin,
                minItems = slider.vars.minItems,
                maxItems = slider.vars.maxItems;

            slider.w = (slider.viewport === undefined) ? slider.width() : slider.viewport.width();
            slider.h = slide.height();
            slider.boxPadding = slide.outerWidth() - slide.width();

            // CAROUSEL:
            if (carousel) {
                slider.itemT = slider.vars.itemWidth + slideMargin;
                slider.itemM = slideMargin;
                slider.minW = (minItems) ? minItems * slider.itemT : slider.w;
                slider.maxW = (maxItems) ? (maxItems * slider.itemT) - slideMargin : slider.w;
                slider.itemW = (slider.minW > slider.w) ? (slider.w - (slideMargin * (minItems - 1))) / minItems :
                               (slider.maxW < slider.w) ? (slider.w - (slideMargin * (maxItems - 1))) / maxItems :
                               (slider.vars.itemWidth > slider.w) ? slider.w : slider.vars.itemWidth;

                slider.visible = Math.floor(slider.w / (slider.itemW));
                slider.move = (slider.vars.move > 0 && slider.vars.move < slider.visible) ? slider.vars.move : slider.visible;
                slider.pagingCount = Math.ceil(((slider.count - slider.visible) / slider.move) + 1);
                slider.last = slider.pagingCount - 1;
                slider.limit = (slider.pagingCount === 1) ? 0 :
                               (slider.vars.itemWidth > slider.w) ? (slider.itemW * (slider.count - 1)) + (slideMargin * (slider.count - 1)) : ((slider.itemW + slideMargin) * slider.count) - slider.w - slideMargin;
            } else {
                slider.itemW = slider.w;
                slider.itemM = slideMargin;
                slider.pagingCount = slider.count;
                slider.last = slider.count - 1;
            }
            slider.computedW = slider.itemW - slider.boxPadding;
            slider.computedM = slider.itemM;
        };

        slider.update = function (pos, action) {
            slider.doMath();

            // update currentSlide and slider.animatingTo if necessary
            if (!carousel) {
                if (pos < slider.currentSlide) {
                    slider.currentSlide += 1;
                } else if (pos <= slider.currentSlide && pos !== 0) {
                    slider.currentSlide -= 1;
                }
                slider.animatingTo = slider.currentSlide;
            }

            // update controlNav
            if (slider.vars.controlNav && !slider.manualControls) {
                if ((action === "add" && !carousel) || slider.pagingCount > slider.controlNav.length) {
                    methods.controlNav.update("add");
                } else if ((action === "remove" && !carousel) || slider.pagingCount < slider.controlNav.length) {
                    if (carousel && slider.currentSlide > slider.last) {
                        slider.currentSlide -= 1;
                        slider.animatingTo -= 1;
                    }
                    methods.controlNav.update("remove", slider.last);
                }
            }
            // update directionNav
            if (slider.vars.directionNav) { methods.directionNav.update(); }

        };

        slider.addSlide = function (obj, pos) {
            var $obj = $(obj);

            slider.count += 1;
            slider.last = slider.count - 1;

            // append new slide
            if (vertical && reverse) {
                (pos !== undefined) ? slider.slides.eq(slider.count - pos).after($obj) : slider.container.prepend($obj);
            } else {
                (pos !== undefined) ? slider.slides.eq(pos).before($obj) : slider.container.append($obj);
            }

            // update currentSlide, animatingTo, controlNav, and directionNav
            slider.update(pos, "add");

            // update slider.slides
            slider.slides = $(slider.vars.selector + ':not(.clone)', slider);
            // re-setup the slider to accomdate new slide
            slider.setup();

            //FlexSlider: added() Callback
            slider.vars.added(slider);
        };
        slider.removeSlide = function (obj) {
            var pos = (isNaN(obj)) ? slider.slides.index($(obj)) : obj;

            // update count
            slider.count -= 1;
            slider.last = slider.count - 1;

            // remove slide
            if (isNaN(obj)) {
                $(obj, slider.slides).remove();
            } else {
                (vertical && reverse) ? slider.slides.eq(slider.last).remove() : slider.slides.eq(obj).remove();
            }

            // update currentSlide, animatingTo, controlNav, and directionNav
            slider.doMath();
            slider.update(pos, "remove");

            // update slider.slides
            slider.slides = $(slider.vars.selector + ':not(.clone)', slider);
            // re-setup the slider to accomdate new slide
            slider.setup();

            // FlexSlider: removed() Callback
            slider.vars.removed(slider);
        };

        //FlexSlider: Initialize
        methods.init();
    };

    // Ensure the slider isn't focussed if the window loses focus.
    $(window).blur(function (e) {
        focused = false;
    }).focus(function (e) {
        focused = true;
    });

    //FlexSlider: Default Settings
    $.flexslider.defaults = {
        namespace: "flex-",             //{NEW} String: Prefix string attached to the class of every element generated by the plugin
        selector: ".slides > li",       //{NEW} Selector: Must match a simple pattern. '{container} > {slide}' -- Ignore pattern at your own peril
        animation: "fade",              //String: Select your animation type, "fade" or "slide"
        easing: "swing",                //{NEW} String: Determines the easing method used in jQuery transitions. jQuery easing plugin is supported!
        direction: "horizontal",        //String: Select the sliding direction, "horizontal" or "vertical"
        reverse: false,                 //{NEW} Boolean: Reverse the animation direction
        animationLoop: true,            //Boolean: Should the animation loop? If false, directionNav will received "disable" classes at either end
        smoothHeight: false,            //{NEW} Boolean: Allow height of the slider to animate smoothly in horizontal mode
        startAt: 0,                     //Integer: The slide that the slider should start on. Array notation (0 = first slide)
        slideshow: true,                //Boolean: Animate slider automatically
        slideshowSpeed: 7000,           //Integer: Set the speed of the slideshow cycling, in milliseconds
        animationSpeed: 600,            //Integer: Set the speed of animations, in milliseconds
        initDelay: 0,                   //{NEW} Integer: Set an initialization delay, in milliseconds
        randomize: false,               //Boolean: Randomize slide order
        fadeFirstSlide: true,           //Boolean: Fade in the first slide when animation type is "fade"
        thumbCaptions: false,           //Boolean: Whether or not to put captions on thumbnails when using the "thumbnails" controlNav.

        // Usability features
        pauseOnAction: true,            //Boolean: Pause the slideshow when interacting with control elements, highly recommended.
        pauseOnHover: false,            //Boolean: Pause the slideshow when hovering over slider, then resume when no longer hovering
        pauseInvisible: true,   		//{NEW} Boolean: Pause the slideshow when tab is invisible, resume when visible. Provides better UX, lower CPU usage.
        useCSS: true,                   //{NEW} Boolean: Slider will use CSS3 transitions if available
        touch: true,                    //{NEW} Boolean: Allow touch swipe navigation of the slider on touch-enabled devices
        video: false,                   //{NEW} Boolean: If using video in the slider, will prevent CSS3 3D Transforms to avoid graphical glitches

        // Primary Controls
        controlNav: true,               //Boolean: Create navigation for paging control of each slide? Note: Leave true for manualControls usage
        directionNav: true,             //Boolean: Create navigation for previous/next navigation? (true/false)
        prevText: "Previous",           //String: Set the text for the "previous" directionNav item
        nextText: "Next",               //String: Set the text for the "next" directionNav item

        // Secondary Navigation
        keyboard: true,                 //Boolean: Allow slider navigating via keyboard left/right keys
        multipleKeyboard: false,        //{NEW} Boolean: Allow keyboard navigation to affect multiple sliders. Default behavior cuts out keyboard navigation with more than one slider present.
        mousewheel: false,              //{UPDATED} Boolean: Requires jquery.mousewheel.js (https://github.com/brandonaaron/jquery-mousewheel) - Allows slider navigating via mousewheel
        pausePlay: false,               //Boolean: Create pause/play dynamic element
        pauseText: "Pause",             //String: Set the text for the "pause" pausePlay item
        playText: "Play",               //String: Set the text for the "play" pausePlay item

        // Special properties
        controlsContainer: "",          //{UPDATED} jQuery Object/Selector: Declare which container the navigation elements should be appended too. Default container is the FlexSlider element. Example use would be $(".flexslider-container"). Property is ignored if given element is not found.
        manualControls: "",             //{UPDATED} jQuery Object/Selector: Declare custom control navigation. Examples would be $(".flex-control-nav li") or "#tabs-nav li img", etc. The number of elements in your controlNav should match the number of slides/tabs.
        customDirectionNav: "",         //{NEW} jQuery Object/Selector: Custom prev / next button. Must be two jQuery elements. In order to make the events work they have to have the classes "prev" and "next" (plus namespace)
        sync: "",                       //{NEW} Selector: Mirror the actions performed on this slider with another slider. Use with care.
        asNavFor: "",                   //{NEW} Selector: Internal property exposed for turning the slider into a thumbnail navigation for another slider

        // Carousel Options
        itemWidth: 0,                   //{NEW} Integer: Box-model width of individual carousel items, including horizontal borders and padding.
        itemMargin: 0,                  //{NEW} Integer: Margin between carousel items.
        minItems: 1,                    //{NEW} Integer: Minimum number of carousel items that should be visible. Items will resize fluidly when below this.
        maxItems: 0,                    //{NEW} Integer: Maxmimum number of carousel items that should be visible. Items will resize fluidly when above this limit.
        move: 0,                        //{NEW} Integer: Number of carousel items that should move on animation. If 0, slider will move all visible items.
        allowOneSlide: true,           //{NEW} Boolean: Whether or not to allow a slider comprised of a single slide

        // Callback API
        start: function () { },            //Callback: function(slider) - Fires when the slider loads the first slide
        before: function () { },           //Callback: function(slider) - Fires asynchronously with each slider animation
        after: function () { },            //Callback: function(slider) - Fires after each slider animation completes
        end: function () { },              //Callback: function(slider) - Fires when the slider reaches the last slide (asynchronous)
        added: function () { },            //{NEW} Callback: function(slider) - Fires after a slide is added
        removed: function () { },           //{NEW} Callback: function(slider) - Fires after a slide is removed
        init: function () { }             //{NEW} Callback: function(slider) - Fires after the slider is initially setup
    };

    //FlexSlider: Plugin Function
    $.fn.flexslider = function (options) {
        if (options === undefined) { options = {}; }

        if (typeof options === "object") {
            return this.each(function () {
                var $this = $(this),
                    selector = (options.selector) ? options.selector : ".slides > li",
                    $slides = $this.find(selector);

                if (($slides.length === 1 && options.allowOneSlide === true) || $slides.length === 0) {
                    $slides.fadeIn(400);
                    if (options.start) { options.start($this); }
                } else if ($this.data('flexslider') === undefined) {
                    new $.flexslider(this, options);
                }
            });
        } else {
            // Helper strings to quickly perform functions on the slider
            var $slider = $(this).data('flexslider');
            switch (options) {
                case "play": $slider.play(); break;
                case "pause": $slider.pause(); break;
                case "stop": $slider.stop(); break;
                case "next": $slider.flexAnimate($slider.getTarget("next"), true); break;
                case "prev":
                case "previous": $slider.flexAnimate($slider.getTarget("prev"), true); break;
                default: if (typeof options === "number") { $slider.flexAnimate(options, true); }
            }
        }
    };
})(jQuery);

/*!
 * jCarousel - Riding carousels with jQuery
 *   http://sorgalla.com/jcarousel/
 *
 * Copyright (c) 2006 Jan Sorgalla (http://sorgalla.com)
 * Dual licensed under the MIT (http://www.opensource.org/licenses/mit-license.php)
 * and GPL (http://www.opensource.org/licenses/gpl-license.php) licenses.
 *
 * Built on top of the jQuery library
 *   http://jquery.com
 *
 * Inspired by the "Carousel Component" by Bill Scott
 *   http://billwscott.com/carousel/
 *
 *   Version 0.2.9
 */

(function (a) { var b = { vertical: !1, rtl: !1, start: 1, offset: 1, size: null, scroll: 3, visible: null, animation: "normal", easing: "swing", auto: 0, wrap: null, initCallback: null, setupCallback: null, reloadCallback: null, itemLoadCallback: null, itemFirstInCallback: null, itemFirstOutCallback: null, itemLastInCallback: null, itemLastOutCallback: null, itemVisibleInCallback: null, itemVisibleOutCallback: null, animationStepCallback: null, buttonNextHTML: "<div></div>", buttonPrevHTML: "<div></div>", buttonNextEvent: "click", buttonPrevEvent: "click", buttonNextCallback: null, buttonPrevCallback: null, itemFallbackDimension: null }, c = !1; a(window).bind("load.jcarousel", function () { c = !0 }), a.jcarousel = function (e, f) { this.options = a.extend({}, b, f || {}), this.locked = !1, this.autoStopped = !1, this.container = null, this.clip = null, this.list = null, this.buttonNext = null, this.buttonPrev = null, this.buttonNextState = null, this.buttonPrevState = null, f && void 0 !== f.rtl || (this.options.rtl = "rtl" == (a(e).attr("dir") || a("html").attr("dir") || "").toLowerCase()), this.wh = this.options.vertical ? "height" : "width", this.lt = this.options.vertical ? "top" : this.options.rtl ? "right" : "left"; for (var g = "", h = e.className.split(" "), i = 0; h.length > i; i++) if (-1 != h[i].indexOf("jcarousel-skin")) { a(e).removeClass(h[i]), g = h[i]; break } "UL" == e.nodeName.toUpperCase() || "OL" == e.nodeName.toUpperCase() ? (this.list = a(e), this.clip = this.list.parents(".jcarousel-clip"), this.container = this.list.parents(".jcarousel-container")) : (this.container = a(e), this.list = this.container.find("ul,ol").eq(0), this.clip = this.container.find(".jcarousel-clip")), 0 === this.clip.size() && (this.clip = this.list.wrap("<div></div>").parent()), 0 === this.container.size() && (this.container = this.clip.wrap("<div></div>").parent()), "" !== g && -1 == this.container.parent()[0].className.indexOf("jcarousel-skin") && this.container.wrap('<div class=" ' + g + '"></div>'), this.buttonPrev = a(".jcarousel-prev", this.container), 0 === this.buttonPrev.size() && null !== this.options.buttonPrevHTML && (this.buttonPrev = a(this.options.buttonPrevHTML).appendTo(this.container)), this.buttonPrev.addClass(this.className("jcarousel-prev")), this.buttonNext = a(".jcarousel-next", this.container), 0 === this.buttonNext.size() && null !== this.options.buttonNextHTML && (this.buttonNext = a(this.options.buttonNextHTML).appendTo(this.container)), this.buttonNext.addClass(this.className("jcarousel-next")), this.clip.addClass(this.className("jcarousel-clip")).css({ position: "relative" }), this.list.addClass(this.className("jcarousel-list")).css({ overflow: "hidden", position: "relative", top: 0, margin: 0, padding: 0 }).css(this.options.rtl ? "right" : "left", 0), this.container.addClass(this.className("jcarousel-container")).css({ position: "relative" }), !this.options.vertical && this.options.rtl && this.container.addClass("jcarousel-direction-rtl").attr("dir", "rtl"); var j = null !== this.options.visible ? Math.ceil(this.clipping() / this.options.visible) : null, k = this.list.children("li"), l = this; if (k.size() > 0) { var m = 0, n = this.options.offset; k.each(function () { l.format(this, n++), m += l.dimension(this, j) }), this.list.css(this.wh, m + 100 + "px"), f && void 0 !== f.size || (this.options.size = k.size()) } this.container.css("display", "block"), this.buttonNext.css("display", "block"), this.buttonPrev.css("display", "block"), this.funcNext = function () { return l.next(), !1 }, this.funcPrev = function () { return l.prev(), !1 }, this.funcResize = function () { l.resizeTimer && clearTimeout(l.resizeTimer), l.resizeTimer = setTimeout(function () { l.reload() }, 100) }, null !== this.options.initCallback && this.options.initCallback(this, "init"), !c && d.isSafari() ? (this.buttons(!1, !1), a(window).bind("load.jcarousel", function () { l.setup() })) : this.setup() }; var d = a.jcarousel; d.fn = d.prototype = { jcarousel: "0.2.9" }, d.fn.extend = d.extend = a.extend, d.fn.extend({ setup: function () { if (this.first = null, this.last = null, this.prevFirst = null, this.prevLast = null, this.animating = !1, this.timer = null, this.resizeTimer = null, this.tail = null, this.inTail = !1, !this.locked) { this.list.css(this.lt, this.pos(this.options.offset) + "px"); var b = this.pos(this.options.start, !0); this.prevFirst = this.prevLast = null, this.animate(b, !1), a(window).unbind("resize.jcarousel", this.funcResize).bind("resize.jcarousel", this.funcResize), null !== this.options.setupCallback && this.options.setupCallback(this) } }, reset: function () { this.list.empty(), this.list.css(this.lt, "0px"), this.list.css(this.wh, "10px"), null !== this.options.initCallback && this.options.initCallback(this, "reset"), this.setup() }, reload: function () { if (null !== this.tail && this.inTail && this.list.css(this.lt, d.intval(this.list.css(this.lt)) + this.tail), this.tail = null, this.inTail = !1, null !== this.options.reloadCallback && this.options.reloadCallback(this), null !== this.options.visible) { var a = this, b = Math.ceil(this.clipping() / this.options.visible), c = 0, e = 0; this.list.children("li").each(function (d) { c += a.dimension(this, b), a.first > d + 1 && (e = c) }), this.list.css(this.wh, c + "px"), this.list.css(this.lt, -e + "px") } this.scroll(this.first, !1) }, lock: function () { this.locked = !0, this.buttons() }, unlock: function () { this.locked = !1, this.buttons() }, size: function (a) { return void 0 !== a && (this.options.size = a, this.locked || this.buttons()), this.options.size }, has: function (a, b) { void 0 !== b && b || (b = a), null !== this.options.size && b > this.options.size && (b = this.options.size); for (var c = a; b >= c; c++) { var d = this.get(c); if (!d.length || d.hasClass("jcarousel-item-placeholder")) return !1 } return !0 }, get: function (b) { return a(">.jcarousel-item-" + b, this.list) }, add: function (b, c) { var e = this.get(b), f = 0, g = a(c); if (0 === e.length) { var h, i = d.intval(b); for (e = this.create(b) ; ;) if (h = this.get(--i), 0 >= i || h.length) { 0 >= i ? this.list.prepend(e) : h.after(e); break } } else f = this.dimension(e); "LI" == g.get(0).nodeName.toUpperCase() ? (e.replaceWith(g), e = g) : e.empty().append(c), this.format(e.removeClass(this.className("jcarousel-item-placeholder")), b); var j = null !== this.options.visible ? Math.ceil(this.clipping() / this.options.visible) : null, k = this.dimension(e, j) - f; return b > 0 && this.first > b && this.list.css(this.lt, d.intval(this.list.css(this.lt)) - k + "px"), this.list.css(this.wh, d.intval(this.list.css(this.wh)) + k + "px"), e }, remove: function (a) { var b = this.get(a); if (b.length && !(a >= this.first && this.last >= a)) { var c = this.dimension(b); this.first > a && this.list.css(this.lt, d.intval(this.list.css(this.lt)) + c + "px"), b.remove(), this.list.css(this.wh, d.intval(this.list.css(this.wh)) - c + "px") } }, next: function () { null === this.tail || this.inTail ? this.scroll("both" != this.options.wrap && "last" != this.options.wrap || null === this.options.size || this.last != this.options.size ? this.first + this.options.scroll : 1) : this.scrollTail(!1) }, prev: function () { null !== this.tail && this.inTail ? this.scrollTail(!0) : this.scroll("both" != this.options.wrap && "first" != this.options.wrap || null === this.options.size || 1 != this.first ? this.first - this.options.scroll : this.options.size) }, scrollTail: function (a) { if (!this.locked && !this.animating && this.tail) { this.pauseAuto(); var b = d.intval(this.list.css(this.lt)); b = a ? b + this.tail : b - this.tail, this.inTail = !a, this.prevFirst = this.first, this.prevLast = this.last, this.animate(b) } }, scroll: function (a, b) { this.locked || this.animating || (this.pauseAuto(), this.animate(this.pos(a), b)) }, pos: function (a, b) { var c = d.intval(this.list.css(this.lt)); if (this.locked || this.animating) return c; "circular" != this.options.wrap && (a = 1 > a ? 1 : this.options.size && a > this.options.size ? this.options.size : a); for (var m, e = this.first > a, f = "circular" != this.options.wrap && 1 >= this.first ? 1 : this.first, g = e ? this.get(f) : this.get(this.last), h = e ? f : f - 1, i = null, j = 0, k = !1, l = 0; e ? --h >= a : a > ++h;) i = this.get(h), k = !i.length, 0 === i.length && (i = this.create(h).addClass(this.className("jcarousel-item-placeholder")), g[e ? "before" : "after"](i), null !== this.first && "circular" == this.options.wrap && null !== this.options.size && (0 >= h || h > this.options.size) && (m = this.get(this.index(h)), m.length && (i = this.add(h, m.clone(!0))))), g = i, l = this.dimension(i), k && (j += l), null !== this.first && ("circular" == this.options.wrap || h >= 1 && (null === this.options.size || this.options.size >= h)) && (c = e ? c + l : c - l); var n = this.clipping(), o = [], p = 0, q = 0; for (g = this.get(a - 1), h = a; ++p;) { if (i = this.get(h), k = !i.length, 0 === i.length && (i = this.create(h).addClass(this.className("jcarousel-item-placeholder")), 0 === g.length ? this.list.prepend(i) : g[e ? "before" : "after"](i), null !== this.first && "circular" == this.options.wrap && null !== this.options.size && (0 >= h || h > this.options.size) && (m = this.get(this.index(h)), m.length && (i = this.add(h, m.clone(!0))))), g = i, l = this.dimension(i), 0 === l) throw Error("jCarousel: No width/height set for items. This will cause an infinite loop. Aborting..."); if ("circular" != this.options.wrap && null !== this.options.size && h > this.options.size ? o.push(i) : k && (j += l), q += l, q >= n) break; h++ } for (var r = 0; o.length > r; r++) o[r].remove(); j > 0 && (this.list.css(this.wh, this.dimension(this.list) + j + "px"), e && (c -= j, this.list.css(this.lt, d.intval(this.list.css(this.lt)) - j + "px"))); var s = a + p - 1; if ("circular" != this.options.wrap && this.options.size && s > this.options.size && (s = this.options.size), h > s) for (p = 0, h = s, q = 0; ++p && (i = this.get(h--), i.length) && (q += this.dimension(i), !(q >= n)) ;); var t = s - p + 1; if ("circular" != this.options.wrap && 1 > t && (t = 1), this.inTail && e && (c += this.tail, this.inTail = !1), this.tail = null, "circular" != this.options.wrap && s == this.options.size && s - p + 1 >= 1) { var u = d.intval(this.get(s).css(this.options.vertical ? "marginBottom" : "marginRight")); q - u > n && (this.tail = q - n - u) } for (b && a === this.options.size && this.tail && (c -= this.tail, this.inTail = !0) ; a-- > t;) c += this.dimension(this.get(a)); return this.prevFirst = this.first, this.prevLast = this.last, this.first = t, this.last = s, c }, animate: function (b, c) { if (!this.locked && !this.animating) { this.animating = !0; var d = this, e = function () { if (d.animating = !1, 0 === b && d.list.css(d.lt, 0), !d.autoStopped && ("circular" == d.options.wrap || "both" == d.options.wrap || "last" == d.options.wrap || null === d.options.size || d.last < d.options.size || d.last == d.options.size && null !== d.tail && !d.inTail) && d.startAuto(), d.buttons(), d.notify("onAfterAnimation"), "circular" == d.options.wrap && null !== d.options.size) for (var a = d.prevFirst; d.prevLast >= a; a++) null === a || a >= d.first && d.last >= a || !(1 > a || a > d.options.size) || d.remove(a) }; if (this.notify("onBeforeAnimation"), this.options.animation && c !== !1) { var f = this.options.vertical ? { top: b } : this.options.rtl ? { right: b } : { left: b }, g = { duration: this.options.animation, easing: this.options.easing, complete: e }; a.isFunction(this.options.animationStepCallback) && (g.step = this.options.animationStepCallback), this.list.animate(f, g) } else this.list.css(this.lt, b + "px"), e() } }, startAuto: function (a) { if (void 0 !== a && (this.options.auto = a), 0 === this.options.auto) return this.stopAuto(); if (null === this.timer) { this.autoStopped = !1; var b = this; this.timer = window.setTimeout(function () { b.next() }, 1e3 * this.options.auto) } }, stopAuto: function () { this.pauseAuto(), this.autoStopped = !0 }, pauseAuto: function () { null !== this.timer && (window.clearTimeout(this.timer), this.timer = null) }, buttons: function (a, b) { null == a && (a = !this.locked && 0 !== this.options.size && (this.options.wrap && "first" != this.options.wrap || null === this.options.size || this.last < this.options.size), this.locked || this.options.wrap && "first" != this.options.wrap || null === this.options.size || !(this.last >= this.options.size) || (a = null !== this.tail && !this.inTail)), null == b && (b = !this.locked && 0 !== this.options.size && (this.options.wrap && "last" != this.options.wrap || this.first > 1), this.locked || this.options.wrap && "last" != this.options.wrap || null === this.options.size || 1 != this.first || (b = null !== this.tail && this.inTail)); var c = this; this.buttonNext.size() > 0 ? (this.buttonNext.unbind(this.options.buttonNextEvent + ".jcarousel", this.funcNext), a && this.buttonNext.bind(this.options.buttonNextEvent + ".jcarousel", this.funcNext), this.buttonNext[a ? "removeClass" : "addClass"](this.className("jcarousel-next-disabled")).attr("disabled", a ? !1 : !0), null !== this.options.buttonNextCallback && this.buttonNext.data("jcarouselstate") != a && this.buttonNext.each(function () { c.options.buttonNextCallback(c, this, a) }).data("jcarouselstate", a)) : null !== this.options.buttonNextCallback && this.buttonNextState != a && this.options.buttonNextCallback(c, null, a), this.buttonPrev.size() > 0 ? (this.buttonPrev.unbind(this.options.buttonPrevEvent + ".jcarousel", this.funcPrev), b && this.buttonPrev.bind(this.options.buttonPrevEvent + ".jcarousel", this.funcPrev), this.buttonPrev[b ? "removeClass" : "addClass"](this.className("jcarousel-prev-disabled")).attr("disabled", b ? !1 : !0), null !== this.options.buttonPrevCallback && this.buttonPrev.data("jcarouselstate") != b && this.buttonPrev.each(function () { c.options.buttonPrevCallback(c, this, b) }).data("jcarouselstate", b)) : null !== this.options.buttonPrevCallback && this.buttonPrevState != b && this.options.buttonPrevCallback(c, null, b), this.buttonNextState = a, this.buttonPrevState = b }, notify: function (a) { var b = null === this.prevFirst ? "init" : this.prevFirst < this.first ? "next" : "prev"; this.callback("itemLoadCallback", a, b), this.prevFirst !== this.first && (this.callback("itemFirstInCallback", a, b, this.first), this.callback("itemFirstOutCallback", a, b, this.prevFirst)), this.prevLast !== this.last && (this.callback("itemLastInCallback", a, b, this.last), this.callback("itemLastOutCallback", a, b, this.prevLast)), this.callback("itemVisibleInCallback", a, b, this.first, this.last, this.prevFirst, this.prevLast), this.callback("itemVisibleOutCallback", a, b, this.prevFirst, this.prevLast, this.first, this.last) }, callback: function (b, c, d, e, f, g, h) { if (null != this.options[b] && ("object" == typeof this.options[b] || "onAfterAnimation" == c)) { var i = "object" == typeof this.options[b] ? this.options[b][c] : this.options[b]; if (a.isFunction(i)) { var j = this; if (void 0 === e) i(j, d, c); else if (void 0 === f) this.get(e).each(function () { i(j, this, e, d, c) }); else for (var k = function (a) { j.get(a).each(function () { i(j, this, a, d, c) }) }, l = e; f >= l; l++) null === l || l >= g && h >= l || k(l) } } }, create: function (a) { return this.format("<li></li>", a) }, format: function (b, c) { b = a(b); for (var d = b.get(0).className.split(" "), e = 0; d.length > e; e++) -1 != d[e].indexOf("jcarousel-") && b.removeClass(d[e]); return b.addClass(this.className("jcarousel-item")).addClass(this.className("jcarousel-item-" + c)).css({ "float": this.options.rtl ? "right" : "left", "list-style": "none" }).attr("jcarouselindex", c), b }, className: function (a) { return a + " " + a + (this.options.vertical ? "-vertical" : "-horizontal") }, dimension: function (b, c) { var e = a(b); if (null == c) return this.options.vertical ? e.innerHeight() + d.intval(e.css("margin-top")) + d.intval(e.css("margin-bottom")) + d.intval(e.css("border-top-width")) + d.intval(e.css("border-bottom-width")) || d.intval(this.options.itemFallbackDimension) : e.innerWidth() + d.intval(e.css("margin-left")) + d.intval(e.css("margin-right")) + d.intval(e.css("border-left-width")) + d.intval(e.css("border-right-width")) || d.intval(this.options.itemFallbackDimension); var f = this.options.vertical ? c - d.intval(e.css("marginTop")) - d.intval(e.css("marginBottom")) : c - d.intval(e.css("marginLeft")) - d.intval(e.css("marginRight")); return a(e).css(this.wh, f + "px"), this.dimension(e) }, clipping: function () { return this.options.vertical ? this.clip[0].offsetHeight - d.intval(this.clip.css("borderTopWidth")) - d.intval(this.clip.css("borderBottomWidth")) : this.clip[0].offsetWidth - d.intval(this.clip.css("borderLeftWidth")) - d.intval(this.clip.css("borderRightWidth")) }, index: function (a, b) { return null == b && (b = this.options.size), Math.round(((a - 1) / b - Math.floor((a - 1) / b)) * b) + 1 } }), d.extend({ defaults: function (c) { return a.extend(b, c || {}) }, intval: function (a) { return a = parseInt(a, 10), isNaN(a) ? 0 : a }, windowLoaded: function () { c = !0 }, isSafari: function () { var a = navigator.userAgent.toLowerCase(), b = /(chrome)[ \/]([\w.]+)/.exec(a) || /(webkit)[ \/]([\w.]+)/.exec(a) || [], c = b[1] || ""; return "webkit" === c } }), a.fn.jcarousel = function (b) { if ("string" == typeof b) { var c = a(this).data("jcarousel"), e = Array.prototype.slice.call(arguments, 1); return c[b].apply(c, e) } return this.each(function () { var c = a(this).data("jcarousel"); c ? (b && a.extend(c.options, b), c.reload()) : a(this).data("jcarousel", new d(this, b)) }) } })(jQuery);
/*! Swipebox v1.4.1 | Constantin Saguin csag.co | MIT License | github.com/brutaldesign/swipebox */

; (function (window, document, $, undefined) {

    $.swipebox = function (elem, options) {

        // Default options
        var ui,
			defaults = {
			    useCSS: true,
			    useSVG: true,
			    initialIndexOnArray: 0,
			    removeBarsOnMobile: true,
			    hideCloseButtonOnMobile: false,
			    hideBarsDelay: 3000,
			    videoMaxWidth: 1140,
			    vimeoColor: 'cccccc',
			    beforeOpen: null,
			    afterOpen: null,
			    afterClose: null,
			    nextSlide: null,
			    prevSlide: null,
			    loopAtEnd: false,
			    autoplayVideos: false,
			    queryStringData: {},
			    toggleClassOnLoad: ''
			},

			plugin = this,
			elements = [], // slides array [ { href:'...', title:'...' }, ...],
			$elem,
			selector = elem.selector,
			$selector = $(selector),
			isMobile = navigator.userAgent.match(/(iPad)|(iPhone)|(iPod)|(Android)|(PlayBook)|(BB10)|(BlackBerry)|(Opera Mini)|(IEMobile)|(webOS)|(MeeGo)/i),
			isTouch = isMobile !== null || document.createTouch !== undefined || ('ontouchstart' in window) || ('onmsgesturechange' in window) || navigator.msMaxTouchPoints,
			supportSVG = !!document.createElementNS && !!document.createElementNS('http://www.w3.org/2000/svg', 'svg').createSVGRect,
			winWidth = window.innerWidth ? window.innerWidth : $(window).width(),
			winHeight = window.innerHeight ? window.innerHeight : $(window).height(),
			currentX = 0,
			/* jshint multistr: true */
			html = '<div id="swipebox-overlay">\
					<div id="swipebox-container">\
						<div id="swipebox-slider"></div>\
						<div id="swipebox-top-bar">\
							<div id="swipebox-title"></div>\
						</div>\
						<div id="swipebox-bottom-bar">\
							<div id="swipebox-arrows">\
								<a id="swipebox-prev"></a>\
								<a id="swipebox-next"></a>\
							</div>\
						</div>\
						<a id="swipebox-close"></a>\
					</div>\
			</div>';

        plugin.settings = {};

        $.swipebox.close = function () {
            ui.closeSlide();
        };

        $.swipebox.extend = function () {
            return ui;
        };

        plugin.init = function () {

            plugin.settings = $.extend({}, defaults, options);

            if ($.isArray(elem)) {

                elements = elem;
                ui.target = $(window);
                ui.init(plugin.settings.initialIndexOnArray);

            } else {

                $(document).on('click', selector, function (event) {

                    // console.log( isTouch );

                    if (event.target.parentNode.className === 'slide current') {

                        return false;
                    }

                    if (!$.isArray(elem)) {
                        ui.destroy();
                        $elem = $(selector);
                        ui.actions();
                    }

                    elements = [];
                    var index, relType, relVal;

                    // Allow for HTML5 compliant attribute before legacy use of rel
                    if (!relVal) {
                        relType = 'data-rel';
                        relVal = $(this).attr(relType);
                    }

                    if (!relVal) {
                        relType = 'rel';
                        relVal = $(this).attr(relType);
                    }

                    if (relVal && relVal !== '' && relVal !== 'nofollow') {
                        $elem = $selector.filter('[' + relType + '="' + relVal + '"]');
                    } else {
                        $elem = $(selector);
                    }

                    $elem.each(function () {

                        var title = null,
							href = null;

                        if ($(this).attr('title')) {
                            title = $(this).attr('title');
                        }


                        if ($(this).attr('href')) {
                            href = $(this).attr('href');
                        }

                        elements.push({
                            href: href,
                            title: title
                        });
                    });

                    index = $elem.index($(this));
                    event.preventDefault();
                    event.stopPropagation();
                    ui.target = $(event.target);
                    ui.init(index);
                });
            }
        };

        ui = {

            /**
			 * Initiate Swipebox
			 */
            init: function (index) {
                if (plugin.settings.beforeOpen) {
                    plugin.settings.beforeOpen();
                }
                this.target.trigger('swipebox-start');
                $.swipebox.isOpen = true;
                this.build();
                this.openSlide(index);
                this.openMedia(index);
                this.preloadMedia(index + 1);
                this.preloadMedia(index - 1);
                if (plugin.settings.afterOpen) {
                    plugin.settings.afterOpen();
                }
            },

            /**
			 * Built HTML containers and fire main functions
			 */
            build: function () {
                var $this = this, bg;

                $('body').append(html);

                if (supportSVG && plugin.settings.useSVG === true) {
                    bg = $('#swipebox-close').css('background-image');
                    bg = bg.replace('png', 'svg');
                    $('#swipebox-prev, #swipebox-next, #swipebox-close').css({
                        'background-image': bg
                    });
                }

                if (isMobile && plugin.settings.removeBarsOnMobile) {
                    $('#swipebox-bottom-bar, #swipebox-top-bar').remove();
                }

                $.each(elements, function () {
                    $('#swipebox-slider').append('<div class="slide"></div>');
                });

                $this.setDim();
                $this.actions();

                if (isTouch) {
                    $this.gesture();
                }

                // Devices can have both touch and keyboard input so always allow key events
                $this.keyboard();

                $this.animBars();
                $this.resize();

            },

            /**
			 * Set dimensions depending on windows width and height
			 */
            setDim: function () {

                var width, height, sliderCss = {};

                // Reset dimensions on mobile orientation change
                if ('onorientationchange' in window) {

                    window.addEventListener('orientationchange', function () {
                        if (window.orientation === 0) {
                            width = winWidth;
                            height = winHeight;
                        } else if (window.orientation === 90 || window.orientation === -90) {
                            width = winHeight;
                            height = winWidth;
                        }
                    }, false);


                } else {

                    width = window.innerWidth ? window.innerWidth : $(window).width();
                    height = window.innerHeight ? window.innerHeight : $(window).height();
                }

                sliderCss = {
                    width: width,
                    height: height
                };

                $('#swipebox-overlay').css(sliderCss);

            },

            /**
			 * Reset dimensions on window resize envent
			 */
            resize: function () {
                var $this = this;

                $(window).resize(function () {
                    $this.setDim();
                }).resize();
            },

            /**
			 * Check if device supports CSS transitions
			 */
            supportTransition: function () {

                var prefixes = 'transition WebkitTransition MozTransition OTransition msTransition KhtmlTransition'.split(' '),
					i;

                for (i = 0; i < prefixes.length; i++) {
                    if (document.createElement('div').style[prefixes[i]] !== undefined) {
                        return prefixes[i];
                    }
                }
                return false;
            },

            /**
			 * Check if CSS transitions are allowed (options + devicesupport)
			 */
            doCssTrans: function () {
                if (plugin.settings.useCSS && this.supportTransition()) {
                    return true;
                }
            },

            /**
			 * Touch navigation
			 */
            gesture: function () {

                var $this = this,
					index,
					hDistance,
					vDistance,
					hDistanceLast,
					vDistanceLast,
					hDistancePercent,
					vSwipe = false,
					hSwipe = false,
					hSwipMinDistance = 10,
					vSwipMinDistance = 50,
					startCoords = {},
					endCoords = {},
					bars = $('#swipebox-top-bar, #swipebox-bottom-bar'),
					slider = $('#swipebox-slider');

                bars.addClass('visible-bars');
                $this.setTimeout();

                $('body').bind('touchstart', function (event) {

                    $(this).addClass('touching');
                    index = $('#swipebox-slider .slide').index($('#swipebox-slider .slide.current'));
                    endCoords = event.originalEvent.targetTouches[0];
                    startCoords.pageX = event.originalEvent.targetTouches[0].pageX;
                    startCoords.pageY = event.originalEvent.targetTouches[0].pageY;

                    $('#swipebox-slider').css({
                        '-webkit-transform': 'translate3d(' + currentX + '%, 0, 0)',
                        'transform': 'translate3d(' + currentX + '%, 0, 0)'
                    });

                    $('.touching').bind('touchmove', function (event) {
                        event.preventDefault();
                        event.stopPropagation();
                        endCoords = event.originalEvent.targetTouches[0];

                        if (!hSwipe) {
                            vDistanceLast = vDistance;
                            vDistance = endCoords.pageY - startCoords.pageY;
                            if (Math.abs(vDistance) >= vSwipMinDistance || vSwipe) {
                                var opacity = 0.75 - Math.abs(vDistance) / slider.height();

                                slider.css({ 'top': vDistance + 'px' });
                                slider.css({ 'opacity': opacity });

                                vSwipe = true;
                            }
                        }

                        hDistanceLast = hDistance;
                        hDistance = endCoords.pageX - startCoords.pageX;
                        hDistancePercent = hDistance * 100 / winWidth;

                        if (!hSwipe && !vSwipe && Math.abs(hDistance) >= hSwipMinDistance) {
                            $('#swipebox-slider').css({
                                '-webkit-transition': '',
                                'transition': ''
                            });
                            hSwipe = true;
                        }

                        if (hSwipe) {

                            // swipe left
                            if (0 < hDistance) {

                                // first slide
                                if (0 === index) {
                                    // console.log( 'first' );
                                    $('#swipebox-overlay').addClass('leftSpringTouch');
                                } else {
                                    // Follow gesture
                                    $('#swipebox-overlay').removeClass('leftSpringTouch').removeClass('rightSpringTouch');
                                    $('#swipebox-slider').css({
                                        '-webkit-transform': 'translate3d(' + (currentX + hDistancePercent) + '%, 0, 0)',
                                        'transform': 'translate3d(' + (currentX + hDistancePercent) + '%, 0, 0)'
                                    });
                                }

                                // swipe rught
                            } else if (0 > hDistance) {

                                // last Slide
                                if (elements.length === index + 1) {
                                    // console.log( 'last' );
                                    $('#swipebox-overlay').addClass('rightSpringTouch');
                                } else {
                                    $('#swipebox-overlay').removeClass('leftSpringTouch').removeClass('rightSpringTouch');
                                    $('#swipebox-slider').css({
                                        '-webkit-transform': 'translate3d(' + (currentX + hDistancePercent) + '%, 0, 0)',
                                        'transform': 'translate3d(' + (currentX + hDistancePercent) + '%, 0, 0)'
                                    });
                                }

                            }
                        }
                    });

                    return false;

                }).bind('touchend', function (event) {
                    event.preventDefault();
                    event.stopPropagation();

                    $('#swipebox-slider').css({
                        '-webkit-transition': '-webkit-transform 0.4s ease',
                        'transition': 'transform 0.4s ease'
                    });

                    vDistance = endCoords.pageY - startCoords.pageY;
                    hDistance = endCoords.pageX - startCoords.pageX;
                    hDistancePercent = hDistance * 100 / winWidth;

                    // Swipe to bottom to close
                    if (vSwipe) {
                        vSwipe = false;
                        if (Math.abs(vDistance) >= 2 * vSwipMinDistance && Math.abs(vDistance) > Math.abs(vDistanceLast)) {
                            var vOffset = vDistance > 0 ? slider.height() : -slider.height();
                            slider.animate({ top: vOffset + 'px', 'opacity': 0 },
								300,
								function () {
								    $this.closeSlide();
								});
                        } else {
                            slider.animate({ top: 0, 'opacity': 1 }, 300);
                        }

                    } else if (hSwipe) {

                        hSwipe = false;

                        // swipeLeft
                        if (hDistance >= hSwipMinDistance && hDistance >= hDistanceLast) {

                            $this.getPrev();

                            // swipeRight
                        } else if (hDistance <= -hSwipMinDistance && hDistance <= hDistanceLast) {

                            $this.getNext();
                        }

                    } else { // Top and bottom bars have been removed on touchable devices
                        // tap
                        if (!bars.hasClass('visible-bars')) {
                            $this.showBars();
                            $this.setTimeout();
                        } else {
                            $this.clearTimeout();
                            $this.hideBars();
                        }
                    }

                    $('#swipebox-slider').css({
                        '-webkit-transform': 'translate3d(' + currentX + '%, 0, 0)',
                        'transform': 'translate3d(' + currentX + '%, 0, 0)'
                    });

                    $('#swipebox-overlay').removeClass('leftSpringTouch').removeClass('rightSpringTouch');
                    $('.touching').off('touchmove').removeClass('touching');

                });
            },

            /**
			 * Set timer to hide the action bars
			 */
            setTimeout: function () {
                if (plugin.settings.hideBarsDelay > 0) {
                    var $this = this;
                    $this.clearTimeout();
                    $this.timeout = window.setTimeout(function () {
                        $this.hideBars();
                    },

						plugin.settings.hideBarsDelay
					);
                }
            },

            /**
			 * Clear timer
			 */
            clearTimeout: function () {
                window.clearTimeout(this.timeout);
                this.timeout = null;
            },

            /**
			 * Show navigation and title bars
			 */
            showBars: function () {
                var bars = $('#swipebox-top-bar, #swipebox-bottom-bar');
                if (this.doCssTrans()) {
                    bars.addClass('visible-bars');
                } else {
                    $('#swipebox-top-bar').animate({ top: 0 }, 500);
                    $('#swipebox-bottom-bar').animate({ bottom: 0 }, 500);
                    setTimeout(function () {
                        bars.addClass('visible-bars');
                    }, 1000);
                }
            },

            /**
			 * Hide navigation and title bars
			 */
            hideBars: function () {
                var bars = $('#swipebox-top-bar, #swipebox-bottom-bar');
                if (this.doCssTrans()) {
                    bars.removeClass('visible-bars');
                } else {
                    $('#swipebox-top-bar').animate({ top: '-50px' }, 500);
                    $('#swipebox-bottom-bar').animate({ bottom: '-50px' }, 500);
                    setTimeout(function () {
                        bars.removeClass('visible-bars');
                    }, 1000);
                }
            },

            /**
			 * Animate navigation and top bars
			 */
            animBars: function () {
                var $this = this,
					bars = $('#swipebox-top-bar, #swipebox-bottom-bar');

                bars.addClass('visible-bars');
                $this.setTimeout();

                $('#swipebox-slider').click(function () {
                    if (!bars.hasClass('visible-bars')) {
                        $this.showBars();
                        $this.setTimeout();
                    }
                });

                $('#swipebox-bottom-bar').hover(function () {
                    $this.showBars();
                    bars.addClass('visible-bars');
                    $this.clearTimeout();

                }, function () {
                    if (plugin.settings.hideBarsDelay > 0) {
                        bars.removeClass('visible-bars');
                        $this.setTimeout();
                    }

                });
            },

            /**
			 * Keyboard navigation
			 */
            keyboard: function () {
                var $this = this;
                $(window).bind('keyup', function (event) {
                    event.preventDefault();
                    event.stopPropagation();

                    if (event.keyCode === 37) {

                        $this.getPrev();

                    } else if (event.keyCode === 39) {

                        $this.getNext();

                    } else if (event.keyCode === 27) {

                        $this.closeSlide();
                    }
                });
            },

            /**
			 * Navigation events : go to next slide, go to prevous slide and close
			 */
            actions: function () {
                var $this = this,
					action = 'touchend click'; // Just detect for both event types to allow for multi-input

                if (elements.length < 2) {

                    $('#swipebox-bottom-bar').hide();

                    if (undefined === elements[1]) {
                        $('#swipebox-top-bar').hide();
                    }

                } else {
                    $('#swipebox-prev').bind(action, function (event) {
                        event.preventDefault();
                        event.stopPropagation();
                        $this.getPrev();
                        $this.setTimeout();
                    });

                    $('#swipebox-next').bind(action, function (event) {
                        event.preventDefault();
                        event.stopPropagation();
                        $this.getNext();
                        $this.setTimeout();
                    });
                }

                $('#swipebox-close').bind(action, function () {
                    $this.closeSlide();
                });
            },

            /**
			 * Set current slide
			 */
            setSlide: function (index, isFirst) {

                isFirst = isFirst || false;

                var slider = $('#swipebox-slider');

                currentX = -index * 100;

                if (this.doCssTrans()) {
                    slider.css({
                        '-webkit-transform': 'translate3d(' + (-index * 100) + '%, 0, 0)',
                        'transform': 'translate3d(' + (-index * 100) + '%, 0, 0)'
                    });
                } else {
                    slider.animate({ left: (-index * 100) + '%' });
                }

                $('#swipebox-slider .slide').removeClass('current');
                $('#swipebox-slider .slide').eq(index).addClass('current');
                this.setTitle(index);

                if (isFirst) {
                    slider.fadeIn();
                }

                $('#swipebox-prev, #swipebox-next').removeClass('disabled');

                if (index === 0) {
                    $('#swipebox-prev').addClass('disabled');
                } else if (index === elements.length - 1 && plugin.settings.loopAtEnd !== true) {
                    $('#swipebox-next').addClass('disabled');
                }
            },

            /**
			 * Open slide
			 */
            openSlide: function (index) {
                $('html').addClass('swipebox-html');
                if (isTouch) {
                    $('html').addClass('swipebox-touch');

                    if (plugin.settings.hideCloseButtonOnMobile) {
                        $('html').addClass('swipebox-no-close-button');
                    }
                } else {
                    $('html').addClass('swipebox-no-touch');
                }
                $(window).trigger('resize'); // fix scroll bar visibility on desktop
                this.setSlide(index, true);
            },

            /**
			 * Set a time out if the media is a video
			 */
            preloadMedia: function (index) {
                var $this = this,
					src = null;

                if (elements[index] !== undefined) {
                    src = elements[index].href;
                }

                if (!$this.isVideo(src)) {
                    setTimeout(function () {
                        $this.openMedia(index);
                    }, 1000);
                } else {
                    $this.openMedia(index);
                }
            },

            /**
			 * Open
			 */
            openMedia: function (index) {
                var $this = this,
					src,
					slide;

                if (elements[index] !== undefined) {
                    src = elements[index].href;
                }

                if (index < 0 || index >= elements.length) {
                    return false;
                }

                slide = $('#swipebox-slider .slide').eq(index);

                if (!$this.isVideo(src)) {
                    slide.addClass('slide-loading');
                    $this.loadMedia(src, function () {
                        slide.removeClass('slide-loading');
                        slide.html(this);
                    });
                } else {
                    slide.html($this.getVideo(src));
                }

            },

            /**
			 * Set link title attribute as caption
			 */
            setTitle: function (index) {
                var title = null;

                $('#swipebox-title').empty();

                if (elements[index] !== undefined) {
                    title = elements[index].title;
                }

                if (title) {
                    $('#swipebox-top-bar').show();
                    $('#swipebox-title').append(title);
                } else {
                    $('#swipebox-top-bar').hide();
                }
            },

            /**
			 * Check if the URL is a video
			 */
            isVideo: function (src) {

                if (src) {
                    if (src.match(/(youtube\.com|youtube-nocookie\.com)\/watch\?v=([a-zA-Z0-9\-_]+)/) || src.match(/vimeo\.com\/([0-9]*)/) || src.match(/youtu\.be\/([a-zA-Z0-9\-_]+)/)) {
                        return true;
                    }

                    if (src.toLowerCase().indexOf('swipeboxvideo=1') >= 0) {

                        return true;
                    }
                }

            },

            /**
			 * Parse URI querystring and:
			 * - overrides value provided via dictionary
			 * - rebuild it again returning a string
			 */
            parseUri: function (uri, customData) {
                var a = document.createElement('a'),
					qs = {};

                // Decode the URI
                a.href = decodeURIComponent(uri);

                // QueryString to Object
                if (a.search) {
                    qs = JSON.parse('{"' + a.search.toLowerCase().replace('?', '').replace(/&/g, '","').replace(/=/g, '":"') + '"}');
                }

                // Extend with custom data
                if ($.isPlainObject(customData)) {
                    qs = $.extend(qs, customData, plugin.settings.queryStringData); // The dev has always the final word
                }

                // Return querystring as a string
                return $
					.map(qs, function (val, key) {
					    if (val && val > '') {
					        return encodeURIComponent(key) + '=' + encodeURIComponent(val);
					    }
					})
					.join('&');
            },

            /**
			 * Get video iframe code from URL
			 */
            getVideo: function (url) {
                var iframe = '',
					youtubeUrl = url.match(/((?:www\.)?youtube\.com|(?:www\.)?youtube-nocookie\.com)\/watch\?v=([a-zA-Z0-9\-_]+)/),
					youtubeShortUrl = url.match(/(?:www\.)?youtu\.be\/([a-zA-Z0-9\-_]+)/),
					vimeoUrl = url.match(/(?:www\.)?vimeo\.com\/([0-9]*)/),
					qs = '';
                if (youtubeUrl || youtubeShortUrl) {
                    if (youtubeShortUrl) {
                        youtubeUrl = youtubeShortUrl;
                    }
                    qs = ui.parseUri(url, {
                        'autoplay': (plugin.settings.autoplayVideos ? '1' : '0'),
                        'v': ''
                    });
                    iframe = '<iframe width="560" height="315" src="//' + youtubeUrl[1] + '/embed/' + youtubeUrl[2] + '?' + qs + '" frameborder="0" allowfullscreen></iframe>';

                } else if (vimeoUrl) {
                    qs = ui.parseUri(url, {
                        'autoplay': (plugin.settings.autoplayVideos ? '1' : '0'),
                        'byline': '0',
                        'portrait': '0',
                        'color': plugin.settings.vimeoColor
                    });
                    iframe = '<iframe width="560" height="315"  src="//player.vimeo.com/video/' + vimeoUrl[1] + '?' + qs + '" frameborder="0" webkitAllowFullScreen mozallowfullscreen allowFullScreen></iframe>';

                } else {
                    iframe = '<iframe width="560" height="315" src="' + url + '" frameborder="0" allowfullscreen></iframe>';
                }

                return '<div class="swipebox-video-container" style="max-width:' + plugin.settings.videoMaxWidth + 'px"><div class="swipebox-video">' + iframe + '</div></div>';
            },

            /**
			 * Load image
			 */
            loadMedia: function (src, callback) {
                // Inline content
                if (src.trim().indexOf('#') === 0) {
                    callback.call(
                    	$('<div>', {
                    	    'class': 'swipebox-inline-container'
                    	})
                    	.append(
                    		$(src)
	                    	.clone()
	                    	.toggleClass(plugin.settings.toggleClassOnLoad)
	                    )
                    );
                }
                    // Everything else
                else {
                    if (!this.isVideo(src)) {
                        var img = $('<img>').on('load', function () {
                            callback.call(img);
                        });

                        img.attr('src', src);
                    }
                }
            },

            /**
			 * Get next slide
			 */
            getNext: function () {
                var $this = this,
					src,
					index = $('#swipebox-slider .slide').index($('#swipebox-slider .slide.current'));
                if (index + 1 < elements.length) {

                    src = $('#swipebox-slider .slide').eq(index).contents().find('iframe').attr('src');
                    $('#swipebox-slider .slide').eq(index).contents().find('iframe').attr('src', src);
                    index++;
                    $this.setSlide(index);
                    $this.preloadMedia(index + 1);
                    if (plugin.settings.nextSlide) {
                        plugin.settings.nextSlide();
                    }
                } else {

                    if (plugin.settings.loopAtEnd === true) {
                        src = $('#swipebox-slider .slide').eq(index).contents().find('iframe').attr('src');
                        $('#swipebox-slider .slide').eq(index).contents().find('iframe').attr('src', src);
                        index = 0;
                        $this.preloadMedia(index);
                        $this.setSlide(index);
                        $this.preloadMedia(index + 1);
                        if (plugin.settings.nextSlide) {
                            plugin.settings.nextSlide();
                        }
                    } else {
                        $('#swipebox-overlay').addClass('rightSpring');
                        setTimeout(function () {
                            $('#swipebox-overlay').removeClass('rightSpring');
                        }, 500);
                    }
                }
            },

            /**
			 * Get previous slide
			 */
            getPrev: function () {
                var index = $('#swipebox-slider .slide').index($('#swipebox-slider .slide.current')),
					src;
                if (index > 0) {
                    src = $('#swipebox-slider .slide').eq(index).contents().find('iframe').attr('src');
                    $('#swipebox-slider .slide').eq(index).contents().find('iframe').attr('src', src);
                    index--;
                    this.setSlide(index);
                    this.preloadMedia(index - 1);
                    if (plugin.settings.prevSlide) {
                        plugin.settings.prevSlide();
                    }
                } else {
                    $('#swipebox-overlay').addClass('leftSpring');
                    setTimeout(function () {
                        $('#swipebox-overlay').removeClass('leftSpring');
                    }, 500);
                }
            },

            nextSlide: function () {
                // Callback for next slide
            },

            prevSlide: function () {
                // Callback for prev slide
            },

            /**
			 * Close
			 */
            closeSlide: function () {
                $('html').removeClass('swipebox-html');
                $('html').removeClass('swipebox-touch');
                $(window).trigger('resize');
                this.destroy();
            },

            /**
			 * Destroy the whole thing
			 */
            destroy: function () {
                $(window).unbind('keyup');
                $('body').unbind('touchstart');
                $('body').unbind('touchmove');
                $('body').unbind('touchend');
                $('#swipebox-slider').unbind();
                $('#swipebox-overlay').remove();

                if (!$.isArray(elem)) {
                    elem.removeData('_swipebox');
                }

                if (this.target) {
                    this.target.trigger('swipebox-destroy');
                }

                $.swipebox.isOpen = false;

                if (plugin.settings.afterClose) {
                    plugin.settings.afterClose();
                }
            }
        };

        plugin.init();
    };

    $.fn.swipebox = function (options) {

        if (!$.data(this, '_swipebox')) {
            var swipebox = new $.swipebox(this, options);
            this.data('_swipebox', swipebox);
        }
        return this.data('_swipebox');

    };

}(window, document, jQuery));
/*! NProgress (c) 2013, Rico Sta. Cruz
 *  http://ricostacruz.com/nprogress */

; (function (factory) {

    if (typeof module === 'function') {
        module.exports = factory(this.jQuery || require('jquery'));
    } else if (typeof define === 'function' && define.amd) {
        define(['jquery'], function ($) {
            return factory($);
        });
    } else {
        this.NProgress = factory(this.jQuery);
    }

})(function ($) {
    var NProgress = {};

    NProgress.version = '0.1.2';

    var Settings = NProgress.settings = {
        minimum: 0.08,
        easing: 'ease',
        positionUsing: '',
        speed: 200,
        trickle: true,
        trickleRate: 0.02,
        trickleSpeed: 800,
        showSpinner: true,
        template: '<div class="bar" role="bar"><div class="peg"></div></div><div class="spinner" role="spinner"><div class="spinner-icon"></div></div>'
    };

    /**
     * Updates configuration.
     *
     *     NProgress.configure({
     *       minimum: 0.1
     *     });
     */
    NProgress.configure = function (options) {
        $.extend(Settings, options);
        return this;
    };

    /**
     * Last number.
     */

    NProgress.status = null;

    /**
     * Sets the progress bar status, where `n` is a number from `0.0` to `1.0`.
     *
     *     NProgress.set(0.4);
     *     NProgress.set(1.0);
     */

    NProgress.set = function (n) {
        var started = NProgress.isStarted();

        n = clamp(n, Settings.minimum, 1);
        NProgress.status = (n === 1 ? null : n);

        var $progress = NProgress.render(!started),
            $bar = $progress.find('[role="bar"]'),
            speed = Settings.speed,
            ease = Settings.easing;

        $progress[0].offsetWidth; /* Repaint */

        $progress.queue(function (next) {
            // Set positionUsing if it hasn't already been set
            if (Settings.positionUsing === '') Settings.positionUsing = NProgress.getPositioningCSS();

            // Add transition
            $bar.css(barPositionCSS(n, speed, ease));

            if (n === 1) {
                // Fade out
                $progress.css({ transition: 'none', opacity: 1 });
                $progress[0].offsetWidth; /* Repaint */

                setTimeout(function () {
                    $progress.css({ transition: 'all ' + speed + 'ms linear', opacity: 0 });
                    setTimeout(function () {
                        NProgress.remove();
                        next();
                    }, speed);
                }, speed);
            } else {
                setTimeout(next, speed);
            }
        });

        return this;
    };

    NProgress.isStarted = function () {
        return typeof NProgress.status === 'number';
    };

    /**
     * Shows the progress bar.
     * This is the same as setting the status to 0%, except that it doesn't go backwards.
     *
     *     NProgress.start();
     *
     */
    NProgress.start = function () {
        if (!NProgress.status) NProgress.set(0);

        var work = function () {
            setTimeout(function () {
                if (!NProgress.status) return;
                NProgress.trickle();
                work();
            }, Settings.trickleSpeed);
        };

        if (Settings.trickle) work();

        return this;
    };

    /**
     * Hides the progress bar.
     * This is the *sort of* the same as setting the status to 100%, with the
     * difference being `done()` makes some placebo effect of some realistic motion.
     *
     *     NProgress.done();
     *
     * If `true` is passed, it will show the progress bar even if its hidden.
     *
     *     NProgress.done(true);
     */

    NProgress.done = function (force) {
        if (!force && !NProgress.status) return this;

        return NProgress.inc(0.3 + 0.5 * Math.random()).set(1);
    };

    /**
     * Increments by a random amount.
     */

    NProgress.inc = function (amount) {
        var n = NProgress.status;

        if (!n) {
            return NProgress.start();
        } else {
            if (typeof amount !== 'number') {
                amount = (1 - n) * clamp(Math.random() * n, 0.1, 0.95);
            }

            n = clamp(n + amount, 0, 0.994);
            return NProgress.set(n);
        }
    };

    NProgress.trickle = function () {
        return NProgress.inc(Math.random() * Settings.trickleRate);
    };

    /**
     * (Internal) renders the progress bar markup based on the `template`
     * setting.
     */

    NProgress.render = function (fromStart) {
        if (NProgress.isRendered()) return $("#nprogress");
        $('html').addClass('nprogress-busy');

        var $el = $("<div id='nprogress'>")
          .html(Settings.template);

        var perc = fromStart ? '-100' : toBarPerc(NProgress.status || 0);

        $el.find('[role="bar"]').css({
            transition: 'all 0 linear',
            transform: 'translate3d(' + perc + '%,0,0)'
        });

        if (!Settings.showSpinner)
            $el.find('[role="spinner"]').remove();

        $el.appendTo(document.body);

        return $el;
    };

    /**
     * Removes the element. Opposite of render().
     */

    NProgress.remove = function () {
        $('html').removeClass('nprogress-busy');
        $('#nprogress').remove();
    };

    /**
     * Checks if the progress bar is rendered.
     */

    NProgress.isRendered = function () {
        return ($("#nprogress").length > 0);
    };

    /**
     * Determine which positioning CSS rule to use.
     */

    NProgress.getPositioningCSS = function () {
        // Sniff on document.body.style
        var bodyStyle = document.body.style;

        // Sniff prefixes
        var vendorPrefix = ('WebkitTransform' in bodyStyle) ? 'Webkit' :
                           ('MozTransform' in bodyStyle) ? 'Moz' :
                           ('msTransform' in bodyStyle) ? 'ms' :
                           ('OTransform' in bodyStyle) ? 'O' : '';

        if (vendorPrefix + 'Perspective' in bodyStyle) {
            // Modern browsers with 3D support, e.g. Webkit, IE10
            return 'translate3d';
        } else if (vendorPrefix + 'Transform' in bodyStyle) {
            // Browsers without 3D support, e.g. IE9
            return 'translate';
        } else {
            // Browsers without translate() support, e.g. IE7-8
            return 'margin';
        }
    };

    /**
     * Helpers
     */

    function clamp(n, min, max) {
        if (n < min) return min;
        if (n > max) return max;
        return n;
    }

    /**
     * (Internal) converts a percentage (`0..1`) to a bar translateX
     * percentage (`-100%..0%`).
     */

    function toBarPerc(n) {
        return (-1 + n) * 100;
    }


    /**
     * (Internal) returns the correct CSS for changing the bar's
     * position given an n percentage, and speed and ease from Settings
     */

    function barPositionCSS(n, speed, ease) {
        var barCSS;

        if (Settings.positionUsing === 'translate3d') {
            barCSS = { transform: 'translate3d(' + toBarPerc(n) + '%,0,0)' };
        } else if (Settings.positionUsing === 'translate') {
            barCSS = { transform: 'translate(' + toBarPerc(n) + '%,0)' };
        } else {
            barCSS = { 'margin-left': toBarPerc(n) + '%' };
        }

        barCSS.transition = 'all ' + speed + 'ms ' + ease;

        return barCSS;
    }

    return NProgress;
});


/*!
 * Cropper v2.0.1
 * https://github.com/fengyuanchen/cropper
 *
 * Copyright (c) 2014-2015 Fengyuan Chen and contributors
 * Released under the MIT license
 *
 * Date: 2015-11-18T03:13:47.241Z
 */

(function (factory) {

    if (typeof define === 'function' && define.amd) {
        // AMD. Register as anonymous module.
        define(['jquery'], factory);
    } else if (typeof exports === 'object') {
        // Node / CommonJS
        factory(require('jquery'));
    } else {
        // Browser globals.
        factory(jQuery);
    }
})(function ($) {

    'use strict';

    // Globals
    var $window = $(window);
    var $document = $(document);
    var location = window.location;

    // Constants
    var NAMESPACE = 'cropper';

    // Classes
    var CLASS_MODAL = 'cropper-modal';
    var CLASS_HIDE = 'cropper-hide';
    var CLASS_HIDDEN = 'cropper-hidden';
    var CLASS_INVISIBLE = 'cropper-invisible';
    var CLASS_MOVE = 'cropper-move';
    var CLASS_CROP = 'cropper-crop';
    var CLASS_DISABLED = 'cropper-disabled';
    var CLASS_BG = 'cropper-bg';

    // Events
    var EVENT_MOUSE_DOWN = 'mousedown touchstart pointerdown MSPointerDown';
    var EVENT_MOUSE_MOVE = 'mousemove touchmove pointermove MSPointerMove';
    var EVENT_MOUSE_UP = 'mouseup touchend touchcancel pointerup pointercancel MSPointerUp MSPointerCancel';
    var EVENT_WHEEL = 'wheel mousewheel DOMMouseScroll';
    var EVENT_DBLCLICK = 'dblclick';
    var EVENT_LOAD = 'load.' + NAMESPACE;
    var EVENT_ERROR = 'error.' + NAMESPACE;
    var EVENT_RESIZE = 'resize.' + NAMESPACE; // Bind to window with namespace
    var EVENT_BUILD = 'build.' + NAMESPACE;
    var EVENT_BUILT = 'built.' + NAMESPACE;
    var EVENT_CROP_START = 'cropstart.' + NAMESPACE;
    var EVENT_CROP_MOVE = 'cropmove.' + NAMESPACE;
    var EVENT_CROP_END = 'cropend.' + NAMESPACE;
    var EVENT_CROP = 'crop.' + NAMESPACE;
    var EVENT_ZOOM = 'zoom.' + NAMESPACE;

    // RegExps
    var REGEXP_ACTIONS = /^(e|w|s|n|se|sw|ne|nw|all|crop|move|zoom)$/;

    // Data keys
    var DATA_PREVIEW = 'preview';
    var DATA_ACTION = 'action';

    // Actions
    var ACTION_EAST = 'e';
    var ACTION_WEST = 'w';
    var ACTION_SOUTH = 's';
    var ACTION_NORTH = 'n';
    var ACTION_SOUTH_EAST = 'se';
    var ACTION_SOUTH_WEST = 'sw';
    var ACTION_NORTH_EAST = 'ne';
    var ACTION_NORTH_WEST = 'nw';
    var ACTION_ALL = 'all';
    var ACTION_CROP = 'crop';
    var ACTION_MOVE = 'move';
    var ACTION_ZOOM = 'zoom';
    var ACTION_NONE = 'none';

    // Supports
    var SUPPORT_CANVAS = $.isFunction($('<canvas>')[0].getContext);

    // Maths
    var num = Number;
    var min = Math.min;
    var max = Math.max;
    var abs = Math.abs;
    var sin = Math.sin;
    var cos = Math.cos;
    var sqrt = Math.sqrt;
    var round = Math.round;

    // Prototype
    var prototype = {
        version: '2.0.1'
    };

    function isNumber(n) {
        return typeof n === 'number' && !isNaN(n);
    }

    function isUndefined(n) {
        return typeof n === 'undefined';
    }

    function toArray(obj, offset) {
        var args = [];

        // This is necessary for IE8
        if (isNumber(offset)) {
            args.push(offset);
        }

        return args.slice.apply(obj, args);
    }

    // Custom proxy to avoid jQuery's guid
    function proxy(fn, context) {
        var args = toArray(arguments, 2);

        return function () {
            return fn.apply(context, args.concat(toArray(arguments)));
        };
    }

    function isCrossOriginURL(url) {
        var parts = url.match(/^(https?:)\/\/([^\:\/\?#]+):?(\d*)/i);

        return parts && (
          parts[1] !== location.protocol ||
          parts[2] !== location.hostname ||
          parts[3] !== location.port
        );
    }

    function addTimestamp(url) {
        var timestamp = 'timestamp=' + (new Date()).getTime();

        return (url + (url.indexOf('?') === -1 ? '?' : '&') + timestamp);
    }

    function getCrossOrigin(crossOrigin) {
        return crossOrigin ? ' crossOrigin="' + crossOrigin + '"' : '';
    }

    function getImageSize(image, callback) {

        var newImage;

        // Modern browsers
        if (image.naturalWidth) {
            return callback(image.naturalWidth, image.naturalHeight);
        }

        // IE8: Don't use `new Image()` here (#319)
        newImage = document.createElement('img');

        newImage.onload = function () {
            callback(this.width, this.height);
        };

        newImage.src = image.src;
    }

    function getTransform(options) {
        var transforms = [];
        var rotate = options.rotate;
        var scaleX = options.scaleX;
        var scaleY = options.scaleY;

        if (isNumber(rotate)) {
            transforms.push('rotate(' + rotate + 'deg)');
        }

        if (isNumber(scaleX) && isNumber(scaleY)) {
            transforms.push('scale(' + scaleX + ',' + scaleY + ')');
        }

        return transforms.length ? transforms.join(' ') : 'none';
    }

    function getRotatedSizes(data, isReversed) {
        var deg = abs(data.degree) % 180;
        var arc = (deg > 90 ? (180 - deg) : deg) * Math.PI / 180;
        var sinArc = sin(arc);
        var cosArc = cos(arc);
        var width = data.width;
        var height = data.height;
        var aspectRatio = data.aspectRatio;
        var newWidth;
        var newHeight;

        if (!isReversed) {
            newWidth = width * cosArc + height * sinArc;
            newHeight = width * sinArc + height * cosArc;
        } else {
            newWidth = width / (cosArc + sinArc / aspectRatio);
            newHeight = newWidth / aspectRatio;
        }

        return {
            width: newWidth,
            height: newHeight
        };
    }

    function getSourceCanvas(image, data) {

        var canvas = $('<canvas>')[0];
        var context = canvas.getContext('2d');
        var x = 0;
        var y = 0;
        var width = data.naturalWidth;
        var height = data.naturalHeight;
        var rotate = data.rotate;
        var scaleX = data.scaleX;
        var scaleY = data.scaleY;
        var scalable = isNumber(scaleX) && isNumber(scaleY) && (scaleX !== 1 || scaleY !== 1);
        var rotatable = isNumber(rotate) && rotate !== 0;
        var advanced = rotatable || scalable;
        var canvasWidth = width;
        var canvasHeight = height;
        var translateX;
        var translateY;
        var rotated;

        if (scalable) {
            translateX = width / 2;
            translateY = height / 2;
        }

        if (rotatable) {
            rotated = getRotatedSizes({
                width: width,
                height: height,
                degree: rotate
            });

            canvasWidth = rotated.width;
            canvasHeight = rotated.height;
            translateX = rotated.width / 2;
            translateY = rotated.height / 2;
        }

        canvas.width = canvasWidth;
        canvas.height = canvasHeight;

        if (advanced) {
            x = -width / 2;
            y = -height / 2;

            context.save();
            context.translate(translateX, translateY);
        }

        if (rotatable) {
            context.rotate(rotate * Math.PI / 180);
        }

        // Should call `scale` after rotated
        if (scalable) {
            context.scale(scaleX, scaleY);
        }

        context.drawImage(image, x, y, width, height);

        if (advanced) {
            context.restore();
        }

        return canvas;
    }

    function Cropper(element, options) {
        this.$element = $(element);
        this.options = $.extend({}, Cropper.DEFAULTS, $.isPlainObject(options) && options);
        this.isLoaded = false;
        this.isBuilt = false;
        this.isCompleted = false;
        this.isRotated = false;
        this.isCropped = false;
        this.isDisabled = false;
        this.isReplaced = false;
        this.isLimited = false;
        this.isImg = false;
        this.originalUrl = '';
        this.crossOrigin = '';
        this.canvas = null;
        this.cropBox = null;
        this.init();
    }

    $.extend(prototype, {
        init: function () {
            var $this = this.$element;
            var url;

            if ($this.is('img')) {
                this.isImg = true;

                // Should use `$.fn.attr` here. e.g.: "img/picture.jpg"
                this.originalUrl = url = $this.attr('src');

                // Stop when it's a blank image
                if (!url) {
                    return;
                }

                // Should use `$.fn.prop` here. e.g.: "http://example.com/img/picture.jpg"
                url = $this.prop('src');
            } else if ($this.is('canvas') && SUPPORT_CANVAS) {
                url = $this[0].toDataURL();
            }

            this.load(url);
        },

        // A shortcut for triggering custom events
        trigger: function (type, data) {
            var e = $.Event(type, data);

            this.$element.trigger(e);

            return e;
        },

        load: function (url) {

            var options = this.options;
            var $this = this.$element;
            var crossOrigin = '';
            var bustCacheUrl;
            var $clone;

            if (!url) {
                return;
            }

            this.url = url;

            // Trigger build event first
            $this.one(EVENT_BUILD, options.build);

            if (this.trigger(EVENT_BUILD).isDefaultPrevented()) {
                return;
            }

            if (options.checkCrossOrigin && isCrossOriginURL(url)) {
                crossOrigin = $this.prop('crossOrigin');

                // Bust cache (#148), only when there was not a "crossOrigin" property
                if (!crossOrigin) {
                    crossOrigin = 'anonymous';
                    bustCacheUrl = addTimestamp(url);
                }
            }

            this.crossOrigin = crossOrigin;
            this.$clone = $clone = $('<img' + getCrossOrigin(crossOrigin) + ' src="' + (bustCacheUrl || url) + '">');

            if (this.isImg) {
                if ($this[0].complete) {
                    this.start();
                } else {
                    $this.one(EVENT_LOAD, $.proxy(this.start, this));
                }
            } else {
                $clone.
                  one(EVENT_LOAD, $.proxy(this.start, this)).
                  one(EVENT_ERROR, $.proxy(this.stop, this)).
                  addClass(CLASS_HIDE).
                  insertAfter($this);
            }
        },

        start: function () {

            var $image = this.$element;
            var $clone = this.$clone;

            if (!this.isImg) {
                $clone.off(EVENT_ERROR, this.stop);
                $image = $clone;
            }

            getImageSize($image[0], $.proxy(function (naturalWidth, naturalHeight) {
                this.image = {
                    naturalWidth: naturalWidth,
                    naturalHeight: naturalHeight,
                    aspectRatio: naturalWidth / naturalHeight
                };

                this.isLoaded = true;
                this.build();
            }, this));
        },

        stop: function () {
            this.$clone.remove();
            this.$clone = null;
        }
    });

    $.extend(prototype, {
        build: function () {
            var options = this.options;
            var $this = this.$element;
            var $clone = this.$clone;
            var $cropper;
            var $cropBox;
            var $face;

            if (!this.isLoaded) {
                return;
            }

            // Unbuild first when replace
            if (this.isBuilt) {
                this.unbuild();
            }

            // Create cropper elements
            this.$container = $this.parent();
            this.$cropper = $cropper = $(Cropper.TEMPLATE);
            this.$canvas = $cropper.find('.cropper-canvas').append($clone);
            this.$dragBox = $cropper.find('.cropper-drag-box');
            this.$cropBox = $cropBox = $cropper.find('.cropper-crop-box');
            this.$viewBox = $cropper.find('.cropper-view-box');
            this.$face = $face = $cropBox.find('.cropper-face');

            // Hide the original image
            $this.addClass(CLASS_HIDDEN).after($cropper);

            // Show the clone image if is hidden
            if (!this.isImg) {
                $clone.removeClass(CLASS_HIDE);
            }

            this.initPreview();
            this.bind();

            options.aspectRatio = max(0, options.aspectRatio) || NaN;
            options.viewMode = max(0, min(3, round(options.viewMode))) || 0;

            if (options.autoCrop) {
                this.isCropped = true;

                if (options.modal) {
                    this.$dragBox.addClass(CLASS_MODAL);
                }
            } else {
                $cropBox.addClass(CLASS_HIDDEN);
            }

            if (!options.guides) {
                $cropBox.find('.cropper-dashed').addClass(CLASS_HIDDEN);
            }

            if (!options.center) {
                $cropBox.find('.cropper-center').addClass(CLASS_HIDDEN);
            }

            if (options.cropBoxMovable) {
                $face.addClass(CLASS_MOVE).data(DATA_ACTION, ACTION_ALL);
            }

            if (!options.highlight) {
                $face.addClass(CLASS_INVISIBLE);
            }

            if (options.background) {
                $cropper.addClass(CLASS_BG);
            }

            if (!options.cropBoxResizable) {
                $cropBox.find('.cropper-line, .cropper-point').addClass(CLASS_HIDDEN);
            }

            this.setDragMode(options.dragMode);
            this.render();
            this.isBuilt = true;
            this.setData(options.data);
            $this.one(EVENT_BUILT, options.built);

            // Trigger the built event asynchronously to keep `data('cropper')` is defined
            setTimeout($.proxy(function () {
                this.trigger(EVENT_BUILT);
                this.isCompleted = true;
            }, this), 0);
        },

        unbuild: function () {
            if (!this.isBuilt) {
                return;
            }

            this.isBuilt = false;
            this.initialImage = null;

            // Clear `initialCanvas` is necessary when replace
            this.initialCanvas = null;
            this.initialCropBox = null;
            this.container = null;
            this.canvas = null;

            // Clear `cropBox` is necessary when replace
            this.cropBox = null;
            this.unbind();

            this.resetPreview();
            this.$preview = null;

            this.$viewBox = null;
            this.$cropBox = null;
            this.$dragBox = null;
            this.$canvas = null;
            this.$container = null;

            this.$cropper.remove();
            this.$cropper = null;
        }
    });

    $.extend(prototype, {
        render: function () {
            this.initContainer();
            this.initCanvas();
            this.initCropBox();

            this.renderCanvas();

            if (this.isCropped) {
                this.renderCropBox();
            }
        },

        initContainer: function () {
            var options = this.options;
            var $this = this.$element;
            var $container = this.$container;
            var $cropper = this.$cropper;

            $cropper.addClass(CLASS_HIDDEN);
            $this.removeClass(CLASS_HIDDEN);

            $cropper.css((this.container = {

                width: max($container.width(), num(options.minContainerWidth) || 200),
                height: max($container.height(), num(options.minContainerHeight) || 100)
            }));

            $this.addClass(CLASS_HIDDEN);
            $cropper.removeClass(CLASS_HIDDEN);
        },

        // Canvas (image wrapper)
        initCanvas: function () {

            var viewMode = this.options.viewMode;
            var container = this.container;
            var containerWidth = container.width;
            var containerHeight = container.height;
            var image = this.image;
            var aspectRatio = image.aspectRatio;

            var canvas = {
                naturalWidth: image.naturalWidth,
                naturalHeight: image.naturalHeight,
                aspectRatio: aspectRatio,
                width: containerWidth,
                height: containerHeight
            };

            if (containerHeight * aspectRatio > containerWidth) {
                if (viewMode === 3) {
                    canvas.width = containerHeight * aspectRatio;
                } else {
                    canvas.height = containerWidth / aspectRatio;
                }
            } else {
                if (viewMode === 3) {
                    canvas.height = containerWidth / aspectRatio;
                } else {
                    canvas.width = containerHeight * aspectRatio;
                }
            }

            canvas.oldLeft = canvas.left = (containerWidth - canvas.width) / 2;
            canvas.oldTop = canvas.top = (containerHeight - canvas.height) / 2;

            this.canvas = canvas;
            this.isLimited = (viewMode === 1 || viewMode === 2);
            this.limitCanvas(true, true);
            this.initialImage = $.extend({}, image);
            this.initialCanvas = $.extend({}, canvas);
        },

        limitCanvas: function (isSizeLimited, isPositionLimited) {
            var options = this.options;
            var viewMode = options.viewMode;
            var container = this.container;
            var containerWidth = container.width;
            var containerHeight = container.height;
            var canvas = this.canvas;
            var aspectRatio = canvas.aspectRatio;
            var cropBox = this.cropBox;
            var isCropped = this.isCropped && cropBox;
            var minCanvasWidth;
            var minCanvasHeight;
            var newCanvasLeft;
            var newCanvasTop;

            if (isSizeLimited) {
                minCanvasWidth = num(options.minCanvasWidth) || 0;
                minCanvasHeight = num(options.minCanvasHeight) || 0;

                if (viewMode) {
                    if (viewMode > 1) {
                        minCanvasWidth = max(minCanvasWidth, containerWidth);
                        minCanvasHeight = max(minCanvasHeight, containerHeight);

                        if (viewMode === 3) {
                            if (minCanvasHeight * aspectRatio > minCanvasWidth) {
                                minCanvasWidth = minCanvasHeight * aspectRatio;
                            } else {
                                minCanvasHeight = minCanvasWidth / aspectRatio;
                            }
                        }
                    } else {
                        if (minCanvasWidth) {
                            minCanvasWidth = max(minCanvasWidth, isCropped ? cropBox.width : 0);
                        } else if (minCanvasHeight) {
                            minCanvasHeight = max(minCanvasHeight, isCropped ? cropBox.height : 0);
                        } else if (isCropped) {
                            minCanvasWidth = cropBox.width;
                            minCanvasHeight = cropBox.height;

                            if (minCanvasHeight * aspectRatio > minCanvasWidth) {
                                minCanvasWidth = minCanvasHeight * aspectRatio;
                            } else {
                                minCanvasHeight = minCanvasWidth / aspectRatio;
                            }
                        }
                    }
                }

                if (minCanvasWidth && minCanvasHeight) {
                    if (minCanvasHeight * aspectRatio > minCanvasWidth) {
                        minCanvasHeight = minCanvasWidth / aspectRatio;
                    } else {
                        minCanvasWidth = minCanvasHeight * aspectRatio;
                    }
                } else if (minCanvasWidth) {
                    minCanvasHeight = minCanvasWidth / aspectRatio;
                } else if (minCanvasHeight) {
                    minCanvasWidth = minCanvasHeight * aspectRatio;
                }

                canvas.minWidth = minCanvasWidth;
                canvas.minHeight = minCanvasHeight;
                canvas.maxWidth = Infinity;
                canvas.maxHeight = Infinity;
            }

            if (isPositionLimited) {
                if (viewMode) {
                    newCanvasLeft = containerWidth - canvas.width;
                    newCanvasTop = containerHeight - canvas.height;

                    canvas.minLeft = min(0, newCanvasLeft);
                    canvas.minTop = min(0, newCanvasTop);
                    canvas.maxLeft = max(0, newCanvasLeft);
                    canvas.maxTop = max(0, newCanvasTop);

                    if (isCropped && this.isLimited) {
                        canvas.minLeft = min(
                          cropBox.left,
                          cropBox.left + cropBox.width - canvas.width
                        );
                        canvas.minTop = min(
                          cropBox.top,
                          cropBox.top + cropBox.height - canvas.height
                        );
                        canvas.maxLeft = cropBox.left;
                        canvas.maxTop = cropBox.top;

                        if (viewMode === 2) {
                            if (canvas.width >= containerWidth) {
                                canvas.minLeft = min(0, newCanvasLeft);
                                canvas.maxLeft = max(0, newCanvasLeft);
                            }

                            if (canvas.height >= containerHeight) {
                                canvas.minTop = min(0, newCanvasTop);
                                canvas.maxTop = max(0, newCanvasTop);
                            }
                        }
                    }
                } else {
                    canvas.minLeft = -canvas.width;
                    canvas.minTop = -canvas.height;
                    canvas.maxLeft = containerWidth;
                    canvas.maxTop = containerHeight;
                }
            }
        },

        renderCanvas: function (isChanged) {
            var canvas = this.canvas;
            var image = this.image;
            var rotate = image.rotate;
            var naturalWidth = image.naturalWidth;
            var naturalHeight = image.naturalHeight;
            var aspectRatio;
            var rotated;

            if (this.isRotated) {
                this.isRotated = false;

                // Computes rotated sizes with image sizes
                rotated = getRotatedSizes({
                    width: image.width + 20,
                    height: image.height + 20,
                    degree: rotate
                });

                aspectRatio = rotated.width / rotated.height;

                if (aspectRatio !== canvas.aspectRatio) {
                    canvas.left -= (rotated.width - canvas.width) / 2;
                    canvas.top -= (rotated.height - canvas.height) / 2;
                    canvas.width = rotated.width;
                    canvas.height = rotated.height;
                    canvas.aspectRatio = aspectRatio;
                    canvas.naturalWidth = naturalWidth;
                    canvas.naturalHeight = naturalHeight;

                    // Computes rotated sizes with natural image sizes
                    if (rotate % 180) {
                        rotated = getRotatedSizes({
                            width: naturalWidth,
                            height: naturalHeight,
                            degree: rotate
                        });

                        canvas.naturalWidth = rotated.width;
                        canvas.naturalHeight = rotated.height;
                    }

                    this.limitCanvas(true, false);
                }
            }

            if (canvas.width > canvas.maxWidth || canvas.width < canvas.minWidth) {
                canvas.left = canvas.oldLeft;
            }

            if (canvas.height > canvas.maxHeight || canvas.height < canvas.minHeight) {
                canvas.top = canvas.oldTop;
            }

            canvas.width = min(max(canvas.width, canvas.minWidth), canvas.maxWidth);
            canvas.height = min(max(canvas.height, canvas.minHeight), canvas.maxHeight);

            this.limitCanvas(false, true);

            canvas.oldLeft = canvas.left = min(max(canvas.left, canvas.minLeft), canvas.maxLeft);
            canvas.oldTop = canvas.top = min(max(canvas.top, canvas.minTop), canvas.maxTop);
            //width: 275.755px; height: 207px; left: 28.1226px; top: 0px;
            this.$canvas.css({
                width: canvas.width,
                height: canvas.height,
                left: canvas.left,
                top: canvas.top
            });

            this.renderImage();

            if (this.isCropped && this.isLimited) {
                this.limitCropBox(true, true);
            }

            if (isChanged) {
                this.output();
            }
        },

        renderImage: function (isChanged) {
            var canvas = this.canvas;
            var image = this.image;
            var reversed;

            if (image.rotate) {
                reversed = getRotatedSizes({
                    width: canvas.width,
                    height: canvas.height,
                    degree: image.rotate,
                    aspectRatio: image.aspectRatio
                }, true);
            }

            $.extend(image, reversed ? {
                width: reversed.width,
                height: reversed.height,
                left: (canvas.width - reversed.width) / 2,
                top: (canvas.height - reversed.height) / 2
            } : {
                width: canvas.width,
                height: canvas.height,
                left: 0,
                top: 0
            });

            this.$clone.css({
                width: image.width,
                height: image.height,
                marginLeft: image.left,
                marginTop: image.top,
                transform: getTransform(image)
            });

            if (isChanged) {
                this.output();
            }
        },

        initCropBox: function () {
            var options = this.options;
            var canvas = this.canvas;
            var aspectRatio = options.aspectRatio;
            var autoCropArea = num(options.autoCropArea) || 0.8;

            var cropBox = {
                width: canvas.width,
                height: canvas.height
            };

            if (aspectRatio) {
                if (canvas.height * aspectRatio > canvas.width) {
                    cropBox.height = cropBox.width / aspectRatio;
                } else {
                    cropBox.width = cropBox.height * aspectRatio;
                }
            }

            this.cropBox = cropBox;
            this.limitCropBox(true, true);

            // Initialize auto crop area
            cropBox.width = min(max(cropBox.width, cropBox.minWidth), cropBox.maxWidth);
            cropBox.height = min(max(cropBox.height, cropBox.minHeight), cropBox.maxHeight);

            // The width of auto crop area must large than "minWidth", and the height too. (#164)
            cropBox.width = max(cropBox.minWidth, cropBox.width * autoCropArea);
            cropBox.height = max(cropBox.minHeight, cropBox.height * autoCropArea);
            cropBox.oldLeft = cropBox.left = canvas.left + (canvas.width - cropBox.width) / 2;
            cropBox.oldTop = cropBox.top = canvas.top + (canvas.height - cropBox.height) / 2;

            this.initialCropBox = $.extend({}, cropBox);
        },

        limitCropBox: function (isSizeLimited, isPositionLimited) {
            var options = this.options;
            var aspectRatio = options.aspectRatio;
            var container = this.container;
            var containerWidth = container.width;
            var containerHeight = container.height;
            var canvas = this.canvas;
            var cropBox = this.cropBox;
            var isLimited = this.isLimited;
            var minCropBoxWidth;
            var minCropBoxHeight;
            var maxCropBoxWidth;
            var maxCropBoxHeight;

            if (isSizeLimited) {
                minCropBoxWidth = num(options.minCropBoxWidth) || 0;
                minCropBoxHeight = num(options.minCropBoxHeight) || 0;

                // The min/maxCropBoxWidth/Height must be less than containerWidth/Height
                minCropBoxWidth = min(minCropBoxWidth, containerWidth);
                minCropBoxHeight = min(minCropBoxHeight, containerHeight);
                maxCropBoxWidth = min(containerWidth, isLimited ? canvas.width : containerWidth);
                maxCropBoxHeight = min(containerHeight, isLimited ? canvas.height : containerHeight);

                if (aspectRatio) {
                    if (minCropBoxWidth && minCropBoxHeight) {
                        if (minCropBoxHeight * aspectRatio > minCropBoxWidth) {
                            minCropBoxHeight = minCropBoxWidth / aspectRatio;
                        } else {
                            minCropBoxWidth = minCropBoxHeight * aspectRatio;
                        }
                    } else if (minCropBoxWidth) {
                        minCropBoxHeight = minCropBoxWidth / aspectRatio;
                    } else if (minCropBoxHeight) {
                        minCropBoxWidth = minCropBoxHeight * aspectRatio;
                    }

                    if (maxCropBoxHeight * aspectRatio > maxCropBoxWidth) {
                        maxCropBoxHeight = maxCropBoxWidth / aspectRatio;
                    } else {
                        maxCropBoxWidth = maxCropBoxHeight * aspectRatio;
                    }
                }

                // The minWidth/Height must be less than maxWidth/Height
                cropBox.minWidth = min(minCropBoxWidth, maxCropBoxWidth);
                cropBox.minHeight = min(minCropBoxHeight, maxCropBoxHeight);
                cropBox.maxWidth = maxCropBoxWidth;
                cropBox.maxHeight = maxCropBoxHeight;
            }

            if (isPositionLimited) {
                if (isLimited) {
                    cropBox.minLeft = max(0, canvas.left);
                    cropBox.minTop = max(0, canvas.top);
                    cropBox.maxLeft = min(containerWidth, canvas.left + canvas.width) - cropBox.width;
                    cropBox.maxTop = min(containerHeight, canvas.top + canvas.height) - cropBox.height;
                } else {
                    cropBox.minLeft = 0;
                    cropBox.minTop = 0;
                    cropBox.maxLeft = containerWidth - cropBox.width;
                    cropBox.maxTop = containerHeight - cropBox.height;
                }
            }
        },

        renderCropBox: function () {
            var options = this.options;
            var container = this.container;
            var containerWidth = container.width;
            var containerHeight = container.height;
            var cropBox = this.cropBox;

            if (cropBox.width > cropBox.maxWidth || cropBox.width < cropBox.minWidth) {
                cropBox.left = cropBox.oldLeft;
            }

            if (cropBox.height > cropBox.maxHeight || cropBox.height < cropBox.minHeight) {
                cropBox.top = cropBox.oldTop;
            }

            cropBox.width = min(max(cropBox.width, cropBox.minWidth), cropBox.maxWidth);
            cropBox.height = min(max(cropBox.height, cropBox.minHeight), cropBox.maxHeight);

            this.limitCropBox(false, true);

            cropBox.oldLeft = cropBox.left = min(max(cropBox.left, cropBox.minLeft), cropBox.maxLeft);
            cropBox.oldTop = cropBox.top = min(max(cropBox.top, cropBox.minTop), cropBox.maxTop);

            if (options.movable && options.cropBoxMovable) {

                // Turn to move the canvas when the crop box is equal to the container
                this.$face.data(DATA_ACTION, (cropBox.width === containerWidth && cropBox.height === containerHeight) ? ACTION_MOVE : ACTION_ALL);
            }

            this.$cropBox.css({
                width: cropBox.width,
                height: cropBox.height,
                left: cropBox.left,
                top: cropBox.top
            });

            if (this.isCropped && this.isLimited) {
                this.limitCanvas(true, true);
            }

            if (!this.isDisabled) {
                this.output();
            }
        },

        output: function () {
            this.preview();

            if (this.isCompleted) {
                this.trigger(EVENT_CROP, this.getData());
            } else if (!this.isBuilt) {

                // Only trigger one crop event before complete
                this.$element.one(EVENT_BUILT, $.proxy(function () {
                    this.trigger(EVENT_CROP, this.getData());
                }, this));
            }
        }
    });

    $.extend(prototype, {
        initPreview: function () {

            var crossOrigin = getCrossOrigin(this.crossOrigin);
            var url = this.url;

            this.$preview = $(this.options.preview);
            this.$viewBox.html('<img' + crossOrigin + ' src="' + url + '">');
            this.$preview.each(function () {
                var $this = $(this);

                // Save the original size for recover
                $this.data(DATA_PREVIEW, {
                    width: $this.width(),
                    height: $this.height(),
                    html: $this.html()
                });

                /**
                 * Override img element styles
                 * Add `display:block` to avoid margin top issue
                 * (Occur only when margin-top <= -height)
                 */
                $this.html(
                  '<img' + crossOrigin + ' src="' + url + '" style="' +
                  'display:block;width:100%;height:auto;' +
                  'min-width:0!important;min-height:0!important;' +
                  'max-width:none!important;max-height:none!important;' +
                  'image-orientation:0deg!important;">'
                );
            });
        },

        resetPreview: function () {
            this.$preview.each(function () {
                var $this = $(this);
                var data = $this.data(DATA_PREVIEW);

                $this.css({
                    width: data.width,
                    height: data.height
                }).html(data.html).removeData(DATA_PREVIEW);
            });
        },

        preview: function () {

            var image = this.image;
            var canvas = this.canvas;
            var cropBox = this.cropBox;
            var cropBoxWidth = cropBox.width;
            var cropBoxHeight = cropBox.height;
            var width = image.width;
            var height = image.height;
            var left = cropBox.left - canvas.left - image.left;
            var top = cropBox.top - canvas.top - image.top;

            if (!this.isCropped || this.isDisabled) {
                return;
            }

            this.$viewBox.find('img').css({
                width: width,
                height: height,
                marginLeft: -left,
                marginTop: -top,
                transform: getTransform(image)
            });

            this.$preview.each(function () {
                var $this = $(this);
                var data = $this.data(DATA_PREVIEW);
                var originalWidth = data.width;
                var originalHeight = data.height;
                var newWidth = originalWidth;
                var newHeight = originalHeight;
                var ratio = 1;

                if (cropBoxWidth) {
                    ratio = originalWidth / cropBoxWidth;
                    newHeight = cropBoxHeight * ratio;
                }

                if (cropBoxHeight && newHeight > originalHeight) {
                    ratio = originalHeight / cropBoxHeight;
                    newWidth = cropBoxWidth * ratio;
                    newHeight = originalHeight;
                }

                $this.css({
                    width: newWidth,
                    height: newHeight
                }).find('img').css({
                    width: width * ratio,
                    height: height * ratio,
                    marginLeft: -left * ratio,
                    marginTop: -top * ratio,
                    transform: getTransform(image)
                });
            });
        }
    });

    $.extend(prototype, {
        bind: function () {

            var options = this.options;
            var $this = this.$element;
            var $cropper = this.$cropper;

            if ($.isFunction(options.cropstart)) {
                $this.on(EVENT_CROP_START, options.cropstart);
            }

            if ($.isFunction(options.cropmove)) {
                $this.on(EVENT_CROP_MOVE, options.cropmove);
            }

            if ($.isFunction(options.cropend)) {
                $this.on(EVENT_CROP_END, options.cropend);
            }

            if ($.isFunction(options.crop)) {
                $this.on(EVENT_CROP, options.crop);
            }

            if ($.isFunction(options.zoom)) {
                $this.on(EVENT_ZOOM, options.zoom);
            }

            $cropper.on(EVENT_MOUSE_DOWN, $.proxy(this.cropStart, this));

            //if (options.zoomable && options.zoomOnWheel) {
            //  $cropper.on(EVENT_WHEEL, $.proxy(this.wheel, this));
            //}

            if (options.toggleDragModeOnDblclick) {
                $cropper.on(EVENT_DBLCLICK, $.proxy(this.dblclick, this));
            }

            $document.
              on(EVENT_MOUSE_MOVE, (this._cropMove = proxy(this.cropMove, this))).
              on(EVENT_MOUSE_UP, (this._cropEnd = proxy(this.cropEnd, this)));

            if (options.responsive) {
                $window.on(EVENT_RESIZE, (this._resize = proxy(this.resize, this)));
            }
        },

        unbind: function () {
            var options = this.options;
            var $this = this.$element;
            var $cropper = this.$cropper;

            if ($.isFunction(options.cropstart)) {
                $this.off(EVENT_CROP_START, options.cropstart);
            }

            if ($.isFunction(options.cropmove)) {
                $this.off(EVENT_CROP_MOVE, options.cropmove);
            }

            if ($.isFunction(options.cropend)) {
                $this.off(EVENT_CROP_END, options.cropend);
            }

            if ($.isFunction(options.crop)) {
                $this.off(EVENT_CROP, options.crop);
            }

            if ($.isFunction(options.zoom)) {
                $this.off(EVENT_ZOOM, options.zoom);
            }

            $cropper.off(EVENT_MOUSE_DOWN, this.cropStart);

            if (options.zoomable && options.zoomOnWheel) {
                $cropper.off(EVENT_WHEEL, this.wheel);
            }

            if (options.toggleDragModeOnDblclick) {
                $cropper.off(EVENT_DBLCLICK, this.dblclick);
            }

            $document.
              off(EVENT_MOUSE_MOVE, this._cropMove).
              off(EVENT_MOUSE_UP, this._cropEnd);

            if (options.responsive) {
                $window.off(EVENT_RESIZE, this._resize);
            }
        }
    });

    $.extend(prototype, {
        resize: function () {
            var $container = this.$container;
            var container = this.container;
            var canvasData;
            var cropBoxData;
            var ratio;

            // Check `container` is necessary for IE8
            if (this.isDisabled || !container) {
                return;
            }

            ratio = $container.width() / container.width;

            // Resize when width changed or height changed
            if (ratio !== 1 || $container.height() !== container.height) {
                canvasData = this.getCanvasData();
                cropBoxData = this.getCropBoxData();

                this.render();
                this.setCanvasData($.each(canvasData, function (i, n) {
                    canvasData[i] = n * ratio;
                }));
                this.setCropBoxData($.each(cropBoxData, function (i, n) {
                    cropBoxData[i] = n * ratio;
                }));
            }
        },

        dblclick: function () {
            if (this.isDisabled) {
                return;
            }

            if (this.$dragBox.hasClass(CLASS_CROP)) {
                this.setDragMode(ACTION_MOVE);
            } else {
                this.setDragMode(ACTION_CROP);
            }
        },

        wheel: function (event) {
            var originalEvent = event.originalEvent;
            var e = originalEvent || event;
            var ratio = num(this.options.wheelZoomRatio) || 0.1;
            var delta = 1;

            if (this.isDisabled) {
                return;
            }

            event.preventDefault();

            if (e.deltaY) {
                delta = e.deltaY > 0 ? 1 : -1;
            } else if (e.wheelDelta) {
                delta = -e.wheelDelta / 120;
            } else if (e.detail) {
                delta = e.detail > 0 ? 1 : -1;
            }

            this.zoom(-delta * ratio, originalEvent);
        },

        cropStart: function (event) {
            var options = this.options;
            var originalEvent = event.originalEvent;
            var touches = originalEvent && originalEvent.touches;
            var e = event;
            var touchesLength;
            var action;

            if (this.isDisabled) {
                return;
            }

            if (touches) {
                touchesLength = touches.length;

                if (touchesLength > 1) {
                    if (options.zoomable && options.zoomOnTouch && touchesLength === 2) {
                        e = touches[1];
                        this.startX2 = e.pageX;
                        this.startY2 = e.pageY;
                        action = ACTION_ZOOM;
                    } else {
                        return;
                    }
                }

                e = touches[0];
            }

            action = action || $(e.target).data(DATA_ACTION);

            if (REGEXP_ACTIONS.test(action)) {
                if (this.trigger(EVENT_CROP_START, {
                    originalEvent: originalEvent,
                    action: action
                }).isDefaultPrevented()) {
                    return;
                }

                event.preventDefault();

                this.action = action;
                this.cropping = false;

                // IE8  has `event.pageX/Y`, but not `event.originalEvent.pageX/Y`
                // IE10 has `event.originalEvent.pageX/Y`, but not `event.pageX/Y`
                this.startX = e.pageX || originalEvent && originalEvent.pageX;
                this.startY = e.pageY || originalEvent && originalEvent.pageY;

                if (action === ACTION_CROP) {
                    this.cropping = true;
                    this.$dragBox.addClass(CLASS_MODAL);
                }
            }
        },

        cropMove: function (event) {
            var options = this.options;
            var originalEvent = event.originalEvent;
            var touches = originalEvent && originalEvent.touches;
            var e = event;
            var action = this.action;
            var touchesLength;

            if (this.isDisabled) {
                return;
            }

            if (touches) {
                touchesLength = touches.length;

                if (touchesLength > 1) {
                    if (options.zoomable && options.zoomOnTouch && touchesLength === 2) {
                        e = touches[1];
                        this.endX2 = e.pageX;
                        this.endY2 = e.pageY;
                    } else {
                        return;
                    }
                }

                e = touches[0];
            }

            if (action) {
                if (this.trigger(EVENT_CROP_MOVE, {
                    originalEvent: originalEvent,
                    action: action
                }).isDefaultPrevented()) {
                    return;
                }

                event.preventDefault();

                this.endX = e.pageX || originalEvent && originalEvent.pageX;
                this.endY = e.pageY || originalEvent && originalEvent.pageY;

                this.change(e.shiftKey, action === ACTION_ZOOM ? originalEvent : null);
            }
        },

        cropEnd: function (event) {
            var originalEvent = event.originalEvent;
            var action = this.action;

            if (this.isDisabled) {
                return;
            }

            if (action) {
                event.preventDefault();

                if (this.cropping) {
                    this.cropping = false;
                    this.$dragBox.toggleClass(CLASS_MODAL, this.isCropped && this.options.modal);
                }

                this.action = '';

                this.trigger(EVENT_CROP_END, {
                    originalEvent: originalEvent,
                    action: action
                });
            }
        }
    });

    $.extend(prototype, {
        change: function (shiftKey, originalEvent) {
            var options = this.options;
            var aspectRatio = options.aspectRatio;
            var action = this.action;
            var container = this.container;
            var canvas = this.canvas;
            var cropBox = this.cropBox;
            var width = cropBox.width;
            var height = cropBox.height;
            var left = cropBox.left;
            var top = cropBox.top;
            var right = left + width;
            var bottom = top + height;
            var minLeft = 0;
            var minTop = 0;
            var maxWidth = container.width;
            var maxHeight = container.height;
            var renderable = true;
            var offset;
            var range;

            // Locking aspect ratio in "free mode" by holding shift key (#259)
            if (!aspectRatio && shiftKey) {
                aspectRatio = width && height ? width / height : 1;
            }

            if (this.limited) {
                minLeft = cropBox.minLeft;
                minTop = cropBox.minTop;
                maxWidth = minLeft + min(container.width, canvas.width);
                maxHeight = minTop + min(container.height, canvas.height);
            }

            range = {
                x: this.endX - this.startX,
                y: this.endY - this.startY
            };

            if (aspectRatio) {
                range.X = range.y * aspectRatio;
                range.Y = range.x / aspectRatio;
            }

            switch (action) {
                // Move crop box
                case ACTION_ALL:
                    left += range.x;
                    top += range.y;
                    break;

                    // Resize crop box
                case ACTION_EAST:
                    if (range.x >= 0 && (right >= maxWidth || aspectRatio &&
                      (top <= minTop || bottom >= maxHeight))) {

                        renderable = false;
                        break;
                    }

                    width += range.x;

                    if (aspectRatio) {
                        height = width / aspectRatio;
                        top -= range.Y / 2;
                    }

                    if (width < 0) {
                        action = ACTION_WEST;
                        width = 0;
                    }

                    break;

                case ACTION_NORTH:
                    if (range.y <= 0 && (top <= minTop || aspectRatio &&
                      (left <= minLeft || right >= maxWidth))) {

                        renderable = false;
                        break;
                    }

                    height -= range.y;
                    top += range.y;

                    if (aspectRatio) {
                        width = height * aspectRatio;
                        left += range.X / 2;
                    }

                    if (height < 0) {
                        action = ACTION_SOUTH;
                        height = 0;
                    }

                    break;

                case ACTION_WEST:
                    if (range.x <= 0 && (left <= minLeft || aspectRatio &&
                      (top <= minTop || bottom >= maxHeight))) {

                        renderable = false;
                        break;
                    }

                    width -= range.x;
                    left += range.x;

                    if (aspectRatio) {
                        height = width / aspectRatio;
                        top += range.Y / 2;
                    }

                    if (width < 0) {
                        action = ACTION_EAST;
                        width = 0;
                    }

                    break;

                case ACTION_SOUTH:
                    if (range.y >= 0 && (bottom >= maxHeight || aspectRatio &&
                      (left <= minLeft || right >= maxWidth))) {

                        renderable = false;
                        break;
                    }

                    height += range.y;

                    if (aspectRatio) {
                        width = height * aspectRatio;
                        left -= range.X / 2;
                    }

                    if (height < 0) {
                        action = ACTION_NORTH;
                        height = 0;
                    }

                    break;

                case ACTION_NORTH_EAST:
                    if (aspectRatio) {
                        if (range.y <= 0 && (top <= minTop || right >= maxWidth)) {
                            renderable = false;
                            break;
                        }

                        height -= range.y;
                        top += range.y;
                        width = height * aspectRatio;
                    } else {
                        if (range.x >= 0) {
                            if (right < maxWidth) {
                                width += range.x;
                            } else if (range.y <= 0 && top <= minTop) {
                                renderable = false;
                            }
                        } else {
                            width += range.x;
                        }

                        if (range.y <= 0) {
                            if (top > minTop) {
                                height -= range.y;
                                top += range.y;
                            }
                        } else {
                            height -= range.y;
                            top += range.y;
                        }
                    }

                    if (width < 0 && height < 0) {
                        action = ACTION_SOUTH_WEST;
                        height = 0;
                        width = 0;
                    } else if (width < 0) {
                        action = ACTION_NORTH_WEST;
                        width = 0;
                    } else if (height < 0) {
                        action = ACTION_SOUTH_EAST;
                        height = 0;
                    }

                    break;

                case ACTION_NORTH_WEST:
                    if (aspectRatio) {
                        if (range.y <= 0 && (top <= minTop || left <= minLeft)) {
                            renderable = false;
                            break;
                        }

                        height -= range.y;
                        top += range.y;
                        width = height * aspectRatio;
                        left += range.X;
                    } else {
                        if (range.x <= 0) {
                            if (left > minLeft) {
                                width -= range.x;
                                left += range.x;
                            } else if (range.y <= 0 && top <= minTop) {
                                renderable = false;
                            }
                        } else {
                            width -= range.x;
                            left += range.x;
                        }

                        if (range.y <= 0) {
                            if (top > minTop) {
                                height -= range.y;
                                top += range.y;
                            }
                        } else {
                            height -= range.y;
                            top += range.y;
                        }
                    }

                    if (width < 0 && height < 0) {
                        action = ACTION_SOUTH_EAST;
                        height = 0;
                        width = 0;
                    } else if (width < 0) {
                        action = ACTION_NORTH_EAST;
                        width = 0;
                    } else if (height < 0) {
                        action = ACTION_SOUTH_WEST;
                        height = 0;
                    }

                    break;

                case ACTION_SOUTH_WEST:
                    if (aspectRatio) {
                        if (range.x <= 0 && (left <= minLeft || bottom >= maxHeight)) {
                            renderable = false;
                            break;
                        }

                        width -= range.x;
                        left += range.x;
                        height = width / aspectRatio;
                    } else {
                        if (range.x <= 0) {
                            if (left > minLeft) {
                                width -= range.x;
                                left += range.x;
                            } else if (range.y >= 0 && bottom >= maxHeight) {
                                renderable = false;
                            }
                        } else {
                            width -= range.x;
                            left += range.x;
                        }

                        if (range.y >= 0) {
                            if (bottom < maxHeight) {
                                height += range.y;
                            }
                        } else {
                            height += range.y;
                        }
                    }

                    if (width < 0 && height < 0) {
                        action = ACTION_NORTH_EAST;
                        height = 0;
                        width = 0;
                    } else if (width < 0) {
                        action = ACTION_SOUTH_EAST;
                        width = 0;
                    } else if (height < 0) {
                        action = ACTION_NORTH_WEST;
                        height = 0;
                    }

                    break;

                case ACTION_SOUTH_EAST:
                    if (aspectRatio) {
                        if (range.x >= 0 && (right >= maxWidth || bottom >= maxHeight)) {
                            renderable = false;
                            break;
                        }

                        width += range.x;
                        height = width / aspectRatio;
                    } else {
                        if (range.x >= 0) {
                            if (right < maxWidth) {
                                width += range.x;
                            } else if (range.y >= 0 && bottom >= maxHeight) {
                                renderable = false;
                            }
                        } else {
                            width += range.x;
                        }

                        if (range.y >= 0) {
                            if (bottom < maxHeight) {
                                height += range.y;
                            }
                        } else {
                            height += range.y;
                        }
                    }

                    if (width < 0 && height < 0) {
                        action = ACTION_NORTH_WEST;
                        height = 0;
                        width = 0;
                    } else if (width < 0) {
                        action = ACTION_SOUTH_WEST;
                        width = 0;
                    } else if (height < 0) {
                        action = ACTION_NORTH_EAST;
                        height = 0;
                    }

                    break;

                    // Move canvas
                case ACTION_MOVE:
                    this.move(range.x, range.y);
                    renderable = false;
                    break;

                    // Zoom canvas
                case ACTION_ZOOM:

                    this.zoom((function (x1, y1, x2, y2) {
                        var z1 = sqrt(x1 * x1 + y1 * y1);
                        var z2 = sqrt(x2 * x2 + y2 * y2);

                        return (z2 - z1) / z1;
                    })(
                      abs(this.startX - this.startX2),
                      abs(this.startY - this.startY2),
                      abs(this.endX - this.endX2),
                      abs(this.endY - this.endY2)
                    ), originalEvent);
                    this.startX2 = this.endX2;
                    this.startY2 = this.endY2;
                    renderable = false;
                    break;

                    // Create crop box
                case ACTION_CROP:
                    if (!range.x || !range.y) {
                        renderable = false;
                        break;
                    }

                    offset = this.$cropper.offset();
                    left = this.startX - offset.left;
                    top = this.startY - offset.top;
                    width = cropBox.minWidth;
                    height = cropBox.minHeight;

                    if (range.x > 0) {
                        action = range.y > 0 ? ACTION_SOUTH_EAST : ACTION_NORTH_EAST;
                    } else if (range.x < 0) {
                        left -= width;
                        action = range.y > 0 ? ACTION_SOUTH_WEST : ACTION_NORTH_WEST;
                    }

                    if (range.y < 0) {
                        top -= height;
                    }

                    // Show the crop box if is hidden
                    if (!this.isCropped) {
                        this.$cropBox.removeClass(CLASS_HIDDEN);
                        this.isCropped = true;

                        if (this.limited) {
                            this.limitCropBox(true, true);
                        }
                    }

                    break;

                    // No default
            }

            if (renderable) {
                cropBox.width = width;
                cropBox.height = height;
                cropBox.left = left;
                cropBox.top = top;
                this.action = action;

                this.renderCropBox();
            }

            // Override
            this.startX = this.endX;
            this.startY = this.endY;
        }
    });

    $.extend(prototype, {

        // Show the crop box manually
        crop: function () {
            if (!this.isBuilt || this.isDisabled) {
                return;
            }

            if (!this.isCropped) {
                this.isCropped = true;
                this.limitCropBox(true, true);

                if (this.options.modal) {
                    this.$dragBox.addClass(CLASS_MODAL);
                }

                this.$cropBox.removeClass(CLASS_HIDDEN);
            }

            this.setCropBoxData(this.initialCropBox);
        },

        // Reset the image and crop box to their initial states
        reset: function () {
            if (!this.isBuilt || this.isDisabled) {
                return;
            }

            this.image = $.extend({}, this.initialImage);
            this.canvas = $.extend({}, this.initialCanvas);
            this.cropBox = $.extend({}, this.initialCropBox);

            this.renderCanvas();

            if (this.isCropped) {
                this.renderCropBox();
            }
        },

        // Clear the crop box
        clear: function () {
            if (!this.isCropped || this.isDisabled) {
                return;
            }

            $.extend(this.cropBox, {
                left: 0,
                top: 0,
                width: 0,
                height: 0
            });

            this.isCropped = false;
            this.renderCropBox();

            this.limitCanvas(true, true);

            // Render canvas after crop box rendered
            this.renderCanvas();

            this.$dragBox.removeClass(CLASS_MODAL);
            this.$cropBox.addClass(CLASS_HIDDEN);
        },

        /**
         * Replace the image's src and rebuild the cropper
         *
         * @param {String} url
         */
        replace: function (url) {
            if (!this.isDisabled && url) {
                if (this.isImg) {
                    this.isReplaced = true;
                    this.$element.attr('src', url);
                }

                // Clear previous data
                this.options.data = null;
                this.load(url);
            }
        },

        // Enable (unfreeze) the cropper
        enable: function () {
            if (this.isBuilt) {
                this.isDisabled = false;
                this.$cropper.removeClass(CLASS_DISABLED);
            }
        },

        // Disable (freeze) the cropper
        disable: function () {
            if (this.isBuilt) {
                this.isDisabled = true;
                this.$cropper.addClass(CLASS_DISABLED);
            }
        },

        // Destroy the cropper and remove the instance from the image
        destroy: function () {
            var $this = this.$element;

            if (this.isLoaded) {
                if (this.isImg && this.isReplaced) {
                    $this.attr('src', this.originalUrl);
                }

                this.unbuild();
                $this.removeClass(CLASS_HIDDEN);
            } else {
                if (this.isImg) {
                    $this.off(EVENT_LOAD, this.start);
                } else if (this.$clone) {
                    this.$clone.remove();
                }
            }

            $this.removeData(NAMESPACE);
        },

        /**
         * Move the canvas with relative offsets
         *
         * @param {Number} offsetX
         * @param {Number} offsetY (optional)
         */
        move: function (offsetX, offsetY) {
            var canvas = this.canvas;

            this.moveTo(
              isUndefined(offsetX) ? offsetX : canvas.left + num(offsetX),
              isUndefined(offsetY) ? offsetY : canvas.top + num(offsetY)
            );
        },

        /**
         * Move the canvas to an absolute point
         *
         * @param {Number} x
         * @param {Number} y (optional)
         */
        moveTo: function (x, y) {
            var canvas = this.canvas;
            var isChanged = false;

            // If "y" is not present, its default value is "x"
            if (isUndefined(y)) {
                y = x;
            }

            x = num(x);
            y = num(y);

            if (this.isBuilt && !this.isDisabled && this.options.movable) {
                if (isNumber(x)) {
                    canvas.left = x;
                    isChanged = true;
                }

                if (isNumber(y)) {
                    canvas.top = y;
                    isChanged = true;
                }

                if (isChanged) {
                    this.renderCanvas(true);
                }
            }
        },

        /**
         * Zoom the canvas with a relative ratio
         *
         * @param {Number} ratio
         * @param {Event} _originalEvent (private)
         */
        zoom: function (ratio, _originalEvent) {
            var canvas = this.canvas;

            ratio = num(ratio);

            if (ratio < 0) {
                ratio = 1 / (1 - ratio);
            } else {
                ratio = 1 + ratio;
            }

            this.zoomTo(canvas.width * ratio / canvas.naturalWidth, _originalEvent);
        },

        /**
         * Zoom the canvas to an absolute ratio
         *
         * @param {Number} ratio
         * @param {Event} _originalEvent (private)
         */
        zoomTo: function (ratio, _originalEvent) {
            var options = this.options;
            var canvas = this.canvas;
            var width = canvas.width;
            var height = canvas.height;
            var naturalWidth = canvas.naturalWidth;
            var naturalHeight = canvas.naturalHeight;
            var newWidth;
            var newHeight;

            ratio = num(ratio);

            if (ratio >= 0 && this.isBuilt && !this.isDisabled && options.zoomable) {
                newWidth = naturalWidth * ratio;
                newHeight = naturalHeight * ratio;

                if (this.trigger(EVENT_ZOOM, {
                    originalEvent: _originalEvent,
                    oldRatio: width / naturalWidth,
                    ratio: newWidth / naturalWidth
                }).isDefaultPrevented()) {
                    return;
                }

                canvas.left -= (newWidth - width) / 2;
                canvas.top -= (newHeight - height) / 2;
                canvas.width = newWidth;
                canvas.height = newHeight;
                this.renderCanvas(true);
            }
        },

        /**
         * Rotate the canvas with a relative degree
         *
         * @param {Number} degree
         */
        rotate: function (degree) {
            this.rotateTo((this.image.rotate || 0) + num(degree));
        },

        /**
         * Rotate the canvas to an absolute degree
         * https://developer.mozilla.org/en-US/docs/Web/CSS/transform-function#rotate()
         *
         * @param {Number} degree
         */
        rotateTo: function (degree) {
            degree = num(degree);

            if (isNumber(degree) && this.isBuilt && !this.isDisabled && this.options.rotatable) {
                this.image.rotate = degree % 360;
                this.isRotated = true;
                this.renderCanvas(true);
            }
        },

        /**
         * Scale the image
         * https://developer.mozilla.org/en-US/docs/Web/CSS/transform-function#scale()
         *
         * @param {Number} scaleX
         * @param {Number} scaleY (optional)
         */
        scale: function (scaleX, scaleY) {
            var image = this.image;
            var isChanged = false;

            // If "scaleY" is not present, its default value is "scaleX"
            if (isUndefined(scaleY)) {
                scaleY = scaleX;
            }

            scaleX = num(scaleX);
            scaleY = num(scaleY);

            if (this.isBuilt && !this.isDisabled && this.options.scalable) {
                if (isNumber(scaleX)) {
                    image.scaleX = scaleX;
                    isChanged = true;
                }

                if (isNumber(scaleY)) {
                    image.scaleY = scaleY;
                    isChanged = true;
                }

                if (isChanged) {
                    this.renderImage(true);
                }
            }
        },

        /**
         * Scale the abscissa of the image
         *
         * @param {Number} scaleX
         */
        scaleX: function (scaleX) {
            var scaleY = this.image.scaleY;

            this.scale(scaleX, isNumber(scaleY) ? scaleY : 1);
        },

        /**
         * Scale the ordinate of the image
         *
         * @param {Number} scaleY
         */
        scaleY: function (scaleY) {
            var scaleX = this.image.scaleX;

            this.scale(isNumber(scaleX) ? scaleX : 1, scaleY);
        },

        /**
         * Get the cropped area position and size data (base on the original image)
         *
         * @param {Boolean} isRounded (optional)
         * @return {Object} data
         */
        getData: function (isRounded) {
            var options = this.options;
            var image = this.image;
            var canvas = this.canvas;
            var cropBox = this.cropBox;
            var ratio;
            var data;

            if (this.isBuilt && this.isCropped) {
                data = {
                    x: cropBox.left - canvas.left,
                    y: cropBox.top - canvas.top,
                    width: cropBox.width,
                    height: cropBox.height
                };

                ratio = image.width / image.naturalWidth;

                $.each(data, function (i, n) {
                    n = n / ratio;
                    data[i] = isRounded ? round(n) : n;
                });

            } else {
                data = {
                    x: 0,
                    y: 0,
                    width: 0,
                    height: 0
                };
            }

            if (options.rotatable) {
                data.rotate = image.rotate || 0;
            }

            if (options.scalable) {
                data.scaleX = image.scaleX || 1;
                data.scaleY = image.scaleY || 1;
            }

            return data;
        },

        /**
         * Set the cropped area position and size with new data
         *
         * @param {Object} data
         */
        setData: function (data) {
            var options = this.options;
            var image = this.image;
            var canvas = this.canvas;
            var cropBoxData = {};
            var isRotated;
            var isScaled;
            var ratio;

            if ($.isFunction(data)) {
                data = data.call(this.element);
            }

            if (this.isBuilt && !this.isDisabled && $.isPlainObject(data)) {
                if (options.rotatable) {
                    if (isNumber(data.rotate) && data.rotate !== image.rotate) {
                        image.rotate = data.rotate;
                        this.isRotated = isRotated = true;
                    }
                }

                if (options.scalable) {
                    if (isNumber(data.scaleX) && data.scaleX !== image.scaleX) {
                        image.scaleX = data.scaleX;
                        isScaled = true;
                    }

                    if (isNumber(data.scaleY) && data.scaleY !== image.scaleY) {
                        image.scaleY = data.scaleY;
                        isScaled = true;
                    }
                }

                if (isRotated) {
                    this.renderCanvas();
                } else if (isScaled) {
                    this.renderImage();
                }

                ratio = image.width / image.naturalWidth;

                if (isNumber(data.x)) {
                    cropBoxData.left = data.x * ratio + canvas.left;
                }

                if (isNumber(data.y)) {
                    cropBoxData.top = data.y * ratio + canvas.top;
                }

                if (isNumber(data.width)) {
                    cropBoxData.width = data.width * ratio;
                }

                if (isNumber(data.height)) {
                    cropBoxData.height = data.height * ratio;
                }

                this.setCropBoxData(cropBoxData);
            }
        },

        /**
         * Get the container size data
         *
         * @return {Object} data
         */
        getContainerData: function () {
            return this.isBuilt ? this.container : {};
        },

        /**
         * Get the image position and size data
         *
         * @return {Object} data
         */
        getImageData: function () {
            return this.isLoaded ? this.image : {};
        },

        /**
         * Get the canvas position and size data
         *
         * @return {Object} data
         */
        getCanvasData: function () {
            var canvas = this.canvas;
            var data = {};

            if (this.isBuilt) {
                $.each([
                  'left',
                  'top',
                  'width',
                  'height',
                  'naturalWidth',
                  'naturalHeight'
                ], function (i, n) {
                    data[n] = canvas[n];
                });
            }

            return data;
        },

        /**
         * Set the canvas position and size with new data
         *
         * @param {Object} data
         */
        setCanvasData: function (data) {
            var canvas = this.canvas;
            var aspectRatio = canvas.aspectRatio;

            if ($.isFunction(data)) {
                data = data.call(this.$element);
            }

            if (this.isBuilt && !this.isDisabled && $.isPlainObject(data)) {
                if (isNumber(data.left)) {
                    canvas.left = data.left;
                }

                if (isNumber(data.top)) {
                    canvas.top = data.top;
                }

                if (isNumber(data.width)) {
                    canvas.width = data.width;
                    canvas.height = data.width / aspectRatio;
                } else if (isNumber(data.height)) {
                    canvas.height = data.height;
                    canvas.width = data.height * aspectRatio;
                }

                this.renderCanvas(true);
            }
        },

        /**
         * Get the crop box position and size data
         *
         * @return {Object} data
         */
        getCropBoxData: function () {
            var cropBox = this.cropBox;
            var data;

            if (this.isBuilt && this.isCropped) {
                data = {
                    left: cropBox.left,
                    top: cropBox.top,
                    width: cropBox.width,
                    height: cropBox.height
                };
            }

            return data || {};
        },

        /**
         * Set the crop box position and size with new data
         *
         * @param {Object} data
         */
        setCropBoxData: function (data) {

            var cropBox = this.cropBox;
            var aspectRatio = this.options.aspectRatio;
            var isWidthChanged;
            var isHeightChanged;

            if ($.isFunction(data)) {
                data = data.call(this.$element);
            }

            if (this.isBuilt && this.isCropped && !this.isDisabled && $.isPlainObject(data)) {

                if (isNumber(data.left)) {
                    cropBox.left = data.left;
                }

                if (isNumber(data.top)) {
                    cropBox.top = data.top;
                }

                if (isNumber(data.width) && data.width !== cropBox.width) {
                    isWidthChanged = true;
                    cropBox.width = data.width;
                }

                if (isNumber(data.height) && data.height !== cropBox.height) {
                    isHeightChanged = true;
                    cropBox.height = data.height;
                }

                if (aspectRatio) {
                    if (isWidthChanged) {
                        cropBox.height = cropBox.width / aspectRatio;
                    } else if (isHeightChanged) {
                        cropBox.width = cropBox.height * aspectRatio;
                    }
                }

                this.renderCropBox();
            }
        },

        /**
         * Get a canvas drawn the cropped image
         *
         * @param {Object} options (optional)
         * @return {HTMLCanvasElement} canvas
         */
        getCroppedCanvas: function (options) {
            var originalWidth;
            var originalHeight;
            var canvasWidth;
            var canvasHeight;
            var scaledWidth;
            var scaledHeight;
            var scaledRatio;
            var aspectRatio;
            var canvas;
            var context;
            var data;

            if (!this.isBuilt || !this.isCropped || !SUPPORT_CANVAS) {
                return;
            }

            if (!$.isPlainObject(options)) {
                options = {};
            }

            data = this.getData();
            originalWidth = data.width;
            originalHeight = data.height;
            aspectRatio = originalWidth / originalHeight;

            if ($.isPlainObject(options)) {
                scaledWidth = options.width;
                scaledHeight = options.height;

                if (scaledWidth) {
                    scaledHeight = scaledWidth / aspectRatio;
                    scaledRatio = scaledWidth / originalWidth;
                } else if (scaledHeight) {
                    scaledWidth = scaledHeight * aspectRatio;
                    scaledRatio = scaledHeight / originalHeight;
                }
            }


            canvasWidth = round(scaledWidth || originalWidth);
            canvasHeight = round(scaledHeight || originalHeight);

            canvas = $('<canvas>')[0];
            canvas.width = canvasWidth;
            canvas.height = canvasHeight;
            context = canvas.getContext('2d');

            if (options.fillColor) {
                context.fillStyle = options.fillColor;
                context.fillRect(0, 0, canvasWidth, canvasHeight);
            }

            // https://developer.mozilla.org/en-US/docs/Web/API/CanvasRenderingContext2D.drawImage
            context.drawImage.apply(context, (function () {
                var source = getSourceCanvas(this.$clone[0], this.image);
                var sourceWidth = source.width;
                var sourceHeight = source.height;
                var args = [source];

                // Source canvas
                var srcX = data.x;
                var srcY = data.y;
                var srcWidth;
                var srcHeight;

                // Destination canvas
                var dstX;
                var dstY;
                var dstWidth;
                var dstHeight;

                if (srcX <= -originalWidth || srcX > sourceWidth) {
                    srcX = srcWidth = dstX = dstWidth = 0;
                } else if (srcX <= 0) {
                    dstX = -srcX;
                    srcX = 0;
                    srcWidth = dstWidth = min(sourceWidth, originalWidth + srcX);
                } else if (srcX <= sourceWidth) {
                    dstX = 0;
                    srcWidth = dstWidth = min(originalWidth, sourceWidth - srcX);
                }

                if (srcWidth <= 0 || srcY <= -originalHeight || srcY > sourceHeight) {
                    srcY = srcHeight = dstY = dstHeight = 0;
                } else if (srcY <= 0) {
                    dstY = -srcY;
                    srcY = 0;
                    srcHeight = dstHeight = min(sourceHeight, originalHeight + srcY);
                } else if (srcY <= sourceHeight) {
                    dstY = 0;
                    srcHeight = dstHeight = min(originalHeight, sourceHeight - srcY);
                }

                args.push(srcX, srcY, srcWidth, srcHeight);

                // Scale destination sizes
                if (scaledRatio) {
                    dstX *= scaledRatio;
                    dstY *= scaledRatio;
                    dstWidth *= scaledRatio;
                    dstHeight *= scaledRatio;
                }

                // Avoid "IndexSizeError" in IE and Firefox
                if (dstWidth > 0 && dstHeight > 0) {
                    args.push(dstX, dstY, dstWidth, dstHeight);
                }

                return args;
            }).call(this));

            return canvas;
        },

        /**
         * Change the aspect ratio of the crop box
         *
         * @param {Number} aspectRatio
         */
        setAspectRatio: function (aspectRatio) {
            var options = this.options;

            if (!this.isDisabled && !isUndefined(aspectRatio)) {

                // 0 -> NaN
                options.aspectRatio = max(0, aspectRatio) || NaN;

                if (this.isBuilt) {
                    this.initCropBox();

                    if (this.isCropped) {
                        this.renderCropBox();
                    }
                }
            }
        },

        /**
         * Change the drag mode
         *
         * @param {String} mode (optional)
         */
        setDragMode: function (mode) {
            var options = this.options;
            var croppable;
            var movable;

            if (this.isLoaded && !this.isDisabled) {
                croppable = mode === ACTION_CROP;
                movable = options.movable && mode === ACTION_MOVE;
                mode = (croppable || movable) ? mode : ACTION_NONE;

                this.$dragBox.
                  data(DATA_ACTION, mode).
                  toggleClass(CLASS_CROP, croppable).
                  toggleClass(CLASS_MOVE, movable);

                if (!options.cropBoxMovable) {

                    // Sync drag mode to crop box when it is not movable(#300)
                    this.$face.
                      data(DATA_ACTION, mode).
                      toggleClass(CLASS_CROP, croppable).
                      toggleClass(CLASS_MOVE, movable);
                }
            }
        }
    });

    $.extend(Cropper.prototype, prototype);

    Cropper.DEFAULTS = {

        // Define the view mode of the cropper
        viewMode: 0, // 0, 1, 2, 3

        // Define the dragging mode of the cropper
        dragMode: 'crop', // 'crop', 'move' or 'none'

        // Define the aspect ratio of the crop box
        aspectRatio: NaN,

        // An object with the previous cropping result data
        data: null,

        // A jQuery selector for adding extra containers to preview
        preview: '',

        // Rebuild when resize the window
        responsive: true,

        // Check if the target image is cross origin
        checkCrossOrigin: true,

        // Show the black modal
        modal: true,

        // Show the dashed lines for guiding
        guides: true,

        // Show the center indicator for guiding
        center: true,

        // Show the white modal to highlight the crop box
        highlight: true,

        // Show the grid background
        background: true,

        // Enable to crop the image automatically when initialize
        autoCrop: true,

        // Define the percentage of automatic cropping area when initializes
        autoCropArea: 1,

        // Enable to move the image
        movable: true,

        // Enable to rotate the image
        rotatable: true,

        // Enable to scale the image
        scalable: true,

        // Enable to zoom the image
        zoomable: true,

        // Enable to zoom the image by dragging touch
        zoomOnTouch: true,

        // Enable to zoom the image by wheeling mouse
        zoomOnWheel: true,

        // Define zoom ratio when zoom the image by wheeling mouse
        wheelZoomRatio: 0.1,

        // Enable to move the crop box
        cropBoxMovable: true,

        // Enable to resize the crop box
        cropBoxResizable: true,

        // Toggle drag mode between "crop" and "move" when click twice on the cropper
        toggleDragModeOnDblclick: false,


        minCanvasWidth: 288,
        minCanvasHeight: 180,
        minCropBoxWidth: 240,
        minCropBoxHeight: 150,
        minContainerWidth: 288,
        minContainerHeight: 180,

        // Shortcuts of events
        build: null,
        built: null,
        cropstart: null,
        cropmove: null,
        cropend: null,
        crop: null,
        zoom: null
    };

    Cropper.setDefaults = function (options) {
        $.extend(Cropper.DEFAULTS, options);
    };

    Cropper.TEMPLATE = (
      '<div class="cropper-container" >' +
        '<div class="cropper-wrap-box">' +
          '<div class="cropper-canvas"></div>' +
        '</div>' +
        '<div class="cropper-drag-box"></div>' +
        '<div class="cropper-crop-box">' +
          '<span class="cropper-view-box"></span>' +
          '<span class="cropper-dashed dashed-h"></span>' +
          '<span class="cropper-dashed dashed-v"></span>' +
          '<span class="cropper-center"></span>' +
          '<span class="cropper-face"></span>' +
          '<span class="cropper-line line-e" data-action="e"></span>' +
          '<span class="cropper-line line-n" data-action="n"></span>' +
          '<span class="cropper-line line-w" data-action="w"></span>' +
          '<span class="cropper-line line-s" data-action="s"></span>' +
          '<span class="cropper-point point-e" data-action="e"></span>' +
          '<span class="cropper-point point-n" data-action="n"></span>' +
          '<span class="cropper-point point-w" data-action="w"></span>' +
          '<span class="cropper-point point-s" data-action="s"></span>' +
          '<span class="cropper-point point-ne" data-action="ne"></span>' +
          '<span class="cropper-point point-nw" data-action="nw"></span>' +
          '<span class="cropper-point point-sw" data-action="sw"></span>' +
          '<span class="cropper-point point-se" data-action="se"></span>' +
        '</div>' +
      '</div>'
    );

    // Save the other cropper
    Cropper.other = $.fn.cropper;

    // Register as jQuery plugin
    $.fn.cropper = function (options) {
        var args = toArray(arguments, 1);
        var result;

        this.each(function () {
            var $this = $(this);
            var data = $this.data(NAMESPACE);
            var fn;

            if (!data) {
                if (/destroy/.test(options)) {
                    return;
                }

                $this.data(NAMESPACE, (data = new Cropper(this, options)));
            }

            if (typeof options === 'string' && $.isFunction(fn = data[options])) {
                result = fn.apply(data, args);
            }
        });

        return isUndefined(result) ? this : result;
    };

    $.fn.cropper.Constructor = Cropper;
    $.fn.cropper.setDefaults = Cropper.setDefaults;

    // No conflict
    $.fn.cropper.noConflict = function () {
        $.fn.cropper = Cropper.other;
        return this;
    };

});

//Edit Upload
function Edit(ed) {
    $(ed).parents('.dz-preview').find('.cropper-container img').css({ "transform": "none" });
    $(ed).parents('.dz-preview').eq(0).find('.tools').removeClass('hidden');
    $(ed).parents('.dz-preview').eq(0).find('.final').addClass('hidden');

    $(ed).parents('.dz-preview').eq(0).find('.cropper-move').addClass('cropper-face');
    //hiden control drop
    $(ed).parent().parent().find('#imgmain img:first').addClass('cropper-hidden');
    $(ed).parent().parent().find('#imgmain img:first').next().removeClass('cropper-hidden');
    //hiden control drop end
    dropNew(ed);
}

function S4(ed) {
    return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
}

function GuidS4() {
    return (S4() + S4() + "-" + S4() + "-4" + S4().substr(0, 3) + "-" + S4() + "-" + S4() + S4() + S4()).toLowerCase();
}

//Delete Upload 
function DeleteUpload(del) {
    eventdelete(del);
    DeleteImg(del);
}

function DeleteImg(obj) {
    var idpic = $(obj).parents(".dz-preview").find('#cusimg').attr('rel');
    $.ajax({
        type: "POST",
        url: "/Services/UploadImg.asmx/DeleteImg",
        dataType: "json",
        data: JSON.stringify({ idAdvert: $('#hdAdvertID').val(), idpicture: idpic }),
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            ChangeAgin();
        },
        error: function (e) {

        }
    });
    return false;
}
function eventdelete(del) {

    $(del).parents(".dz-preview").remove();
}
function ChangeAgin() {
    if ($('.dz-preview').length > 0) {
        $('input[type=radio]').prop("checked", true);
    }

}

////Rotate
var val = 0;

function Rate(el) {
    val = val + 90;
    eventroate(el, val);
}

function eventroate(el, d) {

    //$(el).parents(".dz-preview").eq(0).find('.dz-image').eq(0).css({
    //    '-moz-transform': 'rotate(' + d + 'deg)',
    //    '-webkit-transform': 'rotate(' + d + 'deg)',
    //    '-o-transform': 'rotate(' + d + 'deg)',
    //    '-ms-transform': 'rotate(' + d + 'deg)',
    //    'transform': 'rotate(' + d + 'deg)'
    //});
    var isrotare0 = $(el).parents(".dz-preview").eq(0).find('.dz-image').hasClass("rotare0");
    var isrotare90 = $(el).parents(".dz-preview").eq(0).find('.dz-image').hasClass("rotare90");
    var isrotare180 = $(el).parents(".dz-preview").eq(0).find('.dz-image').hasClass("rotare180");
    var isrotare270 = $(el).parents(".dz-preview").eq(0).find('.dz-image').hasClass("rotare270");
    if (isrotare0)
        $(el).parents(".dz-preview").eq(0).find('.dz-image').removeClass("rotare0").addClass('rotare90');
    if (isrotare90)
        $(el).parents(".dz-preview").eq(0).find('.dz-image').removeClass("rotare90").addClass('rotare180');
    if (isrotare180)
        $(el).parents(".dz-preview").eq(0).find('.dz-image').removeClass("rotare180").addClass('rotare270');
    if (isrotare270)
        $(el).parents(".dz-preview").eq(0).find('.dz-image').removeClass("rotare270").addClass('rotare0');

}
//Upload OK
function Ok(ed) {
    $(ed).parents('.dz-preview').eq(0).find('.tools').addClass('hidden');
    $(ed).parents('.dz-preview').eq(0).find('.final').removeClass('hidden');
    $(ed).parents('.dz-preview').eq(0).find('.cropper-move').removeClass('cropper-face');

}

//Zoom in

function ZoomIn(event) {
    ZoomIn(event);
}

function ZoomIn(el) {
    $(el).parents(".dz-preview").eq(0).find('#cusimg').eq(0).outerWidth($(el).parents(".dz-preview").eq(0).find('#cusimg').eq(0).outerWidth() * 1.2);
    $(el).parents(".dz-preview").eq(0).find('#cusimg').eq(0).outerHeight($(el).parents(".dz-preview").eq(0).find('#cusimg').eq(0).outerHeight() * 1.2);

};

//Zoom Out
function ZoomIOut(event) {
    ZoomOut(event);
}

function ZoomOut(el) {
    if (($(el).parents(".dz-preview").eq(0).find('#cusimg').eq(0).outerWidth() / 1.2) < 300) {

    } else {

        $(el).parents(".dz-preview").eq(0).find('#cusimg').eq(0).outerWidth($(el).parents(".dz-preview").eq(0).find('#cusimg').eq(0).outerWidth() / 1.2);
        $(el).parents(".dz-preview").eq(0).find('#cusimg').eq(0).outerHeight($(el).parents(".dz-preview").eq(0).find('#cusimg').eq(0).outerHeight() / 1.2);
    }
}

//Resize
function resizei(el) {
    $(el).parents(".dz-preview").eq(0).find('#cusimg').eq(0).outerWidth($(el).parents(".dz-preview").eq(0).find('#cusimg').eq(0).outerWidth(300));
    $(el).parents(".dz-preview").eq(0).find('#cusimg').eq(0).outerHeight($(el).parents(".dz-preview").eq(0).find('#cusimg').eq(0).outerHeight(300));
}



/*
 *
 * More info at [www.dropzonejs.com](http://www.dropzonejs.com)
 *
 * Copyright (c) 2012, Matias Meno
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 *
 */

(function () {

    var content =
        "<div class=\"dz-preview dz-file-preview\">\n" +
        "<div class=\"tools final \" >" +
        "<div onclick='Edit(this)'  title=\"Edit\" class=\"btn cusIconImg cusbtn cbtn-edit hidden\"><i class=\"glyphicon glyphicon-pencil\"></i></div>" +
        "<div onclick='DeleteUpload(this)' title=\"Delete\" class=\"btn cusIconImg cusbtn cusbtn-del\">" +
    "<svg version=\"1.1\" id=\"Layer_1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" x=\"0px\" y=\"0px\" width=\"18px\" height=\"18px\" viewBox=\"0 0 15.809 20\" enable-background=\"new 0 0 15.809 20\" xml:space=\"preserve\"><g>" +
    "<path fill=\"#FFFFFF\" d=\"M14.444,2.354h-2.67V0.62c0-0.35-0.281-0.62-0.631-0.62c-0.034,0-0.057,0.012-0.067,0.022" +
    "C11.064,0.012,11.042,0,11.03,0H4.664c-0.349,0-0.62,0.27-0.62,0.62v1.735H1.364C0.597,2.354,0,2.952,0,3.718V5.95h1.183v12.698" +
    "C1.183,19.414,1.769,20,2.535,20h10.738c0.767,0,1.362-0.586,1.362-1.353V5.95h1.173V3.718C15.809,2.952,15.211,2.354,14.444,2.354" +
    "L14.444,2.354z M5.284,1.239h5.239v1.115H5.284V1.239z M13.386,18.647c0,0.079-0.034,0.113-0.112,0.113H2.535" +
    "c-0.079,0-0.113-0.034-0.113-0.113V5.95h10.963V18.647z M14.558,4.71H1.251V3.718c0-0.079,0.033-0.125,0.113-0.125h13.081" +
    "c0.079,0,0.113,0.046,0.113,0.125V4.71z M14.558,4.71\"/>" +
    "<rect x=\"10.175\" y=\"7.402\" fill=\"#FFFFFF\" width=\"1.25\" height=\"10.254\"/>" +
    "<rect x=\"7.29\" y=\"7.402\" fill=\"#FFFFFF\" width=\"1.251\" height=\"10.254\"/>" +
"<rect x=\"4.406\" y=\"7.402\" fill=\"#FFFFFF\" width=\"1.25\" height=\"10.254\"/></g></svg></div></div>" +
        "<div class=\"tools hidden\" > " +
        "<div class=\"btn-group docs-buttons\">" +

          "<button title=\"Zoom In\" data-option=\"-0.1\" data-method=\"zoom\" class=\"cusIconImg btn cusbtn\" type=\"button\">" +
            "<span title=\"\" data-toggle=\"tooltip\" class=\"docs-tooltip\" data-original-title=\"$().cropper(&quot;zoom&quot;, -0.1)\">" +
              "<svg version=\"1.1\" id=\"Layer_1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" x=\"0px\" y=\"0px\"width=\"18px\" height=\"18px\" viewBox=\"0 0 20 20.637\" enable-background=\"new 0 0 20 20.637\" xml:space=\"preserve\"><g><g>" +
              "<path fill=\"#666766\" d=\"M19.186,20.608c-0.213,0-0.418-0.088-0.576-0.246l-5.834-5.834c-1.385,1.012-3.021,1.546-4.739,1.546    C3.605,16.074,0,12.469,0,8.037S3.605,0,8.037,0c4.431,0,8.037,3.605,8.037,8.037c0,1.986-0.736,3.894-2.077,5.381l5.768,5.778    c0.314,0.329,0.314,0.851,0,1.166C19.624,20.515,19.407,20.608,19.186,20.608z M8.037,1.647c-3.524,0-6.391,2.867-6.391,6.39    s2.867,6.391,6.391,6.391s6.39-2.867,6.39-6.391S11.561,1.647,8.037,1.647z\"/>" +
		"<path fill=\"#666766\" d=\"M8.037,15.969c1.783,0,3.421-0.586,4.75-1.579l5.897,5.897c0.144,0.144,0.323,0.216,0.502,0.216    c0.18,0,0.371-0.072,0.503-0.216c0.275-0.275,0.275-0.729,0-1.017l-5.838-5.85c1.304-1.412,2.117-3.302,2.117-5.384    c0-4.366-3.553-7.931-7.931-7.931S0.105,3.671,0.105,8.037S3.659,15.969,8.037,15.969L8.037,15.969z M8.037,1.542    c3.577,0,6.496,2.907,6.496,6.496s-2.906,6.496-6.496,6.496c-3.577,0-6.496-2.907-6.496-6.496S4.448,1.542,8.037,1.542    L8.037,1.542z M8.037,1.542\"/>" +
		"<rect fill=\"none\" width=\"19.997\" height=\"20.637\"/></g>" +
	"<path fill=\"#666766\" d=\"M7.286,8.836H5.118c-0.454,0-0.823-0.369-0.823-0.823c0-0.454,0.369-0.824,0.823-0.824h2.167h1.646h2.167   c0.454,0,0.824,0.37,0.824,0.824c0,0.454-0.37,0.823-0.824,0.823H8.932H7.286z\"/>" +
	"<path fill=\"#666766\" d=\"M5.118,8.731h2.273h1.436h2.272c0.396,0,0.718-0.323,0.718-0.717c0-0.395-0.322-0.718-0.718-0.718H8.827   H7.391H5.118C4.723,7.295,4.4,7.619,4.4,8.014C4.4,8.408,4.723,8.731,5.118,8.731L5.118,8.731z M5.118,8.731\"/>" +
	"</g>" +
"</svg></span> </button>" +
          "<button title=\"Zoom Out\" data-option=\"0.1\" data-method=\"zoom\" class=\"btn cusIconImg cusbtn\" type=\"button\">" +
            "<span title=\"\" data-toggle=\"tooltip\" class=\"docs-tooltip\" data-original-title=\"$().cropper(&quot;zoom&quot;, 0.1)\">" +
             "<svg version=\"1.1\" id=\"Layer_1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" x=\"0px\" y=\"0px\" width=\"18px\" height=\"18px\" viewBox=\"0 0 20 20.637\" enable-background=\"new 0 0 20 20.637\" xml:space=\"preserve\"><g><g>\"+" +
		"<path fill=\"#666766\" d=\"M19.186,20.608c-0.213,0-0.418-0.088-0.576-0.246l-5.834-5.834c-1.385,1.012-3.021,1.546-4.739,1.546    C3.605,16.074,0,12.469,0,8.037S3.605,0,8.037,0c4.431,0,8.037,3.605,8.037,8.037c0,1.986-0.736,3.894-2.077,5.381l5.768,5.778    c0.314,0.329,0.314,0.851,0,1.166C19.624,20.515,19.407,20.608,19.186,20.608z M8.037,1.647c-3.524,0-6.391,2.867-6.391,6.39    s2.867,6.391,6.391,6.391s6.39-2.867,6.39-6.391S11.561,1.647,8.037,1.647z\"/>" +
		"<path fill=\"#666766\" d=\"M8.037,15.969c1.783,0,3.421-0.586,4.75-1.579l5.897,5.897c0.144,0.144,0.323,0.216,0.502,0.216    c0.18,0,0.371-0.072,0.503-0.216c0.275-0.275,0.275-0.729,0-1.017l-5.838-5.85c1.304-1.412,2.117-3.302,2.117-5.384    c0-4.366-3.553-7.931-7.931-7.931S0.105,3.671,0.105,8.037S3.659,15.969,8.037,15.969L8.037,15.969z M8.037,1.542    c3.577,0,6.496,2.907,6.496,6.496s-2.906,6.496-6.496,6.496c-3.577,0-6.496-2.907-6.496-6.496S4.448,1.542,8.037,1.542    L8.037,1.542z M8.037,1.542\"/>" +
		"<rect fill=\"none\" width=\"19.997\" height=\"20.637\"/>	</g>" +
	"<path fill=\"#666766\" d=\"M8.109,11.827c-0.454,0-0.823-0.369-0.823-0.823V8.836H5.118c-0.454,0-0.823-0.369-0.823-0.823   c0-0.454,0.369-0.824,0.823-0.824h2.167V5.022c0-0.454,0.369-0.823,0.823-0.823s0.823,0.369,0.823,0.823V7.19h2.167   c0.454,0,0.824,0.37,0.824,0.824c0,0.454-0.37,0.823-0.824,0.823H8.932v2.167C8.932,11.458,8.563,11.827,8.109,11.827z\"/>" +
	"<path fill=\"#666766\" d=\"M5.118,8.731h2.273v2.273c0,0.395,0.323,0.718,0.718,0.718c0.395,0,0.718-0.323,0.718-0.718V8.731h2.272   c0.396,0,0.718-0.323,0.718-0.717c0-0.395-0.322-0.718-0.718-0.718H8.827V5.022c0-0.395-0.323-0.718-0.718-0.718   c-0.395,0-0.718,0.323-0.718,0.718v2.273H5.118C4.723,7.295,4.4,7.619,4.4,8.014C4.4,8.408,4.723,8.731,5.118,8.731L5.118,8.731z    M5.118,8.731\"/>" +
"</g>" +
"</svg></span> </button>" +

         "<button title=\"Move\" data-option=\"move\" data-method=\"setDragMode\" class=\"btn cusIconImg\" type=\"button\">" +
        " <span title=\"\" data-toggle=\"tooltip\" class=\"docs-tooltip\" data-original-title=\"$().cropper(&quot;setDragMode&quot;, &quot;move&quot;)\">" +
        " <svg version=\"1.1\" id=\"Layer_1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" x=\"0px\" y=\"0px\"width=\"18px\" height=\"18px\" viewBox=\"0 0 12 12\" enable-background=\"new 0 0 12 12\" xml:space=\"preserve\"><g>" +
	"<path fill=\"#666766\" d=\"M11.969,5.848l-0.086-0.129l-1.602-1.602c-0.152-0.156-0.41-0.156-0.563,0c-0.156,0.156-0.156,0.41,0,0.567l0.914,0.918H6.398V1.367l0.918,0.914c0.156,0.157,0.41,0.157,0.566,0c0.156-0.156,0.156-0.41,0-0.562L6.277,0.113L6.152,0.031L6,0" +
    "L5.848,0.031L5.723,0.113L4.117,1.719c-0.156,0.152-0.156,0.406,0,0.562c0.156,0.157,0.41,0.157,0.567,0l0.918-0.914v4.235H1.367" +
    "l0.914-0.918c0.157-0.157,0.157-0.411,0-0.567c-0.156-0.156-0.41-0.156-0.562,0L0.113,5.723L0.031,5.848L0,6l0.031,0.152" +
    "l0.082,0.125l1.606,1.605c0.152,0.156,0.406,0.156,0.562,0c0.157-0.155,0.157-0.409,0-0.566L1.367,6.398h4.235v4.234L4.684,9.719" +
    "c-0.157-0.156-0.411-0.156-0.567,0c-0.156,0.156-0.156,0.41,0,0.563l1.602,1.602l0.129,0.086L6,12l0.152-0.031l0.129-0.086" +
    "l1.602-1.602c0.156-0.152,0.156-0.41,0-0.563c-0.156-0.156-0.41-0.156-0.566,0l-0.918,0.914V6.398h4.235L9.719,7.316" +
    "c-0.156,0.157-0.156,0.411,0,0.566c0.156,0.156,0.41,0.156,0.563,0l1.602-1.602l0.086-0.129L12,6L11.969,5.848z M11.969,5.848\"/></g></svg>" +
"</span></button>" +


        "<button type=\"button\" class=\"btn cusIconImg\" data-method=\"rotate\" data-option=\"90\" title=\"Rotate Right\">" +
                            "<span class=\"docs-tooltip\" data-toggle=\"tooltip\" title=\"$().cropper(&quot;rotate&quot;, 90)\">" +
                               "<svg version=\"1.1\" id=\"Layer_1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" x=\"0px\" y=\"0px\"" +
    "width=\"18px\" height=\"18px\" viewBox=\"0 0 18.728 20\" enable-background=\"new 0 0 18.728 20\" xml:space=\"preserve\">" +
    "<g><g><g>" +
    "<path fill=\"#666766\" d=\"M9.345,20c0.41,0,0.824-0.027,1.233-0.081c2.479-0.324,4.685-1.595,6.209-3.578" +
    "c1.484-1.932,2.152-4.329,1.881-6.749c-0.045-0.393-0.375-0.689-0.77-0.689c-0.029,0-0.059,0.001-0.088,0.005" +
    "c-0.207,0.023-0.391,0.125-0.521,0.287s-0.188,0.365-0.165,0.571c0.227,2.019-0.33,4.018-1.568,5.629" +
    "c-1.271,1.654-3.111,2.714-5.18,2.985c-0.341,0.045-0.688,0.067-1.029,0.067c-1.726,0-3.365-0.56-4.744-1.619" +
    "c-0.475-0.365-0.906-0.782-1.283-1.24c-0.923-1.122-1.511-2.483-1.701-3.939C1.349,9.581,1.9,7.53,3.17,5.876" +
    "c1.272-1.654,3.111-2.715,5.18-2.985c0.341-0.044,0.687-0.067,1.027-0.067c1.62,0,3.177,0.499,4.505,1.443l-0.718,0.718" +
    "c-0.105,0.105-0.146,0.212-0.114,0.301c0.033,0.089,0.133,0.145,0.281,0.158l2.55,0.23c0.017,0.001,0.032,0.002,0.049,0.002" +
    "c0.134,0,0.253-0.051,0.337-0.143c0.086-0.094,0.127-0.222,0.113-0.359l-0.232-2.55c-0.018-0.182-0.1-0.291-0.221-0.291" +
    "c-0.074,0-0.157,0.043-0.238,0.125l-0.696,0.697c-1.637-1.232-3.577-1.884-5.611-1.884c-0.409,0-0.824,0.027-1.233,0.081" +
    "C5.669,1.677,3.464,2.948,1.94,4.931c-1.523,1.983-2.184,4.44-1.859,6.92c0.229,1.746,0.934,3.378,2.039,4.722" +
    "c0.452,0.549,0.97,1.049,1.539,1.486C5.311,19.329,7.278,20,9.345,20L9.345,20z\"/>" +
    "</g><g>" +
    "<path fill=\"#666766\" d=\"M1.96,4.946c-1.52,1.978-2.179,4.429-1.855,6.902c0.23,1.756,0.936,3.375,2.034,4.709" +
    "c0.448,0.545,0.962,1.042,1.535,1.482c1.978,1.52,4.429,2.179,6.9,1.855c2.474-0.324,4.673-1.591,6.193-3.568" +
    "c1.48-1.927,2.146-4.317,1.875-6.73c-0.046-0.412-0.418-0.709-0.83-0.663c-0.412,0.046-0.709,0.418-0.662,0.83" +
    "c0.227,2.025-0.332,4.031-1.574,5.648c-1.275,1.659-3.121,2.723-5.196,2.994c-2.076,0.272-4.132-0.281-5.792-1.557" +
    "c-0.481-0.37-0.912-0.787-1.288-1.244c-0.922-1.12-1.514-2.478-1.707-3.952C1.323,9.577,1.875,7.521,3.151,5.861" +
    "c1.275-1.66,3.121-2.724,5.197-2.995c1.985-0.26,3.954,0.237,5.574,1.398l-0.737,0.739c-0.203,0.203-0.136,0.391,0.15,0.417" +
    "l2.55,0.23c0.286,0.025,0.499-0.188,0.473-0.473l-0.233-2.55c-0.026-0.286-0.213-0.354-0.416-0.15l-0.711,0.712" +
    "c-1.969-1.488-4.395-2.132-6.844-1.812C5.679,1.701,3.48,2.968,1.96,4.946L1.96,4.946z M1.96,4.946\"/>" +
    "</g></g></g><g>" +
    "<polygon fill=\"#666766\" points=\"16.479,6.267 15.741,3.51 15.002,0.753 12.985,2.771 10.966,4.79 13.723,5.528 	\"/>" +
    "<path fill=\"#666766\" d=\"M16.285,6.995l-5.514-1.478c-0.26-0.07-0.463-0.273-0.533-0.533c-0.07-0.26,0.005-0.538,0.195-0.728" +
    "l4.036-4.036c0.19-0.19,0.468-0.265,0.729-0.195c0.26,0.07,0.463,0.272,0.532,0.533l1.478,5.514" +
    "c0.069,0.26-0.005,0.537-0.195,0.728c-0.143,0.143-0.336,0.221-0.533,0.221C16.415,7.021,16.349,7.012,16.285,6.995z M12.422,4.4" +
    "l2.992,0.802l-0.803-2.993L12.422,4.4z\"/></g></svg>" +
                            "</span>" +
                        "</button>" +
                         "<button onclick='Ok(this)' title=\"Crop\" data-method=\"getCroppedCanvas\" class=\"btn cusIconImg cbtn-info-ok\" type=\"button\">" +
        "<span title=\"\" data-toggle=\"tooltip\" class=\"docs-tooltip\" data-original-title=\"$().cropper(&quot;crop&quot;)\">" +

"<svg version=\"1.1\" id=\"Layer_1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" x=\"0px\" y=\"0px\" width=\"18px\" height=\"18px\" viewBox=\"0 0 27 20.168\" enable-background=\"new 0 0 27 20.168\" xml:space=\"preserve\">" +
"<polyline fill=\"none\" stroke=\"#FFFFFF\" stroke-width=\"7\" stroke-linecap=\"round\" stroke-linejoin=\"round\" stroke-miterlimit=\"10\" points=\" 3.5,9.837 10.331,16.668 23.5,3.5 \"/>" +
"</svg>" +
"</span></button>" +
                         "<div onclick='DeleteUpload(this)' title=\"Delete\" class=\"btn cusIconImg cusbtn cusbtn-del\">" +
    "<svg version=\"1.1\" id=\"Layer_1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" x=\"0px\" y=\"0px\" width=\"18px\" height=\"18px\" viewBox=\"0 0 15.809 20\" enable-background=\"new 0 0 15.809 20\" xml:space=\"preserve\"><g>" +
    "<path fill=\"#FFFFFF\" d=\"M14.444,2.354h-2.67V0.62c0-0.35-0.281-0.62-0.631-0.62c-0.034,0-0.057,0.012-0.067,0.022" +
    "C11.064,0.012,11.042,0,11.03,0H4.664c-0.349,0-0.62,0.27-0.62,0.62v1.735H1.364C0.597,2.354,0,2.952,0,3.718V5.95h1.183v12.698" +
    "C1.183,19.414,1.769,20,2.535,20h10.738c0.767,0,1.362-0.586,1.362-1.353V5.95h1.173V3.718C15.809,2.952,15.211,2.354,14.444,2.354" +
    "L14.444,2.354z M5.284,1.239h5.239v1.115H5.284V1.239z M13.386,18.647c0,0.079-0.034,0.113-0.112,0.113H2.535" +
    "c-0.079,0-0.113-0.034-0.113-0.113V5.95h10.963V18.647z M14.558,4.71H1.251V3.718c0-0.079,0.033-0.125,0.113-0.125h13.081" +
    "c0.079,0,0.113,0.046,0.113,0.125V4.71z M14.558,4.71\"/>" +
    "<rect x=\"10.175\" y=\"7.402\" fill=\"#FFFFFF\" width=\"1.25\" height=\"10.254\"/>" +
    "<rect x=\"7.29\" y=\"7.402\" fill=\"#FFFFFF\" width=\"1.251\" height=\"10.254\"/>" +
"<rect x=\"4.406\" y=\"7.402\" fill=\"#FFFFFF\" width=\"1.25\" height=\"10.254\"/></g></svg></div>" +
                        "</div>" +
        "</div>" +
         "<div class=\"containerUpload cusdefault\"><input type=\"radio\" checked=\"checked\" name='groupimg' class=\"reset-data cusradio\" data-defalut-enabled=\"false\"/>Ch?n ?nh này làm ??i di?n</div>" +
        " <div class=\"dz-image containerUpload\"  id=\"imgmain\"><img id=\"cusimg\"   class=\"ghost cussimg\" rel=\"0\" data-dz-thumbnail /></div>\n  " +
        "<div class=\"dz-filename cusfilename\"><input type=\"text\" placeholder=\"Tiêu ?? hình ?nh\" class=\"col-xs-12 form-control valid cusinput\" id=\"txtImage\" value=\"\" name=\"\"> </div>" +
        "<div class=\"input-group input-group-sm hidden\"> " +
        "<input type=\"text\" class=\"form-control\" id=\"dataX\" placeholder=\"x\">" +
        "<input type=\"text\" class=\"form-control\" id=\"dataY\" placeholder=\"y\">" +
        "<input id=\"dataHeight\" class=\"form-control\" type=\"text\" placeholder=\"height\">" +
        "<input id=\"dataRotate\" class=\"form-control\" type=\"text\" placeholder=\"rotate\">" +
        "<input type=\"text\" class=\"form-control\" id=\"dataWidth\" placeholder=\"width\">" +


         " <div class=\"form-control\"  id=\"dataimg\"></div>" +

        "</div>" +
        " <div class=\"dz-progress\"><span class=\"dz-upload\" data-dz-uploadprogress></span></div>\n" +
        "<div class=\"dz-error-message\"><span data-dz-errormessage></span></div>\n  " +
        "<div id=\"cussucess\"  class=\"dz-success-mark\">\n <svg width=\"54px\" height=\"54px\" viewBox=\"0 0 54 54\" version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" xmlns:sketch=\"http://www.bohemiancoding.com/sketch/ns\">\n      <title>Check</title>\n      <defs></defs>\n      <g id=\"Page-1\" stroke=\"none\" stroke-width=\"1\" fill=\"none\" fill-rule=\"evenodd\" sketch:type=\"MSPage\">\n   " +
        "<path d=\"M23.5,31.8431458 L17.5852419,25.9283877 C16.0248253,24.3679711 13.4910294,24.366835 11.9289322,25.9289322 C10.3700136,27.4878508 10.3665912,30.0234455 11.9283877,31.5852419 L20.4147581,40.0716123 C20.5133999,40.1702541 20.6159315,40.2626649 20.7218615,40.3488435 C22.2835669,41.8725651 24.794234,41.8626202 26.3461564,40.3106978 L43.3106978,23.3461564 C44.8771021,21.7797521 44.8758057,19.2483887 43.3137085,17.6862915 C41.7547899,16.1273729 39.2176035,16.1255422 37.6538436,17.6893022 L23.5,31.8431458 Z M27,53 C41.3594035,53 53,41.3594035 53,27 C53,12.6405965 41.3594035,1 27,1 C12.6405965,1 1,12.6405965 1,27 C1,41.3594035 12.6405965,53 27,53 Z\" id=\"Oval-2\" stroke-opacity=\"0.198794158\" stroke=\"#747474\" fill-opacity=\"0.816519475\" fill=\"#FFFFFF\" sketch:type=\"MSShapeGroup\"></path>\n      </g>\n    </svg>\n  " +
        "</div>\n  <div class=\"dz-error-mark\">\n    <svg width=\"54px\" height=\"54px\" viewBox=\"0 0 54 54\" version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" xmlns:sketch=\"http://www.bohemiancoding.com/sketch/ns\">\n      <title>Error</title>\n      <defs></defs>\n      <g id=\"Page-1\" stroke=\"none\" stroke-width=\"1\" fill=\"none\" fill-rule=\"evenodd\" sketch:type=\"MSPage\">\n        <g id=\"Check-+-Oval-2\" sketch:type=\"MSLayerGroup\" stroke=\"#747474\" stroke-opacity=\"0.198794158\" fill=\"#FFFFFF\" fill-opacity=\"0.816519475\">\n          <path d=\"M32.6568542,29 L38.3106978,23.3461564 C39.8771021,21.7797521 39.8758057,19.2483887 38.3137085,17.6862915 C36.7547899,16.1273729 34.2176035,16.1255422 32.6538436,17.6893022 L27,23.3431458 L21.3461564,17.6893022 C19.7823965,16.1255422 17.2452101,16.1273729 15.6862915,17.6862915 C14.1241943,19.2483887 14.1228979,21.7797521 15.6893022,23.3461564 L21.3431458,29 L15.6893022,34.6538436 C14.1228979,36.2202479 14.1241943,38.7516113 15.6862915,40.3137085 C17.2452101,41.8726271 19.7823965,41.8744578 21.3461564,40.3106978 L27,34.6568542 L32.6538436,40.3106978 C34.2176035,41.8744578 36.7547899,41.8726271 38.3137085,40.3137085 C39.8758057,38.7516113 39.8771021,36.2202479 38.3106978,34.6538436 L32.6568542,29 Z M27,53 C41.3594035,53 53,41.3594035 53,27 C53,12.6405965 41.3594035,1 27,1 C12.6405965,1 1,12.6405965 1,27 C1,41.3594035 12.6405965,53 27,53 Z\" id=\"Oval-2\" sketch:type=\"MSShapeGroup\"></path>\n        </g>\n      </g>\n    </svg>\n  </div>\n</div>" +
        "<div class=\"dz-details\">\n    <div class=\"dz-size\"><span data-dz-size></span></div>\n\n </div>\n ";

    //"<input type=\"text\" class=\"form-control\" id=\"dataimg\" placeholder=\"tu\">" +
    var Dropzone,
        Emitter,
        camelize,
        contentLoaded,
        detectVerticalSquash,
        drawImageIOSFix,
        noop,
        without,
        custools,
        __slice = [].slice,
        __hasProp = {}.hasOwnProperty,
        __extends = function (child, parent) {
            for (var key in parent) {
                if (__hasProp.call(parent, key)) child[key] = parent[key];
            }

            function ctor() { this.constructor = child; }

            ctor.prototype = parent.prototype;
            child.prototype = new ctor();
            child.__super__ = parent.prototype;
            return child;
        };


    noop = function () { };

    Emitter = (function () {
        function Emitter() { }

        Emitter.prototype.addEventListener = Emitter.prototype.on;

        Emitter.prototype.on = function (event, fn) {
            this._callbacks = this._callbacks || {};
            if (!this._callbacks[event]) {
                this._callbacks[event] = [];
            }
            this._callbacks[event].push(fn);
            return this;
        };

        Emitter.prototype.emit = function () {
            var args, callback, callbacks, event, _i, _len;
            event = arguments[0], args = 2 <= arguments.length ? __slice.call(arguments, 1) : [];
            this._callbacks = this._callbacks || {};
            callbacks = this._callbacks[event];
            if (callbacks) {
                for (_i = 0, _len = callbacks.length; _i < _len; _i++) {
                    callback = callbacks[_i];
                    callback.apply(this, args);
                }
            }
            return this;
        };

        Emitter.prototype.removeListener = Emitter.prototype.off;

        Emitter.prototype.removeAllListeners = Emitter.prototype.off;

        Emitter.prototype.removeEventListener = Emitter.prototype.off;

        Emitter.prototype.off = function (event, fn) {
            var callback, callbacks, i, _i, _len;
            if (!this._callbacks || arguments.length === 0) {
                this._callbacks = {};
                return this;
            }
            callbacks = this._callbacks[event];
            if (!callbacks) {
                return this;
            }
            if (arguments.length === 1) {
                delete this._callbacks[event];
                return this;
            }
            for (i = _i = 0, _len = callbacks.length; _i < _len; i = ++_i) {
                callback = callbacks[i];
                if (callback === fn) {
                    callbacks.splice(i, 1);
                    break;
                }
            }
            return this;
        };

        return Emitter;

    })();

    Dropzone = (function (_super) {
        var extend, resolveOption;

        __extends(Dropzone, _super);

        Dropzone.prototype.Emitter = Emitter;


        /*
    This is a list of all available events you can register on a dropzone object.
    
    You can register an event handler like this:
    
        dropzone.on("dragEnter", function() { });
     */

        Dropzone.prototype.events = ["drop", "dragstart", "dragend", "dragenter", "dragover", "dragleave", "addedfile", "addedfiles", "removedfile", "thumbnail", "error", "errormultiple", "processing", "processingmultiple", "uploadprogress", "totaluploadprogress", "sending", "sendingmultiple", "success", "successmultiple", "canceled", "canceledmultiple", "complete", "completemultiple", "reset", "maxfilesexceeded", "maxfilesreached", "queuecomplete"];

        Dropzone.prototype.defaultOptions = {
            //url: null,
            //method: "post",
            //withCredentials: false,
            //parallelUploads: 2,
            //uploadMultiple: false,
            //maxFilesize: 2,
            //paramName: "file",
            //createImageThumbnails: true,
            //maxThumbnailFilesize: 2,
            ////thumbnailWidth: 288,
            ////thumbnailHeight: 180,
            //filesizeBase: 1000,
            //maxFiles: null,
            //params: {},
            //clickable: true,
            //ignoreHiddenFiles: true,
            //acceptedFiles: null,
            //acceptedMimeTypes: null,
            //autoProcessQueue: true,
            //autoQueue: true,
            //addRemoveLinks: false,
            //previewsContainer: null,
            //capture: null,
            //dictDefaultMessage: "Ch?n file ?? t?i lên",
            //dictFallbackMessage: "Your browser does not support drag'n'drop file uploads.",
            //dictFallbackText: "Please use the fallback form below to upload your files like in the olden days.",
            //dictFileTooBig: "File is too big ({{filesize}}MiB). Max filesize: {{maxFilesize}}MiB.",
            //dictFilesmall: "Upload Hình Có File L?n h?n",
            //dictInvalidFileType: "You can't upload files of this type.",
            //dictResponseError: "Server responded with {{statusCode}} code.",
            //dictCancelUpload: "Cancel upload",
            //dictCancelUploadConfirmation: "Are you sure you want to cancel this upload?",
            //dictRemoveFile: "Remove file",
            //dictRemoveFileConfirmation: null,
            //dictMaxFilesExceeded: "You can not upload any more files.",

            url: null,
            method: "post",
            withCredentials: false,
            parallelUploads: 1,
            uploadMultiple: true,
            maxFilesize: 16,

            paramName: "file",
            createImageThumbnails: true,
            maxThumbnailFilesize: 1,
            //thumbnailWidth: 615,
            thumbnailHeight: 562,

            filesizeBase: 1024,
            maxFiles: 20,
            params: {},
            clickable: true,

            ignoreHiddenFiles: true,
            acceptedFiles: ".png,.gif,.jpg,.jpeg",
            acceptedMimeTypes: null,
            autoProcessQueue: true,
            autoQueue: true,
            addRemoveLinks: false,
            previewsContainer: null,
            hiddenInputContainer: "body",
            capture: null,
            renameFilename: null,
            dictDefaultMessage: "Drop files here to upload",
            dictFallbackMessage: "Your browser does not support drag'n'drop file uploads.",
            dictFallbackText: "Please use the fallback form below to upload your files like in the olden days.",
            dictFileTooBig: "File is too big ({{filesize}}MiB). Max filesize: {{maxFilesize}}MiB.",
            dictInvalidFileType: "You can't upload files of this type.",
            dictResponseError: "Server responded with {{statusCode}} code.",
            dictCancelUpload: "Cancel upload",
            dictCancelUploadConfirmation: "Are you sure you want to cancel this upload?",
            dictRemoveFile: "Remove file",
            dictRemoveFileConfirmation: null,
            dictMaxFilesExceeded: "You can not upload any more files.",
            accept: function (file, done) {
                return done();
            },
            init: function () {
                return noop;
            },
            forceFallback: false,
            fallback: function () {
                var child, messageElement, span, _i, _len, _ref;
                this.element.className = "" + this.element.className + " dz-browser-not-supported";
                _ref = this.element.getElementsByTagName("div");
                for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                    child = _ref[_i];
                    if (/(^| )dz-message($| )/.test(child.className)) {
                        messageElement = child;
                        child.className = "dz-message";
                        continue;
                    }
                }
                if (!messageElement) {
                    messageElement = Dropzone.createElement("<div class=\"dz-message\"><span></span></div>");
                    this.element.appendChild(messageElement);
                }
                span = messageElement.getElementsByTagName("span")[0];
                if (span) {
                    if (span.textContent != null) {
                        span.textContent = this.options.dictFallbackMessage;
                    } else if (span.innerText != null) {
                        span.innerText = this.options.dictFallbackMessage;
                    }
                }
                return this.element.appendChild(this.getFallbackForm());
            },
            resize: function (file) {

                var info, srcRatio, trgRatio;
                info = {
                    srcX: 0,
                    srcY: 0,
                    srcWidth: file.width,
                    srcHeight: file.height
                };
                srcRatio = file.width / file.height;
                info.optWidth = this.options.thumbnailWidth;
                info.optHeight = this.options.thumbnailHeight;
                if ((info.optWidth == null) && (info.optHeight == null)) {
                    info.optWidth = info.srcWidth;
                    info.optHeight = info.srcHeight;
                } else if (info.optWidth == null) {
                    info.optWidth = srcRatio * info.optHeight;
                } else if (info.optHeight == null) {
                    info.optHeight = (1 / srcRatio) * info.optWidth;
                }
                trgRatio = info.optWidth / info.optHeight;
                if (file.height < info.optHeight || file.width < info.optWidth) {
                    info.trgHeight = info.srcHeight;
                    info.trgWidth = info.srcWidth;
                } else {
                    if (srcRatio > trgRatio) {
                        info.srcHeight = file.height;
                        info.srcWidth = info.srcHeight * trgRatio;
                    } else {
                        info.srcWidth = file.width;
                        info.srcHeight = info.srcWidth / trgRatio;
                    }
                }
                info.srcX = (file.width - info.srcWidth) / 2;
                info.srcY = (file.height - info.srcHeight) / 2;
                return info;
            },
            drop: function (e) {
                return this.element.classList.remove("dz-drag-hover");
            },
            dragstart: noop,
            dragend: function (e) {
                return this.element.classList.remove("dz-drag-hover");
            },
            dragenter: function (e) {
                return this.element.classList.add("dz-drag-hover");
            },
            dragover: function (e) {
                return this.element.classList.add("dz-drag-hover");
            },
            dragleave: function (e) {
                return this.element.classList.remove("dz-drag-hover");
            },
            paste: noop,
            reset: function () {
                return this.element.classList.remove("dz-started");
            },
            addedfile: function (file) {
                var node, removeFileEvent, removeLink, _i, _j, _k, _len, _len1, _len2, _ref, _ref1, _ref2, _results;
                if (this.element === this.previewsContainer) {
                    this.element.classList.add("dz-started");
                }
                if (this.previewsContainer) {
                    file.previewElement = Dropzone.createElement(this.options.previewTemplate.trim());
                    file.previewTemplate = file.previewElement;
                    this.previewsContainer.appendChild(file.previewElement);
                    _ref = file.previewElement.querySelectorAll("[data-dz-name]");
                    for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                        node = _ref[_i];
                        node.textContent = file.name;
                    }
                    _ref1 = file.previewElement.querySelectorAll("[data-dz-size]");
                    for (_j = 0, _len1 = _ref1.length; _j < _len1; _j++) {
                        node = _ref1[_j];
                        node.innerHTML = this.filesize(file.size);
                    }
                    if (this.options.addRemoveLinks) {
                        file._removeLink = Dropzone.createElement("<a class=\"dz-remove\" href=\"javascript:undefined;\" data-dz-remove>" + this.options.dictRemoveFile + "</a>");
                        file.previewElement.appendChild(file._removeLink);
                    }
                    removeFileEvent = (function (_this) {
                        return function (e) {
                            e.preventDefault();
                            e.stopPropagation();
                            if (file.status === Dropzone.UPLOADING) {
                                return Dropzone.confirm(_this.options.dictCancelUploadConfirmation, function () {
                                    return _this.removeFile(file);
                                });
                            } else {
                                if (_this.options.dictRemoveFileConfirmation) {
                                    return Dropzone.confirm(_this.options.dictRemoveFileConfirmation, function () {
                                        return _this.removeFile(file);
                                    });
                                } else {
                                    return _this.removeFile(file);
                                }
                            }
                        };
                    })(this);
                    _ref2 = file.previewElement.querySelectorAll("[data-dz-remove]");
                    _results = [];
                    for (_k = 0, _len2 = _ref2.length; _k < _len2; _k++) {
                        removeLink = _ref2[_k];
                        _results.push(removeLink.addEventListener("click", removeFileEvent));
                    }
                    return _results;
                }
            },
            removedfile: function (file) {
                var _ref;
                if (file.previewElement) {
                    if ((_ref = file.previewElement) != null) {
                        _ref.parentNode.removeChild(file.previewElement);
                    }
                }
                return this._updateMaxFilesReachedClass();
            },
            thumbnail: function (file, dataUrl) {
                if (dataUrl.length > (3.5 * 1024 * 1024)) {
                    var _ref = file && file.previewElement;
                    if (_ref) {
                        _ref.parentNode.removeChild(file.previewElement);
                        if (toastr) toastr.error('Không th? t?i lên vì hình quá l?n (' + file.name + ')');
                    }

                    return false;
                }

                var thumbnailElement, _i, _len, _ref;
                if (file.previewElement) {
                    file.previewElement.classList.remove("dz-file-preview");
                    _ref = file.previewElement.querySelectorAll("[data-dz-thumbnail]");
                    for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                        thumbnailElement = _ref[_i];
                        thumbnailElement.alt = file.name;
                        thumbnailElement.src = dataUrl;
                    }

                    //Create cropper by code below
                    var $previewElement = $(file.previewElement);
                    var $editAction = $previewElement.find('.cbtn-edit');
                    $previewElement.find('.cropper-container img').css({ "transform": "none" });
                    $previewElement.eq(0).find('.tools').removeClass('hidden');
                    $previewElement.eq(0).find('.final').addClass('hidden');

                    $previewElement.eq(0).find('.cropper-move').addClass('cropper-face');
                    //hiden control drop
                    $previewElement.find('#imgmain img:first').addClass('cropper-hidden');
                    $previewElement.find('#imgmain img:first').next().removeClass('cropper-hidden');
                    //hiden control drop end
                    if (dropNew) dropNew($editAction);

                    return setTimeout(((function (_this) {
                        return function () {
                            return file.previewElement.classList.add("dz-image-preview");
                        };
                    })(this)), 1);
                }
            },
            error: function (file, message) {
                var node, _i, _len, _ref, _results;

                if (file.previewElement) {
                    file.previewElement.classList.add("dz-error");
                    if (typeof message !== "String" && message.error) {
                        message = message.error;
                    }
                    _ref = file.previewElement.querySelectorAll("[data-dz-errormessage]");
                    _results = [];
                    for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                        node = _ref[_i];
                        _results.push(node.textContent = "Không th? t?i lên vì hình quá nh? ho?c quá l?n");
                    }
                    return _results;
                }
            },
            errormultiple: noop,
            processing: function (file) {
                if (file.previewElement) {
                    file.previewElement.classList.add("dz-processing");
                    if (file._removeLink) {
                        return file._removeLink.textContent = this.options.dictCancelUpload;
                    }
                }
            },
            processingmultiple: noop,
            uploadprogress: function (file, progress, bytesSent) {
                var node, _i, _len, _ref, _results;

                if (file.previewElement) {
                    _ref = file.previewElement.querySelectorAll("[data-dz-uploadprogress]");
                    _results = [];
                    for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                        node = _ref[_i];
                        if (node.nodeName === 'PROGRESS') {
                            _results.push(node.value = progress);
                        } else {
                            var timerfindcedit1 = setInterval(function () {
                                if (progress < 100) {
                                    $('.btnNextNewUi').addClass('disabled');
                                    $('.btnNextNewUi').text('?ang x? lý...');

                                } else {
                                    _results.push(node.style.width = "" + progress + "%");
                                    $('.btnNextNewUi').removeClass('disabled');
                                    $('.btnNextNewUi').text('B??c ti?p theo');

                                }
                                clearInterval(timerfindcedit1);
                            }, 100);
                        }
                    }

                    return _results;
                }
            },

            totaluploadprogress: noop,
            sending: noop,
            sendingmultiple: noop,
            success: function (file) {
                if (file.previewElement) {
                    return file.previewElement.classList.add("dz-success");
                }
            },
            successmultiple: noop,
            canceled: function (file) {
                return this.emit("error", file, "Upload canceled.");
            },
            canceledmultiple: noop,
            complete: function (file) {
                if (file && file.status === "error") {
                    var _ref = file.previewElement;
                    if (_ref) {
                        _ref.parentNode.removeChild(file.previewElement);
                        if (toastr) toastr.error('Không th? t?i lên vì hình quá nh? ho?c quá l?n (' + file.name + ')');
                    }

                    return false;
                }

                if (file._removeLink) {
                    file._removeLink.textContent = this.options.dictRemoveFile;
                }

                if (file.previewElement) {
                    if (file.height < 350) {
                        var _ref = file.previewElement;
                        if (_ref) {
                            _ref.parentNode.removeChild(file.previewElement);
                            if (toastr) toastr.error('Không th? Upload vì file quá nh? ho?c quá l?n (' + file.name + ')');
                        }
                        return false;
                    }

                    $('.btnNextNewUi').removeClass('disabled');
                    return file.previewElement.classList.add("dz-complete");
                }
            },
            completemultiple: function () {
                $('.dropzone').each(function () {
                    $('input[type=radio]', this).get(0).checked = true;
                });
            },
            maxfilesexceeded: noop,
            maxfilesreached: noop,
            queuecomplete: noop,
            addedfiles: noop,
            previewTemplate: content
        };

        extend = function () {
            var key, object, objects, target, val, _i, _len;
            target = arguments[0], objects = 2 <= arguments.length ? __slice.call(arguments, 1) : [];
            for (_i = 0, _len = objects.length; _i < _len; _i++) {
                object = objects[_i];
                for (key in object) {
                    val = object[key];
                    target[key] = val;
                }
            }
            return target;
        };

        function Dropzone(element, options) {
            var elementOptions, fallback, _ref;
            this.element = element;
            this.version = Dropzone.version;
            this.defaultOptions.previewTemplate = this.defaultOptions.previewTemplate.replace(/\n*/g, "");
            this.clickableElements = [];
            this.listeners = [];
            this.files = [];
            if (typeof this.element === "string") {
                this.element = document.querySelector(this.element);
            }
            if (!(this.element && (this.element.nodeType != null))) {
                throw new Error("Invalid dropzone element.");
            }
            if (this.element.dropzone) {
                throw new Error("Dropzone already attached.");
            }
            Dropzone.instances.push(this);
            this.element.dropzone = this;
            elementOptions = (_ref = Dropzone.optionsForElement(this.element)) != null ? _ref : {};
            this.options = extend({}, this.defaultOptions, elementOptions, options != null ? options : {});
            if (this.options.forceFallback || !Dropzone.isBrowserSupported()) {
                return this.options.fallback.call(this);
            }
            if (this.options.url == null) {
                this.options.url = this.element.getAttribute("action");
            }
            if (!this.options.url) {
                throw new Error("No URL provided.");
            }
            if (this.options.acceptedFiles && this.options.acceptedMimeTypes) {
                throw new Error("You can't provide both 'acceptedFiles' and 'acceptedMimeTypes'. 'acceptedMimeTypes' is deprecated.");
            }
            if (this.options.acceptedMimeTypes) {
                this.options.acceptedFiles = this.options.acceptedMimeTypes;
                delete this.options.acceptedMimeTypes;
            }
            this.options.method = this.options.method.toUpperCase();
            if ((fallback = this.getExistingFallback()) && fallback.parentNode) {
                fallback.parentNode.removeChild(fallback);
            }
            if (this.options.previewsContainer !== false) {
                if (this.options.previewsContainer) {
                    this.previewsContainer = Dropzone.getElement(this.options.previewsContainer, "previewsContainer");
                } else {
                    this.previewsContainer = this.element;
                }
            }
            if (this.options.clickable) {
                if (this.options.clickable === true) {
                    this.clickableElements = [this.element];
                } else {
                    this.clickableElements = Dropzone.getElements(this.options.clickable, "clickable");
                }
            }
            this.init();
        }

        Dropzone.prototype.getAcceptedFiles = function () {
            var file, _i, _len, _ref, _results;
            _ref = this.files;
            _results = [];
            for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                file = _ref[_i];
                if (file.accepted) {
                    _results.push(file);
                }
            }
            return _results;
        };

        Dropzone.prototype.getRejectedFiles = function () {
            var file, _i, _len, _ref, _results;
            _ref = this.files;
            _results = [];
            for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                file = _ref[_i];
                if (!file.accepted) {
                    _results.push(file);
                }
            }
            return _results;
        };

        Dropzone.prototype.getFilesWithStatus = function (status) {
            var file, _i, _len, _ref, _results;
            _ref = this.files;
            _results = [];
            for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                file = _ref[_i];
                if (file.status === status) {
                    _results.push(file);
                }
            }
            return _results;
        };

        Dropzone.prototype.getQueuedFiles = function () {
            return this.getFilesWithStatus(Dropzone.QUEUED);
        };

        Dropzone.prototype.getUploadingFiles = function () {
            return this.getFilesWithStatus(Dropzone.UPLOADING);
        };

        Dropzone.prototype.getAddedFiles = function () {
            return this.getFilesWithStatus(Dropzone.ADDED);
        };

        Dropzone.prototype.getActiveFiles = function () {
            var file, _i, _len, _ref, _results;
            _ref = this.files;
            _results = [];
            for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                file = _ref[_i];
                if (file.status === Dropzone.UPLOADING || file.status === Dropzone.QUEUED) {
                    _results.push(file);
                }
            }
            return _results;
        };

        Dropzone.prototype.init = function () {
            var eventName, noPropagation, setupHiddenFileInput, _i, _len, _ref, _ref1;
            if (this.element.tagName === "form") {
                this.element.setAttribute("enctype", "multipart/form-data");
            }
            if (this.element.classList.contains("dropzone") && !this.element.querySelector(".dz-message")) {
                this.element.appendChild(Dropzone.createElement("<div style=\"display: block;\" class=\"dz-default dz-message cusbuttonmes\"><span class=\"btn btn-action\">" + this.options.dictDefaultMessage + "</span></div>"));
            }
            if (this.clickableElements.length) {
                setupHiddenFileInput = (function (_this) {
                    return function () {
                        if (_this.hiddenFileInput) {
                            document.body.removeChild(_this.hiddenFileInput);
                        }
                        _this.hiddenFileInput = document.createElement("input");
                        _this.hiddenFileInput.setAttribute("type", "file");
                        if ((_this.options.maxFiles == null) || _this.options.maxFiles > 1) {
                            _this.hiddenFileInput.setAttribute("multiple", "multiple");
                        }
                        _this.hiddenFileInput.className = "dz-hidden-input";
                        if (_this.options.acceptedFiles != null) {
                            _this.hiddenFileInput.setAttribute("accept", _this.options.acceptedFiles);
                        }
                        if (_this.options.capture != null) {
                            _this.hiddenFileInput.setAttribute("capture", _this.options.capture);
                        }
                        _this.hiddenFileInput.style.visibility = "hidden";
                        _this.hiddenFileInput.style.position = "absolute";
                        _this.hiddenFileInput.style.top = "0";
                        _this.hiddenFileInput.style.left = "0";
                        _this.hiddenFileInput.style.height = "0";
                        _this.hiddenFileInput.style.width = "0";
                        document.body.appendChild(_this.hiddenFileInput);
                        return _this.hiddenFileInput.addEventListener("change", function () {
                            var file, files, _i, _len;
                            files = _this.hiddenFileInput.files;
                            if (files.length) {

                                for (_i = 0, _len = files.length; _i < _len; _i++) {
                                    file = files[_i];
                                    _this.addFile(file);
                                }


                            }
                            _this.emit("addedfiles", files);
                            return setupHiddenFileInput();
                        });
                    };
                })(this);
                setupHiddenFileInput();
            }
            this.URL = (_ref = window.URL) != null ? _ref : window.webkitURL;
            _ref1 = this.events;
            for (_i = 0, _len = _ref1.length; _i < _len; _i++) {
                eventName = _ref1[_i];
                this.on(eventName, this.options[eventName]);
            }
            this.on("uploadprogress", (function (_this) {
                return function () {
                    return _this.updateTotalUploadProgress();
                };
            })(this));
            this.on("removedfile", (function (_this) {
                return function () {
                    return _this.updateTotalUploadProgress();
                };
            })(this));
            this.on("canceled", (function (_this) {
                return function (file) {
                    return _this.emit("complete", file);
                };
            })(this));
            this.on("complete", (function (_this) {
                return function (file) {
                    if (_this.getAddedFiles().length === 0 && _this.getUploadingFiles().length === 0 && _this.getQueuedFiles().length === 0) {
                        return setTimeout((function () {
                            return _this.emit("queuecomplete");
                        }), 0);
                    }
                };
            })(this));
            noPropagation = function (e) {
                e.stopPropagation();
                if (e.preventDefault) {
                    return e.preventDefault();
                } else {
                    return e.returnValue = false;
                }
            };
            this.listeners = [
                {
                    element: this.element,
                    events: {
                        "dragstart": (function (_this) {
                            return function (e) {
                                return _this.emit("dragstart", e);
                            };
                        })(this),
                        "dragenter": (function (_this) {
                            return function (e) {
                                noPropagation(e);
                                return _this.emit("dragenter", e);
                            };
                        })(this),
                        "dragover": (function (_this) {
                            return function (e) {
                                var efct;
                                try {
                                    efct = e.dataTransfer.effectAllowed;
                                } catch (_error) {
                                }
                                e.dataTransfer.dropEffect = 'move' === efct || 'linkMove' === efct ? 'move' : 'copy';
                                noPropagation(e);
                                return _this.emit("dragover", e);
                            };
                        })(this),
                        "dragleave": (function (_this) {
                            return function (e) {
                                return _this.emit("dragleave", e);
                            };
                        })(this),
                        "drop": (function (_this) {
                            return function (e) {
                                noPropagation(e);
                                return _this.drop(e);
                            };
                        })(this),
                        "dragend": (function (_this) {
                            return function (e) {
                                return _this.emit("dragend", e);
                            };
                        })(this)
                    }
                }
            ];
            this.clickableElements.forEach((function (_this) {
                return function (clickableElement) {
                    return _this.listeners.push({
                        element: clickableElement,
                        events: {
                            "click": function (evt) {
                                if ((clickableElement !== _this.element) || (evt.target === _this.element || Dropzone.elementInside(evt.target, _this.element.querySelector(".dz-message")))) {
                                    _this.hiddenFileInput.click();
                                }
                                return true;
                            }
                        }
                    });
                };
            })(this));
            this.enable();
            return this.options.init.call(this);
        };

        Dropzone.prototype.destroy = function () {
            var _ref;
            this.disable();
            this.removeAllFiles(true);
            if ((_ref = this.hiddenFileInput) != null ? _ref.parentNode : void 0) {
                this.hiddenFileInput.parentNode.removeChild(this.hiddenFileInput);
                this.hiddenFileInput = null;
            }
            delete this.element.dropzone;
            return Dropzone.instances.splice(Dropzone.instances.indexOf(this), 1);
        };

        Dropzone.prototype.updateTotalUploadProgress = function () {
            var activeFiles, file, totalBytes, totalBytesSent, totalUploadProgress, _i, _len, _ref;
            totalBytesSent = 0;
            totalBytes = 0;
            activeFiles = this.getActiveFiles();
            if (activeFiles.length) {
                _ref = this.getActiveFiles();
                for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                    file = _ref[_i];
                    totalBytesSent += file.upload.bytesSent;
                    totalBytes += file.upload.total;
                }
                totalUploadProgress = 100 * totalBytesSent / totalBytes;
            } else {
                totalUploadProgress = 100;
            }
            return this.emit("totaluploadprogress", totalUploadProgress, totalBytes, totalBytesSent);
        };

        Dropzone.prototype._getParamName = function (n) {
            if (typeof this.options.paramName === "function") {
                return this.options.paramName(n);
            } else {
                return "" + this.options.paramName + (this.options.uploadMultiple ? "[" + n + "]" : "");
            }
        };

        Dropzone.prototype.getFallbackForm = function () {
            var existingFallback, fields, fieldsString, form;
            if (existingFallback = this.getExistingFallback()) {
                return existingFallback;
            }
            fieldsString = "<div class=\"dz-fallback\">";
            if (this.options.dictFallbackText) {
                fieldsString += "<p>" + this.options.dictFallbackText + "</p>";
            }
            fieldsString += "<input type=\"file\" name=\"" + (this._getParamName(0)) + "\" " + (this.options.uploadMultiple ? 'multiple="multiple"' : void 0) + " /><input type=\"submit\" value=\"Upload!\"></div>";
            fields = Dropzone.createElement(fieldsString);
            if (this.element.tagName !== "FORM") {
                form = Dropzone.createElement("<form action=\"" + this.options.url + "\" enctype=\"multipart/form-data\" method=\"" + this.options.method + "\"></form>");
                form.appendChild(fields);
            } else {
                this.element.setAttribute("enctype", "multipart/form-data");
                this.element.setAttribute("method", this.options.method);
            }
            return form != null ? form : fields;
        };

        Dropzone.prototype.getExistingFallback = function () {
            var fallback, getFallback, tagName, _i, _len, _ref;
            getFallback = function (elements) {
                var el, _i, _len;
                for (_i = 0, _len = elements.length; _i < _len; _i++) {
                    el = elements[_i];
                    if (/(^| )fallback($| )/.test(el.className)) {
                        return el;
                    }
                }
            };
            _ref = ["div", "form"];
            for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                tagName = _ref[_i];
                if (fallback = getFallback(this.element.getElementsByTagName(tagName))) {
                    return fallback;
                }
            }
        };

        Dropzone.prototype.setupEventListeners = function () {
            var elementListeners, event, listener, _i, _len, _ref, _results;
            _ref = this.listeners;
            _results = [];
            for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                elementListeners = _ref[_i];
                _results.push((function () {
                    var _ref1, _results1;
                    _ref1 = elementListeners.events;
                    _results1 = [];
                    for (event in _ref1) {
                        listener = _ref1[event];
                        _results1.push(elementListeners.element.addEventListener(event, listener, false));
                    }
                    return _results1;
                })());
            }
            return _results;
        };

        Dropzone.prototype.removeEventListeners = function () {
            var elementListeners, event, listener, _i, _len, _ref, _results;
            _ref = this.listeners;
            _results = [];
            for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                elementListeners = _ref[_i];
                _results.push((function () {
                    var _ref1, _results1;
                    _ref1 = elementListeners.events;
                    _results1 = [];
                    for (event in _ref1) {
                        listener = _ref1[event];
                        _results1.push(elementListeners.element.removeEventListener(event, listener, false));
                    }
                    return _results1;
                })());
            }
            return _results;
        };

        Dropzone.prototype.disable = function () {
            var file, _i, _len, _ref, _results;
            this.clickableElements.forEach(function (element) {
                return element.classList.remove("dz-clickable");
            });
            this.removeEventListeners();
            _ref = this.files;
            _results = [];
            for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                file = _ref[_i];
                _results.push(this.cancelUpload(file));
            }
            return _results;
        };

        Dropzone.prototype.enable = function () {
            this.clickableElements.forEach(function (element) {
                return element.classList.add("dz-clickable");
            });
            return this.setupEventListeners();
        };

        Dropzone.prototype.filesize = function (size) {
            var cutoff, i, selectedSize, selectedUnit, unit, units, _i, _len;
            selectedSize = 0;
            selectedUnit = "b";
            if (size > 0) {
                units = ['TB', 'GB', 'MB', 'KB', 'b'];
                for (i = _i = 0, _len = units.length; _i < _len; i = ++_i) {
                    unit = units[i];
                    cutoff = Math.pow(this.options.filesizeBase, 4 - i) / 10;
                    if (size >= cutoff) {
                        selectedSize = size / Math.pow(this.options.filesizeBase, 4 - i);
                        selectedUnit = unit;
                        break;
                    }
                }
                selectedSize = Math.round(10 * selectedSize) / 10;
            }
            return "<strong>" + selectedSize + "</strong> " + selectedUnit;
        };

        Dropzone.prototype._updateMaxFilesReachedClass = function () {
            if ((this.options.maxFiles != null) && this.getAcceptedFiles().length >= this.options.maxFiles) {
                if (this.getAcceptedFiles().length === this.options.maxFiles) {
                    this.emit('maxfilesreached', this.files);
                }
                return this.element.classList.add("dz-max-files-reached");
            } else {
                return this.element.classList.remove("dz-max-files-reached");
            }
        };

        Dropzone.prototype.drop = function (e) {
            var files, items;
            if (!e.dataTransfer) {
                return;
            }
            this.emit("drop", e);
            files = e.dataTransfer.files;
            this.emit("addedfiles", files);
            if (files.length) {
                items = e.dataTransfer.items;
                if (items && items.length && (items[0].webkitGetAsEntry != null)) {
                    this._addFilesFromItems(items);
                } else {
                    this.handleFiles(files);
                }
            }
        };

        Dropzone.prototype.paste = function (e) {
            var items, _ref;
            if ((e != null ? (_ref = e.clipboardData) != null ? _ref.items : void 0 : void 0) == null) {
                return;
            }
            this.emit("paste", e);
            items = e.clipboardData.items;
            if (items.length) {
                return this._addFilesFromItems(items);
            }
        };

        Dropzone.prototype.handleFiles = function (files) {
            var file, _i, _len, _results;
            _results = [];
            for (_i = 0, _len = files.length; _i < _len; _i++) {
                file = files[_i];
                _results.push(this.addFile(file));
            }
            return _results;
        };

        Dropzone.prototype._addFilesFromItems = function (items) {
            var entry, item, _i, _len, _results;
            _results = [];
            for (_i = 0, _len = items.length; _i < _len; _i++) {
                item = items[_i];
                if ((item.webkitGetAsEntry != null) && (entry = item.webkitGetAsEntry())) {
                    if (entry.isFile) {
                        _results.push(this.addFile(item.getAsFile()));
                    } else if (entry.isDirectory) {
                        _results.push(this._addFilesFromDirectory(entry, entry.name));
                    } else {
                        _results.push(void 0);
                    }
                } else if (item.getAsFile != null) {
                    if ((item.kind == null) || item.kind === "file") {
                        _results.push(this.addFile(item.getAsFile()));
                    } else {
                        _results.push(void 0);
                    }
                } else {
                    _results.push(void 0);
                }
            }
            return _results;
        };

        Dropzone.prototype._addFilesFromDirectory = function (directory, path) {
            var dirReader, entriesReader;
            dirReader = directory.createReader();
            entriesReader = (function (_this) {
                return function (entries) {
                    var entry, _i, _len;
                    for (_i = 0, _len = entries.length; _i < _len; _i++) {
                        entry = entries[_i];
                        if (entry.isFile) {
                            entry.file(function (file) {
                                if (_this.options.ignoreHiddenFiles && file.name.substring(0, 1) === '.') {
                                    return;
                                }
                                file.fullPath = "" + path + "/" + file.name;
                                return _this.addFile(file);
                            });
                        } else if (entry.isDirectory) {
                            _this._addFilesFromDirectory(entry, "" + path + "/" + entry.name);
                        }
                    }
                };
            })(this);
            return dirReader.readEntries(entriesReader, function (error) {
                return typeof console !== "undefined" && console !== null ? typeof console.log === "function" ? console.log(error) : void 0 : void 0;
            });
        };

        Dropzone.prototype.accept = function (file, done) {

            if (file.size > this.options.maxFilesize * 1024 * 1024) {
                return done(this.options.dictFileTooBig.replace("{{filesize}}", Math.round(file.size / 1024 / 10.24) / 100).replace("{{maxFilesize}}", this.options.maxFilesize));

            } else if (!Dropzone.isValidFile(file, this.options.acceptedFiles)) {
                return done(this.options.dictInvalidFileType);
            } else if ((this.options.maxFiles != null) && this.getAcceptedFiles().length >= this.options.maxFiles) {
                done(this.options.dictMaxFilesExceeded.replace("{{maxFiles}}", this.options.maxFiles));
                return this.emit("maxfilesexceeded", file);

            } else {
                return this.options.accept.call(this, file, done);
            }
        };

        Dropzone.prototype.addFile = function (file) {
            file.upload = {
                progress: 0,
                total: file.size,
                bytesSent: 0
            };
            this.files.push(file);
            file.status = Dropzone.ADDED;
            this.emit("addedfile", file);
            this._enqueueThumbnail(file);
            return this.accept(file, (function (_this) {
                return function (error) {
                    if (error) {
                        file.accepted = false;
                        _this._errorProcessing([file], error);
                    } else {
                        file.accepted = true;
                        if (_this.options.autoQueue) {
                            _this.enqueueFile(file);
                        }
                    }
                    return _this._updateMaxFilesReachedClass();
                };
            })(this));
        };

        Dropzone.prototype.enqueueFiles = function (files) {
            var file, _i, _len;
            for (_i = 0, _len = files.length; _i < _len; _i++) {
                file = files[_i];
                this.enqueueFile(file);
            }
            return null;
        };

        Dropzone.prototype.enqueueFile = function (file) {
            if (file.status === Dropzone.ADDED && file.accepted === true) {
                file.status = Dropzone.QUEUED;
                if (this.options.autoProcessQueue) {
                    return setTimeout(((function (_this) {
                        return function () {
                            return _this.processQueue();
                        };
                    })(this)), 0);
                }
            } else {
                throw new Error("This file can't be queued because it has already been processed or was rejected.");
            }
        };

        Dropzone.prototype._thumbnailQueue = [];

        Dropzone.prototype._processingThumbnail = false;

        Dropzone.prototype._enqueueThumbnail = function (file) {
            // We don't need it && file.size <= this.options.maxThumbnailFilesize * 1024 * 1024. the accept file will do it
            if (this.options.createImageThumbnails && file.type.match(/image.*/)) {
                this._thumbnailQueue.push(file);
                return setTimeout(((function (_this) {
                    return function () {
                        return _this._processThumbnailQueue();
                    };
                })(this)), 0);
            }
        };

        Dropzone.prototype._processThumbnailQueue = function () {
            if (this._processingThumbnail || this._thumbnailQueue.length === 0) {
                return;
            }
            this._processingThumbnail = true;
            return this.createThumbnail(this._thumbnailQueue.shift(), (function (_this) {
                return function () {
                    _this._processingThumbnail = false;
                    return _this._processThumbnailQueue();
                };
            })(this));
        };

        Dropzone.prototype.removeFile = function (file) {
            if (file.status === Dropzone.UPLOADING) {
                this.cancelUpload(file);
            }
            this.files = without(this.files, file);
            this.emit("removedfile", file);
            if (this.files.length === 0) {
                return this.emit("reset");
            }
        };

        Dropzone.prototype.removeAllFiles = function (cancelIfNecessary) {
            var file, _i, _len, _ref;
            if (cancelIfNecessary == null) {
                cancelIfNecessary = false;
            }
            _ref = this.files.slice();
            for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                file = _ref[_i];
                if (file.status !== Dropzone.UPLOADING || cancelIfNecessary) {
                    this.removeFile(file);
                }
            }
            return null;
        };

        Dropzone.prototype.createThumbnail = function (file, callback) {
            var fileReader;
            fileReader = new FileReader;
            fileReader.onload = (function (_this) {
                return function () {
                    if (file.type === "image/svg+xml") {
                        _this.emit("thumbnail", file, fileReader.result);
                        if (callback != null) {
                            callback();
                        }
                        return;
                    }

                    return _this.createThumbnailFromUrl(file, fileReader.result, callback);
                };
            })(this);
            return fileReader.readAsDataURL(file);
        };

        Dropzone.prototype.createThumbnailFromUrl = function (file, imageUrl, callback) {

            var img;
            img = document.createElement("img");
            img.onload = (function (_this) {
                return function () {
                    var canvas, ctx, resizeInfo, thumbnail, _ref, _ref1, _ref2, _ref3;
                    file.width = img.width;
                    file.height = img.height;
                    resizeInfo = _this.options.resize.call(_this, file);

                    if (resizeInfo.trgWidth == null) {
                        resizeInfo.trgWidth = resizeInfo.optWidth;
                    }
                    if (resizeInfo.trgHeight == null) {
                        resizeInfo.trgHeight = resizeInfo.optHeight;
                    }
                    canvas = document.createElement("canvas");
                    ctx = canvas.getContext("2d");
                    canvas.width = resizeInfo.trgWidth;
                    canvas.height = resizeInfo.trgHeight;
                    drawImageIOSFix(ctx, img, (_ref = resizeInfo.srcX) != null ? _ref : 0, (_ref1 = resizeInfo.srcY) != null ? _ref1 : 0, resizeInfo.srcWidth, resizeInfo.srcHeight, (_ref2 = resizeInfo.trgX) != null ? _ref2 : 0, (_ref3 = resizeInfo.trgY) != null ? _ref3 : 0, resizeInfo.trgWidth, resizeInfo.trgHeight);
                    thumbnail = canvas.toDataURL("image/png");
                    _this.emit("thumbnail", file, thumbnail);

                    if (callback != null) {
                        return callback();
                    }
                };
            })(this);
            if (callback != null) {
                img.onerror = callback;
            }
            return img.src = imageUrl;
        };

        Dropzone.prototype.processQueue = function () {
            var i, parallelUploads, processingLength, queuedFiles;

            parallelUploads = this.options.parallelUploads;
            processingLength = this.getUploadingFiles().length;
            i = processingLength;
            if (processingLength >= parallelUploads) {
                return;
            }
            queuedFiles = this.getQueuedFiles();
            if (!(queuedFiles.length > 0)) {
                return;
            }
            if (this.options.uploadMultiple) {
                return this.processFiles(queuedFiles.slice(0, parallelUploads - processingLength));
            } else {
                while (i < parallelUploads) {
                    if (!queuedFiles.length) {
                        return;
                    }
                    this.processFile(queuedFiles.shift());
                    i++;
                }
            }
        };

        Dropzone.prototype.processFile = function (file) {
            return this.processFiles([file]);
        };

        Dropzone.prototype.processFiles = function (files) {
            var file, _i, _len;
            for (_i = 0, _len = files.length; _i < _len; _i++) {
                file = files[_i];
                file.processing = true;
                file.status = Dropzone.UPLOADING;
                this.emit("processing", file);
            }
            if (this.options.uploadMultiple) {
                this.emit("processingmultiple", files);
            }
            return this.uploadFiles(files);
        };

        Dropzone.prototype._getFilesWithXhr = function (xhr) {
            var file, files;
            return files = (function () {
                var _i, _len, _ref, _results;
                _ref = this.files;
                _results = [];
                for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                    file = _ref[_i];
                    if (file.xhr === xhr) {
                        _results.push(file);
                    }
                }
                return _results;
            }).call(this);
        };

        Dropzone.prototype.cancelUpload = function (file) {
            var groupedFile, groupedFiles, _i, _j, _len, _len1, _ref;
            if (file.status === Dropzone.UPLOADING) {
                groupedFiles = this._getFilesWithXhr(file.xhr);
                for (_i = 0, _len = groupedFiles.length; _i < _len; _i++) {
                    groupedFile = groupedFiles[_i];
                    groupedFile.status = Dropzone.CANCELED;
                }
                file.xhr.abort();
                for (_j = 0, _len1 = groupedFiles.length; _j < _len1; _j++) {
                    groupedFile = groupedFiles[_j];
                    this.emit("canceled", groupedFile);
                }
                if (this.options.uploadMultiple) {
                    this.emit("canceledmultiple", groupedFiles);
                }
            } else if ((_ref = file.status) === Dropzone.ADDED || _ref === Dropzone.QUEUED) {
                file.status = Dropzone.CANCELED;
                this.emit("canceled", file);
                if (this.options.uploadMultiple) {
                    this.emit("canceledmultiple", [file]);
                }
            }
            if (this.options.autoProcessQueue) {
                return this.processQueue();
            }
        };

        resolveOption = function () {
            var args, option;
            option = arguments[0], args = 2 <= arguments.length ? __slice.call(arguments, 1) : [];
            if (typeof option === 'function') {
                return option.apply(this, args);
            }
            return option;
        };

        Dropzone.prototype.uploadFile = function (file) {
            return this.uploadFiles([file]);
        };

        Dropzone.prototype.uploadFiles = function (files) {
            for (var i = 0; i < files.length; i++) {
                var file = files[i];
                file.upload.bytesSent = file.size;
                file.upload.progress = 100;
                file.upload.total = file.size;
                this.emit("uploadprogress", file, 100, file.size);
            }
            return this._finished(files, 'ok', {});

            var file, formData, handleError, headerName, headerValue, headers, i, input, inputName, inputType, key, method, option, progressObj, response, updateProgress, url, value, xhr, _i, _j, _k, _l, _len, _len1, _len2, _len3, _m, _ref, _ref1, _ref2, _ref3, _ref4, _ref5;
            xhr = new XMLHttpRequest();
            for (_i = 0, _len = files.length; _i < _len; _i++) {
                file = files[_i];
                file.xhr = xhr;
            }
            method = resolveOption(this.options.method, files);
            url = resolveOption(this.options.url, files);
            xhr.open(method, url, true);
            xhr.withCredentials = !!this.options.withCredentials;
            response = null;
            handleError = (function (_this) {
                return function () {
                    var _j, _len1, _results;
                    _results = [];
                    for (_j = 0, _len1 = files.length; _j < _len1; _j++) {
                        file = files[_j];
                        _results.push(_this._errorProcessing(files, response || _this.options.dictResponseError.replace("{{statusCode}}", xhr.status), xhr));
                    }
                    return _results;
                };
            })(this);
            updateProgress = (function (_this) {
                return function (e) {
                    var allFilesFinished, progress, _j, _k, _l, _len1, _len2, _len3, _results;
                    if (e != null) {
                        progress = 100 * e.loaded / e.total;
                        for (_j = 0, _len1 = files.length; _j < _len1; _j++) {
                            file = files[_j];
                            file.upload = {
                                progress: progress,
                                total: e.total,
                                bytesSent: e.loaded
                            };
                        }
                    } else {
                        allFilesFinished = true;
                        progress = 100;
                        for (_k = 0, _len2 = files.length; _k < _len2; _k++) {
                            file = files[_k];
                            if (!(file.upload.progress === 100 && file.upload.bytesSent === file.upload.total)) {
                                allFilesFinished = false;
                            }
                            file.upload.progress = progress;
                            file.upload.bytesSent = file.upload.total;
                        }
                        if (allFilesFinished) {
                            return;
                        }
                    }
                    _results = [];
                    for (_l = 0, _len3 = files.length; _l < _len3; _l++) {
                        file = files[_l];
                        _results.push(_this.emit("uploadprogress", file, progress, file.upload.bytesSent));
                    }
                    return _results;
                };
            })(this);
            xhr.onload = (function (_this) {
                return function (e) {
                    var _ref;
                    if (files[0].status === Dropzone.CANCELED) {
                        return;
                    }
                    if (xhr.readyState !== 4) {
                        return;
                    }
                    response = xhr.responseText;
                    if (xhr.getResponseHeader("content-type") && ~xhr.getResponseHeader("content-type").indexOf("application/json")) {
                        try {
                            response = JSON.parse(response);
                        } catch (_error) {
                            e = _error;
                            response = "Invalid JSON response from server.";
                        }
                    }
                    updateProgress();
                    if (!((200 <= (_ref = xhr.status) && _ref < 300))) {
                        return handleError();
                    } else {
                        return _this._finished(files, response, e);
                    }
                };
            })(this);
            xhr.onerror = (function (_this) {
                return function () {
                    if (files[0].status === Dropzone.CANCELED) {
                        return;
                    }
                    return handleError();
                };
            })(this);
            progressObj = (_ref = xhr.upload) != null ? _ref : xhr;
            progressObj.onprogress = updateProgress;
            headers = {
                "Accept": "application/json",
                "Cache-Control": "no-cache",
                "X-Requested-With": "XMLHttpRequest"
            };
            if (this.options.headers) {
                extend(headers, this.options.headers);
            }
            for (headerName in headers) {
                headerValue = headers[headerName];
                if (headerValue) {
                    xhr.setRequestHeader(headerName, headerValue);
                }
            }
            formData = new FormData();
            if (this.options.params) {
                _ref1 = this.options.params;
                for (key in _ref1) {
                    value = _ref1[key];
                    formData.append(key, value);
                }
            }
            for (_j = 0, _len1 = files.length; _j < _len1; _j++) {
                file = files[_j];
                this.emit("sending", file, xhr, formData);
            }
            if (this.options.uploadMultiple) {
                this.emit("sendingmultiple", files, xhr, formData);
            }
            if (this.element.tagName === "FORM") {
                _ref2 = this.element.querySelectorAll("input, textarea, select, button");
                for (_k = 0, _len2 = _ref2.length; _k < _len2; _k++) {
                    input = _ref2[_k];
                    inputName = input.getAttribute("name");
                    inputType = input.getAttribute("type");
                    if (input.tagName === "SELECT" && input.hasAttribute("multiple")) {
                        _ref3 = input.options;
                        for (_l = 0, _len3 = _ref3.length; _l < _len3; _l++) {
                            option = _ref3[_l];
                            if (option.selected) {
                                formData.append(inputName, option.value);
                            }
                        }
                    } else if (!inputType || ((_ref4 = inputType.toLowerCase()) !== "checkbox" && _ref4 !== "radio") || input.checked) {
                        formData.append(inputName, input.value);
                    }
                }
            }
            for (i = _m = 0, _ref5 = files.length - 1; 0 <= _ref5 ? _m <= _ref5 : _m >= _ref5; i = 0 <= _ref5 ? ++_m : --_m) {
                formData.append(this._getParamName(i), files[i], files[i].name);
            }
            return this.submitRequest(xhr, formData, files);
        };

        Dropzone.prototype.submitRequest = function (xhr, formData, files) {
            return xhr.send(formData);
        };

        Dropzone.prototype._finished = function (files, responseText, e) {
            var file, _i, _len;
            for (_i = 0, _len = files.length; _i < _len; _i++) {
                file = files[_i];
                file.status = Dropzone.SUCCESS;
                this.emit("success", file, responseText, e);
                this.emit("complete", file);
            }
            if (this.options.uploadMultiple) {
                this.emit("successmultiple", files, responseText, e);
                this.emit("completemultiple", files);
            }
            if (this.options.autoProcessQueue) {
                return this.processQueue();
            }
        };

        Dropzone.prototype._errorProcessing = function (files, message, xhr) {
            var file, _i, _len;
            for (_i = 0, _len = files.length; _i < _len; _i++) {
                file = files[_i];
                file.status = Dropzone.ERROR;
                this.emit("error", file, message, xhr);
                this.emit("complete", file);
            }
            if (this.options.uploadMultiple) {
                this.emit("errormultiple", files, message, xhr);
                this.emit("completemultiple", files);
            }
            if (this.options.autoProcessQueue) {
                return this.processQueue();
            }
        };

        return Dropzone;

    })(Emitter);

    Dropzone.version = "4.1.0";

    Dropzone.options = {};

    Dropzone.optionsForElement = function (element) {
        if (element.getAttribute("id")) {
            return Dropzone.options[camelize(element.getAttribute("id"))];
        } else {
            return void 0;
        }
    };

    Dropzone.instances = [];

    Dropzone.forElement = function (element) {
        if (typeof element === "string") {
            element = document.querySelector(element);
        }
        if ((element != null ? element.dropzone : void 0) == null) {
            throw new Error("No Dropzone found for given element. This is probably because you're trying to access it before Dropzone had the time to initialize. Use the `init` option to setup any additional observers on your Dropzone.");
        }
        return element.dropzone;
    };

    Dropzone.autoDiscover = true;

    Dropzone.discover = function () {
        var checkElements, dropzone, dropzones, _i, _len, _results;
        if (document.querySelectorAll) {
            dropzones = document.querySelectorAll(".dropzone");
        } else {
            dropzones = [];
            checkElements = function (elements) {
                var el, _i, _len, _results;
                _results = [];
                for (_i = 0, _len = elements.length; _i < _len; _i++) {
                    el = elements[_i];
                    if (/(^| )dropzone($| )/.test(el.className)) {
                        _results.push(dropzones.push(el));
                    } else {
                        _results.push(void 0);
                    }
                }
                return _results;
            };
            checkElements(document.getElementsByTagName("div"));
            checkElements(document.getElementsByTagName("form"));
        }
        _results = [];
        for (_i = 0, _len = dropzones.length; _i < _len; _i++) {
            dropzone = dropzones[_i];
            if (Dropzone.optionsForElement(dropzone) !== false) {
                _results.push(new Dropzone(dropzone));
            } else {
                _results.push(void 0);
            }
        }
        return _results;
    };

    Dropzone.blacklistedBrowsers = [/opera.*Macintosh.*version\/12/i];

    Dropzone.isBrowserSupported = function () {
        var capableBrowser, regex, _i, _len, _ref;
        capableBrowser = true;
        if (window.File && window.FileReader && window.FileList && window.Blob && window.FormData && document.querySelector) {
            if (!("classList" in document.createElement("a"))) {
                capableBrowser = false;
            } else {
                _ref = Dropzone.blacklistedBrowsers;
                for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                    regex = _ref[_i];
                    if (regex.test(navigator.userAgent)) {
                        capableBrowser = false;
                        continue;
                    }
                }
            }
        } else {
            capableBrowser = false;
        }
        return capableBrowser;
    };

    without = function (list, rejectedItem) {
        var item, _i, _len, _results;
        _results = [];
        for (_i = 0, _len = list.length; _i < _len; _i++) {
            item = list[_i];
            if (item !== rejectedItem) {
                _results.push(item);
            }
        }
        return _results;
    };

    camelize = function (str) {
        return str.replace(/[\-_](\w)/g, function (match) {
            return match.charAt(1).toUpperCase();
        });
    };

    Dropzone.createElement = function (string) {
        var div;
        div = document.createElement("div");
        div.innerHTML = string;
        return div.childNodes[0];
    };

    Dropzone.elementInside = function (element, container) {
        if (element === container) {
            return true;
        }
        while (element = element.parentNode) {
            if (element === container) {
                return true;
            }
        }
        return false;
    };

    Dropzone.getElement = function (el, name) {
        var element;
        if (typeof el === "string") {
            element = document.querySelector(el);
        } else if (el.nodeType != null) {
            element = el;
        }
        if (element == null) {
            throw new Error("Invalid `" + name + "` option provided. Please provide a CSS selector or a plain HTML element.");
        }
        return element;
    };

    Dropzone.getElements = function (els, name) {
        var e, el, elements, _i, _j, _len, _len1, _ref;
        if (els instanceof Array) {
            elements = [];
            try {
                for (_i = 0, _len = els.length; _i < _len; _i++) {
                    el = els[_i];
                    elements.push(this.getElement(el, name));
                }
            } catch (_error) {
                e = _error;
                elements = null;
            }
        } else if (typeof els === "string") {
            elements = [];
            _ref = document.querySelectorAll(els);
            for (_j = 0, _len1 = _ref.length; _j < _len1; _j++) {
                el = _ref[_j];
                elements.push(el);
            }
        } else if (els.nodeType != null) {
            elements = [els];
        }
        if (!((elements != null) && elements.length)) {
            throw new Error("Invalid `" + name + "` option provided. Please provide a CSS selector, a plain HTML element or a list of those.");
        }
        return elements;
    };

    Dropzone.confirm = function (question, accepted, rejected) {
        if (window.confirm(question)) {
            return accepted();
        } else if (rejected != null) {
            return rejected();
        }
    };

    Dropzone.isValidFile = function (file, acceptedFiles) {
        var baseMimeType, mimeType, validType, _i, _len;
        if (!acceptedFiles) {
            return true;
        }
        acceptedFiles = acceptedFiles.split(",");
        mimeType = file.type;
        baseMimeType = mimeType.replace(/\/.*$/, "");
        for (_i = 0, _len = acceptedFiles.length; _i < _len; _i++) {
            validType = acceptedFiles[_i];
            validType = validType.trim();
            if (validType.charAt(0) === ".") {
                if (file.name.toLowerCase().indexOf(validType.toLowerCase(), file.name.length - validType.length) !== -1) {
                    return true;
                }
            } else if (/\/\*$/.test(validType)) {
                if (baseMimeType === validType.replace(/\/.*$/, "")) {
                    return true;
                }
            } else {
                if (mimeType === validType) {
                    return true;
                }
            }
        }
        return false;
    };

    if (typeof jQuery !== "undefined" && jQuery !== null) {
        jQuery.fn.dropzone = function (options) {
            return this.each(function () {
                return new Dropzone(this, options);
            });
        };
    }

    if (typeof module !== "undefined" && module !== null) {
        module.exports = Dropzone;
    } else {
        window.Dropzone = Dropzone;
    }

    Dropzone.ADDED = "added";

    Dropzone.QUEUED = "queued";

    Dropzone.ACCEPTED = Dropzone.QUEUED;

    Dropzone.UPLOADING = "uploading";

    Dropzone.PROCESSING = Dropzone.UPLOADING;

    Dropzone.CANCELED = "canceled";

    Dropzone.ERROR = "error";

    Dropzone.SUCCESS = "success";


    /*
  
  Bugfix for iOS 6 and 7
  Source: http://stackoverflow.com/questions/11929099/html5-canvas-drawimage-ratio-bug-ios
  based on the work of https://github.com/stomita/ios-imagefile-megapixel
   */

    detectVerticalSquash = function (img) {
        var alpha, canvas, ctx, data, ey, ih, iw, py, ratio, sy;
        iw = img.naturalWidth;
        ih = img.naturalHeight;
        canvas = document.createElement("canvas");
        canvas.width = 1;
        canvas.height = ih;
        ctx = canvas.getContext("2d");
        ctx.drawImage(img, 0, 0);
        data = ctx.getImageData(0, 0, 1, ih).data;
        sy = 0;
        ey = ih;
        py = ih;
        while (py > sy) {
            alpha = data[(py - 1) * 4 + 3];
            if (alpha === 0) {
                ey = py;
            } else {
                sy = py;
            }
            py = (ey + sy) >> 1;
        }
        ratio = py / ih;
        if (ratio === 0) {
            return 1;
        } else {
            return ratio;
        }
    };

    drawImageIOSFix = function (ctx, img, sx, sy, sw, sh, dx, dy, dw, dh) {
        var vertSquashRatio;
        vertSquashRatio = detectVerticalSquash(img);
        return ctx.drawImage(img, sx, sy, sw, sh, dx, dy, dw, dh / vertSquashRatio);
    };


    /*
   * contentloaded.js
   *
   * Author: Diego Perini (diego.perini at gmail.com)
   * Summary: cross-browser wrapper for DOMContentLoaded
   * Updated: 20101020
   * License: MIT
   * Version: 1.2
   *
   * URL:
   * http://javascript.nwbox.com/ContentLoaded/
   * http://javascript.nwbox.com/ContentLoaded/MIT-LICENSE
   */

    contentLoaded = function (win, fn) {
        var add, doc, done, init, poll, pre, rem, root, top;
        done = false;
        top = true;
        doc = win.document;
        root = doc.documentElement;
        add = (doc.addEventListener ? "addEventListener" : "attachEvent");
        rem = (doc.addEventListener ? "removeEventListener" : "detachEvent");
        pre = (doc.addEventListener ? "" : "on");
        init = function (e) {
            if (e.type === "readystatechange" && doc.readyState !== "complete") {
                return;
            }
            (e.type === "load" ? win : doc)[rem](pre + e.type, init, false);
            if (!done && (done = true)) {
                return fn.call(win, e.type || e);
            }
        };
        poll = function () {
            var e;
            try {
                root.doScroll("left");
            } catch (_error) {
                e = _error;
                setTimeout(poll, 50);
                return;
            }
            return init("poll");
        };
        if (doc.readyState !== "complete") {
            if (doc.createEventObject && root.doScroll) {
                try {
                    top = !win.frameElement;
                } catch (_error) {
                }
                if (top) {
                    poll();
                }
            }
            doc[add](pre + "DOMContentLoaded", init, false);
            doc[add](pre + "readystatechange", init, false);
            return win[add](pre + "load", init, false);
        }
    };

    Dropzone._autoDiscoverFunction = function () {
        if (Dropzone.autoDiscover) {
            return Dropzone.discover();
        }
    };

    contentLoaded(window, Dropzone._autoDiscoverFunction);

}).call(this);

var rakey = 1;
var infoWindow;
var map;
var markers = [];
var mappo;
var idkeym = 0;
var idsate;
var idDistrict;
var idWard;
var id = 59;
var url;
var advertId1;
var advertId;
var idNext;

var HCM_GEO = {
    lat: 10.76025245,
    lng: 106.689502,
    polygon: [{ "lat": 11.160161, "lng": 106.456955 }, { "lat": 11.1558809, "lng": 106.469604 }, { "lat": 11.1432467, "lng": 106.472054 }, { "lat": 11.1419792, "lng": 106.50621 }, { "lat": 11.1213474, "lng": 106.524635 }, { "lat": 11.108182, "lng": 106.5269 }, { "lat": 11.1010885, "lng": 106.51635 }, { "lat": 11.0885506, "lng": 106.533844 }, { "lat": 11.0746708, "lng": 106.523972 }, { "lat": 11.0562649, "lng": 106.53643 }, { "lat": 11.0456123, "lng": 106.553459 }, { "lat": 11.0513067, "lng": 106.575813 }, { "lat": 11.0362854, "lng": 106.588867 }, { "lat": 11.0405741, "lng": 106.603165 }, { "lat": 11.016613, "lng": 106.597511 }, { "lat": 11.00637, "lng": 106.620712 }, { "lat": 10.9921207, "lng": 106.615913 }, { "lat": 10.9793682, "lng": 106.627319 }, { "lat": 10.97888, "lng": 106.649155 }, { "lat": 10.9312754, "lng": 106.651756 }, { "lat": 10.9212961, "lng": 106.684669 }, { "lat": 10.898036, "lng": 106.694344 }, { "lat": 10.8791285, "lng": 106.690506 }, { "lat": 10.8673038, "lng": 106.701561 }, { "lat": 10.87168, "lng": 106.721443 }, { "lat": 10.8942976, "lng": 106.716393 }, { "lat": 10.8814974, "lng": 106.744896 }, { "lat": 10.8654289, "lng": 106.749313 }, { "lat": 10.86727, "lng": 106.7622 }, { "lat": 10.8915377, "lng": 106.766884 }, { "lat": 10.8672609, "lng": 106.784676 }, { "lat": 10.8730507, "lng": 106.808487 }, { "lat": 10.89838, "lng": 106.837891 }, { "lat": 10.8471689, "lng": 106.854088 }, { "lat": 10.81358, "lng": 106.881332 }, { "lat": 10.78804, "lng": 106.86454 }, { "lat": 10.7696714, "lng": 106.874878 }, { "lat": 10.7642078, "lng": 106.868484 }, { "lat": 10.77534, "lng": 106.844315 }, { "lat": 10.7600527, "lng": 106.828133 }, { "lat": 10.777709, "lng": 106.825294 }, { "lat": 10.7778044, "lng": 106.813393 }, { "lat": 10.7270269, "lng": 106.753693 }, { "lat": 10.6977386, "lng": 106.75444 }, { "lat": 10.6584368, "lng": 106.804779 }, { "lat": 10.6284494, "lng": 106.824547 }, { "lat": 10.6477795, "lng": 106.8822 }, { "lat": 10.6095228, "lng": 106.910019 }, { "lat": 10.5800753, "lng": 106.982338 }, { "lat": 10.53047, "lng": 106.987343 }, { "lat": 10.5203629, "lng": 107.012794 }, { "lat": 10.4908781, "lng": 106.99855 }, { "lat": 10.4553862, "lng": 107.022675 }, { "lat": 10.4280558, "lng": 107.018059 }, { "lat": 10.4133062, "lng": 107.004646 }, { "lat": 10.3603439, "lng": 106.873863 }, { "lat": 10.3776026, "lng": 106.82682 }, { "lat": 10.4718256, "lng": 106.751137 }, { "lat": 10.5761213, "lng": 106.74575 }, { "lat": 10.58732, "lng": 106.724159 }, { "lat": 10.6060667, "lng": 106.71904 }, { "lat": 10.6092567, "lng": 106.729294 }, { "lat": 10.6338387, "lng": 106.731369 }, { "lat": 10.6442051, "lng": 106.723274 }, { "lat": 10.6543074, "lng": 106.688164 }, { "lat": 10.6263351, "lng": 106.635918 }, { "lat": 10.6578169, "lng": 106.607635 }, { "lat": 10.6486864, "lng": 106.558517 }, { "lat": 10.6574326, "lng": 106.548935 }, { "lat": 10.6818113, "lng": 106.551308 }, { "lat": 10.688303, "lng": 106.533905 }, { "lat": 10.7161579, "lng": 106.529282 }, { "lat": 10.7339773, "lng": 106.483849 }, { "lat": 10.7565212, "lng": 106.463661 }, { "lat": 10.78892, "lng": 106.504547 }, { "lat": 10.8977242, "lng": 106.531815 }, { "lat": 10.9736071, "lng": 106.411911 }, { "lat": 10.9700651, "lng": 106.37719 }, { "lat": 10.9918957, "lng": 106.356331 }, { "lat": 11.0090189, "lng": 106.412682 }, { "lat": 11.0911493, "lng": 106.426033 }, { "lat": 11.1466074, "lng": 106.458038 }, { "lat": 11.160161, "lng": 106.456955 }],
}

var MAP_ZOOM = {
    state: 9,
    district: 12,
    ward: 14,
    address: 16
};

var NewUI = {
    products: {},
    projects: {},
    districts: [],
    wards: []
};

$(function () {
    $.blockUI.defaults = {
        overlayCSS: {
            backgroundColor: '#000',
            opacity: 0,
            cursor: 'wait'
        }
    };

    backtotop();
    initMap();

    NewUI.url = window.location.href;
    advertId1 = NewUI.url.substr(NewUI.url.lastIndexOf('=') + 1);
    advertId = parseInt(advertId1.split('#')[0]);
    idNext = advertId1.split('#')[1];

    NProgress.start();
    $('#createAdContainer').block({ message: null });
    $.getJSON('/Services/CreateAd/GetInitData', { aid: advertId }, function (respone) {
        NProgress.set(100);
        $('#createAdContainer').unblock();

        NewUI.data = respone;
        StartUI();
    });
});

function StartUI() {
    if (NewUI.data.advert.Id > 0) {
        FillAdvertData(function () {
            $('.hidAdvertIdNewui').text(NewUI.data.advert.Id);

            if (idNext == 'rnew') {
                // Go to choose product view
                GetTypeProduct($('#ddlGeoState').val(), function () {
                    var productAssignedId = 9;
                    if (NewUI.data.advert.ProductAssignments.length > 0) {
                        productAssignedId = NewUI.data.advert.ProductAssignments[0].Id;
                    }
                    GetTypeProductSelected(productAssignedId);

                    $('.classinput').addClass('hidden');
                    $('.ClassSelectService').removeClass('hidden');
                    $('.cuss-steps-1').removeClass('active');
                    $('.cuss-steps-1').addClass('complete');

                    $('.prs1').addClass('progressafter');
                    $('.prsb1').addClass('progress-barafter');
                    $('.numberCircle1').addClass('numberCircleafter');
                    $('.numberCircle1').removeClass('numberCircle');

                    $('.numberCircle2').addClass('numberCircle');
                    $('.numberCircle2').removeClass('numberCirclebefor');
                    $('.cuss-steps-2').addClass('active');
                });
            }
        });
    } else {
        $('#hdAdvertID').val(0);
        LoadState();
        var coradio = $.cookie('radioBan');
        var coSate = $.cookie('stateID');
        var coDis = $.cookie('districtID');
        if (coradio != null) {
            if (coradio == 1) {
                $('input[name="optionsRadiosInline"][value="' + coradio + '"]').prop('checked', true);
                rakey = 1;
                LoadCategory(rakey);
            }

            if (coradio == 2) {
                $('input[name="optionsRadiosInline"][value="' + coradio + '"]').prop('checked', true);
                rakey = 2;
                LoadCategory(rakey);
            }
        } else {
            rakey = 1;
            LoadCategory(rakey);
        }

        $('#ddlGeoState option[value=' + coSate + ']').attr('selected', true);
        var stateId = parseInt(coSate, 10);
        if (isNaN(stateId)) {
            stateId = 0;
        }
        LoadGeoDistrict(stateId);

        var districtId = parseInt(coDis, 10);
        if (isNaN(districtId)) {
            districtId = 0;
        }
        LoadGeoWard(districtId);

        initMap(function () {
            UpdateMapDisplayByLocation({
                StateId: stateId,
                DistrictId: districtId,
                LocationId: 0
            });
        });
    }

    LoadAllUser();
    $('#ddlCategory').change(function () {
        LoadSubCategory(parseInt($(this).val()), 10);
        LoadCategoryAttribites($(this).val(), function () {
            FillAttributes();
        });
    });

    $('#ddlGeoState').change(function () {
        var stateId = parseInt($(this).val(), 10);
        LoadGeoDistrict(stateId, function () {
            idDistrict = $('#ddlGeoDistrict').val();
            UpdateMapDisplayByLocation({
                StateId: stateId,
                DistrictId: 0,
                LocationId: 0,
            });
        });
    });

    $('#ddlGeoDistrict').change(function () {
        var distrcitId = parseInt($(this).val(), 10);
        LoadGeoWard(distrcitId, function () {
            idsate = $('#ddlGeoState').val();
            UpdateMapDisplayByLocation({
                StateId: parseInt(idsate, 10),
                DistrictId: distrcitId,
                LocationId: 0
            });
        });

        LoadProject(distrcitId);
    });

    $('#ddlGeoWard').change(function () {
        idsate = $('#ddlGeoState').val();
        idDistrict = $('#ddlGeoDistrict').val();
        idWard = $(this).val();

        UpdateMapDisplayByLocation({
            StateId: parseInt(idsate, 10),
            DistrictId: parseInt(idDistrict, 10),
            LocationId: parseInt(idWard, 10),
        });
    });

    autosize($('.auto-size'));
    $("#txtPhone2").keydown(function (e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            // Allow: Ctrl+A, Command+A
            (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: home, end, left, right, down, up
            (e.keyCode >= 35 && e.keyCode <= 40) || (e.keyCode == 231)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });

    $('#radioBan').change(function () {
        rakey = 1;
        LoadCategory(rakey);
    });
    $('#radioChothue').change(function () {
        rakey = 2;
        LoadCategory(rakey);
    });

    $('#radioDT1').change(function () {
        $('#hdValueDT').val(1);
    });
    $('#radioDT2').change(function () {
        $('#hdValueDT').val(2);
    });

    if (advertId >= 0) {
        $('.classCreate').addClass('hidden');
        $('.classEdit').removeClass('hidden');
        var valhref = $('.preOldUied').attr('href');
        $('.preOldUied').attr('href', '/member/object-edit?aid=' + advertId);
    } else {
        $('.classCreate').removeClass('hidden');
        $('.classEdit').addClass('hidden');
    }
}

function convertnum(priceval) {
    // No allow number sub > 3
    if ((priceval.match(/,/g) || []).length == 1) {
        var subNumber = priceval.split(',')[1];
        if (subNumber.length > 2) {
            priceval = priceval.split(',')[0] + ',' + subNumber.slice(0, -1);
        } else {
            priceval = priceval.split(',')[0] + ',' + subNumber;
        }
    }
    // No comma in number
    if ((priceval.match(/,/g) || []).length > 1) {
        var lastChar = priceval.slice(-1);
        if (lastChar == ',') {
            priceval = priceval.slice(0, -1);
        }
    }
    return priceval;
}

//Load Category
function LoadCategory(idkey) {
    $('#ddlCategory').children('option').remove();
    $('#ddlCategory').append("<option value='0'>Ch?n danh m?c</option>");

    var html = [];
    var categoryIds = NewUI.data.transactionTypes[idkey];
    for (var i = 0; i < NewUI.data.categories.length; i++) {
        var category = NewUI.data.categories[i];
        if (categoryIds.indexOf(category.CategoryId) >= 0) {
            html.push("<option value=" + category.CategoryId + ">" + category.Name + "</option>");
        }
    }
    $('#ddlCategory').append(html.join(''));
    var categoryId = parseInt($('#ddlCategory').val(), 10);
    LoadSubCategory(categoryId);
}

//Load Subcategory
function LoadSubCategory(categoryId) {
    if (!categoryId || categoryId == 0) {
        $('#ddlSubCategory').children('option').remove();
        $('#ddlSubCategory').append("<option value='0'>Ch?n lo?i</option>");
    } else {
        $('#ddlSubCategory').children('option').remove();
        var html = [];
        var categories = NewUI.data.subCategories[categoryId];
        if (categories && categories.length > 0) {
            $.each(categories, function (i, data) {
                var res = data.Name.replace("(1)", "");
                $('#ddlSubCategory').append("<option value=" + data.CategoryId + ">" + res + "</option>");
            });
        }
    }
}

//Load State
function LoadState() {
    var states = NewUI.data.states;
    for (var i = 0; i < states.length; i++) {
        var data = states[i];
        $('#ddlGeoState').append("<option value=" + data.Id + ">" + data.Name + "</option>");
    }
}

//Load District
function LoadGeoDistrict(stateId, callback) {
    $('#ddlGeoDistrict').children('option').remove();
    $('#ddlGeoDistrict').append("<option value='0'>Ch?n Qu?n/Huy?n</option>");
    if (stateId == 0) {
        $('#ddlGeoWard').children('option').remove();
        $('#ddlGeoWard').append("<option value='0'>Ch?n Ph??ng</option>");
        $('#ddlProject').children('option').remove();
        $('#ddlProject').append("<option value='0'>Ch?n d? án</option>");

        if (callback) callback();
    } else {
        $.getJSON("/Services/Geo/GetDistricts", {
            stateId: stateId
        }, function (respone) {
            NewUI.data.districts = respone;

            $.each(respone, function (i, data) {
                $('#ddlGeoDistrict').append("<option value=" + data.Id + ">" + data.Name + "</option>");
            });

            if (callback) callback();
        });
    }
}

//Load Ward
function LoadGeoWard(districtId, callback) {
    $('#ddlGeoWard').children('option').remove();
    $('#ddlGeoWard').append("<option value='0'>Ch?n Ph??ng</option>");

    if (districtId > 0) {
        $.getJSON("/Services/Geo/GetWards", {
            districtId: districtId
        }, function (respone) {
            NewUI.data.wards = respone;

            $.each(respone, function (i, data) {
                $('#ddlGeoWard').append("<option value=" + data.Id + ">" + data.Name + "</option>");
            });

            if (callback) callback();
        });
    } else {
        if (callback) callback();
    }
}

//Load Project
function LoadProject(districtId) {
    $('#ddlProject').children('option').remove();
    $('#ddlProject').append("<option value='0'>Ch?n d? án</option>");

    if (districtId > 0) {
        if (NewUI.projects[districtId]) {
            InsertProjectsToHtml(NewUI.projects[districtId]);
        } else {
            $.getJSON('/Services/LoadNewUi.ashx', { type: 'GetProject', id: districtId, _: $.now() }, function (respone) {
                NewUI.projects[districtId] = respone;
                InsertProjectsToHtml(NewUI.projects[districtId]);
            });
        }
    }
}
function InsertProjectsToHtml(respone) {
    $.each(respone, function (i, data) {
        var t = data.Name.toLowerCase();
        $('#ddlProject').append("<option value=" + data.Id + ">" + data.Name + "</option>");
    });

    $('#ddlProject').val('').selectpicker('refresh');
    $('#ddlProject').selectpicker();
    $('.glyphicon').removeClass('glyphicon-ok');
}

//Load all data
function FillAttributes() {
    if (NewUI.data.advert.Id > 0) {
        $('.cusEditImg').removeClass('hidden');
        $('.hidAdvertIdNewui').text(NewUI.data.advert.Id);
    } else {
        $('.cusEditImg').addClass('hidden');
    }

    var attributes = [];
    if (NewUI.data.advert && NewUI.data.advert.Attributes) {
        attributes = NewUI.data.advert.Attributes;
    }

    if (attributes.length == 0) return;

    $(attributes).each(function (idex, item) {
        if (item.GroupId == 1) {
            $('[name=grp_1]').val(item.ValueDecimal);
            $('#grp_1').val(item.ValueDecimal);
            if (item.ValueDecimal == 0) {
                $('#grp_1').attr('disabled', 'disable');
                $('.money-text').text('');
            } else {
                setTextMoneyAllowDecimal($('#grp_1'));
            }
        }

        var listAttr = $('[name=grp_' + item.GroupId + ']');
        if (listAttr.length > 0) {
            if (listAttr[0].tagName.toLowerCase() == "input") {
                if (listAttr[0].type == "text") {
                    $(listAttr[0]).val(item.ValueInt == null ? item.ValueDecimal : item.ValueInt);
                } else if (listAttr[0].type == "number") {
                    $(listAttr[0]).val(item.ValueInt == null ? item.ValueDecimal : item.ValueInt);
                } else if (listAttr[0].type == "date") {
                    $(listAttr[0]).val(item.ValueDate);
                } else if (listAttr[0].type == "checkbox") {
                    for (var i = 0; i < listAttr.length; i++) {
                        if ($(listAttr[i]).val() == item.AttributeId) {
                            $(listAttr[i]).attr('checked', 'checked');
                        }
                    }
                } else if (listAttr[0].type == "radio") {
                    for (var i = 0; i < listAttr.length; i++) {
                        if ($(listAttr[i]).val() == item.AttributeId) {
                            $(listAttr[i]).attr('checked', 'checked');
                        }
                    }
                }
            }

            if (listAttr[0].tagName.toLowerCase() == "select") {
                $(listAttr[0]).val(item.AttributeId);
            }
        }
    });
}

//Load all data
function FillAdvertData(callback) {
    LoadState();
    if (NewUI.data.advert.Id > 0) {
        $('.cusEditImg').removeClass('hidden');
        $('#hdAdvertID').val(NewUI.data.advert.Id);
        $('.hidAdvertIdNewui').text(NewUI.data.advert.Id);
    } else {
        $('.cusEditImg').addClass('hidden');
    }

    if (NewUI.data.advert.TypeId == 1) {
        $("input[name='optionsRadiosInline'][value='1']").attr("checked", "checked");
    } else if (NewUI.data.advert.TypeId == 2) {
        $("input[name='optionsRadiosInline'][value='2']").attr("checked", "checked");
    }

    var parentId = NewUI.data.advert.LeafCategory.ParentId;
    var categoryId = NewUI.data.advert.LeafCategory.Id;

    LoadCategory(NewUI.data.advert.TypeId);
    $('#ddlCategory').val(parentId);
    LoadSubCategory(parentId)
    $('#ddlSubCategory').val(categoryId);

    LoadCategoryAttribites(parentId, function () {
        FillAttributes();
    });

    $('#txtTitle').val(NewUI.data.advert.Title);
    $('#txtShortDescription').val(NewUI.data.advert.ShortDescription.length >= 160 ? NewUI.data.advert.ShortDescription.toString().substr(0, 160) : NewUI.data.advert.ShortDescription);
    $('#txtDescription').val(NewUI.data.advert.Description);

    ////////////

    $(NewUI.data.districts).each(function (i, data) {
        $('#ddlGeoDistrict').append("<option value=" + data.Id + ">" + data.Name + "</option>");
    });
    $(NewUI.data.wards).each(function (i, data) {
        $('#ddlGeoWard').append("<option value=" + data.Id + ">" + data.Name + "</option>");
    });
    $(NewUI.data.projects).each(function (i, data) {
        $('#ddlProject').append("<option value=" + data.Id + ">" + data.Name + "</option>");
    });

    var location = NewUI.data.advert.AddressLocation || {};
    $('#txtPhone1').val(location.PhonePrivate);
    $('#pac-input').val(location.Street);
    $('#ddlGeoState').val(location.StateId);
    $('#ddlGeoDistrict').val(location.DistrictId);
    $('#ddlGeoWard').val(location.LocationId);
    $('#ddlProject').val(NewUI.data.advert.ProjectId);
    $('#txtPhone2').val(location.Phone2);

    $('#ddlProject').selectpicker();
    $('.glyphicon').removeClass('glyphicon-ok');

    $('.cusEditImg').html("");
    $('#LoadImgUpload').tmpl(NewUI.data.advert.DetailImages).appendTo('.cusEditImg');

    setTimeout(function () {
        $('.dropzone').each(function () {
            if ($('input[type=radio]', this).length > 0) $('input[type=radio]', this).get(0).checked = true;
        });

        var latlng = new google.maps.LatLng(location.Latitude, location.Longitude);
        removeMarkers();
        markers.setPosition(latlng);
        markers.setMap(map);
        map.setCenter(markers.getPosition());
        map.setZoom(MAP_ZOOM.address);
    }, 150);

    var indexPhone = location.indexPhone || 1;
    if (indexPhone == 2) {
        $('#radioDT2').prop('checked', true);
    } else {
        $('#radioDT1').prop('checked', true);
    }

    //Load map
    initMap(function () {
        UpdateMapDisplayByLocation(location);
    });

    if (callback) {
        callback();
    }
}

//Please don't change method name below, it will be called by google
function initMap(callback) {
    if (map != null) {
        if (callback) callback();
        return;
    }

    try {
        var mapid = document.getElementById('map');
        map = new google.maps.Map(mapid, {
            zoom: 9,
            center: { lat: 10.76025245, lng: 106.689502 },
            mapTypeId: google.maps.MapTypeId.ROADMAP
        });
        markers = new google.maps.Marker({
            map: map,
            draggable: true,
            animation: google.maps.Animation.DROP,
            position: { lat: 10.76025245, lng: 106.689502 }
        });

        var input = document.getElementById('pac-input');

        var searchBox = new google.maps.places.SearchBox(input);

        ////// Bias the SearchBox results towards current map's viewport.
        map.addListener('bounds_changed', function () {
            searchBox.setBounds(map.getBounds());
        });

        //// Listen for the event fired when the user selects a prediction and retrieve
        //// more details for that place.
        searchBox.addListener('places_changed', function () {
            var places = searchBox.getPlaces();

            var address_street = places[0].address_components[0].long_name; var address_street_long = places[0].address_components[1].long_name;
            var address = address_street + ' ' + address_street_long;
            $('#pac-input').val(address);

            if (places.length == 0) {
                return;
            }
            var bounds = new google.maps.LatLngBounds();
            places.forEach(function (place) {
                markers.setPosition(place.geometry.location);

                markers.setMap(map);
                if (place.geometry.viewport) {

                    bounds.union(place.geometry.viewport);
                } else {
                    bounds.extend(place.geometry.location);
                }
            });
            map.fitBounds(bounds);
            $('#lat').val(markers.getPosition().lat());
            $('#lng').val(markers.getPosition().lng());
            map.panTo(markers.getPosition());
        });

        if (callback) {
            callback();
        }
    } catch (e) {

    }
}

function UpdateMapDisplayByLocation(location) {
    try {
        if (mappo && mappo.setMap) mappo.setMap(null);

        if (location.StateId > 0 && location.DistrictId > 0 && location.LocationId > 0) {
            for (var i = 0; i < NewUI.data.wards.length; i++) {
                var ward = NewUI.data.wards[i];
                if (ward.Id == location.LocationId) {
                    UpdateMapDisplay(ward.Lat, ward.Lng, MAP_ZOOM.ward, ward.Polygon);
                    break
                }
            }
        } else if (location.StateId > 0 && location.DistrictId > 0) {
            for (var i = 0; i < NewUI.data.districts.length; i++) {
                var district = NewUI.data.districts[i];
                if (district.Id == location.DistrictId) {
                    UpdateMapDisplay(district.Lat, district.Lng, MAP_ZOOM.district, district.Polygon);
                    break
                }
            }
        } else if (location.StateId > 0) {
            for (var i = 0; i < NewUI.data.states.length; i++) {
                var state = NewUI.data.states[i];
                if (state.Id == location.StateId) {
                    UpdateMapDisplay(state.Lat, state.Lng, MAP_ZOOM.state, state.Polygon);
                    break
                }
            }
        } else {
            UpdateMapDisplay(HCM_GEO.lat, HCM_lng, 9, HCM_GEO.polygon);
        }
    } catch (e) {
        initMap();
    }
}

function UpdateMapDisplay(lat, lng, zoom, polygon) {
    if (lat && lng) {
        setTimeout(function () {
            var latlng = new google.maps.LatLng(lat, lng);
            //removeMarkers();
            map.setCenter(latlng);
            markers.setPosition(latlng);
            markers.setMap(map);

            markers.addListener('click', toggleBounce);
            google.maps.event.addListener(markers, 'dragend', function (evt) {
                $('#lat').val(evt.latLng.lat());
                $('#lng').val(evt.latLng.lng());
            });
            $('#lat').val(markers.getPosition().lat());
            $('#lng').val(markers.getPosition().lng());
            map.panTo(markers.getPosition());

            if (polygon && polygon.length > 0) {
                mappo = new google.maps.Polygon({
                    paths: polygon,
                    strokeColor: '#FF0000',
                    strokeOpacity: 0.9,
                    strokeWeight: 1,
                    fillColor: 'red',
                    fillOpacity: 0.1
                });
                mappo.setMap(map);
            }

            map.setZoom(zoom);
            infoWindow = new google.maps.InfoWindow;
        }, 70);
    }
}

function toggleBounce() {
    if (markers.getAnimation() !== null) {
        markers.setAnimation(null);
    } else {
        markers.setAnimation(google.maps.Animation.BOUNCE);
    }
}
function removeMarkers() {
    for (i = 0; i < markers.length; i++) {
        markers[i].setMap(null);
    }
}
function showArrays(event) {
    // Since this polygon has only one path, we can call getPath() to return the
    // MVCArray of LatLngs.
    var vertices = this.getPath();

    var contentString = '<b>Bermuda Triangle polygon</b><br>' +
        'Clicked location: <br>' + event.latLng.lat() + ',' + event.latLng.lng() +
        '<br>';

    // Iterate over the vertices.
    for (var i = 0; i < vertices.getLength() ; i++) {
        var xy = vertices.getAt(i);
        contentString += '<br>' + 'Coordinate ' + i + ':<br>' + xy.lat() + ',' +
            xy.lng();
    }

    // Replace the info window's content and position.
    infoWindow.setContent(contentString);
    infoWindow.setPosition(event.latLng);

    infoWindow.open(map);
}

function initAutocomplete() {

}

////Insert Advert
function InsertAdvert() {
    $('.g-recaptcha-response').val(1);

    var t = 0;
    if ($('#ddlCategory').val() == "0") {
        toastr.error('Ch?n danh m?c ');
        t++;
    }

    if ($('#ddlSubCategory').val() == "0") {
        toastr.error('Ch?n danh m?c ');
        t++;
    }

    if ($('#ddlGeoState').val() == "0") {
        toastr.error('Ch?n thành ph?/ T?nh thành');
        t++;
    }

    if ($('#ddlGeoDistrict').val() == "0") {
        toastr.error('Ch?n Qu?n/ Huy?n ');
        t++;
    }

    if ($('#ddlGeoWard').val() == "0") {
        toastr.error('Ch?n Ph??ng ');
        t++;
    }

    if ($('#txtTitle').val() == "") {
        toastr.error('Vui lòng nh?p tiêu ??!');
        t++;
    }

    if ($('#txtShortDescription').val() == "") {
        toastr.error('Vui lòng nh?p mô t?');
        t++;
    }

    if ($('#txtDescription').val() == "") {
        toastr.error('Vui lòng nh?p mô t? chi ti?t!');
        t++;
    }

    if ($('#txtDescription').val().length >= 4000) {
        toastr.error('Mô t? chi ti?t ch? t?i ?a 4000 kí t? ');
        t++;
    }

    if ($('#grp_176').val() <= 1) {
        toastr.error('Di?n tích ph?i l?n h?n 1!');
        t++;
    }

    if ($('.dz-preview').size() == 0) {
        toastr.error('Vui lòng ch?n ít nh?t m?t hình!');
        t++;
    } else {
        $('.lblimg').addClass('hidden');
    }

    if ($('#ck_13845').prop('checked') == false) {
        if ($('#grp_1').val() <= 0) {
            toastr.error('Vui lòng nh?p giá!');
            t++;
        }
        if ($('#grp_1').val() > 1000000000) {
            toastr.error('Giá ti?n ch?a h??p lê?. Giá ti?n h?p l? nên t?? 0 ?ê?n 1.000.000');
            t++;
        }
    }

    if ($('#radioDT2').prop('checked') == true) {
        if ($('#txtPhone2').val() == '') {

            toastr.error('Vui lòng nh?p s? ?i?n tho?i 2!');
            t++;
        }
        if ($('#txtPhone2').length > 11) {

            toastr.error('S? ?i?n tho?i t?i ?a 11 s?!');
            t++;
        }
    }

    if ($('#txtPhone2').val() != "") {
        if ($('#txtPhone2').length > 11) {

            toastr.error('S? ?i?n tho?i t?i ?a 11 s?!');
            t++;
        }
    }

    if (t == 0) {
        NProgress.start();
        if (ga) {
            ga('send', 'event', 'draft ad', 'draft listing but not yet put live', $('#ddlCategory option:selected').text(), 0, {
                nonInteraction: true
            });
        }

        $('.btnNextNewUi').addClass('disabled');
        $('.btnNextNewUi').text('?ang x? lý...');

        ////////////// MEMBERSHIP & PROMOTION ////////////////////////
        //if ($('.configMembership').val() == "1") {
        //    loadMembership();
        //    if ($('#hidFagMembership').val() == 1) {
        //        membershipChangeCondition($("#switch-membership").bootstrapSwitch('state'));
        //    }
        //}
        //if ($('.configPromotion').val() == "1") {
        //    loadPromotion();
        //}
        ////////////// END MEMBERSHIP & PROMOTION ////////////////////

        var idAdvertCurrent = $('.hdiDAdvertCurrent').val();
        if (idAdvertCurrent > 0) {
            $('.hidAdvertIdNewui').text(idAdvertCurrent);
            $('.cusbtnNext').find("[id*=btnextstep]").click();
        } else {
            $.post('/Services/LoadNewUi.ashx', $(document.getElementById("ctl01")).serialize(), function (respone) {
                $('.btnNextNewUi').removeClass('disabled');
                $('.btnNextNewUi').text('B??c ti?p theo');
                $('.hdiDAdvertCurrent').val(respone.advertID);

                if (advertId >= 0) {
                    $('.hidAdvertIdNewui').text(advertId);
                    $('.cusbtnNext').find("[id*=btnextstep]").click();
                } else {
                    $('.hidAdvertIdNewui').text(respone.advertID);
                    $('.cusbtnNext').find("[id*=btnextstep]").click();
                }

                $('.hdCategoryID').val(respone.categoryID);
                $('.hdCategoryName').val(respone.categoryName);

                $.cookie('cate', $('#ddlCategory').val());
                $.cookie('subCateID', $('#ddlSubCategory').val());
                $.cookie('stateID', $('#ddlGeoState').val());
                $.cookie('districtID', $('#ddlGeoDistrict').val());

                var radio = $("input[type='radio'][name='optionsRadiosInline']:checked").val();
                $.cookie('radioBan', radio);
            });
        }

        $('#action_type').val('InsertAdvert');
    }
}

/////Load Attributes
function LoadCategoryAttribites(categoryId) {
    //alert("Hi");
    $.getJSON('/Services/LoadNewUi.ashx', { type: 'GetAtrribites', id: categoryId }, function (respone) {
        var innerHtml = "";
        $.each(respone, function (i, data) {

            var content = "";
            var template = "";
            if (data.Unit["vi"] != "") {
                if (data.Name["vi"] == "Giá" || data.ControlId == "grp_176") {
                    template = "<div class='col-lg-6'><div class='row'><div class='col-lg-6'><div class='form-group'><label class='cuslable'>@NameLabel <span class=\"star\">*</span> </label></div></div><div class='col-lg-6'><div class=' form-group input-group'>@Control</div></div></div></div>";
                } else {
                    template = "<div class='col-lg-6'><div class='row'><div class='col-lg-6'><div class='form-group'><label class='cuslable'>@NameLabel </label></div></div><div class='col-lg-6'><div class=' form-group input-group'>@Control</div></div></div></div>";
                }

                //"<div class='col-lg-6'><div class='row'><div class='col-lg-6'><div class='form-group'><label class='cuslable'>@NameLabel (" + data.Unit["vi"] + ")</label></div></div><div class='col-lg-6'><div class='input-group'>@Control</div></div></div></div>";
            }
            else {
                template = "<div class='col-lg-6'><div class='row'><div class='col-lg-6'><div class='form-group'><label class='cuslable'>@NameLabel </label></div></div><div class='col-lg-6'><div class='form-group '>@Control</div></div></div></div>";
            }

            template = template.replace('@NameLabel', data.Name["vi"]);
            switch (data.TypeId) {
                case 1:

                    content += "<select class='form-control form-control-thh dropdown' name=\"" + data.ControlId + "\" ><option value=\"0\">Ch?n</option>";
                    for (var j = 0; j < data.Attributes.length; j++) {
                        var op = "<option value=\"" + data.Attributes[j].Id + "\">" + data.Attributes[j].Name["vi"] + "</option>";
                        content += op;
                    }
                    content += "</select>";
                    break;
                case 2:
                    if (data.Unit["vi"] != "") {
                        template = "<div class='col-lg-12'><div class='row'><div class='col-lg-3'><div class='form-group'><label class='cuslable'>@NameLabel" + data.Unit["vi"] + "</label></div></div><div class='col-lg-9'><div class='well NewUiWellWhite NewUiBorder'><div class='row'>@Control</div></div></div></div></div>";

                    } else {
                        template = "<div class='col-lg-12'><div class='row'><div class='col-lg-3'><div class='form-group'><label class='cuslable'>@NameLabel </label></div></div><div class='col-lg-9'><div class='well NewUiWellWhite NewUiBorder'><div class='row'>@Control</div></div></div></div></div>";
                    }


                    template = template.replace('@NameLabel', data.Name["vi"]);


                    for (var j = 0; j < data.Attributes.length; j++) {

                        content += "<div class='col-lg-4'><div class='checkbox'><label><input type='checkbox' style=\"margin-top: 2px;\" value='" + data.Attributes[j].Id + "' name=\"" + data.ControlId + "\" id=\"" + data.Attributes[j].Id + "\" >" + data.Attributes[j].Name["vi"] + "</label></div></div>";
                    }

                    break;
                case 3:
                    content += "<select class='form-control form-control-thh dropdown' name=\"" + data.ControlId + "\" ><option value=\"0\">Ch?n</option>";
                    for (var j = 0; j < data.Attributes.length; j++) {
                        var op = "<option value=\"" + data.Attributes[j].Id + "\">" + data.Attributes[j].Name["vi"] + "</option>";
                        content += op;
                    }
                    content += "</select>";
                    break;
                case 4:
                    content += "<input class='form-control' type=\"text\" name=\"" + data.ControlId + "\"  value=\"\">";
                    break;
                case 5:
                    content += "<input class='form-control' type=\"text\" name=\"" + data.ControlId + "\"  value=\"\">";
                    break;
                case 6:
                    if (data.Attributes[0].Range != null) {
                        content += "<input type=\"number\" placeholder=\"0\" class='form-control inputnumber' min=\"" + data.Attributes[0].Range.MinValue + "\" max=\"" + data.Attributes[0].Range.MaxValue + "\" step=\"" + data.Attributes[0].Range.StepValue + "\" name=\"" + data.ControlId + "\"  value=\"\">";
                    } else {
                        content += "<input type=\"text\" class='form-control inputnumber'   name=\"" + data.ControlId + "\"  value=\"\">" + data.Name["vi"] + "";
                    }
                    break;
                case 7:
                    if (data.Attributes[0].Range != null) {
                        if (data.Unit["vi"] != "") {
                            if (data.ControlId == "grp_1") {
                                content += "<input type=\"text\" placeholder=\"0\"  class='form-control form-control-thh inputnumber' maxlength='10' min=\"" + data.Attributes[0].Range.MinValue + "\" max=\"" + data.Attributes[0].Range.MaxValue + "\" step=\"" + data.Attributes[0].Range.StepValue + "\" id=\"" + data.ControlId + "\" value=\"\"><span class=\"input-group-addon cuslabefont\" style=\"border: 0 solid #ed1c24!important;\">" + data.Unit["vi"] + "</span><div class='col-lg-offset-3 col-lg-8 money-text'></div>";
                            } else {
                                content += "<input type=\"number\" placeholder=\"0\" class='form-control form-control-thh inputnumber' min=\"" + data.Attributes[0].Range.MinValue + "\" max=\"" + data.Attributes[0].Range.MaxValue + "\" step=\"" + data.Attributes[0].Range.StepValue + "\" name=\"" + data.ControlId + "\" id=\"" + data.ControlId + "\" value=\"\"><span class=\"input-group-addon cuslabefont\" style=\"border: 0 solid #ed1c24!important;\">" + data.Unit["vi"] + "</span>";
                            }
                        } else {
                            content += "<input type=\"text\"  class='form-control inputnumber' min=\"" + data.Attributes[0].Range.MinValue + "\" max=\"" + data.Attributes[0].Range.MaxValue + "\" step=\"" + data.Attributes[0].Range.StepValue + "\" name=\"" + data.ControlId + "\" id=\"" + data.ControlId + "\" placeholder=\"0\" value=\"\">";
                        }

                    } else {
                        if (data.Unit["vi"] != "") {
                            content += "<input class='form-control inputnumber' type=\"text\" name=\"" + data.ControlId + "\"  value=\"\">" + data.Unit["vi"];
                        } else {
                            content += "<input class='form-control inputnumber' type=\"text\" name=\"" + data.ControlId + "\"  value=\"\">";
                        }

                    }
                    break;
                case 8:
                    content += "<label><input class='form-control' type=\"date\" name=\"" + data.ControlId + "\"  value=\"\">";
                    break;
                case 9:
                    template = "<div class='col-lg-6'><div class='row'><div class='col-lg-6'><div class='form-group'><label class='cuslable'>&nbsp;</label></div></div><div class='col-lg-6'><div class='form-group'>@Control</div></div></div></div>";
                    if (data.Attributes.length == 1) {
                        content += "<label class='checkbox-inline' style=\"font-size:13px;\"><input style=\"margin-top: 2px;\" type=\"checkbox\" id=\"ck_" + data.Attributes[0].Id + "\"  name=\"" + data.ControlId + "\"  value=\"" + data.Attributes[0].Id + "\">" + data.Name["vi"] + "</label>";

                    } else {

                        for (var j = 0; j < data.Attributes.length; j++) {

                            content += "<label class='checkbox-inline'><input type=\"checkbox\" style=\"margin-top: 2px;\" id=\"ck_" + data.Attributes[0].Id + "\" name=\"" + data.ControlId + "\"  value=\"" + data.Attributes[0].Id + "\">" + data.Attributes[0].Name["vi"] + "</label>";

                        }
                    }

                    break;

            }
            template = template.replace('@Control', content);
            innerHtml += template;
        });

        if (respone.length > 10) {
            $('.extra-blank').removeClass('hidden');
        }
        else {
            $('.extra-blank').addClass('hidden');
        }

        $('.taka').html(innerHtml);

        $('#loadControl').html("");
        $.each(respone, function (i, data) {
            var tml = '<tr><th class="col-xs-5">@NameLabel</th><td class="col-xs-7"><label class=\"cuslabefont\" id="@Name_Data" data-type="@Type" data-ref="@Name" class="updateValue lblLegalStatus"></label></td></tr>';
            tml = tml.replace('@NameLabel', data.Name["vi"]);
            tml = tml.replace('@Name', data.ControlId);
            tml = tml.replace('@Name', data.ControlId);
            tml = tml.replace('@Type', data.TypeId);
            $('#loadControl').append(tml);
            $('#' + data.ControlId + "_Data").data(data.Attributes);
        });

        $('.inputnumber').keydown(function (e) {

            // Allow: backspace, delete, tab, escape, enter and .
            if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190, 188]) !== -1 ||
                // Allow: Ctrl+A, Command+A
                (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                // Allow: home, end, left, right, down, up
                (e.keyCode >= 35 && e.keyCode <= 40) || (e.keyCode == 231)) {
                // let it happen, don't do anything
                return;
            }

            // Ensure that it is a number and stop the keypress
            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                e.preventDefault();
            }
        });

        if (advertId > 0) {
            FillAttributes();
        }

        $('#ck_13845').on('change', function () {
            if ($(this).is(':checked')) {
                $('#grp_1').attr('disabled', 'disable');
                $('#grp_1').val(0);
                $('#hidXPrice').val(0);
                $('.money-text').text('');
            } else {
                $('#grp_1').val($('#hidXPriceTemp').val());
                setTextMoneyAllowDecimal($('#grp_1'));
                $('#grp_1').removeAttr('disabled');
            }
        });

        eventAfterLoadAttributes();
    });
}

function UpdateValue() {
    $('.updateValue').each(function () {
        var name = $(this).attr('data-ref');
        var type = parseInt($(this).attr('data-type'));

        var text = "";
        var el = document.getElementsByName(name);
        for (var i = 0; i < el.length; i++) {
            var elx = el[i];
            var ix = 0;
            switch (type) {
                case 1:
                    break;
                case 2:
                    if ($(this).data() != null) {


                        while (true) {
                            if ($(this).data()[ix] == null) {
                                break;;
                            }
                            if (elx.checked == true && elx.value == $(this).data()[ix].Id) {
                                text += $(this).data()[ix].Name["vi"] + ", ";

                            }
                            ix++;
                        }

                    }
                    break;
                case 3:
                    if ($(this).data() != null) {


                        while (true) {
                            if ($(this).data()[ix] == null) {
                                break;;
                            }
                            if ($(elx).val() == $(this).data()[ix].Id) {
                                text += $(this).data()[ix].Name["vi"] + ", ";

                            }
                            ix++;
                        }

                    }
                    break;
                case 4:
                    text += $(elx).val();
                    break;
                case 5:
                    text += $(elx).val();
                    break;
                case 6:
                    text += $(elx).val();
                    break;
                case 7:
                    text += $(elx).val();
                    break;
                case 8:
                    text += $(elx).val();
                    break;
                case 9:
                    if ($(this).data() != null) {


                        while (true) {
                            if ($(this).data()[ix] == null) {
                                break;;
                            }
                            if (elx.checked == true && elx.value == $(this).data()[ix].Id) {
                                text += $(this).data()[ix].Name["vi"] + ", ";

                            }
                            ix++;
                        }

                    }
                    break;
            }

        }
        $(this).text(text);


    });
}

////Laod Result Listting  
function LoadResultListing() {
    ToTop();
    $('.vnexpress').addClass('hidden');

    if (NewUI.data.advert.Id > 0) {
        $('.radioNoChange').removeClass('hidden');
        document.getElementById("radio_12").checked = true;
    } else {
        $('.radioNoChange').addClass('hidden');

        if (document.getElementById("radio_9").checked = true) {
            $('.NewUiProduct-TopListing').addClass('Cusopacity');
            $('.NewUiProduct-PreListing').addClass('Cusopacity');
            $('.NewUiProduct-freeListing').removeClass('Cusopacity');
            $('.NewUiProduct-expressListing').addClass('Cusopacity');
        }

        var rd = $('input[name="list"]:checked').val();
        // Promotion check
        if ($('input[name="list"]:checked').attr('attr-product') == "11"
            && $('.promotion-check').attr('attr-top') == 1) {
            rd = 0;
        }

        $('.usertotalCredit').text(rd);

        TotalCredit(rd);
        GetTypeProductSelected(9);
    }

    $.getJSON('/Services/LoadNewUi.ashx', {
        type: 'LoadResultListing',
        id: $('.hidAdvertIdNewui').text(),
        _: $.now()

    }, function (respone) {
        var id = $('.hidAdvertIdNewui').text();
        $('.loadTitleTopLissting').html(respone.result.Title);
        $('.loadImgTopLissting').attr('style', "background:url(" + respone.result.ImgHost + respone.imgname + ")no-repeat; border-radius: 2px 2px 2px 2px;");
        $('.loadPrice').text(respone.result.Price);
        $('.loadAddressStreet').text(respone.result.AddressStreet);
        $('.loadAddressCity').text(respone.result.AddressCity);
        $('.loadcountImg').text(respone.datacount);
        $('.loadRooms').text(respone.result.Rooms);
        $('.loadSurface').text(respone.result.Surface);
        $('.loadShortDescription').text(respone.result.ShortDescription.length >= 150 ? respone.result.ShortDescription.toString().substr(0, 150) : respone.result.ShortDescription);

        $('.loadIdAdvert').text(respone.result.Id);
        $('.loadDescription').text(respone.result.Description);
        $('.hdimahost').text(respone.result.ImgHost);
        $('.loadDatePosted').text(respone.result.DatePosted);
        $('.loadDateRefreshed').text(respone.result.DateRefreshedFormatDate);
        $('.loadIdAdvert').text(respone.result.id);

        if (respone.list != "") {
            $('.cuslabelshowinfor').text(respone.list[0].nameProduct);
            $('.cusShowDateStart').text(respone.list[0].dateS);
            $('.cusShowDateEnd').text(respone.list[0].dateE);
            if (respone.list[0].idpro == 9) {
                //document.getElementById("radio_9").checked = true;
                $('.NewUiProduct-TopListing').addClass('Cusopacity');
                $('.NewUiProduct-PreListing').addClass('Cusopacity');
                $('.NewUiProduct-freeListing').removeClass('Cusopacity');
                $('.NewUiProduct-expressListing').addClass('Cusopacity');
            }

            if (respone.list[0].idpro == 10) {
                $('.NewUiProduct-TopListing').addClass('Cusopacity');
                $('.NewUiProduct-PreListing').removeClass('Cusopacity');
                $('.NewUiProduct-freeListing').addClass('Cusopacity');
                $('.NewUiProduct-expressListing').addClass('Cusopacity');
            }

            if (respone.list[0].idpro == 11) {
                $('.NewUiProduct-TopListing').removeClass('Cusopacity');
                $('.NewUiProduct-PreListing').addClass('Cusopacity');
                $('.NewUiProduct-freeListing').addClass('Cusopacity');
                $('.NewUiProduct-expressListing').addClass('Cusopacity');
            }

            if (respone.list[0].idpro == 35) {
                $('.NewUiProduct-TopListing').addClass('Cusopacity');
                $('.NewUiProduct-PreListing').addClass('Cusopacity');
                $('.NewUiProduct-freeListing').addClass('Cusopacity');
                $('.NewUiProduct-expressListing').removeClass('Cusopacity');
            }
            if (respone.list[0].idpro == 39) {
                $('.NewUiProduct-TopListing').removeClass('Cusopacity');
                $('.NewUiProduct-PreListing').addClass('Cusopacity');
                $('.NewUiProduct-freeListing').addClass('Cusopacity');
                $('.NewUiProduct-expressListing').addClass('Cusopacity');
            }
            if (respone.list[0].idpro == 38) {
                $('.NewUiProduct-TopListing').addClass('Cusopacity');
                $('.NewUiProduct-PreListing').removeClass('Cusopacity');
                $('.NewUiProduct-freeListing').addClass('Cusopacity');
                $('.NewUiProduct-expressListing').addClass('Cusopacity');
            }
        } else {
            document.getElementById("radio_9").checked = true;
            $('.radioNoChange').addClass('hidden');

            if (document.getElementById("radio_9").checked == true) {
                $('.NewUiProduct-TopListing').addClass('Cusopacity');
                $('.NewUiProduct-PreListing').addClass('Cusopacity');
                $('.NewUiProduct-freeListing').removeClass('Cusopacity');
                $('.NewUiProduct-expressListing').addClass('Cusopacity');
            }

            var rd = $('input[name="list"]:checked').val();
            // Promotion check
            if ($('input[name="list"]:checked').attr('attr-product') == "11" && $('.promotion-check').attr('attr-top') == 1) {
                rd = 0;
            }
            $('.usertotalCredit').text(rd);

            TotalCredit(rd);
            GetTypeProductSelected(9);
        }
    });
}

//////Get Detail
function GetDetail() {
    $.getJSON('/Services/LoadNewUi.ashx', {
        type: 'GetDetail',
        id: $('.hidAdvertIdNewui').text(), _: $.now()

    }, function (respone) {

        NProgress.set(100);
        var lblMap = "<a href='https://maps.google.com/?q=loc:" + respone.lat + "," + respone.log + "' target='_blank'><div class='icon map-icon spacer-right-5'></div>Hiê?n thi? trên b?n ??</a>";

        $('.loadContactPhone').html(respone.phone);

        $('.loadContactPhoneSticky').html(respone.phone);
        $('.loadContactName').text(respone.name);
        $('.address-contact').text(respone.address);
        $('.lblMapLink').html(lblMap);
        $('.lblStreet').html(respone.street);
        var contentWard = "<a href='" + respone.links + "'>" + respone.Ward + "</a>";
        $('.lblWard').html(contentWard);
        var contentDistrict = "<a href='" + respone.linkdistrict + "'>" + respone.District + "</a>";
        $('.lblDistrict').html(contentDistrict);
        var contentCity = "<a href='" + respone.linkstate + "'>" + respone.State + "</a>";
        $('.lblCity').html(contentCity);
        $('#loadTitledetail').text(respone.title);
        $('.loadTitledetail').text(respone.title);
        $('#loadShort').text(respone.shortdescription.length >= 150 ? respone.shortdescription.toString().substr(0, 150) : respone.shortdescription);
        $('.loadDescription').text(respone.description);
        $('.loadpostDate').text(respone.postDate);
        $('.loadRefreshDate').text(respone.RefreshDate);
        $('#lblPrice').text(respone.lPrice);
        $('#lblPriceCustom').text(respone.PriceCustom);
        $('#lblLegalStatus').text(respone.LegalStatus);
        $('#lblSurface').text(respone.Surface);
        $('#lblFengShuiDirection').text(respone.FengShuiDirection);
        $('#lblFrontRoadWidth').text(respone.FrontRoadWidth);
        $('#lblFloor').text(respone.Floor);
        $('#lblBedRoom').text(respone.BedRoom);
        $('#lblBathRoom').text(respone.BathRoom);
        $('#lblProject').html(respone.lbllink);
        $('#lblStatus').text(respone.LegalStatus);
        $('#lblUtility').html(respone.Utility);
        $('#lblEnvironment').html(respone.Environment);
        $('.cusseoCate').text(respone.cate);
        var contentShareFb = "";
        contentShareFb += "<a   id=\"change\"><div class=\"col-xs-4 share-facebook\" ></div></a>";
        contentShareFb += "<a   id=\"changegoogle\"><div class=\"col-xs-4 share-google\"></div></a>";
        contentShareFb += " <a  target=\"_blank\" id=\"chageemail\"><div class=\"col-xs-4 share-mail\" ></div></a>";

        $('.loadShareFB').html(contentShareFb);

        $('.loadShareFbS').html("<a target=\"_blank\" href=\"" + respone.shareFb + "\" class=\"btnNewUi  btn-success-outline1 pull-right\" ></a>");
        $('.loadShareGgS').html("<a target=\"_blank\" href=\"" + respone.shareGg + "\" class=\"btnNewUi  btn-success-outline2\" ></a>");

        $('.loadFb').html("<a  class=\"btnFB1\">  <img src=\"/img/svg/ic-facebook.png\"/></a>");
        $('.loadGg').html("<a   class=\"btnFB1\">  <img src=\"/img/svg/ic-gplus.png\"/></a>");

        if (respone.list != null) {
            $('.loadRe').removeClass('hidden');
            $('.loadRe').html("");
            $('#loadListRe').tmpl(respone.list).appendTo('.loadRe');
        } else {
            $('.loadRe').addClass('hidden');
        }
    });
}

function Picture() {
    $.getJSON('/Services/LoadNewUi.ashx', {
        type: 'Picture',
        id: $('.hidAdvertIdNewui').text()
    }, function (respone) {

        var content = "";

        $.each(respone.detailImg, function (i, data) {
            content += "<li data-thumb=\"" + $('.hdimahost').text() + data.tiny + "\">" +
                "<a href=\"" + $('.hdimahost').text() + data.large + "\" class=\"swipebox\" title=\"" + respone.dataAdvert + "\" rel=\"" + respone.dataAdvert + "\">" +
                "<img src=\"" + $('.hdimahost').text() + data.medium + "\" alt=\"" + respone.dataAdvert + "\" />" + " </a></li>";
        });

        $('.loadslides').html(content);
        setTimeout(function () {
            eventSlider();
        }, 100);
    });
}


function GetTypeProduct(stateId, callback) {
    if (NewUI.products[stateId]) {
        InsertProductsToHtml(NewUI.products[stateId]);
        if (callback) callback();
    } else {
        $.getJSON('/Services/LoadNewUi.ashx', {
            type: 'GetTypeProduct', id: stateId, advertId: NewUI.data && NewUI.data.advert && NewUI.data.advert.Id
        }, function (respone) {
            NewUI.products[stateId] = respone;
            InsertProductsToHtml(NewUI.products[stateId]);
            if (callback) callback();
        });
    }
}

function InsertProductsToHtml(respone) {
    $('.loadTopListing').html('');
    $('#LoadTypeList').tmpl(respone).appendTo('.loadTopListing');
    $('.cuslbl_11').css('font-weight', 'bold');
    GetCreditProduct();
    var radio9 = 0;
    var radio10 = 0;
    var radio11 = 0;
    var radio35 = 0;
    var radio12 = 0;
    var radio36 = 0;//vnexpress
    var radio38 = 0;
    var radio39 = 0;
    var cbvnepressIntro = false;
    TotalCredit(radio9);
    //basic id :#radio_9
    ///only show vnexpress checkbox when radio_9 selected 
    $('input[name="list"]').change(function (e) {
        if ($(this).attr('id') == 'radio_9') {
            $('.vnepressIntro').prop('disabled', false);
        } else {
            $('.vnepressIntro').prop('disabled', true);
        }
    });

    $('.vnepressIntro').change(function (e) {
        if ($(this).is(':checked')) {
            radio36 = $('.vnepressIntro').val();

            $('.cuslbl_9.NewUiprice').text(parseInt(radio36).format(0, 3, '.') + ' ?');

            $('.usertotalCredit').text(parseInt(radio36).format(0, 3, '.'));
            TotalMainAccount(radio36);
            GetTypeProductSelected(36);
            $('.NewUiProduct-TopListing').addClass('Cusopacity');
            $('.NewUiProduct-PreListing').addClass('Cusopacity');
            $('.NewUiProduct-freeListing').removeClass('Cusopacity');
            $('.NewUiProduct-expressListing').addClass('Cusopacity');
        } else {
            $('.cuslbl_9.NewUiprice').text($('input[name="list"]:checked').val() + ' ?');
            $('#radio_9').trigger(('change'));
        }
    });

    $('#radio_9').change(function () {
        if ($('.vnepressIntro').is(':checked')) {
            $('.vnepressIntro').trigger(('change'));
        } else {
            radio9 = $('input[name="list"]:checked').val();
            $('.usertotalCredit').text(radio9);
            TotalCredit(radio9);
            GetTypeProductSelected(9);
            $('.NewUiProduct-TopListing').addClass('Cusopacity');
            $('.NewUiProduct-PreListing').addClass('Cusopacity');
            $('.NewUiProduct-freeListing').removeClass('Cusopacity');
            $('.NewUiProduct-expressListing').addClass('Cusopacity');
        }
    });

    $('#radio_10').change(function () {
        radio10 = $('input[name="list"]:checked').val();
        // Promotion check
        if ($('input[name="list"]:checked').attr('attr-product') == "10" && $('.promotion-check').attr('attr-premium') == 1) {
            radio10 = 0;
        }
        $('.usertotalCredit').text(radio10);
        TotalCredit(radio10);
        GetTypeProductSelected(10);
        $('.NewUiProduct-TopListing').addClass('Cusopacity');
        $('.NewUiProduct-PreListing').removeClass('Cusopacity');
        $('.NewUiProduct-freeListing').addClass('Cusopacity');
        $('.NewUiProduct-expressListing').addClass('Cusopacity');
    });

    $('#radio_11').change(function () {
        radio11 = $('input[name="list"]:checked').val();
        // Promotion check
        if ($('input[name="list"]:checked').attr('attr-product') == "11" && $('.promotion-check').attr('attr-top') == 1) {
            radio11 = 0;
        }
        $('.usertotalCredit').text(radio11);
        TotalCredit(radio11);
        GetTypeProductSelected(11);
        $('.NewUiProduct-TopListing').removeClass('Cusopacity');
        $('.NewUiProduct-PreListing').addClass('Cusopacity');
        $('.NewUiProduct-freeListing').addClass('Cusopacity');
        $('.NewUiProduct-expressListing').addClass('Cusopacity');
    });

    $('#radio_12').change(function () {
        radio12 = $('input[name="list"]:checked').val();
        $('.usertotalCredit').text(0);
        TotalCredit(0);
        $('.loadRe').html("");
        $('.loadNameProduct').text(""); $('.loadPriceTotalProduct').text("");
        $('.loadDateProduct').text("");
        $('.loadPriceProduct').text("");
        $('.NewUiProduct-TopListing').removeClass('Cusopacity');
        $('.NewUiProduct-PreListing').removeClass('Cusopacity');
        $('.NewUiProduct-freeListing').removeClass('Cusopacity');
        $('.NewUiProduct-expressListing').removeClass('Cusopacity');
    });

    $('#radio_35').change(function () {
        radio35 = $('input[name="list"]:checked').val();
        $('.usertotalCredit').text(radio35);
        TotalCredit(radio35);
        GetTypeProductSelected(35);
        $('.NewUiProduct-TopListing').addClass('Cusopacity');
        $('.NewUiProduct-PreListing').addClass('Cusopacity');
        $('.NewUiProduct-freeListing').addClass('Cusopacity');
        $('.NewUiProduct-expressListing').removeClass('Cusopacity');
    });

    $('#radio_38').change(function () {
        radio38 = $('input[name="list"]:checked').val();
        $('.usertotalCredit').text(radio38);
        TotalCredit(radio38);
        GetTypeProductSelected(38);
        $('.NewUiProduct-TopListing').addClass('Cusopacity');
        $('.NewUiProduct-PreListing').removeClass('Cusopacity');
        $('.NewUiProduct-freeListing').addClass('Cusopacity');
        $('.NewUiProduct-expressListing').addClass('Cusopacity');
    });

    $('#radio_39').change(function () {
        radio39 = $('input[name="list"]:checked').val();
        $('.usertotalCredit').text(radio39);
        TotalCredit(radio39);
        GetTypeProductSelected(39);
        $('.NewUiProduct-TopListing').removeClass('Cusopacity');
        $('.NewUiProduct-PreListing').addClass('Cusopacity');
        $('.NewUiProduct-freeListing').addClass('Cusopacity');
        $('.NewUiProduct-expressListing').addClass('Cusopacity');
    });

    $('.usertotalCredit').text(radio9);

    LoadResultListing();
}

function GetCreditProduct() {
    $('.userCredit').text(NewUI.data.user.credits.format(0, 3, '.'));
}

function TotalCredit(value) {
    var credits = parseInt(value, 10);
    var moneySubtracted = NewUI.data.user.credits - credits;

    if (moneySubtracted < 0) {
        toastr.error("Tài kho?n c?a b?n không ??, xin vui lòng n?p thêm.");
        $('#btnNextselect').prop('disabled', true);

        $('.btnAddMoney').removeClass('hidden');
    } else {
        $('#btnNextselect').prop('disabled', false);
        $('.btnAddMoney').addClass('hidden');
    }
    $('.userCreditNew').text(moneySubtracted.format(0, 3, '.'));
    $('.userCredit').text(NewUI.data.user.credits.format(0, 3, '.'));
}

function TotalMainAccount(value) {
    var credits = parseInt(value, 10);
    var respone = NewUI.data.user.mainCredits - credits;
    if (respone < 0) {
        toastr.error("Tài kho?n chính c?a b?n hi?n không ?? ti?n, xin vui lòng n?p ti?n thêm.");

        $('#btnNextselect').prop('disabled', true);
        $('.btnAddMoney').removeClass('hidden');
    } else {
        $('#btnNextselect').prop('disabled', false);
        $('.btnAddMoney').addClass('hidden');
    }

    $('.userCreditNew').text(respone.format(0, 3, '.'));
    $('.userCredit').text(NewUI.data.user.mainCredits.format(0, 3, '.'));
}

function GetTypeProductSelected(id) {
    var stateIdStr = $('#ddlGeoState').val();
    var productId = parseInt(id, 10);
    var content = [];
    var idad = $('.hidAdvertIdNewui').text();

    var products = NewUI.products[stateIdStr] || [];
    for (var i = 0; i < products.length; i++) {
        var respone = products[i];

        if (productId != products[i].idProduct) continue;

        content.push("<label>" + respone.nameProduct + "</label>" + "<div class=\"NewUifonfamly cusselect\"> " + respone.desProduct + "</div>");
        $('.loadNameProduct').text(respone.nameProduct);
        $('.loadDateProduct').text(respone.datetime);
        $('.loadPriceProduct').text(respone.priceProduct);
        $('.loadPriceTotalProduct').text(respone.priceProduct);
        // Membership
        if ($('.configMembership').val() == "1") {
            if ($('#hidFagMembership').val() == 1) {
                if ($("#switch-membership").bootstrapSwitch('state') == true) {
                    changeMembership();
                }
            }
        }
        $('.loadIdPro').text(respone.idProduct);
        $('.vnexpressIntro').text(respone.vnxepressIntro);

        $('.hdProductsID').val(id);
        if (id == 9) { // Basic Listing
            $('.clsCapcha').removeClass("hidden");
        } else {
            $('.clsCapcha').addClass("hidden");
        }

        $('.selectedService').html(content.join(''));

        break;
    }
}

function UpdateFavoriteListing() {
    $.post('/Services/LoadNewUi.ashx', {
        type: 'UpdateFavoriteListing',
        id: $('.hidAdvertIdNewui').text()
    }, function (response) {
        if (response == false) {
            toastr.error('Vui lòng ??ng nh?p', 'Thông báo!');
        } else {
            var contentfa = "<div   class=\"cusIconFava " + response.status + "\"></div>";
            $('.showfa').html(contentfa);
        }
    });
}

function UpdateFavoriteListingDetail() {
    $.post('/Services/LoadNewUi.ashx', {
        type: 'UpdateFavoriteListing',
        id: $('.hidAdvertIdNewui').text()
    }, function (response) {
        if (response == false) {
            toastr.error('Vui lòng ??ng nh?p', 'Thông báo!');
        } else {
            var contentfa = "<div style=\"height: 30px;\"  class=\"" + response.status + "\"><span class=\"share-text\">" + response.title + "</span></div>";
            $('.showfadetail').html(contentfa);
        }
    });
}

function loadfa() {
    //Dont use favorite when create advert
    return;

    $.post('/Services/LoadNewUi.ashx', {
        type: 'GetFavoriteListing',
        id: $('.hidAdvertIdNewui').text()
    }, function (response) {

        var contentfa = "<div style=\"height: 30px;\"  class=\"" + response.status + "\"><span class=\"share-text\">" + response.title + "</span></div>";
        $('.showfadetail').html(contentfa);

    });
    $.post('/Services/LoadNewUi.ashx', {
        type: 'GetFavoriteListing',
        id: $('.hidAdvertIdNewui').text()
    }, function (response) {
        var contentfa = "<div   class=\"cusIconFava " + response.status + "\"></div>";
        $('.showfa').html(contentfa);
    });

    $.post('/Services/api.ashx', { type: 'GetContactForm' }, function (response) {
        var contactData = [
            { name: response.name, email: response.email }
        ];
        $('#contact-name-mail').empty();
        $('#share-mail-data').empty();
        $("#contacttmpl").tmpl(contactData).appendTo("#contact-name-mail");
        $("#shareemailtmpl").tmpl(contactData).appendTo("#share-mail-data");
    });
};

$(function () {
    $('.cuss-steps-1').addClass('complete');
    $('.numberCircle2').addClass('numberCirclebefor ');
    $('.numberCircle3').addClass('numberCirclebefor');
});

///// Btn_Next
function NextSelectService() {
    NProgress.start();

    if (IsAnyProject()) {
        ToTop();

        $('.cuss-steps-2').removeClass('active');
        $('.cuss-steps-2').addClass('complete');

        $('.prs2').addClass('progressafter');
        $('.prsb2').addClass('progress-barafter');

        $('.numberCircle2').addClass('numberCircleafter');
        $('.numberCircle2').removeClass('numberCircle');

        $('.numberCircle3').addClass('numberCircle');
        $('.numberCircle3').removeClass('numberCirclebefor');
        $('.cuss-steps-3').addClass('active');
        $('.ClassSelectService').addClass('hidden');
        $('.ClassDetail').removeClass('hidden');
        Picture();
        GetDetail();
    } else {
        var t = 0;
        toastr.error('Ch?n gói d?ch v? ');
        t++;
    }
}
function IsAnyProject() {
    if ($('#radio_9').is(':checked')
        || $('#radio_10').is(':checked')
        || $('#radio_11').is(':checked')
        || $('#radio_12').is(':checked')
        || $('#radio_35').is(':checked')
        || $('#radio_38').is(':checked')
        || $('#radio_39').is(':checked')
        )
        return true;
    return false;
}

function NextPayment() {
    ToTop();
    if ($('#AgreePayment').prop("checked") == false) {
        toastr.error("L?i ch?a ch?p nh?n ?i?u kho?n");
        return;
    }

    NProgress.start();
    $('.custhanhtoan').addClass('disabled');

    ////////////// MEMBERSHIP ///////////////////////////////
    // Rule: 
    //      .configMembership : is config in setting (on/off membership function)
    //      #hidFagMembership : user have membership package
    //      #hidPaymentMembership : payment with membership
    if ($('.configMembership').val() == "1") {
        if ($('#hidFagMembership').val() == "1") {
            // Check if enable and choose true type listing
            if ($('#hidPaymentMembership').val() == "1") {
                paymentmembership();
                return;
            }
        }
    }
    ////////////// END MEMBERSHIP ///////////////////////////

    var valyue = $('.loadPriceProduct').text();
    var idAdvert = $('.hidAdvertIdNewui').text();
    var idpro = $('.loadIdPro').text();
    if (advertId > 0) {
        if ($('#radio_12').prop("checked") == false) {
            payment(advertId, valyue, idpro);
        } else {
            payment(advertId, 0, 0);
        }
    } else {
        payment(idAdvert, valyue, idpro);
    }
}

function payment(id, value, idp) {
    sendGaEventTracking('listings', 'create listings record by advertiser', $('#ddlCategory option:selected').text());
    //using main account only for vnexpress listing (idp=36)
    var idCap = $('#g-recaptcha-response').val();
    $.post('/Services/LoadNewUi.ashx', { type: (idp == 36) ? 'paymentByMainAccount' : 'payment', id: id, value: value, idproduct: idp, reclass: idCap }, function (respone) {
        NProgress.set(100);
        $.post('/Services/LoadNewUi.ashx', { type: 'reload-cache', advertId: id });
        if (respone._productIds == 9 && $('#radio_9').is(':checked')) {
            if (respone.checkCap == true) {
                $('.custhanhtoan').removeClass('hidden');
                $('.ClassSuccsess').removeClass('hidden');
                $('.ClassSuccsesspre').addClass('hidden');
                $('.custhanhtoan').addClass('disabled');
                $('.lblthanhtoan').addClass('hidden');
                $('.ClassDetail').addClass('hidden');
                $('.numberCircle3').addClass('numberCircleafter');
                $('.numberCircle3').removeClass('numberCircle');
                $('.numberCircle3').removeClass('numberCirclebefor');

                $('.cuss-steps-3').removeClass('active');
                $('.cuss-steps-3').addClass('complete');

                $('.prs3').addClass('progressafter');
                $('.prsb3').addClass('progress-barafter');
            } else {

                toastr.error("L?i ch?a nh?p capcha");
                $('.custhanhtoan').removeClass('disabled');
            }
        } else {
            $('.lblthanhtoan').addClass('hidden');
            $('.ClassDetail').addClass('hidden');
            $('.numberCircle3').addClass('numberCircleafter');
            $('.numberCircle3').removeClass('numberCircle');
            $('.numberCircle3').removeClass('numberCirclebefor');

            $('.cuss-steps-3').removeClass('active');
            $('.cuss-steps-3').addClass('complete');

            $('.prs3').addClass('progressafter');
            $('.prsb3').addClass('progress-barafter');
            $('.custhanhtoan').removeClass('disabled');
            $('.ClassSuccsesspre').removeClass('hidden');
            $('.ClassSuccsess').addClass('hidden');
            $('.listing-pay').removeClass('hidden');
            var local = $('.hdimahost').text();;
            var fullink = local + respone.url;
            $('#lbllinkNewUi').html(respone.url);
            var content = "<a target=\"_blank\" href=\"" + fullink + "\">" +
                "<label style=\"text-align: center; font-size: 15px; font-weight: normal;\">" + fullink + " </label></a>";

            $('.linkNewUi').html(content);
            if (idp == 36) $('.VnexpressNotificationForLink').show();
            else $('.VnexpressNotificationForLink').hide();
        }
    });
}

function LoadAllUser() {
    $('#txtPhone1').val(NewUI.data.user.phone);
    $('.hdUserID').val(NewUI.data.user.userId);
}

function returnSelectService() {
    ToTop();
    $('.ClassDetail').addClass('hidden');
    $('.ClassSelectService').removeClass('hidden');
    $('.ClassSuccsesspre').addClass('hidden');
}

function returnClassInput() {
    ToTop();
    $('.ClassSelectService').addClass('hidden');
    $('.ClassSuccsesspre').addClass('hidden');
    $('.classinput').removeClass('hidden');
    if (advertId > 0) {
        FillAdvertData();
    }
}

function sendGaEventTracking(eventCategory, eventAction, eventLabel) {
    if (ga) {
        ga('send', 'event', eventCategory, eventAction, eventLabel, "0", {
            nonInteraction: true
        });
    }
}

$(function () {

    $('.preOldUicre').on('click', function () {
        var hdUserId = $('.hdUserID').val();
    });

    $('.savedra').on('click', function () {
        var categoryId = $('.hdCategoryID').val();
        var categoryName = $('.hdCategoryName').val();
    });

    $('.savestep1').on('click', function () {
        var t = 0;
        if ($('#ddlCategory').val() == "0") {
            toastr.error('Ch?n danh m?c ');
            t++;
        }

        if ($('#ddlSubCategory').val() == "0") {
            toastr.error('Ch?n danh m?c ');
            t++;
        }

        if ($('#ddlGeoState').val() == "0") {
            toastr.error('Ch?n thành ph?/ T?nh thành');
            t++;
        }

        if ($('#ddlGeoDistrict').val() == "0") {
            toastr.error('Ch?n Qu?n/ Huy?n ');
            t++;
        }

        if ($('#ddlGeoWard').val() == "0") {
            toastr.error('Ch?n Ph??ng ');
            t++;
        }

        if ($('#txtTitle').val() == "") {
            toastr.error('Vui lòng nh?p tiêu ??!');
            t++;
        }

        if ($('#txtShortDescription').val() == "") {
            toastr.error('Vui lòng nh?p mô t?');
            t++;
        }

        if ($('#txtDescription').val() == "") {
            toastr.error('Vui lòng nh?p mô t? chi ti?t!');
            t++;
        }

        if ($('#txtDescription').val().length >= 4000) {
            toastr.error('Mô t? chi ti?t ch? t?i ?a 4.000 kí t? ');
            t++;
        }

        if ($('#grp_176').val() <= 1) {
            toastr.error('Di?n tích ph?i l?n h?n 1!');
            t++;
        }

        if ($('.dz-preview').size() == 0) {
            toastr.error('Vui lòng ch?n ít nh?t m?t hình!');
            t++;
        } else {
            $('.lblimg').addClass('hidden');
        }

        if ($('#ck_13845').prop('checked') == false) {
            if ($('#grp_1').val() <= 0) {
                toastr.error('Vui lòng nh?p giá!');
                t++;
            }

            if ($('#grp_1').val() > 1000000000) {

                toastr.error('Giá ti?n ch?a h??p lê?. Giá ti?n nên t?? 0 ?ê?n 1.000.000');
                t++;
            }
        }

        if ($('#radioDT2').prop('checked') == true) {
            if ($('#txtPhone2').val() == '') {
                toastr.error('Vui lòng nh?p s? ?i?n tho?i 2!');
                t++;
            }

            if ($('#txtPhone2').length > 11) {
                toastr.error('S? ?i?n tho?i t?i ?a 11 s?!');
                t++;
            }
        }

        if ($('#txtPhone2').val() != "") {
            if ($('#txtPhone2').length > 11) {

                toastr.error('S? ?i?n tho?i t?i ?a 11 s?!');
                t++;
            }
        }

        if (t == 0) {
            sendGaEventTracking('draft ad', 'draft listing but not yet put live', $('#ddlCategory option:selected').text());

            $('#action_type').val('InsertAdvert');
            $.post('/Services/LoadNewUi.ashx', $(document.getElementById("ctl01")).serialize(), function (respone) {
                if (advertId >= 0) {
                    $('.hidAdvertIdNewui').text(advertId);
                    UploadImg1();
                } else {
                    $('.hidAdvertIdNewui').text(respone.advertID);
                    UploadImg1();
                }

                $('.hdCategoryID').val(respone.categoryID);
                $('.hdCategoryName').val(respone.categoryName);
                $.cookie('cate', $('#ddlCategory').val());
                $.cookie('stateID', $('#ddlGeoState').val());
                $.cookie('districtID', $('#ddlGeoDistrict').val());
                $.cookie('subCateID', $('#ddlSubCategory').val());
                var radio = $("input[type='radio'][name='optionsRadiosInline']:checked").val();
                $.cookie('radioBan', radio);
            });
        }
    });

    var isServer = false;

    function uploadToServer1() {
        $.each($('.cbtn-info-ok'), function (i, o) {
            if ($(o).parents('.dz-preview').find('.tools.final').hasClass('hidden')) {
                isServer = true;

                $(o).click();
            }
        });

        return false;
    }

    function UploadImg1(obj) {
        $('.savestep1').addClass('disabled');
        $('.cropper-container img').css({ "transform": "none" });
        uploadToServer1();
        var totalitem = $('.dz-preview').size();
        $.each($('.dz-preview'), function (i, o) {
            $(o).find("input:radio[name=groupimg][disabled=false]:first").attr('checked', true);
            var image = $(o).find('#cusimg');
            var valuerotate = $(o).find('#dataRotate').val();
            var dataX = $(o).find('#dataX').val();
            var dataY = $(o).find('#dataY').val();
            var dataHeight = $(o).find('#dataHeight').val();
            var dataWidth = $(o).find('#dataWidth').val();
            var right = $(window).width() - dataX - image.width();
            var bottom = $(window).height() - dataY - image.height();
            var dataimg = $(o).find('#dataimg').text();
            var dataname = $(o).find('.cusinput').val();
            var idpic = $(o).find('#cusimg').attr('rel');

            $.ajax({
                type: "POST",
                url: "/Services/UploadImg.asmx/SaveImg",
                dataType: "json",
                data: JSON.stringify({ idAdvert: $('.hidAdvertIdNewui').text(), index: i, name: dataname, cfile: (dataimg == null ? "" : dataimg), isactive: $(o).find('input:checked').size(), idpicture: idpic }),
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    $(o).find('#cusimg').attr('rel', data.d);

                    totalitem--;

                    if (totalitem == 0) {
                        url = "/member/member-object-list";
                        $(location).attr("href", url);

                        $('.savestep1').removeClass('disabled');
                    }
                },
                error: function (e) {
                    toastr.error('Vui lòng ch?n l?i hình ?nh');

                    $('.savestep1').removeClass('disabled');
                }
            });
        });

        return false;
    }

    $('.btnNextNewUi').on('click', function () {
        var categoryId = $('#ddlCategory').val();
        var categoryName = $('#ddlCategory option[value=' + categoryId + ']').attr('selected', true).text();
    });

    $('#btnNextselect').on('click', function () {
        var categoryId = $('.hdCategoryID').val();
        var categoryName = $('.hdCategoryName').val();
    });

    $('.custhanhtoan').on('click', function () {
        var categoryId = $('.hdCategoryID').val();
        var categoryName = $('.hdCategoryName').val();
    });

    $('.backstep1').on('click', function () {
        var categoryId = $('.hdCategoryID').val();
        var categoryName = $('.hdCategoryName').val();
    });

    $('.backstep2').on('click', function () {
        var categoryId = $('.hdCategoryID').val();
        var categoryName = $('.hdCategoryName').val();
    });
});

function ToTop() {
    $('html, body').animate({
        scrollTop: 0
    }, 0);
}

function backtotop() {
    jQuery(window).scroll(function () {
        if (jQuery(this).scrollTop() > 100) {

            jQuery('#back-top').fadeIn();
        } else {
            jQuery('#back-top').fadeOut();
        }
    });

    // scroll body to 0px on click
    jQuery('#back-top a').click(function () {
        jQuery('body, html').stop(false, false).animate({
            scrollTop: 0
        }, 800);
        return false;
    });

    // scroll body to 0px on click
    jQuery('.arrow-scroll a').click(function () {
        jQuery('body, html').stop(false, false).animate({
            scrollTop: 500
        }, 800);
        return false;
    });
}

function eventAfterLoadAttributes() {
    $('#grp_1').keypress(function (event) {
        var $this = $(this);
        if ((event.which != 46 || $this.val().indexOf('.') != -1) &&
           ((event.which < 48 || event.which > 57) &&
           (event.which != 0 && event.which != 8))) {
            event.preventDefault();
        }

        var text = $(this).val();
        if ((event.which == 46) && (text.indexOf('.') == -1)) {
            setTimeout(function () {
                if ($this.val().substring($this.val().indexOf('.')).length > 3) {
                    $this.val($this.val().substring(0, $this.val().indexOf('.') + 3));
                }
            }, 1);
        }

        if ((text.indexOf('.') != -1) &&
            (text.substring(text.indexOf('.')).length > 2) &&
            (event.which != 0 && event.which != 8) &&
            ($(this)[0].selectionStart >= text.length - 2)) {
            event.preventDefault();
        }
    });

    $("#grp_1").on('input', function () {
        setTextMoneyAllowDecimal($(this));
    });
}

function setTextMoneyAllowDecimal(elementObject) {
    var trueValue;
    if (elementObject == null || elementObject.val() == "") trueValue = 0;
    else trueValue = elementObject.val().replace(/,/g, '');

    $('#hidXPrice').val(parseFloat(trueValue));
    $('#hidXPriceTemp').val(parseFloat(trueValue));
    var textMoney;
    var displayValue;
    var displayValueInput = trueValue;
    if (parseFloat(trueValue) < 1000) {
        // ok
        textMoney = parseFloat(trueValue) + " Tri?u";
    } else {
        var pricePrecision = trueValue.toString().split('.')[0];
        var priceScale = trueValue.toString().split('.')[1];
        displayValue = (parseFloat(pricePrecision) / 1000).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        if (typeof priceScale === "undefined") {
            // ok
            textMoney = displayValue + " T?";
            displayValueInput = pricePrecision.replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        } else {
            textMoney = displayValue + '.000' + priceScale + " T?";
            displayValueInput = pricePrecision.replace(/\B(?=(\d{3})+(?!\d))/g, ",") + '.' + priceScale;
        }
    }

    var divPrivatePrice = $('#ck_13845').closest('.row').closest('.col-lg-6');
    if ($('.dividePricePrivate').length == 0) {
        $(divPrivatePrice).before('<div class="clearfix dividePricePrivate"></div>');
        $('.dividePricePrivate').before($('.money-text').remove().clone());
    }
    $('.money-text').text('');
    $('.money-text').text(textMoney);
    $('#hidXPriceDisplay').val(textMoney);
    if (displayValueInput == 0) {
        $('.money-text').addClass('hidden');
    } else {
        $('.money-text').removeClass('hidden');
    }
    if (displayValueInput != null && displayValueInput.length > 1) {
        displayValueInput = displayValueInput.replace(/^0+/, '');
    }
    elementObject.val(displayValueInput);
}

function setTextMoney(elementObject) {
    $(elementObject).parseNumber({ format: "#,###", locale: "vn" });
    $(elementObject).formatNumber({ format: "#,###", locale: "vn" });
    $('#hidXPrice').val($(elementObject).parseNumber({ format: "#,###.00", locale: "vn" }, false));
    $('#hidXPriceTemp').val($(elementObject).val());
    var divPrivatePrice = $('#ck_13845').closest('.row').closest('.col-lg-6');
    if ($('.dividePricePrivate').length == 0) {
        $(divPrivatePrice).before('<div class="clearfix dividePricePrivate"></div>');
        $('.dividePricePrivate').before($('.money-text').remove().clone());
    }

    $('.money-text').text('');
    var textMoney;
    var trueValue;
    var displayValue;
    displayValue = convertPointNumber($(elementObject).val().replace(/[^a-z0-9]/gi, ','));
    trueValue = $(elementObject).parseNumber({ format: "#,###.00", locale: "vn" }, false);

    if (trueValue < 1000) {
        textMoney = displayValue + " Tri?u";
    } else {
        textMoney = trueValue / 1000 + " T?";
    }

    $('.money-text').text(textMoney);
    $('#hidXPriceDisplay').val(textMoney);
}

////////////////////////////////// MEMBERSHIP && PROMOTION AREA ///////////////////////////////////////////////////////////
function loadMembership() {
    $.getJSON('/Services/LoadNewUi.ashx', { type: 'GetMembership', userId: $('.hdUserID').val(), city: $('#ddlGeoState').val(), district: $('#ddlGeoDistrict').val() }, function (response) {
        if (response.membership == 1) {
            $('#hidFagMembership').val(1);
            $('.membership-area').removeClass('hidden');
            $("#switch-membership").bootstrapSwitch();
            $('#switch-membership').on('switchChange.bootstrapSwitch', function (event, state) {
                membershipChangeCondition(state);
            });
            var i = 0;
            $('#ddlMembership').empty();
            $.each(response.listMembership, function (key, value) {
                var listing = "";
                var listingExist = "";
                if (parseInt(value.Top) > 0) {
                    listing += value.Top + " Top";
                }

                if (parseInt(value.Premium) > 0) {
                    if (listing != "") {
                        listing += " - " + value.Premium + " Premium";
                    } else {
                        listing += value.Premium + " Premium";
                    }
                }

                if (parseInt(value.Express) > 0) {
                    if (listing != "") {
                        listing += " - " + value.Express + " Express";
                    } else {
                        listing += value.Express + " Express";
                    }
                }

                if (parseInt(value.TopUnused) > 0) {
                    listingExist += value.TopUnused + " Top";
                }

                if (parseInt(value.PremiumUnused) > 0) {
                    if (listingExist != "") {
                        listingExist += " - " + value.PremiumUnused + " Premium";
                    } else {
                        listingExist += value.PremiumUnused + " Premium";
                    }
                }

                if (parseInt(value.ExpressUnused) > 0) {
                    if (listingExist != "") {
                        listingExist += " - " + value.ExpressUnused + " Express";
                    } else {
                        listingExist += value.ExpressUnused + " Express";
                    }
                }
                $('#ddlMembership').append($('<option>', {
                    value: value.Id,
                    text: value.MembershipType + " (" + listing + ") th?i h?n " + value.Period
                }));
                $('.membership-information').append('<div class="hidden information-membership-package" id="membership-' + value.Id + '">S? l??ng tin còn l?i (' + listingExist + ')</div>');
                $('.membership-information').append('<input type="hidden" id="hidMembershipTop-' + value.Id + '" value="' + value.Top + '"/>');
                $('.membership-information').append('<input type="hidden" id="hidMembershipPremium-' + value.Id + '" value="' + value.Premium + '"/>');
                $('.membership-information').append('<input type="hidden" id="hidMembershipExpress-' + value.Id + '" value="' + value.Express + '"/>');
                $('.membership-information').append('<input type="hidden" id="hidMembershipCity-' + value.Id + '" value="' + value.City + '"/>');
                $('.membership-information').append('<input type="hidden" id="hidMembershipDistrict-' + value.Id + '" value="' + value.District + '"/>');
                i++;
            });

            changeMembership();
            $('#ddlMembership').on('change', function () {
                changeMembership();
            });
        } else {
            $('#hidFagMembership').val(0);
        }
    });
}

function changeMembership() {
    membershipChangeCondition($("#switch-membership").bootstrapSwitch('state'));
    $('[id^="membership-"]').addClass('hidden');
    $('#membership-' + $('#ddlMembership').val()).removeClass('hidden');
}

function membershipChangeCondition(state) {
    if (state == true) {
        getPriceProduct();

        rollBackPrice();
        var membershipId = $('#ddlMembership').val();
        // Location later
        if (parseInt($('#hidMembershipTop-' + membershipId).val()) > 0 && $('#radio_11').is(':checked')) {
            setFreePriceTop();
        }

        if (parseInt($('#hidMembershipPremium-' + membershipId).val()) > 0 && $('#radio_10').is(':checked')) {
            setFreePricePremium();
        }

        if (parseInt($('#hidMembershipExpress-' + membershipId).val()) > 0 && $('#radio_35').is(':checked')) {
            setFreePriceExpress();
        }
    } else {
        rollBackPrice();
    }
}

function loadPromotion() {
    if ($('#ddlProject').val() != null) {
        $.post('/Services/LoadNewUi.ashx', { type: 'GetPromotion', userId: $('.hdUserID').val(), projectId: $('#ddlProject').val() }, function (respone) {
            if (respone.promotion == 1) {
                $('#hidFagPromotion').val(1);
                $('.promotion-area').removeClass('hidden');
                $('.promotion-membership').html(respone.textContent);
                $('.promotion-check').attr('attr-promotion', respone.promotion).attr('attr-premium', respone.premium).attr('attr-top', respone.top);
            } else {
                $('#hidFagPromotion').val(0);
            }
        });
    }
}

function getPriceProduct() {
    $('#hidPriceTop').val($('#radio_11').val());
    $('#hidPricePremium').val($('#radio_10').val());
    $('#hidPriceExpress').val($('#radio_35').val());
}

function setFreePriceTop() {
    if ($('#radio_11').is(':checked')) {
        $('.cuslbl_11').addClass('line-through');
        $('#radio_11').val(0);
        setFreePrice();
    }
}

function setFreePricePremium() {
    if ($('#radio_10').is(':checked')) {
        $('.cuslbl_10').addClass('line-through');
        $('#radio_10').val(0);
        setFreePrice();
    }
}

function setFreePriceExpress() {
    if ($('#radio_35').is(':checked')) {
        $('.cuslbl_35').addClass('line-through');
        $('#radio_35').val(0);
        setFreePrice();
    }
}


function setFreePrice() {
    $('#btnNextselect').prop('disabled', false);
    $('.btnAddMoney').addClass('hidden');
    $('.usertotalCredit').text(0);
    $('.userCreditNew').text(0);
    $('.membership-area-payment').removeClass('hidden');
    $('.membership-area-payment').html('<div class="col-xs-12">B?n ?ang s? d?ng gói membership: <b>' + $('#ddlMembership option:selected').text() + '</b></div>');
    $('.loadPriceProduct').text(0);
    $('.loadPriceTotalProduct').text(0);
    $('#hidPaymentMembership').val(1);
}

function rollBackPrice() {
    $('.cuslbl_11').removeClass('line-through');
    $('.cuslbl_10').removeClass('line-through');
    $('.cuslbl_35').removeClass('line-through');
    $('#radio_11').val($('#hidPriceTop').val());
    $('#radio_10').val($('#hidPricePremium').val());
    $('#radio_35').val($('#hidPriceExpress').val());
    if ($('#radio_11').is(':checked')) {
        $('.usertotalCredit').text($('#radio_11').val());
        $('.loadPriceProduct').text($('#radio_11').val());
        $('.loadPriceTotalProduct').text($('#radio_11').val());
    }

    if ($('#radio_10').is(':checked')) {
        $('.usertotalCredit').text($('#radio_10').val());
        $('.loadPriceProduct').text($('#radio_10').val());
        $('.loadPriceTotalProduct').text($('#radio_10').val());
    }
    if ($('#radio_35').is(':checked')) {
        $('.usertotalCredit').text($('#radio_35').val());
        $('.loadPriceProduct').text($('#radio_35').val());
        $('.loadPriceTotalProduct').text($('#radio_35').val());
    }

    $('.membership-area-payment').addClass('hidden');
    $('#hidPaymentMembership').val(0);
}

function paymentmembership() {
    $.post('/Services/LoadNewUi.ashx', {
        type: 'PaymentMembership',
        advertId: $('.hidAdvertIdNewui').text(),
        productId: $('.loadIdPro').text(),
        membershipId: $('#ddlMembership').val(),
        postVnExpress: $('.vnepressIntro').is(':checked')
    }, function (respone) {

        NProgress.set(100);
        $.post('/Services/LoadNewUi.ashx', { type: 'reload-cache', advertId: $('.hidAdvertIdNewui').text() });

        $('.lblthanhtoan').addClass('hidden');
        $('.ClassDetail').addClass('hidden');
        $('.numberCircle3').addClass('numberCircleafter');
        $('.numberCircle3').removeClass('numberCircle');
        $('.numberCircle3').removeClass('numberCirclebefor');

        $('.cuss-steps-3').removeClass('active');
        $('.cuss-steps-3').addClass('complete');

        $('.prs3').addClass('progressafter');
        $('.prsb3').addClass('progress-barafter');


        if (respone._productIds == 9) {
            $('.custhanhtoan').removeClass('hidden');
            $('.ClassSuccsess').removeClass('hidden');
            $('.ClassSuccsesspre').addClass('hidden');
        } else {
            $('.custhanhtoan').removeClass('disabled');
            $('.ClassSuccsesspre').removeClass('hidden');
            $('.ClassSuccsess').addClass('hidden');
            $('.listing-pay').removeClass('hidden');
            var local = $('.hdimahost').text();;
            var fullink = local + respone.url;
            $('#lbllinkNewUi').html(respone.url);
            var content = "<a target=\"_blank\" href=\"" + fullink + "\">" +
                "<label style=\"text-align: center; font-size: 15px; font-weight: normal;\">" + fullink + " </label></a>";

            $('.linkNewUi').html(content);
        }
    });
}

////////////////////////////////// END MEMBERSHIP && PROMOTION AREA ///////////////////////////////////////////////////////////
/**
 * Format Money
 * Number.prototype.format(n, x, s, c)
 * 
 * @param integer n: length of decimal
 * @param integer x: length of whole part
 * @param mixed   s: sections delimiter
 * @param mixed   c: decimal delimiter
 */
Number.prototype.format = function (n, x, s, c) {
    var re = '\\d(?=(\\d{' + (x || 3) + '})+' + (n > 0 ? '\\D' : '$') + ')',
        num = this.toFixed(Math.max(0, ~~n));

    return (c ? num.replace('.', c) : num).replace(new RegExp(re, 'g'), '$&' + (s || ','));
};

Dropzone.autoDiscover = false;
$("div#demo-upload").dropzone({ url: "/Services/LoadNewUi.ashx?type=Dropzone" });