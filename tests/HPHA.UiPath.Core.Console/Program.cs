// See https://aka.ms/new-console-template for more information
using HPHA.UiPath.Core.Converters;

Console.WriteLine("Hello, World!");

FileInfo fileInfo = new("Json/CFWLstn0m3lw-ZZm_s.json");

var purchaseOrder = JsonToEntityConverter.ConvertSimplifiedJsonToPurchaseOrderEntity(fileInfo);

Console.WriteLine($"Invoice ID: {purchaseOrder.InvoiceId}");
Console.WriteLine($"PO#.......: {purchaseOrder.PurchaseOrder}");