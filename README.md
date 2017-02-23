# Assent

[![Build status](https://ci.appveyor.com/api/projects/status/dnnn06mquuudqpkm/branch/master?svg=true)](https://ci.appveyor.com/project/droyad/assent/branch/master)

Assent is a simple assertion library for long strings. By default it 
uses common diff tools to report on and resolve test failures.

It is test framework agnostic and works with `.NET Framework 4.5`, 
`NET Standard 1.3` and `.NET Core 1.0`.

## Installation
Install the package from nuget by adding `Assent` to your `project.json` dependencies
or running `install-package Assent` from the NuGet tool window.

## Usage
In your test, do your usual setup and execution, but replace your assert
line with:

```c#
this.Assent("String To Assert")
```

The behaviour can be customised by passing a `Configuration` object:
```c#
var configuration = configuration.UsingExtension("json");
this.Assent("String To Assert", configuration);
```

You can use your favourite assertion library, see the [Comparer](https://github.com/droyad/Assent/wiki/Comparison) wiki page.

When the test is run, it will look for an approved file in the same directory as the code file. See [Naming](https://github.com/droyad/Assent/wiki/Naming) on how this is achieved and how to customise this. On the first run, an empty `approved` file is created and the diff tool will show, allowing you to validate the tests result and copy it over to the `approved` file. For more detail, see [How It Works](https://github.com/droyad/Assent/wiki/How-It-Works)

## Automated Builds
Having a diff tool pop up during an automated build is not ideal, so it can be disabled by setting the `AssentNonInteractive` environment variable to `true`.

## Documentation
Refer to the [Wiki](https://github.com/droyad/Assent/wiki)

## Hat Tips
.NET Core Cake build scripts from [https://github.com/michael-wolfenden/dotnetcore-build](https://github.com/michael-wolfenden/dotnetcore-build)

Inspiration is drawn from [ApprovalTests](https://github.com/approvals/ApprovalTests.Net), 
if you need a more powerful tool, and you are using the .NET Framework, it is worth a look.

AppVeyor for supporting OSS and providing free CI builds
