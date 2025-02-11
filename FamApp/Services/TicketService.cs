using FamApp.Interfaces;
using FamApp.Models;
using Microsoft.Data.SqlClient;

namespace FamApp.Services
{
    public class TicketService
    {
        private readonly ITicketRepository _ticketRepo;

        public TicketService(ITicketRepository ticketRepo)
        {
            this._ticketRepo = ticketRepo;
        }

        public IQueryable<Ticket> FilterAndSort(string userId, string filter)
        {
            var tickets = this._ticketRepo.GetTickets();

            if (!string.IsNullOrEmpty(userId))
            {
                switch (filter)
                {
                    case "createdByMe":
                        tickets = tickets.Where(t => t.CreatedByUserId == userId);
                        break;
                    case "solvedByMe":
                        tickets = tickets.Where(t => t.UserTickets.Select(ut => ut.User.Id).Contains(userId));
                        break;
                    default:
                        break;
                }

            }

            // sorting
            tickets = tickets.OrderByDescending(t => t.Priority);
            tickets = tickets.OrderBy(t => t.DeadLineDate);

            return tickets;
        }

        public async Task<bool> AddTicketAsync (Ticket obj, List<string> selectedUserIds)
        {
            if (selectedUserIds == null)
                return false;

            obj.UserTickets = selectedUserIds.Select(userId => new UserTicket
            {
                UserId = userId,
            }).ToList();

            await this._ticketRepo.AddTicketAsync(obj);
            return true;
        }

        public async Task<bool> SolveTicketAsync(int id)
        {
            var ticket = await this._ticketRepo.GetTicketByIdAsync(id);
            if (ticket == null)
                return false;

            ticket.Solved = true;
            await this._ticketRepo.UpdateTicketAsync(ticket);
            return true;
        }

        public async Task<bool> DeleteTicketAsync (int id)
        {
            var ticket = await this._ticketRepo.GetTicketByIdAsync(id);
            if (ticket == null)
            {
                return false;
            }

            await this._ticketRepo.DeleteTicketAsync(ticket);
            return true;
        }
    }
}
