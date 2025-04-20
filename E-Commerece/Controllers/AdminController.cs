using E_Commerece.Models;
using E_Commerece.Services.Interfaces;
using E_Commerece.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerece.Controllers
{
	public class AdminController : Controller
	{
		private readonly ICategoryService _categoryService;
		private readonly IVariationService _variationService;
		private readonly IVariationOptionsService _variationOptionsService;
		private readonly IProductService _productService;
		public AdminController(ICategoryService categoryService
			,IVariationService variationService
			,IVariationOptionsService variationOptionsService
			,IProductService productService)
		{
			this._categoryService = categoryService;
			this._variationService = variationService;
			this._variationOptionsService = variationOptionsService;
			this._productService = productService;
		}
		public IActionResult Categories()
		{
			var categories = _categoryService.GetAllCategories();
			return View(categories);
		}
		public IActionResult CreateCategory()
		{
			return View();
		}
		[HttpPost]
		public IActionResult CreateCategory(Category model)
		{
			if (ModelState.IsValid)
			{
				_categoryService.Create(model);
				_categoryService.SaveChange();
				return RedirectToAction("AddVariationAndOptionRelatedCategory", model);
			}
			return View(model);
		}
		public IActionResult AddVariationAndOptionRelatedCategory(Category category)
		{
			return View(category);
		}

		[HttpPost]
		public IActionResult AddVariationAndOptionRelatedCategory(List<Variation> variations, int categoryId)
		{
            ModelState.Clear();
            var category = _categoryService.GetCategory(categoryId);
			if (variations == null || !variations.Any(v => !string.IsNullOrEmpty(v.Name)))
			{
				ModelState.AddModelError("Variations", "يجب إدخال على الأقل Variation واحد باسمه.");
				foreach (var variation in variations)
				{
					if (variation.VariationOptions == null || !variation.VariationOptions.Any(v => !string.IsNullOrEmpty(v.Value)))
					{
						ModelState.AddModelError("VariationsOptions", "يجب إدخال على الأقل VariationOption واحد باسمه.");
					}
				}
				return View(category);
			}

			else
			{
				this._variationService.AddListOfVariationsAndThierOptions(variations, categoryId);
				this._variationService.SaveChange();
				return RedirectToAction("Categories");
			}
  
		}

		public IActionResult DeleteCategory(int id)
		{
			var category = _categoryService.GetCategory(id);

			if (category == null) return NotFound();
			_categoryService.Delete(category);
			_categoryService.SaveChange();
			return RedirectToAction("Categories");
		}
		public IActionResult EditCategory(int id)
		{
			var category = _categoryService.GetCategory(id);
			if (category == null) return NotFound();
			return View(category);
		}

		public IActionResult DeleteVariation(int id)
		{
			_variationOptionsService.DeleteVariationOptionsByVariationId(id);
			_variationService.DeleteVariationById(id);
			_variationService.SaveChange();
            return Json(new { success = true });
        }

		public IActionResult DeleteOption(int id)
		{
			var variationoption = _variationOptionsService.GetVariationOptionById(id);
			if (variationoption == null) return NotFound();
			_variationOptionsService.DeleteVariationOption(variationoption);
			_variationOptionsService.SaveChange();
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult AddVariation(string Name, int CategoryId)
        {
            // تحققي إذا كانت الـ Variation موجودة بنفس الاسم ونفس الـ Category
            var existingVariation = _variationService
                .GetVariationByNameAndCategory(Name, CategoryId);

            if (existingVariation != null)
            {
                return Json(new { success = false, message = "هذا النوع موجود بالفعل!" });
            }
			
            var variation = new Variation { Name = Name, CategoryId = CategoryId };
            _variationService.AddVariation(variation);
            _variationService.SaveChange();

            return Json(new { success = true });
        }

		public IActionResult EditVariation(string Name , int Id , int CategoryId)
		{
            var existingVariation = _variationService
                .GetVariationByNameAndCategory(Name, CategoryId);

            if (existingVariation != null)
            {
                return Json(new { success = false, message = "هذا النوع موجود بالفعل!" });
            }

            _variationService.EditVariation(Id, Name);
			_variationService.SaveChange();
            return Json(new { success = true });
        }

		public IActionResult EditOption(string Value , int Id , int categoryid)
		{
			var variationoption = _variationOptionsService.GetVariationOptionById(Id);

            var exists = _variationOptionsService
                .GetByValueAndVariation(Value, variationoption.VariationId);

            if (exists != null)
            {
                return Json(new { success = false, message = "هذا الخيار موجود بالفعل!" });
            }
            _variationOptionsService.EditVariationOption(Value, Id);
			_variationOptionsService.SaveChange();
            return Json(new { success = true });
        }
        public IActionResult AddOption(string Value, int Variationid , int categoryid)
        {
            // التحقق هل نفس الـ Option موجودة بالفعل لنفس الـ Variation
            var exists = _variationOptionsService
                .GetByValueAndVariation(Value, Variationid);

            if (exists != null)
            {
                return Json(new { success = false, message = "هذا الخيار موجود بالفعل!" });
            }
			var variation = _variationService.GetVariationById(Variationid);
			var variationoption = new VariationOption
			{
				Value = Value,
				VariationId = Variationid,
				Variation = variation,
							
            };

            _variationOptionsService.CreateVariationOption(variationoption);
            _variationOptionsService.SaveChange();

            return Json(new { success = true });
        }

		public IActionResult DetailsCategory(int id)
		{
			var category = _categoryService.GetCategory(id);
			if (category == null) return NotFound();
			return View(category);
		}
		public IActionResult PendingProducts()
		{
			var penddingproducts = _productService.GetAllPenddingProuct();
			return View(penddingproducts);
		}
		public IActionResult Approve(int id)
		{
			var product = _productService.GetProductById(id);
			if (product == null) return NotFound();
			_productService.ApprovedProduct(id);
			_productService.SeveChanges();
			return RedirectToAction("PendingProducts");
        }

		public IActionResult ApproveAll()
		{
			_productService.ApprovedAllProducts();
			_productService.SeveChanges();
			return RedirectToAction("PendingProducts");
        }

		public IActionResult RejectAll()
		{
			_productService.RejectAllProducts();
			_productService.SeveChanges();
            return RedirectToAction("PendingProducts");
        }
		public IActionResult Reject(int id)
		{
			var product = _productService.GetProductById(id);
			if (product == null) return NotFound();
			_productService.RejectProduct(id);
			_productService.SeveChanges();
            return RedirectToAction("PendingProducts");
        }
    }
}
