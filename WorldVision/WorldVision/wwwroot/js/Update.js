var files = []
var urls = []

tag = "update"
for (i = 0; i < existingFiles.length; i++) {
    files.push({ name: existingFiles[i].imageName, size: existingFiles[i].imageSize, dataURL: existingFiles[i].imageURL, imageId: existingFiles[i].imageId })
    urls.push({ url: existingFiles[i].imageURL })
}



for (i = 0; i < existingFiles.length; i++) { 
    let callback = null;
    let crossOrigin = null; 
    let resizeThumbnail = false
    myDropzone.displayExistingFile(files[i], urls[i].url, callback, crossOrigin, resizeThumbnail);
    myDropzone.files.push(files[i]);
}


