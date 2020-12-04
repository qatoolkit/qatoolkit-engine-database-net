using QAToolKit.Engine.Database.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QAToolKit.Engine.Database.Runners
{
    /// <summary>
    /// SqlServer test runner
    /// </summary>
    public class SqlServerTestRunner : RelationalDatabaseTestRunner
    {
        /// <summary>
        /// SqServer test runner instance
        /// </summary>
        /// <param name="DatabaseTests"></param>
        /// <param name="options"></param>
        public SqlServerTestRunner(IEnumerable<DatabaseTest> DatabaseTests, Action<DatabaseTestRunnerOptions> options)
            : base(DatabaseTests, DatabaseKind.SQLServer, options)
        { }

        /// <summary>
        /// Get SQL server database connection
        /// </summary>
        /// <returns></returns>
        protected override IDbConnection GetConnection()
        {
            return new SqlConnection(_databaseTestOptions.ConnectionString);
        }
    }
}
