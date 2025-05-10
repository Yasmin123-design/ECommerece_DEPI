using System.ComponentModel.DataAnnotations;

namespace E_Commerece.ViewModels
{
    public class EditUserInfoVM
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

}
