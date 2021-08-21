using NSubstitute;
using QAToolKit.Engine.Database.Generators;
using QAToolKit.Engine.Database.Models;
using System;
using System.Threading.Tasks;
using QAToolKit.Engine.Database.Runners.Factories;
using Xunit;

namespace QAToolKit.Engine.Database.Test.Runners
{
    public class PostgresqlTestRunnerTests
    {
        [Fact]
        public async Task PostgresqlCreateRunnerTest_Success()
        {
            var generator = new PostgresqlTestGenerator(options =>
            {
                options.AddDatabaseObjectExitsRule(new string[] { "mytable" }, DatabaseObjectType.Table);
            });

            var script = await generator.Generate();

            var options = Substitute.For<Action<TestRunnerOptions>>();
            var runner = Substitute.For<PostgresqlTestRunnerFactory>(script, options);

            Assert.NotNull(runner);
        }
    }
}
