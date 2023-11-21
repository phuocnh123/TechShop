
$('#frm-customer-info').validate({
    rules: {
        FirstName:
        {
            required: true,
        },
        LastName:
        {
            required: true,
        },
        Email:
        {
            required: true,
        },
        Address:
        {
            required: true,
        },
        PhoneNumber:
        {
            required: true,
        },
        PaymentMethod:
        {
            required: true,
        },

    },
    messages: {
        FirstName:
        {
            required: "you must input firt name",
        },
        LastName:
        {
            required: "you must input last name",
        },
        Email:
        {
            required: "you must input email",
        },
        Address:
        {
            required: "you must input address",
        },
        PhoneNumber:
        {
            required: "you must input phone number",
        },

        PaymentMethod:
        {
            required: "you must choose the payment method",
        },

    }
});


function placeOrder(e) {
    e.preventDefault()
    if ($('#frm-customer-info').valid()) {
        var paymentMethod = Number($('#txt-payment-method').val())
        if (paymentMethod <= 0 || paymentMethod > 3) {
            $.notify("please select one payment method", "warn")
            return
        }
        var checkTerm = $('#terms').prop("checked");
        if (!checkTerm) {
            $.notify("please accept the term", "warn")
            return
        }
        
        var form = $('#frm-customer-info');
        form.submit();
    }
};

function addCheckoutMethodValue() {
    debugger
    var value = $(this).val();
    $('#txt-payment-method').val(value);
}
function init() {
  
    var message = $('#check-out-message').text();
    var status = Number( $('#check-out-status').text()) ;
    if (message != '') {
        var messageType = status == 200 ? "success": "error"
        $.notify(message, messageType);
    }
}
init();
$('body').on('click', '#btn-place-order', (e) => placeOrder(e));
$('body').on('input', '.input-radio input', addCheckoutMethodValue);