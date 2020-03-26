using System;
using System.Diagnostics;
using System.IO;

namespace Ctyar.Ef.Contrib
{
    internal class EfCommand : CommandBase
    {
        public void Execute(string[] arguments)
        {
            var command = string.Join(" ", arguments) + Config;

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
                Print.Error(process.StandardOutput.ReadToEnd());

                Environment.Exit(process.ExitCode);
            }
        }
    }
}