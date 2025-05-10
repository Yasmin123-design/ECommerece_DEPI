using E_Commerece.Models;

namespace E_Commerece.Data.Interfaces
{
    public interface ICartItemRepository
    {
        bool AddCartItem(int productid, ProductItem productItem, int quantity );
        CartItem GetCartItem(int itemid);
        List<CartItem> GetAllItemsByCartId(int cartid);
        void RemoveCartItem(int id);
        void RemoveAllItemsRelatedByCart(int cartid);
        List<CartItem> GetCartItems();
    }
}
