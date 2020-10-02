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


    }
}
