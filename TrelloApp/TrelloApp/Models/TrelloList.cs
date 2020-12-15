using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrelloApp.Models
{
    public class TrelloList
    {
        [JsonProperty (PropertyName = "id")]
        public string ListId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        public TrelloBoard Board { get; set; }
    }
}
