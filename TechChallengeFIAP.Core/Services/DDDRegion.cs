namespace TechChallengeFIAP.Core
{
    public static class DDDRegion
    {
        public class DDDInfo
        {
            public string state { get; set; }
            public List<string> cities { get; set; }
        }

        public static DDDInfo GetInfo(string DDD)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://brasilapi.com.br/api/ddd/v1/");
            var response = client.GetAsync($"{DDD}").Result;
            DDDInfo getResponse = new();
            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var getData = System.Text.Json.JsonSerializer.Deserialize<DDDInfo>(responseContent);
                //Console.WriteLine("UF: " + getData.state);
                //Console.WriteLine("Cidades: " + getData.cities);
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
