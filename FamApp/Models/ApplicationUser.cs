using Microsoft.AspNetCore.Identity;

namespace FamApp.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<UserTicket> UserTickets { get; set; }
    }
}
