using E_Commerece.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerece.Controllers
{
	public class AdminController : Controller
	{
		private readonly ICategoryService _categoryService;
		public AdminController(ICategoryService categoryService)
		{
			this._categoryService = categoryService;
		}
		public IActionResult Categories()
		{
			var categories = _categoryService.GetAllCategories();
			return View(categories);
		}
	}
}
