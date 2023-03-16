using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITraining.Session3
{
    internal class PetModel
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
