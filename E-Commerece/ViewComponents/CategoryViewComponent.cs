using E_Commerece.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerece.ViewComponents
{
    public class CategoryViewComponent:ViewComponent
    {
        private readonly ICategoryService _categoryService;
        public CategoryViewComponent(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

        public IViewComponentResult Invoke(int? selectedCategoryId)
        {
            var categories = _categoryService.GetAllCategories();
			ViewBag.SelectedCategoryId = selectedCategoryId ?? 0;
			return View(categories);
        }
    }
}
