using System.ComponentModel.DataAnnotations;

namespace E_Commerece.ViewModels
{
	public class CheckOutVM
	{
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
}
