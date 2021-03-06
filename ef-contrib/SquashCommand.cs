﻿namespace Ctyar.Ef.Contrib
{
    internal class SquashCommand : CommandBase
    {
        public void Execute()
        {
            var lastMigration = Migrations[^1];
            var secondToLastMigration = Migrations[^2];

            Print.Info($"Squashing last two migrations: {lastMigration}, {secondToLastMigration}");

            Remove();

            Remove();

            AddMigration(lastMigration);

            Print.Info("Done");
        }
    }
}