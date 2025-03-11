using System.Diagnostics.CodeAnalysis;

namespace HPHA.UiPath.Core.Entities.Common
{
    [ExcludeFromCodeCoverage]
    public class ReferenceEntity
    {
        public FileInfo? Pdf { get; set; }
        public FileInfo? DetailedJson { get; set; }
        public FileInfo? CompactedJson { get; set; }
    }
}
