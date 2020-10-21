using System;
using System.Collections.Generic;
using System.Linq;
using EE.Ord.Domain.MasterData;
using EE.Ord.Domain.MasterData.PatientFiles;
using EE.Ord.Infrastructure;
using EE.Ord.Shared.Database.Main;
using Microsoft.EntityFrameworkCore;
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;

namespace EE.Ord.Main.DbSeeder
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<long, int> insuranceNumbers = new Dictionary<long, int>();

            IInfrastructureUser user = new SimpleUser("Seeder", Guid.NewGuid(), 0);

            var randomizerFirstName = RandomizerFactory.GetRandomizer(new FieldOptionsFirstName());
            var randomizerLastName = RandomizerFactory.GetRandomizer(new FieldOptionsLastName());
            var randomizerDateOfBirth = RandomizerFactory.GetRandomizer(new FieldOptionsDateTime() {  From = DateTime.UtcNow.AddYears(-90), To = DateTime.UtcNow.AddMonths(-2) });
            var randomizerInsuranceNumber = RandomizerFactory.GetRandomizer(new FieldOptionsLong() { Min = 1000000000, Max = 9999999999 });

            var randomizerLipsum= RandomizerFactory.GetRandomizer(new FieldOptionsTextLipsum());
            var randomizerTitles = RandomizerFactory.GetRandomizer(new FieldOptionsTextWords() {Min = 1, Max = 3, UseNullValues = false});

            var randomizerNumber = RandomizerFactory.GetRandomizer(new FieldOptionsInteger() {Min = 1, Max = 20, UseNullValues = false});

            using (var context = new SeedContext(new DbContextOptions<SeedContext>(), user))
            {
                for (int i = 0; i < 10000; i++)
                {
                    var patient = new Patient
                    {
                        FirstName = randomizerFirstName.Generate(),
                        LastName = randomizerLastName.Generate(),
                        DateOfBirth = randomizerDateOfBirth.Generate()
                    };

                    long insuranceNumber = randomizerInsuranceNumber.Generate().Value;

                    while (!insuranceNumbers.TryAdd(insuranceNumber, i))
                    {
                        insuranceNumber = randomizerInsuranceNumber.Generate().Value;
                    }

                    patient.InsuranceNumber = insuranceNumber;

                    int numNotes = randomizerNumber.Generate().Value;

                    for (int j = 0; j <= numNotes; j++)
                    {
                        var note = new SimplePatientNote
                        {
                            Content = randomizerLipsum.Generate(),
                            Title = randomizerTitles.Generate(),
                            Patient = patient
                        };

                        context.SimplePatientNotes.Add(note);
                    }

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
