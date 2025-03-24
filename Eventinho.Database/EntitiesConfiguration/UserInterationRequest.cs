using Eventinho.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventinho.Database.EntitiesConfiguration
{
    internal class UserInterationRequestEntityConfiguration : IEntityTypeConfiguration<UserInterationRequest>
    {
        public void Configure(EntityTypeBuilder<UserInterationRequest> builder)
        {
            builder.ToTable("user_interation_requests");

            builder.HasKey(x => x.Id);

            builder.Property(i => i.UserSenderId).IsRequired();
            builder.Property(i => i.UserReceiverId).IsRequired();
            builder.Property(i => i.Type).IsRequired();
            builder.Property(i => i.State).IsRequired();

            builder.Property(i => i.CreatedAt).IsRequired();
            builder.Property(i => i.CreatedBy).IsRequired();
            builder.Property(i => i.UpdatedBy);
            builder.Property(i => i.UpdatedAt);
        }

    }
}
