using HPHA.UiPath.Core.Azure.DocumentIntelligence;
using HPHA.UiPath.Core.Entities.Common;
using System.Text;
using System.Text.Json;

namespace HPHA.UiPath.Core.Converters
{
    public static class JsonToEntityConverter
    {
        private const double CONFIDENCE_RATE = 0.500d;

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

            DateOnly? invoiceDate = null;
            if (DateOnly.TryParse(invoiceData?.InvoiceDate?.Content, out var parsedDate))
            {
                invoiceDate = parsedDate;
            }

            DateOnly? dueDate = null;
            if (DateOnly.TryParse(invoiceData?.DueDate?.Content, out var parsedDueDate))
            {
                dueDate = parsedDueDate;
            }

            var items = invoiceData?.Items?.Select(item => new PurchaseOrderItemEntity
            {
                Description = item.Description?.Content,
                Quantity = item.Quantity?.Content,
                UnitPrice = item.UnitPrice?.Content,
                Tax = item.Tax?.Content,
                Amount = item.Amount?.Content
            }).ToArray();

            var entity = new PurchaseOrderEntity
            {
                InvoiceId = invoiceData?.InvoiceId?.Content,
                InvoiceDate = invoiceDate,
                DueDate = dueDate,
                PurchaseOrder = invoiceData?.PurchaseOrder?.Content,
                Vendor = new() { Name = invoiceData?.VendorName?.Content },
                Freight = invoiceData?.Freight?.Content,
                SubTotal = invoiceData?.SubTotal?.Content,
                TotalTax = invoiceData?.TotalTax?.Content,
                InvoiceTotal = invoiceData?.InvoiceTotal?.Content,
                Items = items ?? Array.Empty<PurchaseOrderItemEntity>()
            };

            SetTotals(entity, invoiceData!);

            return entity;
        }

        private static void SetTotals(PurchaseOrderEntity entity, InvoiceData invoiceData)
        {
            var subTotal = invoiceData!.SubTotal ?? null;
            var totalTax = invoiceData!.TotalTax ?? null;
            var invoiceTotal = invoiceData!.InvoiceTotal ?? null;
            var amountDue = invoiceData!.AmountDue ?? null;

            if (subTotal is null && totalTax != null && invoiceTotal != null && amountDue != null
                && amountDue?.Content > invoiceTotal?.Content
                && (totalTax?.Content + invoiceTotal?.Content) == amountDue?.Content)
            {
                subTotal = invoiceTotal;
                subTotal!.Confidence = CONFIDENCE_RATE;
                invoiceTotal = amountDue;
                invoiceTotal!.Confidence = CONFIDENCE_RATE;
            }

            entity.SubTotal = subTotal?.Content;
            entity.TotalTax = totalTax?.Content;
            entity.InvoiceTotal = invoiceTotal?.Content;
        }
    }
}
