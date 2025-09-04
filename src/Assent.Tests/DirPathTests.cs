using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;

namespace Assent.Tests;

public static class DirPathTests
{
    public class NoSubDirs
    {
        [Test]
        public void SuccessWithValueIfEnvVarSetAndDirExists()
        {
            var result = DirPath.TryGetFromEnvironment("LOCALAPPDATA", [], out var value,
                getEnvironmentVariable: _ => "localAppDataFolder",
                directoryExists: _ => true);

            result.Should().BeTrue();
            value.Should().Be("localAppDataFolder");
        }

        [Test]
        public void FailIfEnvVarNotSet()
        {
            var result = DirPath.TryGetFromEnvironment("LOCALAPPDATA", [], out var value,
                getEnvironmentVariable: _ => null,
                directoryExists: _ => throw new InvalidOperationException("should not get here"));

            result.Should().BeFalse();
            value.Should().BeNull();
        }

        [Test]
        public void FailIfEnvVarIsEmpty()
        {
            var result = DirPath.TryGetFromEnvironment("LOCALAPPDATA", [], out var value,
                getEnvironmentVariable: _ => "",
                directoryExists: _ => throw new InvalidOperationException("should not get here"));

            result.Should().BeFalse();
            value.Should().BeNull();
        }

        [Test]
        public void FailIfEnvVarSetAndDirDoesNotExist()
        {
            var result = DirPath.TryGetFromEnvironment("LOCALAPPDATA", [], out var value,
                getEnvironmentVariable: _ => "localAppDataFolder",
                directoryExists: _ => false);

            result.Should().BeFalse();
            value.Should().BeNull();
        }
    }
    
    public class CombiningSubDirs
    {
        [Test]
        public void SuccessWithValueIfEnvVarSetAndDirExists()
        {
            var result = DirPath.TryGetFromEnvironment("LOCALAPPDATA", ["foo", "bar"], out var value,
                getEnvironmentVariable: _ => "localAppDataFolder",
                directoryExists: _ => true);

            result.Should().BeTrue();
            value.Should().Be(Path.Combine("localAppDataFolder", "foo", "bar"));
        }

        [Test]
        public void FailIfEnvVarNotSet()
        {
            var result = DirPath.TryGetFromEnvironment("LOCALAPPDATA", ["foo", "bar"], out var value,
                getEnvironmentVariable: _ => null,
                directoryExists: _ => throw new InvalidOperationException("should not get here"));

            result.Should().BeFalse();
            value.Should().BeNull();
        }

        [Test]
        public void FailIfEnvVarIsEmpty()
        {
            var result = DirPath.TryGetFromEnvironment("LOCALAPPDATA", ["foo", "bar"], out var value,
                getEnvironmentVariable: _ => "",
                directoryExists: _ => throw new InvalidOperationException("should not get here"));

            result.Should().BeFalse();
            value.Should().BeNull();
        }

        [Test]
        public void FailIfEnvVarSetAndDirDoesNotExist()
        {
            var result = DirPath.TryGetFromEnvironment("LOCALAPPDATA", ["foo", "bar"], out var value,
                getEnvironmentVariable: _ => "localAppDataFolder",
                directoryExists: d =>
                {
                    // it should check the existence of the combined subdir, NOT the base dir
                    d.Should().Be(Path.Combine("localAppDataFolder", "foo", "bar"));
                    return false;
                });

            result.Should().BeFalse();
            value.Should().BeNull();
        }
    }
}