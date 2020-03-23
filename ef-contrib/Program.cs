using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Diagnostics;

namespace Ctyar.Ef.Contrib
{
    internal class Program
    {
        private static int Main(string[] args)
        {
            var rootCommand = new RootCommand
            {
                Handler = CommandHandler.Create(Squash)
            };

            /*var squashCommand = new Command("--squash")
            {
                Handler = CommandHandler.Create(Squash)
            };
            squashCommand.AddAlias("-s");

            rootCommand.Add(squashCommand);*/

            return rootCommand.Invoke(args);
        }

        private static void Squash()
        {
            const string workingDirectory = @"C:\Shahriar\Projects\whynotearth\meredith-core";

            var lastMigration = "NewMigration";
            var secondToLastMigration = "ShareImage";
            var beforeSecondToLastMigration = "LandingPageData";

            PrintInfo($"Squashing last two migrations: {lastMigration}, {secondToLastMigration}");

            Remove(secondToLastMigration, workingDirectory);

            Remove(beforeSecondToLastMigration, workingDirectory);

            AddMigration(lastMigration, workingDirectory);
        }

        private static void Remove(string migrationName, string workingDirectory)
        {
            PrintInfo($"Removing migration: {migrationName}");

            UpdateDatabase(migrationName, workingDirectory);

            RemoveLastMigration(workingDirectory);
        }

        private static void RemoveLastMigration(string workingDirectory)
        {
            PrintInfo("Removing last migration");

            Execute("ef migrations remove -p WhyNotEarth.Meredith.Data.Entity -s WhyNotEarth.Meredith.App", workingDirectory);
        }

        private static void AddMigration(string migrationName, string workingDirectory)
        {
            PrintInfo($"Adding new migration: {migrationName}");

            Execute($"ef migrations add {migrationName} -p WhyNotEarth.Meredith.Data.Entity -s WhyNotEarth.Meredith.App", workingDirectory);
        }

        private static void UpdateDatabase(string migrationName, string workingDirectory)
        {
            PrintInfo($"Updating database to : {migrationName}");

            Execute(
                $"ef database update {migrationName} -p WhyNotEarth.Meredith.Data.Entity -s WhyNotEarth.Meredith.App",
                workingDirectory);
        }

        private static void Execute(string command, string workingDirectory)
        {
            using var process = new System.Diagnostics.Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = command,
                    CreateNoWindow = false,
                    WorkingDirectory = workingDirectory
                }
            };

            process.Start();
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                Environment.Exit(process.ExitCode);
            }
        }

        private static void PrintInfo(string message)
        {
            var previousColor = Console.ForegroundColor;
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(message);

            Console.ForegroundColor = previousColor;
        }
    }
}