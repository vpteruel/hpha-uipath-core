using System.Text.Json.Serialization;

namespace HPHA.UiPath.Core.Azure.DocumentIntelligence
{
    public class Line
    {
        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonPropertyName("polygon")]
        public List<double?> Polygon { get; set; }

        [JsonPropertyName("spans")]
        public List<Span> Spans { get; set; }
    }
}
