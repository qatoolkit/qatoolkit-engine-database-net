using Dapper;
using QAToolKit.Core.Interfaces;
using QAToolKit.Engine.Database.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace QAToolKit.Engine.Database.Runners
{
    /// <summary>
    /// General relational database test runner
    /// </summary>
    public abstract class RelationalDatabaseTestRunner : IDatabaseTestRunner<DatabaseTestResult>
    {
        /// <summary>
        /// database test options
        /// </summary>
        protected readonly DatabaseTestRunnerOptions _databaseTestOptions;
        private readonly IEnumerable<DatabaseTest> _DatabaseTests;
        private readonly DatabaseKind _databaseKind;

        /// <summary>
        /// General relational database test runner instance
        /// </summary>
        /// <param name="DatabaseTests"></param>
        /// <param name="databaseKind"></param>
        /// <param name="options"></param>
        public RelationalDatabaseTestRunner(IEnumerable<DatabaseTest> DatabaseTests, DatabaseKind databaseKind, Action<DatabaseTestRunnerOptions> options)
        {
            _databaseTestOptions = new DatabaseTestRunnerOptions();
            options?.Invoke(_databaseTestOptions);
            _DatabaseTests = DatabaseTests ?? throw new ArgumentNullException($"{nameof(DatabaseTests)} is null.");
            _databaseKind = databaseKind;
        }

        /// <summary>
        /// Run the database runner
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<DatabaseTestResult>> Run()
        {
            var results = new List<DatabaseTestResult>();

            using (var dbConnection = GetConnection())
            {
                dbConnection.Open();

                foreach (var script in _DatabaseTests)
                {
                    var databaseResult = await dbConnection.ExecuteScalarAsync<int>(script.Script);

                    results.Add(new DatabaseTestResult(
                        Convert.ToBoolean(databaseResult),
                        script.Variable,
                        script.Script,
                        script.DatabaseTestType,
                        _databaseKind));
                }
            }

            return results;
        }

        /// <summary>
        /// Get database connection template
        /// </summary>
        /// <returns></returns>
        protected abstract IDbConnection GetConnection();
    }
}
