namespace QAToolKit.Engine.Database.Models
{
    /// <summary>
    /// Database script
    /// </summary>
    public class Test
    {
        /// <summary>
        /// Variable name of the object being tested
        /// </summary>
        public string Variable { get; private set; }
        /// <summary>
        /// Script to run the test
        /// </summary>
        public string Script { get; private set; }
        /// <summary>
        /// Database test type
        /// </summary>
        public TestType DatabaseTestType { get; private set; }
        /// <summary>
        /// Database kind
        /// </summary>
        public DatabaseKind DatabaseKind { get; private set; }

        /// <summary>
        /// Create new instance of the database test
        /// </summary>
        /// <param name="variable"></param>
        /// <param name="script"></param>
        /// <param name="databaseTestType"></param>
        /// <param name="databaseKind"></param>
        public Test(string variable, string script, TestType databaseTestType, DatabaseKind databaseKind)
        {
            Variable = variable;
            Script = script;
            DatabaseTestType = databaseTestType;
            DatabaseKind = databaseKind;
        }

        /// <summary>
        /// Create new instance of the database test
        /// </summary>
        /// <param name="script"></param>
        /// <param name="databaseTestType"></param>
        /// <param name="databaseKind"></param>
        public Test(string script, TestType databaseTestType, DatabaseKind databaseKind)
        {
            Variable = null;
            Script = script;
            DatabaseTestType = databaseTestType;
            DatabaseKind = databaseKind;
        }
    }
}
