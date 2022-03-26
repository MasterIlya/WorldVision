
function ShowMore() {
    const elementsForPage = 5;
    $.ajax({
        dataType: "json",
        url: "/Reviews/GetMoreComments",
        method: "GET",
        data: {
            "skip": skip,
            "reviewId": reviewId
        },
        success: function (comments) {
            if (comments.length != 0) {
                for (var i = 0; i < comments.length; i++) {
                    let elem1 = document.createElement("blockquote");
                    let elem2 = document.createElement("p");
                    let elem3 = document.createElement("cite");
                    elem2.appendChild(document.createTextNode(comments[i].content))
                    let str = comments[i].name + ". " + comments[i].createDate
                    elem3.appendChild(document.createTextNode(str))
                    elem1.appendChild(elem2)
                    elem1.appendChild(elem3)
                    document.getElementById("comments").appendChild(elem1);
                }
            }
            if (comments.length != elementsForPage) {
                document.getElementById("showMore").remove();
            }
            skip += comments.length;d
        },
    });

}
