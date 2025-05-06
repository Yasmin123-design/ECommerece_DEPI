using E_Commerece.Models;
using E_Commerece.Services.Interfaces;
using E_Commerece.UnitOfWork.Interfaces;
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
		private readonly IOrderService _orderService;
		private readonly IUserService _userService;
		private readonly IProductItemService _productItemService;
		private readonly IUnitOfWork _unitOfWork;
		public AdminController(ICategoryService categoryService
			, IVariationService variationService
			, IVariationOptionsService variationOptionsService
			, IProductService productService
			, IUserService userService
			, IOrderService orderService
			, IProductItemService productItemService
			, IUnitOfWork unitOfWork
			)
		{
			this._categoryService = categoryService;
			this._variationService = variationService;
			this._variationOptionsService = variationOptionsService;
			this._productService = productService;
			this._userService = userService;
			this._orderService = orderService;
			this._productItemService = productItemService;
			this._unitOfWork = unitOfWork;
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

		public IActionResult Analytics()
		{
            ViewBag.TotalUsers = _userService.GetUserCount();
            ViewBag.TotalOrders = _orderService.GetOrderCount();
			ViewBag.TotalCategories = _categoryService.GetCategoryCount();
			ViewBag.TotalProducts = _productService.GetProductCount();
		    var  SalesPerMonth = _orderService.SalesPerMonth();
            // مصفوفة تمثل الشهور كلها
            string[] allMonths = new[] { "january", "february", "march", "april", "may", "june",
                             "july", "august", "september", "october", "november", "december" };
            var monthlySales = new List<decimal>();

            foreach (var month in allMonths)
            {
                var sale = SalesPerMonth.FirstOrDefault(s => s.Month.ToLower() == month);
                monthlySales.Add(sale != null ? sale.TotalPrice : 0);
            }
			ViewBag.SalesPerMonth = monthlySales;

            return View();
        }

		public IActionResult Orderes()
		{
			var orders = _orderService.AllOrderes();
			return View(orders);
		}
		public IActionResult AcceptOrder(int id)
		{
			var order = _orderService.GetOrderById(id);
			order.Status = Status.Shipped;
			_orderService.SaveChange();
            return Json(new { redirectUrl = Url.Action("Orderes", "Admin") });
			
        }

		public IActionResult rejectOrder(int id)
		{
            var order = _orderService.GetOrderById(id);
            order.Status = Status.Cancelled;
            _orderService.SaveChange();
            return Json(new { redirectUrl = Url.Action("Orderes", "Admin") });
        }
		public IActionResult MarkAsDelievered(int id)
		{
            var order = _orderService.GetOrderById(id);
            order.Status = Status.Delivered;
            _orderService.SaveChange();
            return Json(new { redirectUrl = Url.Action("Orderes", "Admin") });
        }

        public IActionResult AllPrdsToManage()
        {
            var products = _productService.AllPrdsToManage();
            ViewBag.IsAdmin = true;
            return View(products);
        }

		public IActionResult DeleteProduct([FromBody]DeletePrdVM prd)
		{
			var product = _productService.GetProductById(prd.Id);
			_productService.DeletedProduct(product);
			_productService.SeveChanges();
            return Json(new { success = true });
        }
		public IActionResult EditProduct(int id)
		{
			var product = _productService.GetProductById(id);
            var variations = _variationService.GetVariationByCategoryId(product.CategoryId);
            ViewBag.Variations = variations;
            return View(product);
		}
        [HttpPost]
        public IActionResult EditProduct(Product model, IFormFile imageFile)
        {
            if (imageFile == null && !string.IsNullOrEmpty(model.Image))
            {
                // إزالة الخطأ لو موجود على الـ Image
                ModelState.Remove("imageFile");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

			var existingProduct = _productService.GetProductById(model.Id);

            if (existingProduct == null)
            {
                return NotFound();
            }
            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img");
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(fileStream);
                }

                if (!string.IsNullOrEmpty(existingProduct.Image))
                {
                    var oldImagePath = Path.Combine(uploadsFolder, existingProduct.Image);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

				existingProduct.Image = uniqueFileName;
            }
            else
            {
				existingProduct.Image = model.Image;
            }

            _productService.UpdateProduct(existingProduct, model);

            for (int i = 0; i < model.Items.Count; i++)
            {
                var item = model.Items[i];
				var existingItem = _productItemService.GetProductItemById(item.Id);
                if (existingItem != null)
                {
					_productItemService.UpdatePrdItem(existingItem, item);

                    for (int j = 0; j < item.VariationOptions.Count; j++)
                    {
                        var option = item.VariationOptions[j];
						var existingOption = _variationOptionsService.GetVariationOptionById(option.Id);
                        if (existingOption != null)
                        {
							_variationOptionsService.UpdateVariationOption(existingOption, option);
                        }
                    }
                }
            }
			_unitOfWork.Save();
            ViewBag.IsAdmin = true;
            return RedirectToAction("AllPrdsToManage");
        }

		public IActionResult DeletedPrdItem(int id)
		{
			var prditem = _productItemService.GetProductItemById(id);
			_productItemService.DeletePrdItem(prditem);
			_productItemService.SeveChanges();
            return Json(new { success = true });
        }

		public IActionResult AddNewPrdItem([FromBody]SaveVariationRequestVM model)
		{
            if (model == null || model.SelectedOptions == null || !model.SelectedOptions.Any())
            {
                return BadRequest("البيانات غير مكتملة");
            }
            List<int> optionIds = model.SelectedOptions.Select(x => x.OptionId).ToList();
			_productItemService.AddProductItem(model.ProductId, model.Quantity, optionIds);
			_productItemService.SeveChanges();
            return Json(new { success = true });
        }
    }
}
