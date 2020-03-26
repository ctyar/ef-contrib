namespace Ctyar.Ef.Contrib
{
    internal abstract class CommandBase
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        private Config _config;
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        protected Config Config
        {
            get
            {
                if (_config is null)
                {
                    var configCommand = new ConfigCommand();
                    _config = configCommand.ReadConfigFile();
                }

                return _config;
            }
        }
    }
}
