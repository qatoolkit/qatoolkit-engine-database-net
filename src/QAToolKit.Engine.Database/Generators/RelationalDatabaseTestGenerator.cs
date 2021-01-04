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
    public abstract class RelationalDatabaseTestGenerator : IDatabaseTestGenerator<DatabaseTest>
    {
        /// <summary>
        /// Database test options
        /// </summary>
        protected readonly DatabaseTestGeneratorOptions _databaseTestOptions;
        /// <summary>
        /// Database kind
        /// </summary>
        public readonly DatabaseKind DatabaseKind;

        /// <summary>
        /// Create new instance of MySQL script generator
        /// </summary>
        /// <param name="databaseKind"></param>
        /// <param name="options"></param>
        public RelationalDatabaseTestGenerator(DatabaseKind databaseKind, Action<DatabaseTestGeneratorOptions> options = null)
        {
            _databaseTestOptions = new DatabaseTestGeneratorOptions();
            options?.Invoke(_databaseTestOptions);
            DatabaseKind = databaseKind;
        }

        /// <summary>
        /// Generate MySQL database test scripts
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<DatabaseTest>> Generate()
        {
            if (_databaseTestOptions == null)
            {
                throw new ArgumentNullException($"{nameof(_databaseTestOptions)} is null.");
            }

            var results = new List<DatabaseTest>();

            results.AddRange(GenerateObjectExistScripts());
            results.AddRange(GenerateCountRecordsScripts());
            results.AddRange(GenerateRecordsExistScripts());
            results.AddRange(GenerateCustomScripts());

            return Task.FromResult(results.AsEnumerable());
        }

        private IEnumerable<DatabaseTest> GenerateCustomScripts()
        {
            var results = new List<DatabaseTest>();

            if (_databaseTestOptions.CustomSqlRules != null)
            {
                results.AddRange(GetCustomScripts());
            }

            return results;
        }

        private IEnumerable<DatabaseTest> GetCustomScripts()
        {
            var results = new List<DatabaseTest>();

            foreach (var record in _databaseTestOptions.CustomSqlRules)
            {
                results.Add(new DatabaseTest(
                    GetCustomScript(record),
                    DatabaseTestType.CustomScript,
                    DatabaseKind));
            }

            return results;
        }

        private IEnumerable<DatabaseTest> GenerateCountRecordsScripts()
        {
            var results = new List<DatabaseTest>();

            if (_databaseTestOptions.DatabaseRecordsCountRules != null)
            {
                results.AddRange(GetRecordCountScripts());
            }

            return results;
        }

        private IEnumerable<DatabaseTest> GetRecordCountScripts()
        {
            var results = new List<DatabaseTest>();

            foreach (var record in _databaseTestOptions.DatabaseRecordsCountRules)
            {
                results.Add(new DatabaseTest(
                    record.TableName,
                    GetRecordCountScript(record),
                    DatabaseTestType.RecordCount,
                    DatabaseKind));
            }

            return results;
        }

        private IEnumerable<DatabaseTest> GenerateRecordsExistScripts()
        {
            var results = new List<DatabaseTest>();

            if (_databaseTestOptions.DatabaseRecordsExitsRules != null)
            {
                results.AddRange(GetRecordsExistScripts());
            }

            return results;
        }

        private IEnumerable<DatabaseTest> GetRecordsExistScripts()
        {
            var results = new List<DatabaseTest>();

            foreach (var record in _databaseTestOptions.DatabaseRecordsExitsRules)
            {
                results.Add(new DatabaseTest(
                    record.TableName,
                    GetRecordExistScript(record),
                    DatabaseTestType.RecordExist,
                    DatabaseKind));
            }

            return results;
        }

        private IEnumerable<DatabaseTest> GenerateObjectExistScripts()
        {
            var results = new List<DatabaseTest>();

            if (_databaseTestOptions.DatabaseObjectsExistRules != null)
            {
                results.AddRange(GetTableExistScripts());
                results.AddRange(GetViewExistScripts());
                results.AddRange(GetStoredProcedureExistScripts());
            }

            return results;
        }

        private IEnumerable<DatabaseTest> GetTableExistScripts()
        {
            var results = new List<DatabaseTest>();

            _databaseTestOptions.DatabaseObjectsExistRules.TryGetValue(DatabaseObjectType.Table, out var tableValues);

            if (tableValues != null)
            {
                foreach (var table in tableValues)
                {
                    results.Add(new DatabaseTest(
                        table,
                        GetTableExistScript(table),
                        DatabaseTestType.ObjectExist,
                        DatabaseKind));
                }
            }

            return results;
        }

        private IEnumerable<DatabaseTest> GetViewExistScripts()
        {
            var results = new List<DatabaseTest>();

            _databaseTestOptions.DatabaseObjectsExistRules.TryGetValue(DatabaseObjectType.View, out var viewValues);

            if (viewValues != null)
            {
                foreach (var view in viewValues)
                {
                    results.Add(new DatabaseTest(
                        view,
                        GetViewExistScript(view),
                        DatabaseTestType.ObjectExist,
                        DatabaseKind));
                }
            }

            return results;
        }

        private IEnumerable<DatabaseTest> GetStoredProcedureExistScripts()
        {
            var results = new List<DatabaseTest>();

            _databaseTestOptions.DatabaseObjectsExistRules.TryGetValue(DatabaseObjectType.StoredProcedure, out var storedProcedureValues);

            if (storedProcedureValues != null)
            {
                foreach (var storedProcedure in storedProcedureValues)
                {
                    results.Add(new DatabaseTest(
                        storedProcedure,
                        GetStoredProcedureExistScript(storedProcedure),
                        DatabaseTestType.ObjectExist,
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
        protected abstract string GetRecordExistScript(DatabaseRecordExistRule recordExist);

        /// <summary>
        /// Get script to count the records in a table
        /// </summary>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        protected abstract string GetRecordCountScript(DatabaseRecordCountRule recordCount);

        /// <summary>
        /// Get custom sql script
        /// </summary>
        /// <param name="script"></param>
        /// <returns></returns>
        protected abstract string GetCustomScript(string script);
    }
}
