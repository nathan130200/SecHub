using Microsoft.EntityFrameworkCore;
using SecHub.Models;

namespace SecHub;

public class EscolaDbContext : DbContext
{
    public EscolaDbContext(DbContextOptions options) : base(options)
    {
    }

    protected EscolaDbContext()
    {
    }

    public DbSet<Aluno> Aluno { get; set; }
    public DbSet<Responsavel> Responsavel { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EscolaDbContext).Assembly);
    }
}
