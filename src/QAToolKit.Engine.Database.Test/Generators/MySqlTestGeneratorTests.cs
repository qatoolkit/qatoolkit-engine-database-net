using ExpectedObjects;
using QAToolKit.Engine.Database.Generators;
using QAToolKit.Engine.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace QAToolKit.Engine.Database.Test.Generators
{
    public class MySqlTestGeneratorTests
    {
        [Fact]
        public async Task MySqlTableExistScriptTest_Success()
        {
            var generator = new MySqlTestGenerator(options =>
            {
                options.AddDatabaseObjectExitsRule(new string[] { "mytable" }, DatabaseObjectType.Table);
            });

            var results = new List<Models.Test>
            {
                new Models.Test(
                        "mytable",
                        $@"SELECT EXISTS(SELECT * FROM `information_schema`.`tables` WHERE `table_name` = 'mytable');",
                        TestType.ObjectExist,
                        DatabaseKind.MySql)
            }.ToExpectedObject();

            results.ShouldEqual(await generator.Generate());
            Assert.Equal(DatabaseKind.MySql, generator.DatabaseKind);
        }

        [Fact]
        public async Task MySqlViewExistScriptTest_Success()
        {
            var generator = new MySqlTestGenerator(options =>
            {
                options.AddDatabaseObjectExitsRule(new string[] { "myview" }, DatabaseObjectType.View);
            });

            var results = new List<Models.Test>
            {
                new Models.Test(
                        "myview",
                        $@"SELECT EXISTS(SELECT * FROM `information_schema`.`views` WHERE `table_name` = 'myview');",
                        TestType.ObjectExist,
                        DatabaseKind.MySql)
            }.ToExpectedObject();

            results.ShouldEqual(await generator.Generate());
            Assert.Equal(DatabaseKind.MySql, generator.DatabaseKind);
        }

        [Fact]
        public async Task MySqlStoredProcedureExistScriptTest_Success()
        {
            var generator = new MySqlTestGenerator(options =>
            {
                options.AddDatabaseObjectExitsRule(new string[] { "mystoredprocedure" }, DatabaseObjectType.StoredProcedure);
            });

            var results = new List<Models.Test>
            {
                new Models.Test(
                        "mystoredprocedure",
                        $@"SELECT EXISTS(SELECT * FROM `information_schema`.`routines` WHERE `routine_name` = 'mystoredprocedure');",
                        TestType.ObjectExist,
                        DatabaseKind.MySql)
            }.ToExpectedObject();

            results.ShouldEqual(await generator.Generate());
            Assert.Equal(DatabaseKind.MySql, generator.DatabaseKind);
        }

        [Fact]
        public async Task MySqlMultipleTableExistScriptTest_Success()
        {
            var generator = new MySqlTestGenerator(options =>
            {
                options.AddDatabaseObjectExitsRule(new string[] { "table1", "table2" }, DatabaseObjectType.Table);
            });

            var results = new List<Models.Test>
            {
               new Models.Test(
                        "table1",
                        $@"SELECT EXISTS(SELECT * FROM `information_schema`.`tables` WHERE `table_name` = 'table1');",
                        TestType.ObjectExist,
                        DatabaseKind.MySql),
               new Models.Test(
                        "table2",
                        $@"SELECT EXISTS(SELECT * FROM `information_schema`.`tables` WHERE `table_name` = 'table2');",
                        TestType.ObjectExist,
                        DatabaseKind.MySql)
            }.ToExpectedObject();

            results.ShouldEqual(await generator.Generate());
            Assert.Equal(DatabaseKind.MySql, generator.DatabaseKind);
        }

        [Fact]
        public async Task MySqlMultipleViewExistScriptTest_Success()
        {
            var generator = new MySqlTestGenerator(options =>
            {
                options.AddDatabaseObjectExitsRule(new string[] { "view1", "view2" }, DatabaseObjectType.View);
            });

            var results = new List<Models.Test>
            {
                 new Models.Test(
                        "view1",
                        $@"SELECT EXISTS(SELECT * FROM `information_schema`.`views` WHERE `table_name` = 'view1');",
                        TestType.ObjectExist,
                        DatabaseKind.MySql),
                 new Models.Test(
                        "view2",
                        $@"SELECT EXISTS(SELECT * FROM `information_schema`.`views` WHERE `table_name` = 'view2');",
                        TestType.ObjectExist,
                        DatabaseKind.MySql)
            }.ToExpectedObject();

            results.ShouldEqual(await generator.Generate());
            Assert.Equal(DatabaseKind.MySql, generator.DatabaseKind);
        }

        [Fact]
        public async Task MySqlMultipleStoredProcedureExistScriptTest_Success()
        {
            var generator = new MySqlTestGenerator(options =>
            {
                options.AddDatabaseObjectExitsRule(new string[] { "sp1", "sp2" }, DatabaseObjectType.StoredProcedure);
            });

            var results = new List<Models.Test>
            {
                new Models.Test(
                        "sp1",
                        $@"SELECT EXISTS(SELECT * FROM `information_schema`.`routines` WHERE `routine_name` = 'sp1');",
                        TestType.ObjectExist,
                        DatabaseKind.MySql),
                new Models.Test(
                        "sp2",
                        $@"SELECT EXISTS(SELECT * FROM `information_schema`.`routines` WHERE `routine_name` = 'sp2');",
                        TestType.ObjectExist,
                        DatabaseKind.MySql)
            }.ToExpectedObject();

            results.ShouldEqual(await generator.Generate());
            Assert.Equal(DatabaseKind.MySql, generator.DatabaseKind);
        }

        [Fact]
        public void MySqlObjectExistScriptNullDbKindTest_Success()
        {
            var generator = new MySqlTestGenerator(options =>
            {
                options.AddDatabaseObjectExitsRule(new string[] { "sp1", "sp2" }, DatabaseObjectType.StoredProcedure);
            });

            Assert.Equal(DatabaseKind.MySql, generator.DatabaseKind);
        }

        [Fact]
        public async Task MySqlRecordExistScriptTest_Success()
        {
            var generator = new MySqlTestGenerator(options =>
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
                        $@"SELECT EXISTS(SELECT * FROM `mytable` WHERE `name` = 'myname');",
                        TestType.RecordExist,
                        DatabaseKind.MySql)
            }.ToExpectedObject();

            results.ShouldEqual(await generator.Generate());
            Assert.Equal(DatabaseKind.MySql, generator.DatabaseKind);
        }

        [Fact]
        public async Task MySqlRecordCountScriptTest_Success()
        {
            var generator = new MySqlTestGenerator(options =>
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
                        $@"SELECT EXISTS (SELECT * FROM `mytable` WHERE (SELECT COUNT(*) AS `count` FROM `mytable`) = 100);",
                        TestType.RecordCount,
                        DatabaseKind.MySql)
            }.ToExpectedObject();

            results.ShouldEqual(await generator.Generate());
            Assert.Equal(DatabaseKind.MySql, generator.DatabaseKind);
        }

        [Fact]
        public async Task MySqlCustomScriptTest_Success()
        {
            var generator = new MySqlTestGenerator(options =>
            {
                options.AddCustomSqlRule(
                    new List<string>()
                    {
                        "SELECT * FROM mytable WHERE (SELECT COUNT(*) AS count FROM mytable) = 50"
                    });
            });

            var results = new List<Models.Test>
            {
                new Models.Test(
                        null,
                        $@"SELECT EXISTS (SELECT * FROM mytable WHERE (SELECT COUNT(*) AS count FROM mytable) = 50);",
                        TestType.CustomScript,
                        DatabaseKind.MySql)
            }.ToExpectedObject();

            results.ShouldEqual(await generator.Generate());
            Assert.Equal(DatabaseKind.MySql, generator.DatabaseKind);
        }
    }
}
