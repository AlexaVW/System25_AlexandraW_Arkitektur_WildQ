using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities.Models.ApiModels
{
    // From API Ninjas - Used for Search Animal
    public class SearchAnimal
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("characteristics")]
        public Characteristics Characteristics { get; set; }
    }

    public class Characteristics
    {
        [JsonPropertyName("name_of_young")]
        public string Name_of_young { get; set; }

        [JsonPropertyName("group_behavior")]
        public string Group_behavior { get; set; }

        [JsonPropertyName("estimated_population_size")]
        public string Estimated_population_size { get; set; }

        [JsonPropertyName("gestation_period")]
        public string Gestation_period { get; set; }

        [JsonPropertyName("litter_size")]
        public string Litter_size { get; set; }

        [JsonPropertyName("diet")]
        public string Diet { get; set; }

        [JsonPropertyName("location")]
        public string Location { get; set; }

        [JsonPropertyName("top_speed")]
        public string Top_speed { get; set; }

        [JsonPropertyName("lifespan")]
        public string Lifespan { get; set; }

        [JsonPropertyName("weight")]
        public string Weight { get; set; }

        [JsonPropertyName("height")]
        public string Height { get; set; }

        [JsonPropertyName("age_of_sexual_maturity")]
        public string Age_of_sexual_maturity { get; set; }
    }
}
