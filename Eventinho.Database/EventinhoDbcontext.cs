using Eventinho.Domain.Entities;
using Eventinho.Domain.Interfaces;
using Eventinho.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Eventinho.Database
{
    public class EventinhoDbcontext : DbContext
    {
        private readonly IUserContextService _userService;
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventModerator> EventColaborators { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<UserInterationRequest> UserInterationRequests { get; set; }

        public EventinhoDbcontext(DbContextOptions<EventinhoDbcontext> options , IUserContextService userService) : base(options)
        {
            _userService = userService;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                        .HasMany(a => a.SenderRequests)
                        .WithOne(a => a.UserSender)
                        .HasForeignKey(a => a.UserSenderId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                        .HasMany(a => a.ReceiverRequests)
                        .WithOne(a => a.UserReceiver)
                        .HasForeignKey(a => a.UserReceiverId)
                        .OnDelete(DeleteBehavior.Restrict);

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
                        entry.Entity.CreatedBy = _userService.GetCurrentUserName();
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        entry.Entity.UpdatedBy = _userService.GetCurrentUserName();
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
