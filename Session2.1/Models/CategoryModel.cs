﻿using Newtonsoft.Json;

namespace APITraining.Session2._1
{
    internal class CategoryModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}