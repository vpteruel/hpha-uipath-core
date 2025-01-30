using System.Text.Json.Serialization;

namespace HPHA.UiPath.Core.Azure.DocumentIntelligence.Detailed
{
    public class InvoiceData
    {
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("createdDateTime")]
        public DateTime? CreatedDateTime { get; set; }

        [JsonPropertyName("lastUpdatedDateTime")]
        public DateTime? LastUpdatedDateTime { get; set; }

        [JsonPropertyName("analyzeResult")]
        public AnalyzeResult? AnalyzeResult { get; set; }
    }
}
