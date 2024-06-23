using System.Data.Common;
using Evently.Modules.Ticketing.Application.Abstractions.Data;
using Evently.Modules.Ticketing.Domain.Customers;
using Evently.Modules.Ticketing.Domain.Events;
using Evently.Modules.Ticketing.Domain.Orders;
using Evently.Modules.Ticketing.Domain.Payments;
using Evently.Modules.Ticketing.Domain.Tickets;
using Evently.Modules.Ticketing.Infrastructure.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Evently.Modules.Ticketing.Infrastructure.Database;

public sealed class TicketingDbContext(DbContextOptions<TicketingDbContext> options)
    : DbContext(options), IUnitOfWork
{
    internal DbSet<Customer> Customers { get; set; }

    internal DbSet<Event> Events { get; set; }

    internal DbSet<TicketType> TicketTypes { get; set; }

    internal DbSet<Order> Orders { get; set; }

    internal DbSet<OrderItem> OrderItems { get; set; }

    internal DbSet<Ticket> Tickets { get; set; }

    internal DbSet<Payment> Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Ticketing);

        // cardinality
        modelBuilder.Entity<TicketType>()
            .HasOne<Event>()
            .WithMany().HasForeignKey(x => x.EventId);
        
        // Order Item
        modelBuilder.Entity<OrderItem>()
            .HasOne<Order>()
            .WithMany().HasForeignKey(x => x.OrderId);
            
        modelBuilder.Entity<OrderItem>()
            .HasOne<TicketType>()
            .WithMany().HasForeignKey(x => x.TicketTypeId);

        // Order   
        modelBuilder.Entity<Order>()
            .HasOne<Customer>()
            .WithMany().HasForeignKey(x => x.CustomerId);
        
        // Payment
        modelBuilder.Entity<Payment>()
        .HasOne<Order>()
        .WithMany().HasForeignKey(x => x.OrderId);

        // Ticket
        modelBuilder.Entity<Ticket>()
        .HasOne<Order>()
        .WithMany().HasForeignKey(x => x.OrderId);

        modelBuilder.Entity<Ticket>()
        .HasOne<Event>()
        .WithMany().HasForeignKey(x => x.EventId);

        modelBuilder.Entity<Ticket>()
        .HasOne<Customer>()
        .WithMany().HasForeignKey(x => x.CustomerId);

        modelBuilder.Entity<Ticket>()
        .HasOne<TicketType>()
        .WithMany().HasForeignKey(x => x.TicketTypeId);
    }

    public async Task<DbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (Database.CurrentTransaction is not null)
        {
            await Database.CurrentTransaction.DisposeAsync();
        }

        return (await Database.BeginTransactionAsync(cancellationToken)).GetDbTransaction();
    }
}
