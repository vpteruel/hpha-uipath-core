using HPHA.UiPath.Core.Formatters;
using Shouldly;

namespace HPHA.UiPath.Core.UnitTests.Formatters
{
    public class TimerFormatterTest
    {
        [Theory]
        [InlineData(1, 2, 3, 4, 500, new[] { "1d", "2h", "3m", "4s", "500ms" })]
        [InlineData(0, 0, 0, 0, 500, new[] { "500ms" })]
        [InlineData(0, 0, 0, 4, 0, new[] { "4s" })]
        [InlineData(0, 0, 3, 0, 0, new[] { "3m" })]
        [InlineData(0, 2, 0, 0, 0, new[] { "2h" })]
        [InlineData(100, 0, 0, 0, 0, new[] { "100d" })]
        public void GetTimeParts_ShouldReturnCorrectParts(int days, int hours, int minutes, int seconds, int milliseconds, string[] expectedParts)
        {
            // Arrange
            TimeSpan timeSpan = new(days, hours, minutes, seconds, milliseconds);

            // Act
            List<string> result = TimerFormatter.GetTimeParts(timeSpan);

            // Assert
            result.ShouldBe(expectedParts);
        }

        [Theory]
        [InlineData(93784000, "1d 2h 3m 4s")] // 1 day, 2 hours, 3 minutes, 4 seconds, 0 milliseconds
        [InlineData(86400000, "1d")] // 1 day
        [InlineData(3600000, "1h")] // 1 hour
        [InlineData(60000, "1m")] // 1 minute
        [InlineData(1000, "1s")] // 1 second
        [InlineData(500, "500ms")] // 500 milliseconds
        [InlineData(0, "0ms")] // 0 milliseconds
        public void FormatHumanReadable_ShouldReturnCorrectFormat(long elapsedMilliseconds, string expectedFormat)
        {
            // Act
            string result = TimerFormatter.FormatHumanReadable(elapsedMilliseconds);

            // Assert
            result.ShouldBe(expectedFormat);
        }

        [Theory]
        [InlineData(60000, 10, "6s")] // 1 minute, 10 rows
        [InlineData(0, 10, "0ms")] // 0 milliseconds, 10 rows
        [InlineData(-1, 0, "N/A")] // Invalid input
        public void GetAverageTimePerRow_ShouldReturnCorrectAverage(long elapsedMilliseconds, int rowsProcessed, string expectedAverage)
        {
            // Act
            string result = TimerFormatter.GetAverageTimePerRow(elapsedMilliseconds, rowsProcessed);

            // Assert
            result.ShouldBe(expectedAverage);
        }

        [Theory]
        [InlineData(60000, 10, 20, "1m")] // 1 minute, 10 rows processed, 20 total rows
        [InlineData(3600000, 1, 100, "4d 3h")] // 1 hour, 1 row processed, 100 total rows
        [InlineData(0, 0, 100, "N/A")] // Edge case
        [InlineData(60000, 0, 0, "N/A")] // Invalid input
        public void GetEstimatedTimeRemaining_ShouldReturnCorrectEstimate(long elapsedMilliseconds, int rowsProcessed, int totalRows, string expectedEstimate)
        {
            // Act
            string result = TimerFormatter.GetEstimatedTimeRemaining(elapsedMilliseconds, rowsProcessed, totalRows);

            // Assert
            result.ShouldBe(expectedEstimate);
        }
    }
}
