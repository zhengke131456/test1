function check() {
    $('#nmerror').html("");
    $('#pderror').html("");
    if ($('#username').val() == "" || $('#username').val() == "用户名")
    { $('#nmerror').html("请输入用户名！！"); return false; }
    if ($('#password').val() == "")
    { $('#pderror').html("请输入密码！！"); return false; }
    var chb = "";
    if (document.getElementById("chb").checked) {
        chb = "true";

    }
    else {
        chb = "false";
    }
    $.post("../Handler/ajaxlogin.ashx?um=" + encodeURI($('#username').val()) + "&pd=" + encodeURI($('#password').val()) , { pdq: "" + chb + "" },
              function (data) {
                  if (data == 1) {
                      window.location.replace('../default.aspx');
                  }
                  else {
                      alert("用户名密码错误！");
                      //  $('#Login1_ErrorInfo').html("<b>&nbsp;</b><span id=\"Login1_FailureText\">用户名密码错误！</span>");
                  }
              }, "txt");
} 
function cz() {
    $('#username').val("");
    $('#password').val("");
    $('#nmerror').html("");
    $('#pderror').html("");
}