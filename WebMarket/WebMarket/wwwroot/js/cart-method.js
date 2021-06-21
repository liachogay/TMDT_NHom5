$(document).ready(function () {
    UpdateQuantity();
    $(".qty-increase").click(function () {
        num = parseInt($(this).parent().find('.qty-input').val());
        if (num >= 1) {
            num += 1;
            var input = $(this).parent().find('.qty-input');
            input.val(num);
            let id = $(this).parent().find('.qty-input').data("masp");
            $.ajax({
                type: "POST",
                url: "Cart/UpdateCart",
                data: {
                    "Id": input.data("masp"),
                    "Quantity": input.val(),
                },
                dataType: "json",
                success: function (res) {
                    $(".count").text(res.soLuong);
                    $('.prices__value--final').text(res.tongTien + ' đ');
                    $('.' + id).children().children().children("span[class='basket-quantity']").text(num);
                }
            });
        }        
        else {
            num = 1;
            var input = $(this).parent().find('.qty-input');
            input.val(num);
        }
    });
    $(".qty-decrease").click(function () {
        num = parseInt($(this).parent().find('.qty-input').val());
        if (num > 1) {
            num -= 1;
            var input = $(this).parent().find('.qty-input');
            input.val(num);
            let id = $(this).parent().find('.qty-input').data("masp");
            $.ajax({
                type: "POST",
                url: "Cart/UpdateCart",
                data: {
                    "Id": input.data("masp"),
                    "Quantity": parseInt(input.val()),
                },
                dataType: "json",
                success: function (data) {
                    $(".count").text(data.soLuong);
                    $('.prices__value--final').text(data.tongTien + ' đ');
                    $('.' + id).children().children().children("span[class='basket-quantity']").text(num);
                }
            });
        }
        else {
            num = 1;
            var input = $(this).parent().find('.qty-input');
            input.val(num);
        }
    });
    
    
    $('.cart-products__del').click(function () {
        let deleteButton = $(this).parent().parent().parent();
        let id = $(this).data("masp");
        $.ajax({
            type: "POST",
            url: "Cart/DeleteItem",
            data: {
                "Id": id,
            },
            dataType: "json",
            success: function (data) {
                $('.' + id).remove();
                   deleteButton.remove();
                $(".count").text(data.soLuong);
                $('.prices__value--final').text(data.tongTien + ' đ');
                if (data.soLuong == 0) {
                    var header = "<h2>Your shopping cart is empty!</h2>";
                    var cnt = '<div class="checkout-right-basket"><a href ="~/Home"> <span class="glyphicon glyphicon-menu-left" aria-hidden="true"></span>Continue Shopping</a></div><div class="clearfix"> </div>';
                    $(".header-cart").html(header);
                    $(".checkout-left").html(cnt);
                    $(".timetable_sub").html('');
                }
            }
        });
    });
});

function UpdateQuantity() {
    $('.qty-input').change(function () {
        $.ajax({
            type: "POST",
            url: "Cart/UpdateCart",
            data: {
                "Id": $(this).data("masp"),
                "Quantity": $(this).val(),
            },
            dataType: "json",
            success: function (data) {
                $(".count").text(data.soLuong);
                $('.prices__value--final').text(data.TongTien + ' đ');
            }
        });
    });
}

