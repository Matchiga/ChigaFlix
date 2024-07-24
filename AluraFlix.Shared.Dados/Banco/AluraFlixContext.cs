using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AluraFlix.Modelos;
using AluraFlix.Shared.Dados.Modelos;
using Microsoft.AspNetCore.Identity;

namespace AluraFlix.Shared.Dados.Banco;

public class AluraFlixContext : IdentityDbContext<PessoaComAcesso, PerfilDeAcesso, int>
{
    public DbSet<Videos> Videos { get; set; }
    public DbSet<Categorias> Categorias { get; set; }

    private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AluraFlix;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

    public AluraFlixContext(DbContextOptions<AluraFlixContext> options) : base(options)
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
            .HasOne(v => v.Categorias) 
            .WithMany(c => c.Videos) 
            .HasForeignKey(v => v.CategoriasId);

        modelBuilder.Entity<IdentityUserLogin<int>>()
                    .HasKey(login => new { login.LoginProvider, login.ProviderKey });

        modelBuilder.Entity<IdentityUserRole<int>>()
            .HasKey(role => new { role.UserId, role.RoleId });
    }
}
