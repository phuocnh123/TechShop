
$('#frm-save').validate({
    rules: {
        CategoryId:
        {
            required: true,
        },
        Name:
        {
            required: true,
        },
        Price:
        {
            required: true,
        },
       
        Detail:
        {
            required: true,
        },

        Quantity:
        {
            required: true,
        },

        Description:
        {
            required: true,
        },
        Images:
        {
            required: true,
        },
    },
    messages: {
        CategoryId:
        {
            required: "you must choose the category",
        },
        Name:
        {
            required: "you must input product name",
        },
        Price:
        {
            required: "you must input price",
        },
      
        Detail:
        {
            required: "you must input product detail",
        },

        Description:
        {
            required: "you must input product description",
        },
        Images:
        {
            required: "you must select images for uploading",
        },

        Quantity:
        {
            required: "you must input product quantity"
        },

    }
});


function save(e) {
    e.preventDefault()
    if ($('#frm-save').valid()) {
        var a = $('#frm-save');
        a.submit();
    }
};

$('body').on('click', '#btn-save', (e) => save(e));

