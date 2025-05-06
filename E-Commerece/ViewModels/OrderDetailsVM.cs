using E_Commerece.Models;

namespace E_Commerece.ViewModels
{
    public class OrderDetailsVM
    {
        public List<OrderItemVM> Items { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
