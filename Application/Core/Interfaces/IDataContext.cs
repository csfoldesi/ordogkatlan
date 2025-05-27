using Microsoft.EntityFrameworkCore;

namespace Application.Core.Interfaces
{
    public interface IDataContext
    {
        public DbSet<Domain.Genre> Genres { get; set; }
        public DbSet<Domain.Production> Programs { get; set; }
        public DbSet<Domain.Stage> Stages { get; set; }
        public DbSet<Domain.Performance> TimeTables { get; set; }
        public DbSet<Domain.UserPerformance> UserTimetables { get; set; }
        public DbSet<Domain.Village> Villages { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
