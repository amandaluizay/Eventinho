using Eventinho.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventinho.Database.EntitiesConfiguration
{
    internal class GuestEntityConfiguration : IEntityTypeConfiguration<Guest>
    {
        public void Configure(EntityTypeBuilder<Guest> builder)
        {
            builder.ToTable("guests");

            builder.HasKey(x => x.Id);
            builder.Property(i => i.CreatedAt).IsRequired();
            builder.Property(i => i.CreatedBy).IsRequired();
            builder.Property(i => i.UpdatedBy);
            builder.Property(i => i.UpdatedAt);
            builder.Property(i => i.EventId).IsRequired();
            builder.Property(i => i.UserId).IsRequired();
            builder.Property(i => i.State).IsRequired();
            
            builder.HasOne(i => i.Event)
                   .WithMany(i => i.Guests)
                   .HasForeignKey(i => i.EventId);

            builder.HasOne(i => i.User)
                   .WithMany(i => i.EventGuests)
                   .HasForeignKey(i => i.UserId);
        }
    }
}
