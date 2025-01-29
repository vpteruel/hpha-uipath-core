using System.Text;
using System.Text.Json;
using HPHA.UiPath.Core.Azure.DocumentIntelligence;

namespace HPHA.UiPath.Core.Converters
{
    public static class InvoiceDataConverter
    {
        /// <summary>
        /// Converts a detailed invoice data to a simplified invoice data.
        /// </summary>
        /// <param name="detailed"></param>
        /// <returns></returns>
        public static InvoiceDataSimplified ConvertDetailedToSimplified(InvoiceDataDetailed detailed)
        {
            var fields = detailed?.AnalyzeResult?.Documents?[0].Fields;
            var items = fields?.Items?.ValueArray?
                .Select(item => item.ValueObject)
                .Where(item => item != null)
                .Cast<ValueObject>()
                .ToList();

            return new InvoiceDataSimplified
            {
                InvoiceDate = fields?.InvoiceDate,
                InvoiceId = fields?.InvoiceId,
                InvoiceTotal = fields?.InvoiceTotal,
                PurchaseOrder = fields?.PurchaseOrder,
                SubTotal = fields?.SubTotal,
                TotalTax = fields?.TotalTax,
                VendorName = fields?.VendorName,
                Items = items
            };
        }

        /// <summary>
        /// Reads a detailed JSON file and deserializes it into an InvoiceDataDetailed object.
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static InvoiceDataDetailed ReadDetailedJsonFile(FileInfo fileInfo)
        {
            if (fileInfo == null || !fileInfo.Exists)
                throw new FileNotFoundException("File not found.", fileInfo?.FullName);

            string jsonContent;
            using (StreamReader reader = new(fileInfo.FullName, Encoding.UTF8))
            {
                jsonContent = reader.ReadToEnd();
            }

            try
            {
                var invoiceDataDetailed = JsonSerializer.Deserialize<InvoiceDataDetailed>(jsonContent)
                    ?? throw new InvalidOperationException("Deserialization failed.");

                return invoiceDataDetailed;
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException("Deserialization failed.", ex);
            }
        }

        /// <summary>
        /// Reads a detailed JSON file and converts it to a simplified invoice data.
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        public static InvoiceDataSimplified ReadAndConvertDetailedJsonToSimplified(FileInfo fileInfo)
        {
            var detailed = ReadDetailedJsonFile(fileInfo);
            return ConvertDetailedToSimplified(detailed);
        }
    }
}