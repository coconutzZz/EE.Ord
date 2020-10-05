using System;

namespace EE.Ord.Infrastructure.EntityFramework
{
    public interface IEntityWithAuditing : IEntity
    {
        DateTime CreatedOn { get; set; }
        DateTime? ModifiedOn { get; set; }
        Guid CreatedBy { get; set; }
        Guid? ModifiedBy { get; set; }
    }
}
