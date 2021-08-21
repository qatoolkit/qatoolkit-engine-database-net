using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dapper;
using QAToolKit.Core.Helpers;
using QAToolKit.Engine.Database.Interfaces;
using QAToolKit.Engine.Database.Models;

namespace QAToolKit.Engine.Database.Runners
{
    /// <summary>
    /// SQL server test runner for a single script
    /// </summary>
    public class SqlServerTestRunner : ISqlRunner
    {
        /// <summary>
        /// Create new instance of SQL server runner
        /// </summary>
        public SqlServerTestRunner()
        {
        }

        /// <summary>
        /// Run the database runner
        /// </summary>
        /// <returns></returns>
        public async Task<TestResult> Run(Test databaseTest,
            TestRunnerOptions databaseTestOptions)
        {
            using (var dbConnection = new SqlConnection(databaseTestOptions.ConnectionString))
            {
                var serverCpuTime = 0;
                var serverElapsedTime = 0;
                var statistics = new Dictionary<string, Dictionary<string, long>>();

                dbConnection.Open();

                if (databaseTest.DatabaseTestType == TestType.QueryStatistics)
                {
                    dbConnection.InfoMessage += (sender, args) =>
                    {
                        var message = args.Message;

                        if (message.Contains("SQL Server Execution Times:"))
                        {
                            var cpuTime = Regex.Match(message, "(?<=CPU time = ).*?(?= ms,)");
                            var elapsedTime = Regex.Match(message, "(?<=elapsed time = ).*?(?= ms.)");

                            if (Convert.ToInt32(cpuTime.Value) > 0)
                            {
                                serverCpuTime = Convert.ToInt32(cpuTime.Value);
                            }

                            if (Convert.ToInt32(elapsedTime.Value) > 0)
                            {
                                serverElapsedTime = Convert.ToInt32(elapsedTime.Value);
                            }
                        }

                        if (message.StartsWith("Table"))
                        {
                            var tableName = Regex.Match(message, "(?<=Table ').*?(?='.)");
                            var stats = new Dictionary<string, long>();

                            var list = new List<string>();
                            list.AddRange(Regex.Replace(message, $"Table '{tableName}'. ", string.Empty).Split(','));

                            foreach (var item in list)
                            {
                                var statName = Regex.Match(item, "[^0-9]+");
                                var statValue = Regex.Match(item, @"\d+");
                                stats.Add(statName.Value.ToPascalCase(new[] {"-", "_", " "}),
                                    Convert.ToInt64(statValue.Value));
                            }

                            statistics.Add(tableName.Value, stats);
                        }
                    };
                }

                var sw = new Stopwatch();
                sw.Start();

                bool? databaseResult = null;
                if (databaseTest.DatabaseTestType == TestType.QueryStatistics)
                {
                    await dbConnection.QueryAsync<dynamic>(databaseTest.Script);
                }
                else
                {
                    var result = await dbConnection.ExecuteScalarAsync<int>(databaseTest.Script);
                    databaseResult = Convert.ToBoolean(result);
                }

                sw.Stop();

                return new TestResult(
                    databaseResult,
                    databaseTest.Variable,
                    databaseTest.Script,
                    databaseTest.DatabaseTestType,
                    DatabaseKind.SqlServer,
                    serverCpuTime,
                    serverElapsedTime,
                    sw.ElapsedMilliseconds,
                    statistics);
            }
        }
    }
}