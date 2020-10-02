using System;
using System.Linq;
using System.Threading.Tasks;
using EE.Ord.Domain.MasterData;

namespace EE.Ord.Shared.Database.Main
{
    public interface IStorageBroker
    {
        public ValueTask<Patient> InsertPatientAsync(Patient patient);
        public IQueryable<Patient> SelectAllPatients();
        public ValueTask<Patient> SelectPatientByIdAsync(Guid patientId);
        public ValueTask<Patient> UpdatePatientAsync(Patient patient);
        public ValueTask<Patient> DeletePatientAsync(Patient patient);
    }
}
