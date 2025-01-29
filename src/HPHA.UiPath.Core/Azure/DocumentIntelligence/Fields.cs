using System.Text.Json.Serialization;

namespace HPHA.UiPath.Core.Azure.DocumentIntelligence
{
    public class Fields
    {
        [JsonPropertyName("AmountDue")]
        public AmountDue AmountDue { get; set; }

        [JsonPropertyName("BillingAddress")]
        public BillingAddress BillingAddress { get; set; }

        [JsonPropertyName("BillingAddressRecipient")]
        public BillingAddressRecipient BillingAddressRecipient { get; set; }

        [JsonPropertyName("CustomerName")]
        public CustomerName CustomerName { get; set; }

        [JsonPropertyName("InvoiceDate")]
        public InvoiceDate InvoiceDate { get; set; }

        [JsonPropertyName("InvoiceId")]
        public InvoiceId InvoiceId { get; set; }

        [JsonPropertyName("InvoiceTotal")]
        public InvoiceTotal InvoiceTotal { get; set; }

        [JsonPropertyName("Items")]
        public Items Items { get; set; }

        [JsonPropertyName("PaymentTerm")]
        public PaymentTerm PaymentTerm { get; set; }

        [JsonPropertyName("PurchaseOrder")]
        public PurchaseOrder PurchaseOrder { get; set; }

        [JsonPropertyName("RemittanceAddress")]
        public RemittanceAddress RemittanceAddress { get; set; }

        [JsonPropertyName("RemittanceAddressRecipient")]
        public RemittanceAddressRecipient RemittanceAddressRecipient { get; set; }

        [JsonPropertyName("ServiceStartDate")]
        public ServiceStartDate ServiceStartDate { get; set; }

        [JsonPropertyName("SubTotal")]
        public SubTotal SubTotal { get; set; }

        [JsonPropertyName("TotalTax")]
        public TotalTax TotalTax { get; set; }

        [JsonPropertyName("VendorName")]
        public VendorName VendorName { get; set; }
    }
}
