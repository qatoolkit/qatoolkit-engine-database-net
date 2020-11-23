using Npgsql;
using QAToolKit.Engine.Database.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace QAToolKit.Engine.Database.Runners
{
    /// <summary>
    /// SqlServer test runner
    /// </summary>
    public class PostgresqlTestRunner : RelationalDatabaseTestRunner
    {
        /// <summary>
        /// SqServer test runner instance
        /// </summary>
        /// <param name="databaseScripts"></param>
        /// <param name="options"></param>
        public PostgresqlTestRunner(IEnumerable<DatabaseScript> databaseScripts, Action<DatabaseTestRunnerOptions> options)
            : base(databaseScripts, DatabaseKind.PostgreSQL, options)
        { }

        /// <summary>
        /// Get PostgreSQL database connection
        /// </summary>
        /// <returns></returns>
        protected override IDbConnection GetConnection()
        {
            return new NpgsqlConnection(_databaseTestOptions.ConnectionString);
        }
    }
}
