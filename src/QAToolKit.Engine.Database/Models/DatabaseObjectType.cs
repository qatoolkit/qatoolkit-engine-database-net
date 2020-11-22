namespace QAToolKit.Engine.Database.Models
{
    /// <summary>
    /// Database object type
    /// </summary>
    public enum DatabaseObjectType
    {
        /// <summary>
        /// Undefined
        /// </summary>
        Undefined,
        /// <summary>
        /// Table
        /// </summary>
        Table,
        /// <summary>
        /// View
        /// </summary>
        View,
        /// <summary>
        /// Stored procedure, routine in MySql and PostgreSQL
        /// </summary>
        StoredProcedure
    }
}