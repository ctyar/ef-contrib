using System.CommandLine;
using System.CommandLine.Invocation;

namespace Ctyar.Ef.Contrib
{
    internal class Program
    {
        private static int Main(string[] args)
        {
            var rootCommand = new RootCommand();

            var squashCommand = new Command("--squash")
            {
                Handler = CommandHandler.Create(Squash),
                Description = "Merges last two migrations"
            };
            squashCommand.AddAlias("-s");

            rootCommand.Add(squashCommand);

            return rootCommand.Invoke(args);
        }

        private static void Squash()
        {
            var squashCommand = new SquashCommand();

            squashCommand.Execute();
        }
    }
}