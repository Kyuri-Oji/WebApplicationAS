using System.ComponentModel.DataAnnotations;

namespace WebApplication3.ViewModels
{
    public class Register
    {
        [Required(ErrorMessage = "Email Address field must not be empty")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password field must not be empty")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm Password field must not be empty")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and confirmation password does not match")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Full Name field must not be empty")]
        [DataType(DataType.Text)]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Credit Card field must not be empty")]
        [DataType(DataType.Text)]
        public string CreditCard { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number field must not be empty")]
        [DataType(DataType.PhoneNumber)]
        public string MobileNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Delivery address field must not be empty")]
        [DataType(DataType.Text)]
        public string DeliveryAddr { get; set; } = string.Empty;

        [Required(ErrorMessage = "About me field must not be empty")]
        [DataType(DataType.Text)]
        public string AboutMe { get; set; } = string.Empty;

        [Required(ErrorMessage = "How did you make this empty?")]
        [DataType(DataType.Text)]
        public string Gender { get; set; } = string.Empty;

        [Required(ErrorMessage = "Photo must not be empty")]
        [JpgValidator("jpg", ErrorMessage = "Only .jpg files are allowed.")]
        public IFormFile? Photo { get; set; }
    }
}
