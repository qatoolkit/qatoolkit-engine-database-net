using System;
using System.Collections.Generic;
using QAToolKit.Engine.Database.Interfaces;
using QAToolKit.Engine.Database.Models;

namespace QAToolKit.Engine.Database.Runners.Factories
{
    /// <summary>
    /// SqlServer test runner
    /// </summary>
    public class PostgresqlTestRunnerFactory: TestRunnerFactory
    {
        /// <summary>
        /// SqServer test runner instance
        /// </summary>
        /// <param name="databaseTests"></param>
        /// <param name="options"></param>
        public PostgresqlTestRunnerFactory(IEnumerable<Test> databaseTests,
            Action<TestRunnerOptions> options): base(databaseTests, options)
        {}

        /// <summary>
        /// Get PostgreSQl test runner
        /// </summary>
        /// <returns></returns>
        protected override ISqlRunner GetRunner()
        {
            return new PostgresqlTestRunner();
        }
    }
}
