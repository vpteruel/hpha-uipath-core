using System.Text.Json.Serialization;

namespace HPHA.UiPath.Core.Azure.DocumentIntelligence.Simplified
{
    public class ProductCode
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("valueString")]
        public string? ValueString { get; set; }

        [JsonPropertyName("content")]
        public string? Content { get; set; }

        [JsonPropertyName("confidence")]
        public double? Confidence { get; set; }
    }
}
