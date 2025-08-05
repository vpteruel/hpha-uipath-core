using System.Text.Json.Serialization;

namespace HPHA.UiPath.Core.Azure.DocumentIntelligence
{
    public class InvoiceData
    {
        [JsonPropertyName("ModelId")]
        public string? ModelId { get; set; }

        [JsonPropertyName("UniqueId")]
        public string? UniqueId { get; set; }

        [JsonPropertyName("Type")]
        public string? Type { get; set; }

        [JsonPropertyName("InvoiceId")]
        public string? InvoiceId { get; set; }

        [JsonPropertyName("InvoiceDate")]
        public string? InvoiceDate { get; set; }

        [JsonPropertyName("DueDate")]
        public string? DueDate { get; set; }

        [JsonPropertyName("PurchaseOrder")]
        public string? PurchaseOrder { get; set; }

        [JsonPropertyName("VendorName")]
        public string? VendorName { get; set; }

        [JsonPropertyName("Freight")]
        public double? Freight { get; set; }

        [JsonPropertyName("ShippingCredit")]
        public double? ShippingCredit { get; set; }

        [JsonPropertyName("HazardousFee")]
        public double? HazardousFee { get; set; }

        [JsonPropertyName("FuelSurcharge")]
        public double? FuelSurcharge { get; set; }

        [JsonPropertyName("TransportationCharge")]
        public double? TransportationCharge { get; set; }

        [JsonPropertyName("MinimumOrder")]
        public double? MinimumOrder { get; set; }

        [JsonPropertyName("SubTotal")]
        public double? SubTotal { get; set; }

        [JsonPropertyName("TotalTax")]
        public double? TotalTax { get; set; }

        [JsonPropertyName("InvoiceTotal")]
        public double? InvoiceTotal { get; set; }

        [JsonPropertyName("AmountDue")]
        public double? AmountDue { get; set; }

        [JsonPropertyName("Items")]
        public List<Item>? Items { get; set; }
    }
}
