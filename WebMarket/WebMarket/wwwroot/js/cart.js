$(document).ready(function () {
    showModel();
})


function showModel() {
    $(".show").click(function () {
        var product = $(this).parent().parent();
        var id = product.attr("data-id");
        var name = product.find("p").text();
        var img = product.find("img").attr("src");
        var price = product.find("h4").html();
        var desc = $(this).parent().find(".item-desc").val();
        var modal = $(".modal-body");
        modal.find("img").attr("src", img)
        modal.find(".modal-item-name").text(name);
        modal.find(".modal-item-price").html(price);
        modal.find(".model-item-desc").html(desc);
        modal.attr("data-id", id);
    })
    addToCart();
}

function addToCart() {
    $(".add-to-cart").click(function () {
        var id = $(".modal-body").attr("data-id");
        var quantity = $(".modal-body").find('input[name=quantity]').val();
        $.ajax({
            type: "POST",
            url: '/Cart/AddToCart',
            data: {
                'id': id,
                'quantity': quantity,
                'type': "ajax"
            },
            success: function (data) {
                Swal.fire({
                    icon: 'success',
                    title: 'Thêm giỏ hàng thành công',
                    showConfirmButton: false,
                    timer: 2500
                });
                $(".count").text(data.quantity);
                $(".modal-body").find('input[name=quantity]').val("1");
                $('#modal-cart').modal('hide');
            },
            error: function () {
                Swal.fire({
                    icon: 'error',
                    title: 'Thêm giỏ hàng thất bại',
                    text: 'Vui lòng thử lại',
                    showConfirmButton: false,
                    timer: 2500
                });
                $(".modal-body").find('input[name=quantity]').val("1");
                $('#modal-cart').modal('hide');
            },
        })
    })
}