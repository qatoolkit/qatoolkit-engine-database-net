﻿using QAToolKit.Engine.Database.Models;
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

        /// <summary>
        /// Add database object exist rules
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="databaseObjectType"></param>
        /// <returns></returns>
        public DatabaseTestGeneratorOptions AddDatabaseObjectExitsRule(string[] objects, DatabaseObjectType databaseObjectType)
        {
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
            if (DatabaseRecordsCountRules == null)
            {
                DatabaseRecordsCountRules = new List<DatabaseRecordCountRule>();
            }

            DatabaseRecordsCountRules.AddRange(objects);

            return this;
        }

        /// <summary>
        /// Add database record exsit rules
        /// </summary>
        /// <param name="objects"></param>
        /// <returns></returns>
        public DatabaseTestGeneratorOptions AddDatabaseRecordExitsRule(List<DatabaseRecordExistRule> objects)
        {
            if (DatabaseRecordsExitsRules == null)
            {
                DatabaseRecordsExitsRules = new List<DatabaseRecordExistRule>();
            }

            DatabaseRecordsExitsRules.AddRange(objects);

            return this;
        }
    }
}
