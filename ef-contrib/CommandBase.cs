using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Ctyar.Ef.Contrib
{
    internal abstract class CommandBase
    {
        private Config? _config;
        private List<string>? _migrations;

        protected List<string> Migrations => _migrations ??= GetMigrations();

        protected Config Config => _config ??= GetConfig();

        protected void Remove()
        {
            Print.Info($"Removing migration: {Migrations[^1]}");

            UpdateDatabase(Migrations[^2]);

            StartProcess("ef migrations remove");

            Migrations.RemoveAt(Migrations.Count - 1);
        }

        protected void AddMigration(string migrationName)
        {
            Print.Info($"Adding new migration: {migrationName}");

            var cleanName = CleanName(migrationName);

            StartProcess($"ef migrations add {cleanName}");

            UpdateDatabase(cleanName);

            Migrations.Add(cleanName);
        }

        protected void StartProcess(string command)
        {
            var finalCommand = command + Config;

            using var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = finalCommand,
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

        private List<string> GetMigrations()
        {
            var lines = GetProcessResult("ef migrations list");
            
            // Skip first two lines:
            // Build started...
            // Build succeeded.
            _migrations = lines.GetRange(2, lines.Count - 2);

            return _migrations.ToList();
        }

        private void UpdateDatabase(string migrationName)
        {
            Print.Info($"Updating database to: {migrationName}");

            StartProcess($"ef database update {migrationName}");
        }

        private Config GetConfig()
        {
            return new ConfigCommand().ReadConfigFile();
        }

        private List<string> GetProcessResult(string command)
        {
            var finalCommand = command + Config;

            using var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = finalCommand,
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

        private string CleanName(string migrationName)
        {
            var index = migrationName.IndexOf('_');
            return migrationName.Substring(index + 1);
        }
    }
}
