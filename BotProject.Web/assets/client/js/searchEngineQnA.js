﻿//############ Table and Search Engine #############//
var pageSize = 10;
//var allowed = true;
var debounceTimeout = null;
$(document).ready(function () {
    // init data
    getDataTable(1, pageSize);

    $("#btnSearch").off().on('click', function () {
        var content = $("#search-terms").val();
        search(content);
        return false;
    })

    // writing show suggest
    $("#search-terms").keyup(function (e) {
       // e.preventDefault();
        //if (e.keyCode == 13) {
        //    return false;
        //}
        if (e.keyCode == 40 || e.keyCode == 38) {
            $(this).focusToEnd();
            return false;
        }

        clearTimeout(debounceTimeout);

        debounceTimeout = setTimeout(getSuggest($(this).val()), 500);


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

function getDataTable(page, pageSize) {
    var param = {
        page: page,
        pageSize: pageSize
    }
    param = JSON.stringify(param)
    $.ajax({
        url: _Host + 'apiv1/GetAll',
        contentType: 'application/json; charset=utf-8',
        data: param,
        type: 'POST',
        success: function (result) {
            new renderTemplate(result).Table();
        },
    });
}

function getSuggest(content) {

    if (content.trim() == "") {
        $("#div-suggest").css('display', 'none');
        return false;
    }
    var param = {
        text: content,
        isAccentVN: true
    }
    param = JSON.stringify(param)
    $.ajax({
        url: _Host + 'apiv1/Suggest',
        contentType: 'application/json; charset=utf-8',
        data: param,
        type: 'POST',
        success: function (result) {
            new renderTemplate(result).Suggest(content);
        },
    });
}

function search(content) {

    $("#div-suggest").css('display', 'none');
    if (content.trim() == "") return false;
    if (content.trim() == sessionStorage.getItem("_search").trim()) return false;
    var param = {
        text: content,
        isAccentVN: true
    }
    param = JSON.stringify(param)
    $.ajax({
        url: _Host + 'apiv1/Search',
        contentType: 'application/json; charset=utf-8',
        data: param,
        type: 'POST',
        success: function (result) {
            new renderTemplate(result.Items).Search();
            sessionStorage.setItem("_search", content);
        },
    });
}

function addQnA() {
    var questionContent = $("#txtQuestion").text();
    var answerContent = $("#txtAnswer").text();

    if (questionContent == "" && answerContent == "") return;
    if (questionContent == "") return;

    var param = {
        question: question,
        answer: answer
    }
    //$.ajax({

    //})

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
                html += '<td>' + item.Body + '</td>';
                html += '<td></td>';
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
    },
    this.Suggest = function (content) {
        var html = '';
        if (data.length != 0) {
            $("#div-suggest").css('display', 'block');
            html += '<li class="sbct" jsaction="touchstart:.CLIENT;touchend:.CLIENT;touchcancel:.CLIENT;touchmove:.CLIENT;contextmenu:.CLIENT" style="display:none">';
            html += '<div class="suggestions-inner-container">';
            html += '<div class="sbtc" role="option">';
            html += '<div class="sbl1"><span>' + content + '</span></div>';
            html += '</div>';
            html += '</div>';
            html += '</li>';
            $.each(data, function (index, item) {
                html += '<li class="sbct" jsaction="touchstart:.CLIENT;touchend:.CLIENT;touchcancel:.CLIENT;touchmove:.CLIENT;contextmenu:.CLIENT">';
                html += '<div class="suggestions-inner-container">';
                html += '<div class="sbtc" role="option">';
                html += '<div class="sbl1"><span>' + item + '</span></div>';
                html += '</div>';
                //html += '<div class="sbab" style="display: none;"><div class="sbai">Xóa</div></div>';
                html += '</div>';
                html += '</li>';
            })
        } else {
            $("#div-suggest").css('display', 'none');
        }
        // append list suggest
        $("#ul-suggest").empty().append(html);

        // arrow keyup select text suggest
        var $listItems = $('li.sbct'),
            $current;
        var isFocusFirstLiHidden = true;
        $("#search-terms").keydown(function (e) {
            var key = e.keyCode,
                $selected = $listItems.filter('.suggest-selected');

            if (e.keyCode == 13) {
                e.stopImmediatePropagation();
                $("#btnSearch").click();
                //outsite focus tag input search
                $("#search-terms").blur();
                return false;
            }
            if (key != 40 && key != 38) {

                return;
            }

            $listItems.removeClass('suggest-selected');

            if (document.getElementById('ul-suggest') !== null) {
                if (key == 40) // Down key
                {
                    if (!$selected.length || $selected.is(':last-child')) {
                        if (isFocusFirstLiHidden) {// xử lý lần đầu key up bỏ qua thẻ li đầu tiên
                            $current = $listItems.eq(1);
                            isFocusFirstLiHidden = false;
                        } else {
                            $current = $listItems.eq(0);
                        }
                    }
                    else {
                        $current = $selected.next();
                    }
                }
                else if (key == 38) // Up key
                {
                    if (!$selected.length || $selected.is(':first-child')) {
                        $current = $listItems.last();
                    }
                    else {
                        $current = $selected.prev();
                    }
                }

                $current.addClass('suggest-selected');
                $(this).val($current.addClass('suggest-selected').text());
                $(this).focusToEnd();
            }
        })

        // select text suggest to search
        $(".sbtc").off().on('click', function () {
            var content = $(this).text();
            $("#search-terms").val(content);
            search(content);
        })
    },
    this.Search = function () {
        $("#header-search").css('display', 'block');
        var html = '';
        if (data.length != 0) {
            $.each(data, function (index, item) {
                html += '<li class="list-group-item"><a href="#">' + item.Body + '</a></li>';
            })
        } else {
            html += '<li class="list-group-item">Không tìm thấy</li>';
        }
        $("#ul-list-search").empty().append(html);
    }
}

// set cursor to end position textarea
$.fn.focusToEnd = function () {
    return this.each(function () {
        var v = $(this).val();
        $(this).focus().val("").val(v);
    });
};


//########## Open & Close Form Search ###########//
var ismobile = navigator.userAgent.match(/(iPad)|(iPhone)|(iPod)|(android)|(webOS)/i) != null
var touchorclick = (ismobile) ? 'touchstart' : 'click'
var searchcontainer = document.getElementById('searchcontainer')
var searchfield = document.getElementById('search-terms')
var searchlabel = document.getElementById('search-label')

searchlabel.addEventListener(touchorclick, function (e) { // when user clicks on search label
    searchcontainer.classList.toggle('opensearch') // add or remove 'opensearch' to searchcontainer
    if (!searchcontainer.classList.contains('opensearch')) { // if hiding searchcontainer
        searchfield.blur() // blur search field
        e.preventDefault() // prevent default label behavior of focusing on search field again
    }
    e.stopPropagation() // stop event from bubbling upwards
}, false)

searchfield.addEventListener(touchorclick, function (e) { // when user clicks on search field
    e.stopPropagation() // stop event from bubbling upwards
}, false)

document.addEventListener(touchorclick, function (e) { // when user clicks anywhere in document
    if (e.target.className == "close-form-search") {
        searchcontainer.classList.remove('opensearch')
        searchfield.blur()
    }

}, false)


//Import excel QnA
importExcelQnA = function () {
    if ($("#file").val() == '') {
        alert("Bạn chưa chọn file để tải lên");
    }
    else if (!checkFileExtension()) {
        alert("Bạn chỉ được tải lên file excel với định dạng .xls hoặc .xlsx");
    }
    else {
        // Checking whether FormData is available in browser  
        if (window.FormData !== undefined) {

            var fileUpload = $("#file").get(0);
            var files = fileUpload.files;

            // Create FormData object  
            var fileData = new FormData();

            // Looping over all files and add it to FormData object  
            for (var i = 0; i < files.length; i++) {
                fileData.append(files[i].name, files[i]);
            }

            // Adding one more key to FormData object  
            //fileData.append('username', 'QALaw');

            $.ajax({
                url: _Host + 'api/ImportExcelQnA',
                type: "POST",
                contentType: false,
                processData: false,
                data: fileData,
                beforeSend: function () {
                    $("body").append('<div class="wt-waiting wt-fixed wt-large"></div>');
                },
                complete: function () {
                    // Handle the complete event
                    $(".wt-waiting").remove();
                },
                success: function (result) {
                    console.log(result);
                    //if (result.listques != null) {
                        //listQues = result.listques;
                        //AlertDialog("Thông báo", result.status, function () { });

                    //} else {
                        //AlertDialog("Thông báo", result, function () { });
                    //}
                },
                error: function (err) {
                }
            });
        } else {
            alert("FormData is not supported.");
        }
    }
}

checkFileExtension = function () {
    // get the file name, possibly with path (depends on browser)
    var filename = $("#file").val();
    // Use a regular expression to trim everything before final dot
    var extension = filename.replace(/^.*\./, '');
    if (extension == filename) {
        extension = '';
    } else {
        extension = extension.toLowerCase();
    }
    switch (extension) {
        case 'xls':
        case 'xlsx':
            return true;
    }
    return false;
}
