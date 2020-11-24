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
    public abstract class RelationalDatabaseTestGenerator : IDatabaseTestGenerator<DatabaseScript>
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
        public Task<IEnumerable<DatabaseScript>> Generate()
        {
            if (_databaseTestOptions == null)
            {
                throw new ArgumentNullException($"{nameof(_databaseTestOptions)} is null.");
            }

            var results = new List<DatabaseScript>();

            results.AddRange(GenerateObjectExistScripts());
            results.AddRange(GenerateCountRecordsScripts());
            results.AddRange(GenerateRecordsExistScripts());

            return Task.FromResult(results.AsEnumerable());
        }

        private IEnumerable<DatabaseScript> GenerateCountRecordsScripts()
        {
            var results = new List<DatabaseScript>();

            if (_databaseTestOptions.DatabaseRecordsCountRules != null)
            {
                results.AddRange(GetRecordCountScripts());
            }

            return results;
        }

        private IEnumerable<DatabaseScript> GetRecordCountScripts()
        {
            var results = new List<DatabaseScript>();

            foreach (var record in _databaseTestOptions.DatabaseRecordsCountRules)
            {
                results.Add(new DatabaseScript(
                    record.TableName,
                    GetRecordCountScript(record),
                    DatabaseTestType.RecordCount,
                    DatabaseKind));
            }

            return results;
        }

        private IEnumerable<DatabaseScript> GenerateRecordsExistScripts()
        {
            var results = new List<DatabaseScript>();

            if (_databaseTestOptions.DatabaseRecordsExitsRules != null)
            {
                results.AddRange(GetRecordsExistScripts());
            }

            return results;
        }

        private IEnumerable<DatabaseScript> GetRecordsExistScripts()
        {
            var results = new List<DatabaseScript>();

            foreach (var record in _databaseTestOptions.DatabaseRecordsExitsRules)
            {
                results.Add(new DatabaseScript(
                    record.TableName,
                    GetRecordExistScript(record),
                    DatabaseTestType.RecordExist,
                    DatabaseKind));
            }

            return results;
        }

        private IEnumerable<DatabaseScript> GenerateObjectExistScripts()
        {
            var results = new List<DatabaseScript>();

            if (_databaseTestOptions.DatabaseObjectsExistRules != null)
            {
                results.AddRange(GetTableExistScripts());
                results.AddRange(GetViewExistScripts());
                results.AddRange(GetStoredProcedureExistScripts());
            }

            return results;
        }

        private IEnumerable<DatabaseScript> GetTableExistScripts()
        {
            var results = new List<DatabaseScript>();

            _databaseTestOptions.DatabaseObjectsExistRules.TryGetValue(DatabaseObjectType.Table, out var tableValues);

            if (tableValues != null)
            {
                foreach (var table in tableValues)
                {
                    results.Add(new DatabaseScript(
                        table,
                        GetTableExistScript(table),
                        DatabaseTestType.ObjectExist,
                        DatabaseKind));
                }
            }

            return results;
        }

        private IEnumerable<DatabaseScript> GetViewExistScripts()
        {
            var results = new List<DatabaseScript>();

            _databaseTestOptions.DatabaseObjectsExistRules.TryGetValue(DatabaseObjectType.View, out var viewValues);

            if (viewValues != null)
            {
                foreach (var view in viewValues)
                {
                    results.Add(new DatabaseScript(
                        view,
                        GetViewExistScript(view),
                        DatabaseTestType.ObjectExist,
                        DatabaseKind));
                }
            }

            return results;
        }

        private IEnumerable<DatabaseScript> GetStoredProcedureExistScripts()
        {
            var results = new List<DatabaseScript>();

            _databaseTestOptions.DatabaseObjectsExistRules.TryGetValue(DatabaseObjectType.StoredProcedure, out var storedProcedureValues);

            if (storedProcedureValues != null)
            {
                foreach (var storedProcedure in storedProcedureValues)
                {
                    results.Add(new DatabaseScript(
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
        protected abstract string GetRecordExistScript(DatabaseRule recordExist);

        /// <summary>
        /// Get script to count the records in a table
        /// </summary>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        protected abstract string GetRecordCountScript(DatabaseRule recordCount);
    }
}
