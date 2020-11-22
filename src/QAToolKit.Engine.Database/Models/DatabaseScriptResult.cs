namespace QAToolKit.Engine.Database.Models
{
    public class DatabaseScriptResult
    {
        public bool DatabaseResult { get; set; }
        public string Variable { get; set; }
        public string Script { get; set; }
        public DatabaseTestType DatabaseTestType { get; set; }
        public DatabaseKind DatabaseKind { get; private set; }

        public DatabaseScriptResult(bool databaseResult, string variable, string script, DatabaseTestType databaseTestType,
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
