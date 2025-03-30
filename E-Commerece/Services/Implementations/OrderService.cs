using E_Commerece.Models;
using E_Commerece.Services.Interfaces;
using E_Commerece.UnitOfWork.Interfaces;
using E_Commerece.ViewModels;

namespace E_Commerece.Services.Implementations
{
	public class OrderService : IOrderService
	{
		private readonly IUnitOfWork _unitOfWork;
		public OrderService(IUnitOfWork unitOfWork)
		{
			this._unitOfWork = unitOfWork;
		}
		public void CreateOrder(CheckOutVM model)
		{
			this._unitOfWork.Orders.CreateOrder(model);

		}

		public Order GetLatestOrder() => this._unitOfWork.Orders.GetLatestOrder();

		public Order GetOrderById(int orderid) => this._unitOfWork.Orders.GetOrderById(orderid);

		public List<OrderItem> GetOrderItemsByOrderId(int orderid) => this._unitOfWork.Orders.GetOrderItemsByOrderId(orderid);

        public void SaveChange()
		{
			this._unitOfWork.Save();
		}
	}
}
