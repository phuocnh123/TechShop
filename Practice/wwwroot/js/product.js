var _model = new Object();
_model.PageSize = 2
_model.PageIndex = 1

// functions
function loadProduct(model, isLoadMore = false) {
    $.ajax({
        type: 'post',
        url: '/product/ProductPartial',
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (isLoadMore) {

                $('#product-list').append(data)
            }
            else {
                $('#product-list').html(data)
            }
        }
    });
}

function init() {
    loadProduct(_model);
}

function search() {
    _model.PageIndex = 1;
    _model.KeyWord = $('#txt-search').val();
    loadProduct(_model);
}

function loadmore() {
    _model.PageIndex ++;
    loadProduct(_model, true);
}

init()
$('body').on('click', '#btn-search', search);
$('body').on('click', '#btn-loadmore', loadmore);