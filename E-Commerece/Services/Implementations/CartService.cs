using E_Commerece.Models;
using E_Commerece.Services.Interfaces;
using E_Commerece.UnitOfWork.Interfaces;

namespace E_Commerece.Services.Implementations
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public void ClearCart(string userid)
        {
            this._unitOfWork.Carts.ClearCart(userid);
        }

        public Cart GetCartById(int id) => this._unitOfWork.Carts.GetCartById(id);

        public Cart GetCartByUserId(string userId) => this._unitOfWork.Carts.GetCartByUserId(userId);

        public void SaveChange()
        {
            this._unitOfWork.Save();
        }
    }
}
