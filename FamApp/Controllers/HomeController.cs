using FamApp.Areas.Identity.Data;
using FamApp.Data;
using FamApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ApplicationUser = FamApp.Areas.Identity.Data.ApplicationUser;

namespace FamApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _logger = logger;
            this._userManager = userManager;
            this._db = db;
        }

        public async Task<IActionResult> Index()
        {
            //ViewData["UserID"] = _userManager.GetUserId(this.User);
            var user = await _userManager.GetUserAsync(this.User);
            return View(user);
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
