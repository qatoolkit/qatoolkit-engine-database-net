﻿using QAToolKit.Engine.Database.Models;
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
    }
}
