using System;
using System.Linq;
using System.Threading.Tasks;
using EE.Ord.Domain.MasterData;
using EE.Ord.Shared.Database.Main;

namespace EE.Ord.Services
{
    public class PatientsService : IPatientsService
    {
        private readonly IStorageBroker _storageBroker;

        public PatientsService(IStorageBroker storageBroker)
        {
            _storageBroker = storageBroker;
        }

        public async ValueTask<Patient> AddNewPatient(Patient patient) => await _storageBroker.InsertPatientAsync(patient);
        public ValueTask<Patient> RetrievePatientByIdAsync(Guid patientId)
        {
            throw new NotImplementedException();
        }

        public ValueTask<Patient> ModifyPatientAsync(Patient patient)
        {
            throw new NotImplementedException();
        }

        public ValueTask<Patient> DeletePatientAsync(Guid patientId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Patient> RetrieveAllPatients()
        {
            IQueryable<Patient> storagePatients = _storageBroker.SelectAllPatients();
            return storagePatients;
        }

        public IQueryable<Patient> FindPatient(Patient patient)
        {
            IQueryable<Patient> storagePatient = _storageBroker.FindPatient(patient);
            return storagePatient;
        }
    }
}
