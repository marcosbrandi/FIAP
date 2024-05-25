using TechChallengeFIAP.Core.Entities;
using TechChallengeFIAP.Core.Interfaces;

namespace TechChallengeFIAP.Infrastructure.Services
{
    public class DDDRegionService : IDDDRegionService
    {
        private readonly HttpClient client;
        public DDDRegionService(HttpClient httpClient)
        {
            client = httpClient;
        }

        /// <summary>
        /// Método que retorna informações da região sobre o DDD inserido
        /// </summary>
        /// <param name="DDD"></param>
        /// <returns></returns>
        //public static DDDInfo GetInfo(string DDD)
        public async Task<DDDInfo> GetInfo(string DDD)
        {
            if (client.BaseAddress is null)
                client.BaseAddress = new Uri("https://brasilapi.com.br/api/ddd/v1/");
            var response = client.GetAsync($"{DDD}").Result;
            DDDInfo getResponse = new();
            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var getData = System.Text.Json.JsonSerializer.Deserialize<DDDInfo>(responseContent);
                getResponse = getData;
            }
            else
            {
                Console.WriteLine("Error: " + response.StatusCode);
            }
            return getResponse;
        }
    }
}
