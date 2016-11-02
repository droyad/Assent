# Assent

[![Build status](https://ci.appveyor.com/api/projects/status/dnnn06mquuudqpkm/branch/master?svg=true)](https://ci.appveyor.com/project/droyad/assent/branch/master)

Assent is a simple assertion library for long strings. By default it 
uses common diff tools to report on and resolve test failures.

It is test framework agnostic and works with `.NET Framework 4.5`, 
`NET Standard 1.3` and `.NET Core 1.0`.

## Getting Started
### Installation
Install the package from nuget by adding `Assent` to your `project.json` dependencies
or running `install-package Assent` from the NuGet tool window.

### Usage
In your test, do your usual setup and execution, but replace your assert
line with:

```
this.Assent("String To Assert")
```

You can use your favourite assertion library, see the [Comparer](#comparer) section.

### Execution
When the test is run, it will look for an approval file in the same 
directory as the code file. See the [Naming](#naming) on how this is achieved
and how to customise this. On the first run, an empty approval file is created.

If the received value does not match the value in
the `approved` file, a `received` file is written and the diff tool launched
comparing the `received` and `approved` files. The test execution will resume
once the diff tool is closed.

You can then compare the two files and determine whether there is an 
error in the code, or the `approved` file needs updating. If it is the 
latter, use the diff tool to update and save the `approved` file. If
this is done during the test execution, and the files now match, the test will
pass. Otherwise it will fail.

## Configuration
The behaviour of Assent can be customised by passing a `Configuration` object
to the `Assent` method:

```
var configuration = new Configuration();
this.Assent("String To Assert", configuration);
```

`Configuration` is immutable, so a configuration can be maintained for
a suit of tests, and then customised individual tests. Instance methods
return a new mutated instance of `Configuration`, similar to LINQ methods.
For example:

```
Configuration reusedConfiguration = new Configuration()
	.UsingComparer(new CustomComparer());

public void Test()
{
    var testConfiguration = reusedConfiguration.UsingExtension("json");
    this.Assent("String To Assert", testConfiguration);
}
```

### Naming
By default the approved and received files are named in the following format:

```
{sourceFileDirectory}\{TypeName}.{MethodName}.{approved|received}.{extension}
``` 

The `this.Assent()` method call captures the test class instance (`this`),
method name and source file location.

The naming (ie the directory and filename) can be customised by implementing
`INamer` and configuring it:

```
configuration
    .WithNamer(new CustomNamer())
    .UsingExtension(".json");
```

### Comparer
By default a simple string comparison is used to compare the `received`
and `approved` values. This can be customised by:

Implementing `IComparer<string>`:
```
public class CustomComparer : IComparer<string>
{
    public CompareResult Compare(string received, string approved)
    {
        return received == approved 
            ? CompareResult.Pass()
            : CompareResult.Fail("Strings differ");
    }
}

configuration.UsingComparer(new CustomComparer);
```

A delegate that throws an exception (example uses [FluentAssertions](http://www.fluentassertions.com/))
```
configuration.UsingComparer((recieved, approved) => recieved.Should().Be(approved));
```

A delegate that returns a `CompareResult`
```
configuration.UsingComparer(
    (recieved, approved) => received == approved 
        ? CompareResult.Pass()
        : CompareResult.Fail("Strings differ");
);
```

### Reporter
The reporter behaviour can also be modified to use something other than 
a diff tool (for example a logger) by implementing `IReporter`:

```
configuration.UsingReporter(new LogReporter());
```

New diff programs can be added or the order thereof changed by
passing a new instance of `DiffReporter`:

```
var programs = new[] {
                    new AnotherDiffProgram()
                }
                .Concat(DiffReporter.DefaultDiffPrograms)
                .ToArray();
var reporter = new DiffReporter(programs);
configuration.UsingReporter(reporter);
```

## Hat Tips
.NET Core Cake build scripts from [https://github.com/michael-wolfenden/dotnetcore-build](https://github.com/michael-wolfenden/dotnetcore-build)

Inspiration is drawn from [ApprovalTests](https://github.com/approvals/ApprovalTests.Net), 
if you need a more powerful tool, and you are using the .NET Framework, it is worth a look.

AppVeyor for supporting OSS and providing free CI builds
