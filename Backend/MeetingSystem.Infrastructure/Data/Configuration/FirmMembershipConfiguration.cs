using MeetHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeetHub.Infrastructure.Data.Configuration
{
    public class FirmMembershipConfiguration : IEntityTypeConfiguration<FirmMembership>
    {
        public void Configure(EntityTypeBuilder<FirmMembership> builder)
        {
            builder.ToTable("FirmMemberships");

            builder.HasKey(r => r.Id);
            
            builder.Property(r => r.UserId).IsRequired();
            builder.Property(r => r.FirmId).IsRequired();
            builder.Property(r => r.Profile).HasConversion<int>().IsRequired();
            builder.Property(r => r.Status).HasConversion<int>().IsRequired();
            builder.Property(r => r.CreatedAt).IsRequired();

            builder.HasIndex(r => new { r.UserId, r.FirmId }).IsUnique();

            builder.HasOne<User>().WithMany().HasForeignKey(r => r.UserId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<Firm>().WithMany().HasForeignKey(r => r.FirmId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
