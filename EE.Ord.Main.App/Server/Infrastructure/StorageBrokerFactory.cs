using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EE.Ord.Infrastructure;
using EE.Ord.Shared.Database.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EE.Ord.Main.App.Server.Infrastructure
{
    public class StorageBrokerFactory : IDesignTimeDbContextFactory<StorageBroker>
    {
        public StorageBroker CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var dbContextBuilder = new DbContextOptionsBuilder();

            var connectionString = configuration
                .GetConnectionString("DefaultConnection");

            dbContextBuilder.UseSqlite(connectionString, options => options.MigrationsAssembly("EE.Ord.Main.App.Server"));

            return new StorageBroker(dbContextBuilder.Options, new SimpleUser("SystemUser", Guid.Parse("9DB5F12C-8428-405B-928C-7644F624C081"), 0));
        }
    }
}
