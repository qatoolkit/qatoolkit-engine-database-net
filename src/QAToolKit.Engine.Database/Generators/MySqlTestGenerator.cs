using QAToolKit.Engine.Database.Models;
using System;
using SqlKata;
using SqlKata.Compilers;
using SqlKata.Extensions;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QAToolKit.Engine.Database.Generators
{
    /// <summary>
    /// MySQL database test generator
    /// </summary>
    public class MySqlTestGenerator : RelationalDatabaseTestGenerator
    {
        private readonly MySqlCompiler mySqlCompiler;

        /// <summary>
        /// Create new instance of MySQL script generator
        /// </summary>
        /// <param name="options"></param>        
        public MySqlTestGenerator(Action<DatabaseTestGeneratorOptions> options = null) :
            base(DatabaseKind.MySQL, options)
        {
            mySqlCompiler = new MySqlCompiler();
        }

        /// <summary>
        /// Get MySQl script for table exists abstract method
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        protected override string GetTableExistScript(string table)
        {
            var query = new Query("information_schema.tables").Select("*").Where("table_name", table);
            var result = mySqlCompiler.Compile(query);

            return $@"SELECT EXISTS({result});";
        }

        /// <summary>
        /// Get MySQL script for view exists abstract method
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        protected override string GetViewExistScript(string view)
        {
            var query = new Query("information_schema.views").Select("*").Where("table_name", view);
            var result = mySqlCompiler.Compile(query);

            return $@"SELECT EXISTS({result});";
        }

        /// <summary>
        /// Get MySQL script for stored procedure exists abstract method
        /// </summary>
        /// <param name="storedProcedure"></param>
        /// <returns></returns>
        protected override string GetStoredProcedureExistScript(string storedProcedure)
        {
            var query = new Query("information_schema.routines").Select("*").Where("routine_name", storedProcedure);
            var result = mySqlCompiler.Compile(query);

            return $@"SELECT EXISTS({result});";
        }

        /// <summary>
        /// Get MySQL script to check if record exist
        /// </summary>
        /// <param name="recordExist"></param>
        /// <returns></returns>
        protected override string GetRecordExistScript(DatabaseRecordExistRule recordExist)
        {
            var query = new Query(recordExist.TableName)
                .Select("*")
                .Where(recordExist.ColumnName, recordExist.Operator, recordExist.Value);

            var result = mySqlCompiler.Compile(query);

            return $@"SELECT EXISTS({result});";
        }

        /// <summary>
        /// Get MySQL script to count the records in a table
        /// </summary>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        protected override string GetRecordCountScript(DatabaseRecordCountRule recordCount)
        {
            var countQuery = new Query(recordCount.TableName).AsCount();

            var query = new Query(recordCount.TableName).Select("*").WhereRaw($"({mySqlCompiler.Compile(countQuery)}) {recordCount.Operator} {recordCount.Count}");

            var result = mySqlCompiler.Compile(query);

            return $"SELECT EXISTS ({result});";
        }
    }
}
