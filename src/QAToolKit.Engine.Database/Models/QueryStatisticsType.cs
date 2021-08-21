namespace QAToolKit.Engine.Database.Models
{
    /// <summary>
    /// Query statistics rule
    /// </summary>
    public enum QueryStatisticsType
    {
        /// <summary>
        /// Measure of database time statistics
        /// </summary>
        Time,
        /// <summary>
        /// Measure of database IO statistics
        /// </summary>
        Io
    }
}