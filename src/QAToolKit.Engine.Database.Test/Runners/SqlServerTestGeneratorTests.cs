using NSubstitute;
using QAToolKit.Engine.Database.Generators;
using QAToolKit.Engine.Database.Models;
using System;
using System.Threading.Tasks;
using QAToolKit.Engine.Database.Runners.Factories;
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

            var options = Substitute.For<Action<TestRunnerOptions>>();
            var runner = Substitute.For<SqlServerTestRunnerFactory>(script, options);

            Assert.NotNull(runner);
        }
    }
}
