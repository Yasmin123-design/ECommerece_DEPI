using System.ComponentModel.DataAnnotations;

namespace E_Commerece.Models
{
    public class VariationOption
    {
        public int Id { get; set; }

        [Required]
        public int VariationId { get; set; }

        [Required]
        public Variation Variation { get; set; } = new();

        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "القيمة يجب أن تكون بين 1 و 100 حرف.")]
        public string Value { get; set; } = string.Empty;

        public List<ProductItem> ProductItems { get; set; } = new();
    }

}
