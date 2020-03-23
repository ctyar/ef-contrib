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

            Remove("ShareImage", workingDirectory);

            Remove("LandingPageData", workingDirectory);

            AddMigration("NewMigration", workingDirectory);
        }

        private static void Remove(string migrationName, string workingDirectory)
        {
            UpdateDatabase(migrationName, workingDirectory);

            RemoveLastMigration(workingDirectory);
        }

        private static void RemoveLastMigration(string workingDirectory)
        {
            Execute("ef migrations remove -p WhyNotEarth.Meredith.Data.Entity -s WhyNotEarth.Meredith.App", workingDirectory);
        }

        private static void AddMigration(string migrationName, string workingDirectory)
        {
            Execute($"ef migrations add {migrationName} -p WhyNotEarth.Meredith.Data.Entity -s WhyNotEarth.Meredith.App", workingDirectory);
        }

        private static void UpdateDatabase(string migrationName, string workingDirectory)
        {
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
        }
    }
}