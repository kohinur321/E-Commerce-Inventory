

function getOrderPending() {
    $.ajax({
        type: "GET",
        url: "/Admin/Admin/GetPendingOrder",
        data: "{}",
        success: function (response) {
            $('#pendingOrder').append(response.length);
        }
    });
}
function getOrderComplete() {
    $.ajax({
        type: "GET",
        url: "/Admin/Admin/GetCompleteOrder",
        data: "{}",
        success: function (response) {
            $('#completeOrder').append(response.length);
        }
    });
}
