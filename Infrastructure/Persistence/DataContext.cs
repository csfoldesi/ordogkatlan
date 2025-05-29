using Application.Core.Interfaces;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Persistence;

public class DataContext : IdentityDbContext<User>, IDataContext
{
    public DataContext(DbContextOptions options)
        : base(options) { }

    public DbSet<Genre> Genres { get; set; }
    public DbSet<Production> Productions { get; set; }
    public DbSet<Stage> Stages { get; set; }
    public DbSet<Performance> Performances { get; set; }
    public DbSet<UserPerformance> UserTimetables { get; set; }
    public DbSet<Village> Villages { get; set; }

    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
    {
        return this.Database.BeginTransactionAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder
            .Entity<Stage>()
            .HasOne(s => s.Village)
            .WithMany(v => v.Stages)
            .HasForeignKey(s => s.VillageId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .Entity<Performance>()
            .HasOne(p => p.Production)
            .WithMany(pr => pr.Performances)
            .HasForeignKey(p => p.ProductionId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .Entity<Performance>()
            .HasOne(p => p.Stage)
            .WithMany(s => s.Performances)
            .HasForeignKey(p => p.StageId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Production>().HasMany(p => p.Genres).WithMany(g => g.Productions);

        builder
            .Entity<UserPerformance>()
            .HasOne(u => u.User)
            .WithMany(u => u.Timetable)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .Entity<UserPerformance>()
            .HasOne(u => u.Performance)
            .WithMany(p => p.UserPerformances)
            .HasForeignKey(u => u.PerformanceId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
