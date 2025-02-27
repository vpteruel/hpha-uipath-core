using System.Text.Json.Serialization;

namespace HPHA.UiPath.Core.Azure.DocumentIntelligence.Minified
{
    public class Item
    {
        [JsonPropertyName("Quantity")]
        public IntValue? Quantity { get; set; }

        [JsonPropertyName("Unit")]
        public StringValue? Unit { get; set; }

        [JsonPropertyName("UnitPrice")]
        public DoubleValue? UnitPrice { get; set; }

        [JsonPropertyName("Date")]
        public StringValue? Date { get; set; }

        [JsonPropertyName("Tax")]
        public DoubleValue? Tax { get; set; }

        [JsonPropertyName("Amount")]
        public DoubleValue? Amount { get; set; }
    }
}
