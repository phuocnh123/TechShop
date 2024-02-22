$('#frm-save').validate({
    rules: {
        //CategoryId:
        //{
        //    required: true,
        //},
        Name:
        {
            required: true,
        },
        Images:
        {
            required: true,
        },
    },
    messages: {

        Name:
        {
            required: "you must input product name",
        },
        
        Images:
        {
            required: "you must select images for uploading",
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

