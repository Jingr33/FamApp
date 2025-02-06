using FamApp.Data;
using FamApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FamApp.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TicketsController (ApplicationDbContext db)
        {
            this._db = db;
        }

        public IActionResult Index()
        {
            List<Ticket> tickets = _db.Tickets.ToList();
            return View(tickets);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Ticket obj)
        {
            _db.Tickets.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index", "Tickets");
        }
    }
}
