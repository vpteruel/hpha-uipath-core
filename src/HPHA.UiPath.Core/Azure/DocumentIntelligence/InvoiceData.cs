using System.Text.Json.Serialization;

namespace HPHA.UiPath.Core.Azure.DocumentIntelligence
{
    public class InvoiceData
    {
        [JsonPropertyName("InvoiceId")]
        public StringValue? InvoiceId { get; set; }

        [JsonPropertyName("InvoiceDate")]
        public StringValue? InvoiceDate { get; set; }

        [JsonPropertyName("DueDate")]
        public StringValue? DueDate { get; set; }

        [JsonPropertyName("PurchaseOrder")]
        public StringValue? PurchaseOrder { get; set; }

        [JsonPropertyName("VendorName")]
        public StringValue? VendorName { get; set; }

        [JsonPropertyName("Freight")]
        public DoubleValue? Freight { get; set; }

        [JsonPropertyName("SubTotal")]
        public DoubleValue? SubTotal { get; set; }

        [JsonPropertyName("TotalTax")]
        public DoubleValue? TotalTax { get; set; }

        [JsonPropertyName("InvoiceTotal")]
        public DoubleValue? InvoiceTotal { get; set; }

        [JsonPropertyName("AmountDue")]
        public DoubleValue? AmountDue { get; set; }

        [JsonPropertyName("Items")]
        public List<Item>? Items { get; set; }
    }
}
