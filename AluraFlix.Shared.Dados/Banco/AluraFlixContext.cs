using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AluraFlix.Modelos;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluraFlix.Shared.Dados.Banco;

public class AluraFlixContext : DbContext
{
    public DbSet<Videos> Videos { get; set; }

    private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AluraFlix;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

    public AluraFlixContext(DbContextOptions<AluraFlixContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(connectionString);
    }
}
