using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using QAToolKit.Engine.Database.Models;

namespace QAToolKit.Engine.Database.Interfaces
{
    /// <summary>
    /// ITester asserter interface
    /// </summary>
    public interface ITestAsserter
    {
        /// <summary>
        /// Evaluate Server CPU time needed for the query
        /// </summary>
        /// <param name="predicateFunction"></param>
        /// <returns></returns>
        ITestAsserter EvaluateServerCpuTime(Expression<Func<long, bool>> predicateFunction);
        /// <summary>
        /// Evaluate Server server elapsed time needed for the query to finish
        /// </summary>
        /// <param name="predicateFunction"></param>
        /// <returns></returns>
        ITestAsserter EvaluateServerElapsedTime(Expression<Func<long, bool>> predicateFunction);
        /// <summary>
        /// Evaluate Total elapsed time needed for the query to finish with network
        /// </summary>
        /// <param name="predicateFunction"></param>
        /// <returns></returns>
        ITestAsserter EvaluateTotalElapsedTime(Expression<Func<long, bool>> predicateFunction);
        /// <summary>
        /// Evaluate SQL server logical reads for the executing query
        /// </summary>
        /// <param name="databaseObjectName"></param>
        /// <param name="predicateFunction"></param>
        /// <returns></returns>
        ITestAsserter EvaluateLogicalReads(Expression<Func<long, bool>> predicateFunction, string databaseObjectName = null);
        /// <summary>
        /// Add custom property to assert results
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        ITestAsserter WithCustomProperty(KeyValuePair<string, string> property);
        /// <summary>
        /// Assert all and return results
        /// </summary>
        /// <returns></returns>
        IEnumerable<AssertResult> AssertAll();
    }
}