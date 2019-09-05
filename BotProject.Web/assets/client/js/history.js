var pageSize = 10;
$(document).ready(function () {
    getDataTable(1, pageSize);
    // select number show table
    $("select#results-pp").change(function () {
        pageSize = $(this).children("option:selected").val();
        getDataTable(1, pageSize);
    });
})


//date-format:2019-09-05 11:16:20.227
function DateVN(date) {
    date = date.substring(0, date.length - 4);
    let y = date.substring(0, 4),
        m = date.substring(5, 7),
        d = date.substring(8, 10),
        time = date.substring(11, date.length);
    let dateVN = time + " " + d + "-" + m + "-" + y;
    return dateVN;
}

function getDataTable(page, pageSize) {
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
