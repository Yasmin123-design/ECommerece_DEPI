using E_Commerece.Models;

namespace E_Commerece.ViewModels
{
	public class QuickViewModel
	{
		public Product Product { get; set; }
		public List<Variation> Variations { get; set; } // 🔥 جميع الـ Variations الخاصة بـ Category
		public List<VariationOption> SelectedVariationOptions { get; set; }
        public ProductItem? SelectedProductItem { get; set; }
    }
}
