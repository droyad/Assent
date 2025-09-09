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
            var value = DirPath.GetFromEnvironmentOrNull("LOCALAPPDATA", [],
                getEnvironmentVariable: _ => "localAppDataFolder",
                directoryExists: _ => true);

            value.Should().Be("localAppDataFolder");
        }

        [Test]
        public void FailIfEnvVarNotSet()
        {
            var value = DirPath.GetFromEnvironmentOrNull("LOCALAPPDATA", [],
                getEnvironmentVariable: _ => null,
                directoryExists: _ => throw new InvalidOperationException("should not get here"));

            value.Should().BeNull();
        }

        [Test]
        public void FailIfEnvVarIsEmpty()
        {
            var value = DirPath.GetFromEnvironmentOrNull("LOCALAPPDATA", [],
                getEnvironmentVariable: _ => "",
                directoryExists: _ => throw new InvalidOperationException("should not get here"));

            value.Should().BeNull();
        }

        [Test]
        public void FailIfEnvVarSetAndDirDoesNotExist()
        {
            var value = DirPath.GetFromEnvironmentOrNull("LOCALAPPDATA", [],
                getEnvironmentVariable: _ => "localAppDataFolder",
                directoryExists: _ => false);

            value.Should().BeNull();
        }
    }
    
    public class CombiningSubDirs
    {
        [Test]
        public void SuccessWithValueIfEnvVarSetAndDirExists()
        {
            var value = DirPath.GetFromEnvironmentOrNull("LOCALAPPDATA", ["foo", "bar"],
                getEnvironmentVariable: _ => "localAppDataFolder",
                directoryExists: _ => true);

            value.Should().Be(Path.Combine("localAppDataFolder", "foo", "bar"));
        }

        [Test]
        public void FailIfEnvVarNotSet()
        {
            var value = DirPath.GetFromEnvironmentOrNull("LOCALAPPDATA", ["foo", "bar"],
                getEnvironmentVariable: _ => null,
                directoryExists: _ => throw new InvalidOperationException("should not get here"));

            value.Should().BeNull();
        }

        [Test]
        public void FailIfEnvVarIsEmpty()
        {
            var value = DirPath.GetFromEnvironmentOrNull("LOCALAPPDATA", ["foo", "bar"],
                getEnvironmentVariable: _ => "",
                directoryExists: _ => throw new InvalidOperationException("should not get here"));

            value.Should().BeNull();
        }

        [Test]
        public void FailIfEnvVarSetAndDirDoesNotExist()
        {
            var value = DirPath.GetFromEnvironmentOrNull("LOCALAPPDATA", ["foo", "bar"],
                getEnvironmentVariable: _ => "localAppDataFolder",
                directoryExists: d =>
                {
                    // it should check the existence of the combined subdir, NOT the base dir
                    d.Should().Be(Path.Combine("localAppDataFolder", "foo", "bar"));
                    return false;
                });

            value.Should().BeNull();
        }
    }
}