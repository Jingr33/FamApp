namespace FamApp.Models
{
    public class UserTicket
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
    }
}
