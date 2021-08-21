using System;
using System.Collections.Generic;
using QAToolKit.Core.Helpers;

namespace QAToolKit.Engine.Database.Models
{
    /// <summary>
    /// Database test result
    /// </summary>
    public class TestResult
    {
        /// <summary>
        /// Run Id
        /// </summary>
        public Guid Id => Guid.NewGuid();

        /// <summary>
        /// Calculated Hash
        /// </summary>
        public string Hash => HashingHelper.GenerateStringHash(Script);
        /// <summary>
        /// When the query was run
        /// </summary>
        public DateTimeOffset RunAt => DateTimeOffset.Now;
        /// <summary>
        /// The result of the script test
        /// </summary>
        public bool? DatabaseResult { get; set; }
        /// <summary>
        /// Variable name of the object beiing tested
        /// </summary>
        public string Variable { get; set; }
        /// <summary>
        /// Script to run the test
        /// </summary>
        public string Script { get; set; }
        /// <summary>
        /// Databae test type
        /// </summary>
        public TestType DatabaseTestType { get; set; }
        /// <summary>
        /// Database kind
        /// </summary>
        public DatabaseKind DatabaseKind { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public long ServerCpuTime { get; private set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public long ServerElapsedTime { get; private set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public long TotalElapsedTime { get; private set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, Dictionary<string, long>> Statistics { get; private set; } = null;

        /// <summary>
        /// Create new instance of database script result object - extended
        /// </summary>
        /// <param name="databaseResult"></param>
        /// <param name="variable"></param>
        /// <param name="script"></param>
        /// <param name="databaseTestType"></param>
        /// <param name="databaseKind"></param>
        /// <param name="serverCpuTime"></param>
        /// <param name="serverElapsedTime"></param>
        /// <param name="totalElapsedTime"></param>
        /// <param name="statistics"></param>
        public TestResult(bool? databaseResult, string variable, string script, TestType databaseTestType,
            DatabaseKind databaseKind, long serverCpuTime, long serverElapsedTime, long totalElapsedTime,
            Dictionary<string, Dictionary<string, long>> statistics)
        {
            DatabaseResult = databaseResult;
            Variable = variable;
            Script = script;
            DatabaseTestType = databaseTestType;
            DatabaseKind = databaseKind;
            ServerCpuTime = serverCpuTime;
            ServerElapsedTime = serverElapsedTime;
            TotalElapsedTime = totalElapsedTime;
            Statistics = statistics;
        }
    }
}
