namespace QAToolKit.Engine.Database.Models
{
    /// <summary>
    /// Database test result
    /// </summary>
    public class DatabaseTestResult

    {
        /// <summary>
        /// The result of the script test
        /// </summary>
        public bool DatabaseResult { get; set; }
        /// <summary>
        /// Variable name of the object beiing tested
        /// </summary>
        public string Variable { get; set; }
        /// <summary>
        /// Script to run the test
        /// </summary>
        public string Script { get; set; }
        /// <summary>
        /// Databae test type
        /// </summary>
        public DatabaseTestType DatabaseTestType { get; set; }
        /// <summary>
        /// Database kind
        /// </summary>
        public DatabaseKind DatabaseKind { get; private set; }

        /// <summary>
        /// Create new instance of database script result object
        /// </summary>
        /// <param name="databaseResult"></param>
        /// <param name="variable"></param>
        /// <param name="script"></param>
        /// <param name="databaseTestType"></param>
        /// <param name="databaseKind"></param>
        public DatabaseTestResult(bool databaseResult, string variable, string script, DatabaseTestType databaseTestType,
            DatabaseKind databaseKind)
        {
            DatabaseResult = databaseResult;
            Variable = variable;
            Script = script;
            DatabaseTestType = databaseTestType;
            DatabaseKind = databaseKind;
        }
    }
}
