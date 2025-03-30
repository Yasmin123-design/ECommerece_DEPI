using E_Commerece.Services.Interfaces;
using E_Commerece.ViewComponents;
using E_Commerece.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Security.Claims;

namespace E_Commerece.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ICartItemService _cartItemService;
        private readonly IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();

		public CartController(ICartService cartService , ICartItemService cartItemService )
        {
            this._cartService = cartService;
            this._cartItemService = cartItemService;
        }
        [HttpPost]
        public IActionResult UpdateQuantity([FromBody] CartUpdateModel model)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                // البحث عن العنصر في السلة
                var cartItem = this._cartItemService.GetCartItem(model.Id);
                var cart = this._cartService.GetCartByUserId(userId);
                if (cartItem == null)
                {
                    return Json(new { success = false });
                }
                cartItem.Quantity = model.Quantity;
                cart.TotalPrice = model.GrandTotal;
                this._cartService.SaveChange();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false});
            }
        }

        public IActionResult Index()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var cart = this._cartService.GetCartByUserId(userId);
            var x = cart.CartItems;

            return View(cart);
        }

    }
}
