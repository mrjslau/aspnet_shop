function onBtnAddToCartClick(el, productId, refreshCart) {
    var productQuantity = parseInt($(el).siblings(".productQuantity").val());
    $.ajax({
        type: "GET",
        url: '/ShoppingCart/AddItem',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        data: { productId: productId, quantity: productQuantity },
        error: function (xhr, status, errorThrown) {
            var errorMsg = "Status: " + status + " " + errorThrown;
            console.log(errorMsg);
            alert(errorMsg);
        },
        success: function (response) {
            if (response.success) {
                $("#shoppingCartItemsCount").text(response.shoppingCartItemsCount);

                if (refreshCart) {
                    var oldQuantity = parseInt($('.quantity').text());
                    $('.quantity').text(oldQuantity + productQuantity);
                }
                alert("Prekė buvo sėkmingai įkelta į krėpšelį");
            }
        }
    })
}

//neleidžiam įvesti mažiau už 1
function onProductQuantityChange(element) {
    if ($(element).val() < 1)
        $(element).val(1);
}