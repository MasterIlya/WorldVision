document.querySelector("#third").onclick = function () {
    var users = []
    var checkboxes = document.querySelectorAll('input[type=checkbox]:checked')

    for (var i = 0; i < checkboxes.length; i++) {
        users.push(checkboxes[i].value)
    }
    if (users.length != 0) {
        if (confirm("You definitely want to delete the marked users!")) {
        }
        else {
            return false
        }
    }
    else {
        alert("Select users before you delete them!");
        return false
    } 
}