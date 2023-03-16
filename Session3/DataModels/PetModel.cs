using Newtonsoft.Json;

namespace APITraining.Session3.DataModels
{
    public class PetModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("category")]
        public CategoryModel Category { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("photoUrls")]
        public string[] PhotoUrls { get; set; }

        [JsonProperty("tags")]
        public CategoryModel[] Tags { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
