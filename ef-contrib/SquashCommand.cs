using System;
using System.Diagnostics;
using System.IO;

namespace Ctyar.Ef.Contrib
{
    internal class SquashCommand
    {
        public void Execute()
        {
            var lastMigration = "NewMigration";
            var secondToLastMigration = "ShareImage";
            var beforeSecondToLastMigration = "LandingPageData";

            PrintInfo($"Squashing last two migrations: {lastMigration}, {secondToLastMigration}");

            Remove(secondToLastMigration);

            Remove(beforeSecondToLastMigration);

            AddMigration(lastMigration);
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

        private void Execute(string command)
        {
            using var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = command,
                    CreateNoWindow = false,
                    WorkingDirectory = Directory.GetCurrentDirectory()
                }
            };

            process.Start();
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                Environment.Exit(process.ExitCode);
            }
        }

        private void PrintInfo(string message)
        {
            var previousColor = Console.ForegroundColor;
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(message);

            Console.ForegroundColor = previousColor;
        }
    }
}