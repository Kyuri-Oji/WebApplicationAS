using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication3.Model;
using WebApplication3.ViewModels;

namespace WebApplication3.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Login LModel { get; set; }

        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly AuthDbContext _authDbContext;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<User> signInManager,
                            UserManager<User> userManager,
                            AuthDbContext authDbContext,
                            IConfiguration configuration,
                            IHttpContextAccessor contextAccessor,
                            ILogger<LoginModel> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _authDbContext = authDbContext;
            _configuration = configuration;
            _contextAccessor = contextAccessor;
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var now = DateTime.UtcNow;
            if (ModelState.IsValid)
            {
                var identityResult = await _signInManager.PasswordSignInAsync(LModel.Email, LModel.Password, true, true);
                if (identityResult.Succeeded)
                {
                    Audit myLog = new()
                    {
                        UserEmail = LModel.Email,
                        UserActivity = "Login"

                    };

                    _authDbContext.Auditlog.Add(myLog);
                    _authDbContext.SaveChanges();


                    return RedirectToPage("Index");
                }
                if (identityResult.IsLockedOut)
                {
                    Audit newLockedOutLog = new()
                    {
                        UserEmail = LModel.Email,
                        UserActivity = "User locked out",
                        CreatedAt = now
                    };

                    _authDbContext.Auditlog.Add(newLockedOutLog);
                    _authDbContext.SaveChanges();
                    return RedirectToPage("Index");
                }
                ModelState.AddModelError("", "Username or Password incorrect");
            }
            return Page();
        }
    }
}

