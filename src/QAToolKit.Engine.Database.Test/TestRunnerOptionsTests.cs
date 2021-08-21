using Microsoft.Extensions.Logging;
using System;
using Xunit;
using Xunit.Abstractions;

namespace QAToolKit.Engine.Database.Test
{
    public class TestRunnerOptionsTests
    {
        private readonly ILogger<TestRunnerOptionsTests> _logger;

        public TestRunnerOptionsTests(ITestOutputHelper testOutputHelper)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(testOutputHelper));
            _logger = loggerFactory.CreateLogger<TestRunnerOptionsTests>();
        }

        [Fact]
        public void AddMySQLConnectionTest_Successful()
        {
            var options = new TestRunnerOptions();
            options.AddMySQLConnection("server=localhost;user=user;password=mypassword;Initial Catalog=myDatabase");

            Assert.NotNull(options);
        }

        [Fact]
        public void AddMySQLConnectionTest_Failure()
        {
            var options = new TestRunnerOptions();
            Assert.Throws<ArgumentNullException>(() => options.AddMySQLConnection(null));
        }

        [Fact]
        public void AddPostgreSQLConnectionTest_Successful()
        {
            var options = new TestRunnerOptions();
            options.AddPostgreSQLConnection("server=localhost;user=user;password=mypassword;Initial Catalog=myDatabase");

            Assert.NotNull(options);
        }

        [Fact]
        public void AddPostgreSQLConnectionTest_Failure()
        {
            var options = new TestRunnerOptions();
            Assert.Throws<ArgumentNullException>(() => options.AddPostgreSQLConnection(null));
        }

        [Fact]
        public void AddSQLServerConnectionTest_Successful()
        {
            var options = new TestRunnerOptions();
            options.AddSQLServerConnection("server=localhost;user=user;password=mypassword;Initial Catalog=myDatabase");

            Assert.NotNull(options);
        }

        [Fact]
        public void AddSQLServerConnectionTest_Failure()
        {
            var options = new TestRunnerOptions();
            Assert.Throws<ArgumentNullException>(() => options.AddSQLServerConnection(null));
        }
    }
}
