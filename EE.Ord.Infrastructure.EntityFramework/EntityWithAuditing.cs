using System;
using System.Collections.Generic;
using System.Text;

namespace EE.Ord.Infrastructure.EntityFramework
{
    public class EntityWithAuditing : IEntityWithAuditing
    {
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }
    }
}
