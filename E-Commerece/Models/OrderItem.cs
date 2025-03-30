using System.ComponentModel.DataAnnotations;

namespace E_Commerece.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        [Required]
        public int ProductItemId { get; set; }
        public ProductItem ProductItem { get; set; } = null!; // تأكيد عدم كونه `null`

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "الكمية يجب أن تكون رقمًا موجبًا أكبر من الصفر.")]
        public int Quantity { get; set; } = 1; // تعيين قيمة افتراضية للكمية

        [Required]
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!; // تأكيد عدم كونه `null`
    }

}
