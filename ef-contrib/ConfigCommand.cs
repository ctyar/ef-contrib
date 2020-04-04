using System;
using System.IO;
using System.Text.Json;

namespace Ctyar.Ef.Contrib
{
    internal class ConfigCommand
    {
        public void Execute()
        {
            var config = new Config();

            Print.Info("The DbContext to use:");
            config.DbContext = Console.ReadLine();

            Print.Info("The project to use:");
            config.Project = Console.ReadLine();

            Print.Info("The startup project to use:");
            config.StartupProject = Console.ReadLine();
            
            WriteConfigFile(config);
        }

        internal Config ReadConfigFile()
        {
            var config = new Config();

            try
            {
                config = JsonSerializer.Deserialize<Config>(File.ReadAllText(GetConfigFilePath()));
            }
            catch(Exception e) when(e is FileNotFoundException || e is JsonException)
            {
            }

            return config;
        }

        private void WriteConfigFile(Config config)
        {
            var configFile = JsonSerializer.Serialize(config, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(GetConfigFilePath(), configFile);
        }

        private string GetConfigFilePath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), ".vscode", "ef-contrib.json");
        }
    }
}