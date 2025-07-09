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

            Ulid? uniqueId = null;
            if (Ulid.TryParse(invoiceData.UniqueId, out var parsedUniqueId))
                uniqueId = parsedUniqueId;

            DateOnly? invoiceDate = null;
            if (DateOnly.TryParse(invoiceData?.InvoiceDate, out var parsedInvoiceDate))
                invoiceDate = parsedInvoiceDate;

            DateOnly? dueDate = null;
            if (DateOnly.TryParse(invoiceData?.DueDate, out var parsedDueDate))
                dueDate = parsedDueDate;

            var items = invoiceData?.Items?.Select(item => new PurchaseOrderItemEntity
            {
                Description = item.Description,
                Quantity = item.Quantity,
                Unit = item.Unit,
                UnitPrice = item.UnitPrice,
                Tax = item.Tax,
                Amount = item.Amount
            }).ToArray();

            var entity = new PurchaseOrderEntity
            {
                ModelId = invoiceData?.ModelId,
                UniqueId = uniqueId,
                InvoiceId = invoiceData?.InvoiceId,
                InvoiceDate = invoiceDate,
                DueDate = dueDate,
                PurchaseOrder = invoiceData?.PurchaseOrder,
                VendorName = invoiceData?.VendorName,
                Freight = invoiceData?.Freight,
                ShippingCredit = invoiceData?.ShippingCredit,
                HazardousFee = invoiceData?.HazardousFee,
                FuelSurcharge = invoiceData?.FuelSurcharge,
                TransportationCharge = invoiceData?.TransportationCharge,
                MinimumOrder = invoiceData?.MinimumOrder,
                SubTotal = invoiceData?.SubTotal,
                TotalTax = invoiceData?.TotalTax,
                InvoiceTotal = invoiceData?.InvoiceTotal,
                AmountDue = invoiceData?.AmountDue,
                Items = items ?? Array.Empty<PurchaseOrderItemEntity>()
            };

            return entity;
        }
    }
}
