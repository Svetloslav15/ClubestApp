namespace ClubestApp.Controllers
{
    using ClubestApp.Data.Models;
    using ClubestApp.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class RequestNewClubController : Controller
    {
        private readonly RequestNewClubService requestNewClubService;

        public RequestNewClubController(RequestNewClubService requestNewClubService)
        {
            this.requestNewClubService = requestNewClubService;
        }

        public async Task<IActionResult> Details(string id)
        {
            RequestNewClub request = await this.requestNewClubService.GetRequestNewClubById(id);
            return View(request);
        }

        public async Task<IActionResult> Delete(string id)
        {
            await this.requestNewClubService.Delete(id);
            return this.Redirect("/Club/GetAllRequestNewClub");

        }
        public async Task<IActionResult> Approve(string id)
        {
            await this.requestNewClubService.ApproveAndMakeClub(id);
            return this.Redirect("/Club/GetAllRequestNewClub");
        }
    }
}