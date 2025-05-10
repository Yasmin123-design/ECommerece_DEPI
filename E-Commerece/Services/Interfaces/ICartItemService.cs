using E_Commerece.Models;

namespace E_Commerece.Services.Interfaces
{
    public interface ICartItemService
    {
        void SeveChanges();
        bool AddCartItem(int productid, ProductItem productItem, int quantity);
        CartItem GetCartItem(int itemid);
        void RemoveCartItem(int id);
        void RemoveAllItemsRelatedByCart(int cartid);
        List<CartItem> GetCartItems();
    }
}
