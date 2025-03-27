using FamApp.Data;
using FamApp.Interfaces;
using FamApp.Models;
using FamApp.ViewModels;
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
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _db;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger,
                              UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager,
                              ApplicationDbContext db, IUserService userService)
        {
            _logger = logger;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._db = db;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(this.User);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePersonalData(UpdatePersonalDataViewModel model)
        {   
            if(!ModelState.IsValid)
                return View("Index", model);

            var user = await this._userManager.FindByIdAsync(model.Id);
            if (user == null)
                return NotFound();

            //if (user == null)
            //    return NotFound();

            //user.Nick = model.Nick;
            //user.Color = model.Color;
            //var result = await _userManager.UpdateAsync(user);

            var result = await _userService.UpdateUserAsync(model);

            if (result)
            {
                TempData["SuccessMessagePersonal"] = "Údaje byly úspìšnì aktualizovány!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Údaje se nepodaøilo aktualizovat.");
            return View("Index", user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Index", model);

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                TempData["SuccessMessagePassword"] = "Heslo bylo úspìšnì zmìnìno!";
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View("Index", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
