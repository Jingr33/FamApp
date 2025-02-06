using Microsoft.AspNetCore.Identity;

namespace FamApp.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<Ticket> CraetedTicktes { get; set; }
        public ICollection<UserTicket> UserTickets { get; set; }
    }
}
