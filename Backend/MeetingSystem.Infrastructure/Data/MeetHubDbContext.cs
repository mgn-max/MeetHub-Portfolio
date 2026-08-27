using MeetHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetHub.Infrastructure.Data
{
    public class MeetHubDbContext : DbContext
    {
        public MeetHubDbContext(DbContextOptions<MeetHubDbContext> options) : base(options)
        {

        }

        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<MeetingParticipant> MeetingParticipants { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Firm> Firms { get; set; }
        public DbSet<FirmMembership> FirmMemberships { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeetHubDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
