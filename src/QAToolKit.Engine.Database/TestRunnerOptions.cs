using QAToolKit.Engine.Database.Models;
using System;

namespace QAToolKit.Engine.Database
{
    /// <summary>
    /// Database test runner options
    /// </summary>
    public class TestRunnerOptions
    {
        internal DatabaseKind DatabaseKind { get; private set; } = DatabaseKind.Undefined;
        internal string ConnectionString { get; private set; }

        /// <summary>
        /// Add SqlServer database connection string
        /// </summary>
        /// <param name="serverConnection"></param>
        /// <returns></returns>
        public TestRunnerOptions AddSQLServerConnection(string serverConnection)
        {
            DatabaseKind = DatabaseKind.SqlServer;
            ConnectionString = serverConnection ?? throw new ArgumentNullException($"{nameof(serverConnection)} is null.");
            return this;
        }

        /// <summary>
        /// Add MySQL database connection string
        /// </summary>
        /// <param name="serverConnection"></param>
        /// <returns></returns>
        public TestRunnerOptions AddMySQLConnection(string serverConnection)
        {
            DatabaseKind = DatabaseKind.MySql;
            ConnectionString = serverConnection ?? throw new ArgumentNullException($"{nameof(serverConnection)} is null.");
            return this;
        }

        /// <summary>
        /// Add Postgresql database connection string
        /// </summary>
        /// <param name="serverConnection"></param>
        /// <returns></returns>
        public TestRunnerOptions AddPostgreSQLConnection(string serverConnection)
        {
            DatabaseKind = DatabaseKind.PostgreSql;
            ConnectionString = serverConnection ?? throw new ArgumentNullException($"{nameof(serverConnection)} is null.");
            return this;
        }
    }
}
