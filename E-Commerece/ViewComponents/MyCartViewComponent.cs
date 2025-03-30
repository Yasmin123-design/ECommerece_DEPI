using E_Commerece.Services.Implementations;
using E_Commerece.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerece.ViewComponents
{
    public class MyCartViewComponent : ViewComponent
    {
        private readonly ICartService _cartService;
        private readonly IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
        public MyCartViewComponent(ICartService cartService)
        {
            this._cartService = cartService;
        }
        public async Task< IViewComponentResult> InvokeAsync()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var cart = this._cartService.GetCartByUserId(userId);
            return View(cart);
        }
    }
}
