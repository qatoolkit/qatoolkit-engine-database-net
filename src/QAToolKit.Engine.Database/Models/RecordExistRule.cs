namespace QAToolKit.Engine.Database.Models
{
    /// <summary>
    /// Database rule
    /// </summary>
    public class RecordExistRule
    {
        /// <summary>
        /// Database table name
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// Column name
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// Operator
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// Value
        /// </summary>
        public object Value { get; set; }
    }
}
