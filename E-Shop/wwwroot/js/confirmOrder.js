
function getInit() {
    var id = $('#OrderId').val();
    $.ajax({
        type: "GET",
        url: "/Admin/Order/GetInit?id=" + id,
        data: "{}",
        success: function (response) {
            var items = response;
            $('#itemsbody').empty();
            for (var i = 0; i < items.length; i++){
                $('#itemsbody').append(
                    `<tr>
                        <td>` + items[i].product.name + `</td>
                        <td>` + items[i].product.unit + `</td>
                        <td>` + items[i].product.price + `</td>
                        <td>` + items[i].quantity + `</td>
                        <td>` + items[i].totalPrice + `</td>
                    </tr>`
                );
            }
        }
    });
}

