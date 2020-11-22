using QAToolKit.Engine.Database.Interfaces;
using QAToolKit.Engine.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QAToolKit.Engine.Database.Generators
{
    /// <summary>
    /// SqlServer database test generator
    /// </summary>
    public class SqlServerTestGenerator : IDatabaseTestGenerator<DatabaseScript>
    {
        private readonly DatabaseTestGeneratorOptions _databaseTestOptions;
        /// <summary>
        /// Database kind/type
        /// </summary>
        public DatabaseKind DatabaseKind => DatabaseKind.SQLServer;

        /// <summary>
        /// Create new instance of SqlServer script generator
        /// </summary>
        /// <param name="options"></param>
        public SqlServerTestGenerator(Action<DatabaseTestGeneratorOptions> options = null)
        {
            _databaseTestOptions = new DatabaseTestGeneratorOptions();
            options?.Invoke(_databaseTestOptions);
        }

        /// <summary>
        /// Generate SqlServer database test scripts
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<DatabaseScript>> Generate()
        {
            if (_databaseTestOptions == null)
            {
                throw new ArgumentNullException($"DatabaseTestOptions is null.");
            }

            var results = new List<DatabaseScript>();

            results.AddRange(GenerateObjectExistScripts());

            return Task.FromResult(results.AsEnumerable());
        }

        private IEnumerable<DatabaseScript> GenerateObjectExistScripts()
        {
            if (_databaseTestOptions.DatabaseObjectsExistRules == null)
            {
                throw new ArgumentNullException($"{nameof(_databaseTestOptions.DatabaseObjectsExistRules)} is null.");
            }

            var results = new List<DatabaseScript>();

            _databaseTestOptions.DatabaseObjectsExistRules.TryGetValue(DatabaseObjectType.Table, out var tableValues);

            if (tableValues != null)
            {
                foreach (var table in tableValues)
                {
                    results.Add(new DatabaseScript(
                        table,
                        $@"IF EXISTS(SELECT 1 FROM sys.tables WHERE Name = '{table}') BEGIN Select 1 END ELSE BEGIN Select 0 END",
                        DatabaseTestType.ObjectExist,
                        DatabaseKind));
                }
            }

            _databaseTestOptions.DatabaseObjectsExistRules.TryGetValue(DatabaseObjectType.View, out var viewValues);

            if (viewValues != null)
            {
                foreach (var view in viewValues)
                {
                    results.Add(new DatabaseScript(
                        view,
                        $@"IF EXISTS(SELECT 1 FROM sys.views WHERE Name = '{view}') BEGIN Select 1 END ELSE BEGIN Select 0 END",
                        DatabaseTestType.ObjectExist,
                        DatabaseKind));
                }
            }

            _databaseTestOptions.DatabaseObjectsExistRules.TryGetValue(DatabaseObjectType.StoredProcedure, out var storedProcedureValues);

            if (storedProcedureValues != null)
            {
                foreach (var storedProcedure in storedProcedureValues)
                {
                    results.Add(new DatabaseScript(
                        storedProcedure,
                        $@"IF EXISTS(SELECT 1 FROM sys.procedures WHERE Name = '{storedProcedure}') BEGIN Select 1 END ELSE BEGIN Select 0 END",
                        DatabaseTestType.ObjectExist,
                        DatabaseKind));
                }
            }

            return results;
        }
    }
}
