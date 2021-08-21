namespace QAToolKit.Engine.Database.Models
{
    /// <summary>
    /// Database rule
    /// </summary>
    public class RecordCountRule
    {
        /// <summary>
        /// Database table name
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// Operator
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// Value
        /// </summary>
        public long Count { get; set; }
    }
}
