using E_Commerece.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerece.Controllers
{
	public class StoreController : Controller
	{
		private readonly IProductService _productService;
		public StoreController(IProductService productService)
		{
			this._productService = productService;
		}
		public IActionResult Index()
		{
			var products = this._productService.GetAllProducts();
            ViewBag.IsAdmin = false;
            return View(products);
		}
	}
}
