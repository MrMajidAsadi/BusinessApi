using System.Reflection;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

#nullable disable
public class OVitrinDbContext : DbContext
{
    public OVitrinDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Picture> Pictures { get; set; }
    public DbSet<Business> Businesses { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}