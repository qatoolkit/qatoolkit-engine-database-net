using Npgsql;
using QAToolKit.Engine.Database.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Dapper;
using QAToolKit.Engine.Database.Interfaces;

namespace QAToolKit.Engine.Database.Runners
{
    /// <summary>
    /// Postgresql server test runner
    /// </summary>
    public class PostgresqlTestRunner : ISqlRunner
    {
        /// <summary>
        /// Create new instance of Postgresql server runner
        /// </summary>
        public PostgresqlTestRunner()
        {}

        /// <summary>
        /// Run the database runner
        /// </summary>
        /// <returns></returns>
        public async Task<TestResult> Run(Test databaseTest,
            TestRunnerOptions databaseTestOptions)
        {
            using (var dbConnection = new NpgsqlConnection(databaseTestOptions.ConnectionString))
            {
                dbConnection.Open();

                var sw = new Stopwatch();
                sw.Start();

                var databaseResult = await dbConnection.ExecuteScalarAsync<int>(databaseTest.Script);

                sw.Stop();

                return new TestResult(
                    Convert.ToBoolean(databaseResult),
                    databaseTest.Variable,
                    databaseTest.Script,
                    databaseTest.DatabaseTestType,
                    DatabaseKind.PostgreSql,
                    0,
                    0,
                    sw.ElapsedMilliseconds,
                    null);
            }
        }
    }
}