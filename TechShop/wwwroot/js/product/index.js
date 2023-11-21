
var _model = new Object();
_model.PageSize = 9
_model.PageIndex = 1
_model.CategoryId = $('#txt-categoryId').text();
_model.KeyWord = $('#txt-keyword').text();

// functions
function loadProduct(model, isLoadMore = false) {
    $.ajax({
        type: 'post',
        url: '/product/ProductListPartial',
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (!isLoadMore) {
                $("#product-list-partial").html(data);
            }
            else {
                $("#product-list-partial").append(data);
            }
            
        }
    });
}

function init() {
    _model.CategoryId = $('#txt-categoryId').text();
    _model.KeyWord = $('#txt-keyword').text();
    loadProduct(_model)
}

function filterCategory() {
    _model.PageIndex = 1
    _model.CategoryId = $(this).val();
    loadProduct(_model)
}

function filterPrice() {
    _model.PageIndex = 1
    _model.FromPrice = Number($('#price-min').val());
    _model.ToPrice = Number($('#price-max').val());
    loadProduct(_model)
}
function orderProduct() {
    _model.PageIndex = 1
    _model.SortBy = Number( $('#product-order-by').val());
    loadProduct(_model)
}

function changePageSize() {
    _model.PageIndex = 1
    _model.PageSize = Number( $('#product-page-size').val());
    loadProduct(_model)
}

function loadMore() {
    _model.PageIndex += 1;
    loadProduct(_model, true)
}


// biding event
init();
$('body').on('change', '.input-radio input', filterCategory);
$('body').on('change', '#price-min', filterPrice);
$('body').on('change', '#price-max', filterPrice);
$('body').on('change', '#product-order-by', orderProduct);
$('body').on('change', '#product-page-size', changePageSize);
$('body').on('click', '#btn-load-more', loadMore);
