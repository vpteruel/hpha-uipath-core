using System.Diagnostics.CodeAnalysis;

namespace HPHA.UiPath.Core.Entities.Common
{
    [ExcludeFromCodeCoverage]
    public class PurchaseOrderEntity
    {
        public string? ModelId { get; set; }
        public Ulid? UniqueId { get; set; }
        public string? InvoiceId { get; set; }
        public DateOnly? InvoiceDate { get; set; }
        public DateOnly? DueDate { get; set; }
        public string? PurchaseOrder { get; set; }
        public PurchaseOrderVendorEntity? Vendor { get; set; }
        public double? Freight { get; set; }
        public double? HazardousFee { get; set; }
        public double? FuelSurcharge { get; set; }
        public double? TransportationCharge { get; set; }
        public double? MinimumOrder { get; set; }
        public double? SubTotal { get; set; }
        public double? TotalTax { get; set; }
        public double? InvoiceTotal { get; set; }
        public double? AmountDue { get; set; }
        public PurchaseOrderItemEntity[]? Items { get; set; }

        public ReferenceEntity? Reference { get; set; }
    }
}
