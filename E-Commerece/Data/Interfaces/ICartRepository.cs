using E_Commerece.Models;

namespace E_Commerece.Data.Interfaces
{
    public interface ICartRepository
    {
        Cart GetCartByUserId(string userId);
        Cart GetCartById(int id);
        void ClearCart(string userid);
    }
}
