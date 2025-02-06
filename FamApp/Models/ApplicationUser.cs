using Microsoft.AspNetCore.Identity;

namespace FamApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Nick { get; set; } = "Unknown";
        public ICollection<Ticket> CreatedTickets { get; set; }
        public ICollection<UserTicket> UserTickets { get; set; }
    }
}
