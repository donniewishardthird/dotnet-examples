using System.Text.Json.Serialization;

namespace ActionFilterApi.Classes
{
    public class DemoRequestData
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("data")]
        public string Data { get; set; }
    }
}
