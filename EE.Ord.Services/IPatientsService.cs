using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EE.Ord.Domain.MasterData;

namespace EE.Ord.Services
{
    public interface IPatientsService
    {
        ValueTask<Patient> AddNewPatient(Patient patient);
        ValueTask<Patient> RetrievePatientByIdAsync(Guid patientId);
        ValueTask<Patient> ModifyPatientAsync(Patient patient);
        ValueTask<Patient> DeletePatientAsync(Guid patientId);
        IQueryable<Patient> RetrieveAllPatients();
        IQueryable<Patient> FindPatient(Patient patient);
    }
}
