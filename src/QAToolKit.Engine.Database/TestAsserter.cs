using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using QAToolKit.Engine.Database.Interfaces;
using QAToolKit.Engine.Database.Models;

namespace QAToolKit.Engine.Database
{
    /// <summary>
    /// Database test asserter
    /// </summary>
    public class TestAsserter : ITestAsserter
    {
        private readonly TestResult _result;
        private readonly List<AssertResult> _assertResults;
        private readonly Dictionary<string, string> _customProperties;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        public TestAsserter(TestResult result)
        {
            _result = result;
            _assertResults = new List<AssertResult>();
            _customProperties = new Dictionary<string, string>();
        }

        /// <summary>
        /// Evaluate SQL server CPU time for the execution of the query
        /// </summary>
        /// <param name="predicateFunction"></param>
        /// <returns></returns>
        public ITestAsserter EvaluateServerCpuTime(Expression<Func<long, bool>> predicateFunction)
        {
            Evaluate(_result.ServerCpuTime, predicateFunction);

            return this;
        }

        /// <summary>
        /// Evaluate SQL server elapsed time for the execution of query
        /// </summary>
        /// <param name="predicateFunction"></param>
        /// <returns></returns>
        public ITestAsserter EvaluateServerElapsedTime(Expression<Func<long, bool>> predicateFunction)
        {
            Evaluate(_result.ServerElapsedTime, predicateFunction);

            return this;
        }

        /// <summary>
        /// Evaluate total elapsed time for the execution of the query
        /// </summary>
        /// <param name="predicateFunction"></param>
        /// <returns></returns>
        public ITestAsserter EvaluateTotalElapsedTime(Expression<Func<long, bool>> predicateFunction)
        {
            Evaluate(_result.TotalElapsedTime, predicateFunction);

            return this;
        }

        /// <summary>
        /// Evaluate database logical read for a database object (table,...) for the execution of the query
        /// </summary>
        /// <param name="databaseObjectName">Name of the database object you want to filter for evaluation</param>
        /// <param name="predicateFunction"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public ITestAsserter EvaluateLogicalReads(Expression<Func<long, bool>> predicateFunction, string databaseObjectName = null)
        {
            KeyValuePair<string, Dictionary<string, long>> dbObject;
            if (string.IsNullOrEmpty(databaseObjectName))
            {
                dbObject = _result.Statistics.FirstOrDefault();
                databaseObjectName = dbObject.Key;
            }
            else
            {
                dbObject = _result.Statistics.FirstOrDefault(x => x.Key == databaseObjectName);
            }

            if (dbObject.Value == null)
            {
                throw new NullReferenceException("DatabaseObject is null.");
            }

            dbObject.Value.TryGetValue("LogicalReads", out var value);
            Evaluate(value, databaseObjectName, predicateFunction);

            return this;
        }

        /// <summary>
        /// Add custom property to all test asserts
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public ITestAsserter WithCustomProperty(KeyValuePair<string, string> property)
        {
            _customProperties.Add(property.Key, property.Value);

            return this;
        }

        /// <summary>
        /// Return all Assert messages of the Asserter
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AssertResult> AssertAll()
        {
            foreach (var assertResult in _assertResults)
            {
                assertResult.CustomProperties = _customProperties;
            }
            
            return _assertResults;
        }
        
        private void Evaluate(long value, string name, Expression<Func<long, bool>> predicateFunction,
            [CallerMemberName] string memberName = "")
        {
            var isTrue = predicateFunction.Compile().Invoke(value);

            _assertResults.Add(new AssertResult()
            {
                Name = memberName,
                Message = $"Value '{memberName} is {value}' for database object '{name}' evaluated with {predicateFunction}.",
                IsTrue = isTrue
            });
        }

        private void Evaluate(long value, Expression<Func<long, bool>> predicateFunction,
            [CallerMemberName] string memberName = "")
        {
            var isTrue = predicateFunction.Compile().Invoke(value);

            _assertResults.Add(new AssertResult()
            {
                Name = memberName,
                Message = $"Value '{memberName} is {value}' evaluated with {predicateFunction}.",
                IsTrue = isTrue
            });
        }
    }
}