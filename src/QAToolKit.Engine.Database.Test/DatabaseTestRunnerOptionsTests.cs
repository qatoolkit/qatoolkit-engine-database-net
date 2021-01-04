using Microsoft.Extensions.Logging;
using System;
using Xunit;
using Xunit.Abstractions;

namespace QAToolKit.Engine.Database.Test
{
    public class DatabaseTestRunnerOptionsTests
    {
        private readonly ILogger<DatabaseTestRunnerOptionsTests> _logger;

        public DatabaseTestRunnerOptionsTests(ITestOutputHelper testOutputHelper)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(testOutputHelper));
            _logger = loggerFactory.CreateLogger<DatabaseTestRunnerOptionsTests>();
        }

        [Fact]
        public void AddMySQLConnectionTest_Successful()
        {
            var options = new DatabaseTestRunnerOptions();
            options.AddMySQLConnection("server=localhost;user=user;password=mypassword;Initial Catalog=myDatabase");

            Assert.NotNull(options);
        }

        [Fact]
        public void AddMySQLConnectionTest_Failure()
        {
            var options = new DatabaseTestRunnerOptions();
            Assert.Throws<ArgumentNullException>(() => options.AddMySQLConnection(null));
        }

        [Fact]
        public void AddPostgreSQLConnectionTest_Successful()
        {
            var options = new DatabaseTestRunnerOptions();
            options.AddPostgreSQLConnection("server=localhost;user=user;password=mypassword;Initial Catalog=myDatabase");

            Assert.NotNull(options);
        }

        [Fact]
        public void AddPostgreSQLConnectionTest_Failure()
        {
            var options = new DatabaseTestRunnerOptions();
            Assert.Throws<ArgumentNullException>(() => options.AddPostgreSQLConnection(null));
        }

        [Fact]
        public void AddSQLServerConnectionTest_Successful()
        {
            var options = new DatabaseTestRunnerOptions();
            options.AddSQLServerConnection("server=localhost;user=user;password=mypassword;Initial Catalog=myDatabase");

            Assert.NotNull(options);
        }

        [Fact]
        public void AddSQLServerConnectionTest_Failure()
        {
            var options = new DatabaseTestRunnerOptions();
            Assert.Throws<ArgumentNullException>(() => options.AddSQLServerConnection(null));
        }
    }
}
