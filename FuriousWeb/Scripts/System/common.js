﻿function onBtnAddToCartClick(btnAddToCart, productId, refreshCart) {
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

    btnAddToCart.classList.add("added_to_cart");
    btnAddToCart.innerHTML = "<i class='fa fa-check'></i> Prekė pridėta";
    btnAddToCart.disabled = true;
    setTimeout(function () {
        btnAddToCart.disabled = false;
        btnAddToCart.innerHTML = "<i class='fa fa-shopping-cart'></i> Įdėti į krėpšelį";
    }, 5000);
}

$(document).ready(function () {
    validatePersonalInfo();
    $('.personal_info #name, .personal_info #lastname, .personal_info #email, .personal_info #phone, .personal_info #address').change(validatePersonalInfo);
});

function validatePersonalInfo() {
    if ($('.personal_info #name').val().length > 0 &&
        $('.personal_info #lastname').val().length > 0 &&
        $('.personal_info #email').val().length > 0 &&
        $('.personal_info #phone').val().length > 0 &&
        $('.personal_info #address').val().length > 0 ) {
        $(".billing_info .do_action #pay-now").prop("disabled", false);
    }
    else {
        $(".billing_info .do_action #pay-now").prop("disabled", true);
    }
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
       val = val.replace(/[^\d\.]+/g, '')
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

function getUsersCount(queryString) {
    var count = 0;
    $.ajax({
        type: "GET",
        url: '/Admin/GetUsersCount',
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

function getOrdersCount(queryString) {
    var count = 0;
    $.ajax({
        type: "GET",
        url: '/Admin/GetOrdersCount',
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

function getProducts(queryString, adminAccess, page, isPartial) {

    var actionName = adminAccess ? "GetProductsListForAdmin" : "GetProductsListForUser";
    var actionLocation = adminAccess ? "#partial-body" : ".products";
    var queryInput = adminAccess ? "#search-query" : "#user-product-search";
    $.ajax({
        type: "GET",
        url: '/Products/' + actionName,
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        async: true,
        data: {
            query: queryString,
            currentPage: page,
            isPartial: isPartial
        },
        error: function (xhr, status, errorThrown) {
            var errorMsg = "Status: " + status + " " + errorThrown;
            console.log(errorMsg);
        },
        success: function (data) {
            $(actionLocation).html(data);
            setTimeout(function () {
                var count = getProductsCount(queryString);

                pages = Math.ceil(count / 12);
                $('.products-pagination').html('');
                $('.products-pagination').append('<li class="active" data-value="1">' + 1 + '</li>');
                for (i = 1; i < pages; i++) {
                    $('.products-pagination').append('<li data-value="' + (i + 1) + '">' + (i + 1) + '</li>');
                }
                $(".products-pagination li").removeClass('active');
                $(".products-pagination").find("[data-value='" + page + "']").addClass('active');
            }, 100);
            $(queryInput).val(queryString);
        }
    })
}

function getOrders(queryString, adminAccess, page) {
    var actionName = adminAccess ? "GetOrderListForAdmin" : "GetOrderListForUser";
    var queryInput = adminAccess ? "#search-query" : "#user-product-search";
    $.ajax({
        type: "GET",
        url: '/Admin/' + actionName,
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        async: true,
        data: {
            query: queryString,
            currentPage: page,
            isPartial:true
        },
        error: function (xhr, status, errorThrown) {
            var errorMsg = "Status: " + status + " " + errorThrown;
        },
        success: function (data) {
            $("#partial-body").html(data);
            setTimeout(function () {
                var count = getOrdersCount(queryString);
                pages = Math.ceil(count / 12);
                $('.orders-pagination').html('');
                $('.orders-pagination').append('<li class="active" data-value="1">' + 1 + '</li>');
                for (i = 1; i < pages; i++) {
                    $('.orders-pagination').append('<li data-value="' + (i + 1) + '">' + (i + 1) + '</li>');
                }
                $(".orders-pagination li").removeClass('active');
                $(".orders-pagination").find("[data-value='" + page + "']").addClass('active');
            }, 100);
            $(queryInput).val(queryString);
        }
    })
}

function getUsers(queryString, adminAccess, page) {
    var actionName = adminAccess ? "GetUsersListForAdmin" : "GetUsersListForUser";
    var queryInput = adminAccess ? "#search-query" : "#user-product-search";
    $.ajax({
        type: "GET",
        url: '/Admin/' + actionName,
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        async: true,
        data: {
            query: queryString,
            currentPage: page,
            isPartial: true
        },
        error: function (xhr, status, errorThrown) {
            var errorMsg = "Status: " + status + " " + errorThrown;
        },
        success: function (data) {
            $("#partial-body").html(data);
            setTimeout(function () {
                var count = getUsersCount(queryString);
                pages = Math.ceil(count / 12);
                $('.users-pagination').html('');
                $('.users-pagination').append('<li class="active" data-value="1">' + 1 + '</li>');
                for (i = 1; i < pages; i++) {
                    $('.users-pagination').append('<li data-value="' + (i + 1) + '">' + (i + 1) + '</li>');
                }
                $(".users-pagination li").removeClass('active');
                $(".users-pagination").find("[data-value='" + page + "']").addClass('active');
            }, 100);
            $(queryInput).val(queryString);
        }
    })
}

//neleidžiam įvesti mažiau už 1
function onProductQuantityChange(element) {
    if ($(element).val() < 1)
        $(element).val(1);
}

function search(adminAccess, entity) {
    console.log(entity);
    if(entity == 'products')
        getProducts($("#search-query").val(), adminAccess, 1, true);
    if (entity == 'orders')
        getOrders($("#search-query").val(), adminAccess, 1);
    if (entity == 'users')
        getUsers($("#search-query").val(), adminAccess, 1);
}


$(document).ready(function () {
    if ($('#products').length) {
        //getProducts("", true, 1, true);
        setTimeout(function () {
            var count = getProductsCount('');
            pages = Math.ceil(count / 12);
            $('.admin.products-pagination').html('');
            $('.admin.products-pagination').append('<li class="active" data-value="1">' + 1 + '</li>');
            for (i = 1; i < pages; i++) {
                $('.admin.products-pagination').append('<li data-value="' + (i + 1) + '">' + (i + 1) + '</li>');
            }
        }, 100);
        $('body').on('click', '.admin.products-pagination li', function () {
            var page = $(this).data('value');
            getProducts($("#search-query").val(), true, page, true);
        });
    }
    if ($('#users').length) {
        //getUsers("", true, 1);
        setTimeout(function () {
            var count = getUsersCount('');
            pages = Math.ceil(count / 12);
            $('.users-pagination').html('');
            $('.users-pagination').append('<li class="active" data-value="1">' + 1 + '</li>');
            for (i = 1; i < pages; i++) {
                $('.users-pagination').append('<li data-value="' + (i + 1) + '">' + (i + 1) + '</li>');
            }
        }, 100);
        $('body').on('click', '.users-pagination li', function () {
            var page = $(this).data('value');
            getUsers($("#search-query").val(), true, page);
        });
    }
    if ($('#orders').length) {
        //
        //getOrders("", true, 1);
        setTimeout(function () {
             
            var count = getOrdersCount('');
            pages = Math.ceil(count / 12);
            $('.orders-pagination').html('');
            $('.orders-pagination').append('<li class="active" data-value="1">' + 1 + '</li>');
            for (i = 1; i < pages; i++) {
                $('.orders-pagination').append('<li data-value="' + (i + 1) + '">' + (i + 1) + '</li>');
            }
        }, 100);
        $('body').on('click', '.orders-pagination li', function () {
            var page = $(this).data('value');
            getOrders($("#search-query").val(), true, page);
        });
    }
    
});



function addProductImage(){
    $.ajax({
        type: "GET",
        url: '/File/AddFile',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        async: true,
        data: {
            query: queryString,
            currentPage: page,
            isPartial: true
        },
        error: function (xhr, status, errorThrown) {
            var errorMsg = "Status: " + status + " " + errorThrown;
            alert(errorMsg);
        },
        succes: function(){
            alert("success");
        }
    });
}

function loadImage(inputFile) {
    $(inputFile).hide();

    if (inputFile.files && inputFile.files[0]) {
        var new_input_img = "<input type='file' class='form-control' name='images' onchange='loadImage(this)'/>";
        $NEW_INPUT_IMG = $(new_input_img);

        var reader = new FileReader();

        reader.onload = function (e) {
            var imgElement = "<img src='#' alt='your image' name='product_image' />"
            $imgElement = $(imgElement);
            $imgElement.attr('src', e.target.result);

            var imageContainer = "<div class='prod-img-thumbnail'><span class='close' onclick='removeImage(this)'>&times;</span>" + $imgElement.prop('outerHTML') + "</div>"

            $('.prod-img-container').append(imageContainer);
            $('.file_inputs').append($NEW_INPUT_IMG);

            delete $NEW_INPUT_IMG;
        }
        reader.readAsDataURL(inputFile.files[0]);
    }
}

function loadMainImage(inputFile) {
    if (inputFile.files && inputFile.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {

            var imgElement = "<img src='#' alt='your image' name='image' />"
            $imgElement = $(imgElement);
            $imgElement.attr('src', e.target.result);

            var imageContainer = "<div class='prod-img-main'><span class='close' onclick='removeImage(this)'>&times;</span>" + $imgElement.prop('outerHTML') + "</div>"

            $('.prod-img-main').remove();
  
            $('.prod-img-container').append(imageContainer);
        }

        reader.readAsDataURL(inputFile.files[0]);
    }
}