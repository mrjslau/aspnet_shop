/*price range*/

 $('#sl2').slider();

	var RGBChange = function() {
	  $('#RGB').css('background', 'rgb('+r.getValue()+','+g.getValue()+','+b.getValue()+')')
	};	
		
/*scroll to top*/

$(document).ready(function(){
	$(function () {
		$.scrollUp({
	        scrollName: 'scrollUp', // Element ID
	        scrollDistance: 300, // Distance from top/bottom before showing element (px)
	        scrollFrom: 'top', // 'top' or 'bottom'
	        scrollSpeed: 300, // Speed back to top (ms)
	        easingType: 'linear', // Scroll to top easing (see http://easings.net/)
	        animation: 'fade', // Fade, slide, none
	        animationSpeed: 200, // Animation in speed (ms)
	        scrollTrigger: false, // Set a custom triggering element. Can be an HTML string or jQuery object
					//scrollTarget: false, // Set a custom target element for scrolling to the top
	        scrollText: '<i class="fa fa-angle-up"></i>', // Text for element, can contain HTML
	        scrollTitle: false, // Set a custom <a> title if required.
	        scrollImg: false, // Set true to use image
	        activeOverlay: false, // Set CSS color to display scrollUp active point, e.g '#00FFFF'
	        zIndex: 2147483647 // Z-Index for the overlay
		});
    });
    fillCartPrice();
});

$(".cart_quantity_up").click(function () {
    event.preventDefault();
    var value = parseInt($(this).next("input").val());
    var value = value + 1;
    $(this).next("input").val(value);
    fillCartPrice();
});
$(".cart_quantity_down").click(function (event) {
    event.preventDefault();
    var value = parseInt($(this).prev("input").val());
    var value = value - 1;
    if (value >= 1) { $(this).prev("input").val(value); }
    else { $(this).prev("input").val(1); }
    fillCartPrice();
});

function fillCartPrice() {
    var total = 0;
    $('.cart_info tbody .to_count').each(function () {
        var price = superSafeParseFloat($(this).find(".cart_price p").text());
        var quantity = $(this).find(".cart_quantity_input").val();
        var sum = price * quantity;
        $(this).find(".cart_total_price").html("&euro; "+sum.toFixed(2));
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