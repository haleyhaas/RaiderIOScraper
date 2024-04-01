using Newtonsoft.Json;
using System.Web;

namespace RaiderIOScraper
{
    public class RaiderIOAPI
    {
        public Character GetCharacterInfo(string realm, string characterName)
        {
            var endpoint = $"https://raider.io/api/v1/characters/profile?region=us&realm={realm}&name={characterName}&fields=mythic_plus_ranks";

            using var httpClient = new HttpClient();

            // Make a GET request to the API
            HttpResponseMessage response = httpClient.GetAsync(endpoint).Result;

            // Read and display the response content as a string
            string responseBody = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<Character>(responseBody);
        }
    }
}
