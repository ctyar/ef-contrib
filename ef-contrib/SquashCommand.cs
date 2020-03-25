using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Ctyar.Ef.Contrib
{
    internal class SquashCommand
    {
        public void Execute()
        {
            var migrations = GetMigrations();

            var lastMigration = migrations[^1];
            var secondToLastMigration = migrations[^2];
            var beforeSecondToLastMigration = migrations[^3];

            PrintInfo($"Squashing last two migrations: {lastMigration}, {secondToLastMigration}");

            Remove(secondToLastMigration);

            Remove(beforeSecondToLastMigration);

            AddMigration(lastMigration);

            PrintInfo("Done");
        }

        private void Remove(string migrationName)
        {
            PrintInfo($"Removing migration: {migrationName}");

            UpdateDatabase(migrationName);

            RemoveLastMigration();
        }

        private void RemoveLastMigration()
        {
            PrintInfo("Removing last migration");

            Execute("ef migrations remove -p WhyNotEarth.Meredith.Data.Entity -s WhyNotEarth.Meredith.App");
        }

        private void AddMigration(string migrationName)
        {
            PrintInfo($"Adding new migration: {migrationName}");

            Execute($"ef migrations add {migrationName} -p WhyNotEarth.Meredith.Data.Entity -s WhyNotEarth.Meredith.App");
        }

        private void UpdateDatabase(string migrationName)
        {
            PrintInfo($"Updating database to : {migrationName}");

            Execute(
                $"ef database update {migrationName} -p WhyNotEarth.Meredith.Data.Entity -s WhyNotEarth.Meredith.App");
        }

        private string[] GetMigrations()
        {
            PrintInfo("Getting migrations list");

            var lines = GetCommandResult("ef migrations list -p WhyNotEarth.Meredith.Data.Entity -s WhyNotEarth.Meredith.App");
            
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
                    Arguments = command,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    WorkingDirectory = Directory.GetCurrentDirectory()
                }
            };

            process.Start();
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                PrintError(process.StandardOutput.ReadToEnd());

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
                    Arguments = command,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    WorkingDirectory = Directory.GetCurrentDirectory()
                }
            };

            process.Start();
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                PrintError(process.StandardOutput.ReadToEnd());

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

        private void PrintInfo(string message)
        {
            var previousColor = Console.ForegroundColor;
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(message);

            Console.ForegroundColor = previousColor;
        }

        private void PrintError(string message)
        {
            var previousColor = Console.ForegroundColor;
            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);

            Console.ForegroundColor = previousColor;
        }
    }
}