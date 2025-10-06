using HPHA.UiPath.Core.Azure.DocumentIntelligence;
using HPHA.UiPath.Core.Entities.Common;
using System.Text;
using System.Text.Json;

namespace HPHA.UiPath.Core.Converters
{
    public static class JsonToEntityConverter
    {
        /// <summary>
        /// Converts a compacted JSON file to a PurchaseOrderEntity.
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static PurchaseOrderEntity? ConvertJsonToEntity(FileInfo fileInfo)
        {
            if (fileInfo == null || !fileInfo.Exists)
                throw new FileNotFoundException("File not found.", fileInfo?.FullName);

            string jsonContent;
            using (StreamReader reader = new(fileInfo.FullName, Encoding.UTF8))
            {
                jsonContent = reader.ReadToEnd();
            }

            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            try
            {
                var invoiceData = JsonSerializer.Deserialize<InvoiceData>(jsonContent, options)
                    ?? throw new InvalidOperationException("Deserialization failed.");

                return ConvertJsonToEntity(invoiceData);
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException("Deserialization failed.", ex);
            }
        }

        private static PurchaseOrderEntity? ConvertJsonToEntity(InvoiceData invoiceData)
        {
            if (invoiceData == null)
                throw new ArgumentNullException(nameof(invoiceData));

            Ulid? identifier = null;
            if (Ulid.TryParse(invoiceData.Identifier, out var parsedIdentifier))
                identifier = parsedIdentifier;

            DateOnly? invoiceDate = null;
            if (DateOnly.TryParse(invoiceData?.InvoiceDate, out var parsedInvoiceDate))
                invoiceDate = parsedInvoiceDate;

            var items = invoiceData?.Items?.Select(item => new PurchaseOrderItemEntity
            {
                Quantity = item.Quantity,
                UnitPrice = item.AmountUnit,
                Tax = item.AmountTax,
                Amount = item.Amount
            }).ToArray();

            var entity = new PurchaseOrderEntity
            {
                UniqueId = identifier,
                InvoiceId = invoiceData?.InvoiceNumber,
                InvoiceDate = invoiceDate,
                PurchaseOrder = invoiceData?.PoNumber,
                Vendor = new() { Name = invoiceData?.VendorName },
                Freight = invoiceData?.AmountFreight,
                SubTotal = invoiceData?.AmountUntaxed,
                TotalTax = invoiceData?.AmountTax,
                InvoiceTotal = invoiceData?.Amount,
                AmountDue = invoiceData?.AmountDue,
                HazardousFee = invoiceData?.HazardousFee,
                HandlingFee = invoiceData?.HandlingFee,
                FuelSurcharge = invoiceData?.FuelSurcharge,
                TransportationCharge = invoiceData?.TransportationCharge,
                MinimumOrder = invoiceData?.MinimumOrder,
                Items = items ?? Array.Empty<PurchaseOrderItemEntity>()
            };

            return entity;
        }
    }
}
