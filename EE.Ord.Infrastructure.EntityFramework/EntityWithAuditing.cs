using System;
using System.Collections.Generic;
using System.Text;

namespace EE.Ord.Infrastructure.EntityFramework
{
    public class EntityWithAuditing : IEntityWithAuditing
    {
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset ModifiedOn { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }
    }
}
