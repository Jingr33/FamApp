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
            var user = await _userManager.GetUserAsync(this.User);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([Bind("Id", "Nick", "Color")] ApplicationUser obj)
        {   
            if(ModelState.IsValid)
            {
                var user = await this._userManager.FindByIdAsync(obj.Id);

                if (user == null)
                    return NotFound();

                user.Nick = obj.Nick;
                user.Color = obj.Color;
                Console.WriteLine($"##############################################################################x");
                Console.WriteLine($"Color from form: {user.Color}");
                Console.WriteLine($"##############################################################################x");

                await this._userManager.UpdateAsync(user);
                await this._db.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var reloadedObj = await _userManager.FindByIdAsync(obj.Id);
                return View("Index", reloadedObj);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
