//function
function addCart() {
    var productId = $(this).attr('data-id')
    $.ajax({
        type: 'post',
        url: '/cart/AddToCart/?productId=' + productId,
        success: function (data) {
            handleResponse(data)
        }
    });
}

function confirmRemove() {
    var productId = $(this).attr('data-id')
    var productName = $(this).attr('data-name')
    bootbox.confirm(`are you sure to delete ${productName}`, function (result) {
        if (result) {
            
            removeItem(productId)
        }
    })
}
function removeItem(productId) {
    
    $.ajax({
        type: 'post',
        url: '/cart/RemoveFromCart/?productId=' + productId,
        success: function (data) {
            handleResponse(data)
        }
    });
}
function handleResponse(data) {
    if (data.statusCode == 200) {
        $.notify(data.message, "success")
        renderCart()
    }
    else {
        $.notify(data.message, "error")
    }
}

function renderCart() {
    $.ajax({
        type: 'post',
        url: '/cart/CartPartial',
        success: function (data) {
            $('#cart-partial').html(data)

        }
    });
}
//binding event
renderCart()
$('body').on('click', '.add-to-cart-btn', addCart);
$('body').on('click', '.remove-item', confirmRemove);

