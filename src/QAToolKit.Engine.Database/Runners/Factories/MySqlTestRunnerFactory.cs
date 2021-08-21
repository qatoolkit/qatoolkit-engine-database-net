using System;
using System.Collections.Generic;
using QAToolKit.Engine.Database.Interfaces;
using QAToolKit.Engine.Database.Models;

namespace QAToolKit.Engine.Database.Runners.Factories
{
    /// <summary>
    /// SqlServer test runner
    /// </summary>
    public class MySqlTestRunnerFactory: TestRunnerFactory
    {
        /// <summary>
        /// SqServer test runner instance
        /// </summary>
        /// <param name="databaseTests"></param>
        /// <param name="options"></param>
        public MySqlTestRunnerFactory(IEnumerable<Test> databaseTests,
            Action<TestRunnerOptions> options): base(databaseTests, options)
        { }

        /// <summary>
        /// Get Mysql test runner
        /// </summary>
        /// <returns></returns>
        protected override ISqlRunner GetRunner()
        {
            return new MySqlTestRunner();
        }
    }
}
