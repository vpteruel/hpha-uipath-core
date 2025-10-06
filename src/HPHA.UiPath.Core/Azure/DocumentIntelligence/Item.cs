using System.Text.Json.Serialization;

namespace HPHA.UiPath.Core.Azure.DocumentIntelligence
{
    public class Item
    {
        [JsonPropertyName("Quantity")]
        public int? Quantity { get; set; }

        [JsonPropertyName("amount_unit")]
        public double? AmountUnit { get; set; }

        [JsonPropertyName("amount_tax")]
        public double? AmountTax { get; set; }

        [JsonPropertyName("amount")]
        public double? Amount { get; set; }
    }
}
