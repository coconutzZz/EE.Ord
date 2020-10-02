using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EE.Ord.Infrastructure.EntityFramework
{
    public abstract class DbContextBase : DbContext, IDbContextBase
    {
        private readonly IInfrastructureUser _infrastructureUser;

        protected DbContextBase(DbContextOptions options, IInfrastructureUser infrastructureUser) : base(options)
        {
            _infrastructureUser = infrastructureUser;
        }

        public override int SaveChanges()
        {
            PrepareAuditedEntities();

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            PrepareAuditedEntities();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            PrepareAuditedEntities();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void PrepareAuditedEntities()
        {
            var addedEntities = ChangeTracker.Entries<IEntityWithAuditing>()
                .Where(p => p.State == EntityState.Added).Select(p => p.Entity);

            var modifiedEntities = ChangeTracker.Entries<IEntityWithAuditing>()
                .Where(p => p.State == EntityState.Modified).Select(p => p.Entity);


            var now = DateTimeOffset.UtcNow;
            var user = _infrastructureUser;
            
            foreach (var entity in addedEntities)
            {
                entity.CreatedBy = user.ID;
                entity.CreatedOn = now;
            }

            foreach (var entity in modifiedEntities)
            {
                entity.ModifiedBy = user.ID;
                entity.ModifiedOn = now;
            }
        }
    }
}
