var countData = $('.wrap-content .panel.panel-flat').length,
		limitpage = 5,
		txtError = 'Lỗi',
		txtAlert = 'Bạn có những từ khóa giống nhau!',
		txtAlert1 = 'Bạn có chắc chắn muốn xóa nhóm này?',
		txtAlert2 = 'Bạn có chắc chắn muốn xóa?',
		txtplholder = 'Nhập câu trả lời của bạn',
		txtbt = 'Thêm câu trả lời';
        txtbt1 = 'Đồng ý';
        txtbt2 = 'Hủy';
$(document).ready(function () {
    // loading
    appendPaging(countData, limitpage);

    var checkAlert = false;
    $('body').on('click', '.moveTop', function (event) {
        var el_index = $(this).closest('.panel-flat').index();
        var el_html = '';
        $(this).closest('.panel-flat').slideUp('slow', function () {
            el_html = $(this).closest('.panel-flat')[0].outerHTML;
            $(this).remove();
            $('.wrap-content .panel-flat').eq(el_index - 1).before(el_html);
            $('.wrap-content .panel-flat').eq(el_index - 1).slideDown();

            if ($('.limitPage').length > 0) {
                var lmtPage = $('.limitPage').val();
                var countDataPage = $('.wrap-content .panel.panel-flat').length;
                $('.wrap-content .panel.panel-flat').each(function (index, el) {
                    if (index >= lmtPage) {
                        $(this).hide();
                    } else {
                        $(this).show();
                    }
                });
                $('#paging').empty();
                appendPaging(countDataPage, lmtPage);
            }
        });
    })

    $('body').on('click', '.moveBot', function (event) {
        var el_index = $(this).closest('.panel-flat').index();
        var el_html = '';
        $(this).closest('.panel-flat').slideUp('slow', function () {
            el_html = $(this).closest('.panel-flat')[0].outerHTML;
            $(this).remove();
            $('.wrap-content .panel-flat').eq(el_index).after(el_html);
            $('.wrap-content .panel-flat').eq(el_index + 1).slideDown();

            if ($('.limitPage').length > 0) {
                var lmtPage = $('.limitPage').val();
                var countDataPage = $('.wrap-content .panel.panel-flat').length;
                $('.wrap-content .panel.panel-flat').each(function (index, el) {
                    if (index >= lmtPage) {
                        $(this).hide();
                    } else {
                        $(this).show();
                    }
                });
                $('#paging').empty();
                appendPaging(countDataPage, lmtPage);
            }
        });
    })

    $('body').on('click', '.switchery', function () {
        var tempNonChecked = '<span class="switchery switchery-default" style="box-shadow: rgb(223, 223, 223) 0px 0px 0px 0px inset; border-color: rgb(223, 223, 223); background-color: rgb(255, 255, 255); transition: border 0.4s ease 0s, box-shadow 0.4s ease 0s;"><small style="left: 0px; transition: background-color 0.4s ease 0s, left 0.2s ease 0s;"></small></span>';
        var tempChecked = '<span class="switchery switchery-default" style="background-color: rgb(100, 189, 99); border-color: rgb(100, 189, 99); box-shadow: rgb(100, 189, 99) 0px 0px 0px 8px inset; transition: border 0.4s ease 0s, box-shadow 0.4s ease 0s, background-color 1.2s ease 0s;"><small style="left: 14px; transition: background-color 0.4s ease 0s, left 0.2s ease 0s; background-color: rgb(255, 255, 255);"></small></span>';
        $(this).next().remove();
        if ($(this).prop('checked')) {
            $(this).next().remove();
            $(tempChecked).insertAfter($(this))
            $($(this).parent().parent().next().next()).removeClass("hidden");
        } else {
            $(this).next().remove();
            $(tempNonChecked).insertAfter($(this))
            $($(this).parent().parent().next().next()).addClass("hidden");
        }
    })

    $('.wrap-content .addedTag').each(function (index, el) {
        if (!$(this).hasClass('error-tag')) {
            if ($('.wrap-content input[type="hidden"][value="' + $(this).children('input').val() + '"]').length > 1) {
                $(this).addClass('error-tag');
            }
        }
    });

    $('.wrap-content').on('click', '.tagRemove', function (event) {
        event.preventDefault();
        if ($(this).parent().hasClass('error-tag')) {
            var val = $(this).siblings('input').val();
            if ($('.wrap-content input[type="hidden"][value="' + val + '"]').length <= 2) {
                $('.wrap-content input[type="hidden"][value="' + val + '"]').parent('.addedTag').removeClass('error-tag');
            }
        }

        $(this).parent().remove();
    });

    $('.wrap-content').on('click', 'ul.tags', function (event) {
        $(this).find('.search-field').show();
        $(this).find('.search-field').focus();
    });

    $('.wrap-content').on('keypress', '.search-field', function (event) {
        var elParent = $(this).parents('.tags');
        if (event.which == '13') {
            if (($(this).val().trim() != '') && ($(".tags .addedTag input[value=\"" + $(this).val().trim() + "\"]").length == 0)) {
                $("<li class=\"addedTag\">" + $(this).val().toLowerCase().trim() + "<span class=\"tagRemove\">x</span><input type=\"hidden\" value=\"" + $(this).val().toLowerCase().trim() + "\" name=\"data[answer][" + elParent.parents('.panel.panel-flat').attr('indexpanel') + "][]\"></li>").insertBefore($(this).parents('.tagAdd'));

                var attr = $(this).attr('attr-data');
                if (typeof attr !== typeof undefined && attr !== false) {
                    if ($('.wrap-content input[value="' + attr + '"]').length <= 1) {
                        $('.wrap-content input[value="' + attr + '"]').parent('.addedTag').removeClass('error-tag');
                    }
                    $(this).removeAttr('attr-data');
                }

                $(this).val('');
                $(this).parents('.tags').append($(this).parent().clone());
                $(this).parent().remove();
                elParent.find('.search-field').focus();
            } else if ($(".tags .addedTag input[value=\"" + $(this).val().trim() + "\"]").length > 0) {
                if (!checkAlert) {
                    checkAlert = true;
                    bootbox.alert({
                        message: txtAlert,
                        callback: function () {
                            checkAlert = false;
                        }
                    });
                }
            } else {
                $(this).val('');
            }
        }
    });

    $('.wrap-content').on('click', '.addedTag', function (event) {
        event.preventDefault();
        var elParent = $(this).parents('.tags');
        var elSearch = $(this).parents('.tags').find('.tagAdd.taglist .search-field').val();

        if (typeof elSearch != 'undefined') {
            if ($(this).parents('.tags').find('.tagAdd.taglist .search-field').val().trim() == '') {
                $(this).parents('.tags').find('.tagAdd.taglist').remove();
            } else {
                var html1 = "<li class=\"addedTag\">" + $(this).parents('.tags').find('.tagAdd.taglist .search-field').val().toLowerCase().trim() + "<span class=\"tagRemove\">x</span><input type=\"hidden\" value=\"" + $(this).parents('.tags').find('.tagAdd.taglist .search-field').val().toLowerCase().trim() + "\" name=\"data[answer][" + elParent.parents('.panel.panel-flat').attr('indexpanel') + "][]\"></li>";
                $(this).parents('.tags').find('.tagAdd.taglist').replaceWith(html1);
            }

            var val = $(this).children('input').val().trim();
            var html = '<li class="tagAdd taglist"><input type="text" attr-data="' + val + '" class="search-field" value="" style="display: inline-block;"></li>';
            $(this).replaceWith(html);
            elParent.find('.search-field').focus().val(val);
        }

    });

    $('.wrap-content').on('focusout', '.tagAdd.taglist', function (event) {
        var elParent = $(this).parents('.tags');
        if ($(this).children('input').val().trim() != '') {
            var classTag = '';
            if ($(".tags .addedTag input[value=\"" + $(this).children('input').val().trim() + "\"]").length > 0) {
                if (!checkAlert) {
                    checkAlert = true;
                    bootbox.alert({
                        message: txtAlert,
                        callback: function () {
                            checkAlert = false;
                        }
                    });
                }

                classTag = 'error-tag';
            }

            $("<li class=\"addedTag " + classTag + "\">" + $(this).children('input').val().toLowerCase().trim() + "<span class=\"tagRemove\">x</span><input type=\"hidden\" value=\"" + $(this).children('input').val().toLowerCase().trim() + "\" name=\"data[answer][" + elParent.parents('.panel.panel-flat').attr('indexpanel') + "][]\"></li>").insertBefore($(this));

            // var attr = $(this).children('input').attr('attr-data');
            // if (typeof attr !== typeof undefined && attr !== false) {
            // 	if($('input[value=\""+attr+"\"]').size() <= 1){
            // 		$('input[value=\""+attr+"\"]').parent('.addedTag').removeClass('error-tag');
            // 	}
            $(this).children('input').removeAttr('attr-data');
            // }
            //
            $(this).children('input').val('');
            $(this).parents('.tags').append($(this).clone());
            $(this).remove();
            elParent.find('.search-field').focus();
        } else {
            $(this).children('input').val('');
        }
    })


    $('body').on('click', '.addCt', function (event) {
        var sizeCT = $('.wrap-content').attr('data-countpanel');
        sizeCT = parseInt(sizeCT);
        var stri = '<div class="panel panel-flat" indexpanel="' + (sizeCT + 1) + '">' +
				'<div class="panel-body">' +
					'<i class="fa fa-trash icon-bin rmCt"></i>' +
					'<div class="wrMove">' +
                        '<i class="fa fa-arrow-up moveTop" style="display: inline;"></i>'+
                        '<i class="fa fa-arrow-down moveBot" style="display: inline;"></i>'+
					'</div>' +
					'<div class="row">' +
						'<div class="col-lg-6 userSay">' +
							'<label>Người dùng nói</label>' +
							'<div class="input-group">' +
								'<ul class="tags checkvalid">' +
						            '<li class="tagAdd taglist">' +
						                '<input type="text" class="search-field">' +
						            '</li>' +
						        '</ul>' +
								'<span class="input-group-addon">' +
									'<input type="checkbox" class="styled" ' +
									'name="data[exactly][' + sizeCT + ']" checked="checked">' +
								'</span>' +
							'</div>' +
						'</div>' +
						'<div class="col-lg-6 botReply">' +
							'<label>Bot trả lời với&nbsp;</label>' +
							'<label class="learn_switchbot">' +
								'<input type="checkbox" name="data[Bot][Status]" class="learn_switchinput" checked="">' +
								'<div class="learn_sliderbot learn_roundbot"></div>' +
							'</label>' +
							'<div class="checkbox checkbox-switchery switchery-xs pull-right pd0">' +
								'<label>' +
									'<input type="checkbox" class="switchery randomText" style="display:none">' +
                                    '<span class="switchery switchery-default" style="box-shadow: rgb(223, 223, 223) 0px 0px 0px 0px inset; border-color: rgb(223, 223, 223); background-color: rgb(255, 255, 255); transition: border 0.4s ease 0s, box-shadow 0.4s ease 0s;"><small style="left: 0px; transition: background-color 0.4s ease 0s, left 0.2s ease 0s;"></small></span>'+
									'Ngẫu nhiên' +
								'</label>' +
							'</div>' +
							'<div class="wrbutton" indexbt="1">' +
								'<div class="bt">' +
									'<input type="text" name="data[question][' + sizeCT + '][]" class="form-control checkvalid" maxlength="320">' +
									'<i class="fa fa-remove icon-bin rmText"></i>' +
								'</div>' +
							'</div>' +
							'<button type="button" class="btn btn-success btn-rounded mt20 w100 hidden"><i class="icon-plus22"></i> ' + txtbt + '</button>' +
						'</div>' +
					'</div>' +
				'</div>' +
			'</div>';

        $('.wrap-content').prepend(stri);
        // $('[indexpanel="'+(sizeCT+1)+'"] .tokenfield').tagEditor({
        //   	placeholder: 'Enter tags ...',
        //   	autocomplete: { delay: 250, html: true, position: { collision: 'flip' }}
        // });

        //if (Array.prototype.forEach) {
        //    var elems = Array.prototype.slice.call(document.querySelectorAll('.switchery'));
        //    elems.forEach(function (html) {
        //        var switchery = new Switchery(html);
        //    });
        //}
        //else {
        //    var elems = document.querySelectorAll('.switchery');
        //    for (var i = 0; i < elems.length; i++) {
        //        var switchery = new Switchery(elems[i]);
        //    }
        //}
        $('.rmCt, .moveBot, .moveTop').show();

        $('.wrap-content').attr('data-countpanel', sizeCT + 1);

        if ($('.limitPage').length > 0) {
            var lmtPage = $('.limitPage').val();
            var countDataPage = $('.wrap-content .panel.panel-flat').length;
            $('.wrap-content .panel.panel-flat').each(function (index, el) {
                if (index >= lmtPage) {
                    $(this).hide();
                } else {
                    $(this).show();
                }
            });
            $('#paging').empty();
            appendPaging(countDataPage, lmtPage);
        }

        //var lmtPage = 5;//$('.limitPage').val();
        //var countDataPage = $('.wrap-content .panel.panel-flat').length;
        //$('.wrap-content .panel.panel-flat').each(function (index, el) {
        //    if (index >= lmtPage) {
        //        $(this).hide();
        //    } else {
        //        $(this).show();
        //    }
        //});
        //$('#paging').empty();
        //appendPaging(countDataPage, lmtPage);

    })

    $('body').on('click', '.randomText', function (event) {
        if ($(this).is(':checked')) {
            var size = $(this).parents('.col-lg-6').find('.bt').length;
            if (size < 5) {
                $(this).parents('.col-lg-6').find('.btn').removeClass('hidden');
            }
            if (size == 1) {
                $(this).parents('.col-lg-6').find('.rmText').hide();
            }
            if (size == 5) {
                $(this).parents('.col-lg-6').find('.btn').addClass('hidden');
                $(this).parents('.col-lg-6').find('.rmText').show();
            }
            else {
                $(this).parents('.col-lg-6').find('.rmText').show();
            }
            $(this).parents('.col-lg-6').find('.bt').show();
            $(this).parents('.col-lg-6').find('.wrbutton').attr('indexbt', size);
        } else {
            $(this).parents('.col-lg-6').find('.btn').addClass('hidden');
            $(this).parents('.col-lg-6').find('.bt').hide();
            $(this).parents('.col-lg-6').find('.bt').first().show();
            $(this).parents('.col-lg-6').find('.wrbutton').attr('indexbt', 1);
            $(this).parents('.col-lg-6').find('.rmText').hide();
        }
    });

    $('body').on('click', '.btn-rounded', function (event) {
        var panel = $(this).parents('.panel-flat').attr('indexpanel');
        var bt = $(this).siblings('.wrbutton').attr('indexbt');
        $(this).parents('.col-lg-6').find('.rmText').show();
        if (bt <= 4) {
            var str = '<div class="bt">' +
					'<label class="mt6">Hoặc </label>' +
					'<label class="learn_switchbot">' +
						'<input type="checkbox" name="data[Bot][Status]" class="learn_switchinput" checked="">' +
						'<div class="learn_sliderbot learn_roundbot"></div>' +
					'</label>' +
					'<input type="text" name="data[question][' + (panel - 1) + '][]" class="form-control checkvalid" maxlength="320">' +
					'<i class="fa fa-remove icon-bin rmText" style="display: inline;"></i>' +
				'</div>';
            $(this).siblings('.wrbutton').attr('indexbt', (parseInt(bt) + 1));
            $(this).siblings('.wrbutton').append(str);
            if (bt == 4) {
                $(this).addClass('hidden');
            }
        }
    })

    $('body').on('click', '.rmText', function (event) {
        var si = $(this).parents('.wrbutton').find('.bt').length;
        if (si <= 2) {
            $(this).parents('.wrbutton').find('.rmText').hide();
        }
        $(this).parents('.wrbutton').attr('indexbt', parseInt(si) - 1);
        $(this).parents('.col-lg-6').find('.btn').removeClass('hidden');
        if ($(this).parents('.bt').index() == 0) {
            $(this).parents('.wrbutton').find('.bt').eq(1).find('label').remove();
            $(this).parents('.bt').remove();
        } else {
            $(this).parents('.bt').remove();
        }
    })

    $('body').on('click', '.learn_switchinput', function (event) {
        if ($(this).parents('.wrbutton').length > 0) {
            elParent = $(this).parents('.bt');
        } else {
            elParent = $(this).parents('.col-lg-6').find('.wrbutton .bt').eq(0);
        }
        if ($(this).is(':checked')) {
            vt = $(this).parents('.panel-flat').attr('indexpanel');
            elParent.find('.selectKeyword').remove();
            strHtml = '<input type="text" name="data[question][' + (vt - 1) + '][]" class="form-control checkvalid" maxlength="640" placeholder="' + txtplholder + '">';
            elParent.find('.rmText').before(strHtml);
        } else {
            vt = $(this).parents('.panel-flat').attr('indexpanel');
            elParent.find('input[type=text]').remove();
            strHtml = '<select data-live-search="true" name="data[question][' + (vt - 1) + '][]" class="form-control selectKeyword checkvalid">' +
					card() +
					'</select>';
            elParent.find('.rmText').before(strHtml);
            elParent.find('.rmText').parent().find('.selectKeyword').selectpicker();

            elParent.find('.rmText').parent().find('.selectKeyword').on('show.bs.select', function (e) {
                var val = $(this).selectpicker('val');
                $(this).parents('.bt').find('.selectKeyword option').remove();
                $(this).parents('.bt').find('select.selectKeyword').append(card());
                $(this).parents('.bt').find('.selectKeyword').selectpicker('refresh');
                $(this).parents('.bt').find('.selectKeyword').selectpicker('val', val);
            });

            elParent.find('.rmText').parent().find('.selectKeyword').on('hidden.bs.select', function (e) {
                var val = $(this).selectpicker('val');
                $(this).parents('.bt').find('.selectKeyword optgroup').each(function (index, el) {
                    if ($(el).find('[value="' + val + '"]').length <= 0) {
                        $(el).remove();
                    }
                });
                $(this).parents('.bt').find('.selectKeyword option:not([value="' + val + '"])').remove();
                $(this).parents('.bt').find('.selectKeyword').selectpicker('refresh');
            });

        }
    });

    $('body').on('click', '.rmCt', function (event) {
        var element = $(this);
        bootbox.confirm({
            message: txtAlert2,
            buttons: {
                confirm: {
                    label: txtbt1,
                    className: 'btn-primary'
                },
                cancel: {
                    label: txtbt2,
                    className: 'btn-default'
                }
            },
            callback: function (result) {
                if (result) {
                    var siict = $('.wrap-content .panel-flat').length;

                    element.parents('.panel-flat').find('.userSay ul.tags li.addedTag').each(function (index, el) {
                        if ($(el).hasClass('error-tag')) {
                            var val = $(el).children('input').val();
                            if ($('.wrap-content input[type="hidden"][value="' + val + '"]').length <= 2) {
                                $('.wrap-content input[type="hidden"][value="' + val + '"]').parent('.addedTag').removeClass('error-tag');
                            }
                        }
                    });

                    element.parents('.panel-flat').remove();
                    if (siict < 2) {
                        $('.addCt').trigger('click');
                    }

                    if ($('.limitPage').length > 0) {
                        var lmtPage = $('.limitPage').val();
                        var countDataPage = $('.wrap-content .panel.panel-flat').length;
                        $('.wrap-content .panel.panel-flat').each(function (index, el) {
                            if (index >= lmtPage) {
                                $(this).hide();
                            } else {
                                $(this).show();
                            }
                        });
                        $('#paging').empty();
                        appendPaging(countDataPage, lmtPage);
                    }
                }
            }
        })
    })

    // Paging
    $('body').on('change', '.limitPage', function () {
        var limitPageVal = $(this).find(":selected").val();
        if (Math.ceil(countData / limitPageVal) > 1) {
            var htmlUl = fn_htmlPaging(countData, limitPageVal);
            $('.pagination').remove();
            $('#paging .panel-flat').prepend(htmlUl);
            $('.wrap-content .panel.panel-flat').each(function (index, el) {
                if (index >= limitPageVal) {
                    $(this).hide();
                } else {
                    $(this).show();
                }
            });
        } else {
            $('.pagination').remove();
            $('.wrap-content .panel.panel-flat').show();
        }

    });
    $('body').on('click', '.pagination li a', function (event) {
        event.preventDefault();
        var getLimitPage = $('.limitPage').val();
        if (!$(this).closest('li').hasClass('disabled') && !$(this).closest('li').hasClass('active')) {
            if ($(this).closest('li').hasClass('prev')) {
                $('.pagination li').eq($('.pagination li.active').index() - 1).find('a').trigger('click');
            } else if ($(this).closest('li').hasClass('next')) {
                $('.pagination li').eq($('.pagination li.active').index() + 1).find('a').trigger('click');
            } else {
                var pagelocation = $(this).attr('attr-page');
                $(this).closest('li').siblings('li').removeClass('active');
                $(this).closest('li').addClass('active');
                $('.pagination li').removeClass('disabled');
                if ($(this).closest('li').index() == 1) {
                    $('.pagination li.prev').addClass('disabled');
                }
                if ($(this).closest('li').index() == ($('.pagination li').length - 2)) {
                    $('.pagination li.next').addClass('disabled');
                }

                $('.wrap-content .panel.panel-flat').each(function (index, el) {
                    if (((pagelocation - 1) * getLimitPage < (index + 1)) && ((index + 1) <= pagelocation * getLimitPage)) {
                        $(this).show();
                    } else {
                        $(this).hide();
                    }
                });
            }
        }
    });
})


