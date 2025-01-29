using System.Text.Json.Serialization;

namespace HPHA.UiPath.Core.Azure.DocumentIntelligence
{
    public class BoundingRegion
    {
        [JsonPropertyName("pageNumber")]
        public int? PageNumber { get; set;}

        [JsonPropertyName("polygon")]
        public List<double?>? Polygon { get; set; }
    }
}
