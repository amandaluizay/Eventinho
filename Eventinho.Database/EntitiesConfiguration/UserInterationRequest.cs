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
            builder.Property(i => i.CreatedAt).IsRequired();
            builder.Property(i => i.CreatedBy).IsRequired();
            builder.Property(i => i.UpdatedBy);
            builder.Property(i => i.UpdatedAt);

            builder.Property(i => i.User_SenderId).IsRequired();
            builder.Property(i => i.User_ReceiverId).IsRequired();
            builder.Property(i => i.Type).IsRequired();

            builder.HasOne(i => i.User_Sender)
                   .WithMany(i => i.SenderRequests)
                   .HasForeignKey(i => i.User_SenderId);

            builder.HasOne(i => i.User_Receiver)
                   .WithMany(i => i.ReceiverRequests)
                   .HasForeignKey(i => i.User_ReceiverId);
        }
    }
}
