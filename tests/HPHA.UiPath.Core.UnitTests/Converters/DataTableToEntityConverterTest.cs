using System.Data;
using HPHA.UiPath.Core.Converters;
using HPHA.UiPath.Core.Entities.Common;
using FluentAssertions;

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
            result.Should().HaveCount(2);

            var po123 = result.First(po => po.Number == "PO123");
            po123.Vendor?.Number.Should().Be("V001");
            po123.Vendor?.Mnemonic.Should().Be("VM001");
            po123.Vendor?.Name.Should().Be("Vendor 1");
            po123.Items.Should().HaveCount(2);
            po123.Items[0].PoLine.Should().Be(1);
            po123.Items[0].Number.Should().Be("Item001");
            po123.Items[0].Name.Should().Be("Item Name 1");
            po123.Items[0].Description.Should().Be("Item Description 1");
            po123.Items[0].Quantity.Should().Be(10);
            po123.Items[0].UnitPrice.Should().Be(100.50d);
            po123.Items[1].PoLine.Should().Be(2);
            po123.Items[1].Number.Should().Be("Item002");
            po123.Items[1].Name.Should().Be("Item Name 2");
            po123.Items[1].Description.Should().Be("Item Description 2");
            po123.Items[1].Quantity.Should().Be(20);
            po123.Items[1].UnitPrice.Should().Be(200.75d);

            var po124 = result.First(po => po.Number == "PO124");
            po124.Vendor?.Number.Should().Be("V002");
            po124.Vendor?.Mnemonic.Should().Be("VM002");
            po124.Vendor?.Name.Should().Be("Vendor 2");
            po124.Items.Should().HaveCount(1);
            po124.Items[0].PoLine.Should().Be(1);
            po124.Items[0].Number.Should().Be("Item003");
            po124.Items[0].Name.Should().Be("Item Name 3");
            po124.Items[0].Description.Should().Be("Item Description 3");
            po124.Items[0].Quantity.Should().Be(30);
            po124.Items[0].UnitPrice.Should().Be(300.00d);
        }
    }
}