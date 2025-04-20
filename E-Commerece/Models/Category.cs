using System.ComponentModel.DataAnnotations;

namespace E_Commerece.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "يجب أن يكون الاسم على الأقل حرفين.")]
        [MaxLength(100, ErrorMessage = "يجب ألا يتجاوز الاسم 100 حرف.")]
        public string Name { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "يجب أن يكون الوصف على الأقل 10 أحرف.")]
        [MaxLength(1000, ErrorMessage = "يجب ألا يتجاوز الوصف 1000 حرف.")]
        public string Description { get; set; }

        public bool IsActive { get; set; } = true;

        public List<Product>? Products { get; set; } = new();
        public List<Variation>? Variations { get; set; } = new();
    }

}
