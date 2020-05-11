var product = {
    init: function () {
        product.registerEvents();
    },
  
    registerEvents: function () {
        $('.btn-images').off('click').on("click", function (e) {
            e.preventDefault();
            $('#hidProductID').val($(this).data('id'));
            product.navigateToDetail();
           
        });
        

 
    },

    navigateToDetail: function () {
        var id = $('#hidProductID').val();
        var url = "Admin/ProductDetail/Index/" + id;
        //var url = '@Url.Action("ProductDetail", "Index", "_value" )';
        var urlWithValue = url.replace('_value', id);
        window.location.pathname = url;
      
    },

}

product.init();