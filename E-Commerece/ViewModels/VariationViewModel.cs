using E_Commerece.Models;

namespace E_Commerece.ViewModels
{
    public class VariationViewModel
    {
        public int CategoryId { get; set; }
        public List<Variation> Variations { get; set; } = new List<Variation>();
    }

}
