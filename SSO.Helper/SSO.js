function LogIn() {
    var urlList = arguments;
    for (var url in urlList) {
        $.ajax({
            url: url,
            dataType: "jsonp",
            jsonp: "callback",
            success: function (data) {
                console.log(data)
            }
        });
    }
}