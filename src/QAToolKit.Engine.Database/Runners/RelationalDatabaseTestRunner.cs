using Dapper;
using QAToolKit.Engine.Database.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using QAToolKit.Core.Interfaces;

namespace QAToolKit.Engine.Database.Runners
{
    /// <summary>
    /// General relational database test runner
    /// </summary>
    public abstract class RelationalDatabaseTestRunner : IDatabaseTestRunner<DatabaseScriptResult>
    {
        /// <summary>
        /// database test options
        /// </summary>
        protected readonly DatabaseTestRunnerOptions _databaseTestOptions;
        private readonly IEnumerable<DatabaseScript> _databaseScripts;
        private readonly DatabaseKind _databaseKind;

        /// <summary>
        /// General relational database test runner instance
        /// </summary>
        /// <param name="databaseScripts"></param>
        /// <param name="databaseKind"></param>
        /// <param name="options"></param>
        public RelationalDatabaseTestRunner(IEnumerable<DatabaseScript> databaseScripts, DatabaseKind databaseKind, Action<DatabaseTestRunnerOptions> options)
        {
            _databaseTestOptions = new DatabaseTestRunnerOptions();
            options?.Invoke(_databaseTestOptions);
            _databaseScripts = databaseScripts ?? throw new ArgumentNullException($"{nameof(databaseScripts)} is null.");
            _databaseKind = databaseKind;
        }

        /// <summary>
        /// Run the database runner
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<DatabaseScriptResult>> Run()
        {
            var results = new List<DatabaseScriptResult>();

            using (var dbConnection = GetConnection())
            {
                dbConnection.Open();

                foreach (var script in _databaseScripts)
                {
                    var databaseResult = await dbConnection.ExecuteScalarAsync<int>(script.Script);

                    results.Add(new DatabaseScriptResult(
                        Convert.ToBoolean(databaseResult),
                        script.Script,
                        script.Variable,
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
