using System.Text.Json.Serialization;

namespace HPHA.UiPath.Core.Azure.DocumentIntelligence
{
    public class Items
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("valueArray")]
        public List<ValueArray>? ValueArray { get; set; }
    }
}
