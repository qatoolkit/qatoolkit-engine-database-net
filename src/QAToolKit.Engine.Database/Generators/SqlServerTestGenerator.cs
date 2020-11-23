using QAToolKit.Engine.Database.Models;
using System;

namespace QAToolKit.Engine.Database.Generators
{
    /// <summary>
    /// SqlServer database test generator
    /// </summary>
    public class SqlServerTestGenerator : RelationalDatabaseTestGenerator
    {
        /// <summary>
        /// Create new instance of SqlServer script generator
        /// </summary>
        /// <param name="options"></param>
        public SqlServerTestGenerator(Action<DatabaseTestGeneratorOptions> options = null) :
            base(DatabaseKind.SQLServer, options)
        { }

        /// <summary>
        /// Get SQL server script for table exists abstract method
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        protected override string GetTableExistScript(string table)
        {
            return $@"IF EXISTS(SELECT 1 FROM sys.tables WHERE Name = '{table}') BEGIN Select 1 END ELSE BEGIN Select 0 END";
        }

        /// <summary>
        /// Get SQLServer script for view exists abstract method
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        protected override string GetViewExistScript(string view)
        {
            return $@"IF EXISTS(SELECT 1 FROM sys.views WHERE Name = '{view}') BEGIN Select 1 END ELSE BEGIN Select 0 END";
        }

        /// <summary>
        /// Get SQLServer script for stored procedure exists abstract method
        /// </summary>
        /// <param name="storedProcedure"></param>
        /// <returns></returns>
        protected override string GetStoredProcedureExistScript(string storedProcedure)
        {
            return $@"IF EXISTS(SELECT 1 FROM sys.procedures WHERE Name = '{storedProcedure}') BEGIN Select 1 END ELSE BEGIN Select 0 END";
        }
    }
}
