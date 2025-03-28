using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FamApp.Models
{
    public class UserTicket
    {
        [ForeignKey("User")]
        public string UserId { get; set; }
        public Areas.Identity.Data.ApplicationUser User { get; set; }

        [ForeignKey("Ticket")]
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
    }
}
