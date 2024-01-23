using Microsoft.AspNetCore.Identity;

namespace WebApplication3.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public int CreditCard { get; set; }
        public string Gender { get; set; } = string.Empty;
        public int MobileNo { get; set; }
        public string DeliveryAddr { get; set; } = string.Empty;
        public string AboutMe { get; set; } = string.Empty;

		public static implicit operator ApplicationUser(ApplicationUser v)
		{
			throw new NotImplementedException();
		}
	}
}