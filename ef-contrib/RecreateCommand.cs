namespace Ctyar.Ef.Contrib
{
    internal class RecreateCommand : CommandBase
    {
        public void Execute()
        {
            var lastMigration = Migrations[^1];

            Print.Info("Recreating the last migration");

            Remove();

            AddMigration(lastMigration);

            Print.Info("Done");
        }
    }
}