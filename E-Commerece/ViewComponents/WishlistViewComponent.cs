using E_Commerece.Services.Implementations;
using E_Commerece.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerece.ViewComponents
{
    public class WishlistViewComponent : ViewComponent
    {
        private readonly IWishlistService _wishlistService;
        public WishlistViewComponent(IWishlistService wishlistService)
        {
            this._wishlistService = wishlistService;
        }
        public IViewComponentResult Invoke()
        {
            var wishlist = this._wishlistService.GetWishlistByUserId();
            return View(wishlist);
        }
    }
}
