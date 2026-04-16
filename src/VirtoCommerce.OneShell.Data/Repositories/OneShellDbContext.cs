using System.Reflection;
using Microsoft.EntityFrameworkCore;
using VirtoCommerce.OneShell.Data.Models;
using VirtoCommerce.Platform.Data.Infrastructure;

namespace VirtoCommerce.OneShell.Data.Repositories;

public class OneShellDbContext : DbContextBase
{
    public OneShellDbContext(DbContextOptions<OneShellDbContext> options)
        : base(options)
    {
    }

    protected OneShellDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MainMenuEventEntity>().ToTable("MainMenuEvent").HasKey(x => x.Id);
        modelBuilder.Entity<MainMenuEventEntity>().Property(x => x.Id).HasMaxLength(IdLength).ValueGeneratedOnAdd();

        switch (Database.ProviderName)
        {
            case "Pomelo.EntityFrameworkCore.MySql":
                modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("VirtoCommerce.OneShell.Data.MySql"));
                break;
            case "Npgsql.EntityFrameworkCore.PostgreSQL":
                modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("VirtoCommerce.OneShell.Data.PostgreSql"));
                break;
            case "Microsoft.EntityFrameworkCore.SqlServer":
                modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("VirtoCommerce.OneShell.Data.SqlServer"));
                break;
        }
    }
}
