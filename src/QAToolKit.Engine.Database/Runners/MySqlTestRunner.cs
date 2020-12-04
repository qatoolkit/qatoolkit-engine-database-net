using MySql.Data.MySqlClient;
using QAToolKit.Engine.Database.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace QAToolKit.Engine.Database.Runners
{
    /// <summary>
    /// SqlServer test runner
    /// </summary>
    public class MySqlTestRunner : RelationalDatabaseTestRunner
    {
        /// <summary>
        /// SqServer test runner instance
        /// </summary>
        /// <param name="DatabaseTests"></param>
        /// <param name="options"></param>
        public MySqlTestRunner(IEnumerable<DatabaseTest> DatabaseTests, Action<DatabaseTestRunnerOptions> options)
            : base(DatabaseTests, DatabaseKind.MySQL, options)
        { }

        /// <summary>
        /// Get MySQL database connection
        /// </summary>
        /// <returns></returns>
        protected override IDbConnection GetConnection()
        {
            return new MySqlConnection(_databaseTestOptions.ConnectionString);
        }
    }
}
