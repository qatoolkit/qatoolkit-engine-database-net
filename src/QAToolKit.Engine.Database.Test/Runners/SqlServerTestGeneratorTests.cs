using ExpectedObjects;
using NSubstitute;
using QAToolKit.Engine.Database.Generators;
using QAToolKit.Engine.Database.Models;
using QAToolKit.Engine.Database.Runners;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace QAToolKit.Engine.Database.Test.Runners
{
    public class SqlServerTestRunnerTests
    {
        [Fact]
        public async Task MysqlCreateRunnerTest_Success()
        {
            var generator = new SqlServerTestGenerator(options =>
            {
                options.AddDatabaseObjectExitsRule(new string[] { "mytable" }, DatabaseObjectType.Table);
            });

            var script = await generator.Generate();

            var options = Substitute.For<Action<DatabaseTestRunnerOptions>>();
            var runner = Substitute.For<SqlServerTestRunner>(script, options);

            Assert.NotNull(runner);
        }
    }
}
