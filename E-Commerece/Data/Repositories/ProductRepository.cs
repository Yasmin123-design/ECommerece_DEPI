﻿using E_Commerece.Data.Interfaces;
using E_Commerece.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using System.Security.Claims;
namespace E_Commerece.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly EcommereceContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
        private readonly string userid;

        public ProductRepository(EcommereceContext context)
        {
			this._context = context;
            userid = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
		}

        public void AddReviewOnProduct(int productId, string comment, int rating)
        {
            _context.Reviews.Add(
                new Review
                {
                    Comment = comment,
                    Rating = rating,
                    ProductId = productId,
                    UserId = userid
                });
        }

        public List<Product> FilteredProductByCategoryId(int categoryid)
        {
            if (categoryid == 0 || !_context.Categories.Any(c => c.Id == categoryid))
            {
                return _context.Products.Include(x => x.Category).Where(x => x.ApprovalStatus == ProductStatus.Accept).ToList(); // إرجاع كل المنتجات
            }
            else
            {
                return _context.Products.Include(x => x.Category).Where(x => x.CategoryId == categoryid && x.ApprovalStatus == ProductStatus.Accept).ToList();
            }

        }

        public List<Product> GetAccessoriesProducts() => _context.Products.Include(p => p.Category).Where(p => p.Category.Name == "Accessories" && p.ApprovalStatus == ProductStatus.Accept).ToList();

        public List<Product> GetAllProducts() => _context.Products.Include(p => p.Category).Where(x => x.ApprovalStatus == ProductStatus.Accept).ToList();

        public List<Product> GetAllProductsStartWith(string query) => _context.Products.Where(p => p.Name.StartsWith(query) && p.ApprovalStatus == ProductStatus.Accept).ToList();

        public int GetAvailableQuantity(int productId, List<int> optionIds)
        {
            var product = GetProductById(productId);

            if (product == null)
            {
                return 0;
            }

            // البحث عن `ProductItem` الذي يحتوي على جميع الخيارات المحددة
            var selectedProductItem = product.Items.FirstOrDefault(item =>
                optionIds.All(id => item.VariationOptions.Any(o => o.Id == id))
            );
            return selectedProductItem?.Quantity ?? 0;
        }

        public ProductItem CheckAddProductToCart(int productId, List<int> optionIds)
        {
            var product = GetProductById(productId);

            // البحث عن `ProductItem` الذي يحتوي على جميع الخيارات المحددة
            var selectedProductItem = product.Items.FirstOrDefault(item =>
                optionIds.All(id => item.VariationOptions.Any(o => o.Id == id))
            );
            return selectedProductItem;
        }
        public List<Product> GetCameraProducts() => _context.Products.Include(p => p.Category).Where(p => p.Category.Name == "Cameras" && p.ApprovalStatus == ProductStatus.Accept).ToList();

        public List<Product> GetLaptopsProducts() => _context.Products.Include(p => p.Category).Where(p => p.Category.Name == "Laptops" && p.ApprovalStatus == ProductStatus.Accept).ToList();

        public Product GetProductById(int id) =>
     _context.Products
         .Include(p => p.Reviews)
         .Include(p => p.Category)
             .ThenInclude(c => c.Variations) // 🔥 تضمين Variations الخاصة بـ Category
         .Include(p => p.Items)
             .ThenInclude(pi => pi.VariationOptions)
                 .ThenInclude(vo => vo.Variation)
            .ThenInclude(x => x.Category)
         .FirstOrDefault(p => p.Id == id);


        public List<Product> GetSmartPhoneProducts() => _context.Products.Include(p => p.Category).Where(p => p.Category.Name == "Smartphones" && p.ApprovalStatus == ProductStatus.Accept).ToList();


        public List<object> GetProductsByPage(int pageNumber)
        {
            int pageSize = 5;
            return _context.Products
                           .Skip((pageNumber - 1) * pageSize)
                           .Take(pageSize)
                           .Select(p => new
                           {
                               Id = p.Id,
                               Name = p.Name,
                               Price = p.Price,
                               Image = p.Image,
                               Category = p.Category.Name
                           })
                           .ToList<object>(); // تحويل القائمة إلى `List<object>` لاستخدامها مع JSON
        }

        public List<object> GetProductsByCategoryIdJsonReturnedForUser(List<int> categoryIds)
        {
			return this._context.Products
				 .Where(p => categoryIds.Contains(p.CategoryId) && p.ApprovalStatus == ProductStatus.Accept )
				.Select(p => new
				{
					Id = p.Id,
					Name = p.Name,
					Price = p.Price,
					Image = p.Image, 
					Category = p.Category.Name
				}).ToList<object>();
		}

        public List<object> GetProductsByCategoryIdJsonReturnedForAdmin(List<int> categoryIds)
        {
            return this._context.Products
                 .Where(p => categoryIds.Contains(p.CategoryId) && p.ApprovalStatus == ProductStatus.Accept && p.SellerId == null)
                .Select(p => new
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Image = p.Image,
                    Category = p.Category.Name
                }).ToList<object>();
        }
        public List<object> GetAllProductsJsonReturned()
		{
			return _context.Products
                           .Where(x => x.ApprovalStatus == ProductStatus.Accept)
						   .Include(x => x.Category)
						   .Select(p => new
						   {
							   Id = p.Id,
							   Name = p.Name,
							   Price = p.Price,
							   Image = p.Image,
							   Category = p.Category.Name
						   })
						   .ToList<object>();
		}

		public List<object> GetTopRatedProducts()
		{
            return this._context.Products.Where(p => p.Reviews.Any() && p.ApprovalStatus == ProductStatus.Accept).Select(p => new
            {
                Product = p,
                AverageRating = p.Reviews.Average(r => r.Rating)
            }).OrderByDescending(p => p.AverageRating).Take(5).ToList<object>();
		}

        public bool? AddProductToWishlist(int productid)
        {
            var product = this._context.Products.Where(x => x.Id == productid).FirstOrDefault();
            if (product == null) return null; // تأكد أن المنتج موجود

            if (string.IsNullOrEmpty(userid)) return null;

            // البحث عن قائمة الرغبات للمستخدم الحالي
            var wishlist = _context.Wishlists
                .Include(w => w.Products)
                .FirstOrDefault(w => w.UserId == userid);

            if (wishlist == null)
            {
                // إنشاء قائمة رغبات جديدة إذا لم تكن موجودة
                wishlist = new Wishlist { UserId = userid, Products = new List<Product>() };
                _context.Wishlists.Add(wishlist);
            }

            // التأكد من عدم إضافة المنتج مرتين
            if (wishlist.Products.Any(p => p.Id == productid))
            {
                return false;
                
            }
            wishlist.Products.Add(product);
            return true;
        }

        public void DeleteProductFromWishlist(int id)
        {
            var wishlist = _context.Wishlists.Include(x => x.Products).FirstOrDefault(x => x.UserId == userid);
            var product = GetProductById(id);
            if (product == null) return;
            wishlist.Products.Remove(product);
        }

		public List<Product> ShowAllProductsSelledBySpecificUser()
		{
            var products = _context.Products.Where(x => x.SellerId == userid).ToList();
            return products;
		}

        public void CreateSelledProductByUser(Product product)
        {
            if (product == null) return;
            product.SellerId = userid;
            product.IsApproved = false;
            product.ApprovalStatus = ProductStatus.Pendding;
            _context.Products.Add(product);
        }
        public void CreateProductByAdmin(Product product)
        {
            if (product == null) return;
            product.SellerId = null;
            product.IsApproved = true;
            product.ApprovalStatus = ProductStatus.Accept;
            _context.Products.Add(product);
        }
        public int GetCategoryIdByProductId(int productId)
        {
            var product = GetProductById(productId);
            if (product == null) return 0;
            return product.CategoryId;
        }

        public void DeleteProductFromSelledPrds(Product prd)
        {
            this._context.Products.Remove(prd);
        }

        public List<Order> GetLatestPurchasedProducts() =>
            _context.Orders
            .Include(x => x.Items)
            .ThenInclude(x => x.ProductItem)
            .ThenInclude(x => x.Product)
            .Where(o => o.UserId == userid)
            .OrderByDescending(o => o.OrderDate)
            .ToList();

        public List<Product> GetLatestSoldProducts() =>
            _context.Products.Where(x => x.SellerId == userid)
            .ToList();
        public List<OrderItem> GetRequestedItemsForSeller() =>
     _context.OrderItems
         .Include(i => i.ProductItem)
             .ThenInclude(pi => pi.Product)
         .Include(i => i.Order)
         .Where(i => i.ProductItem.Product.SellerId == userid)
         .OrderByDescending(i => i.Order.OrderDate)
         .ToList();


        public List<Product> GetAllPenddingProuct() => this._context.Products.Include(x => x.Category).Include(x => x.Seller).Where(x => x.SellerId != null && x.ApprovalStatus == ProductStatus.Pendding).ToList();

        public void ApprovedProduct(int id)
        {
            var product = GetProductById(id);
            product.IsApproved = true;
            product.ApprovalStatus = ProductStatus.Accept;
        }

        public void ApprovedAllProducts()
        {
            var products = GetAllPenddingProuct();
            foreach(var product in products)
            {
                product.IsApproved = true;
                product.ApprovalStatus = ProductStatus.Accept;
            }
        }

        public void RejectAllProducts()
        {
            var products = GetAllPenddingProuct();
            foreach(var product in products)
            {
                product.IsApproved = false;
                product.ApprovalStatus = ProductStatus.Reject;
            }
        }

        public void RejectProduct(int id)
        {
            var product = GetProductById(id);
            product.IsApproved = false;
            product.ApprovalStatus = ProductStatus.Reject;
        }

        public int GetProductCount() => this._context.Products.Where(x => x.SellerId == null).Count();

        public List<Product> AllPrdsToManage() => this._context.Products.Include(x => x.Category).Where(x => x.SellerId == null).ToList();

        public void DeletedProduct(Product product)
        {
            this._context.Products.Remove(product);
        }

        public void UpdateProduct(Product oldPrd, Product newPrd)
        {
            oldPrd.Price = newPrd.Price;
            oldPrd.Name = newPrd.Name;
            oldPrd.Description = newPrd.Description;
        }
    }
}
