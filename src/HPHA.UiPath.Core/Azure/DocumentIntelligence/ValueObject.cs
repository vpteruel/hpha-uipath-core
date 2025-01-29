using System.Text.Json.Serialization;

namespace HPHA.UiPath.Core.Azure.DocumentIntelligence
{
    public class ValueObject
    {
        [JsonPropertyName("Amount")]
        public Amount Amount { get; set; }

        [JsonPropertyName("Date")]
        public Date Date { get; set; }

        [JsonPropertyName("Description")]
        public Description Description { get; set; }

        [JsonPropertyName("ProductCode")]
        public ProductCode ProductCode { get; set; }

        [JsonPropertyName("PurchaseOrder")]
        public PurchaseOrder PurchaseOrder { get; set; }

        [JsonPropertyName("Quantity")]
        public Quantity Quantity { get; set; }

        [JsonPropertyName("Tax")]
        public Tax Tax { get; set; }

        [JsonPropertyName("UnitPrice")]
        public UnitPrice UnitPrice { get; set; }
    }
}
