namespace QAToolKit.Engine.Database.Models
{
    /// <summary>
    /// Database script
    /// </summary>
    public class DatabaseTest

    {
        /// <summary>
        /// Variable name of the object beiing tested
        /// </summary>
        public string Variable { get; private set; }
        /// <summary>
        /// Script to run the test
        /// </summary>
        public string Script { get; private set; }
        /// <summary>
        /// Databae test type
        /// </summary>
        public DatabaseTestType DatabaseTestType { get; private set; }
        /// <summary>
        /// Database kind
        /// </summary>
        public DatabaseKind DatabaseKind { get; private set; }

        /// <summary>
        /// Create new instance of the script
        /// </summary>
        /// <param name="variable"></param>
        /// <param name="script"></param>
        /// <param name="databaseTestType"></param>
        /// <param name="databaseKind"></param>
        public DatabaseTest(string variable, string script, DatabaseTestType databaseTestType, DatabaseKind databaseKind)
        {
            Variable = variable;
            Script = script;
            DatabaseTestType = databaseTestType;
            DatabaseKind = databaseKind;
        }
    }
}
