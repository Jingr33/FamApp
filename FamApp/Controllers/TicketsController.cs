using FamApp.Areas.Identity.Data;
using FamApp.Data;
using FamApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FamApp.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public TicketsController (ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            this._db = db;
            this._userManager = userManager;
        }

        public IActionResult Index()
        {
            List<Ticket> tickets = _db.Tickets.Include(t => t.CreatedByUser)
                                              .Include(t => t.UserTickets)
                                              .ThenInclude(ut => ut.User).ToList();
            return View(tickets);
        }

        public IActionResult Create()
        {
            ViewData["Users"] = new SelectList(_db.Users, "Id", "Nick");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Ticket obj, List<string> SelectedUserIds)
        {
            obj.CreationDate = DateTime.Now;
            obj.CreatedByUserId = _userManager.GetUserId(this.User);

            if (SelectedUserIds != null)
            {
                obj.UserTickets = SelectedUserIds.Select(userId => new UserTicket
                {
                    UserId = userId,
                }).ToList();
            }

            _db.Tickets.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index", "Tickets");
        }
    }
}
