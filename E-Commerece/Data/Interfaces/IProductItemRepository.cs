using E_Commerece.Models;

namespace E_Commerece.Data.Interfaces
{
    public interface IProductItemRepository
    {
        ProductItem GetProductItemByProductId(int productid);
        void AddProductItem(int productId, int quantity , List<int> variationOptions);
        ProductItem GetProductItemById(int id);
        void UpdatePrdItem(ProductItem oldPrdItem, ProductItem newPrdItem);
        void DeletePrdItem(ProductItem item);
    }
}
