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
        [InlineData("Converters/Json/baxter_c.json", "36794479", "256049", "BAXTER CORPORATION", -1d, 175.52d, 22.82d, 198.34d, 1)]
        [InlineData("Converters/Json/chs_c.json", "PS-INV394638", "257773", "Canadian Hospital Specialties Limited", 31.26d, 132.54d, 17.23d, 149.77d, 1)]
        public void ConvertCompactedJsonToPurchaseOrderEntity_ShouldReturnCorrectEntity_WhenJsonIsValid(
            string jsonPath
            , string invoiceId
            , string purchaseOrder
            , string vendorName
            , double freight
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
            if (freight > 0d)
                result.Freight.ShouldBe(freight);
            result.SubTotal.ShouldBe(subTotal);
            result.TotalTax.ShouldBe(totalTax);
            result.InvoiceTotal.ShouldBe(invoiceTotal);
            result.Items.ShouldNotBeNull();
            result.Items.Length.ShouldBe(itemsCount);
        }
    }
}