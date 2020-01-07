namespace ClubestApp.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using ClubestApp.Models;
    using ClubestApp.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public class HomeController : Controller
    {
        private readonly UserManager<User> userManager;

        public HomeController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(HttpContext.User);
            
            if (user == null)
            {
                return this.View("IndexNotLogged");
            }
            if (user.Interests == null)
            {
                return this.View("AddInterests");
            }

            //TODO load latest data from clubs
            return this.View();
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