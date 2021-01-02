# QAToolKit Engine Database library
[![Build .NET Library](https://github.com/qatoolkit/qatoolkit-engine-database-net/workflows/Build%20.NET%20Library/badge.svg)](https://github.com/qatoolkit/qatoolkit-engine-database-net/actions)
[![CodeQL](https://github.com/qatoolkit/qatoolkit-engine-database-net/workflows/CodeQL%20Analyze/badge.svg)](https://github.com/qatoolkit/qatoolkit-engine-database-net/security/code-scanning)
[![Sonarcloud Quality gate](https://github.com/qatoolkit/qatoolkit-engine-database-net/workflows/Sonarqube%20Analyze/badge.svg)](https://sonarcloud.io/dashboard?id=qatoolkit_qatoolkit-engine-database-net)
[![NuGet package](https://img.shields.io/nuget/v/QAToolKit.Engine.DataBase?label=QAToolKit.Engine.Database)](https://www.nuget.org/packages/QAToolKit.Engine.Database/)
[![Discord](https://img.shields.io/discord/787220825127780354?color=%23267CB9&label=Discord%20chat)](https://discord.gg/hYs6ayYQC5)

## Description
`QAToolKit.Engine.Database` is a .NET standard library, which can be used to do database fitness tests. For example, if you want to test that table is present in database, or certain number of records exist in specific table or if a record exists.

`DatabaseTestType` enumeration currently described those three test types:
- `ObjectExits`: Check if table, view or stored procedure exists.
- `RecordCount`: Check if record count in specific table equals an expression.
- `RecordExist`: Check if a record exists in specific table.

Currently supports only relational databases: `SQLServer`, `MySQL` and `PostgreSQL`.

Get in touch with me on:

[![Discord](https://img.shields.io/discord/787220825127780354?color=%23267CB9&label=Discord%20chat)](https://discord.gg/hYs6ayYQC5)

## Sample

```csharp
var generator = new SqlServerTestGenerator(options =>
{
    options.AddDatabaseObjectExitsRule(new string[] { "mytable" }, DatabaseObjectType.Table);

    options.AddDatabaseRecordExitsRule(new List<DatabaseRecordExistRule>()
    {
        new DatabaseRecordExistRule()
        {
            TableName = "mytable",
            ColumnName = "name",
            Operator = "=",
            Value = "myname"
        }
    });

    options.AddDatabaseRecordsCountRule(new List<DatabaseRecordCountRule>() 
    {
        new DatabaseRecordCountRule() 
        {
            TableName = "mytable", 
            Count = 100,
            Operator = "=" 
        } 
    });
});

List<DatabaseTest> scripts = await generator.Generate();
```
The code above will generate a SQLServer `DatabaseTest` list, which will be used by runner to run the tests against database.

Above example adds all three test types to the generator:
- `AddDatabaseObjectExitsRule`: will check if a table `mytable` exists in the database.
- `AddDatabaseRecordExitsRule`: will check if a record in table `mytable` with `name` equals `myname` exists.
- `AddDatabaseRecordsCountRule`: will check if there is exactly 100 records in the `mytable` table.

Alternatively if you want to use `MySQL` or `PostgreSQL` generators, you can use MySqlTestGenerator` or `PostgresqlTestGenerator` respectively.

To run the tests, we create a `SqlServerTestRunner` runner:

```csharp
var runner = new SqlServerTestRunner(scripts, options =>
{
    options.AddSQLServerConnection("server=localhost;user=user;password=mypassword;Initial Catalog=myDatabase");
});

List<DatabaseTestResult> results = await runner.Run();
```

Alternatively if you want to use `MySQL` or `PostgreSQL` runners, you can use MySqlTestRunner` or `PostgresqlTestRunner` respectively.

Please note that **your user must have correct database permissions**. I suggest a read-only permissions that can also access `sys` or `information_schema` schemas.

## To-Do

- Add more test types if necessary.

## License

MIT License

Copyright (c) 2020-2021 Miha Jakovac

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
