using FamApp.Models;

namespace FamApp.Interfaces
{
    public interface ITicketService
    {
        IQueryable<Ticket> FilterAndSort(string userId, string filter);
        Task<bool> AddTicketAsync(Ticket obj, List<string> selectedUserIds);
        Task<bool> SolveTicketAsync(int id);
        Task<bool> DeleteTicketAsync(int id);
    }
}
