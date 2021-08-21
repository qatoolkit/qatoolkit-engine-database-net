using System;
using System.Collections.Generic;
using QAToolKit.Engine.Database.Interfaces;
using QAToolKit.Engine.Database.Models;

namespace QAToolKit.Engine.Database.Runners.Factories
{
    /// <summary>
    /// SqlServer test runner
    /// </summary>
    public class SqlServerTestRunnerFactory: TestRunnerFactory
    {
        /// <summary>
        /// SqServer test runner instance
        /// </summary>
        /// <param name="databaseTests"></param>
        /// <param name="options"></param>
        public SqlServerTestRunnerFactory(IEnumerable<Test> databaseTests,
            Action<TestRunnerOptions> options): base(databaseTests, options)
        {}

        /// <summary>
        /// Get SqlServer test runner
        /// </summary>
        /// <returns></returns>
        protected override ISqlRunner GetRunner()
        {
            return new SqlServerTestRunner();
        }
    }
}