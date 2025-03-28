using FamApp.Areas.Identity.Data;
using FamApp.Data;
using FamApp.Interfaces;
using FamApp.Models;
using FamApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FamApp.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly IUserService _userService;

        public TicketsController (ITicketService ticketService,
                                  IUserService userService)
        {
            _ticketService = ticketService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? filter)
        {
            var user = await _userService.GetCurrentUserAsync();
            string userId = await _userService.GetUserIdAsync(user);
            List<Ticket> tickets = this._ticketService.FilterAndSort(userId, filter).ToList();
            ViewData["UserId"] = userId;

            ViewBag.Filter = filter;
            return View(tickets);
        }

        public async Task<IActionResult> Create()
        {
            var model = new CreateTicketViewModel
            {
                Users = await _userService.GetUserSelectListAsync()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Ticket obj, List<string> selectedUserIds)
        {
            obj.CreationDate = DateTime.Now;
            var user = await _userService.GetCurrentUserAsync();
            obj.CreatedByUserId = await _userService.GetUserIdAsync(user);
            bool added = await this._ticketService.AddTicketAsync(obj, selectedUserIds);
            
            if (!added)
            {
                return NotFound();
            }
            return RedirectToAction("Index", "Tickets");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                bool deleted = await this._ticketService.DeleteTicketAsync(id);
                if (!deleted)
                    return NotFound();
            }
            return RedirectToAction("Index", "Tickets");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Solve(int id)
        {
            if (ModelState.IsValid)
            {
                bool updated =  await this._ticketService.SolveTicketAsync(id);
                if (!updated)
                {
                    return NotFound();
                }
            }
            return RedirectToAction("Index", "Tickets");
        }
    }
}
