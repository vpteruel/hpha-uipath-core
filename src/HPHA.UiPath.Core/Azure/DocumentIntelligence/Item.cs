using System.Text.Json.Serialization;

namespace HPHA.UiPath.Core.Azure.DocumentIntelligence
{
    public class Item
    {
        [JsonPropertyName("Description")]
        public string? Description { get; set; }

        [JsonPropertyName("Quantity")]
        public int? Quantity { get; set; }

        [JsonPropertyName("Unit")]
        public string? Unit { get; set; }

        [JsonPropertyName("UnitPrice")]
        public double? UnitPrice { get; set; }

        [JsonPropertyName("Tax")]
        public double? Tax { get; set; }

        [JsonPropertyName("Amount")]
        public double? Amount { get; set; }
    }
}
