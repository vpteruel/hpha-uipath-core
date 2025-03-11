using HPHA.UiPath.Core.Entities.Common;
using System.Data;

namespace HPHA.UiPath.Core.Converters
{
    public static class DataTableToEntityConverter
    {
        /// <summary>
        /// Converts a DataTable to an array of PurchaseOrderEntity.
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static PurchaseOrderEntity[] ConvertToPurchaseOrderEntities(DataTable table)
        {
            var purchaseOrders = table.AsEnumerable()
                .GroupBy(row => row.Field<string>("PO #"))
                .Select(poGroup => new PurchaseOrderEntity
                {
                    PurchaseOrder = poGroup.Key,
                    Vendor = new PurchaseOrderVendorEntity
                    {
                        Number = poGroup.First().Field<string>("Vendor #"),
                        Mnemonic = poGroup.First().Field<string>("Vendor Mnemonic"),
                        Name = poGroup.First().Field<string>("Vendor Name")
                    },
                    Items = poGroup.Select(row => new PurchaseOrderItemEntity
                    {
                        PoLine = Convert.ToInt32(!string.IsNullOrWhiteSpace(row.Field<string?>("PO Line")) ? row.Field<string?>("PO Line") : "0"),
                        Number = row.Field<string>("Item #"),
                        Name = row.Field<string>("Item Name"),
                        Description = row.Field<string>("Item Description"),
                        Quantity = Convert.ToInt32(!string.IsNullOrWhiteSpace(row.Field<string?>("Item Quantity")) ? row.Field<string?>("Item Quantity") : "1"),
                        UnitPrice = Convert.ToDouble(!string.IsNullOrWhiteSpace(row.Field<string?>("Item Cost UP")) ? row.Field<string?>("Item Cost UP") : "0.00")
                    }).ToArray()
                })
                .OrderBy(row => row.PurchaseOrder);

            // Increment PoLine only when it's 0
            purchaseOrders
                .SelectMany(po => po.Items!) // Flatten the list of items
                .Where(item => item.PoLine == 0) // Filter items with PoLine == 0
                .ToList() // Convert to a list to apply changes
                .ForEach(item => item.PoLine++); // Increment the PoLine for each item

            return purchaseOrders.ToArray();
        }
    }
}
