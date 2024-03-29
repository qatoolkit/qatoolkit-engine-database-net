﻿using QAToolKit.Engine.Database.Models;
using SqlKata;
using SqlKata.Compilers;
using System;
using System.Linq;

namespace QAToolKit.Engine.Database.Generators
{
    /// <summary>
    /// SqlServer database test generator
    /// </summary>
    public class SqlServerTestGenerator : RelationalDatabaseTestGenerator
    {
        private readonly SqlServerCompiler _sqlServerCompiler;

        /// <summary>
        /// Create new instance of SqlServer script generator
        /// </summary>
        /// <param name="options"></param>
        public SqlServerTestGenerator(Action<TestGeneratorOptions> options = null) :
            base(DatabaseKind.SqlServer, options)
        {
            _sqlServerCompiler = new SqlServerCompiler();
        }

        /// <summary>
        /// Get SQL server script for table exists abstract method
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        protected override string GetTableExistScript(string table)
        {
            var query = new Query("sys.tables").Select("*").Where("Name", table);
            var result = _sqlServerCompiler.Compile(query);
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
            var result = _sqlServerCompiler.Compile(query);
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
            var result = _sqlServerCompiler.Compile(query);
            return $"IF EXISTS ({result}) BEGIN Select 1 END ELSE BEGIN Select 0 END;";
        }

        /// <summary>
        /// Get SQLServer script to check if record exist
        /// </summary>
        /// <param name="recordExist"></param>
        /// <returns></returns>
        protected override string GetRecordExistScript(RecordExistRule recordExist)
        {
            var query = new Query(recordExist.TableName)
                .Select("*")
                .Where(recordExist.ColumnName, recordExist.Operator, recordExist.Value);

            var result = _sqlServerCompiler.Compile(query);

            return $"IF EXISTS ({result}) BEGIN Select 1 END ELSE BEGIN Select 0 END;";
        }

        /// <summary>
        /// Get SQLServer script to count the records in a table
        /// </summary>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        protected override string GetRecordCountScript(RecordCountRule recordCount)
        {
            var countQuery = new Query(recordCount.TableName).AsCount();

            var query = new Query(recordCount.TableName).Select("*")
                .WhereRaw($"({_sqlServerCompiler.Compile(countQuery)}) {recordCount.Operator} {recordCount.Count}");

            var result = _sqlServerCompiler.Compile(query);

            return $"IF EXISTS ({result}) BEGIN Select 1 END ELSE BEGIN Select 0 END;";
        }

        /// <summary>
        /// Get SQLServer custom script
        /// </summary>
        /// <param name="script"></param>
        /// <returns></returns>
        protected override string GetCustomScript(string script)
        {
            return $"IF EXISTS ({script}) BEGIN Select 1 END ELSE BEGIN Select 0 END;";
        }

        /// <summary>
        /// Get SQLServer custom query statistics script
        /// </summary>
        /// <param name="script"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        protected override string GetQueryStatisticsScript(string script, QueryStatisticsType[] types)
        {
            var result = "";

            if (types.Contains(QueryStatisticsType.Time))
            {
                result += "SET STATISTICS TIME ON;";
            }

            if (types.Contains(QueryStatisticsType.Io))
            {
                result += "SET STATISTICS IO ON;";
            }

            result += script;

            if (!result.EndsWith(";"))
            {
                result += ";";
            }

            if (types.Contains(QueryStatisticsType.Time))
            {
                result += "SET STATISTICS TIME OFF;";
            }

            if (types.Contains(QueryStatisticsType.Io))
            {
                result += "SET STATISTICS IO OFF;";
            }

            return result;
        }
    }
}