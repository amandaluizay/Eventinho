using Eventinho.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventinho.Database.EntitiesConfiguration
{
    internal class EventEntityConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("events");

            builder.HasKey(x => x.Id);
            builder.Property(i => i.CreatedAt).IsRequired();
            builder.Property(i => i.CreatedBy).IsRequired();
            builder.Property(i => i.UpdatedBy);
            builder.Property(i => i.UpdatedAt);

            builder.Property(i => i.Description).IsRequired();
            builder.Property(i => i.Local).IsRequired();
            builder.Property(i => i.StartDate).IsRequired();
            builder.Property(i => i.EndDate);
            builder.Property(i => i.Limit);
            builder.Property(i => i.OwnerId).IsRequired();

            builder.HasOne(i => i.Owner)
                   .WithMany(i=> i.Events)
                   .HasForeignKey(i => i.OwnerId);

            builder.HasMany(e => e.Colaborators)
                   .WithMany(u => u.Events)
                   .UsingEntity(j => j.ToTable("EventColaborators"));
        }
    }
}
