using FluentAssertions;
using HPHA.UiPath.Core.Converters;
using HPHA.UiPath.Core.Entities.Common;

namespace HPHA.UiPath.Core.UnitTests.Converters
{
    public class JsonToEntityConverterTest
    {
        private readonly string _simplifiedJsonPath = "Converters/Json/simplified-invoice-data.json";
        private readonly string _detailedJsonPath = "Converters/Json/detailed-invoice-data.json";

        [Fact]
        public void ConvertSimplifiedJsonToPurchaseOrderEntity_ShouldThrowFileNotFoundException_WhenFileDoesNotExist()
        {
            // Arrange
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "nonexistentfile.json");
            var fileInfo = new FileInfo(filePath);

            // Act
            Action act = () => JsonToEntityConverter.ConvertSimplifiedJsonToPurchaseOrderEntity(fileInfo);

            // Assert
            act.Should().Throw<FileNotFoundException>().WithMessage("File not found.");
        }

        [Fact]
        public void ConvertSimplifiedJsonToPurchaseOrderEntity_ShouldThrowInvalidOperationException_WhenJsonIsInvalid()
        {
            // Arrange
            var tempFilePath = Path.GetTempFileName();
            File.WriteAllText(tempFilePath, "invalid json");

            var fileInfo = new FileInfo(tempFilePath);

            // Act
            Action act = () => JsonToEntityConverter.ConvertSimplifiedJsonToPurchaseOrderEntity(fileInfo);

            // Assert
            act.Should().Throw<InvalidOperationException>().WithMessage("Deserialization failed.");

            // Cleanup
            File.Delete(tempFilePath);
        }

        [Fact]
        public void ConvertSimplifiedJsonToPurchaseOrderEntity_ShouldReturnCorrectEntity_WhenJsonIsValid()
        {
            // Arrange
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _simplifiedJsonPath);
            var fileInfo = new FileInfo(filePath);

            // Act
            PurchaseOrderEntity result = JsonToEntityConverter.ConvertSimplifiedJsonToPurchaseOrderEntity(fileInfo);

            // Assert
            result.InvoiceDate.Should().Be(new DateOnly(2025, 1, 15));
            result.InvoiceId.Should().Be("DEC24-HPHA");
            result.InvoiceTotal.Should().Be(295.77d);
            result.PurchaseOrder.Should().Be("255167");
            result.SubTotal.Should().Be(293.45d);
            result.TotalTax.Should().Be(2.32d);
            result.Vendor?.Name.Should().Be("Culligan\nTM");

            result.Items.Should().HaveCount(11);

            var item1 = result.Items[0];
            item1.Amount.Should().Be(11.44d);
            item1.Description.Should().Be("Rental - Bottled Water Coolers");
            item1.Quantity.Should().Be(1);
            item1.Tax.Should().Be(1.49d);
            item1.UnitPrice.Should().Be(9.95d);
        }

        [Fact]
        public void ConvertDetailedJsonToPurchaseOrderEntity_ShouldThrowFileNotFoundException_WhenFileDoesNotExist()
        {
            // Arrange
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "nonexistentfile.json");
            var fileInfo = new FileInfo(filePath);

            // Act
            Action act = () => JsonToEntityConverter.ConvertDetailedJsonToPurchaseOrderEntity(fileInfo);

            // Assert
            act.Should().Throw<FileNotFoundException>().WithMessage("File not found.");
        }

        [Fact]
        public void ConvertDetailedJsonToPurchaseOrderEntity_ShouldThrowInvalidOperationException_WhenJsonIsInvalid()
        {
            // Arrange
            var tempFilePath = Path.GetTempFileName();
            File.WriteAllText(tempFilePath, "invalid json");

            var fileInfo = new FileInfo(tempFilePath);

            // Act
            Action act = () => JsonToEntityConverter.ConvertDetailedJsonToPurchaseOrderEntity(fileInfo);

            // Assert
            act.Should().Throw<InvalidOperationException>().WithMessage("Deserialization failed.");

            // Cleanup
            File.Delete(tempFilePath);
        }

        [Fact]
        public void ConvertDetailedJsonToPurchaseOrderEntity_ShouldReturnCorrectEntity_WhenJsonIsValid()
        {
            // Arrange
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _detailedJsonPath);
            var fileInfo = new FileInfo(filePath);

            // Act
            PurchaseOrderEntity result = JsonToEntityConverter.ConvertDetailedJsonToPurchaseOrderEntity(fileInfo);

            // Assert
            result.InvoiceDate.Should().Be(new DateOnly(2025, 1, 15));
            result.InvoiceId.Should().Be("DEC24-HPHA");
            result.InvoiceTotal.Should().Be(295.77d);
            result.PurchaseOrder.Should().Be("255167");
            result.SubTotal.Should().Be(293.45d);
            result.TotalTax.Should().Be(2.32d);
            result.Vendor?.Name.Should().Be("Culligan\nTM");

            result.Items.Should().HaveCount(11);

            var item1 = result.Items[0];
            item1.Amount.Should().Be(11.44d);
            item1.Description.Should().Be("Rental - Bottled Water Coolers");
            item1.Quantity.Should().Be(1);
            item1.Tax.Should().Be(1.49d);
            item1.UnitPrice.Should().Be(9.95d);
        }
    }
}