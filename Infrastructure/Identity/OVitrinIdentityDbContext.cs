using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity;

public class OVitrinIdentityDbContext : IdentityDbContext
{
    public OVitrinIdentityDbContext(DbContextOptions options) : base(options)
    {
    }
}