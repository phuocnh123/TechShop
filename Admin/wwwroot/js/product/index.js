
var homeconfig = {
    pageSize: 10,
    pageIndex: 1
}

var dataTable =
{
    loadData: function (changePageSize) {

        var total = 0;
        var model = new Object();
        model.PageSize = homeconfig.pageSize
        model.PageIndex = homeconfig.pageIndex;
        model.CategoryId = $('#categoryId').val()
        model.KeyWord = $('#keyword').val()

        $.ajax({
            type: 'post',
            url: '/Product/CountPagination',
            dataType: 'json',
            data: JSON.stringify(model),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                total = parseInt(data.result);

            }
        });
        $.ajax({
            type: 'post',
            url: '/Product/GetPagination_Pta',
            data: JSON.stringify(model),
            contentType: "application/json; charset=utf-8",
            success: function (data) {

                $("#table-content").html(data);
                dataTable.paging(total, function () {

                }, changePageSize);
            }
        });
    },
    paging: function (totalRow, callback, changePageSize) {

        var totalPage = 0;
        if (totalRow < homeconfig.pageSize) {
            totalPage = 1
        }
        else {
            totalPage = Math.ceil(totalRow / homeconfig.pageSize);
        }
        if ($('#pagination a').length === 0 || changePageSize === true) {
            $('#pagination').empty();
            $('#pagination').removeData("twbs-pagination");
            $('#pagination').unbind("page");
        }
        $('#pagination').twbsPagination({
            totalPages: totalPage,
            first: "<<",
            next: ">",
            last: ">>",
            prev: "<",
            visiblePages: 3,
            onPageClick: function (event, page) {

                homeconfig.pageIndex = page;
                dataTable.loadData();

            }
        });
    },
}

dataTable.loadData();

function filter() {
    dataTable.loadData(true)
}
$('body').on('change', '#categoryId', filter);
$('body').on('click', '#btn-search', filter);

