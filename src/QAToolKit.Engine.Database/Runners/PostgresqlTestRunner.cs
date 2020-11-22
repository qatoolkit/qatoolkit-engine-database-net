using Dapper;
using Npgsql;
using QAToolKit.Engine.Database.Interfaces;
using QAToolKit.Engine.Database.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace QAToolKit.Engine.Database.Runners
{
    /// <summary>
    /// SqlServer test runner
    /// </summary>
    public class PostgresqlTestRunner : IDatabaseTestRunner<DatabaseScriptResult>
    {
        private readonly DatabaseTestRunnerOptions _databaseTestOptions;
        private readonly IEnumerable<DatabaseScript> _databaseScripts;

        /// <summary>
        /// Database kind/type
        /// </summary>
        public DatabaseKind DatabaseKind => DatabaseKind.PostgreSQL;

        /// <summary>
        /// SqServer test runner instance
        /// </summary>
        /// <param name="databaseScripts"></param>
        /// <param name="options"></param>
        public PostgresqlTestRunner(IEnumerable<DatabaseScript> databaseScripts, Action<DatabaseTestRunnerOptions> options)
        {
            _databaseTestOptions = new DatabaseTestRunnerOptions();
            options?.Invoke(_databaseTestOptions);
            _databaseScripts = databaseScripts ?? throw new ArgumentNullException($"{nameof(databaseScripts)} is null.");
        }

        /// <summary>
        /// Run the database runner
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<DatabaseScriptResult>> Run()
        {
            var results = new List<DatabaseScriptResult>();

            using (var dbConnection = Connection)
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
                        DatabaseKind));
                }
            }

            return results;
        }

        private IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(_databaseTestOptions.ConnectionString);
            }
        }
    }
}
