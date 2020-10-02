using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EE.Ord.Infrastructure.EntityFramework
{
    public interface IDbContextBase
    {
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbQuery<TEntity> Query<TEntity>() where TEntity : class;
    }
}
