using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using WebApplication3.Model;
using WebApplication3.ViewModels;

namespace WebApplication3.Pages
{
    public class RegisterModel : PageModel
    {

        private UserManager<User> userManager { get; }
        private SignInManager<User> signInManager { get; }
        private readonly AuthDbContext authDbContext;
        private readonly ILogger<RegisterModel> logger;

        [BindProperty]
        public Register RModel { get; set; }

        public RegisterModel(UserManager<User> userManager,
        SignInManager<User> signInManager,
        AuthDbContext dbContext,
        ILogger<RegisterModel> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.authDbContext = dbContext;
        }

        public void OnGet()
        {
            // onget onget uooohhhh
        }

        public Register GetRModel()
        {
            return RModel;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                string? imagePath = null;
                var dataProtectionProvider = DataProtectionProvider.Create("EncryptData");
                var protectorOfTheGalaxy = dataProtectionProvider.CreateProtector("SecretKey");

                string validateNameRegex = @"^[a-zA-Z ]+$";
                if (!Regex.IsMatch(RModel.FullName, validateNameRegex))
                {
                    ModelState.AddModelError("", "Full Name cannot contain special characters.");
                }

                string validateCreditCardRegex = @"^[0-9]+$";
                if (!Regex.IsMatch(RModel.CreditCard, validateCreditCardRegex))
                {
                    ModelState.AddModelError("", "Credit card number can only contain numbers.");
                }

                string validateDeliveryAddrRegex = @"^[a-zA-Z0-9]+";
                if (!Regex.IsMatch(RModel.DeliveryAddr, validateDeliveryAddrRegex))
                {
                    ModelState.AddModelError("", "Delivery Address can only contain alphanumerical characters.");
                }

                var rng = RandomNumberGenerator.GetInt32(1, 999999999);
                SHA256 hash = SHA256.Create();

                string saltyPasswords = RModel.Password + rng;
                _ = hash.ComputeHash(Encoding.UTF8.GetBytes(saltyPasswords));
                byte[] saltedHashbrowns = hash.ComputeHash(Encoding.UTF8.GetBytes(saltyPasswords.Substring(1)));

                string freshlyCookedHashbrowns = Convert.ToBase64String(saltedHashbrowns);

                if (RModel.Photo != null)
                {
				    imagePath = Path.Combine("wwwroot/images", RModel.Photo.FileName);
				    using (var stream = new FileStream(imagePath, FileMode.Create))
				    {
					    await RModel.Photo.CopyToAsync(stream);
                    }
                }
                else
                {
				    ModelState.AddModelError("", "Error uploading file.");
                }

                var user = new User()
                {
                    UserName = RModel.Email,
                    Email = RModel.Email,
                    Password = freshlyCookedHashbrowns,
                    FullName = RModel.FullName,
                    CreditCard = protectorOfTheGalaxy.Protect(RModel.CreditCard),
                    MobileNo = RModel.MobileNo,
                    DeliveryAddr = RModel.DeliveryAddr,
                    AboutMe = RModel.AboutMe,
                    Gender = RModel.Gender,
                    Photo = imagePath,
                };
                var result = await userManager.CreateAsync(user, RModel.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToPage("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return Page();
        }
    }
}
