namespace QAToolKit.Engine.Database.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class QueryStatisticsTask
    {
        /// <summary>
        /// Create new query statistics task
        /// </summary>
        /// <param name="queries"></param>
        /// <param name="queryStatisticsTypes"></param>
        public QueryStatisticsTask(
            string[] queries, 
            QueryStatisticsType[] queryStatisticsTypes)
        {
            Queries = queries;
            QueryStatisticsTypes = queryStatisticsTypes;
        }

        /// <summary>
        /// A list of queries to measure
        /// </summary>
        public string[] Queries { get; private set; }
        /// <summary>
        /// Capture specific query statistic types
        /// </summary>
        public QueryStatisticsType[] QueryStatisticsTypes { get; private set; }
    }
}