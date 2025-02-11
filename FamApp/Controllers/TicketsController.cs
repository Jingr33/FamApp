using FamApp.Areas.Identity.Data;
using FamApp.Data;
using FamApp.Models;
using FamApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace FamApp.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly TicketService _ticketService;
        private readonly UserManager<ApplicationUser> _userManager;

        public TicketsController (ApplicationDbContext db, TicketService ticketService, UserManager<ApplicationUser> userManager)
        {
            this._db = db;
            this._ticketService = ticketService;
            this._userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index(string? filter)
        {
            string userId = _userManager.GetUserId(this.User) ?? throw new NullReferenceException("User not found");
            List<Ticket> tickets = this._ticketService.FilterAndSort(userId, filter).ToList();
            ViewData["UserId"] = userId;

            ViewBag.Filter = filter;
            return View(tickets);
        }

        public IActionResult Create()
        {
            ViewData["Users"] = new SelectList(_db.Users, "Id", "Nick");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Ticket obj, List<string> selectedUserIds)
        {
            obj.CreationDate = DateTime.Now;
            obj.CreatedByUserId = _userManager.GetUserId(this.User);
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
