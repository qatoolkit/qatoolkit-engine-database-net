using ExpectedObjects;
using QAToolKit.Engine.Database.Generators;
using QAToolKit.Engine.Database.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace QAToolKit.Engine.Database.Test
{
    public class SqlServerTestGeneratorTests
    {
        [Fact]
        public async Task SqlServerTableExistScriptTest_Success()
        {
            var generator = new SqlServerTestGenerator(options =>
            {
                options.UseDatabase(DatabaseKind.SQLServer);
                options.AddDatabaseObjectExitsRule(new string[] { "mytable" }, DatabaseObjectType.Table);
            });

            var results = new List<DatabaseScript>
            {
                new DatabaseScript(
                        "mytable",
                        $@"IF EXISTS(SELECT 1 FROM sys.tables WHERE Name = 'mytable') BEGIN Select 1 END ELSE BEGIN Select 0 END",
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
                options.UseDatabase(DatabaseKind.SQLServer);
                options.AddDatabaseObjectExitsRule(new string[] { "myview" }, DatabaseObjectType.View);
            });

            var results = new List<DatabaseScript>
            {
                new DatabaseScript(
                        "myview",
                        $@"IF EXISTS(SELECT 1 FROM sys.views WHERE Name = 'myview') BEGIN Select 1 END ELSE BEGIN Select 0 END",
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
                options.UseDatabase(DatabaseKind.SQLServer);
                options.AddDatabaseObjectExitsRule(new string[] { "mystoredprocedure" }, DatabaseObjectType.StoredProcedure);
            });

            var results = new List<DatabaseScript>
            {
                new DatabaseScript(
                        "mystoredprocedure",
                        $@"IF EXISTS(SELECT 1 FROM sys.procedures WHERE Name = 'mystoredprocedure') BEGIN Select 1 END ELSE BEGIN Select 0 END",
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
                options.UseDatabase(DatabaseKind.SQLServer);
                options.AddDatabaseObjectExitsRule(new string[] { "table1", "table2" }, DatabaseObjectType.Table);
            });

            var results = new List<DatabaseScript>
            {
               new DatabaseScript(
                        "table1",
                        $@"IF EXISTS(SELECT 1 FROM sys.tables WHERE Name = 'table1') BEGIN Select 1 END ELSE BEGIN Select 0 END",
                        DatabaseTestType.ObjectExist,
                        DatabaseKind.SQLServer),
               new DatabaseScript(
                        "table2",
                        $@"IF EXISTS(SELECT 1 FROM sys.tables WHERE Name = 'table2') BEGIN Select 1 END ELSE BEGIN Select 0 END",
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
                options.UseDatabase(DatabaseKind.SQLServer);
                options.AddDatabaseObjectExitsRule(new string[] { "view1", "view2" }, DatabaseObjectType.View);
            });

            var results = new List<DatabaseScript>
            {
                 new DatabaseScript(
                        "view1",
                        $@"IF EXISTS(SELECT 1 FROM sys.views WHERE Name = 'view1') BEGIN Select 1 END ELSE BEGIN Select 0 END",
                        DatabaseTestType.ObjectExist,
                        DatabaseKind.SQLServer),
                 new DatabaseScript(
                        "view2",
                        $@"IF EXISTS(SELECT 1 FROM sys.views WHERE Name = 'view2') BEGIN Select 1 END ELSE BEGIN Select 0 END",
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
                options.UseDatabase(DatabaseKind.SQLServer);
                options.AddDatabaseObjectExitsRule(new string[] { "sp1", "sp2" }, DatabaseObjectType.StoredProcedure);
            });

            var results = new List<DatabaseScript>
            {
                new DatabaseScript(
                        "sp1",
                        $@"IF EXISTS(SELECT 1 FROM sys.procedures WHERE Name = 'sp1') BEGIN Select 1 END ELSE BEGIN Select 0 END",
                        DatabaseTestType.ObjectExist,
                        DatabaseKind.SQLServer),
                new DatabaseScript(
                        "sp2",
                        $@"IF EXISTS(SELECT 1 FROM sys.procedures WHERE Name = 'sp2') BEGIN Select 1 END ELSE BEGIN Select 0 END",
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
        public async Task SqlServerObjectExistScriptNullOptionsTest_Fails()
        {
            var generator = new SqlServerTestGenerator();

            Assert.Equal(DatabaseKind.SQLServer, generator.DatabaseKind);
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await generator.Generate());
        }
    }
}
