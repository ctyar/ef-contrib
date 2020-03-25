using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Ctyar.Ef.Contrib
{
    internal class SquashCommand
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        private Config _config;
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        public void Execute()
        {
            var configCommand = new ConfigCommand();
            _config = configCommand.ReadConfigFile();

            var migrations = GetMigrations();

            var lastMigration = migrations[^1];
            var secondToLastMigration = migrations[^2];
            var thirdToLastMigration = migrations[^3];

            Print.Info($"Squashing last two migrations: {lastMigration}, {secondToLastMigration}");

            Remove(secondToLastMigration);

            Remove(thirdToLastMigration);

            AddMigration(lastMigration);

            Print.Info("Done");
        }

        private void Remove(string migrationName)
        {
            Print.Info($"Removing migration: {migrationName}");

            UpdateDatabase(migrationName);

            RemoveLastMigration();
        }

        private void RemoveLastMigration()
        {
            Print.Info("Removing last migration");

            Execute("ef migrations remove");
        }

        private void AddMigration(string migrationName)
        {
            Print.Info($"Adding new migration: {migrationName}");

            Execute($"ef migrations add {migrationName}");
        }

        private void UpdateDatabase(string migrationName)
        {
            Print.Info($"Updating database to : {migrationName}");

            Execute(
                $"ef database update {migrationName}");
        }

        private string[] GetMigrations()
        {
            Print.Info("Getting migrations list");

            var lines = GetCommandResult("ef migrations list");
            
            // Skip first two lines:
            // Build started...
            // Build succeeded.
            return lines.ToArray()[2..];
        }

        private void Execute(string command)
        {
            using var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = command + _config.ToString(),
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    WorkingDirectory = Directory.GetCurrentDirectory()
                }
            };

            process.Start();
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                Print.Error(process.StandardOutput.ReadToEnd());

                Environment.Exit(process.ExitCode);
            }
        }

        private List<string> GetCommandResult(string command)
        {
            using var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = command + _config.ToString(),
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    WorkingDirectory = Directory.GetCurrentDirectory()
                }
            };

            process.Start();
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                Print.Error(process.StandardOutput.ReadToEnd());

                Environment.Exit(process.ExitCode);
            }

            var result = new List<string>();
            while (!process.StandardOutput.EndOfStream)
            {
                var line = process.StandardOutput.ReadLine();

                if (line != null)
                {
                    result.Add(line);
                }
            }

            return result;
        }
    }
}