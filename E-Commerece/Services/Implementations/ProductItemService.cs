using E_Commerece.Data.Interfaces;
using E_Commerece.Migrations;
using E_Commerece.Models;
using E_Commerece.Services.Interfaces;
using E_Commerece.UnitOfWork.Interfaces;

namespace E_Commerece.Services.Implementations
{
    public class ProductItemService : IProductItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductItemService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public void AddProductItem(int productId, int quantity , List<int> variationOptions)
        {
            this._unitOfWork.ProductItems.AddProductItem(productId, quantity,variationOptions);
        }

        public ProductItem GetProductItemByProductId(int productid) => this._unitOfWork.ProductItems.GetProductItemByProductId(productid);

        public void SeveChanges()
        {
            this._unitOfWork.Save();
        }
    }
}
