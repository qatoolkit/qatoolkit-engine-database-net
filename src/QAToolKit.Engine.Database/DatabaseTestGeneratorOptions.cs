using QAToolKit.Engine.Database.Models;
using System.Collections.Generic;

namespace QAToolKit.Engine.Database
{
    /// <summary>
    /// Database test generator options
    /// </summary>
    public class DatabaseTestGeneratorOptions
    {
        /// <summary>
        /// Database kind/type
        /// </summary>
        public DatabaseKind DatabaseKind { get; private set; } = DatabaseKind.Undefined;
        internal Dictionary<DatabaseObjectType, string[]> DatabaseObjectsExistRules { get; private set; }

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
        /// Use database type
        /// </summary>
        /// <param name="databaseKind"></param>
        /// <returns></returns>
        public DatabaseTestGeneratorOptions UseDatabase(DatabaseKind databaseKind)
        {
            DatabaseKind = databaseKind;

            return this;
        }
    }
}
