using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity;

public class OVitrinIdentityDbContext : IdentityDbContext
{
    public OVitrinIdentityDbContext(DbContextOptions<OVitrinIdentityDbContext> options) : base(options)
    {
    }

    public DbSet<OVitrinUser>? OVitrinUsers { get; set; }
}