const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/comments")
    .withAutomaticReconnect()
    .configureLogging(signalR.LogLevel.Information)
    .build();


$(function () {
    $('form').submit(function (e) {
        $.ajax({
            dataType: "json",
            url: "/Reviews/CreateComment",
            method: "POST",
            data: {
                "ReviewId": document.forms["form"].elements["reviewId"].value,
                "Email": document.forms["form"].elements["email"].value,
                "Content": document.forms["form"].elements["content"].value
            }
        });
        e.preventDefault();
    });
});

function sendform(e) {
    $.ajax({
        dataType: "json",
        url: "/Reviews/CreateComment",
        method: "POST",
        data: {
            "ReviewId": document.forms["form"].elements["reviewId"].value,
            "Email": document.forms["form"].elements["email"].value,
            "Content": document.forms["form"].elements["content"].value
        }

    });
    e.preventDefault();
}
   
hubConnection.on("Send", function (model) {
    skip++;

    let elem1 = document.createElement("blockquote");
    let elem2 = document.createElement("p");
    let elem3 = document.createElement("cite");
    elem2.appendChild(document.createTextNode(model.content))
    let str = model.name + ". " + model.createDate
    elem3.appendChild(document.createTextNode(str))
    elem1.appendChild(elem2)
    elem1.appendChild(elem3)
    let firstElem = document.getElementById("comments").firstChild;
    document.getElementById("comments").insertBefore(elem1, firstElem);
    let elem = document.getElementById("noComment");
    elem.remove()
 
});
 
hubConnection.start();