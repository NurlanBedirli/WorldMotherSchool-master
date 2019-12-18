    document.addEventListener("DOMContentLoaded", function () {
        $.ajax({
            url: "/momsch/Ajax/ResourcesViewAjax",
            type: "Post",
            dataType: "Json",
            success: function (respons) {
                $("#resources").remove('option');
                if (respons.message = 200) {
                    for (var elm of respons.data) {
                        var option = document.createElement('option');
                        option.text = elm.name;
                        option.value = elm.id;
                        $("#resources").append(option);
                    }
                }
                if (respons.message = 400) {
                    $("#ViewName").text = "Bosdu";
                }
            }
        });

        $.ajax({
            url: "/momsch/Ajax/GetRoleAjax",
            type: "Post",
            dataType: "Json",
            success: function (respons) {
                $("#role").remove('option');
                if (respons.message = 200) {
                    for (var elm of respons.role) {
                        var option = document.createElement('option');
                        option.text = elm.name;
                        $("#role").append(option);
                    }
                }
                if (respons.message = 400) {
                    $("#ViewName").text = "Bosdu";
                }
            }
        });
      

        $("#resources").on("change", function () {
            var Id = $(this).val();
            var culture = $("#inputState").val();
            var edit = $("div #Edit");
            edit.attr("href", "/momsch/Panel/EditView/" + Id +"?culture=" + culture);
        });

        $("#inputState").on("change", function () {
            var culture = $(this).val();
            var Id = $("#resources").val();
            var edit = $("div #Edit");
            edit.attr("href", "/momsch/Panel/EditView/" + Id + "?culture=" + culture);
        });

        $(".form-group").on("click","div #delete", function () {
            var parent = $(this).parent().parent("div");
            var fileName = parent.find("img.imageDelete").attr('src').split('img/').pop();
            parent.remove();
            $.ajax({
                url: "/momsch/Ajax/DeletePhotoDataBase?photo=" + fileName,
                type: "Post",
                contentType: false,
                processData: false,
                success: function (response) {
                    if (response.message == 200) {
                        $("#errors").text = "Silindi";
                    }
                }
            });
        });

});
