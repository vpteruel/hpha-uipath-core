using System.Data;
using HPHA.UiPath.Core.Converters;
using HPHA.UiPath.Core.Entities.Common;
using Shouldly;

namespace HPHA.UiPath.Core.UnitTests.Converters
{
    public class DataTableToEntityConverterTest
    {
        [Fact]
        public void ConvertToPurchaseOrderEntities_ShouldReturnCorrectEntities()
        {
            // Arrange
            DataTable table = new();
            table.Columns.Add("PO #", typeof(string));
            table.Columns.Add("Vendor #", typeof(string));
            table.Columns.Add("Vendor Mnemonic", typeof(string));
            table.Columns.Add("Vendor Name", typeof(string));
            table.Columns.Add("PO Line", typeof(string));
            table.Columns.Add("Item #", typeof(string));
            table.Columns.Add("Item Name", typeof(string));
            table.Columns.Add("Item Description", typeof(string));
            table.Columns.Add("Item Quantity", typeof(string));
            table.Columns.Add("Item Cost UP", typeof(string));

            table.Rows.Add("PO123", "V001", "VM001", "Vendor 1", "1", "Item001", "Item Name 1", "Item Description 1", "10", "100.50");
            table.Rows.Add("PO123", "V001", "VM001", "Vendor 1", "2", "Item002", "Item Name 2", "Item Description 2", "20", "200.75");
            table.Rows.Add("PO124", "V002", "VM002", "Vendor 2", "1", "Item003", "Item Name 3", "Item Description 3", "30", "300.00");

            // Act
            PurchaseOrderEntity[] result = DataTableToEntityConverter.ConvertToPurchaseOrderEntities(table);

            // Assert
            result.Count().ShouldBe(2);

            var po123 = result.First(po => po.InvoiceId == "PO123");
            po123.InvoiceNumber.ShouldBe("PO123");
            po123.Vendor?.Number.ShouldBe("V001");
            po123.Vendor?.Mnemonic.ShouldBe("VM001");
            po123.Vendor?.Name.ShouldBe("Vendor 1");
            po123.Items.ShouldNotBeNull();
            po123.Items.Length.ShouldBe(2);
            po123.Items[0].PoLine.ShouldBe(1);
            po123.Items[0].Number.ShouldBe("Item001");
            po123.Items[0].Name.ShouldBe("Item Name 1");
            po123.Items[0].Description.ShouldBe("Item Description 1");
            po123.Items[0].Quantity.ShouldBe(10);
            po123.Items[0].UnitPrice.ShouldBe(100.50d);
            po123.Items[1].PoLine.ShouldBe(2);
            po123.Items[1].Number.ShouldBe("Item002");
            po123.Items[1].Name.ShouldBe("Item Name 2");
            po123.Items[1].Description.ShouldBe("Item Description 2");
            po123.Items[1].Quantity.ShouldBe(20);
            po123.Items[1].UnitPrice.ShouldBe(200.75d);

            var po124 = result.First(po => po.InvoiceId == "PO124");
            po124.InvoiceNumber.ShouldBe("PO124");
            po124.Vendor?.Number.ShouldBe("V002");
            po124.Vendor?.Mnemonic.ShouldBe("VM002");
            po124.Vendor?.Name.ShouldBe("Vendor 2");
            po124.Items.ShouldNotBeNull();
            po124.Items.Length.ShouldBe(1);
            po124.Items[0].PoLine.ShouldBe(1);
            po124.Items[0].Number.ShouldBe("Item003");
            po124.Items[0].Name.ShouldBe("Item Name 3");
            po124.Items[0].Description.ShouldBe("Item Description 3");
            po124.Items[0].Quantity.ShouldBe(30);
            po124.Items[0].UnitPrice.ShouldBe(300.00d);
        }
    }
}