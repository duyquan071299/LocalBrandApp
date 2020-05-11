var product = {
    init: function () {
        product.registerEvents();
    },

    registerEvents: function () {
        $('.btn-images').off('click').on("click", function (e) {
            e.preventDefault();
            $('#imagesManage').modal('show');
            $('#hidProductID').val($(this).data('id'));
          
            product.loadImages();
        });
        //$('.btn-active').off('click').on('click', function (e) {
        //    e.preventDefault();
        //    var btn = $(this);
        //    var id = btn.data('id');
        //    $.ajax({
        //        url: "/Admin/ProductDetail/ChangeStatus",
        //        data: { id: id },
        //        dataType: "json",
        //        type: "POST",
        //        success: function (response) {
        //            console.log(response);
        //            if (response.status == true) {
        //                btn.text('Kích hoạt');
        //            }
        //            else {
        //                btn.text('Khoá');
        //            }
        //        }
        //    });
        //});

        $('#btnchooseimage').off('click').on('click', function (e) {
            e.preventDefault();
            var finder = new CKFinder();
            finder.selectActionFunction = function (url) {
                $('#imageList').append("<div> <img src='" + url + "'width=100>" +
                    '<a href="#" class="btn-del"><i class ="fa fa-times" ></i></a></div> ');
                $('.btn-del').off('click').on('click', function (e) {
                    e.preventDefault();
                    $(this).parent().remove();
                });
            };

            finder.popup();
        })

        $('#btnSaveImages').off('click').on("click", function (e) {
            var images = [];
            var id = $('#hidProductID').val();

            $.each($('#imageList img'), function (i, item) {
                images.push($(item).prop('src'));
            })
            $.ajax({
                url: '/Admin/ProductDetail/SaveImages',
                type: 'POST',
                data: {
                    id: id,
                    images: JSON.stringify(images)
                },
                dataType: 'json',
                success: function (res) {
                    if (res.status) {
                        $('#imagesManage').modal('show');
                        alert("Lưu ảnh thành công");
                    }
                    else {
                        alert("Lưu thất bại");
                    }

                }
            });
        });
    },

    loadImages: function () {
        $.ajax({
            url: '/Admin/ProductDetail/LoadImages',
            type: 'GET',
            data: {
                id: $('#hidProductID').val()
            },
            dataType: 'json',
            success: function (response) {

                var data = response.data;
                console.log(data);
                var html = '';

                $.each(data, function (i, item) {
                    html += "<div> <img src='" + item + "'width=100>" +
                        '<a href="#" class="btn-del"><i class ="fa fa-times" ></i></a></div> ';
                });

                $('#imageList').html(html);

                $('.btn-del').off('click').on('click', function (e) {
                    e.preventDefault();
                    $(this).parent().remove();
                });

                //thong bao thanh cong
            }
        });
    },
}

product.init();