using System;
using System.Collections.Generic;
using System.Linq;
using EE.Ord.Domain.MasterData;
using EE.Ord.Infrastructure;
using EE.Ord.Shared.Database.Main;
using Microsoft.EntityFrameworkCore;

namespace EE.Ord.Main.DbSeeder
{
    class Program
    {
        private static Random random = new Random((int)DateTime.Now.Ticks);//thanks to McAden

        static long LongRandom(long maxValue, long minValue)
        {
            return (long)Math.Round(random.NextDouble() * (maxValue - minValue - 1)) + minValue;

        }

        static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string randomString = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray()).ToLower();
            return randomString.First().ToString().ToUpper() + randomString.Substring(1);
        }


        static void Main(string[] args)
        {
            Dictionary<long, int> insuranceNumbers = new Dictionary<long, int>();

            IInfrastructureUser user = new SimpleUser("Seeder", Guid.NewGuid(), 0);
           
            using (var context = new SeedContext(new DbContextOptions<SeedContext>(), user))
            {
                for (int i = 0; i < 10000; i++)
                {
                    var patient = new Patient
                    {
                        FirstName = RandomString(random.Next(4, 15)),
                        LastName = RandomString(random.Next(4, 15)),
                    };

                    long insuranceNumber = LongRandom(9999999999, 1000000000);

                    while (!insuranceNumbers.TryAdd(insuranceNumber, i))
                    {
                        insuranceNumber = LongRandom(9999999999,1000000000);
                    }

                    patient.InsuranceNumber = insuranceNumber;

                    context.Patients.Add(patient);
                }

                context.SaveChanges();
            }
        }
    }

    public class SeedContext : StorageBroker
    {
        public SeedContext(DbContextOptions options, IInfrastructureUser infrastructureUser) : base(options, infrastructureUser)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=ee.ord.main.dev.db");
        }
    }
}
