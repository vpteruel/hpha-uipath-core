using System.Diagnostics.CodeAnalysis;

namespace HPHA.UiPath.Core.Entities.Common
{
    [ExcludeFromCodeCoverage]
    public class ReferenceEntity
    {
        public string? EmailFrom { get; set; }
        public string? VendorNickName { get; set; }
        public FileInfo? Pdf { get; set; }
        public FileInfo? DetailedJson { get; set; }
        public FileInfo? CompactedJson { get; set; }
    }
}
