using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace E_Commerece.Models
{
    public class Variation
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "اسم التغيير لا يمكن أن يتجاوز 100 حرف.")]
        public string Name { get; set; } = string.Empty;

        public List<VariationOption> VariationOptions { get; set; } = new();

        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }

}
