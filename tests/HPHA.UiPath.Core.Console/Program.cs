using HPHA.UiPath.Core.Converters;

FileInfo fileInfo = new("Json/test1_c.json");

var purchaseOrder = JsonToEntityConverter.ConvertJsonToEntity(fileInfo);

Console.WriteLine($"Invoice ID: {purchaseOrder?.InvoiceId}");
Console.WriteLine($"PO#.......: {purchaseOrder?.PurchaseOrder}");