# QAToolKit Engine Database library

[![Build .NET Library](https://github.com/qatoolkit/qatoolkit-engine-database-net/workflows/Build%20.NET%20Library/badge.svg)](https://github.com/qatoolkit/qatoolkit-engine-database-net/actions)
[![CodeQL](https://github.com/qatoolkit/qatoolkit-engine-database-net/workflows/CodeQL%20Analyze/badge.svg)](https://github.com/qatoolkit/qatoolkit-engine-database-net/security/code-scanning)
[![Sonarcloud Quality gate](https://github.com/qatoolkit/qatoolkit-engine-database-net/workflows/Sonarqube%20Analyze/badge.svg)](https://sonarcloud.io/dashboard?id=qatoolkit_qatoolkit-engine-database-net)
[![NuGet package](https://img.shields.io/nuget/v/QAToolKit.Engine.DataBase?label=QAToolKit.Engine.Database)](https://www.nuget.org/packages/QAToolKit.Engine.Database/)
[![Discord](https://img.shields.io/discord/787220825127780354?color=%23267CB9&label=Discord%20chat)](https://discord.gg/hYs6ayYQC5)

## Description

`QAToolKit.Engine.Database` is a .NET standard library, which can be used to do database fitness tests. For example, if
you want to test that table is present in database, or certain number of records exist in specific table or if a record
exists.

`DatabaseTestType` enumeration currently described those three test types:

- `ObjectExits`: Check if table, view or stored procedure exists.
- `RecordCount`: Check if record count in specific table equals an expression.
- `RecordExist`: Check if a record exists in specific table.
- `CustomScript`: Check if a custom script returns results. You can write a custom select query and check if it has any
  results.

Currently supports only relational databases: `SQLServer`, `MySQL` and `PostgreSQL`.

Get in touch with me on:

[![Discord](https://img.shields.io/discord/787220825127780354?color=%23267CB9&label=Discord%20chat)](https://discord.gg/hYs6ayYQC5)

## Generators and Runners

```csharp
var generator = new SqlServerTestGenerator(options =>
{
    options.AddDatabaseObjectExitsRule(new string[] { "mytable" }, DatabaseObjectType.Table);

    options.AddDatabaseRecordExitsRule(new List<RecordExistRule>()
    {
        new RecordExistRule()
        {
            TableName = "mytable",
            ColumnName = "name",
            Operator = "=",
            Value = "myname"
        }
    });

    options.AddDatabaseRecordsCountRule(new List<RecordCountRule>() 
    {
        new RecordCountRule() 
        {
            TableName = "mytable", 
            Count = 100,
            Operator = "=" 
        } 
    });

    options.AddCustomSqlRule(
        new List<string>()
        {
            "SELECT * FROM [table] WHERE timestamp = '2016-05-31'",
            "SELECT * FROM [table] WHERE value < 1"
        });
    
    //Currently only for SQL Server    
    options.CaptureQueryStatistics(new string[]
        {
            @"SELECT * FROM myTable1 INNER JOIN myTable ON myTable1.id=myTable.sampleID;"
        },
        new QueryStatisticsType[]
        {
            QueryStatisticsType.Time, QueryStatisticsType.Io
        });
});

List<DatabaseTest> scripts = await generator.Generate();
```

The code above will generate a SQLServer `DatabaseTest` list, which will be used by runner to run the tests against
database.

Above example adds all three test types to the generator:

- `AddDatabaseObjectExitsRule`: will check if a table `mytable` exists in the database.
- `AddDatabaseRecordExitsRule`: will check if a record in table `mytable` with `name` equals `myname` exists.
- `AddDatabaseRecordsCountRule`: will check if there is exactly 100 records in the `mytable` table.
- `AddCustomSqlRule`: will check if a custom script returns any results. This way you have a lot of freedom to define
  your own queries. Please note, that the query you specify is wrapped in the `EXISTS` clause for a specific database
  vendor.
- `CaptureQueryStatistics`: capture the `time` and/or `IO` statistics by running the specified queries. Runner will
  collect the results from
  the [InfoMessage event](https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlconnection.infomessage?view=dotnet-plat-ext-5.0)
  . This is currently implemented only for SQL Server.

Alternatively if you want to use `MySQL` or `PostgreSQL` generators, you can use MySqlTestGenerator` or `
PostgresqlTestGenerator` respectively.

To run the tests, we create a `SqlServerTestRunner` runner:

```csharp
var runner = new SqlServerTestRunner(scripts, options =>
{
    options.AddSQLServerConnection("server=localhost;user=user;password=mypassword;Initial Catalog=myDatabase");
});

List<DatabaseTestResult> results = await runner.Run();
```

With version 0.4.0 you can run runners in parallel, by specifying the number in Run() method:

```csharp
//Run in 5 parallels
var results = runner.Run(5);
```

Alternatively if you want to use `MySQL` or `PostgreSQL` runners, you can use MySqlTestRunner` or `PostgresqlTestRunner`
respectively.

Please note that **your user must have correct database permissions**. I suggest a read-only permissions that can also
access `sys` or `information_schema` schemas.

Below is a sample of database test result:

```json
[
    {
        "Id": "c9ac5d7f-d4fd-44e5-a743-837be2740b72",
        "Hash": "0x4E263AF99F6C0D3AB0268A249CC14E27",
        "RunAt": "2021-03-07T12:39:16.4181917+01:00",
        "DatabaseResult": null,
        "Variable": null,
        "Script": "SET STATISTICS TIME ON;SET STATISTICS IO ON;SELECT * FROM myTable1 INNER JOIN myTable ON myTable1.id=myTable.sampleID;SET STATISTICS TIME OFF;SET STATISTICS IO OFF;",
        "DatabaseTestType": 4,
        "DatabaseKind": 1,
        "ServerCpuTime": 234,
        "ServerElapsedTime": 494,
        "TotalElapsedTime": 887,
        "Statistics": {
            "Workfile": {
                ...
            },
            "Worktable": {
                ...
            },
            "myTable": {
                "ScanCount": 1,
                "LogicalReads": 1674,
                "PhysicalReads": 0,
                "PageServerReads": 0,
                "ReadAheadReads": 0,
                "PageServerReadAheadReads": 0,
                "LobLogicalReads": 0,
                "LobPhysicalReads": 0,
                "LobPageServerReads": 0,
                "LobReadAheadReads": 0,
                "LobPageServerReadAheadReads": 0
            },
            "myTable1": {
                "ScanCount": 1,
                "LogicalReads": 218,
                "PhysicalReads": 0,
                "PageServerReads": 0,
                "ReadAheadReads": 0,
                "PageServerReadAheadReads": 0,
                "LobLogicalReads": 0,
                "LobPhysicalReads": 0,
                "LobPageServerReads": 0,
                "LobReadAheadReads": 0,
                "LobPageServerReadAheadReads": 0
            }
        }
    }
]
```
A bit of explanations:

- `Id`: A unique GUID generated on the fly for every result.
- `Hash`: is a MurMurHash of the script string.
- `Statistics`: those are collected only if a `DatabaseTest` with `CaptureQueryStatistics` is used. It contains the parsed information retrieved from the SQL server (currently there is no support PostgreSQL or MySQL).

## Asserters

If you want to assert query statistics in version `0.4.0` and above, you can assert the results with `TestAsserter`.
Runner will return `DatabaseResult = null`, because it can not assert the vast arrays of results when executing a query.
You need to do that after you run your queries.

Sample code for asserter:

```csharp
var generator = new SqlServerTestGenerator(options =>
{ 
    options.CaptureQueryStatistics(new string[]
        {
            @"SELECT * FROM myTable1 INNER JOIN myTable ON myTable1.id=myTable.sampleID;"
        },
        new QueryStatisticsType[]
        {
            QueryStatisticsType.Time, QueryStatisticsType.Io
        });
});

List<DatabaseTest> scripts = await generator.Generate();
var runner = new SqlServerTestRunnerFactory(scripts,
    options => options.AddSQLServerConnection(
        "server=localhost;user=sa;password=password;Initial Catalog=MyTable"));
var results = await runner.Run();

var asserter = new TestAsserter(result)
                  .EvaluateTotalElapsedTime(x => x > 0 && x < 1500)
                  .EvaluateLogicalReads(x => x < 2000)//, "MyTable")
                  .WithCustomProperty(new KeyValuePair<string, string>("script",result.Script))
                  .AssertAll();
```

## To-Do

- Collect time / IO statistics for `PostgreSQL` and `MySQL` (currently throws `NotImplementedException`).

## Breaking changes

### version 0.3.0 to 0.4.0

- Renamed model names, by removing `Database` prefix from names, for example `DatabaseRecordExistRule` was renamed
  to `RecordExistRule`
- Renamed SQL runner to support parallel running. For example `SqlServerTestRunner` was renamed
  to `SqlServerTestRunnerFactory`.

## License

MIT License

Copyright (c) 2020-2021 Miha Jakovac

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
documentation files (the "Software"), to deal in the Software without restriction, including without limitation the
rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit
persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the
Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
