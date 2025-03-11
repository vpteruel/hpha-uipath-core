using System.Diagnostics.CodeAnalysis;

namespace HPHA.UiPath.Core.Entities.Common
{
    [ExcludeFromCodeCoverage]
    public class PurchaseOrderVendorEntity
    {
        public string? Number { get; set; }
        public string? Mnemonic { get; set; }
        public string? Name { get; set; }
    }
}
