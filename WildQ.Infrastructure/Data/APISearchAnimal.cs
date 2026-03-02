using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Entities.Models.ApiModels;

namespace WildQ.Infrastructure.Data
{
    public class APISearchAnimal
    {
        private static string _baseAddress = "https://api.api-ninjas.com/";
        private static string _uri = "v1/animals?name=";
        private static string _apiKey = "kFuMOyBGTMbivVSVlERno2L4Qy0tX5MiXVf6pIet";

        public static async Task<List<SearchAnimal>> GetAnimalsAsync(string animalName)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(_baseAddress);
            client.DefaultRequestHeaders.Add("X-Api-Key", _apiKey);

            HttpResponseMessage response = await client.GetAsync(_uri + animalName);

            List<SearchAnimal> animals = null;
            if (response.IsSuccessStatusCode)
            {
                string responseStr = await response.Content.ReadAsStringAsync();
                animals = JsonSerializer.Deserialize<List<SearchAnimal>>(responseStr);
            }

            return animals;
        }
    }
}
