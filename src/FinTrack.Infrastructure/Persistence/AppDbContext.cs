using FinTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace FinTrack.Infrastructure.Persistence;

public class AppDbContent(DbContextOptions<AppDbContent> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Fund> Funds => Set<Fund>();

    public DbSet<Investment> Investments => Set<Investment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContent).Assembly);
    }

}