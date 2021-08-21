using QAToolKit.Core.Interfaces;
using QAToolKit.Engine.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QAToolKit.Engine.Database.Generators
{
    /// <summary>
    /// MySQL database test generator
    /// </summary>
    public abstract class RelationalDatabaseTestGenerator : IDatabaseTestGenerator<Test>
    {
        /// <summary>
        /// Database test options
        /// </summary>
        protected readonly TestGeneratorOptions DatabaseTestOptions;

        /// <summary>
        /// Database kind
        /// </summary>
        public readonly DatabaseKind DatabaseKind;

        /// <summary>
        /// Create new instance of MySQL script generator
        /// </summary>
        /// <param name="databaseKind"></param>
        /// <param name="options"></param>
        public RelationalDatabaseTestGenerator(DatabaseKind databaseKind,
            Action<TestGeneratorOptions> options = null)
        {
            DatabaseTestOptions = new TestGeneratorOptions();
            options?.Invoke(DatabaseTestOptions);
            DatabaseKind = databaseKind;
        }

        /// <summary>
        /// Generate MySQL database test scripts
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Test>> Generate()
        {
            if (DatabaseTestOptions == null)
            {
                throw new ArgumentNullException($"{nameof(DatabaseTestOptions)} is null.");
            }

            var results = new List<Test>();

            results.AddRange(GenerateObjectExistScripts());
            results.AddRange(GenerateCountRecordsScripts());
            results.AddRange(GenerateRecordsExistScripts());
            results.AddRange(GenerateCustomScripts());
            results.AddRange(GenerateQueryStatisticsScripts());

            return Task.FromResult(results.AsEnumerable());
        }

        private IEnumerable<Test> GenerateQueryStatisticsScripts()
        {
            var results = new List<Test>();

            if (DatabaseTestOptions.QueryStatisticsTasks != null)
            {
                results.AddRange(GetQueryStatisticsScripts());
            }

            return results;
        }

        private IEnumerable<Test> GetQueryStatisticsScripts()
        {
            var results = new List<Test>();

            foreach (var record in DatabaseTestOptions.QueryStatisticsTasks)
            {
                foreach (var query in record.Queries)
                {
                    results.Add(new Test(
                        GetQueryStatisticsScript(query, record.QueryStatisticsTypes),
                        TestType.QueryStatistics,
                        DatabaseKind));
                }
            }

            return results;
        }

        private IEnumerable<Test> GenerateCustomScripts()
        {
            var results = new List<Test>();

            if (DatabaseTestOptions.CustomSqlRules != null)
            {
                results.AddRange(GetCustomScripts());
            }

            return results;
        }

        private IEnumerable<Test> GetCustomScripts()
        {
            var results = new List<Test>();

            foreach (var record in DatabaseTestOptions.CustomSqlRules)
            {
                results.Add(new Test(
                    GetCustomScript(record),
                    TestType.CustomScript,
                    DatabaseKind));
            }

            return results;
        }

        private IEnumerable<Test> GenerateCountRecordsScripts()
        {
            var results = new List<Test>();

            if (DatabaseTestOptions.DatabaseRecordsCountRules != null)
            {
                results.AddRange(GetRecordCountScripts());
            }

            return results;
        }

        private IEnumerable<Test> GetRecordCountScripts()
        {
            var results = new List<Test>();

            foreach (var record in DatabaseTestOptions.DatabaseRecordsCountRules)
            {
                results.Add(new Test(
                    record.TableName,
                    GetRecordCountScript(record),
                    TestType.RecordCount,
                    DatabaseKind));
            }

            return results;
        }

        private IEnumerable<Test> GenerateRecordsExistScripts()
        {
            var results = new List<Test>();

            if (DatabaseTestOptions.DatabaseRecordsExitsRules != null)
            {
                results.AddRange(GetRecordsExistScripts());
            }

            return results;
        }

        private IEnumerable<Test> GetRecordsExistScripts()
        {
            var results = new List<Test>();

            foreach (var record in DatabaseTestOptions.DatabaseRecordsExitsRules)
            {
                results.Add(new Test(
                    record.TableName,
                    GetRecordExistScript(record),
                    TestType.RecordExist,
                    DatabaseKind));
            }

            return results;
        }

        private IEnumerable<Test> GenerateObjectExistScripts()
        {
            var results = new List<Test>();

            if (DatabaseTestOptions.DatabaseObjectsExistRules != null)
            {
                results.AddRange(GetTableExistScripts());
                results.AddRange(GetViewExistScripts());
                results.AddRange(GetStoredProcedureExistScripts());
            }

            return results;
        }

        private IEnumerable<Test> GetTableExistScripts()
        {
            var results = new List<Test>();

            DatabaseTestOptions.DatabaseObjectsExistRules.TryGetValue(DatabaseObjectType.Table,
                out var tableValues);

            if (tableValues != null)
            {
                foreach (var table in tableValues)
                {
                    results.Add(new Test(
                        table,
                        GetTableExistScript(table),
                        TestType.ObjectExist,
                        DatabaseKind));
                }
            }

            return results;
        }

        private IEnumerable<Test> GetViewExistScripts()
        {
            var results = new List<Test>();

            DatabaseTestOptions.DatabaseObjectsExistRules.TryGetValue(DatabaseObjectType.View, out var viewValues);

            if (viewValues != null)
            {
                foreach (var view in viewValues)
                {
                    results.Add(new Test(
                        view,
                        GetViewExistScript(view),
                        TestType.ObjectExist,
                        DatabaseKind));
                }
            }

            return results;
        }

        private IEnumerable<Test> GetStoredProcedureExistScripts()
        {
            var results = new List<Test>();

            DatabaseTestOptions.DatabaseObjectsExistRules.TryGetValue(DatabaseObjectType.StoredProcedure,
                out var storedProcedureValues);

            if (storedProcedureValues != null)
            {
                foreach (var storedProcedure in storedProcedureValues)
                {
                    results.Add(new Test(
                        storedProcedure,
                        GetStoredProcedureExistScript(storedProcedure),
                        TestType.ObjectExist,
                        DatabaseKind));
                }
            }

            return results;
        }

        /// <summary>
        /// Get script for table exists abstract method
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        protected abstract string GetTableExistScript(string table);

        /// <summary>
        /// Get script for view exists abstract method
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        protected abstract string GetViewExistScript(string view);

        /// <summary>
        /// Get script for stored procedure exists abstract method
        /// </summary>
        /// <param name="storedProcedure"></param>
        /// <returns></returns>
        protected abstract string GetStoredProcedureExistScript(string storedProcedure);

        /// <summary>
        /// Get script to check if record exist
        /// </summary>
        /// <param name="recordExist"></param>
        /// <returns></returns>
        protected abstract string GetRecordExistScript(RecordExistRule recordExist);

        /// <summary>
        /// Get script to count the records in a table
        /// </summary>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        protected abstract string GetRecordCountScript(RecordCountRule recordCount);

        /// <summary>
        /// Get custom sql script
        /// </summary>
        /// <param name="script"></param>
        /// <returns></returns>
        protected abstract string GetCustomScript(string script);

        /// <summary>
        /// Get custom query statistics script
        /// </summary>
        /// <param name="script"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        protected abstract string GetQueryStatisticsScript(string script, QueryStatisticsType[] types);
    }
}