
$('#btn-search-product').on('click', function (e) {
    e.preventDefault()
    var keyWord = $('#txt-search-product').val();
    location.href = '/product/index?keyWord=' + keyWord

})
