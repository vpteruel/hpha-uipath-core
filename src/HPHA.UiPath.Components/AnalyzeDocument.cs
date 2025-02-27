using System.Activities;
using System.ComponentModel;

namespace HPHA.UiPath.Components
{
    /// <summary>
    /// Analyzes a document using the Azure Form Recognizer service.
    /// </summary>
    public class AnalyzeDocument : CodeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        [DisplayName("Endpoint")]
        [Description("The endpoint of the Azure Form Recognizer service.")]
        public InArgument<string> Endpoint { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [DisplayName("Key")]
        [Description("The subscription key for the Azure Form Recognizer service.")]
        public InArgument<string> Key { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [DisplayName("API Version")]
        [Description("The version of the Azure Form Recognizer API to use.")]
        public InArgument<string> ApiVersion { get; set; }

        [Category("Input")]
        [DisplayName("Enable Query Fields")]
        [Description("Whether to enable querying fields in the document.")]
        public InArgument<bool> EnableQueryFields { get; set; }

        [Category("Input")]
        [DisplayName("Query Fields")]
        [Description("The fields to query in the document.")]
        public InArgument<string[]> QueryFields { get; set; }

        [Category("Input")]
        [DisplayName("Enable Key-Value Pairs")]
        [Description("Whether to enable key-value pairs in the document.")]
        public InArgument<bool> KeyValuePairs { get; set; }

        [Category("Input")]
        [DisplayName("Base64 Source")]
        [Description("The base64-encoded source of the document to analyze.")]
        public InArgument<string> Base64Source { get; set; }

        [Category("Output")]
        [DisplayName("Result")]
        [Description("The result of the document analysis.")]
        public OutArgument<string> Result { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var endpoint = Endpoint.Get(context);
            var key = Key.Get(context);
            var apiVersion = ApiVersion.Get(context);
            var enableQueryFields = EnableQueryFields.Get(context);
            var queryFields = QueryFields.Get(context);
            var keyValuePairs = KeyValuePairs.Get(context);
            var base64Source = Base64Source.Get(context);

            //BinaryData bytesSource = new BinaryData(Convert.FromBase64String(base64Source));

            //var uri = new Uri(endpoint);
            //var credential = new AzureKeyCredential(key);
            //var client = new DocumentIntelligenceClient(uri, credential);

            //var options = new AnalyzeDocumentOptions("prebuilt-invoice", bytesSource)
            //{
            //    Locale = "en-US"
            //};

            //if (enableQueryFields && queryFields.Any())
            //{
            //    options.Features.Add(DocumentAnalysisFeature.QueryFields);
            //    foreach (var field in queryFields)
            //        options.QueryFields.Add(field);
            //}

            //if (keyValuePairs)
            //    options.Features.Add(DocumentAnalysisFeature.KeyValuePairs);

            //Operation<AnalyzeResult> operation = client
            //    .AnalyzeDocument(WaitUntil.Completed, options);

            //AnalyzeResult result = operation.Value;

            Result.Set(context, "test");
        }
    }
}
