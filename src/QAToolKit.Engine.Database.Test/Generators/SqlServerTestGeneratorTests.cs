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
                options.AddDatabaseObjectExitsRule(new string[] { "mytable" }, DatabaseObjectType.Table);
            });

            var results = new List<DatabaseTest>
            {
                new DatabaseTest(
                        "mytable",
                        $@"IF EXISTS (SELECT * FROM [sys].[tables] WHERE [Name] = 'mytable') BEGIN Select 1 END ELSE BEGIN Select 0 END;",
                        DatabaseTestType.ObjectExist,
                        DatabaseKind.SQLServer)
            }.ToExpectedObject();

            results.ShouldEqual(await generator.Generate());
            Assert.Equal(DatabaseKind.SQLServer, generator.DatabaseKind);
        }

        [Fact]
        public async Task SqlServerViewExistScriptTest_Success()
        {
            var generator = new SqlServerTestGenerator(options =>
            {
                options.AddDatabaseObjectExitsRule(new string[] { "myview" }, DatabaseObjectType.View);
            });

            var results = new List<DatabaseTest>
            {
                new DatabaseTest(
                        "myview",
                        $@"IF EXISTS (SELECT * FROM [sys].[views] WHERE [Name] = 'myview') BEGIN Select 1 END ELSE BEGIN Select 0 END;",
                        DatabaseTestType.ObjectExist,
                        DatabaseKind.SQLServer)
            }.ToExpectedObject();

            results.ShouldEqual(await generator.Generate());
            Assert.Equal(DatabaseKind.SQLServer, generator.DatabaseKind);
        }

        [Fact]
        public async Task SqlServerStoredProcedureExistScriptTest_Success()
        {
            var generator = new SqlServerTestGenerator(options =>
            {
                options.AddDatabaseObjectExitsRule(new string[] { "mystoredprocedure" }, DatabaseObjectType.StoredProcedure);
            });

            var results = new List<DatabaseTest>
            {
                new DatabaseTest(
                        "mystoredprocedure",
                        $@"IF EXISTS (SELECT * FROM [sys].[procedures] WHERE [Name] = 'mystoredprocedure') BEGIN Select 1 END ELSE BEGIN Select 0 END;",
                        DatabaseTestType.ObjectExist,
                        DatabaseKind.SQLServer)
            }.ToExpectedObject();

            results.ShouldEqual(await generator.Generate());
            Assert.Equal(DatabaseKind.SQLServer, generator.DatabaseKind);
        }

        [Fact]
        public async Task SqlServerMultipleTableExistScriptTest_Success()
        {
            var generator = new SqlServerTestGenerator(options =>
            {
                options.AddDatabaseObjectExitsRule(new string[] { "table1", "table2" }, DatabaseObjectType.Table);
            });

            var results = new List<DatabaseTest>
            {
               new DatabaseTest(
                        "table1",
                        $@"IF EXISTS (SELECT * FROM [sys].[tables] WHERE [Name] = 'table1') BEGIN Select 1 END ELSE BEGIN Select 0 END;",
                        DatabaseTestType.ObjectExist,
                        DatabaseKind.SQLServer),
               new DatabaseTest(
                        "table2",
                        $@"IF EXISTS (SELECT * FROM [sys].[tables] WHERE [Name] = 'table2') BEGIN Select 1 END ELSE BEGIN Select 0 END;",
                        DatabaseTestType.ObjectExist,
                        DatabaseKind.SQLServer)
            }.ToExpectedObject();

            results.ShouldEqual(await generator.Generate());
            Assert.Equal(DatabaseKind.SQLServer, generator.DatabaseKind);
        }

        [Fact]
        public async Task SqlServerMultipleViewExistScriptTest_Success()
        {
            var generator = new SqlServerTestGenerator(options =>
            {
                options.AddDatabaseObjectExitsRule(new string[] { "view1", "view2" }, DatabaseObjectType.View);
            });

            var results = new List<DatabaseTest>
            {
                 new DatabaseTest(
                        "view1",
                        $@"IF EXISTS (SELECT * FROM [sys].[views] WHERE [Name] = 'view1') BEGIN Select 1 END ELSE BEGIN Select 0 END;",
                        DatabaseTestType.ObjectExist,
                        DatabaseKind.SQLServer),
                 new DatabaseTest(
                        "view2",
                        $@"IF EXISTS (SELECT * FROM [sys].[views] WHERE [Name] = 'view2') BEGIN Select 1 END ELSE BEGIN Select 0 END;",
                        DatabaseTestType.ObjectExist,
                        DatabaseKind.SQLServer)
            }.ToExpectedObject();

            results.ShouldEqual(await generator.Generate());
            Assert.Equal(DatabaseKind.SQLServer, generator.DatabaseKind);
        }

        [Fact]
        public async Task SqlServerMultipleStoredProcedureExistScriptTest_Success()
        {
            var generator = new SqlServerTestGenerator(options =>
            {
                options.AddDatabaseObjectExitsRule(new string[] { "sp1", "sp2" }, DatabaseObjectType.StoredProcedure);
            });

            var results = new List<DatabaseTest>
            {
                new DatabaseTest(
                        "sp1",
                        $@"IF EXISTS (SELECT * FROM [sys].[procedures] WHERE [Name] = 'sp1') BEGIN Select 1 END ELSE BEGIN Select 0 END;",
                        DatabaseTestType.ObjectExist,
                        DatabaseKind.SQLServer),
                new DatabaseTest(
                        "sp2",
                        $@"IF EXISTS (SELECT * FROM [sys].[procedures] WHERE [Name] = 'sp2') BEGIN Select 1 END ELSE BEGIN Select 0 END;",
                        DatabaseTestType.ObjectExist,
                        DatabaseKind.SQLServer)
            }.ToExpectedObject();

            results.ShouldEqual(await generator.Generate());
            Assert.Equal(DatabaseKind.SQLServer, generator.DatabaseKind);
        }

        [Fact]
        public void SqlServerObjectExistScriptNullDbKindTest_Success()
        {
            var generator = new SqlServerTestGenerator(options =>
            {
                options.AddDatabaseObjectExitsRule(new string[] { "sp1", "sp2" }, DatabaseObjectType.StoredProcedure);
            });

            Assert.Equal(DatabaseKind.SQLServer, generator.DatabaseKind);
        }


        [Fact]
        public async Task SqlServerRecordExistScriptTest_Success()
        {
            var generator = new SqlServerTestGenerator(options =>
            {
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
            });

            var results = new List<DatabaseTest>
            {
                new DatabaseTest(
                        "mytable",
                        $@"IF EXISTS (SELECT * FROM [mytable] WHERE [name] = 'myname') BEGIN Select 1 END ELSE BEGIN Select 0 END;",
                        DatabaseTestType.RecordExist,
                        DatabaseKind.SQLServer)
            }.ToExpectedObject();

            results.ShouldEqual(await generator.Generate());
            Assert.Equal(DatabaseKind.SQLServer, generator.DatabaseKind);
        }

        [Fact]
        public async Task SqlServerRecordCountScriptTest_Success()
        {
            var generator = new SqlServerTestGenerator(options =>
            {
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
            });

            var results = new List<DatabaseTest>
            {
                new DatabaseTest(
                        "mytable",
                        $@"IF EXISTS (SELECT * FROM [mytable] WHERE (SELECT COUNT(*) AS [count] FROM [mytable]) = 100) BEGIN Select 1 END ELSE BEGIN Select 0 END;",
                        DatabaseTestType.RecordCount,
                        DatabaseKind.SQLServer)
            }.ToExpectedObject();

            results.ShouldEqual(await generator.Generate());
            Assert.Equal(DatabaseKind.SQLServer, generator.DatabaseKind);
        }
    }
}
