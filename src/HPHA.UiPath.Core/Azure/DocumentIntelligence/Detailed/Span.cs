using System.Text.Json.Serialization;

namespace HPHA.UiPath.Core.Azure.DocumentIntelligence.Detailed
{
    public class Span
    {
        [JsonPropertyName("offset")]
        public int? Offset { get; set; }

        [JsonPropertyName("length")]
        public int? Length { get; set; }
    }
}
