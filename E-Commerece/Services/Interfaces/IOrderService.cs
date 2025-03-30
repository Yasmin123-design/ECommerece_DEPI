using E_Commerece.Models;
using E_Commerece.ViewModels;

namespace E_Commerece.Services.Interfaces
{
	public interface IOrderService
	{
		void CreateOrder(CheckOutVM model);
        Order GetLatestOrder();
        Order GetOrderById(int orderid);
        List<OrderItem> GetOrderItemsByOrderId(int orderid);
        void SaveChange();
	}
}
