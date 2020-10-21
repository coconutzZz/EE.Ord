using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using EE.Ord.Domain.MasterData;
using EE.Ord.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EE.Ord.Shared.Database.Main
{
    public partial class StorageBroker
    {
        public DbSet<Patient> Patients { get; set; }

        public async ValueTask<Patient> InsertPatientAsync(Patient patient)
        {
            EntityEntry<Patient> patientEntityEntry = await Patients.AddAsync(patient);
            await SaveChangesAsync();

            return patientEntityEntry.Entity;
        }

        public IQueryable<Patient> SelectAllPatients() => Patients.AsQueryable();

        public async ValueTask<Patient> SelectPatientByIdAsync(Guid patientId)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return await Patients.FindAsync(patientId);
        }

        public IQueryable<Patient> FindPatient(Patient patient)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            IQueryable<Patient> patientResult = Patients.AsQueryable().Where(x => x.InsuranceNumber.ToString().Contains(patient.InsuranceNumber.ToString()))
                                                    .Where(x => x.FirstName.StartsWith(patient.FirstName))
                                                    .Where(x => x.LastName.StartsWith(patient.LastName));

            return patientResult;
        }


        public async ValueTask<Patient> UpdatePatientAsync(Patient patient)
        {
            EntityEntry<Patient> patientEntityEntry = Patients.Update(patient);
            await SaveChangesAsync();

            return patientEntityEntry.Entity;
        }

        public async ValueTask<Patient> DeletePatientAsync(Patient patient)
        {
            EntityEntry<Patient> patientEntityEntry = Patients.Remove(patient);
            await this.SaveChangesAsync();

            return patientEntityEntry.Entity;
        }
    }
}
