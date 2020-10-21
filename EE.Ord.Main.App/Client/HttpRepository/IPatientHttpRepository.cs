using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EE.Ord.Domain.MasterData;

namespace EE.Ord.Main.App.Client.HttpRepository
{
    public interface IPatientHttpRepository
    {
        Task<IList<Patient>> GetPatients();

        Task<IQueryable<Patient>> SearchPatient(string insuranceNumber, string firstName, string lastName,
            DateTime? dateOfBirth);
    }
}
