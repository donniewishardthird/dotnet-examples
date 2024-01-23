/*
    File: DemoRequestData.cs
    Author: Donnie Wishard
    Date: January 23, 2024
    Description: This class hold data related to a Demo Data Reqest.
*/
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
