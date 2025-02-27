using System.Text.Json.Serialization;

namespace HPHA.UiPath.Core.Azure.DocumentIntelligence.Minified
{
    public class StringValue
    {
        [JsonPropertyName("content")]
        public string? Content { get; set; }

        [JsonPropertyName("confidence")]
        public double? Confidence { get; set; }
    }
}
