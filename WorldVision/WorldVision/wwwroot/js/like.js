const email = document.getElementById("email").value
var a = btn
function like(value) {
    if (a == "like") {
        a ="dislike"
        $.ajax({
            dataType: "json",
            url: "/Reviews/AddLike",
            method: "POST",
            data: {
                reviewId: value,
                email: email
            },
            success: UpdateTolike(),
        });
    }
    else {
        a = "like"
        $.ajax({
            dataType: "json",
            url: "/Reviews/RemoveLike",
            method: "POST",
            data: {
                reviewId: value,
                email: email
            },
            success: UpdateTodislike(),
        });
    }

  }

function UpdateTolike() {
/*    let newImg = document.createElement("img")*/
    //newImg.src = "https://res.cloudinary.com/dtomjloda/image/upload/v1646919016/like_kyrkdt.png"
    //newImg.height = 39
    //newImg.width = 45
    document.getElementById("like").src = "https://res.cloudinary.com/dtomjloda/image/upload/v1646919016/like_kyrkdt.png"

}

function UpdateTodislike() {
/*    let newImg = document.createElement("img")*/
    //newImg.src = 
    //newImg.height = 39
    //newImg.width = 45
    document.getElementById("like").src = "https://res.cloudinary.com/dtomjloda/image/upload/v1646918477/like_kiw3rf.png"
}