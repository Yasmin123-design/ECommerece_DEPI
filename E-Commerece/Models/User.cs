using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Identity;

namespace E_Commerece.Models
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage ="this field required")]
        [StringLength(50, ErrorMessage = "الاسم الأول لا يمكن أن يتجاوز 50 حرفًا.")]
        public string Fname { get; set; } = string.Empty;

        [Required(ErrorMessage = "this field required")]
        [StringLength(50, ErrorMessage = "الاسم الأخير لا يمكن أن يتجاوز 50 حرفًا.")]
        public string Lname { get; set; } = string.Empty;

        [Required(ErrorMessage = "this field required")]
        [StringLength(200, ErrorMessage = "العنوان لا يمكن أن يتجاوز 200 حرف.")]
        public string Address { get; set; } = string.Empty;

        public List<Review> Reviews { get; set; } = new();
        public List<Product> PostedProducts { get; set; } = new();
        public List<Order> Orders { get; set; } = new();

        public Wishlist? Wishlist { get; set; }

    }
}
