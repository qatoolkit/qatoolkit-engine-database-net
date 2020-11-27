using QAToolKit.Engine.Database.Models;
using SqlKata;
using SqlKata.Compilers;
using System;

namespace QAToolKit.Engine.Database.Generators
{
    /// <summary>
    /// SqlServer database test generator
    /// </summary>
    public class SqlServerTestGenerator : RelationalDatabaseTestGenerator
    {
        private readonly SqlServerCompiler sqlServerCompiler;

        /// <summary>
        /// Create new instance of SqlServer script generator
        /// </summary>
        /// <param name="options"></param>
        public SqlServerTestGenerator(Action<DatabaseTestGeneratorOptions> options = null) :
            base(DatabaseKind.SQLServer, options)
        {
            sqlServerCompiler = new SqlServerCompiler();
        }

        /// <summary>
        /// Get SQL server script for table exists abstract method
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        protected override string GetTableExistScript(string table)
        {
            var query = new Query("sys.tables").Select("*").Where("Name", table);
            var result = sqlServerCompiler.Compile(query);
            return $"IF EXISTS ({result}) BEGIN Select 1 END ELSE BEGIN Select 0 END;";
        }

        /// <summary>
        /// Get SQLServer script for view exists abstract method
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        protected override string GetViewExistScript(string view)
        {
            var query = new Query("sys.views").Select("*").Where("Name", view);
            var result = sqlServerCompiler.Compile(query);
            return $"IF EXISTS ({result}) BEGIN Select 1 END ELSE BEGIN Select 0 END;";
        }

        /// <summary>
        /// Get SQLServer script for stored procedure exists abstract method
        /// </summary>
        /// <param name="storedProcedure"></param>
        /// <returns></returns>
        protected override string GetStoredProcedureExistScript(string storedProcedure)
        {
            var query = new Query("sys.procedures").Select("*").Where("Name", storedProcedure);
            var result = sqlServerCompiler.Compile(query);
            return $"IF EXISTS ({result}) BEGIN Select 1 END ELSE BEGIN Select 0 END;";
        }

        /// <summary>
        /// Get SQLServer script to check if record exist
        /// </summary>
        /// <param name="recordExist"></param>
        /// <returns></returns>
        protected override string GetRecordExistScript(DatabaseRecordExistRule recordExist)
        {
            //return $@"IF EXISTS(SELECT 1 FROM {recordExist.TableName} WHERE {recordExist.PredicateValue}) BEGIN Select 1 END ELSE BEGIN Select 0 END";

            var query = new Query(recordExist.TableName)
                .Select("*")
                .Where(recordExist.ColumnName, recordExist.Operator, recordExist.Value);

            var result = sqlServerCompiler.Compile(query);

            return $"IF EXISTS ({result}) BEGIN Select 1 END ELSE BEGIN Select 0 END;";
        }

        /// <summary>
        /// Get SQLServer script to count the records in a table
        /// </summary>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        protected override string GetRecordCountScript(DatabaseRecordCountRule recordCount)
        {
            var countQuery = new Query(recordCount.TableName).AsCount();

            var query = new Query(recordCount.TableName).Select("*").WhereRaw($"({sqlServerCompiler.Compile(countQuery)}) {recordCount.Operator} {recordCount.Count}");

            var result = sqlServerCompiler.Compile(query);

            return $"IF EXISTS ({result}) BEGIN Select 1 END ELSE BEGIN Select 0 END;";
        }
    }
}
