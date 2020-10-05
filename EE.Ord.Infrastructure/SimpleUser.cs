using System;
using System.Collections.Generic;
using System.Text;

namespace EE.Ord.Infrastructure
{
    public class SimpleUser : IInfrastructureUser
    {
        public string AuthenticationType { get; }
        public bool IsAuthenticated { get; }
        public string Name { get; }
        public Guid ID { get; }
        public int? UtcOffset { get; set; }

        public SimpleUser(string name, Guid id, int? utcOffset)
        {
            Name = name;
            ID = id;
            UtcOffset = utcOffset;
        }
    }
}
