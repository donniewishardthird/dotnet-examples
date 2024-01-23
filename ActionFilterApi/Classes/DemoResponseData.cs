/*
    File: DemoResponseData.cs
    Author: Donnie Wishard
    Date: January 23, 2024
    Description: This class hold data related to a Demo Data Response.
*/
using System.Text.Json.Serialization;

namespace ActionFilterApi.Classes
{
    public class DemoResponseData
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("requestId")]
        public string RequestId { get; set; } = string.Empty;

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
    }
}
