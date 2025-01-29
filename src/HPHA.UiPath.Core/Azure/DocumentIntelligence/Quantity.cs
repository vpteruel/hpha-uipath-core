using System.Text.Json.Serialization;

namespace HPHA.UiPath.Core.Azure.DocumentIntelligence
{
    public class Quantity
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("valueNumber")]
        public int? ValueNumber { get; set; }

        [JsonPropertyName("content")]
        public string? Content { get; set; }

        [JsonPropertyName("boundingRegions")]
        public List<BoundingRegion>? BoundingRegions { get; set; }

        [JsonPropertyName("confidence")]
        public double? Confidence { get; set; }

        [JsonPropertyName("spans")]
        public List<Span>? Spans { get; set; }
    }
}
