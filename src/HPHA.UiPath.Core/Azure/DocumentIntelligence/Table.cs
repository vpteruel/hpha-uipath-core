using System.Text.Json.Serialization;

namespace HPHA.UiPath.Core.Azure.DocumentIntelligence
{
    public class Table
    {
        [JsonPropertyName("rowCount")]
        public int? RowCount { get; set; }

        [JsonPropertyName("columnCount")]
        public int? ColumnCount { get; set; }

        [JsonPropertyName("cells")]
        public List<Cell>? Cells { get; set; }

        [JsonPropertyName("boundingRegions")]
        public List<BoundingRegion>? BoundingRegions { get; set; }

        [JsonPropertyName("spans")]
        public List<Span>? Spans { get; set; }
    }
}
