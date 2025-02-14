using System.Text;
using System.Text.Json;
using HPHA.UiPath.Core.Entities.Common;
using S = HPHA.UiPath.Core.Azure.DocumentIntelligence.Simplified;
using D = HPHA.UiPath.Core.Azure.DocumentIntelligence.Detailed;

namespace HPHA.UiPath.Core.Converters
{
    public static class JsonToEntityConverter
    {
        private static JsonSerializerOptions _options = new()
        {
            PropertyNameCaseInsensitive = true
        };

        /// <summary>
        /// Converts a simplified JSON file to a PurchaseOrderEntity.
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static PurchaseOrderEntity ConvertSimplifiedJsonToPurchaseOrderEntity(FileInfo fileInfo)
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
                var invoiceData = JsonSerializer.Deserialize<S.InvoiceData>(jsonContent, _options)
                    ?? throw new InvalidOperationException("Deserialization failed.");

                return ConvertSimplifiedToPurchaseOrderEntity(invoiceData);
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException("Deserialization failed.", ex);
            }
        }

        private static PurchaseOrderEntity ConvertSimplifiedToPurchaseOrderEntity(S.InvoiceData invoiceData)
        {
            DateOnly? invoiceDate = null;
            if (DateOnly.TryParse(invoiceData?.InvoiceDate?.Content, out var parsedDate))
            {
                invoiceDate = parsedDate;
            }

            var items = invoiceData?.Items?.Select(item => new PurchaseOrderItemEntity
            {
                Amount = item.Amount?.ValueCurrency?.Amount,
                Description = item.Description?.ValueString,
                Quantity = item.Quantity?.ValueNumber,
                Tax = item.Tax?.ValueCurrency?.Amount,
                UnitPrice = item.UnitPrice?.ValueCurrency?.Amount
            }).ToArray();

            return new PurchaseOrderEntity
            {
                InvoiceDate = invoiceDate,
                InvoiceId = invoiceData?.InvoiceId?.ValueString,
                InvoiceTotal = invoiceData?.InvoiceTotal?.ValueCurrency?.Amount,
                PurchaseOrder = invoiceData?.PurchaseOrder?.ValueString,
                SubTotal = invoiceData?.SubTotal?.ValueCurrency?.Amount,
                TotalTax = invoiceData?.TotalTax?.ValueCurrency?.Amount,
                Vendor = new() { Name = invoiceData?.VendorName?.ValueString },
                Items = items ?? Array.Empty<PurchaseOrderItemEntity>()
            };
        }

        /// <summary>
        /// Converts a detailed JSON file to a PurchaseOrderEntity.
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static PurchaseOrderEntity ConvertDetailedJsonToPurchaseOrderEntity(FileInfo fileInfo)
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
                var invoiceData = JsonSerializer.Deserialize<D.InvoiceData>(jsonContent, _options)
                    ?? throw new InvalidOperationException("Deserialization failed.");

                return ConvertDetailedToPurchaseOrderEntity(invoiceData);
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException("Deserialization failed.", ex);
            }
        }

        private static PurchaseOrderEntity ConvertDetailedToPurchaseOrderEntity(D.InvoiceData invoiceData)
        {
            var fields = invoiceData?.AnalyzeResult?.Documents?[0].Fields;

            DateOnly? invoiceDate = null;
            if (DateOnly.TryParse(fields?.InvoiceDate?.Content, out var parsedDate))
            {
                invoiceDate = parsedDate;
            }

            var items = fields?.Items?.ValueArray?.Select(item => new PurchaseOrderItemEntity
            {
                Amount = item.ValueObject?.Amount?.ValueCurrency?.Amount,
                Description = item.ValueObject?.Description?.ValueString,
                Quantity = item.ValueObject?.Quantity?.ValueNumber,
                Tax = item.ValueObject?.Tax?.ValueCurrency?.Amount,
                UnitPrice = item.ValueObject?.UnitPrice?.ValueCurrency?.Amount
            }).ToArray();

            return new PurchaseOrderEntity
            {
                InvoiceDate = invoiceDate,
                InvoiceId = fields?.InvoiceId?.ValueString,
                InvoiceTotal = fields?.InvoiceTotal?.ValueCurrency?.Amount,
                PurchaseOrder = fields?.PurchaseOrder?.ValueString,
                SubTotal = fields?.SubTotal?.ValueCurrency?.Amount,
                TotalTax = fields?.TotalTax?.ValueCurrency?.Amount,
                Vendor = new() { Name = fields?.VendorName?.ValueString },
                Items = items ?? Array.Empty<PurchaseOrderItemEntity>()
            };
        }
    }
}
