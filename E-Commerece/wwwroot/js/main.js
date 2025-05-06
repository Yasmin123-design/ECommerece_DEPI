(function ($) {
	$(document).ready(function () {
		$(".store-grid li").click(function () {
			// إزالة الـ active من جميع الأزرار وإضافته للزر المضغوط
			$(".store-grid li").removeClass("active");
			$(this).addClass("active");

			// التحقق من نوع العرض المطلوب
			var viewType = $(this).data("view");
			var productList = $("#product-list");

			if (viewType === "list") {
				productList.addClass("list-view").removeClass("grid-view");
			} else {
				productList.addClass("grid-view").removeClass("list-view");
			}
		});
	});


	function displayProducts(data) {
		$("#product-list").html(""); // تفريغ القائمة الحالية
		data.forEach(product => {
			$("#product-list").append(
				`
                                <div class="col-md-4 col-xs-6">
                                    <div class="product">
                                        <div class="product-img">
                                            <img src="./img/${product.image}" alt="${product.name}">
                                        </div>
                                        <div class="product-body">
                                            <p class="product-category">${product.category}</p>
                                            <h3 class="product-name"><a href="#">${product.name}</a></h3>
                                            <h4 class="product-price">$${product.price}</h4>
                                                    <div class="product-btns">
                                                            <a href="/Product/QuickView/${product.id}"  class="quick-view">
                                                        <i class="fa fa-heart"></i>
                                                    </a>
                                                            <a href="/Product/QuickView/${product.id}" class="quick-view">
                                                        <i class="fa fa-shopping-cart"></i>
                                                    </a>
                                                            <a href="/Product/QuickView/${product.id}" class="quick-view">
                                                        <i class="fa fa-eye"></i>
                                                    </a>

                                                </div>
                                        </div>
                                    </div>
                                </div>
                                `
			);
		});
	}



	"use strict"

	// Mobile Nav toggle
	$('.menu-toggle > a').on('click', function (e) {
		e.preventDefault();
		$('#responsive-nav').toggleClass('active');
	});

	$("#menuadmin-toggle").on('click', function (e) {
		e.preventDefault();
		$(".menu").slideToggle();
	});
	

	// Fix cart dropdown from closing
	$('.cart-dropdown').on('click', function (e) {
		e.stopPropagation();
	});

	$(document).ready(function () {
		$('.products-slick').slick({
			slidesToShow: 4,
			slidesToScroll: 1,
			autoplay: true,
			infinite: true,
			speed: 300,
			dots: false,
			arrows: true,
			appendArrows: '#slick-nav-1',
			responsive: [
				{
					breakpoint: 991,
					settings: {
						slidesToShow: 2,
						slidesToScroll: 1,
					}
				},
				{
					breakpoint: 480,
					settings: {
						slidesToShow: 1,
						slidesToScroll: 1,
					}
				}
			]
		});
	});
	$(document).ready(function () {
		$('.products-slick2').slick({
			slidesToShow: 4,
			slidesToScroll: 1,
			autoplay: true,
			infinite: true,
			speed: 300,
			dots: false,
			arrows: true,
			appendArrows: '#slick-nav-2',
			responsive: [
				{
					breakpoint: 991,
					settings: {
						slidesToShow: 2,
						slidesToScroll: 1,
					}
				},
				{
					breakpoint: 480,
					settings: {
						slidesToShow: 1,
						slidesToScroll: 1,
					}
				}
			]
		});
	});
	// Products Widget Slick
	$('.products-widget-slick').each(function() {
		var $this = $(this),
				$nav = $this.attr('data-nav');

		$this.slick({
			infinite: true,
			autoplay: true,
			speed: 300,
			dots: false,
			arrows: true,
			appendArrows: $nav ? $nav : false,
		});
	});

	/////////////////////////////////////////

	// Product Main img Slick
	$('#product-main-img').slick({
    infinite: true,
    speed: 300,
    dots: false,
    arrows: true,
    fade: true,
    asNavFor: '#product-imgs',
  });

	// Product imgs Slick
  $('#product-imgs').slick({
    slidesToShow: 3,
    slidesToScroll: 1,
    arrows: true,
    centerMode: true,
    focusOnSelect: true,
		centerPadding: 0,
		vertical: true,
    asNavFor: '#product-main-img',
		responsive: [{
        breakpoint: 991,
        settings: {
					vertical: false,
					arrows: false,
					dots: true,
        }
      },
    ]
  });

	// Product img zoom
	var zoomMainProduct = document.getElementById('product-main-img');
	if (zoomMainProduct) {
		$('#product-main-img .product-preview').zoom();
	}

	/////////////////////////////////////////

	// Input number
	$('.input-number').each(function() {
		var $this = $(this),
		$input = $this.find('input[type="number"]'),
		up = $this.find('.qty-up'),
		down = $this.find('.qty-down');

		down.on('click', function () {
			var value = parseInt($input.val()) - 1;
			value = value < 1 ? 1 : value;
			$input.val(value);
			$input.change();
			updatePriceSlider($this , value)
		})

		up.on('click', function () {
			var value = parseInt($input.val()) + 1;
			$input.val(value);
			$input.change();
			updatePriceSlider($this , value)
		})
	});

})(jQuery);
function AddToCart(productId) {
	window.location.href = `/Product/QuickView/${productId}`;
}
function ckeckOut() {
	window.location.href = `/Order/CheckOut`;
}
function addToWishlist(productId) {
	$.ajax({
		url: "/Product/AddToWishlist",
		type: "POST",
		data: { productid: productId },
		success: function (response) {
			alert(response.message);
			location.reload();
		},
		error: function (xhr) {
			if (xhr.status === 401) {
				// إعادة توجيه المستخدم إلى صفحة تسجيل الدخول
				window.location.href = "/Account/Login";
			} else {
				alert("حدث خطأ أثناء الإضافة!");
			}
		}
	});
}


