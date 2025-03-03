using HPHA.UiPath.Core.Converters;
using HPHA.UiPath.Core.Entities.Common;
using Shouldly;

namespace HPHA.UiPath.Core.UnitTests.Converters
{
    public class JsonToEntityConverterTest
    {
        [Fact]
        public void ConvertJsonToEntity_ShouldThrowFileNotFoundException_WhenFileDoesNotExist()
        {
            // Arrange
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "nonexistentfile.json");
            var fileInfo = new FileInfo(filePath);

            // Act
            Action act = () => JsonToEntityConverter.ConvertJsonToEntity(fileInfo);

            // Assert
            act.ShouldThrow<FileNotFoundException>("File not found.");
        }

        [Fact]
        public void ConvertJsonToEntity_ShouldThrowInvalidOperationException_WhenJsonIsInvalid()
        {
            // Arrange
            var tempFilePath = Path.GetTempFileName();
            File.WriteAllText(tempFilePath, "invalid json");

            var fileInfo = new FileInfo(tempFilePath);

            // Act
            Action act = () => JsonToEntityConverter.ConvertJsonToEntity(fileInfo);

            // Assert
            act.ShouldThrow<InvalidOperationException>("Deserialization failed.");

            // Cleanup
            File.Delete(tempFilePath);
        }

        [Theory]
        [InlineData("Converters/Json/test1_c.json", "36808192", "257456", "Baxter", 2397.44d, 311.67d, 2709.11d, 2)]
        [InlineData("Converters/Json/test2_c.json", "F627987/E", "256746", "STEVENS", 1294.24d, 168.25d, 1462.49d, 8)]
        public void ConvertCompactedJsonToPurchaseOrderEntity_ShouldReturnCorrectEntity_WhenJsonIsValid(
            string jsonPath
            , string invoiceId
            , string purchaseOrder
            , string vendorName
            , double subTotal
            , double totalTax
            , double invoiceTotal
            , int itemsCount)
        {
            // Arrange
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, jsonPath);
            var fileInfo = new FileInfo(filePath);

            // Act
            PurchaseOrderEntity? result = JsonToEntityConverter.ConvertJsonToEntity(fileInfo);

            // Assert
            result.ShouldNotBeNull();
            result.InvoiceId.ShouldBe(invoiceId);
            result.PurchaseOrder.ShouldBe(purchaseOrder);
            result.Vendor?.Name.ShouldBe(vendorName);
            result.SubTotal.ShouldBe(subTotal);
            result.TotalTax.ShouldBe(totalTax);
            result.InvoiceTotal.ShouldBe(invoiceTotal);
            result.Items.ShouldNotBeNull();
            result.Items.Length.ShouldBe(itemsCount);
        }
    }
}