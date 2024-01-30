using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
                string imagePath = null;

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
                    FullName = RModel.FullName,
                    CreditCard = RModel.CreditCard,
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
