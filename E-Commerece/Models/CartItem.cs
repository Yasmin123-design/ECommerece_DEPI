using System.ComponentModel.DataAnnotations;

namespace E_Commerece.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        [Required]
        public int ProductItemId { get; set; }
        public ProductItem ProductItem { get; set; }

        [Required]
        public int CartId { get; set; }
        public Cart Cart { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "الكميه يجب ان تكون رقم موجبا")]
        public int Quantity { get; set; }
    }

}
