using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrelloApp.Models
{
    public class TrelloCard
    {
        [JsonProperty(PropertyName = "id")]
        public string CardId { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "closed")]
        public bool IsClosed { get; set; }

        [JsonProperty(PropertyName ="badges")]
        public CardInformation CardInfo { get; set; }
        public TrelloList List { get; set; }
    }
}
