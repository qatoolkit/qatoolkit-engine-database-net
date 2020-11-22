namespace QAToolKit.Engine.Database.Models
{
    public class DatabaseScript
    {
        public string Variable { get; private set; }
        public string Script { get; private set; }
        public DatabaseTestType DatabaseTestType { get; private set; }
        public DatabaseKind DatabaseKind { get; private set; }

        public DatabaseScript(string variable, string script, DatabaseTestType databaseTestType, DatabaseKind databaseKind)
        {
            Variable = variable;
            Script = script;
            DatabaseTestType = databaseTestType;
            DatabaseKind = databaseKind;
        }
    }
}