function appendPaging(countData, limitpage) {
    var htmlPaging = '';
    if (Math.ceil(countData / limitpage) > 1) {
        htmlPaging += '<div class="panel-flat">';
        htmlPaging += fn_htmlPaging(countData, limitpage);
        htmlPaging += '<div class="pull-right">';
        htmlPaging += '<select class="form-control limitPage">';
        for (var j = 1; j <= 4; j++) {
            var selected = '';
            if (j == 1) {
                selected = 'selected';
            }
            htmlPaging += '<option value="' + limitpage * j + '" ' + selected + '>' + limitpage * j + '</option>';
        }
        htmlPaging += '</select>';
        htmlPaging += '</div>';
        htmlPaging += '</div>';
        $('#paging').append(htmlPaging);
    }
}
function fn_htmlPaging(countData, limitpage) {
    var htmlPag = '';
    htmlPag += '<ul class="pagination"><li class="disabled prev"><a href="#">&lsaquo;</a></li>';
    for (var i = 1; i <= Math.ceil(countData / limitpage) ; i++) {
        var classActi = '';
        if (i == 1) {
            classActi = 'class="active"';
        }
        htmlPag += '<li ' + classActi + '><a attr-page="' + i + '" href="#">' + i + '</a></li>';
    }
    htmlPag += '<li class="next"><a href="#">&rsaquo;</a></li></ul>';
    return htmlPag;
}

