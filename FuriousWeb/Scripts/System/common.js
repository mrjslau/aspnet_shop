function onBtnAddToCartClick(btnAddToCart, productId, refreshCart) {
    //define as global vars because we need to persist these values after ajax call.
    $BTN_ADD_TO_CART = $(btnAddToCart);
    PRODUCT_QUANTITY_TO_ADD = 1;

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
                //alert("Prekė buvo sėkmingai įkelta į krėpšelį");
            }
        },
        complete: function () {
            delete $BTN_ADD_TO_CART;
            delete PRODUCT_QUANTITY_TO_ADD;
        }
    })
}

$(".cart_quantity_up").click(function () {
    event.preventDefault();
    var value = parseInt($(this).next("input").val());
    var value = value + 1;
    $(this).next("input").val(value);
    var productID = $(this).next('input').data('product');
    var quantity = $(this).next('input').val();

    console.log(productID);
    console.log(quantity);
    onBtnEditCartItemQuantityClick('.cart-actions a', productID, true, quantity);
    fillCartPrice();
});

$(".cart_quantity_down").click(function (event) {
    event.preventDefault();
    var value = parseInt($(this).prev("input").val());
    var value = value - 1;
    if (value >= 1) { $(this).prev("input").val(value); }
    else { $(this).prev("input").val(1); }
    var productID = $(this).prev('input').data('product');
    var quantity = $(this).prev('input').val();
    console.log(productID);
    console.log(quantity);
    onBtnEditCartItemQuantityClick('.cart-actions a', productID, true, quantity);
    fillCartPrice();
});

function fillCartPrice() {
    var total = 0;
    $('.cart_info tbody .to_count').each(function () {
        var price = superSafeParseFloat($(this).find(".cart_price p").text());
        var quantity = $(this).find(".cart_quantity_input").val();
        var sum = price * quantity;
        $(this).find(".cart_total_price").html("&euro; " + sum.toFixed(2));
    });

    $('.cart_info tbody .to_count').each(function () {
        total += superSafeParseFloat($(this).find(".cart_total_price").text());
        $(".cart_info").find("#display_total").html("&euro; " + total.toFixed(2));
    });
}

function superSafeParseFloat(val) {
    if (isNaN(val)) {
        if ((val = val.match(/([0-9\.,]+\d)/g))) {
            val = val[0].replace(/[^\d\.]+/g, '')
        }
    }
    return parseFloat(val)
}

function onBtnEditCartItemQuantityClick(btnEditCartItemQuantity, productId, refreshCart, quantity) {
    //define as global vars because we need to persist these values after ajax call.
    $BTN_EDIT_CART_ITEM_QUANTITY = $(btnEditCartItemQuantity);

    NEW_PRODUCT_QUANTITY = quantity;
   
    $.ajax({
        type: "GET",
        url: '/ShoppingCart/EditItemQuantity',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        data: { productId: productId, newQuantity: NEW_PRODUCT_QUANTITY },
        error: function (xhr, status, errorThrown) {
            var errorMsg = "Status: " + status + " " + errorThrown;
            console.log(errorMsg);
            alert(errorMsg);
        },
        success: function (response) {
            if (response.success) {
                $("#shoppingCartItemsCount").text(response.shoppingCartItemsCount);

                if (refreshCart) {
                    var $quantityInCartField = $BTN_EDIT_CART_ITEM_QUANTITY.parent().siblings('.quantityInCart');
                    var oldQuantity = parseInt($quantityInCartField.text());
                    $quantityInCartField.text(NEW_PRODUCT_QUANTITY);
                }
            }
        },
        complete: function () {
            delete $BTN_EDIT_CART_ITEM_QUANTITY;
            delete NEW_PRODUCT_QUANTITY;
        }
    })
}


function getProductsCount(queryString) {
    var count = 0;
    $.ajax({
        type: "GET",
        url: '/Products/GetProductCount',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        async: false,
        data: {
            query: queryString
        },
        error: function (xhr, status, errorThrown) {
            var errorMsg = "Status: " + status + " " + errorThrown;
            console.log(errorMsg);
        },
        success: function (data) {
            count = parseInt(data);
        }
    })
    return count;
}

function getProducts(queryString, adminAccess, page) {
    var actionName = adminAccess ? "GetProductsListForAdmin" : "GetProductsListForUser";
    $.ajax({
        type: "GET",
        url: '/Products/' + actionName,
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        async: true,
        data: {
            query: queryString,
            page: page
        },
        error: function (xhr, status, errorThrown) {
            var errorMsg = "Status: " + status + " " + errorThrown;
            console.log(errorMsg);
            alert(errorMsg);
        },
        success: function (data) {
            $(".products").html(data);
        }
    })
}

//neleidžiam įvesti mažiau už 1
function onProductQuantityChange(element) {
    if ($(element).val() < 1)
        $(element).val(1);
}

function search(adminAccess) {
    getProducts($("#search-query").val(), adminAccess);
}