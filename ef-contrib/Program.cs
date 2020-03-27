using System.CommandLine;
using System.CommandLine.Invocation;

namespace Ctyar.Ef.Contrib
{
    internal class Program
    {
        public static int Main(string[] args)
        {
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

            var addCommand = new Command("add")
            {
                Handler = CommandHandler.Create<string>(Add),
                Description = "Adds a new migration"
            };
            // Keep this name and type in sync with Add method's argument
            var addArgument = new Argument<string>("migrationName")
            {
                Description = "Migration name to add"
            };
            addCommand.AddArgument(addArgument);

            var removeCommand = new Command("remove")
            {
                Handler = CommandHandler.Create(Remove),
                Description = "Removes the last migration"
            };

            var efCommand = new Command("ef")
            {
                Handler = CommandHandler.Create<string[]>(Ef),
                Description = "Call ef command directly with your specified configs"
            };
            // Keep this name and type in sync with Add method's argument
            var efArgument = new Argument<string[]>("arguments")
            {
                Description = "Arguments"
            };
            efCommand.AddArgument(efArgument);

            var configCommand = new Command("config")
            {
                Handler = CommandHandler.Create(Config),
                Description = "Adds a config file with default project info"
            };
            
            rootCommand.Add(recreateCommand);
            rootCommand.Add(squashCommand);
            rootCommand.Add(addCommand);
            rootCommand.Add(removeCommand);
            rootCommand.Add(efCommand);
            rootCommand.Add(configCommand);

            return rootCommand.Invoke(args);
        }

        private static void Recreate()
        {
            new RecreateCommand().Execute();
        }

        private static void Squash()
        {
            new SquashCommand().Execute();
        }

        private static void Add(string migrationName)
        {
            new AddCommand().Execute(migrationName);
        }

        private static void Remove()
        {
            new RemoveCommand().Execute();
        }

        private static void Config()
        {
            new ConfigCommand().Execute();
        }

        private static void Ef(string[] arguments)
        {
            new EfCommand().Execute(arguments);
        }
    }
}