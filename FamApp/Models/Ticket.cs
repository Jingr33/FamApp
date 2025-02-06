﻿using System.ComponentModel.DataAnnotations;

namespace FamApp.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool? Public { get; set; }
        public bool? Priority { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? DeadLineDate { get; set; }
        public int CreateByUserId { get; set; }

        public ICollection<UserTicket>? UserTickets { get; set; }
    }
}
