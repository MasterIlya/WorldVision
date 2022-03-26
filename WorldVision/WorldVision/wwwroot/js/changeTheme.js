function changeCSS() {
    var checkbox = document.getElementById("Theme")
    var theme;
    if(checkbox.checked)
    {
        theme = "dark"
    }
    else
    {
        theme = "light"
    }
    var url = location.href
    $.post("/Home/SetTheme",
        {
            data: theme,
            url: url
        },
        add
    );
    function add() { location.reload(); }

}