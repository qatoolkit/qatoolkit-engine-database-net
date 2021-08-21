using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QAToolKit.Engine.Database.Interfaces;
using QAToolKit.Engine.Database.Models;

namespace QAToolKit.Engine.Database.Runners.Factories
{
    /// <summary>
    /// Test runner
    /// </summary>
    public abstract class TestRunnerFactory
    {
        private readonly TestRunnerOptions _databaseTestOptions;
        private readonly IEnumerable<Test> _databaseTests;

        /// <summary>
        /// SqServer test runner instance
        /// </summary>
        /// <param name="databaseTests"></param>
        /// <param name="options"></param>
        public TestRunnerFactory(IEnumerable<Test> databaseTests, Action<TestRunnerOptions> options)
        {
            _databaseTestOptions = new TestRunnerOptions();
            options?.Invoke(_databaseTestOptions);
            _databaseTests = databaseTests ?? throw new ArgumentNullException($"{nameof(databaseTests)} is null.");
        }

        /// <summary>
        /// Run tests
        /// </summary>
        /// <param name="parallelize">max 10</param>
        /// <returns></returns>
        public async Task<IEnumerable<TestResult>> Run(short parallelize = 1)
        {
            if (parallelize > 10 || parallelize < 1)
                throw new ArgumentException("Parallelization must be between 1 and 10.");

            var results = new BlockingCollection<TestResult>();

            var batchCount = (int) Math.Ceiling((double) _databaseTests.Count() / parallelize);

            for (var i = 0; i < batchCount; i++)
            {
                var parallelRunners = _databaseTests.Skip(i * parallelize).Take(parallelize);
                var tasks = parallelRunners.Select(script =>
                {
                    var runner = GetRunner();
                    return runner.Run(script, _databaseTestOptions);
                });

                var batchResult = await Task.WhenAll(tasks);

                foreach (var result in batchResult)
                {
                    results.Add(result);
                }
            }

            return results;
        }

        /// <summary>
        /// Get runner instance
        /// </summary>
        /// <returns></returns>
        protected abstract ISqlRunner GetRunner();
    }
}