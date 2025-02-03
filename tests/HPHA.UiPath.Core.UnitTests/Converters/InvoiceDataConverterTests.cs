using FluentAssertions;
using HPHA.UiPath.Core.Converters;

namespace HPHA.UiPath.Core.UnitTests.Converters
{
    public class InvoiceDataConverterTests
    {
        [Theory]
        [InlineData("Converters/Json/01JJVQBRGKR77SZCFBYND9NB0V_d.json", "255167")]
        [InlineData("Converters/Json/AAMkAGQ2MDlhMTdhLTMzNGItNDk1Ny1h_d.json", "256494")]
        [InlineData("Converters/Json/sj9SScj5Clox6_m7_d.json", "256746")]
        public void ConvertDetailedToSimplified_ShouldConvertCorrectly(string detailedJsonPath, string purchaseOrder)
        {
            // Arrange
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, detailedJsonPath);
            var fileInfo = new FileInfo(filePath);
            
            var detailed = InvoiceDataConverter.ReadDetailedJsonFile(fileInfo);
            var fields = detailed?.AnalyzeResult?.Documents?[0].Fields;

            // Act
            var simplified = InvoiceDataConverter.ConvertDetailedToSimplified(detailed);

            // Assert
            simplified.Should().NotBeNull();
            simplified.InvoiceDate?.ValueDate.Should().Be(fields?.InvoiceDate?.ValueDate);
            simplified.InvoiceId?.ValueString.Should().Be(fields?.InvoiceId?.ValueString);
            simplified.InvoiceTotal?.ValueCurrency?.Amount.Should().Be(fields?.InvoiceTotal?.ValueCurrency?.Amount);
            simplified.PurchaseOrder?.ValueString.Should().Be(purchaseOrder);
            simplified.SubTotal?.ValueCurrency?.Amount.Should().Be(fields?.SubTotal?.ValueCurrency?.Amount);
            simplified.TotalTax?.ValueCurrency?.Amount.Should().Be(fields?.TotalTax?.ValueCurrency?.Amount);
            simplified.VendorName?.ValueString.Should().Be(fields?.VendorName?.ValueString);
            simplified.Items.Should().HaveCount(fields?.Items?.ValueArray?.Count ?? 0);
        }

        [Theory]
        [InlineData("Converters/Json/sj9SScj5Clox6_m7_d.json", "256746")]
        public void ConvertYourOrderNumberToPurchaseNumber_ShouldConvertCorrectly(string detailedJsonPath, string purchaseOrder)
        {
            // Arrange
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, detailedJsonPath);
            var fileInfo = new FileInfo(filePath);

            // Act
            var simplified = InvoiceDataConverter.ReadAndConvertDetailedJsonToSimplified(fileInfo);

            // Assert
            simplified.Should().NotBeNull();
            simplified.PurchaseOrder?.ValueString.Should().Be(purchaseOrder);
        }

        [Theory]
        [InlineData("Converters/Json/AAMkAGQ2MDlhMTdhLTMzNGItNDk1Ny1h_d.json", "619649554", "256494", 6)]
        [InlineData("Converters/Json/01JJVQBRGKR77SZCFBYND9NB0V_d.json", "DEC24-HPHA", "255167", 11)]
        public void ReadDetailedJsonFile_ShouldReadAndDeserializeCorrectly(
            string detailedJsonPath
            , string invoiceId
            , string purchaseOrder
            , int itemsCount)
        {
            // Arrange
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, detailedJsonPath);
            var fileInfo = new FileInfo(filePath);

            // Act
            var detailed = InvoiceDataConverter.ReadDetailedJsonFile(fileInfo);

            // Assert
            detailed.Should().NotBeNull();
            detailed.Status.Should().Be("succeeded");
            detailed.AnalyzeResult.Should().NotBeNull();
            detailed.AnalyzeResult.Documents.Should().HaveCount(1);
            detailed.AnalyzeResult.Documents[0].Fields?.InvoiceId?.ValueString.Should().Be(invoiceId);
            detailed.AnalyzeResult.Documents[0].Fields?.PurchaseOrder?.ValueString.Should().Be(purchaseOrder);
            detailed.AnalyzeResult.Documents[0].Fields?.Items?.ValueArray.Should().HaveCount(itemsCount);
        }

        [Theory]
        [InlineData("Converters/Json/AAMkAGQ2MDlhMTdhLTMzNGItNDk1Ny1h_d.json", "619649554", "256494", 6)]
        [InlineData("Converters/Json/01JJVQBRGKR77SZCFBYND9NB0V_d.json", "DEC24-HPHA", "255167", 11)]
        public void ReadAndConvertDetailedJsonToSimplified_ShouldConvertCorrectly(
            string detailedJsonPath
            , string invoiceId
            , string purchaseOrder
            , int itemsCount)
        {
            // Arrange
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, detailedJsonPath);
            var fileInfo = new FileInfo(filePath);

            // Act
            var simplified = InvoiceDataConverter.ReadAndConvertDetailedJsonToSimplified(fileInfo);

            // Assert
            simplified.Should().NotBeNull();
            simplified.InvoiceId?.ValueString.Should().Be(invoiceId);
            simplified.PurchaseOrder?.ValueString.Should().Be(purchaseOrder);
            simplified.Items.Should().HaveCount(itemsCount);
        }
    }
}