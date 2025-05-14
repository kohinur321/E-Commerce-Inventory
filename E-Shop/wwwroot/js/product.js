
var item = 1;
var allCartItems = [];
var rowTableIdx = 0;
var grandPrice = 0;
function dataInit() {
    $('#qtyNo').text(item);
}

//Increcent decrement in cart
$('#increment, #decrement').click(function () {
    item += $(this).attr('id') === 'increment' ? 1 : item > 1 ? -1 : 0;
    dataInit();
});

///User save data request
$('#buyNow').click(function () {
    buyRequest();
});
function buyRequest() {
    var data = new FormData();
    data.append('CartId', 0);
    data.append('ProductId', $('#productId').val());
    data.append('Quantity', item);
    data.append('TotalPrice', item * parseInt($('#price').val()));

    $.ajax({
        processData: false,
        contentType: false, 
        type: "POST",
        url: "/User/Home/Save",
        data: data,
        success: function (response) {
            alertify.notify(response.message, 'success', 1, function () { window.location = "/User/Home" });
        }
    });
}

///Get All item in cart
function GetCartItem() {
    $.ajax({
        type: "GET",
        url: "/User/Home/GetBuyItem",
        data: "{}",
        success: function (response) {
            var items = response;
            allCartItems = response;
            $('#cart').text(items.length);
        }
    })
};

///show data in modal popup
$('#cartdetails').click(function () {
    $('#cartModal').modal('show');
    getOrderItemDetails();
});

///remove data in popup and show confirm message
$('#itemsbody').on('click', '.remove', function () {
    var cartId = parseInt($(this).closest('tr').find('[id^="cartId"]').text());
    $.confirm({
        title: 'Are you sure want to delete?',
        content: 'You will not be able to recover this item.',
        type: 'red',
        buttons: {
            yes: {
                btnClass: 'btn-danger',
                keys: ['enter'],
                action: function () {
                    $.ajax({
                        type: "GET",
                        url: "/User/Home/Delete?id=" + cartId,
                        data: "{}",
                        success: function (response) {
                            if (response.success) {
                                getOrderItemDetails();
                            }
                        }
                    });
                }
            },
            no: function () {
            }
        }
    });
})


///get item details
function getOrderItemDetails() {
    $.ajax({
        type: "GET",
        url: "/User/Order/GetCartItem",
        data: "{}",
        success: function (response) {
            var items = response;
            $('#cart').text(items.length);
            $('#itemsbody').empty();
            grandPrice = 0;

            for (var i = 0; i < items.length; i++) {
                $('#itemsbody').append(
                    `<tr id = "Item${++rowTableIdx}" >
                    <td hidden id="cartId${rowTableIdx}">` + items[i].cartId + `</td>
                    <td id="images${rowTableIdx}"><img src="/images/${items[i].product.imageUrl}" style='height: 50px; width: 50px' ></td>
                    <td id="name${rowTableIdx}">` + items[i].product.name + `</td>
                    <td id="quantity${rowTableIdx}">` + items[i].quantity + `</td>
                    <td id="price${rowTableIdx}">` + items[i].product.price + `</td>
                    <td id="totalPrice${rowTableIdx}">` + items[i].totalPrice + `</td>

                    <td class="text-center">
                        <button class="btn btn-sm  remove" type="button"> <i class="fa-solid fa-xmark text-danger" aria-hidden="true" title="delete"></i </button>
                    </td>
                </tr>`
                )
                grandPrice += items[i].totalPrice;
                $('#grandTotal').text(grandPrice);
            }
        }
    })
}

$('#checkOut').click(function () {
    window.location = "/User/Order"
});

//user save final data 
$('#placeOrder').click(function () {
    SaveRequest();
});

function SaveRequest() {
    if (!ValidationCheck()) { return }

    var data = new FormData();
    data.append('CustomerId', 0);
    data.append('FirstName', $('#fname').val());
    data.append('LastName', $('#lname').val());
    data.append('Email', $('#email').val());
    data.append('Address', $('#address').val());

    $.ajax({
        processData: false,
        contentType: false,
        type: "POST",
        url: "/User/Order/Save",
        data: data,
        success: function (response) {
            alertify.notify(response.message, 'success', 1, function () { window.location = "/User/Home" });
        }
    });
};

///check validation for user form
function ValidationCheck() {
    var response = true;

    var fname = $('#fname').val();
    var lname = $('#lname').val();
    var email = $('#email').val();
    var address = $('#address').val();

    showErrorMessageBelowCtrl('fname', 'First Name is required', false);
    showErrorMessageBelowCtrl('lname', 'Last is required', false);
    showErrorMessageBelowCtrl('email', 'Email is required', false);
    showErrorMessageBelowCtrl('address', 'Address is required', false);

    if (fname == undefined || fname == '') {
        showErrorMessageBelowCtrl('fname', 'First Name is required', true); response = false;
    }
    else {
        showErrorMessageBelowCtrl('fname', 'First Name is required', false);
    }

    if (lname == undefined || lname == '') {
        showErrorMessageBelowCtrl('lname', 'Last  Name is required', true); response = false;
    }
    else {
        showErrorMessageBelowCtrl('lname', 'Last  Name is required', false);
    }

    if (email == undefined || email == '') {
        showErrorMessageBelowCtrl('email', 'Email is required', true); response = false;
    }
    else {
        showErrorMessageBelowCtrl('email', 'Email is required', false);
    }

    if (address == undefined || address == '') {
        showErrorMessageBelowCtrl('address', 'Adress is required', true); response = false;
    }
    else {
        showErrorMessageBelowCtrl('address', 'Address is required', false);
    }

    return response;
}
