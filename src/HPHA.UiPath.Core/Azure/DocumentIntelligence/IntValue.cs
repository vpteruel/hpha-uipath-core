using System.Text.Json.Serialization;

namespace HPHA.UiPath.Core.Azure.DocumentIntelligence
{
    public class IntValue
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("content")]
        public int? Content { get; set; }

        [JsonPropertyName("confidence")]
        public double? Confidence { get; set; }
    }
}
