using Eventinho.Domain.Entities;
using Eventinho.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Eventinho.Database
{
    public class EventinhoDbcontext : DbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Event> Events { get; set; }
        DbSet<Guest> Guests { get; set; }
        DbSet<UserNotification> UserNotifications { get; set; }
        DbSet<UserInterationRequest> UserInterationRequests { get; set; }

        public EventinhoDbcontext(DbContextOptions<EventinhoDbcontext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<IEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
