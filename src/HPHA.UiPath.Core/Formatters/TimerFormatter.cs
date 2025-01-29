namespace HPHA.UiPath.Core.Formatters
{
    public static class TimerFormatter
    {
        /// <summary>
        /// Returns a list of time parts (e.g., days, hours, minutes, seconds, milliseconds) for the given TimeSpan.
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public static List<string> GetTimeParts(TimeSpan timeSpan)
        {
            List<string> parts = new();

            if (timeSpan.Days > 0)
                parts.Add(string.Format("{0}d", timeSpan.Days));
            if (timeSpan.Hours > 0)
                parts.Add(string.Format("{0}h", timeSpan.Hours));
            if (timeSpan.Minutes > 0)
                parts.Add(string.Format("{0}m", timeSpan.Minutes));
            if (timeSpan.Seconds > 0)
                parts.Add(string.Format("{0}s", timeSpan.Seconds));
            if (timeSpan.Milliseconds > 0 || parts.Count == 0)
                parts.Add(string.Format("{0}ms", timeSpan.Milliseconds));

            return parts;
        }

        /// <summary>
        /// Formats the elapsed time in milliseconds into a human-readable string.
        /// </summary>
        /// <param name="elapsedMilliseconds"></param>
        /// <returns></returns>
        public static string FormatHumanReadable(long elapsedMilliseconds)
        {
            TimeSpan timeSpan = TimeSpan.FromMilliseconds(elapsedMilliseconds);
            List<string> parts = GetTimeParts(timeSpan);
            return string.Join(" ", parts);
        }

        /// <summary>
        /// Returns the average time per row in a human-readable format.
        /// </summary>
        /// <param name="elapsedMilliseconds"></param>
        /// <param name="rowsProcessed"></param>
        /// <returns></returns>
        public static string GetAverageTimePerRow(long elapsedMilliseconds, int rowsProcessed)
        {
            // Validate inputs
            if (rowsProcessed <= 0 || elapsedMilliseconds < 0)
                return "N/A";

            // Calculate average time per row
            double averageTimePerRow = elapsedMilliseconds / rowsProcessed;
            long roundedAverage = (long)Math.Round(averageTimePerRow);

            // Ensure non-negative result (e.g., if elapsedMilliseconds = 0)
            roundedAverage = Math.Max(0, roundedAverage);

            // Display the results
            return FormatHumanReadable(roundedAverage);
        }

        /// <summary>
        /// Returns the estimated time remaining in a human-readable format.
        /// </summary>
        /// <param name="elapsedMilliseconds"></param>
        /// <param name="rowsProcessed"></param>
        /// <param name="totalRows"></param>
        /// <returns></returns>
        public static string GetEstimatedTimeRemaining(long elapsedMilliseconds, int rowsProcessed, int totalRows)
        {
            // Handle invalid or edge cases
            if (rowsProcessed <= 0 || totalRows <= 0 || rowsProcessed >= totalRows)
                return "N/A";

            // Calculate average time per row
            double averageTimePerRow = elapsedMilliseconds / rowsProcessed;

            // Calculate estimated time remaining
            int rowsRemaining = totalRows - rowsProcessed;
            double estimatedTimeRemaining = rowsRemaining * averageTimePerRow;
            long roundedEstimatedTimeRemaining = (long)Math.Round(estimatedTimeRemaining);

            // Ensure non-negative result (e.g., if elapsedMilliseconds = 0)
            roundedEstimatedTimeRemaining = Math.Max(0, roundedEstimatedTimeRemaining);

            // Display the results
            return FormatHumanReadable(roundedEstimatedTimeRemaining);
        }
    }
}
