using E_Commerece.Data.Interfaces;
using E_Commerece.Models;
using E_Commerece.Services.Interfaces;
using E_Commerece.UnitOfWork.Interfaces;

namespace E_Commerece.Services.Implementations
{
    public class CartItemService : ICartItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartItemService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public bool  AddCartItem(int productid, ProductItem productItem, int quantity) => this._unitOfWork.CartItems.AddCartItem(productid, productItem, quantity);

        public CartItem GetCartItem(int itemid) => this._unitOfWork.CartItems.GetCartItem(itemid);

        public void RemoveCartItem(int id)
        {
            this._unitOfWork.CartItems.RemoveCartItem(id);
        }

        public void RemoveAllItemsRelatedByCart(int cartid)
        {
            this._unitOfWork.CartItems.RemoveAllItemsRelatedByCart(cartid);
        }



		public void SeveChanges()
		{
            _unitOfWork.Save();
        }
	}
}
