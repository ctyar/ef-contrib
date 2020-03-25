using System.Text;

namespace Ctyar.Ef.Contrib
{
    internal class Config
    {
        public string? DbContext { get; set; }

        public string? Project { get; set; }

        public string? StartupProject { get; set; }

        public override string ToString()
        {
            var result = new StringBuilder();

            if (!string.IsNullOrEmpty(DbContext))
            {
                result.Append($" --context {DbContext}");
            }

            if (!string.IsNullOrEmpty(Project))
            {
                result.Append($" --project {Project}");
            }

            if (!string.IsNullOrEmpty(StartupProject))
            {
                result.Append($" --startup-project {StartupProject}");
            }

            return result.ToString();
        }
    }
}
