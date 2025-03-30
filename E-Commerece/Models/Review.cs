using System.ComponentModel.DataAnnotations;

namespace E_Commerece.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; } // لا حاجة لجعلها nullable طالما أن ProductId مطلوب

        [Required]
        public string UserId { get; set; }
        public User User { get; set; } // العلاقة يجب أن تكون مطلوبة

        [Required]
        [Range(1, 5, ErrorMessage = "التقييم يجب أن يكون من 1 إلى 5.")]
        public int Rating { get; set; } // لا داعي لأن يكون nullable

        [Required]
        [StringLength(500, ErrorMessage = "التعليق لا يمكن أن يتجاوز 500 حرف.")]
        public string Comment { get; set; }
    }

}
