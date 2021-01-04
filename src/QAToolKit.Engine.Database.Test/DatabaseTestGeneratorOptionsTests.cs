using Microsoft.Extensions.Logging;
using QAToolKit.Engine.Database.Models;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace QAToolKit.Engine.Database.Test
{
    public class DatabaseTestGeneratorOptionsTests
    {
        private readonly ILogger<DatabaseTestGeneratorOptionsTests> _logger;

        public DatabaseTestGeneratorOptionsTests(ITestOutputHelper testOutputHelper)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(testOutputHelper));
            _logger = loggerFactory.CreateLogger<DatabaseTestGeneratorOptionsTests>();
        }

        [Fact]
        public void AddCustomSqlRuleTest_Successful()
        {
            var options = new DatabaseTestGeneratorOptions();
            options.AddCustomSqlRule(new List<string>() {
                "query 1",
                "query 2"
            });

            Assert.NotNull(options);
        }

        [Fact]
        public void AddCustomSqlRuleTest_Failure()
        {
            var options = new DatabaseTestGeneratorOptions();
            Assert.Throws<ArgumentNullException>(() => options.AddCustomSqlRule(null));
        }

        [Fact]
        public void AddDatabaseObjectExitsRuleTest_Successful()
        {
            var options = new DatabaseTestGeneratorOptions();
            options.AddDatabaseObjectExitsRule(new string[] { "sp1", "sp2" }, DatabaseObjectType.StoredProcedure);

            Assert.NotNull(options);
        }

        [Fact]
        public void AddDatabaseObjectExitsRuleTest_Failure()
        {
            var options = new DatabaseTestGeneratorOptions();
            Assert.Throws<ArgumentNullException>(() => options.AddDatabaseObjectExitsRule(null, DatabaseObjectType.StoredProcedure));
        }

        [Fact]
        public void AddDatabaseRecordExitsRuleTest_Successful()
        {
            var options = new DatabaseTestGeneratorOptions();
            options.AddDatabaseRecordExitsRule(
                new List<DatabaseRecordExistRule>()
                {
                        new DatabaseRecordExistRule()
                        {
                            TableName = "mytable",
                            ColumnName = "name",
                            Operator = "=",
                            Value = "myname"
                        }
                });

            Assert.NotNull(options);
        }

        [Fact]
        public void AddDatabaseRecordExitsRuleTest_Failure()
        {
            var options = new DatabaseTestGeneratorOptions();
            Assert.Throws<ArgumentNullException>(() => options.AddDatabaseRecordExitsRule(null));
        }

        [Fact]
        public void AddDatabaseRecordsCountRuleTest_Successful()
        {
            var options = new DatabaseTestGeneratorOptions();
            options.AddDatabaseRecordsCountRule(
                   new List<DatabaseRecordCountRule>()
                   {
                        new DatabaseRecordCountRule()
                        {
                            TableName = "mytable",
                            Operator = "=",
                            Count = 100
                        }
                   });

            Assert.NotNull(options);
        }

        [Fact]
        public void AddDatabaseRecordsCountRuleTest_Failure()
        {
            var options = new DatabaseTestGeneratorOptions();
            Assert.Throws<ArgumentNullException>(() => options.AddDatabaseRecordsCountRule(null));
        }
    }
}
