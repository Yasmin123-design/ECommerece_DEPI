using System.ComponentModel.DataAnnotations;

namespace E_Commerece.Models
{
    public class ProductItem
    {
        public int Id { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "الكميه يجب ان تكون رقم موجبا")]
        public int Quantity { get; set; }

        [Required]
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public List<VariationOption>? VariationOptions { get; set; } = new List<VariationOption>();
        public List<CartItem>? CartItems { get; set; } = new List<CartItem>();
        public List<OrderItem>? OrderItems { get; set; } = new List<OrderItem>();
    }
}
