using System.Text;
using System.Text.Json;
using S = HPHA.UiPath.Core.Azure.DocumentIntelligence.Simplified;
using D = HPHA.UiPath.Core.Azure.DocumentIntelligence.Detailed;

namespace HPHA.UiPath.Core.Converters
{
    public static class InvoiceDataConverter
    {
        /// <summary>
        /// Converts a detailed invoice data to a simplified invoice data.
        /// </summary>
        /// <param name="detailed"></param>
        /// <returns></returns>
        public static S.InvoiceData ConvertDetailedToSimplified(D.InvoiceData detailed)
        {
            var fields = detailed?.AnalyzeResult?.Documents?[0].Fields;
            var items = fields?.Items?.ValueArray?
                .Select(item => item.ValueObject)
                .Where(item => item != null)
                .Cast<D.ValueObject>()
                .ToList();

            return new S.InvoiceData
            {
                InvoiceDate = new()
                {
                    Confidence = fields?.InvoiceDate?.Confidence,
                    Content = fields?.InvoiceDate?.Content,
                    Type = fields?.InvoiceDate?.Type,
                    ValueDate = fields?.InvoiceDate?.ValueDate
                },
                InvoiceId = new()
                {
                    Confidence = fields?.InvoiceId?.Confidence,
                    Content = fields?.InvoiceId?.Content,
                    Type = fields?.InvoiceId?.Type,
                    ValueString = fields?.InvoiceId?.ValueString
                },
                InvoiceTotal = new()
                {
                    Confidence = fields?.InvoiceTotal?.Confidence,
                    Content = fields?.InvoiceTotal?.Content,
                    Type = fields?.InvoiceTotal?.Type,
                    ValueCurrency = new()
                    {
                        Amount = fields?.InvoiceTotal?.ValueCurrency?.Amount,
                        CurrencyCode = fields?.InvoiceTotal?.ValueCurrency?.CurrencyCode,
                        CurrencySymbol = fields?.InvoiceTotal?.ValueCurrency?.CurrencySymbol
                    }
                },
                PurchaseOrder = new()
                {
                    Confidence = fields?.PurchaseOrder?.Confidence,
                    Content = fields?.PurchaseOrder?.Content,
                    Type = fields?.PurchaseOrder?.Type,
                    ValueString = fields?.PurchaseOrder?.ValueString
                },
                SubTotal = new()
                {
                    Confidence = fields?.SubTotal?.Confidence,
                    Content = fields?.SubTotal?.Content,
                    Type = fields?.SubTotal?.Type,
                    ValueCurrency = new()
                    {
                        Amount = fields?.SubTotal?.ValueCurrency?.Amount,
                        CurrencyCode = fields?.SubTotal?.ValueCurrency?.CurrencyCode,
                        CurrencySymbol = fields?.SubTotal?.ValueCurrency?.CurrencySymbol
                    }
                },
                TotalTax = new()
                {
                    Confidence = fields?.TotalTax?.Confidence,
                    Content = fields?.TotalTax?.Content,
                    Type = fields?.TotalTax?.Type,
                    ValueCurrency = new()
                    {
                        Amount = fields?.TotalTax?.ValueCurrency?.Amount,
                        CurrencyCode = fields?.TotalTax?.ValueCurrency?.CurrencyCode,
                        CurrencySymbol = fields?.TotalTax?.ValueCurrency?.CurrencySymbol
                    }
                },
                VendorName = new()
                {
                    Confidence = fields?.VendorName?.Confidence,
                    Content = fields?.VendorName?.Content,
                    Type = fields?.VendorName?.Type,
                    ValueString = fields?.VendorName?.ValueString
                },
                Items = items?.Select(item => new S.ValueObject()
                {
                    Amount = new()
                    {
                        Confidence = item.Amount?.Confidence,
                        Content = item.Amount?.Content,
                        Type = item.Amount?.Type,
                        ValueCurrency = new()
                        {
                            Amount = item.Amount?.ValueCurrency?.Amount,
                            CurrencyCode = item.Amount?.ValueCurrency?.CurrencyCode,
                            CurrencySymbol = item.Amount?.ValueCurrency?.CurrencySymbol
                        }
                    },
                    Date = new()
                    {
                        Confidence = item.Date?.Confidence,
                        Content = item.Date?.Content,
                        Type = item.Date?.Type,
                        ValueDate = item.Date?.ValueDate
                    },
                    Description = new()
                    {
                        Confidence = item.Description?.Confidence,
                        Content = item.Description?.Content,
                        Type = item.Description?.Type,
                        ValueString = item.Description?.ValueString
                    },
                    Quantity = new()
                    {
                        Confidence = item.Quantity?.Confidence,
                        Content = item.Quantity?.Content,
                        Type = item.Quantity?.Type,
                        ValueNumber = item.Quantity?.ValueNumber
                    },
                    ProductCode = new()
                    {
                        Confidence = item.ProductCode?.Confidence,
                        Content = item.ProductCode?.Content,
                        Type = item.ProductCode?.Type,
                        ValueString = item.ProductCode?.ValueString
                    },
                    PurchaseOrder = new()
                    {
                        Confidence = item.PurchaseOrder?.Confidence,
                        Content = item.PurchaseOrder?.Content,
                        Type = item.PurchaseOrder?.Type,
                        ValueString = item.PurchaseOrder?.ValueString
                    },
                    Tax = new()
                    {
                        Confidence = item.Tax?.Confidence,
                        Content = item.Tax?.Content,
                        Type = item.Tax?.Type,
                        ValueCurrency = new()
                        {
                            Amount = item.Tax?.ValueCurrency?.Amount,
                            CurrencyCode = item.Tax?.ValueCurrency?.CurrencyCode,
                            CurrencySymbol = item.Tax?.ValueCurrency?.CurrencySymbol
                        }
                    },
                    UnitPrice = new()
                    {
                        Confidence = item.UnitPrice?.Confidence,
                        Content = item.UnitPrice?.Content,
                        Type = item.UnitPrice?.Type,
                        ValueCurrency = new()
                        {
                            Amount = item.UnitPrice?.ValueCurrency?.Amount,
                            CurrencyCode = item.UnitPrice?.ValueCurrency?.CurrencyCode,
                            CurrencySymbol = item.UnitPrice?.ValueCurrency?.CurrencySymbol
                        }
                    }
                }).ToList()
            };
        }

        /// <summary>
        /// Reads a detailed JSON file and deserializes it into an InvoiceDataDetailed object.
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static D.InvoiceData ReadDetailedJsonFile(FileInfo fileInfo)
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
                var invoiceData = JsonSerializer.Deserialize<D.InvoiceData>(jsonContent)
                    ?? throw new InvalidOperationException("Deserialization failed.");

                return invoiceData;
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
        public static S.InvoiceData ReadAndConvertDetailedJsonToSimplified(FileInfo fileInfo)
        {
            var detailed = ReadDetailedJsonFile(fileInfo);
            return ConvertDetailedToSimplified(detailed);
        }
    }
}