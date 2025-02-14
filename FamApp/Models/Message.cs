using FamApp.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace FamApp.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; } = DateTime.Now;

        public int ChatId { get; set; }
        [ForeignKey("ChatId")]
        public Chat Chat { get; set; }

        public string SenderId { get; set; }
        [ForeignKey("SenderId")]
        public ApplicationUser Sender { get; set; }
    }
}
