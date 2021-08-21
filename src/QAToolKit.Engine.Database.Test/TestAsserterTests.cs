using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QAToolKit.Engine.Database.Models;
using Xunit;
using Xunit.Abstractions;

namespace QAToolKit.Engine.Database.Test
{
    public class TestAsserterTests
    {
        private readonly ILogger<TestAsserterTests> _logger;

        public TestAsserterTests(ITestOutputHelper testOutputHelper)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(testOutputHelper));
            _logger = loggerFactory.CreateLogger<TestAsserterTests>();
        }

        [Fact]
        public void LoadTestResultFixture_Successful()
        {
            var results =
                JsonConvert.DeserializeObject<TestResult>(File.ReadAllText("Fixtures/TestResultFixture.json"));
            Assert.NotNull(results);
        }

        [Fact]
        public void EvaluateTotalElapsedTimeTest_Successful()
        {
            var result = JsonConvert.DeserializeObject<TestResult>(File.ReadAllText("Fixtures/TestResultFixture.json"));

            var asserter = new TestAsserter(result)
                .EvaluateTotalElapsedTime((x) => x == 447)
                .AssertAll();

            _logger.LogInformation(JsonConvert.SerializeObject(asserter));

            Assert.True(asserter.FirstOrDefault().IsTrue);
        }

        [Fact]
        public void EvaluateTotalElapsedTimeTest_Failed()
        {
            var result = JsonConvert.DeserializeObject<TestResult>(File.ReadAllText("Fixtures/TestResultFixture.json"));

            var asserter = new TestAsserter(result)
                .EvaluateTotalElapsedTime((x) => x == 123124)
                .AssertAll();

            _logger.LogInformation(JsonConvert.SerializeObject(asserter));

            Assert.False(asserter.FirstOrDefault().IsTrue);
        }

        [Fact]
        public void EvaluateServerCpuTimeTest_Successful()
        {
            var result = JsonConvert.DeserializeObject<TestResult>(File.ReadAllText("Fixtures/TestResultFixture.json"));

            var asserter = new TestAsserter(result)
                .EvaluateServerCpuTime((x) => x == 68)
                .AssertAll();

            _logger.LogInformation(JsonConvert.SerializeObject(asserter));

            Assert.True(asserter.FirstOrDefault().IsTrue);
        }

        [Fact]
        public void EvaluateServerCpuTimeTest_Failed()
        {
            var result = JsonConvert.DeserializeObject<TestResult>(File.ReadAllText("Fixtures/TestResultFixture.json"));

            var asserter = new TestAsserter(result)
                .EvaluateServerCpuTime((x) => x == 2342)
                .AssertAll();

            _logger.LogInformation(JsonConvert.SerializeObject(asserter));

            Assert.False(asserter.FirstOrDefault().IsTrue);
        }

        [Fact]
        public void EvaluateServerElapsedTimeTest_Successful()
        {
            var result = JsonConvert.DeserializeObject<TestResult>(File.ReadAllText("Fixtures/TestResultFixture.json"));

            var asserter = new TestAsserter(result)
                .EvaluateServerElapsedTime((x) => x == 242)
                .AssertAll();

            _logger.LogInformation(JsonConvert.SerializeObject(asserter));

            Assert.True(asserter.FirstOrDefault().IsTrue);
        }

        [Fact]
        public void EvaluateServerElapsedTimeTest_Failed()
        {
            var result = JsonConvert.DeserializeObject<TestResult>(File.ReadAllText("Fixtures/TestResultFixture.json"));

            var asserter = new TestAsserter(result)
                .EvaluateServerElapsedTime((x) => x == 453)
                .AssertAll();

            _logger.LogInformation(JsonConvert.SerializeObject(asserter));

            Assert.False(asserter.FirstOrDefault().IsTrue);
        }

        [Fact]
        public void EvaluateLogicalReadsTest_Successful()
        {
            var result = JsonConvert.DeserializeObject<TestResult>(File.ReadAllText("Fixtures/TestResultFixture.json"));

            var asserter = new TestAsserter(result)
                .EvaluateLogicalReads((x) => x == 1674, "myTable")
                .AssertAll();

            _logger.LogInformation(JsonConvert.SerializeObject(asserter));

            Assert.True(asserter.FirstOrDefault().IsTrue);
        }

        [Fact]
        public void EvaluateLogicalReadsTest_Failed()
        {
            var result = JsonConvert.DeserializeObject<TestResult>(File.ReadAllText("Fixtures/TestResultFixture.json"));

            var asserter = new TestAsserter(result)
                .EvaluateLogicalReads((x) => x == 24234, "myTable")
                .AssertAll();

            _logger.LogInformation(JsonConvert.SerializeObject(asserter));

            Assert.False(asserter.FirstOrDefault().IsTrue);
        }
        
        [Fact]
        public void EvaluateLogicalReadsTest_SuccessWhenNull()
        {
            var result = JsonConvert.DeserializeObject<TestResult>(File.ReadAllText("Fixtures/TestResultFixture.json"));

            var asserter = new TestAsserter(result)
                .EvaluateLogicalReads((x) => x == 1674)
                .AssertAll();

            _logger.LogInformation(JsonConvert.SerializeObject(asserter));

            Assert.True(asserter.FirstOrDefault().IsTrue);
        }
        
        [Fact]
        public void EvaluateLogicalReadsTest_FailedWhenNull()
        {
            var result = JsonConvert.DeserializeObject<TestResult>(File.ReadAllText("Fixtures/TestResultFixture.json"));

            var asserter = new TestAsserter(result)
                .EvaluateLogicalReads((x) => x == 24234)
                .AssertAll();

            _logger.LogInformation(JsonConvert.SerializeObject(asserter));

            Assert.False(asserter.FirstOrDefault().IsTrue);
        }

        [Fact]
        public void EvaluateLogicalReadsTest_FailedWithException()
        {
            var result = JsonConvert.DeserializeObject<TestResult>(File.ReadAllText("Fixtures/TestResultFixture.json"));

            Assert.Throws<NullReferenceException>(() => new TestAsserter(result)
                .EvaluateLogicalReads((x) => x == 24234, "myTable3")
                .AssertAll());
        }
        
        [Fact]
        public void CustomPropertyAddTest_Successful()
        {
            var result = JsonConvert.DeserializeObject<TestResult>(File.ReadAllText("Fixtures/TestResultFixture.json"));

            var asserter = new TestAsserter(result)
                .EvaluateTotalElapsedTime((x) => x == 447)
                .WithCustomProperty(new KeyValuePair<string, string>("script",result.Script))
                .AssertAll();

            _logger.LogInformation(JsonConvert.SerializeObject(asserter));

            Assert.True(asserter.FirstOrDefault().IsTrue);
            Assert.True(asserter.FirstOrDefault().CustomProperties.Count == 1);
        }

        [Fact]
        public void CustomPropertyAddTest_Failed()
        {
            var result = JsonConvert.DeserializeObject<TestResult>(File.ReadAllText("Fixtures/TestResultFixture.json"));

            var asserter = new TestAsserter(result)
                .EvaluateTotalElapsedTime((x) => x == 123124)
                .AssertAll();

            _logger.LogInformation(JsonConvert.SerializeObject(asserter));

            Assert.False(asserter.FirstOrDefault().IsTrue);
        }
    }
}