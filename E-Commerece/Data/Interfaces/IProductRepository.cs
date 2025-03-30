using E_Commerece.Models;
using Microsoft.CodeAnalysis;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace E_Commerece.Data.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts();
        List<Product> FilteredProductByCategoryId(int categoryid);
        Product GetProductById(int id);

        List<Product> GetCameraProducts();
        List<Product> GetSmartPhoneProducts();
        List<Product> GetLaptopsProducts();
        List<Product> GetAccessoriesProducts();
        List<Product> GetAllProductsStartWith(string query);
        void AddReviewOnProduct(int productId, string comment, int rating);

        int GetAvailableQuantity(int productId, List<int> optionIds);
        ProductItem CheckAddProductToCart(int productId, List<int> optionIds);
		List<object> GetProductsByPage(int pageNumber);
		List<object> GetProductsByCategoryIdJsonReturned(List<int> categoryIds);
        List<object> GetAllProductsJsonReturned();
        List<object> GetTopRatedProducts();
        bool? AddProductToWishlist(int productid);

        void DeleteProductFromWishlist(int id);
        List<Product> ShowAllProductsSelledBySpecificUser();
        void CreateSelledProductByUser(Product product);

        int GetCategoryIdByProductId(int productId);
        void DeleteProductFromSelledPrds(Product prd);

        List<Order> GetLatestPurchasedProducts();
        List<Product> GetLatestSoldProducts();
        List<Order> GetRequestedProducts();
    }
}
