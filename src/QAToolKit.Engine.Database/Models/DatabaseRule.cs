namespace QAToolKit.Engine.Database.Models
{
    /// <summary>
    /// Database rule
    /// </summary>
    public class DatabaseRule
    {
        /// <summary>
        /// Database table name
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// Predicate value
        /// </summary>
        public string PredicateValue { get; set; }
    }
}
