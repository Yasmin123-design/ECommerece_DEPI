using E_Commerece.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerece.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ICategoryService _categoryService;
		public CategoryController(ICategoryService categoryService)
		{
			this._categoryService = categoryService;
		}
		public IActionResult Index()
		{
			var categories = _categoryService.GetAllCategories();
			return View(categories);
		}

	}
}
