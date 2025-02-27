using System.Text.Json.Serialization;

namespace HPHA.UiPath.Core.Azure.DocumentIntelligence.Minified
{
    public class DoubleValue
    {
        [JsonPropertyName("content")]
        public double? Content { get; set; }

        [JsonPropertyName("confidence")]
        public double? Confidence { get; set; }
    }
}
