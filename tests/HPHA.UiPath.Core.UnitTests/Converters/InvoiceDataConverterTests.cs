using HPHA.UiPath.Core.Converters;
using Shouldly;
using D = HPHA.UiPath.Core.Azure.DocumentIntelligence.Detailed;
using M = HPHA.UiPath.Core.Azure.DocumentIntelligence.Minified;
using S = HPHA.UiPath.Core.Azure.DocumentIntelligence.Simplified;

namespace HPHA.UiPath.Core.UnitTests.Converters
{
    public class InvoiceDataConverterTests
    {
        [Fact]
        public void ConvertDetailedToSimplified_ShouldReturnNull_WhenDetailedInvoiceDataIsNull()
        {
            // Act
            var simplifiedInvoiceData = InvoiceDataConverter.ConvertDetailedToSimplified(null);

            // Assert
            Assert.Null(simplifiedInvoiceData);
        }
        
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
            var simplified = InvoiceDataConverter.ConvertDetailedToSimplified(detailed!);

            // Assert
            simplified.ShouldNotBeNull();
            simplified.InvoiceDate?.Confidence.ShouldBe(fields?.InvoiceDate?.Confidence);
            simplified.InvoiceDate?.Content.ShouldBe(fields?.InvoiceDate?.Content);
            simplified.InvoiceDate?.Type.ShouldBe(fields?.InvoiceDate?.Type);
            simplified.InvoiceDate?.ValueDate.ShouldBe(fields?.InvoiceDate?.ValueDate);
            simplified.InvoiceId?.Confidence.ShouldBe(fields?.InvoiceId?.Confidence);
            simplified.InvoiceId?.Content.ShouldBe(fields?.InvoiceId?.Content);
            simplified.InvoiceId?.Type.ShouldBe(fields?.InvoiceId?.Type);
            simplified.InvoiceId?.ValueString.ShouldBe(fields?.InvoiceId?.ValueString);
            simplified.InvoiceTotal?.Confidence.ShouldBe(fields?.InvoiceTotal?.Confidence);
            simplified.InvoiceTotal?.Content.ShouldBe(fields?.InvoiceTotal?.Content);
            simplified.InvoiceTotal?.Type.ShouldBe(fields?.InvoiceTotal?.Type);
            simplified.InvoiceTotal?.ValueCurrency?.Amount.ShouldBe(fields?.InvoiceTotal?.ValueCurrency?.Amount);
            simplified.InvoiceTotal?.ValueCurrency?.CurrencyCode.ShouldBe(fields?.InvoiceTotal?.ValueCurrency?.CurrencyCode);
            simplified.InvoiceTotal?.ValueCurrency?.CurrencySymbol.ShouldBe(fields?.InvoiceTotal?.ValueCurrency?.CurrencySymbol);
            simplified.PurchaseOrder?.ValueString.ShouldBe(purchaseOrder);
            simplified.SubTotal?.Confidence.ShouldBe(fields?.SubTotal?.Confidence);
            simplified.SubTotal?.Content.ShouldBe(fields?.SubTotal?.Content);
            simplified.SubTotal?.Type.ShouldBe(fields?.SubTotal?.Type);
            simplified.SubTotal?.ValueCurrency?.Amount.ShouldBe(fields?.SubTotal?.ValueCurrency?.Amount);
            simplified.SubTotal?.ValueCurrency?.CurrencyCode.ShouldBe(fields?.SubTotal?.ValueCurrency?.CurrencyCode);
            simplified.SubTotal?.ValueCurrency?.CurrencySymbol.ShouldBe(fields?.SubTotal?.ValueCurrency?.CurrencySymbol);
            simplified.TotalTax?.Confidence.ShouldBe(fields?.TotalTax?.Confidence);
            simplified.TotalTax?.Content.ShouldBe(fields?.TotalTax?.Content);
            simplified.TotalTax?.Type.ShouldBe(fields?.TotalTax?.Type);
            simplified.TotalTax?.ValueCurrency?.Amount.ShouldBe(fields?.TotalTax?.ValueCurrency?.Amount);
            simplified.TotalTax?.ValueCurrency?.CurrencyCode.ShouldBe(fields?.TotalTax?.ValueCurrency?.CurrencyCode);
            simplified.TotalTax?.ValueCurrency?.CurrencySymbol.ShouldBe(fields?.TotalTax?.ValueCurrency?.CurrencySymbol);
            simplified.VendorName?.Confidence.ShouldBe(fields?.VendorName?.Confidence);
            simplified.VendorName?.Content.ShouldBe(fields?.VendorName?.Content);
            simplified.VendorName?.Type.ShouldBe(fields?.VendorName?.Type);
            simplified.VendorName?.ValueString.ShouldBe(fields?.VendorName?.ValueString);
            simplified.Items.ShouldNotBeNull();
            simplified.Items.Count.ShouldBe(fields?.Items?.ValueArray?.Count ?? 0);
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
            simplified.ShouldNotBeNull();
            simplified.PurchaseOrder?.ValueString.ShouldBe(purchaseOrder);
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
            detailed.ShouldNotBeNull();
            detailed.Status.ShouldBe("succeeded");
            detailed.AnalyzeResult.ShouldNotBeNull();
            detailed.AnalyzeResult.Documents.ShouldNotBeNull();
            detailed.AnalyzeResult.Documents.Count.ShouldBe(1);
            detailed.AnalyzeResult.Documents[0].Fields?.InvoiceId?.ValueString.ShouldBe(invoiceId);
            detailed.AnalyzeResult.Documents[0].Fields?.PurchaseOrder?.ValueString.ShouldBe(purchaseOrder);
            detailed.AnalyzeResult.Documents[0].Fields?.Items?.ValueArray.ShouldNotBeNull();
            detailed.AnalyzeResult.Documents[0].Fields?.Items?.ValueArray?.Count.ShouldBe(itemsCount);
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
            simplified.ShouldNotBeNull();
            simplified.InvoiceId?.ValueString.ShouldBe(invoiceId);
            simplified.PurchaseOrder?.ValueString.ShouldBe(purchaseOrder);
            simplified.Items.ShouldNotBeNull();
            simplified.Items.Count.ShouldBe(itemsCount);
        }
    }
}