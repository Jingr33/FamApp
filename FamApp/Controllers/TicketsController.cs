using FamApp.Areas.Identity.Data;
using FamApp.Data;
using FamApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FamApp.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<Areas.Identity.Data.ApplicationUser> _userManager;

        public TicketsController (ApplicationDbContext db, UserManager<Areas.Identity.Data.ApplicationUser> userManager)
        {
            this._db = db;
            this._userManager = userManager;
        }

        public IActionResult Index()
        {
            List<Ticket> tickets = _db.Tickets.Include(t => t.CreatedByUser).ToList();
            return View(tickets);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Ticket obj)
        {
            obj.CreationDate = DateTime.Now;
            obj.CreatedByUserId = _userManager.GetUserId(this.User);
            _db.Tickets.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index", "Tickets");
        }
    }
}
