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

		public List<Order> AllOrderes() => this._unitOfWork.Orders.AllOrderes();

		public void CreateOrder(CheckOutVM model)
		{
			this._unitOfWork.Orders.CreateOrder(model);

		}

		public OrderDetailsVM GetCartOrderDetails() => this._unitOfWork.Orders.GetCartOrderDetails();

        public Order GetLatestOrder() => this._unitOfWork.Orders.GetLatestOrder();

		public Order GetOrderById(int orderid) => this._unitOfWork.Orders.GetOrderById(orderid);

		public int GetOrderCount() => this._unitOfWork.Orders.GetOrderCount();

        public List<OrderItem> GetOrderItemsByOrderId(int orderid) => this._unitOfWork.Orders.GetOrderItemsByOrderId(orderid);

        public List<SalePerMonthVM> SalesPerMonth() => this._unitOfWork.Orders.SalesPerMonth();

        public void SaveChange()
		{
			this._unitOfWork.Save();
		}
	}
}
