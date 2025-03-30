using System.ComponentModel.DataAnnotations;

namespace E_Commerece.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public List<CartItem> CartItems { get; set; } = new List<CartItem>();

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "اجمال السعر يجب ان يكون رقم موجبا")]
        public decimal TotalPrice { get; set; } = 0;

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }
    }

}
