using System.ComponentModel.DataAnnotations;

namespace E_Commerece.Models
{
    public class Wishlist
    {
        public int Id { get; set; }

        public List<Product> Products { get; set; } = new();

        [Required]
        public string UserId { get; set; } = string.Empty;

        public User User { get; set; }
    }

}
