using QAToolKit.Engine.Database.Models;
using System;
using System.Collections.Generic;

namespace QAToolKit.Engine.Database
{
    /// <summary>
    /// Database test generator options
    /// </summary>
    public class TestGeneratorOptions
    {
        internal Dictionary<DatabaseObjectType, string[]> DatabaseObjectsExistRules { get; private set; }
        internal List<RecordCountRule> DatabaseRecordsCountRules { get; private set; }
        internal List<RecordExistRule> DatabaseRecordsExitsRules { get; private set; }
        internal List<string> CustomSqlRules { get; private set; }
        internal List<QueryStatisticsTask> QueryStatisticsTasks { get; private set; }

        /// <summary>
        /// Add database object exist rules
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="databaseObjectType"></param>
        /// <returns></returns>
        public TestGeneratorOptions AddDatabaseObjectExitsRule(string[] objects, DatabaseObjectType databaseObjectType)
        {
            if (objects == null)
                throw new ArgumentNullException($"{nameof(objects)} is null.");

            DatabaseObjectsExistRules ??= new Dictionary<DatabaseObjectType, string[]>();

            DatabaseObjectsExistRules.Add(databaseObjectType, objects);

            return this;
        }

        /// <summary>
        /// Add database record count rules
        /// </summary>
        /// <param name="objects"></param>
        /// <returns></returns>
        public TestGeneratorOptions AddDatabaseRecordsCountRule(List<RecordCountRule> objects)
        {
            if (objects == null)
                throw new ArgumentNullException($"{nameof(objects)} is null.");

            DatabaseRecordsCountRules ??= new List<RecordCountRule>();

            DatabaseRecordsCountRules.AddRange(objects);

            return this;
        }

        /// <summary>
        /// Add database record exsist rules
        /// </summary>
        /// <param name="objects"></param>
        /// <returns></returns>
        public TestGeneratorOptions AddDatabaseRecordExitsRule(List<RecordExistRule> objects)
        {
            if (objects == null)
                throw new ArgumentNullException($"{nameof(objects)} is null.");

            DatabaseRecordsExitsRules ??= new List<RecordExistRule>();

            DatabaseRecordsExitsRules.AddRange(objects);

            return this;
        }

        /// <summary>
        /// Add custom sql rule
        /// </summary>
        /// <param name="sqlStatements"></param>
        /// <returns></returns>
        public TestGeneratorOptions AddCustomSqlRule(List<string> sqlStatements)
        {
            if (sqlStatements == null)
                throw new ArgumentNullException($"{nameof(sqlStatements)} is null.");

            CustomSqlRules ??= new List<string>();

            CustomSqlRules.AddRange(sqlStatements);

            return this;
        }

        /// <summary>
        /// Add database query statistics rules
        /// </summary>
        /// <param name="queries"></param>
        /// <param name="statisticsTypes"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public TestGeneratorOptions CaptureQueryStatistics(string[] queries,
            QueryStatisticsType[] statisticsTypes)
        {
            if (queries == null)
                throw new ArgumentNullException($"{nameof(queries)} is null.");

            QueryStatisticsTasks ??= new List<QueryStatisticsTask>();

            QueryStatisticsTasks.Add(new QueryStatisticsTask(queries, statisticsTypes));

            return this;
        }
    }
}
