function LogIn() {
    debugger;
    var urlList = arguments;
    for (var i = 1; i < urlList.length; i++) {
        CreateScript(urlList[i]);
    }
    window.location.href = urlList[0];
}

function CreateScript(src) {
    $("<script><//script>").attr("src", src).appendTo("body")
}