using E_Commerece.Services.Interfaces;
using E_Commerece.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Stripe;                
using Stripe.Checkout;         


namespace E_Commerece.Controllers
{
	public class OrderController : Controller
	{
		private readonly IOrderService _orderService;
		private readonly ICartService _cartService;
		private readonly IProductItemService _productItemService;
		private readonly IConfiguration Configuration;
        private readonly ICartItemService _cartItemService;
		public OrderController(IOrderService orderService 
            , ICartService cartService 
            , IProductItemService productItemService 
            , IConfiguration configuration
            , ICartItemService cartItemService
            )
		{
			this._orderService = orderService;
			this._cartService = cartService;
			this._productItemService = productItemService;
			this.Configuration = configuration;
            this._cartItemService = cartItemService;
		}
		public IActionResult CheckOut()
		{
			return View();
		}

		[HttpPost]
		public IActionResult CheckOut(CheckOutVM  model)
		{
			this._orderService.CreateOrder(model);
			this._orderService.SaveChange();
			return RedirectToAction("OrderDetails");

		}
		public IActionResult OrderDetails()
		{
            var cartOrder = _orderService.GetCartOrderDetails();
            return View(cartOrder);
        }

		public IActionResult Payment(PaymentVM model)
		{
			var order = _orderService.GetLatestOrder();
			var Paymentmodel = new PaymentVM()
			{
				OrderId = order.Id,
				Email = order.EmailAddress,
				TotalPrice = order.TotalPrice
			};
			return View(Paymentmodel);
		}
        //private void ProcessOrderAfterPayment(int orderId)
        //{
        //    var order = _orderService.GetOrderById(orderId);
        //    if (order == null) return;


        //    var orderItems = _orderService.GetOrderItemsByOrderId(order.Id);
        //    foreach (var item in orderItems)
        //    {
        //        var productItem = item.ProductItem;
        //        if (productItem != null && productItem.Quantity >= item.Quantity)
        //        {
        //            productItem.Quantity -= item.Quantity;
        //        }
        //    }

        //    _cartService.ClearCart(order.UserId);
        //    _orderService.SaveChange();
        //}
        private void ProcessOrderAfterPayment(int orderId)
        {
            var order = _orderService.GetOrderById(orderId);
            if (order == null) return;

            var orderItems = _orderService.GetOrderItemsByOrderId(order.Id);
            var userCartItems = _cartItemService.GetCartItems(); // هات العناصر الموجودة فعلاً في السلة

            foreach (var item in orderItems)
            {
                var productItem = item.ProductItem;

                // التأكد إن العنصر موجود في السلة قبل خصم الكمية
                bool isInCart = userCartItems.Any(ci => ci.ProductItemId == productItem.Id);

                if (productItem != null && isInCart && productItem.Quantity >= item.Quantity)
                {
                    productItem.Quantity -= item.Quantity;
                }
            }

            _cartService.ClearCart(order.UserId);
            _orderService.SaveChange();
        }

        public IActionResult CashOnDelivery(PaymentVM model)
        {
            if (!ModelState.IsValid)
            {
                return View("Payment", model); // إعادة عرض الصفحة مع الأخطاء
            }

            return RedirectToAction("OrderConfirmation", new { orderId = model.OrderId });
        }


        public IActionResult OrderConfirmation(int orderId)
		{
            ProcessOrderAfterPayment(orderId);
            return View();
		}
        public IActionResult PayWithStripe(PaymentVM model)
        {
            if (!ModelState.IsValid)
            {
                return View("Payment", model); // إعادة عرض الصفحة مع الأخطاء
            }

            StripeConfiguration.ApiKey = Configuration["Stripe:SecretKey"];

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
        {
            new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = "Order #" + model.OrderId
                    },
                    UnitAmount = (long)(model.TotalPrice * 100),
                },
                Quantity = 1,
            },
        },
                Mode = "payment",
                SuccessUrl = Url.Action("OrderConfirmation", "Order", new { orderId = model.OrderId }, Request.Scheme),
                CancelUrl = Url.Action("PaymentFailed", "Order", new { orderId = model.OrderId }, Request.Scheme),
            };

            var service = new SessionService();
            Session session = service.Create(options);

            return Redirect(session.Url);
        }


        public IActionResult PaymentFailed(int orderId)
		{ 
			return View();
		}
    }
}
