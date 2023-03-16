using Newtonsoft.Json;

namespace APITraining.Session3.DataModels
{
    public class CategoryModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
