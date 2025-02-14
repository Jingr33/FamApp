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
    public DbSet<Chat> Chat { get; set; }
    public DbSet<Message> Message { get; set; }
    public DbSet<ChatUser> ChatUser { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Tickets
        builder.Entity<UserTicket>()
            .HasKey(ut => new { ut.UserId, ut.TicketId });

        builder.Entity<UserTicket>()
            .HasOne(ut => ut.User)
            .WithMany(u => u.UserTickets)
            .HasForeignKey(ut => ut.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<UserTicket>()
           .HasOne(ut => ut.Ticket)
           .WithMany(t => t.UserTickets)
           .HasForeignKey(ut => ut.TicketId)
           .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Ticket>()
            .HasOne(t => t.CreatedByUser)
            .WithMany(u => u.CreatedTickets)
            .HasForeignKey(t => t.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Chats
        builder.Entity<ChatUser>()
            .HasKey(cu => new { cu.UserId, cu.ChatId });

        builder.Entity<ChatUser>()
            .HasOne(cu => cu.Chat)
            .WithMany(c => c.UserChats)
            .HasForeignKey(cu => cu.ChatId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<ChatUser>()
            .HasOne(cu => cu.User)
            .WithMany(u => u.ChatUsers)
            .HasForeignKey(cu => cu.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Message>()
            .HasOne(m => m.Chat)
            .WithMany(c => c.Messages)
            .HasForeignKey(m => m.ChatId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Message>()
            .HasOne(m => m.Sender)
            .WithMany(u => u.Messages)
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
