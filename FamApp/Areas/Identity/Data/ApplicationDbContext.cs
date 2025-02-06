using FamApp.Areas.Identity.Data;
using FamApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Reflection.Emit;

namespace FamApp.Data;

public class ApplicationDbContext : IdentityDbContext<Areas.Identity.Data.ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<UserTicket> UserTickets { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserTicket>()
            .HasKey(ut => new { ut.UserId, ut.TicketId });

        builder.Entity<UserTicket>()
            .HasOne(ut => ut.User)
            .WithMany(u => u.UserTickets)
            .HasForeignKey(ut => ut.UserId);

        builder.Entity<UserTicket>()
           .HasOne(ut => ut.Ticket)
           .WithMany(t => t.UserTickets)
           .HasForeignKey(ut => ut.TicketId);

        builder.Entity<Ticket>()
            .HasOne(t => t.CreatedByUser)
            .WithMany(u => u.CreatedTickets)
            .HasForeignKey(t => t.CreatedByUserId);
    }
}
