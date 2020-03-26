namespace Ctyar.Ef.Contrib
{
    internal class RemoveCommand : CommandBase
    {
        public void Execute()
        {
            Print.Info("Removing the last migration");

            Remove();

            Print.Info("Done");
        }
    }
}