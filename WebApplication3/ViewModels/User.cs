using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.ViewModels
{
    public class User: IdentityUser
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string CreditCard { get; set; } = string.Empty;
        public string MobileNo { get; set; } = string.Empty;
        public string DeliveryAddr { get; set; } = string.Empty;
        public string AboutMe { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
    }
}
