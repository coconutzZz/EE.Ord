using System;
using System.Linq;
using System.Threading.Tasks;
using EE.Ord.Domain.MasterData;
using EE.Ord.Infrastructure;
using EE.Ord.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
namespace EE.Ord.Shared.Database.Main
{
    public partial class StorageBroker : DbContextBase, IStorageBroker
    {
        public StorageBroker(DbContextOptions options, IInfrastructureUser infrastructureUser) : base(options, infrastructureUser)
        {
        }
    }
}
