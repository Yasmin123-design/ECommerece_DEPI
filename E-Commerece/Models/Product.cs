using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Build.ObjectModelRemoting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerece.Models
{
    public class Product
    {
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string Name { get; set; }
        public string? Image { get; set; }
        public bool IsApproved { get; set; } = false;

        [Required]
        [MinLength(10, ErrorMessage = "يجب أن يكون الوصف على الأقل 10 أحرف.")]
        [MaxLength(1000, ErrorMessage = "يجب ألا يتجاوز الوصف 1000 حرف.")]
        public string Description { get; set; }

        [Required]
        [Range(0.01, 100000, ErrorMessage = "السعر يجب أن يكون بين 0.01 و 100,000.")]
        public decimal Price { get; set; }
        [Required]
        public int CategoryId { get; set; }

        public string? SellerId { get; set; }
        public User? Seller { get; set; }
        public Category? Category { get; set; }
        public List<ProductItem> Items { get; set; } = new List<ProductItem>();

        public List<Review> Reviews { get; set; } = new List<Review>();

        public List<Wishlist> Wishlists { get; set; } = new List<Wishlist>();

        public ProductStatus ApprovalStatus { get; set; } = ProductStatus.Pendding;
    }
    public enum ProductStatus
    {
        Pendding , Accept , Reject
    }
}
