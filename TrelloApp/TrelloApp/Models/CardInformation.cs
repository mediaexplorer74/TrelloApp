using Newtonsoft.Json;

namespace TrelloApp.Models
{
    public class CardInformation
    {
        [JsonProperty(PropertyName = "comments")]
        public int NumComments { get; set; }

        [JsonProperty(PropertyName = "attachments")]
        public int NumAttachments { get; set; }
    }
}