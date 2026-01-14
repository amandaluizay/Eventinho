using Eventinho.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventinho.Database.EntitiesConfiguration
{
    internal class EventColaboratorEntityConfiguration : IEntityTypeConfiguration<EventModerator>
    {
        public void Configure(EntityTypeBuilder<EventModerator> builder)
        { 
            builder.ToTable("event_colaborators");
            builder.HasKey(x => x.Id);
            builder.Property(i => i.CreatedAt).IsRequired();
            builder.Property(i => i.CreatedBy).IsRequired();
            builder.Property(i => i.UpdatedBy);
            builder.Property(i => i.UpdatedAt);

            builder.Property(i => i.EventId);
            builder.Property(i => i.UserId);

            builder.HasOne(i => i.Event)
                   .WithMany(i => i.Moderators)
                   .HasForeignKey(i => i.EventId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(i => i.User)
                   .WithMany(i => i.EventColaborators)
                   .HasForeignKey(i => i.UserId)
                    .OnDelete(DeleteBehavior.NoAction);
        }

    }
}
