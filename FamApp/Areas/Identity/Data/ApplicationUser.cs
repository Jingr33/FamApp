using System.ComponentModel.DataAnnotations.Schema;
using FamApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace FamApp.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string Nick {  get; set; }

    public string? Color { get; set; }

    public ICollection<Ticket>? CreatedTickets { get; set; }

    public ICollection<UserTicket> UserTickets { get; set; } = new List<UserTicket>();

    public ICollection<ChatUser> ChatUsers { get; set; }

    public ICollection<Message> Messages { get; set; }
}

