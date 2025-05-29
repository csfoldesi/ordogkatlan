using Microsoft.EntityFrameworkCore;

namespace Application.Core.Interfaces
{
    public interface IDataContext
    {
        public DbSet<Domain.Genre> Genres { get; set; }
        public DbSet<Domain.Production> Productions { get; set; }
        public DbSet<Domain.Stage> Stages { get; set; }
        public DbSet<Domain.Performance> Performances { get; set; }
        public DbSet<Domain.UserPerformance> UserTimetables { get; set; }
        public DbSet<Domain.Village> Villages { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        int SaveChanges();
        Task<Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction> BeginTransactionAsync(
            CancellationToken cancellationToken
        );
    }
}
