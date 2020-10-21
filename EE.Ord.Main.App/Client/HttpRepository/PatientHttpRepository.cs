using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EE.Ord.Domain.MasterData;
using EE.Ord.Main.App.Client.Infrastructure;
using EE.Ord.Main.App.Client.Infrastructure.Abstractions.Cache;

namespace EE.Ord.Main.App.Client.HttpRepository
{
    public class PatientHttpRepository : IPatientHttpRepository
    {
        private readonly HttpClient _httpClient;

        public PatientHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IList<Patient>> GetPatients()
        {
            var response = await _httpClient.GetAsync("patients");
            var content = await response.Content.ReadAsStringAsync();

            IEnumerable<Patient> patients = JsonSerializer.Deserialize<IEnumerable<Patient>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return patients.ToList();
        }

        public async Task<IQueryable<Patient>> SearchPatient(string insuranceNumber, string firstName, string lastName, DateTime? dateOfBirth)
        {
            
            Patient patient = new Patient
            {
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth
            };

            if (long.TryParse(insuranceNumber, out long insuranceNum))
            {
                patient.InsuranceNumber = insuranceNum;
            }

            HttpContent bodyContent = new StringContent(JsonSerializer.Serialize(patient), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("patients/find", bodyContent);
            var content = await response.Content.ReadAsStringAsync();

            IQueryable<Patient> patients = JsonSerializer.Deserialize<IQueryable<Patient>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return patients;
        }
    }
}
