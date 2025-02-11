using System.Linq;
using FamApp.Models;


namespace FamApp.Interfaces
{
    public interface ITicketRepository
    {
        IQueryable<Ticket> GetTickets();
        Task<Ticket?> GetTicketByIdAsync(int id);
        Task AddTicketAsync(Ticket ticket);
        Task UpdateTicketAsync(Ticket ticket);
        Task DeleteTicketAsync(Ticket ticket);
    }
}
