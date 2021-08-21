using MySql.Data.MySqlClient;
using QAToolKit.Engine.Database.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Dapper;
using QAToolKit.Engine.Database.Interfaces;

namespace QAToolKit.Engine.Database.Runners
{
    /// <summary>
    /// MySql server test runner
    /// </summary>
    public class MySqlTestRunner : ISqlRunner
    {
        /// <summary>
        /// Create new instance of MySQL server runner
        /// </summary>
        public MySqlTestRunner()
        {
        }

        /// <summary>
        /// Run the database runner
        /// </summary>
        /// <returns></returns>
        public async Task<TestResult> Run(Test databaseTest,
            TestRunnerOptions databaseTestOptions)
        {
            using (var dbConnection = new MySqlConnection(databaseTestOptions.ConnectionString))
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
                    DatabaseKind.MySql,
                    0,
                    0,
                    sw.ElapsedMilliseconds,
                    null);
            }
        }
    }
}