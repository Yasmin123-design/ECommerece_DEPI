using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerece.Models
{
    public class Order
    {
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "إجمالي السعر يجب أن يكون رقمًا موجبًا.")]
        public decimal TotalPrice { get; set; }

        public List<OrderItem> Items { get; set; } = new();

        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime ArrivalDate { get; set; } = DateTime.Now.AddDays(14);

        public User? User { get; set; }

        [Required]
        public string UserId { get; set; }

        public Status Status { get; set; } = Status.Pendding;

        // بيانات المستخدم 
        [Required(ErrorMessage = "الاسم الأول مطلوب.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "يجب أن يكون الاسم الأول بين 2 و 50 حرفًا.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "اسم العائلة مطلوب.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "يجب أن يكون اسم العائلة بين 2 و 50 حرفًا.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "رقم الهاتف مطلوب.")]
        [Phone(ErrorMessage = "رقم الهاتف غير صالح.")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "يجب أن يكون رقم الهاتف بين 10 و 15 رقمًا.")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "البريد الإلكتروني مطلوب.")]
        [EmailAddress(ErrorMessage = "يجب إدخال بريد إلكتروني صالح.")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "المدينة مطلوبة.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "يجب أن يكون اسم المدينة بين 2 و 100 حرف.")]
        public string City { get; set; }

        [Required(ErrorMessage = "الشارع مطلوب.")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "يجب أن يكون اسم الشارع بين 2 و 150 حرف.")]
        public string Street { get; set; }
    }

    public enum Status
    {
        Pendding, Shipped, Delivered, Cancelled
    }

}
