using E_Commerece.Data.Interfaces;
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
                return _context.Products.Include(x => x.Category).ToList(); // إرجاع كل المنتجات
            }
            else
            {
                return _context.Products.Include(x => x.Category).Where(x => x.CategoryId == categoryid).ToList();
            }

        }

        public List<Product> GetAccessoriesProducts() => _context.Products.Include(p => p.Category).Where(p => p.Category.Name == "Accessories").ToList();

        public List<Product> GetAllProducts() => _context.Products.Include(p => p.Category).Where(x => x.IsApproved).ToList();

        public List<Product> GetAllProductsStartWith(string query) => _context.Products.Where(p => p.Name.StartsWith(query)).ToList();

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
        public List<Product> GetCameraProducts() => _context.Products.Include(p => p.Category).Where(p => p.Category.Name == "Cameras").ToList();

        public List<Product> GetLaptopsProducts() => _context.Products.Include(p => p.Category).Where(p => p.Category.Name == "Laptops").ToList();

        public Product GetProductById(int id) =>
     _context.Products
         .Include(p => p.Reviews)
         .Include(p => p.Category)
             .ThenInclude(c => c.Variations) // 🔥 تضمين Variations الخاصة بـ Category
         .Include(p => p.Items)
             .ThenInclude(pi => pi.VariationOptions)
                 .ThenInclude(vo => vo.Variation)
         .FirstOrDefault(p => p.Id == id);


        public List<Product> GetSmartPhoneProducts() => _context.Products.Include(p => p.Category).Where(p => p.Category.Name == "Smartphones").ToList();


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

        public List<object> GetProductsByCategoryIdJsonReturned(List<int> categoryIds)
        {
			return _context.Products
				 .Where(p => categoryIds.Contains(p.CategoryId))
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
            return this._context.Products.Where(p => p.Reviews.Any()).Select(p => new
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
        public List<Order> GetRequestedProducts() =>
         _context.Orders
             .Include(o => o.Items)
             .ThenInclude(i => i.ProductItem)
             .ThenInclude(p => p.Product)
             .Where(o => o.Items.Any(i => i.ProductItem.Product.SellerId == userid)) 
             .OrderByDescending(o => o.OrderDate) 
             .ToList();

    }
}
