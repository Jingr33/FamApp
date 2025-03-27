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
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger,
                              UserManager<ApplicationUser> userManager,
                              IUserService userService)
        {
            _logger = logger;
            _userManager = userManager;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userService.GetCurrentUserAsync();
            //var user = await _userManager.GetCurrentUserAsync(this.User);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePersonalData(UpdatePersonalDataViewModel model)
        {   
            if(!ModelState.IsValid)
            {
                TempData["FailureMessagePersonal"] = "Údaje se nepodaøilo zmìnit.";
                return RedirectToAction("Index");
            }

            var result = await _userService.UpdateUserAsync(model);

            if (result)
            {
                TempData["SuccessMessagePersonal"] = "Údaje byly úspìšnì aktualizovány!";
                return RedirectToAction("Index");
            }

            TempData["FailureMessagePersonal"] = "Údaje se nepodaøilo zmìnit.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["FailureMessagePassword"] = "Heslo se nepodaøilo zmìnit.";
                return RedirectToAction("Index");
            }

            var user = await _userService.ChangePasswordAsync(model);

            if (user != null)
            {
                await _userService.RefreshSignInAsync(user);
                TempData["SuccessMessagePassword"] = "Heslo bylo úspìšnì zmìnìno!";
                return RedirectToAction("Index");
            }

            TempData["FailureMessagePassword"] = "Heslo se nepodaøilo zmìnit.";
            return RedirectToAction("Index");
        }
    }
}
