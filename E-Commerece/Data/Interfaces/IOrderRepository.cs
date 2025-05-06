using E_Commerece.Models;
using E_Commerece.ViewModels;

namespace E_Commerece.Data.Interfaces
{
	public interface IOrderRepository
	{
		void CreateOrder(CheckOutVM model);
		Order GetLatestOrder();
		Order GetOrderById(int orderid);

		List<OrderItem> GetOrderItemsByOrderId(int orderid);

		int GetOrderCount();
		List<SalePerMonthVM> SalesPerMonth();
		List<Order> AllOrderes();
		OrderDetailsVM GetCartOrderDetails();

    }
}
