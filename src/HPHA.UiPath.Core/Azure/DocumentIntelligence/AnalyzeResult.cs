using System.Text.Json.Serialization;

namespace HPHA.UiPath.Core.Azure.DocumentIntelligence
{
    public class AnalyzeResult
    {
        [JsonPropertyName("apiVersion")]
        public string ApiVersion { get; set; }

        [JsonPropertyName("modelId")]
        public string ModelId { get; set; }

        [JsonPropertyName("stringIndexType")]
        public string StringIndexType { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonPropertyName("pages")]
        public List<Page> Pages { get; set; }

        [JsonPropertyName("tables")]
        public List<Table> Tables { get; set; }

        [JsonPropertyName("styles")]
        public List<object> Styles { get; set; }

        [JsonPropertyName("documents")]
        public List<Document> Documents { get; set; }

        [JsonPropertyName("contentFormat")]
        public string ContentFormat { get; set; }
    }
}
