using ExpectedObjects;
using QAToolKit.Engine.Database.Generators;
using QAToolKit.Engine.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace QAToolKit.Engine.Database.Test.Generators
{
    public class SqlServerTestGeneratorTests
    {
        [Fact]
        public async Task SqlServerTableExistScriptTest_Success()
        {
            var generator = new SqlServerTestGenerator(options =>
            {
                options.AddDatabaseObjectExitsRule(new string[] {"mytable"}, DatabaseObjectType.Table);
            });

            var results = new List<Models.Test>
            {
                new Models.Test(
                    "mytable",
                    $@"IF EXISTS (SELECT * FROM [sys].[tables] WHERE [Name] = 'mytable') BEGIN Select 1 END ELSE BEGIN Select 0 END;",
                    TestType.ObjectExist,
                    DatabaseKind.SqlServer)
            }.ToExpectedObject();

            results.ShouldEqual(await generator.Generate());
            Assert.Equal(DatabaseKind.SqlServer, generator.DatabaseKind);
        }

        [Fact]
        public async Task SqlServerViewExistScriptTest_Success()
        {
            var generator = new SqlServerTestGenerator(options =>
            {
                options.AddDatabaseObjectExitsRule(new string[] {"myview"}, DatabaseObjectType.View);
            });

            var results = new List<Models.Test>
            {
                new Models.Test(
                    "myview",
                    $@"IF EXISTS (SELECT * FROM [sys].[views] WHERE [Name] = 'myview') BEGIN Select 1 END ELSE BEGIN Select 0 END;",
                    TestType.ObjectExist,
                    DatabaseKind.SqlServer)
            }.ToExpectedObject();

            results.ShouldEqual(await generator.Generate());
            Assert.Equal(DatabaseKind.SqlServer, generator.DatabaseKind);
        }

        [Fact]
        public async Task SqlServerStoredProcedureExistScriptTest_Success()
        {
            var generator = new SqlServerTestGenerator(options =>
            {
                options.AddDatabaseObjectExitsRule(new string[] {"mystoredprocedure"},
                    DatabaseObjectType.StoredProcedure);
            });

            var results = new List<Models.Test>
            {
                new Models.Test(
                    "mystoredprocedure",
                    $@"IF EXISTS (SELECT * FROM [sys].[procedures] WHERE [Name] = 'mystoredprocedure') BEGIN Select 1 END ELSE BEGIN Select 0 END;",
                    TestType.ObjectExist,
                    DatabaseKind.SqlServer)
            }.ToExpectedObject();

            results.ShouldEqual(await generator.Generate());
            Assert.Equal(DatabaseKind.SqlServer, generator.DatabaseKind);
        }

        [Fact]
        public async Task SqlServerMultipleTableExistScriptTest_Success()
        {
            var generator = new SqlServerTestGenerator(options =>
            {
                options.AddDatabaseObjectExitsRule(new string[] {"table1", "table2"}, DatabaseObjectType.Table);
            });

            var results = new List<Models.Test>
            {
                new Models.Test(
                    "table1",
                    $@"IF EXISTS (SELECT * FROM [sys].[tables] WHERE [Name] = 'table1') BEGIN Select 1 END ELSE BEGIN Select 0 END;",
                    TestType.ObjectExist,
                    DatabaseKind.SqlServer),
                new Models.Test(
                    "table2",
                    $@"IF EXISTS (SELECT * FROM [sys].[tables] WHERE [Name] = 'table2') BEGIN Select 1 END ELSE BEGIN Select 0 END;",
                    TestType.ObjectExist,
                    DatabaseKind.SqlServer)
            }.ToExpectedObject();

            results.ShouldEqual(await generator.Generate());
            Assert.Equal(DatabaseKind.SqlServer, generator.DatabaseKind);
        }

        [Fact]
        public async Task SqlServerMultipleViewExistScriptTest_Success()
        {
            var generator = new SqlServerTestGenerator(options =>
            {
                options.AddDatabaseObjectExitsRule(new string[] {"view1", "view2"}, DatabaseObjectType.View);
            });

            var results = new List<Models.Test>
            {
                new Models.Test(
                    "view1",
                    $@"IF EXISTS (SELECT * FROM [sys].[views] WHERE [Name] = 'view1') BEGIN Select 1 END ELSE BEGIN Select 0 END;",
                    TestType.ObjectExist,
                    DatabaseKind.SqlServer),
                new Models.Test(
                    "view2",
                    $@"IF EXISTS (SELECT * FROM [sys].[views] WHERE [Name] = 'view2') BEGIN Select 1 END ELSE BEGIN Select 0 END;",
                    TestType.ObjectExist,
                    DatabaseKind.SqlServer)
            }.ToExpectedObject();

            results.ShouldEqual(await generator.Generate());
            Assert.Equal(DatabaseKind.SqlServer, generator.DatabaseKind);
        }

        [Fact]
        public async Task SqlServerMultipleStoredProcedureExistScriptTest_Success()
        {
            var generator = new SqlServerTestGenerator(options =>
            {
                options.AddDatabaseObjectExitsRule(new string[] {"sp1", "sp2"}, DatabaseObjectType.StoredProcedure);
            });

            var results = new List<Models.Test>
            {
                new Models.Test(
                    "sp1",
                    $@"IF EXISTS (SELECT * FROM [sys].[procedures] WHERE [Name] = 'sp1') BEGIN Select 1 END ELSE BEGIN Select 0 END;",
                    TestType.ObjectExist,
                    DatabaseKind.SqlServer),
                new Models.Test(
                    "sp2",
                    $@"IF EXISTS (SELECT * FROM [sys].[procedures] WHERE [Name] = 'sp2') BEGIN Select 1 END ELSE BEGIN Select 0 END;",
                    TestType.ObjectExist,
                    DatabaseKind.SqlServer)
            }.ToExpectedObject();

            results.ShouldEqual(await generator.Generate());
            Assert.Equal(DatabaseKind.SqlServer, generator.DatabaseKind);
        }

        [Fact]
        public void SqlServerObjectExistScriptNullDbKindTest_Success()
        {
            var generator = new SqlServerTestGenerator(options =>
            {
                options.AddDatabaseObjectExitsRule(new string[] {"sp1", "sp2"}, DatabaseObjectType.StoredProcedure);
            });

            Assert.Equal(DatabaseKind.SqlServer, generator.DatabaseKind);
        }


        [Fact]
        public async Task SqlServerRecordExistScriptTest_Success()
        {
            var generator = new SqlServerTestGenerator(options =>
            {
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
            });

            var results = new List<Models.Test>
            {
                new Models.Test(
                    "mytable",
                    $@"IF EXISTS (SELECT * FROM [mytable] WHERE [name] = 'myname') BEGIN Select 1 END ELSE BEGIN Select 0 END;",
                    TestType.RecordExist,
                    DatabaseKind.SqlServer)
            }.ToExpectedObject();

            results.ShouldEqual(await generator.Generate());
            Assert.Equal(DatabaseKind.SqlServer, generator.DatabaseKind);
        }

        [Fact]
        public async Task SqlServerRecordCountScriptTest_Success()
        {
            var generator = new SqlServerTestGenerator(options =>
            {
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
            });

            var results = new List<Models.Test>
            {
                new Models.Test(
                    "mytable",
                    $@"IF EXISTS (SELECT * FROM [mytable] WHERE (SELECT COUNT(*) AS [count] FROM [mytable]) = 100) BEGIN Select 1 END ELSE BEGIN Select 0 END;",
                    TestType.RecordCount,
                    DatabaseKind.SqlServer)
            }.ToExpectedObject();

            results.ShouldEqual(await generator.Generate());
            Assert.Equal(DatabaseKind.SqlServer, generator.DatabaseKind);
        }

        [Fact]
        public async Task SqlServerCustomScriptTest_Success()
        {
            var generator = new SqlServerTestGenerator(options =>
            {
                options.AddCustomSqlRule(
                    new List<string>()
                    {
                        "SELECT * FROM [mytable] WHERE (SELECT COUNT(*) AS [count] FROM [mytable]) = 50"
                    });
            });

            var results = new List<Models.Test>
            {
                new Models.Test(
                    null,
                    $@"IF EXISTS (SELECT * FROM [mytable] WHERE (SELECT COUNT(*) AS [count] FROM [mytable]) = 50) BEGIN Select 1 END ELSE BEGIN Select 0 END;",
                    TestType.CustomScript,
                    DatabaseKind.SqlServer)
            }.ToExpectedObject();

            results.ShouldEqual(await generator.Generate());
            Assert.Equal(DatabaseKind.SqlServer, generator.DatabaseKind);
        }

        [Fact]
        public async Task SqlServerQueryStatisticsScriptWithTimeAndIOTest_Success()
        {
            var generator = new SqlServerTestGenerator(options =>
            {
                options.CaptureQueryStatistics(
                    new List<string>()
                    {
                        "SELECT * FROM [mytable] WHERE (SELECT COUNT(*) AS [count] FROM [mytable]) = 50"
                    }.ToArray(),
                    new QueryStatisticsType[] {QueryStatisticsType.Time, QueryStatisticsType.Io});
            });

            var results = new List<Models.Test>
            {
                new Models.Test(
                    null,
                    $@"SET STATISTICS TIME ON;SET STATISTICS IO ON;SELECT * FROM [mytable] WHERE (SELECT COUNT(*) AS [count] FROM [mytable]) = 50;SET STATISTICS TIME OFF;SET STATISTICS IO OFF;",
                    TestType.QueryStatistics,
                    DatabaseKind.SqlServer)
            }.ToExpectedObject();

            results.ShouldEqual(await generator.Generate());
            Assert.Equal(DatabaseKind.SqlServer, generator.DatabaseKind);
        }

        [Fact]
        public async Task SqlServerQueryStatisticsScriptWithTimeTest_Success()
        {
            var generator = new SqlServerTestGenerator(options =>
            {
                options.CaptureQueryStatistics(
                    new List<string>()
                    {
                        "SELECT * FROM [mytable] WHERE (SELECT COUNT(*) AS [count] FROM [mytable]) = 50"
                    }.ToArray(),
                    new QueryStatisticsType[] {QueryStatisticsType.Time});
            });

            var results = new List<Models.Test>
            {
                new Models.Test(
                    null,
                    $@"SET STATISTICS TIME ON;SELECT * FROM [mytable] WHERE (SELECT COUNT(*) AS [count] FROM [mytable]) = 50;SET STATISTICS TIME OFF;",
                    TestType.QueryStatistics,
                    DatabaseKind.SqlServer)
            }.ToExpectedObject();

            results.ShouldEqual(await generator.Generate());
            Assert.Equal(DatabaseKind.SqlServer, generator.DatabaseKind);
        }

        [Fact]
        public async Task SqlServerQueryStatisticsScriptWithIOTest_Success()
        {
            var generator = new SqlServerTestGenerator(options =>
            {
                options.CaptureQueryStatistics(
                    new[]
                    {
                        "SELECT * FROM [mytable] WHERE (SELECT COUNT(*) AS [count] FROM [mytable]) = 50"
                    },
                    new QueryStatisticsType[] {QueryStatisticsType.Io});
            });

            var results = new List<Models.Test>
            {
                new Models.Test(
                    null,
                    $@"SET STATISTICS IO ON;SELECT * FROM [mytable] WHERE (SELECT COUNT(*) AS [count] FROM [mytable]) = 50;SET STATISTICS IO OFF;",
                    TestType.QueryStatistics,
                    DatabaseKind.SqlServer)
            }.ToExpectedObject();

            results.ShouldEqual(await generator.Generate());
            Assert.Equal(DatabaseKind.SqlServer, generator.DatabaseKind);
        }
    }
}