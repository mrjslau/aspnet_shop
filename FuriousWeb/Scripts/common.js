function onBtnAddToCartClick(btnAddToCart, productId, refreshCart) {
    //define as global vars because we need to persist these values after ajax call.
    $BTN_ADD_TO_CART = $(btnAddToCart);
    PRODUCT_QUANTITY_TO_ADD = parseInt($BTN_ADD_TO_CART.siblings(".productQuantityToAdd").val());

    $.ajax({
        type: "GET",
        url: '/ShoppingCart/AddItem',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        data: { productId: productId, quantity: PRODUCT_QUANTITY_TO_ADD },
        error: function (xhr, status, errorThrown) {
            var errorMsg = "Status: " + status + " " + errorThrown;
            console.log(errorMsg);
            alert(errorMsg);
        },
        success: function (response) {
            if (response.success) {
                $("#shoppingCartItemsCount").text(response.shoppingCartItemsCount);

                if (refreshCart) {
                    var $quantityInCartField = $BTN_ADD_TO_CART.parent().siblings('.quantityInCart');
                    var oldQuantity = parseInt($quantityInCartField.text());
                    $quantityInCartField.text(oldQuantity + PRODUCT_QUANTITY_TO_ADD);
                }
                alert("Prekė buvo sėkmingai įkelta į krėpšelį");
            }
        },
        complete: function () {
            delete $BTN_ADD_TO_CART;
            delete PRODUCT_QUANTITY_TO_ADD;
        }
    })
}

function getProducts(queryString) {
    $.ajax({
        type: "GET",
        url: '/Products/GetProductsList',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        async: true,
        data: {
            isPartial: true, query: queryString
        },
        error: function (xhr, status, errorThrown) {
            var errorMsg = "Status: " + status + " " + errorThrown;
            console.log(errorMsg);
            alert(errorMsg);
        },
        success: function (data) {
            console.log(3);
            console.log($(".products"));
            $(".products").html(data);
        }
    }).promi

}

//neleidžiam įvesti mažiau už 1
function onProductQuantityChange(element) {
    if ($(element).val() < 1)
        $(element).val(1);
}