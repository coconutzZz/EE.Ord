using System;
using System.Security.Principal;

namespace EE.Ord.Infrastructure
{
    public interface IInfrastructureUser : IIdentity
    {
        Guid ID { get; }
        int? UtcOffset { get; set; }
    }
}
