
function openErrorModal(strMessage) {
    var myDiv = document.getElementById("MyModalErrorAlertBody");
    myDiv.innerHTML = strMessage;
    $('#myModalError').modal('show');
}
function openSuccessModal(strMessage) {
    var myDiv = document.getElementById("MyModalSuccessAlertBody");
    myDiv.innerHTML = strMessage;
    $('#myModalSuccess').modal('show');
}
function showErrorMessageBelowCtrl(ctrlId, message, show) {

    var divHtml = '<div style="color:red;" id="' + ctrlId + '_err_div" >' + message + '</div>';
    if (show == true) {
        $('#' + ctrlId).addClass("highlight");
        $('#' + ctrlId).after(divHtml);
    } else {
        $('#' + ctrlId).removeClass("highlight");
        $('#' + ctrlId + '_err_div').remove();
    }
}