using FluentAssertions;
using HPHA.UiPath.Core.Azure.DocumentIntelligence;
using HPHA.UiPath.Core.Converters;

namespace HPHA.UiPath.Core.UnitTests.Converters
{
    public class InvoiceDataConverterTests
    {
        [Fact]
        public void ConvertDetailedToSimplified_ShouldConvertCorrectly()
        {
            // Arrange
            var detailed = new InvoiceDataDetailed
            {
                AnalyzeResult = new AnalyzeResult
                {
                    Documents =
                    [
                        new Document
                        {
                            Fields = new Fields
                            {
                                InvoiceDate = new InvoiceDate { ValueDate = "2023-10-01" },
                                InvoiceId = new InvoiceId { ValueString = "INV123" },
                                InvoiceTotal = new InvoiceTotal { ValueCurrency = new ValueCurrency { Amount = 100.0 } },
                                PurchaseOrder = new PurchaseOrder { ValueString = "PO123" },
                                SubTotal = new SubTotal { ValueCurrency = new ValueCurrency { Amount = 90.0 } },
                                TotalTax = new TotalTax { ValueCurrency = new ValueCurrency { Amount = 10.0 } },
                                VendorName = new VendorName { ValueString = "Vendor Inc." },
                                Items = new Items
                                {
                                    ValueArray =
                                    [
                                        new()
                                        {
                                            ValueObject = new ValueObject
                                            {
                                                Amount = new Amount { ValueCurrency = new ValueCurrency { Amount = 50.0 } },
                                                Description = new Description { ValueString = "Item 1" },
                                                Quantity = new Quantity { ValueNumber = 1 },
                                                Tax = new Tax { ValueCurrency = new ValueCurrency { Amount = 5.0 } },
                                                UnitPrice = new UnitPrice { ValueCurrency = new ValueCurrency { Amount = 50.0 } }
                                            }
                                        }
                                    ]
                                }
                            }
                        }
                    ]
                }
            };

            // Act
            var simplified = InvoiceDataConverter.ConvertDetailedToSimplified(detailed);
            var fields = detailed.AnalyzeResult.Documents[0].Fields;

            // Assert
            simplified.Should().NotBeNull();
            simplified.InvoiceDate.Should().Be(fields?.InvoiceDate);
            simplified.InvoiceId.Should().Be(fields?.InvoiceId);
            simplified.InvoiceTotal.Should().Be(fields?.InvoiceTotal);
            simplified.PurchaseOrder.Should().Be(fields?.PurchaseOrder);
            simplified.SubTotal.Should().Be(fields?.SubTotal);
            simplified.TotalTax.Should().Be(fields?.TotalTax);
            simplified.VendorName.Should().Be(fields?.VendorName);
            simplified.Items.Should().HaveCount(1);
        }

        [Theory]
        [InlineData("Converters/Json/01JJVPZTSD38E959DYVXXDFJRH_d.json", "619649554", "256494", 6)]
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
        [InlineData("Converters/Json/01JJVPZTSD38E959DYVXXDFJRH_d.json", "619649554", "256494", 6)]
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