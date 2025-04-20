using E_Commerece.Models;
using E_Commerece.Services.Interfaces;
using E_Commerece.UnitOfWork.Interfaces;

namespace E_Commerece.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public ProductItem CheckAddProductToCart(int productId, List<int> optionIds) => this._unitOfWork.Products.CheckAddProductToCart(productId, optionIds);

        public void AddReviewOnProduct(int productId, string comment, int rating)
        {
            this._unitOfWork.Products.AddReviewOnProduct(productId, comment, rating);
        }

        public List<Product> FilteredProductByCategoryId(int categoryid) => this._unitOfWork.Products.FilteredProductByCategoryId(categoryid);

        public List<Product> GetAccessoriesProducts() => this._unitOfWork.Products.GetAccessoriesProducts();

        public List<Product> GetAllProducts() => this._unitOfWork.Products.GetAllProducts();

        public List<Product> GetAllProductsStartWith(string query) => this._unitOfWork.Products.GetAllProductsStartWith(query);

        public int GetAvailableQuantity(int productId, List<int> optionIds) => this._unitOfWork.Products.GetAvailableQuantity(productId, optionIds);

        public List<Product> GetCameraProducts() => this._unitOfWork.Products.GetCameraProducts();

        public List<Product> GetLaptopsProducts() => this._unitOfWork.Products.GetLaptopsProducts();

        public Product GetProductById(int id) => this._unitOfWork.Products.GetProductById(id);

        public List<Product> GetSmartPhoneProducts() => this._unitOfWork.Products.GetSmartPhoneProducts();

        public void SeveChanges()
        {
            _unitOfWork.Save();
        }

        public List<object> GetProductsByPage(int pageNumber) => this._unitOfWork.Products.GetProductsByPage(pageNumber);

        public List<object> GetProductsByCategoryIdJsonReturned(List<int> categoryIds) => this._unitOfWork.Products.GetProductsByCategoryIdJsonReturned(categoryIds);

        public List<object> GetAllProductsJsonReturned() => this._unitOfWork.Products.GetAllProductsJsonReturned();

        public List<object> GetTopRatedProducts() => this._unitOfWork.Products.GetTopRatedProducts();

        public bool? AddProductToWishlist(int productid) => this._unitOfWork.Products.AddProductToWishlist(productid);

        public void DeleteProductFromWishlist(int id) => this._unitOfWork.Products.DeleteProductFromWishlist(id);

        public List<Product> ShowAllProductsSelledBySpecificUser() => this._unitOfWork.Products.ShowAllProductsSelledBySpecificUser();

        public void CreateSelledProductByUser(Product product) => this._unitOfWork.Products.CreateSelledProductByUser(product);

        public int GetCategoryIdByProductId(int productId) => this._unitOfWork.Products.GetCategoryIdByProductId(productId);

        public void DeleteProductFromSelledPrds(Product prd)
        {
            this._unitOfWork.Products.DeleteProductFromSelledPrds(prd);
        }

        public List<Order> GetLatestPurchasedProducts() => this._unitOfWork.Products.GetLatestPurchasedProducts();

        public List<Product> GetLatestSoldProducts() => this._unitOfWork.Products.GetLatestSoldProducts();

        public List<Order> GetRequestedProducts() => this._unitOfWork.Products.GetRequestedProducts();

        public List<Product> GetAllPenddingProuct() =>
            this._unitOfWork.Products.GetAllPenddingProuct();

        public void ApprovedProduct(int id)
        {
            this._unitOfWork.Products.ApprovedProduct(id);
        }

        public void ApprovedAllProducts()
        {
            this._unitOfWork.Products.ApprovedAllProducts();
        }

        public void RejectAllProducts()
        {
            this._unitOfWork.Products.RejectAllProducts();
        }

        public void RejectProduct(int id)
        {
            this._unitOfWork.Products.RejectProduct(id);
        }
    }
}
