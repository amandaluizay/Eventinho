using Eventinho.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Eventinho.Database.EntitiesConfiguration
{
    internal class UserNotificationEntityConfiguration : IEntityTypeConfiguration<UserNotification>
    {
        public void Configure(EntityTypeBuilder<UserNotification> builder)
        {
            builder.ToTable("user_notifications");

            builder.HasKey(x => x.Id);
            builder.Property(i => i.CreatedAt).IsRequired();
            builder.Property(i => i.CreatedBy).IsRequired();
            builder.Property(i => i.UpdatedBy);
            builder.Property(i => i.UpdatedAt);

            builder.Property(i => i.UserId).IsRequired();
            builder.Property(i => i.Message).IsRequired();
            builder.Property(i => i.IsRead).IsRequired();

            builder.HasOne(i => i.User)
                   .WithMany(i => i.Notifications)
                   .HasForeignKey(i => i.UserId);
        }
    }
}
