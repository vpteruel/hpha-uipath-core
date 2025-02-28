using HPHA.UiPath.Core.Converters;
using HPHA.UiPath.Core.Entities.Common;
using Shouldly;

namespace HPHA.UiPath.Core.UnitTests.Converters
{
    public class JsonToEntityConverterTest
    {
        private readonly string _detailedJsonPath = "Converters/Json/01JJVQBRGKR77SZCFBYND9NB0V_d.json";
        private readonly string _minifiedJsonPath = "Converters/Json/01JJVQBRGKR77SZCFBYND9NB0V_m.json";
        private readonly string _simplifiedJsonPath = "Converters/Json/01JJVQBRGKR77SZCFBYND9NB0V_s.json";

        [Fact]
        public void ConvertDetailedJsonToPurchaseOrderEntity_ShouldThrowFileNotFoundException_WhenFileDoesNotExist()
        {
            // Arrange
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "nonexistentfile.json");
            var fileInfo = new FileInfo(filePath);

            // Act
            Action act = () => JsonToEntityConverter.ConvertDetailedJsonToPurchaseOrderEntity(fileInfo);

            // Assert
            act.ShouldThrow<FileNotFoundException>("File not found.");
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
            act.ShouldThrow<InvalidOperationException>("Deserialization failed.");

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
            result.InvoiceDate.ShouldBe(new DateOnly(2025, 1, 15));
            result.InvoiceId.ShouldBe("DEC24-HPHA");
            result.InvoiceTotal.ShouldBe(295.77d);
            result.PurchaseOrder.ShouldBe("255167");
            result.SubTotal.ShouldBe(293.45d);
            result.TotalTax.ShouldBe(2.32d);
            result.Vendor?.Name.ShouldBe("Culligan\nTM");
            result.Items.ShouldNotBeNull();
            result.Items.Length.ShouldBe(11);

            var item1 = result.Items[0];
            item1.Amount.ShouldBe(11.44d);
            item1.Description.ShouldBe("Rental - Bottled Water Coolers");
            item1.Quantity.ShouldBe(1);
            item1.Tax.ShouldBe(1.49d);
            item1.UnitPrice.ShouldBe(9.95d);
        }

        [Fact]
        public void ConvertMinifiedJsonToPurchaseOrderEntity_ShouldThrowFileNotFoundException_WhenFileDoesNotExist()
        {
            // Arrange
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "nonexistentfile.json");
            var fileInfo = new FileInfo(filePath);

            // Act
            Action act = () => JsonToEntityConverter.ConvertMinifiedJsonToPurchaseOrderEntity(fileInfo);

            // Assert
            act.ShouldThrow<FileNotFoundException>("File not found.");
        }

        [Fact]
        public void ConvertMinifiedJsonToPurchaseOrderEntity_ShouldThrowInvalidOperationException_WhenJsonIsInvalid()
        {
            // Arrange
            var tempFilePath = Path.GetTempFileName();
            File.WriteAllText(tempFilePath, "invalid json");

            var fileInfo = new FileInfo(tempFilePath);

            // Act
            Action act = () => JsonToEntityConverter.ConvertMinifiedJsonToPurchaseOrderEntity(fileInfo);

            // Assert
            act.ShouldThrow<InvalidOperationException>("Deserialization failed.");

            // Cleanup
            File.Delete(tempFilePath);
        }

        [Fact]
        public void ConvertMinifiedJsonToPurchaseOrderEntity_ShouldReturnCorrectEntity_WhenJsonIsValid()
        {
            // Arrange
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _minifiedJsonPath);
            var fileInfo = new FileInfo(filePath);

            // Act
            PurchaseOrderEntity result = JsonToEntityConverter.ConvertMinifiedJsonToPurchaseOrderEntity(fileInfo);

            // Assert
            result.InvoiceDate.ShouldBe(new DateOnly(2025, 1, 15));
            result.InvoiceId.ShouldBe("PS-INV379258");
            result.InvoiceTotal.ShouldBe(2131.91d);
            result.PurchaseOrder.ShouldBe("255558");
            result.SubTotal.ShouldBe(1896.02d);
            result.TotalTax.ShouldBe(235.89d);
            result.Vendor?.Name.ShouldBe("chs\u00ae\n\u00ae");
            result.Items.ShouldNotBeNull();
            result.Items.Length.ShouldBe(3);

            var item1 = result.Items[0];
            item1.Amount.ShouldBe(81.52d);
            item1.Description.ShouldBe("ADULT MASK, O2 ELONGATED M\nCONC. W 7' KINK RESISTANT TUBING,\n50/CS\nLOT/SN: 2408012344 (2)");
            item1.Quantity.ShouldBe(2.0d);
            item1.Tax.ShouldBeNull();
            item1.UnitPrice.ShouldBe(40.76d);
        }

        [Fact]
        public void ConvertSimplifiedJsonToPurchaseOrderEntity_ShouldThrowFileNotFoundException_WhenFileDoesNotExist()
        {
            // Arrange
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "nonexistentfile.json");
            var fileInfo = new FileInfo(filePath);

            // Act
            Action act = () => JsonToEntityConverter.ConvertSimplifiedJsonToPurchaseOrderEntity(fileInfo);

            // Assert
            act.ShouldThrow<FileNotFoundException>("File not found.");
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
            act.ShouldThrow<InvalidOperationException>("Deserialization failed.");

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
            result.InvoiceDate.ShouldBe(new DateOnly(2025, 1, 15));
            result.InvoiceId.ShouldBe("DEC24-HPHA");
            result.InvoiceTotal.ShouldBe(295.77d);
            result.PurchaseOrder.ShouldBe("255167");
            result.SubTotal.ShouldBe(293.45d);
            result.TotalTax.ShouldBe(2.32d);
            result.Vendor?.Name.ShouldBe("Culligan\nTM");
            result.Items.ShouldNotBeNull();
            result.Items.Length.ShouldBe(11);

            var item1 = result.Items[0];
            item1.Amount.ShouldBe(11.44d);
            item1.Description.ShouldBe("Rental - Bottled Water Coolers");
            item1.Quantity.ShouldBe(1);
            item1.Tax.ShouldBe(1.49d);
            item1.UnitPrice.ShouldBe(9.95d);
        }
    }
}