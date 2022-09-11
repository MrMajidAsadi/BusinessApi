using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Config;

public class BusinessConfiguration : IEntityTypeConfiguration<Business>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Business> builder)
    {
        builder.Property(b => b.Name)
            .HasMaxLength(150);
        
        builder.Property(b => b.Description)
            .HasMaxLength(500);
    }
}