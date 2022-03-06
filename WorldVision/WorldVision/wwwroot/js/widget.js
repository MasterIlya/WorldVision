Dropzone.autoDiscover = false
let tag = "yes"
const email = document.getElementById("hiddenEmail").value
const pageId = document.getElementById("pageId").value

var myDropzone = new Dropzone(".dropzone", {
    addRemoveLinks: true,

    removedfile: function (file) {
        var formData = new FormData()
        if (tag == "yes") {
            formData.append("file", file)
            formData.append("email", email)
            formData.append("pageId", pageId)
            $.ajax({
                cache: false,
                processData: false,
                contentType: false,
                dataType: "json",
                url: "/Reviews/DeleteImage",
                method: "POST",
                data: formData,
            });
        }
        else if (tag == "update") {
            formData.append("imageId", file.imageId)
            formData.append("fileName", file.name)
            $.ajax({
                cache: false,
                processData: false,
                contentType: false,
                dataType: "json",
                url: "/Reviews/DeleteImageOnUpdate",
                method: "POST",
                data: formData,
            });
        }
        else {
            tag = "yes"
            let mySpan = document.getElementById("except")
            mySpan.innerHTML = "A file with the name" + name + "has already been added"
        }

        var _ref

        return (_ref = file.previewElement) != null ? _ref.parentNode.removeChild(file.previewElement) : void 0;
    }
});

myDropzone.on("addedfile", function (file) {
    if (this.files.length) {
        var _i, _len
        for (_i = 0, _len = this.files.length; _i < _len - 1; _i++) // -1 to exclude current file
        {
            if (this.files[_i].name === file.name && this.files[_i].size === file.size && this.files[_i].lastModifiedDate.toString() === file.lastModifiedDate.toString()) {
                tag = "no"
                this.removeFile(file)
            }
        }
    }
});

myDropzone.on("sending", function (file, xhr, formData) {
    formData.append("email", email)
    formData.append("pageId", pageId)
});

