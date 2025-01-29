using System.Diagnostics.CodeAnalysis;

namespace HPHA.UiPath.Core.Entities.Common
{
    [ExcludeFromCodeCoverage]
    public class PurchaseOrderItemEntity
    {
        public int? PoLine { get; set; }
        public string? Number { get; set; }
        public string? Name { get; set; }
        public double? Amount { get; set; }
        public string? Description { get; set; }
        public int? Quantity { get; set; }
        public double? Tax { get; set; }
        public string? Unit { get; set; }
        public double? UnitPrice { get; set; }
    }
}
