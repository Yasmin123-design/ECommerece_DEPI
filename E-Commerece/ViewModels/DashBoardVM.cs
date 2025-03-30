

using E_Commerece.Models;

namespace E_Commerece.ViewModels
{
    public class DashBoardVM
    {
        public List<Order> PurchasedProducts { get; set; }
        public List<Product> SoldchasedProducts { get; set; }
        public List<Order> RequestProducts { get; set; }
    }
}
