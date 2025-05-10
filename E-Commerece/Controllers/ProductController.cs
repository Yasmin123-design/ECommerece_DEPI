using E_Commerece.Models;
using E_Commerece.Services.Interfaces;
using E_Commerece.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Security.Claims;

namespace E_Commerece.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductItemService _productitemService;
        private readonly ICartItemService _cartItemService;
        private readonly IWishlistService _wishlistService;
        private readonly ICategoryService _categoryService;
        private readonly IVariationService _variationService;
        public ProductController(IProductService productService,
            IProductItemService productItemService,
            ICartItemService cartItemService,
            IWishlistService wishlistService,
            ICategoryService categoryService,
            IVariationService variationService
            )
        {
            this._productService = productService;
            this._productitemService = productItemService;
            this._cartItemService = cartItemService;
            this._wishlistService = wishlistService;
            this._categoryService = categoryService;
            this._variationService = variationService;
        }
        public IActionResult Index()
        {
            var products = _productService.GetAllProducts();
            return View(products);
        }
        public IActionResult GetAllProducts()
        {
            if (User.IsInRole("Admin"))
            {
                ViewBag.IsAdmin = true;
            }
            else
            {
                ViewBag.IsAdmin = false;
            }
                var products = _productService.GetAllProductsJsonReturned();
            return Json(new { products });
        }

        [Authorize(Roles = "User")]
        public IActionResult QuickView(int id)
        {
            var product = _productService.GetProductById(id);


            if (product == null)
            {
                return NotFound();
            }

            var selectedProductItem = product.Items
      .FirstOrDefault(item => item.VariationOptions.All(option =>
          product.Items.SelectMany(i => i.VariationOptions).Any(o => o.Id == option.Id)
      ));
            var viewModel = new QuickViewModel
            {

                Product = product,
                Variations = product.Category.Variations.ToList(),  // 🔥 جميع الـ Variations الخاصة بـ Category
                SelectedVariationOptions = product.Items.SelectMany(i => i.VariationOptions).ToList(), // 🔥 الخيارات المختارة
                SelectedProductItem = selectedProductItem
            };
            var val = viewModel.SelectedProductItem.Quantity;
            var val2 = viewModel.SelectedProductItem;

            var count = selectedProductItem.Quantity;
            Console.WriteLine(count);
            return View(viewModel);
        }
        public IActionResult FilterByCategory(int categoryId)
        {
            var products = _productService.FilteredProductByCategoryId(categoryId);
            ViewBag.SelectedCategoryId = categoryId;
            return View(products);
        }

        [AllowAnonymous]
        public IActionResult FilterByCategoryReturnedJson(string categoryIds)
        {
            bool isAdmin = User.IsInRole("Admin");

            

            if (string.IsNullOrEmpty(categoryIds))
            {
                var products = _productService.GetAllProducts();
                return Json(new { isAdmin, products });
            }
            else
            {
                var categoryIdsList = categoryIds.Split(',').Select(int.Parse).ToList();
                var products = isAdmin
                    ? _productService.GetProductsByCategoryIdJsonReturnedForAdmin(categoryIdsList)
                    : _productService.GetProductsByCategoryIdJsonReturnedForUser(categoryIdsList);
                return Json(new { isAdmin, products });
            }

            
        }

        public IActionResult Cameras()
        {
            var products = _productService.GetCameraProducts();
            return View("FilterByCategory", products);
        }

        public IActionResult Laptops()
        {
            var products = _productService.GetLaptopsProducts();
            return View("FilterByCategory", products);
        }
        public IActionResult SmartPhones()
        {
            var products = _productService.GetSmartPhoneProducts();
            return View("FilterByCategory", products);
        }
        public IActionResult Accessories()
        {
            var products = _productService.GetAccessoriesProducts();
            return View("FilterByCategory", products);
        }
        public IActionResult ProductsStartWith(string query)
        {
            var products = _productService.GetAllProductsStartWith(query);
            return View("FilterByCategory", products);
        }
        [Authorize(Roles = "User")]
        public IActionResult AddReviewOnProduct(int productId, string comment, int rating)
        {
            _productService.AddReviewOnProduct(productId, comment, rating);
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            _productService.SeveChanges();
            return Json(new
            {
                success = true,
                message = "Review added successfully",
                userEmail = email, // استرجاع اسم المستخدم
                rating = rating,
                comment = comment
            });
        }

        public IActionResult GetAvailableQuantity(int productId, string optionIds)
        {
            var selectedOptionIds = optionIds.Split(',').Select(int.Parse).ToList();
            int quantity = this._productService.GetAvailableQuantity(productId, selectedOptionIds);
            return Json(new {quantity});
        }

        [Authorize(Roles = "User")]
        public IActionResult AddToCart(int productId, string optionIds, int SelectedQuantity)
        {
            var product = this._productService.GetProductById(productId);
            var selectedOptionIds = optionIds.Split(',').Select(int.Parse).ToList();
            var productitem = this._productService.CheckAddProductToCart(productId, selectedOptionIds);
            string message;
            bool check;
            if (productitem != null)
            {
                if (SelectedQuantity <= productitem.Quantity)
                {
                    check = this._cartItemService.AddCartItem(productId, productitem, SelectedQuantity);
                    _cartItemService.SeveChanges();
                    if (check)
                    {
                        message = "Your Product Added To Your Cart";
                    }
                    else
                    {
                        message = "Your Product Already Exist In Your Cart";
                    }

                }
                else
                {
                    message = "This Product Out Of Stock";
                }
            }
            else
            {
                message = "This Product Out Of Stock";
            }
            return Json(new { message });
        }
        public IActionResult GetProductsByPage(int pageNumber)
        {
            int pageSize = 5;
            var products = this._productService.GetProductsByPage(pageNumber);

            return Json(products);
        }

        [Authorize(Roles = "User")]
        public IActionResult AddToWishlist(int productid)
        {
            try
            {
                bool? check = this._productService.AddProductToWishlist(productid);
                if (check == true)
                {
                    this._productService.SeveChanges();
                    return Json(new { success = true, message = "تمت إضافة المنتج إلى قائمة الرغبات!" });
                }
                else
                {
                    return Json(new { success = true, message = "المنتج موجود بالفعل فى قائمه الرغبات" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "حدث خطأ أثناء الإضافة!", error = ex.Message });
            }
        }

        public IActionResult DeleteFromWishlist([FromBody] DeletedFromWishlistVm model)
        {
            _productService.DeleteProductFromWishlist(model.Id);
            _productService.SeveChanges();
            return Json(new { success = true });
        }

        [HttpGet]
        public IActionResult ShowAllProductsSelledBySpecificUser()
        {
            var products = _productService.ShowAllProductsSelledBySpecificUser();
            if (products == null) { }
            return View(products);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var categories = this._categoryService.GetAllCategories();
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product model, IFormFile imageFile)
        {
           
            if (imageFile == null || imageFile.Length == 0)
            {
                ModelState.AddModelError(nameof(imageFile), "Image is required.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img");

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imageFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        imageFile.CopyTo(fileStream);
                    }

                  
                    model.Image = uniqueFileName;

                    if (User.IsInRole("User"))
                    {
                        _productService.CreateSelledProductByUser(model);
                    }
                    if (User.IsInRole("Admin"))
                    {
                        _productService.CreateProductByAdmin(model);
                    }
                        _productService.SeveChanges();

                    return RedirectToAction("AddVariationOptionsToSelledProductByUser", new { ProductId = model.Id, CategoryId = model.CategoryId });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(nameof(imageFile), "An error occurred while uploading the image: " + ex.Message);
                }
            }

            ViewBag.Categories = _categoryService.GetAllCategories();
            return View(model);
        }


        [HttpGet]
        public IActionResult AddVariationOptionsToSelledProductByUser(int ProductId, int CategoryId)
        {
            var variations = _variationService.GetVariationByCategoryId(CategoryId);
            if (User.IsInRole("Admin"))
            {
                ViewBag.IsAdmin = true;
            }
            else
            {
                ViewBag.IsAdmin = false;
            }
            ViewBag.ProductID = ProductId;
            return View(variations);
        }

        [HttpPost]
        public IActionResult AddVariationOptionsToSelledProductByUser(int ProductId, List<int> SelectedOptions, int Quantity)
        {
            if (User.IsInRole("Admin"))
            {
                ViewBag.IsAdmin = true;
            }
            else
            {
                ViewBag.IsAdmin = false;
            }
            var variations = _variationService.GetVariationByCategoryId(_productService.GetCategoryIdByProductId(ProductId));
            try
            {
                if (SelectedOptions == null || !SelectedOptions.Any())
            {
                ModelState.AddModelError("", "Please select at least one variation option.");
                ViewBag.ProductID = ProductId;
                    return View(variations); 
            }

            _productitemService.AddProductItem(ProductId, Quantity, SelectedOptions);
            _productitemService.SeveChanges();

                ViewBag.ProductID = ProductId;
                return View(variations); 
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred: " + ex.Message);
                ViewBag.ProductID = ProductId;
                return View(variations);
             }
    
        }

        public IActionResult ProductAddededBySellerDetails(int id)
        {
            var product = _productService.GetProductById(id);


            if (product == null)
            {
                return NotFound();
            }

            var selectedProductItem = product.Items
      .FirstOrDefault(item => item.VariationOptions.All(option =>
          product.Items.SelectMany(i => i.VariationOptions).Any(o => o.Id == option.Id)
      ));
            var viewModel = new QuickViewModel
            {

                Product = product,
                Variations = product.Category.Variations.ToList(),  // 🔥 جميع الـ Variations الخاصة بـ Category
                SelectedVariationOptions = product.Items.SelectMany(i => i.VariationOptions).ToList(), // 🔥 الخيارات المختارة
                SelectedProductItem = selectedProductItem
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult EditProductAddededBySeller(int id)
        {
            var product = _productService.GetProductById(id);
            ViewBag.Categories = _categoryService.GetAllCategories();
            ViewBag.Variation = _variationService.GetVariationByCategoryId(product.CategoryId);
            ViewBag.SelectedVariationOptionIds = product.Items
                .ToDictionary(item => item.Id, item => item.VariationOptions.Select(v => v.Id).ToList());
            // {1 , [5,6,7]}
            return View(product);
        }

        [HttpPost]
        public IActionResult EditProductAddededBySeller(Product model, IFormCollection form , IFormFile? ProductImage)
        {
            var existingProduct = _productService.GetProductById(model.Id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.Name = model.Name;
            existingProduct.Price = model.Price;
            existingProduct.Description = model.Description;
            existingProduct.CategoryId = model.CategoryId;

            if (ProductImage != null && ProductImage.Length > 0)
            {
                var fileName = Path.GetFileName(ProductImage.FileName);
                var filePath = Path.Combine("wwwroot/images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    ProductImage.CopyTo(stream);
                }

                existingProduct.Image = fileName;
            }

            foreach (var item in model.Items)
            {
                var existingItem = existingProduct.Items.FirstOrDefault(i => i.Id == item.Id);
                if (existingItem != null)
                {
                    existingItem.Quantity = item.Quantity;

                    // استخراج SelectedVariationOptionIds من الـ FormCollection
                    var selectedOptions = form[$"Items[{model.Items.IndexOf(item)}].SelectedVariationOptionIds"]
                        .Select(int.Parse).ToList();

                    existingItem.VariationOptions = _variationService.GetVariationOptionsByIds(selectedOptions);
                }
            }

            _variationService.SaveChange();
            return RedirectToAction("ShowAllProductsSelledBySpecificUser");
        }

        public IActionResult DeletePrdFromSelledPrds([FromBody]SelledProductVM model)
        {
            var product = _productService.GetProductById(model.Id);
            if(product != null)
            {
                _productService.DeleteProductFromSelledPrds(product);
                _productService.SeveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Product not found" });
        }

        
    }
}
