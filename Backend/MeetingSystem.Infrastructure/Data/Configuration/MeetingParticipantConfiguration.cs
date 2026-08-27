using MeetHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeetHub.Infrastructure.Data.Configuration
{
    public class MeetingParticipantConfiguration : IEntityTypeConfiguration<MeetingParticipant>
    {
        public void Configure(EntityTypeBuilder<MeetingParticipant> builder)
        {
            builder.ToTable("MeetingParticipants");

            builder.HasKey(mp => mp.Id);
            builder.HasOne(mp => mp.Meeting).WithMany(m => m.MeetingParticipants).HasForeignKey(mp => mp.MeetingId).OnDelete(DeleteBehavior.Cascade);

            builder.Property(mp => mp.ParticipantName).IsRequired().HasMaxLength(100);
            builder.Property(mp => mp.PermissionProfile).IsRequired();
            builder.Property(mp => mp.IsPresent).IsRequired();
        }
    }
}
