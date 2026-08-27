using MeetHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace MeetHub.Infrastructure.Data.Configuration
{
    public class FirmConfiguration : IEntityTypeConfiguration<Firm>
    {
        public void Configure(EntityTypeBuilder<Firm> builder)
        {
            builder.ToTable("Firms");

            builder.HasKey(f => f.Id);

            builder.Property(f => f.CorporateReason).IsRequired().HasMaxLength(100);
            builder.Property(f => f.FantasyName).IsRequired().HasMaxLength(100);
            builder.Property(f => f.Email).HasMaxLength(200);
            builder.Property(f => f.PhoneNumber).HasMaxLength(15);
            builder.Property(f => f.Cnpj).HasMaxLength(14);
            builder.Property(f => f.LogoUrl).HasMaxLength(500);
            builder.Property(f => f.IsActive).IsRequired();
            builder.Property(f => f.ZipCode).HasMaxLength(15);
            builder.Property(f => f.Street).HasMaxLength(255);
            builder.Property(f => f.AddressNumber).HasMaxLength(20);
            builder.Property(f => f.Neighborhood).HasMaxLength(100);
            builder.Property(f => f.City).HasMaxLength(100);
            builder.Property(f => f.State).HasMaxLength(50);
            builder.Property(f => f.Country).IsRequired().HasMaxLength(60);
            builder.Property(f => f.CreatedAt).IsRequired();

            builder.HasIndex(f => f.Cnpj).IsUnique();
        }
    }
}
