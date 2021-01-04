using QAToolKit.Engine.Database.Models;
using System;
using System.Collections.Generic;

namespace QAToolKit.Engine.Database
{
    /// <summary>
    /// Database test generator options
    /// </summary>
    public class DatabaseTestGeneratorOptions
    {
        internal Dictionary<DatabaseObjectType, string[]> DatabaseObjectsExistRules { get; private set; }
        internal List<DatabaseRecordCountRule> DatabaseRecordsCountRules { get; private set; }
        internal List<DatabaseRecordExistRule> DatabaseRecordsExitsRules { get; private set; }
        internal List<string> CustomSqlRules { get; private set; }

        /// <summary>
        /// Add database object exist rules
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="databaseObjectType"></param>
        /// <returns></returns>
        public DatabaseTestGeneratorOptions AddDatabaseObjectExitsRule(string[] objects, DatabaseObjectType databaseObjectType)
        {
            if (objects == null)
                throw new ArgumentNullException($"{nameof(objects)} is null.");

            if (DatabaseObjectsExistRules == null)
            {
                DatabaseObjectsExistRules = new Dictionary<DatabaseObjectType, string[]>();
            }

            DatabaseObjectsExistRules.Add(databaseObjectType, objects);

            return this;
        }

        /// <summary>
        /// Add database record count rules
        /// </summary>
        /// <param name="objects"></param>
        /// <returns></returns>
        public DatabaseTestGeneratorOptions AddDatabaseRecordsCountRule(List<DatabaseRecordCountRule> objects)
        {
            if (objects == null)
                throw new ArgumentNullException($"{nameof(objects)} is null.");

            if (DatabaseRecordsCountRules == null)
            {
                DatabaseRecordsCountRules = new List<DatabaseRecordCountRule>();
            }

            DatabaseRecordsCountRules.AddRange(objects);

            return this;
        }

        /// <summary>
        /// Add database record exsist rules
        /// </summary>
        /// <param name="objects"></param>
        /// <returns></returns>
        public DatabaseTestGeneratorOptions AddDatabaseRecordExitsRule(List<DatabaseRecordExistRule> objects)
        {
            if (objects == null)
                throw new ArgumentNullException($"{nameof(objects)} is null.");

            if (DatabaseRecordsExitsRules == null)
            {
                DatabaseRecordsExitsRules = new List<DatabaseRecordExistRule>();
            }

            DatabaseRecordsExitsRules.AddRange(objects);

            return this;
        }

        /// <summary>
        /// Add custom sql rule
        /// </summary>
        /// <param name="sqlStatements"></param>
        /// <returns></returns>
        public DatabaseTestGeneratorOptions AddCustomSqlRule(List<string> sqlStatements)
        {
            if (sqlStatements == null)
                throw new ArgumentNullException($"{nameof(sqlStatements)} is null.");

            if (CustomSqlRules == null)
            {
                CustomSqlRules = new List<string>();
            }

            CustomSqlRules.AddRange(sqlStatements);

            return this;
        }
    }
}
