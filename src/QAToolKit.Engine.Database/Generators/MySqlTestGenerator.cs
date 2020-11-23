using QAToolKit.Engine.Database.Models;
using System;

namespace QAToolKit.Engine.Database.Generators
{
    /// <summary>
    /// MySQL database test generator
    /// </summary>
    public class MySqlTestGenerator : RelationalDatabaseTestGenerator
    {
        /// <summary>
        /// Create new instance of MySQL script generator
        /// </summary>
        /// <param name="options"></param>        
        public MySqlTestGenerator(Action<DatabaseTestGeneratorOptions> options = null) :
            base(DatabaseKind.MySQL, options)
        { }

        /// <summary>
        /// Get MySQl script for table exists abstract method
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        protected override string GetTableExistScript(string table)
        {
            return $@"SELECT EXISTS(SELECT * FROM information_schema.tables WHERE table_name = '{table}');";
        }

        /// <summary>
        /// Get MySQL script for view exists abstract method
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        protected override string GetViewExistScript(string view)
        {
            return $@"SELECT EXISTS(SELECT * FROM information_schema.views WHERE table_name = '{view}');";
        }

        /// <summary>
        /// Get MySQL script for stored procedure exists abstract method
        /// </summary>
        /// <param name="storedProcedure"></param>
        /// <returns></returns>
        protected override string GetStoredProcedureExistScript(string storedProcedure)
        {
            return $@"SELECT EXISTS(SELECT * FROM information_schema.routines WHERE routine_name = '{storedProcedure}');";
        }

        /// <summary>
        /// Get MySQL script to check if record exist
        /// </summary>
        /// <param name="recordExist"></param>
        /// <returns></returns>
        protected override string GetRecordExistScript(DatabaseRule recordExist)
        {
            return $@"SELECT EXISTS (SELECT 1 FROM {recordExist.TableName} WHERE {recordExist.PredicateValue});";
        }

        /// <summary>
        /// Get MySQL script to count the records in a table
        /// </summary>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        protected override string GetRecordCountScript(DatabaseRule recordCount)
        {
            return $@"SELECT EXISTS (SELECT 1 FROM {recordCount.TableName} WHERE (SELECT count(*) FROM {recordCount.TableName}){recordCount.PredicateValue});";
        }
    }
}
