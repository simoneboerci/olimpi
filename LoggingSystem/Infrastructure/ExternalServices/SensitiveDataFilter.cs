using LoggingSystem.Core.Domain.Interfaces;

namespace LoggingSystem.Infrastructure.ExternalServices
{
    public class SensitiveDataFilter(HashSet<string>? sensitiveWords = null) : ISensitiveDataFiler
    {
        private readonly HashSet<string> _sensitiveWords = sensitiveWords ?? ["password", "apiKey", "secret"];

        public string ApplyCensorship(string message, bool isCensored)
        {
            if (!isCensored) return message;

            foreach (var word in _sensitiveWords)
            {
                message = System.Text.RegularExpressions.Regex.Replace(message,
                    $@"(?<={word}=)[^\s&]+",
                    m => new string('*', m.Length),
                    System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            }

            return message;
        }
    }
}