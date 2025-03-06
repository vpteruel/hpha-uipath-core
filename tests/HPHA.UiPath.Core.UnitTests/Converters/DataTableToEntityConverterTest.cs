using HPHA.UiPath.Core.Converters;
using Microsoft.VisualBasic.FileIO;
using Shouldly;
using System.Data;

namespace HPHA.UiPath.Core.UnitTests.Converters
{
    public class DataTableToEntityConverterTest
    {
        [Fact]
        public void ConvertToPurchaseOrderEntities_ShouldReturnCorrectEntities()
        {
            // Arrange
            var csvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Converters/Csv/test.csv");
            DataTable table = LoadDataTableFromCsv(csvPath);
            
            // Act
            var result = DataTableToEntityConverter.ConvertToPurchaseOrderEntities(table);

            // Assert
            result.ShouldNotBeNull();
            result
                .Where(po => !string.IsNullOrEmpty(po.PurchaseOrder))
                .Count()
                .ShouldBe(38_112);
            result
                .Sum(po => po.Items?.Length)
                .ShouldBe(176_620);
            result
                .Sum(po => po.Items?.Sum(it => it.UnitPrice))
                .ShouldBe(82_470_118.61999889d);
        }

        private static DataTable LoadDataTableFromCsv(string csvPath)
        {
            var table = new DataTable();
            using (var parser = new TextFieldParser(csvPath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters("|");

                // Add columns
                string[] headers = parser.ReadFields()
                    ?? throw new ArgumentNullException("Headers are null");
                foreach (string header in headers)
                {
                    table.Columns.Add(header);
                }

                // Add rows
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields()
                        ?? throw new ArgumentNullException("Fields are null");
                    table.Rows.Add(fields);
                }
            }
            return table;
        }
    }
}