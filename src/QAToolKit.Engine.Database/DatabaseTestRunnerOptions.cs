using QAToolKit.Engine.Database.Models;

namespace QAToolKit.Engine.Database
{
    /// <summary>
    /// Database test runner options
    /// </summary>
    public class DatabaseTestRunnerOptions
    {
        internal DatabaseKind DatabaseKind { get; private set; } = DatabaseKind.Undefined;
        internal string ConnectionString { get; private set; }

        /// <summary>
        /// Add SqlServer database connection string
        /// </summary>
        /// <param name="serverConnection"></param>
        /// <returns></returns>
        public DatabaseTestRunnerOptions AddSQLServerConnection(string serverConnection)
        {
            DatabaseKind = DatabaseKind.SQLServer;
            ConnectionString = serverConnection;
            return this;
        }

        /// <summary>
        /// Add MySQL database connection string
        /// </summary>
        /// <param name="serverConnection"></param>
        /// <returns></returns>
        public DatabaseTestRunnerOptions AddMySQLConnection(string serverConnection)
        {
            DatabaseKind = DatabaseKind.MySQL;
            ConnectionString = serverConnection;
            return this;
        }

        /// <summary>
        /// Add Postgresql database connection string
        /// </summary>
        /// <param name="serverConnection"></param>
        /// <returns></returns>
        public DatabaseTestRunnerOptions AddPostgreSQLConnection(string serverConnection)
        {
            DatabaseKind = DatabaseKind.PostgreSQL;
            ConnectionString = serverConnection;
            return this;
        }
    }
}
