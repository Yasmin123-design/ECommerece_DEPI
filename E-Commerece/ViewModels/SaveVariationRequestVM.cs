namespace E_Commerece.ViewModels
{
    public class SaveVariationRequestVM
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public List<SelectedOptionVM> SelectedOptions { get; set; }
    }
}
