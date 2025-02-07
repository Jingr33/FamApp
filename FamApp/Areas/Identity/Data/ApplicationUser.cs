using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using FamApp.Models;
using Microsoft.AspNetCore.Identity;

namespace FamApp.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string Nick {  get; set; }

    public ICollection<Ticket> CreatedTickets { get; set; }
    public ICollection<UserTicket> UserTickets { get; set; } = new List<UserTicket>();
}

