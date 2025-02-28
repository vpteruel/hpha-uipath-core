using System.Text;
using System.Text.Json;
using D = HPHA.UiPath.Core.Azure.DocumentIntelligence.Detailed;
using M = HPHA.UiPath.Core.Azure.DocumentIntelligence.Minified;
using S = HPHA.UiPath.Core.Azure.DocumentIntelligence.Simplified;

namespace HPHA.UiPath.Core.Converters
{
    public static class InvoiceDataConverter
    {
        /// <summary>
        /// Converts a detailed invoice data to a minified invoice data.
        /// </summary>
        /// <param name="detailed"></param>
        /// <returns></returns>
        public static M.InvoiceData ConvertDetailedToMinified(D.InvoiceData detailed)
        {
            var fields = detailed?.AnalyzeResult?.Documents?[0].Fields;
            var items = fields?.Items?.ValueArray?
                .Select(item => item.ValueObject)
                .Where(item => item != null)
                .Cast<D.ValueObject>()
                .ToList();

            return new M.InvoiceData
            {
                InvoiceDate = new M.StringValue
                {
                    Content = fields?.InvoiceDate?.Content,
                    Confidence = fields?.InvoiceDate?.Confidence,
                },
                InvoiceId = new M.StringValue
                {
                    Content = fields?.InvoiceId?.Content,
                    Confidence = fields?.InvoiceId?.Confidence,
                },
                InvoiceTotal = new M.DoubleValue
                {
                    Content = fields?.InvoiceTotal?.ValueCurrency?.Amount,
                    Confidence = fields?.InvoiceTotal?.Confidence,
                },
                PurchaseOrder = GetPurchaseOrderMinified(fields),
                SubTotal = new M.DoubleValue
                {
                    Content = fields?.SubTotal?.ValueCurrency?.Amount,
                    Confidence = fields?.SubTotal?.Confidence
                },
                TotalTax = new M.DoubleValue
                {
                    Content = fields?.TotalTax?.ValueCurrency?.Amount,
                    Confidence = fields?.TotalTax?.Confidence
                },
                VendorName = new M.StringValue
                {
                    Content = fields?.VendorName?.Content,
                    Confidence = fields?.VendorName?.Confidence
                },
                Items = items?.Select(item => new M.Item()
                {
                    Quantity = new M.IntValue
                    {
                        Content = item.Quantity?.ValueNumber,
                        Confidence = item.Quantity?.Confidence
                    },
                    Unit = null,
                    UnitPrice = new M.DoubleValue
                    {
                        Content = item.UnitPrice?.ValueCurrency?.Amount,
                        Confidence = item.UnitPrice?.Confidence
                    },
                    Description = new M.StringValue
                    {
                        Content = item.Description?.Content,
                        Confidence = item.Description?.Confidence
                    },
                    Tax = new M.DoubleValue
                    {
                        Content = item.Tax?.ValueCurrency?.Amount,
                        Confidence = item.Tax?.Confidence
                    },
                    Amount = new M.DoubleValue
                    {
                        Content = item.Amount?.ValueCurrency?.Amount,
                        Confidence = item.Amount?.Confidence
                    },
                }).ToList()
            };
        }

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
                PurchaseOrder = GetPurchaseOrderSimplified(fields),
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

        private static M.StringValue GetPurchaseOrderMinified(D.Fields? fields)
        {
            var purchaseOrder = new M.StringValue
            {
                Content = fields?.PurchaseOrder?.Content,
                Confidence = fields?.PurchaseOrder?.Confidence ?? 0
            };

            if (fields?.CustomerReference?.Confidence > purchaseOrder.Confidence)
            {
                purchaseOrder = new()
                {
                    Confidence = fields?.CustomerReference?.Confidence,
                    Content = fields?.CustomerReference?.Content
                };
            }

            if (fields?.YourOrderNumber?.Confidence > purchaseOrder.Confidence)
            {
                purchaseOrder = new()
                {
                    Confidence = fields?.YourOrderNumber?.Confidence,
                    Content = fields?.YourOrderNumber?.Content
                };
            }

            return purchaseOrder;
        }

        private static S.PurchaseOrder GetPurchaseOrderSimplified(D.Fields? fields)
        {
            var purchaseOrder = new S.PurchaseOrder
            {
                Confidence = fields?.PurchaseOrder?.Confidence ?? 0,
                Content = fields?.PurchaseOrder?.Content,
                Type = fields?.PurchaseOrder?.Type,
                ValueString = fields?.PurchaseOrder?.ValueString
            };

            if (fields?.CustomerReference?.Confidence > purchaseOrder.Confidence)
            {
                purchaseOrder = new()
                {
                    Confidence = fields?.CustomerReference?.Confidence,
                    Content = fields?.CustomerReference?.Content,
                    Type = fields?.CustomerReference?.Type,
                    ValueString = fields?.CustomerReference?.ValueString
                };
            }

            if (fields?.YourOrderNumber?.Confidence > purchaseOrder.Confidence)
            {
                purchaseOrder = new()
                {
                    Confidence = fields?.YourOrderNumber?.Confidence,
                    Content = fields?.YourOrderNumber?.Content,
                    Type = fields?.YourOrderNumber?.Type,
                    ValueString = fields?.YourOrderNumber?.ValueString
                };
            }

            return purchaseOrder;
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
        /// Reads a detailed JSON file and converts it to a minified invoice data.
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        public static M.InvoiceData ReadAndConvertDetailedJsonToMinified(FileInfo fileInfo)
        {
            var detailed = ReadDetailedJsonFile(fileInfo);
            return ConvertDetailedToMinified(detailed);
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