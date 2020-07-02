var product = {
    init: function () {
        product.registerEvents();
        product.loadImages();
        product.loadSizes();


    },

    registerEvents: function () {

        $('.btn-images').off('click').on("click", function (e) {
            e.preventDefault();
           // $('.size-item').dropdown.data
            $('#hidProductID').val($(this).data('id'));
            
            product.loadImages();
            product.loadSizes();
        });
    },

    loadImages: function () {
        $.ajax({
            url: '/Product/ProductDetail',
            type: 'GET',
            data: {
                id: $('#hidProductID').val()
            },
            dataType: 'json',
            success: function (response) {
                var data = response.data;
                var html = '';
                html += ' <div class="w3-content" style="max-width:1200px">';
                $.each(data, function (i, item) {
                    if (i == 0) html += ' <img class="mySlides" src = "' + item +
                        '" style = "width: 90%;height: 90%" >';
                    else
                        html += ' <img class="mySlides" src = "' + item +
                            '" style = "width: 90%;height: 90%;display:none" >';

                });
                html += ' <div class="w3-row-padding w3-section">';
                $.each(data, function (i, item) {
                    html += ' <div class="w3-col s4 mySlides"> <img class="img_load demo w3-opacity w3-hover-opacity-off" src="'
                        + item + '"width=50%;cursor:pointer" onclick="currentDiv(' + i + ')"' +
                        '<a href="#" class="btn-del"><i class ="fa fa-times" ></i></a></div> ';
                });

                html += ' </div>';
                html += ' </div>';
               

                $('#imageList').html(html);
                //thong bao thanh cong
            }
        });
    },
    loadSizes: function () {
        $.ajax({
            url: '/Product/SizeDetail',
            type: 'GET',
            data: {
                id: $('#hidProductID').val()
            },
            dataType: 'json',
            success: function (response) {

                var data = response.data;
                var html = '';
               // html += '<select name="size" class="size">';
                $.each(data, function (i, item) {
                    html += '<option value="'+item+'">'+item+'</option>';
                    
                    //$("#size").append(
                    //    $('<option></option>').val(item).text(item));
                });
                //html += '</select>';
                $('.size').html(html);
                
                //$(".sizebox").append(`${html}`);
            }
        });
    },
}


product.init();