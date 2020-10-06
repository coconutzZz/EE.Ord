using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using EE.Ord.Domain.MasterData;

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
    }
}
