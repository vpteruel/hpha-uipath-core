using System.Text.Json.Serialization;

namespace HPHA.UiPath.Core.Azure.DocumentIntelligence
{
    public class Word
    {
        [JsonPropertyName("content")]
        public string? Content { get; set; }

        [JsonPropertyName("polygon")]
        public List<double?>? Polygon { get; set; }

        [JsonPropertyName("confidence")]
        public double? Confidence { get; set; }

        [JsonPropertyName("span")]
        public Span? Span { get; set; }
    }
}