function card() {
    var htmlListCard = '<optgroup label="THÔNG TIN"><option  value="5c00dc63c941482ab456a094">BẮT ĐẦU</option><option  value="5c00dc63c941482ab456a09e">CÂU MẶC ĐỊNH</option><option  value="5c00dc63c941482ab456a0a1">LIÊN HỆ</option><option  value="5c00dc63c941482ab456a0a2">CÁC CHUYÊN MỤC</option><option  value="5c00dc63c941482ab456a0a3">TRANG CHỦ</option></optgroup><optgroup label="MÔ-ĐUN"><option  value="5c00dc63c941482ab456a095">TIN MỚI</option><option  value="5c00dc63c941482ab456a096">THỜI SỰ</option><option  value="5c00dc63c941482ab456a097">THẾ GIỚI</option><option  value="5c00dc63c941482ab456a098">KINH DOANH</option><option  value="5c00dc63c941482ab456a099">STARTUP</option><option  value="5c00dc63c941482ab456a09a">GIẢI TRÍ</option><option  value="5c00dc63c941482ab456a09b">THỂ THAO</option><option  value="5c00dc63c941482ab456a09c">PHÁP LUẬT</option><option  value="5c00dc63c941482ab456a09d">GIÁO DỤC</option><option  value="5c00dc63c941482ab456a09f">CHAT VỚI ADMIN</option><option  value="5c00dc63c941482ab456a0a0">GỬI PHẢN HỒI</option></optgroup><optgroup label="ok"></optgroup><optgroup label="Nhóm 4"></optgroup>';    return htmlListCard;
}