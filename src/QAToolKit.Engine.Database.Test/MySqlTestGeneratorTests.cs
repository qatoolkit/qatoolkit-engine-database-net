using ExpectedObjects;
using QAToolKit.Engine.Database.Generators;
using QAToolKit.Engine.Database.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace QAToolKit.Engine.Database.Test
{
    public class MySqlTestGeneratorTests
    {
        [Fact]
        public async Task MySqlTableExistScriptTest_Success()
        {
            var generator = new MySqlTestGenerator(options =>
            {
                options.UseDatabase(DatabaseKind.MySQL);
                options.AddDatabaseObjectExitsRule(new string[] { "mytable" }, DatabaseObjectType.Table);
            });

            var results = new List<DatabaseScript>
            {
                new DatabaseScript(
                        "mytable",
                        $@"SELECT EXISTS(SELECT * FROM information_schema.tables WHERE table_name = 'mytable');",
                        DatabaseTestType.ObjectExist,
                        DatabaseKind.MySQL)
            }.ToExpectedObject();

            results.ShouldEqual(await generator.Generate());
            Assert.Equal(DatabaseKind.MySQL, generator.DatabaseKind);
        }

        [Fact]
        public async Task MySqlViewExistScriptTest_Success()
        {
            var generator = new MySqlTestGenerator(options =>
            {
                options.UseDatabase(DatabaseKind.MySQL);
                options.AddDatabaseObjectExitsRule(new string[] { "myview" }, DatabaseObjectType.View);
            });

            var results = new List<DatabaseScript>
            {
                new DatabaseScript(
                        "myview",
                        $@"SELECT EXISTS(SELECT * FROM information_schema.views WHERE table_name = 'myview');",
                        DatabaseTestType.ObjectExist,
                        DatabaseKind.MySQL)
            }.ToExpectedObject();

            results.ShouldEqual(await generator.Generate());
            Assert.Equal(DatabaseKind.MySQL, generator.DatabaseKind);
        }

        [Fact]
        public async Task MySqlStoredProcedureExistScriptTest_Success()
        {
            var generator = new MySqlTestGenerator(options =>
            {
                options.UseDatabase(DatabaseKind.MySQL);
                options.AddDatabaseObjectExitsRule(new string[] { "mystoredprocedure" }, DatabaseObjectType.StoredProcedure);
            });

            var results = new List<DatabaseScript>
            {
                new DatabaseScript(
                        "mystoredprocedure",
                        $@"SELECT EXISTS(SELECT * FROM information_schema.routines WHERE routine_name = 'mystoredprocedure');",
                        DatabaseTestType.ObjectExist,
                        DatabaseKind.MySQL)
            }.ToExpectedObject();

            results.ShouldEqual(await generator.Generate());
            Assert.Equal(DatabaseKind.MySQL, generator.DatabaseKind);
        }

        [Fact]
        public async Task MySqlMultipleTableExistScriptTest_Success()
        {
            var generator = new MySqlTestGenerator(options =>
            {
                options.UseDatabase(DatabaseKind.MySQL);
                options.AddDatabaseObjectExitsRule(new string[] { "table1", "table2" }, DatabaseObjectType.Table);
            });

            var results = new List<DatabaseScript>
            {
               new DatabaseScript(
                        "table1",
                        $@"SELECT EXISTS(SELECT * FROM information_schema.tables WHERE table_name = 'table1');",
                        DatabaseTestType.ObjectExist,
                        DatabaseKind.MySQL),
               new DatabaseScript(
                        "table2",
                        $@"SELECT EXISTS(SELECT * FROM information_schema.tables WHERE table_name = 'table2');",
                        DatabaseTestType.ObjectExist,
                        DatabaseKind.MySQL)
            }.ToExpectedObject();

            results.ShouldEqual(await generator.Generate());
            Assert.Equal(DatabaseKind.MySQL, generator.DatabaseKind);
        }

        [Fact]
        public async Task MySqlMultipleViewExistScriptTest_Success()
        {
            var generator = new MySqlTestGenerator(options =>
            {
                options.UseDatabase(DatabaseKind.MySQL);
                options.AddDatabaseObjectExitsRule(new string[] { "view1", "view2" }, DatabaseObjectType.View);
            });

            var results = new List<DatabaseScript>
            {
                 new DatabaseScript(
                        "view1",
                        $@"SELECT EXISTS(SELECT * FROM information_schema.views WHERE table_name = 'view1');",
                        DatabaseTestType.ObjectExist,
                        DatabaseKind.MySQL),
                 new DatabaseScript(
                        "view2",
                        $@"SELECT EXISTS(SELECT * FROM information_schema.views WHERE table_name = 'view2');",
                        DatabaseTestType.ObjectExist,
                        DatabaseKind.MySQL)
            }.ToExpectedObject();

            results.ShouldEqual(await generator.Generate());
            Assert.Equal(DatabaseKind.MySQL, generator.DatabaseKind);
        }

        [Fact]
        public async Task MySqlMultipleStoredProcedureExistScriptTest_Success()
        {
            var generator = new MySqlTestGenerator(options =>
            {
                options.UseDatabase(DatabaseKind.MySQL);
                options.AddDatabaseObjectExitsRule(new string[] { "sp1", "sp2" }, DatabaseObjectType.StoredProcedure);
            });

            var results = new List<DatabaseScript>
            {
                new DatabaseScript(
                        "sp1",
                        $@"SELECT EXISTS(SELECT * FROM information_schema.routines WHERE routine_name = 'sp1');",
                        DatabaseTestType.ObjectExist,
                        DatabaseKind.MySQL),
                new DatabaseScript(
                        "sp2",
                        $@"SELECT EXISTS(SELECT * FROM information_schema.routines WHERE routine_name = 'sp2');",
                        DatabaseTestType.ObjectExist,
                        DatabaseKind.MySQL)
            }.ToExpectedObject();

            results.ShouldEqual(await generator.Generate());
            Assert.Equal(DatabaseKind.MySQL, generator.DatabaseKind);
        }

        [Fact]
        public void MySqlObjectExistScriptNullDbKindTest_Success()
        {
            var generator = new MySqlTestGenerator(options =>
            {
                options.AddDatabaseObjectExitsRule(new string[] { "sp1", "sp2" }, DatabaseObjectType.StoredProcedure);
            });

            Assert.Equal(DatabaseKind.MySQL, generator.DatabaseKind);
        }

        [Fact]
        public async Task MySqlObjectExistScriptNullOptionsTest_Fails()
        {
            var generator = new MySqlTestGenerator();

            Assert.Equal(DatabaseKind.MySQL, generator.DatabaseKind);
            await Assert.ThrowsAsync<ArgumentNullException>(async() => await generator.Generate());
        }
    }
}
