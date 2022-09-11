using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class PictureConfiguration : IEntityTypeConfiguration<Picture>
{
    public void Configure(EntityTypeBuilder<Picture> builder)
    {        
        builder.Property(p => p.MimeType)
            .HasMaxLength(50);
        
        builder.Property(p => p.SeoFileName)
            .HasMaxLength(100);
        
        builder.Property(p => p.AltAttribute)
            .HasMaxLength(250);
        
        builder.Property(p => p.TitleAttribute)
            .HasMaxLength(250);
    }
}