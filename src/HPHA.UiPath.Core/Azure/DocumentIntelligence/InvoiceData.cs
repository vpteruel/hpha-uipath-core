using System.Text.Json.Serialization;

namespace HPHA.UiPath.Core.Azure.DocumentIntelligence
{
    public class InvoiceData
    {
        [JsonPropertyName("identifier")]
        public string? Identifier { get; set; }

        [JsonPropertyName("invoice_type")]
        public string? InvoiceType { get; set; }
        
        [JsonPropertyName("invoice_number")]
        public string? InvoiceNumber { get; set; }

        [JsonPropertyName("po_number")]
        public string? PoNumber { get; set; }

        [JsonPropertyName("invoice_date")]
        public string? InvoiceDate { get; set; }

        [JsonPropertyName("vendor_name")]
        public string? VendorName { get; set; }

        [JsonPropertyName("amount_freight")]
        public double? AmountFreight { get; set; }

        [JsonPropertyName("amount_untaxed")]
        public double? AmountUntaxed { get; set; }

        [JsonPropertyName("amount_tax")]
        public double? AmountTax { get; set; }

        [JsonPropertyName("amount")]
        public double? Amount { get; set; }

        [JsonPropertyName("amount_due")]
        public double? AmountDue { get; set; }

        [JsonPropertyName("shipping_credit")]
        public double? ShippingCredit { get; set; }

        [JsonPropertyName("handling_fee")]
        public double? HandlingFee { get; set; }

        [JsonPropertyName("hazardous_fee")]
        public double? HazardousFee { get; set; }

        [JsonPropertyName("fuel_surcharge")]
        public double? FuelSurcharge { get; set; }

        [JsonPropertyName("transportation_charge")]
        public double? TransportationCharge { get; set; }

        [JsonPropertyName("minimum_order")]
        public double? MinimumOrder { get; set; }

        [JsonPropertyName("Items")]
        public List<Item>? Items { get; set; }
    }
}
