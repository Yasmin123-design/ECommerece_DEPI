using E_Commerece.Models;

namespace E_Commerece.ViewModels
{
    public class FilterRequestVM
    {
        public List<int> CategoryIds { get; set; }
        public List<Product> Products { get; set; }
    }

}
