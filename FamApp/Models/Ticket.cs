﻿using FamApp.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FamApp.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        public bool Public { get; set; }

        public bool Priority { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? DeadLineDate { get; set; }

        [ForeignKey("CreatedByUser")]
        public string CreatedByUserId { get; set; }

        public ApplicationUser CreatedByUser { get; set; }

        public ICollection<UserTicket>? UserTickets { get; set; }

        public bool Solved { get; set; } = false;
    }
}
