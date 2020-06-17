var productDetail = {
    init: function () {
        productDetail.registerEvents();
        $('#hidProductIDD').val($('.firstID').data('id'));
  
        productDetail.loadImages();
       
        productDetail.loadSize();
        
    },

    registerEvents: function () {
        $('.btn-images').off('click').on("click", function (e) {
            e.preventDefault();
            $("#imagesManagee").load(location.href + " #imagesManagee>*", "");
            $('#hidProductIDD').val($(this).data('id'));
            productDetail.loadImages();
          
            productDetail.loadSize();
        });
    },


    loadImages: function () {
        $.ajax({
            url: '/Product/LoadImages',
            type: 'GET',
            data:
            {
                id: $('#hidProductIDD').val()
            },
            dataType: 'json',
            success: function (response) {
                var data = response.data;
                console.log(data);
              
                var html = '';
                var htmll = '';
               
                $.each(data, function (i, item) {
                    html += "<img class='h3-slide' src='" + item+"'>";
                   
                    htmll += "<img class='h3-slide' src='" + item + "'>"

                        
                        ;
                  
                });
                
                $('.slider-for').html(html);
                $('.slider-nav').html(htmll);
                productDetail.loadSlide();


                //thong bao thanh cong
            }
        });

    },

    
    loadSize: function () {
        $.ajax({
            url: '/Product/LoadSize',
            type: 'GET',
            data:
            {
                id: $('#hidProductIDD').val()
            },
            dataType: 'json',
            success: function (response) {
                var data = response.data;
                console.log(data);
                var html = '';
                $.each(data, function (i, item) {
                    html += "  <a class='dropdown - item' href='#'>" + item + "</a>";              
                });

                $('.dropdown-menu').html(html);

   
            }
        });
    },
    loadSlide: function () {

        $('.slider-for').slick({
            slidesToShow: 1,
            slidesToScroll: 1,
            arrows: false,
            fade: true,
            asNavFor: '.slider-nav'
        });
        $('.slider-nav').slick({
            slidesToShow: 3,
            slidesToScroll: 1,
            asNavFor: '.slider-for',
            dots: true,
            focusOnSelect: true
        });

    },
    
}
productDetail.init();