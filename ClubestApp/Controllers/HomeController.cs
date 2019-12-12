namespace ClubestApp.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using ClubestApp.Models;
    using ClubestApp.Data;
    using ClubestApp.Data.Models;
    using Microsoft.AspNetCore.Identity;

    public class HomeController : Controller
    {
        private ApplicationDbContext dbContext;
        private UserManager<User> userManager;

        public HomeController(ApplicationDbContext dbContext, UserManager<User> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            var user = this.userManager.GetUserAsync(HttpContext.User);
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
