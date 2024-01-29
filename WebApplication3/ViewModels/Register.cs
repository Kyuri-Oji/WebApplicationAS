using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WebApplication3.ViewModels
{
    public class Register
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and confirmation password does not match")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Text)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.CreditCard)]
        public string CreditCard { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string MobileNo { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Text)]
        public string DeliveryAddr { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Text)]
		public string AboutMe { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Text)]
        public string Gender { get; set; } = string.Empty;

        [Required]
        [BindProperty]
        [DataType(DataType.Upload)]
        public IFormFile? Photo { get; set; }
    }
}
