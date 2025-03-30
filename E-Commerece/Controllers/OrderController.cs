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
		public OrderController(IOrderService orderService , ICartService cartService , IProductItemService productItemService , IConfiguration configuration)
		{
			this._orderService = orderService;
			this._cartService = cartService;
			this._productItemService = productItemService;
			this.Configuration = configuration;
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
			var order = _orderService.GetLatestOrder();
			return View(order);
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
        private void ProcessOrderAfterPayment(int orderId)
        {
            var order = _orderService.GetOrderById(orderId);
            if (order == null) return;

            order.Status = E_Commerece.Models.Status.Shipped;

            var orderItems = _orderService.GetOrderItemsByOrderId(order.Id);
            foreach (var item in orderItems)
            {
                var productItem = _productItemService.GetProductItemByProductId(item.ProductItem.Product.Id);
                if (productItem != null && productItem.Quantity >= item.Quantity)
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
