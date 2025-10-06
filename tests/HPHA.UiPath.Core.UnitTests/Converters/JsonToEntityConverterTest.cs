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
        [InlineData("baxter.json", "01JPAP64MG66799B4R3F2WF910", "36794479", "2025-01-17", "256049", "Baxter", -1d, -1d, -1d, -1d, -1d, -1d, -1d, 175.52d, 22.82d, 198.34d, 198.34d, 1)]
        [InlineData("chs.json", "01JPAM1NWEV3KFCCFEGX7SA9C0", "PS-INV394638", "2025-03-04", "257773", "Canadian Hospital Specialties Limited", 31.26d, -1d, -1d, -1d, -1d, -1d, -1d, 132.54d, 17.23d, 149.77d, -1d, 1)]
        [InlineData("fisher-scientific1.json", "01JPANGC02PZ7YSRPJ8JE6XN7K", "1821376", "2025-03-03", "257767", "Fisher Scientific Company", 26d, -1d, -1d, -1d, 8d, 18d, 29d, 131.05d, 17.04d, 148.09d, -1d, 1)]
        [InlineData("fisher-scientific2.json", "01JPANGVDK57VR19CT17QG4KA6", "1765399", "2025-01-21", "255140", "Fisher Scientific Company", -1d, -1d, -1d, -1d, -1d, -1d, -1d, 184.35d, 23.97d, 208.32d, -1d, 1)]
        [InlineData("fisher-scientific3.json", "01JPANHB1MMNEK5MSYKDCZ4R3M", "1759521", "2025-01-16", "256111", "Fisher Scientific Company", 93.29d, -1d, 45d, -1d, 8d, 40.29d, -1d, 316.63d, 41.16d, 357.79d, -1d, 2)]
        [InlineData("fisher-scientific4.json", "01JPANHT7VJ6JSZR8117SHD808", "1763490", "2025-01-20", "256254", "Fisher Scientific Company", 94.88d, -1d, -1d, -1d, 8d, 86.88d, -1d, 576.48d, 74.94d, 651.42d, -1d, 1)]
        [InlineData("fisher-scientific5.json", "01JPANJA7KREFMK7V0FJ5CX3R1", "1759521", "2025-01-16", "256111", "Fisher Scientific Company", 93.29d, -1d, 45d, -1d, 8d, 40.29d, -1d, 316.63d, 41.16d, 357.79d, -1d, 2)]
        public void ConvertCompactedJsonToPurchaseOrderEntity_ShouldReturnCorrectEntity_WhenJsonIsValid(
            string jsonPath
            , string uniqueId
            , string invoiceId
            , string invoiceDate
            , string purchaseOrder
            , string vendorName
            , double freight
            , double shippingCredit
            , double hazardousFee
            , double handlingFee
            , double fuelSurcharge
            , double transportationCharge
            , double minimumOrder
            , double subTotal
            , double totalTax
            , double invoiceTotal
            , double amountDue
            , int itemsCount)
        {
            // Arrange
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Converters", "Json", jsonPath);
            var fileInfo = new FileInfo(filePath);

            // Act
            PurchaseOrderEntity? result = JsonToEntityConverter.ConvertJsonToEntity(fileInfo);

            // Assert
            result.ShouldNotBeNull();
            result.UniqueId.ShouldBe(Ulid.Parse(uniqueId));
            result.InvoiceId.ShouldBe(invoiceId);
            result.InvoiceDate.ShouldBe(DateOnly.Parse(invoiceDate));
            result.PurchaseOrder.ShouldBe(purchaseOrder);
            result.VendorName.ShouldBe(vendorName);
            if (freight > 0d)
                result.Freight.ShouldBe(freight);
            if (shippingCredit > 0d)
                result.ShippingCredit.ShouldBe(shippingCredit);
            if (hazardousFee > 0d)
                result.HazardousFee.ShouldBe(hazardousFee);
            if (handlingFee > 0d)
                result.HandlingFee.ShouldBe(handlingFee);
            if (fuelSurcharge > 0d)
                result.FuelSurcharge.ShouldBe(fuelSurcharge);
            if (transportationCharge > 0d)
                result.TransportationCharge.ShouldBe(transportationCharge);
            if (minimumOrder > 0d)
                result.MinimumOrder.ShouldBe(minimumOrder);
            result.SubTotal.ShouldBe(subTotal);
            result.TotalTax.ShouldBe(totalTax);
            result.InvoiceTotal.ShouldBe(invoiceTotal);
            if (amountDue > 0d)
                result.AmountDue.ShouldBe(amountDue);
            result.Items.ShouldNotBeNull();
            result.Items.Length.ShouldBe(itemsCount);
            Math.Round(Convert.ToDecimal(result.SubTotal + result.TotalTax), 2)
                .ShouldBe(Convert.ToDecimal(result.InvoiceTotal));
        }
    }
}