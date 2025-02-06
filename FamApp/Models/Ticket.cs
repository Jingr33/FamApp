using System.ComponentModel.DataAnnotations;

namespace FamApp.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? User { get; set; }

        public ICollection<UserTicket>? UserTickets { get; set; }
    }
}
