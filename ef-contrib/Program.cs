using System.CommandLine;
using System.CommandLine.Invocation;

namespace Ctyar.Ef.Contrib
{
    internal class Program
    {
        public static int Main(string[] args)
        {
            if (args[0].ToLower() == "ef")
            {
                Ef(args);

                return 0;
            }

            var rootCommand = new RootCommand();

            var recreateCommand = new Command("recreate")
            {
                Handler = CommandHandler.Create(Recreate),
                Description = "Recreates the last migration"
            };

            var squashCommand = new Command("squash")
            {
                Handler = CommandHandler.Create(Squash),
                Description = "Merges last two migrations"
            };

            var configCommand = new Command("config")
            {
                Handler = CommandHandler.Create(Config),
                Description = "Adds a config file with default project info"
            };
            
            rootCommand.Add(recreateCommand);
            rootCommand.Add(squashCommand);
            rootCommand.Add(configCommand);

            return rootCommand.Invoke(args);
        }

        private static void Recreate()
        {
            var recreateCommand = new RecreateCommand();

            recreateCommand.Execute();
        }

        private static void Squash()
        {
            var squashCommand = new SquashCommand();

            squashCommand.Execute();
        }

        private static void Config()
        {
            var configCommand = new ConfigCommand();

            configCommand.Execute();
        }

        private static void Ef(string[] arguments)
        {
            var efCommand = new EfCommand();

            efCommand.Execute(arguments);
        }
    }
}