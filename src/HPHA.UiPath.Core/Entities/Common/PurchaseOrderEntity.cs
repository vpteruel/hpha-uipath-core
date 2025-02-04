using System.Diagnostics.CodeAnalysis;

namespace HPHA.UiPath.Core.Entities.Common
{
    [ExcludeFromCodeCoverage]
    public class PurchaseOrderEntity
    {
        public DateOnly? InvoiceDate { get; set; }
        public string? InvoiceId { get; set; }
        public string? InvoiceNumber => InvoiceId;
        public double? InvoiceTotal { get; set; }
        public string? PurchaseOrder { get; set; }
        public double? SubTotal { get; set; }
        public double? TotalTax { get; set; }
        public PurchaseOrderVendorEntity? Vendor { get; set; }
        public PurchaseOrderItemEntity[]? Items { get; set; }
        
        public ReferenceEntity? Reference { get; set; }
    }
}
