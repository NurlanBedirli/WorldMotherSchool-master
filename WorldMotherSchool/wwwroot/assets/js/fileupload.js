document.addEventListener("DOMContentLoaded", function () {

    document.getElementById("fileimage").addEventListener("change", function () {

        var data = $(this).get(0).files;

        var filereader = new FileReader();
        filereader.readAsDataURL(fileimage.files[0]);
        filereader.onload = function () {
            document.getElementById("img").setAttribute("src", filereader.result);
        }

        var val = $(this).val();
        var text = val.substring(val.lastIndexOf("\\")+1,val.length);
        var input = document.getElementById("filetext");
        input.value = text;
    });
});