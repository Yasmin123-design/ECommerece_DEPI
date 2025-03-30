using E_Commerece.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerece.ViewComponents
{
	public class TopRatedCursorViewComponent : ViewComponent
	{
		private readonly IProductService _productService;
		public TopRatedCursorViewComponent(IProductService productService)
		{
			this._productService = productService;
		}
		public IViewComponentResult Invoke()
		{
			var topratedproducts = _productService.GetTopRatedProducts();
			return View(topratedproducts);
		}
	}
}
