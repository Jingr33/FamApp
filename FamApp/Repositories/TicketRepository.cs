using FamApp.Data;
using FamApp.Interfaces;
using FamApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FamApp.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ApplicationDbContext _db;

        public TicketRepository (ApplicationDbContext db)
        {
            this._db = db;
        }

        public IQueryable<Ticket> GetTickets()
        {
            return _db.Tickets.Include(t => t.CreatedByUser)
                              .Include(t => t.UserTickets)
                              .ThenInclude(ut => ut.User);
        }

        public async Task<Ticket?> GetTicketByIdAsync (int id)
        {
            return await this._db.Tickets.FindAsync(id);
        }

        public async Task AddTicketAsync (Ticket ticket)
        {
            this._db.Tickets.Add(ticket);
            await this._db.SaveChangesAsync();
        }

        public async Task UpdateTicketAsync (Ticket ticket)
        {
            this._db.Update(ticket);
            await this._db.SaveChangesAsync();
        }

        public async Task DeleteTicketAsync (Ticket ticket)
        {
            this._db.Tickets.Remove(ticket);
            await _db.SaveChangesAsync();
        }
    }
}
