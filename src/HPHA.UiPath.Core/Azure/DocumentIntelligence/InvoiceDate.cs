using System.Text.Json.Serialization;

namespace HPHA.UiPath.Core.Azure.DocumentIntelligence
{
    public class InvoiceDate
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("valueDate")]
        public string? ValueDate { get; set; }

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
