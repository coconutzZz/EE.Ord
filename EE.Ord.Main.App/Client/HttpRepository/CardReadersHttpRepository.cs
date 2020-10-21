using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using EE.Ord.Domain.MasterData;
using EE.Ord.Main.App.Client.Models;

namespace EE.Ord.Main.App.Client.HttpRepository
{
    public class CardReadersHttpRepository
    {
        private readonly HttpClient _httpClient;

        public CardReadersHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IList<CardReader>> GetCardReaders()
        {
            var response = await _httpClient.GetAsync("patients");
            var content = await response.Content.ReadAsStringAsync();

            IEnumerable<CardReader> cardReaders = JsonSerializer.Deserialize<IEnumerable<CardReader>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return cardReaders.ToList();
        }
    }
}
