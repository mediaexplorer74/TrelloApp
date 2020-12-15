using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace TrelloApp.Models
{
    public class TrelloBoard
    {
        [JsonProperty (PropertyName = "id")]
        public string BoardId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "starred")]
        public bool IsFavorite { get; set; }

        public string ColorHex { get; set; }

        public string memberId { get; set; }

        [JsonExtensionData]
        private Dictionary<string, JToken> _extraJsonData = new Dictionary<string, JToken>();

        [OnDeserialized]
        private void ProcessExtraJsonData(StreamingContext context)
        {
            //prefs.backgroundColor
            JToken prefsData = (JToken)_extraJsonData["prefs"];

            //ColorHex = (string)prefsData.SelectToken("backgroundColor");
            ColorHex = (string)prefsData.SelectToken("backgroundTopColor");

            JToken t = (JToken)_extraJsonData["memberships"];

            memberId = (string)t[0].SelectToken("idMember");
        }
    }
}
