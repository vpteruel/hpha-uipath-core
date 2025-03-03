using System.Text.Json.Serialization;

namespace HPHA.UiPath.Core.Azure.DocumentIntelligence
{
    public class Item
    {
        [JsonPropertyName("Description")]
        public StringValue? Description { get; set; }

        [JsonPropertyName("Quantity")]
        public IntValue? Quantity { get; set; }

        [JsonPropertyName("UnitPrice")]
        public DoubleValue? UnitPrice { get; set; }

        [JsonPropertyName("Tax")]
        public DoubleValue? Tax { get; set; }

        [JsonPropertyName("Amount")]
        public DoubleValue? Amount { get; set; }
    }
}
