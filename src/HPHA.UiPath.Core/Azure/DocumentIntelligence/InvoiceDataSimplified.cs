using System.Text.Json.Serialization;

namespace HPHA.UiPath.Core.Azure.DocumentIntelligence
{
    public class InvoiceDataSimplified
    {
        [JsonPropertyName("InvoiceDate")]
        public InvoiceDate InvoiceDate { get; set; }

        [JsonPropertyName("InvoiceId")]
        public InvoiceId InvoiceId { get; set; }

        [JsonPropertyName("InvoiceTotal")]
        public InvoiceTotal InvoiceTotal { get; set; }

        [JsonPropertyName("PurchaseOrder")]
        public PurchaseOrder PurchaseOrder { get; set; }

        [JsonPropertyName("SubTotal")]
        public SubTotal SubTotal { get; set; }

        [JsonPropertyName("TotalTax")]
        public TotalTax TotalTax { get; set; }

        [JsonPropertyName("VendorName")]
        public VendorName VendorName { get; set; }

        [JsonPropertyName("Items")]
        public List<ValueObject> Items { get; set; }
    }
}
