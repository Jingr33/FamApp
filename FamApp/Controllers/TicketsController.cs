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

        private TicketsController (ApplicationDbContext db)
        {
            this._db = db;
        }

        public IActionResult Index()
        {
            List<Ticket> tickets = _db.Tickets.ToList();
            return View(tickets);
        }
    }
}
