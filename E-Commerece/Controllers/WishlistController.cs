using E_Commerece.Data.Interfaces;
using E_Commerece.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerece.Controllers
{
	public class WishlistController : Controller
	{
		private readonly IWishlistService _wishlistService;
		public WishlistController(IWishlistService wishlistService)
		{
			this._wishlistService = wishlistService;
		}
		public IActionResult Index()
		{
			var wishlist = this._wishlistService.GetWishlistByUserId();
			return View(wishlist);
		}
	}
}
