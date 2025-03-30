using E_Commerece.Models;

namespace E_Commerece.Services.Interfaces
{
    public interface ICartService
    {
        Cart GetCartByUserId(string userId);
        Cart GetCartById(int id);
        void ClearCart(string userid);
        void SaveChange();
    }
}
