using System;

namespace EE.Ord.Infrastructure.EntityFramework
{
    public interface IEntityWithAuditing : IEntity
    {
        DateTimeOffset CreatedOn { get; set; }
        DateTimeOffset ModifiedOn { get; set; }
        Guid CreatedBy { get; set; }
        Guid ModifiedBy { get; set; }
    }
}
