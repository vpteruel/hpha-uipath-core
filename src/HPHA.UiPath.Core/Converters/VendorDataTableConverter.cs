using HPHA.UiPath.Core.Entities.Common;
using System.Data;

namespace HPHA.UiPath.Core.Converters
{
    public static class VendorDataTableConverter
    {
        /// <summary>
        /// Loads vendor data from a text file into a DataTable.
        /// </summary>
        /// <param name="filePath">The path to the text file.</param>
        /// <returns>A DataTable containing the vendor data.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the file path is null or empty.</exception>
        public static DataTable LoadVendorData(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(nameof(filePath), "File path cannot be null or empty.");
            }

            var dataTable = new DataTable();
            dataTable.Columns.Add("Vendor", typeof(string));
            dataTable.Columns.Add("Email", typeof(string));

            // Read the text file and populate the DataTable
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var columns = line.Split(';');
                if (columns.Length == 2)
                {
                    dataTable.Rows.Add(columns[0], columns[1]);
                }
            }

            return dataTable;
        }

        /// <summary>
        /// Retrieves a specific field value from a DataTable based on a given condition.
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="from"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static string GetFieldByEmail(DataTable dataTable, string from, string fieldName)
        {
            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                throw new ArgumentException("DataTable is empty or null.", nameof(dataTable));
            }

            if (string.IsNullOrEmpty(from))
            {
                throw new ArgumentNullException(nameof(from), "From parameter cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(fieldName))
            {
                throw new ArgumentNullException(nameof(fieldName), "Field name cannot be null or empty.");
            }

            var dataRow = dataTable.AsEnumerable()
                .FirstOrDefault(row => row.Field<string>("Email") == from);

            return dataRow?[fieldName].ToString() ?? string.Empty;
        }
    }
}
