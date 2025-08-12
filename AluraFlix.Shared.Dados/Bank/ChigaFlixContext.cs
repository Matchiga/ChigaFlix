using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ChigaFlix.Models;
using ChigaFlix.Shared.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace ChigaFlix.Shared.Data.Bank;

public class ChigaFlixContext : IdentityDbContext<PersonWithAccess, AccessProfile, int>
{
    public DbSet<Videos> Videos { get; set; }
    public DbSet<Categories> Categories { get; set; }

    private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AluraFlix;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

    public ChigaFlixContext(DbContextOptions<ChigaFlixContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Videos>()
            .HasOne(v => v.Categories) 
            .WithMany(c => c.Videos) 
            .HasForeignKey(v => v.CategoriesId);

        modelBuilder.Entity<IdentityUserLogin<int>>()
                    .HasKey(login => new { login.LoginProvider, login.ProviderKey });

        modelBuilder.Entity<IdentityUserRole<int>>()
            .HasKey(role => new { role.UserId, role.RoleId });
    }
}
