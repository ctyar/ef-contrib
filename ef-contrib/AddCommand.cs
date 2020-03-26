namespace Ctyar.Ef.Contrib
{
    internal class AddCommand : CommandBase
    {
        public void Execute(string migrationName)
        {
            Print.Info($"Adding a new migration: {migrationName}");

            AddMigration(migrationName);

            Print.Info("Done");
        }
    }
}