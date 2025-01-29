using System.Text.Json.Serialization;

namespace HPHA.UiPath.Core.Azure.DocumentIntelligence
{
    public class Cell
    {
        [JsonPropertyName("kind")]
        public string? Kind { get; set; }

        [JsonPropertyName("rowIndex")]
        public int? RowIndex { get; set; }

        [JsonPropertyName("columnIndex")]
        public int? ColumnIndex { get; set; }

        [JsonPropertyName("content")]
        public string? Content { get; set; }

        [JsonPropertyName("boundingRegions")]
        public List<BoundingRegion>? BoundingRegions { get; set; }

        [JsonPropertyName("spans")]
        public List<Span>? Spans { get; set; }
    }
}
