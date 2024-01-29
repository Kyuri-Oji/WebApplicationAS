using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.ViewModels;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public HomeController(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager) {
            IHttpContextAccessor _contextAccessor = httpContextAccessor;
            UserManager<User> _userManager = userManager;
        }
    }
}
