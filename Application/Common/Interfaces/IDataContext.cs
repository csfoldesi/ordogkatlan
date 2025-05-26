using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IDataContext
    {
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Production> Programs { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public DbSet<Performance> TimeTables { get; set; }
        public DbSet<UserPerformance> UserTimetables { get; set; }
        public DbSet<Village> Villages { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
