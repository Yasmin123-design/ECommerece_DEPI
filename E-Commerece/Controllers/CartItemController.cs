using E_Commerece.Services.Interfaces;
using E_Commerece.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerece.Controllers
{
    public class CartItemController : Controller
    {
        private readonly ICartItemService _cartItemService;
        private readonly ICartService _cartService;
        public CartItemController(ICartItemService cartItemService , ICartService cartService)
        {
            this._cartItemService = cartItemService;
            this._cartService = cartService;
        }
        [HttpPost]
        public IActionResult DeleteCartItem([FromBody] CartItemRequest request)
        {
            this._cartItemService.RemoveCartItem(request.Id);
            var cart = this._cartService.GetCartById(request.CartId);
            if (cart != null)
            {
                cart.TotalPrice = request.GrandDeleted;
            }
            this._cartItemService.SaveChange();
            return Json(new { success = true });
        }
        public IActionResult DeleteAll([FromBody]CartRequest request)
        {
            var cart = this._cartService.GetCartById(request.CartId);
            this._cartItemService.RemoveAllItemsRelatedByCart(request.CartId);
            cart.TotalPrice = 0;
            this._cartItemService.SaveChange();
            return Json(new { success = true });
        }
    }
}
