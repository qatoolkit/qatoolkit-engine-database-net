namespace QAToolKit.Engine.Database.Models
{
    /// <summary>
    /// Database test type
    /// </summary>
    public enum TestType
    {
        /// <summary>
        /// Assert if the object exists in the database, like tables, views or stored procedures
        /// </summary>
        ObjectExist = 0,
        /// <summary>
        /// Assert if the record exits in the database
        /// </summary>
        RecordExist = 1,
        /// <summary>
        /// Assert if a certain query returns the number of records
        /// </summary>
        RecordCount = 2,
        /// <summary>
        /// Custom sql script
        /// </summary>
        CustomScript = 3,
        /// <summary>
        /// Query statistics script
        /// </summary>
        QueryStatistics = 4
    }
}
