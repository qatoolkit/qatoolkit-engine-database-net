using Microsoft.Extensions.Logging;
using QAToolKit.Engine.Database.Models;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace QAToolKit.Engine.Database.Test
{
    public class TestGeneratorOptionsTests
    {
        private readonly ILogger<TestGeneratorOptionsTests> _logger;

        public TestGeneratorOptionsTests(ITestOutputHelper testOutputHelper)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(testOutputHelper));
            _logger = loggerFactory.CreateLogger<TestGeneratorOptionsTests>();
        }

        [Fact]
        public void AddCustomSqlRuleTest_Successful()
        {
            var options = new TestGeneratorOptions();
            options.AddCustomSqlRule(new List<string>()
            {
                "query 1",
                "query 2"
            });

            Assert.NotNull(options);
        }

        [Fact]
        public void AddCustomSqlRuleTest_Failure()
        {
            var options = new TestGeneratorOptions();
            Assert.Throws<ArgumentNullException>(() => options.AddCustomSqlRule(null));
        }

        [Fact]
        public void AddDatabaseObjectExitsRuleTest_Successful()
        {
            var options = new TestGeneratorOptions();
            options.AddDatabaseObjectExitsRule(new string[] {"sp1", "sp2"}, DatabaseObjectType.StoredProcedure);

            Assert.NotNull(options);
        }

        [Fact]
        public void AddDatabaseObjectExitsRuleTest_Failure()
        {
            var options = new TestGeneratorOptions();
            Assert.Throws<ArgumentNullException>(() =>
                options.AddDatabaseObjectExitsRule(null, DatabaseObjectType.StoredProcedure));
        }

        [Fact]
        public void AddDatabaseRecordExitsRuleTest_Successful()
        {
            var options = new TestGeneratorOptions();
            options.AddDatabaseRecordExitsRule(
                new List<RecordExistRule>()
                {
                    new RecordExistRule()
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
            var options = new TestGeneratorOptions();
            Assert.Throws<ArgumentNullException>(() => options.AddDatabaseRecordExitsRule(null));
        }

        [Fact]
        public void AddDatabaseRecordsCountRuleTest_Successful()
        {
            var options = new TestGeneratorOptions();
            options.AddDatabaseRecordsCountRule(
                new List<RecordCountRule>()
                {
                    new RecordCountRule()
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
            var options = new TestGeneratorOptions();
            Assert.Throws<ArgumentNullException>(() => options.AddDatabaseRecordsCountRule(null));
        }

        [Fact]
        public void AddQueryStatisticsRuleTest_SuccessfulIfTypesNull()
        {
            var options = new TestGeneratorOptions();
            options.CaptureQueryStatistics(new[]{"query1","query2"}, new QueryStatisticsType[] {QueryStatisticsType.Time});

            Assert.NotNull(options);
        }
        
        [Fact]
        public void AddQueryStatisticsRuleTest_FailsOnDuplicateType()
        {
            var options = new TestGeneratorOptions();
            options.CaptureQueryStatistics(new[]{"query1","query2"},
                new QueryStatisticsType[] {QueryStatisticsType.Io , QueryStatisticsType.Io});

            Assert.NotNull(options);
        }

        [Fact]
        public void AddQueryStatisticsRuleTest_Failure()
        {
            var options = new TestGeneratorOptions();
            Assert.Throws<ArgumentNullException>(() => options.CaptureQueryStatistics(null, new QueryStatisticsType[] {QueryStatisticsType.Io}));
        }
    }
}