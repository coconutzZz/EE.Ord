using System;
using System.Linq;
using System.Threading.Tasks;
using EE.Ord.Domain.MasterData;
using EE.Ord.Domain.MasterData.PatientFiles;

namespace EE.Ord.Shared.Database.Main
{
    public interface IStorageBroker
    {
        public ValueTask<Patient> InsertPatientAsync(Patient patient);
        public IQueryable<Patient> SelectAllPatients();
        public ValueTask<Patient> SelectPatientByIdAsync(Guid patientId);
        public ValueTask<Patient> UpdatePatientAsync(Patient patient);
        public ValueTask<Patient> DeletePatientAsync(Patient patient);
        public IQueryable<Patient> FindPatient(Patient patient);


        public ValueTask<SimplePatientNote> InsertSimplePatientNoteAsync(SimplePatientNote simplePatientNote);
        public ValueTask<SimplePatientNote> SelectSimplePatientNoteByPatientIdAsync(Guid patientId);
        public ValueTask<SimplePatientNote> UpdateSimplePatientNoteAsync(SimplePatientNote simplePatientNote);
        public ValueTask<SimplePatientNote> DeleteSimplePatientNoteAsync(SimplePatientNote simplePatientNote);


    }
}
