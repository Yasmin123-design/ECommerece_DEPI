using E_Commerece.Models;

namespace E_Commerece.Services.Interfaces
{
    public interface IProductItemService
    {
        ProductItem GetProductItemByProductId(int productid);
        void AddProductItem(int productId, int quantity , List<int> variationOptions);
        void SeveChanges();
    }
}
